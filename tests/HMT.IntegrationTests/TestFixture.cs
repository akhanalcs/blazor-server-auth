using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using HMT.Web.Server.Data;
using Respawn;

namespace HMT.IntegrationTests
{
    [CollectionDefinition(nameof(TestFixture))]
    public class TestFixtureCollection : ICollectionFixture<TestFixture> { }

    public class TestFixture : IAsyncLifetime
    {
        private readonly IConfiguration _configuration;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly WebApplicationFactory<Program> _factory;
        // Respawn is a small utility to help in resetting test databases to a clean state.
        // Instead of deleting data at the end of a test or rolling back a transaction,
        // Respawn resets the database back to a clean checkpoint by intelligently deleting data from tables.
        private Respawner _respawner; // <-- Comes from Respawn library. 

        public TestFixture()
        {
            _factory = new MMTTestApplicationFactory();
            _configuration = _factory.Services.GetRequiredService<IConfiguration>();
            _scopeFactory = _factory.Services.GetRequiredService<IServiceScopeFactory>();
        }

        // Whenever this method is called:
        // 1. It calls ExecuteScopeAsync which expects ServiceProvider (sp) and returns Task<T>.
        // 2. ExecuteScopeAsync creates the scope that the action needs (sp a.k.a ServiceProvider in this case, which is used to resolve IMediator service.)
        // 3. From ExecuteScopeAsync, it calls the action (declared inside ExecuteScopeAsync) by providing sp.
        // 4. medaitor service is resolved, and mediator sends the request provided to this method.
        // 5. Inside ExecuteScopeAsync, "var result = await action(scope.ServiceProvider)" will hold the response that is the result of "return mediator.Send(request)".
        // 6. Result of step 5 will be provided to whoever called this method.
        public Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
        {
            return ExecuteScopeAsync(sp =>
            {
                var mediator = sp.GetRequiredService<IMediator>();
                return mediator.Send(request);
            });
        }

        // Basically you call this method to get service provider, so that you can resolve your services (for eg: IMediator, DbContext etc.) and call your action.
        public async Task<T> ExecuteScopeAsync<T>(Func<IServiceProvider, Task<T>> action)
        {
            using var scope = _scopeFactory.CreateScope();

            try
            {
                var result = await action(scope.ServiceProvider); // This just provides service provider to whatever the action that was sent here.
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        // For eg: This can be called to directly hit your test database like this from your tests:
        // var repairOrder = await _fixture.ExecuteDbContextAsync(dbCxt => dbCxt.RepairOrders.Where(ro => c.OrderId == 123).FirstOrDefaultAsync());
        public Task<T> ExecuteDbContextAsync<T>(Func<HMTDbContext, Task<T>> action)
        {
            return ExecuteScopeAsync(sp => action(sp.GetService<HMTDbContext>()));
        }

        public async Task InitializeAsync()
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            _respawner = await Respawner.CreateAsync(connectionString);
            await _respawner.ResetAsync(connectionString);
        }

        public Task DisposeAsync()
        {
            _factory?.Dispose();
            return Task.CompletedTask;
        }
    }

    public class MMTTestApplicationFactory
    : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureAppConfiguration((_, configBuilder) =>
            {
                configBuilder.AddInMemoryCollection(new Dictionary<string, string>
                {
                    {"ConnectionStrings:DefaultConnection", _connectionString}
                });
            });
        }

        private readonly string _connectionString = "Server=(localdb)\\mssqllocaldb;Database=HandyMansToolDb-Test;Trusted_Connection=True;MultipleActiveResultSets=true";
    }
}
