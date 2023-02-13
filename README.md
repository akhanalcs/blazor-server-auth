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
   
## Branch: feature/AddADAuthentication
This branch builds up on `feature/AddUserManagementPages` branch and adds capability to do Authentication using Local Active Directory.

This answer explains what I'm doing here: https://stackoverflow.com/a/74734478/8644294

