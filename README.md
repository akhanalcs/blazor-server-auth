# Blazor Server Identity Examples
This repo has examples related to doing Auth in Blazor Server.
You can navigate to different branches to try different scenarios.

It's a basic Blazor Server app that comes out of the box (You can use Visual Studio or command line to create it).

I just added Identity to it following directions from [here](https://learn.microsoft.com/en-us/aspnet/core/security/authentication/scaffold-identity?view=aspnetcore-7.0&tabs=visual-studio#scaffold-identity-into-a-blazor-server-project).

## Getting started
1. Clone the repo.
2. Launch the app. It will initialize the Db on Startup using `Data/DbInitializer.cs`
4. If you want to login as a test user, the credentials are inside: `Data/DbInitializer.cs` file. 
   
   For eg: Email -> handyman@coolapp, Password -> Password123!
   
## Branch: feature/AddClaimsDuringLogin
This branch builds up on _feature/LayoutWithIdentityPages_ to show how to add Claims during Login (in [`Login.cshtml.cs`](https://github.com/affableashish/blazor-server-auth/blob/bab11ae89e3e3f1120b523f8688ca91c3ff71dff/src/HMT.Web.Server/Areas/Identity/Pages/Account/Login.cshtml.cs#L121) file) and access those claims from [Razor Component](https://github.com/affableashish/blazor-server-auth/blob/bab11ae89e3e3f1120b523f8688ca91c3ff71dff/src/HMT.Web.Server/Areas/Identity/Components/TakeABreak.razor#L17).

When you launch the app, login and click 'Break' and add a breakpoint in [this line](https://github.com/affableashish/blazor-server-auth/blob/57b8e844632cc0b26023d5a065a489fe032b457e/src/HMT.Web.Server/Areas/Identity/Components/TakeABreak.razor#L17), you should get the new claim set in [this line](https://github.com/affableashish/blazor-server-auth/blob/57b8e844632cc0b26023d5a065a489fe032b457e/src/HMT.Web.Server/Areas/Identity/Pages/Account/Login.cshtml.cs#L121).
<img width="1856" alt="image" src="https://user-images.githubusercontent.com/30603497/217914365-10413758-780f-4c46-94a3-1783f2ade15a.png">


It doesn't work currently.
