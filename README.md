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
   
## Branch: feature/LayoutWithIdentityPages
This is the end result of me following the documentation and figuring out the rest of the missing pieces in the documentation. 
The missing pieces are:
  1. [How to fit the Identity Razor Pages in the Blazor app to give consistent look and feel when the user navigates between Razor Pages (coming from Identity Nuget Package) and Razor Components.](https://github.com/dotnet/aspnetcore/issues/45174#issuecomment-1325657322)
  2. [How to implement Revalidation.](https://github.com/dotnet/aspnetcore/issues/45174#issuecomment-1332517355)
  3. [How to implement `RedirectToLogin` so users are directed to Login page if they aren't authenticated and need to access authenticated page.](https://github.com/dotnet/aspnetcore/issues/45174#issuecomment-1346978198)
