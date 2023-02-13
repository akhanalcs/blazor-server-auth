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
   
## Branch: feature/AddUserManagementPages
This branch builds up on `feature/LayoutWithIdentityPages` branch and adds pages to manage users, roles and permissions.
This follows the concepts explained by Jason Taylor in [his video](https://youtu.be/OW5wBERdhQU).
Just launch the app. The credentilas are in the `Data/DbInitializer.cs` file. Use them to login as admin and you can see these pages:
<img width="1857" alt="image" src="https://user-images.githubusercontent.com/30603497/217652210-5e393041-b3df-4489-b6bc-478acde7ac38.png">
<img width="1863" alt="image" src="https://user-images.githubusercontent.com/30603497/217652296-60a8d732-a74e-4561-8ae7-6b2fac9749b5.png">
<img width="1861" alt="image" src="https://user-images.githubusercontent.com/30603497/217652374-72ea73cf-4d2f-4ae4-886e-0c1c34c714df.png">

I'll write more about how I implemented it (step-by-step) at a later date when I get some time.
