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
  1. [How to fit the Identity Razor Pages in the Blazor app to give consistent look and feel when the user navigates between Razor Pages (coming from Identity Razor Package) and Razor Components.](https://github.com/dotnet/aspnetcore/issues/45174#issuecomment-1325657322)
  2. [How to implement Revalidation.](https://github.com/dotnet/aspnetcore/issues/45174#issuecomment-1332517355)
  3. [How to implement `RedirectToLogin` so users are directed to Login page if they aren't authenticated and need to access authenticated page.](https://github.com/dotnet/aspnetcore/issues/45174#issuecomment-1346978198)


## Branch: feature/AddUserManagementPages
This branch builds up on the previous branch and adds pages to manage users, roles and permissions.
This follows the concepts explained by Jason Taylor in [his video](https://youtu.be/OW5wBERdhQU).
Just launch the app. The credentials are in the `Data/DbInitializer.cs` file. Use them to login as admin and you can see these pages:
<img width="1857" alt="image" src="https://user-images.githubusercontent.com/30603497/217652210-5e393041-b3df-4489-b6bc-478acde7ac38.png">
<img width="1863" alt="image" src="https://user-images.githubusercontent.com/30603497/217652296-60a8d732-a74e-4561-8ae7-6b2fac9749b5.png">
<img width="1861" alt="image" src="https://user-images.githubusercontent.com/30603497/217652374-72ea73cf-4d2f-4ae4-886e-0c1c34c714df.png">

I'll write more about how I implemented it (step-by-step) at a later date when I get some time.

## Branch: feature/AddADAuthentication
This branch builds up on the previous branch and adds capability to do Authentication using Local Active Directory.

This answer explains what I'm doing here: https://stackoverflow.com/a/74734478/8644294

## Branch: feature/AddClaimsDuringLogin
This branch builds up on _feature/LayoutWithIdentityPages_ to show how to add Claims during Login (in [`Login.cshtml.cs`](https://github.com/affableashish/blazor-server-auth/blob/07781afdb20689548ba6e7e04b8eb943c810ebc6/src/HMT.Web.Server/Areas/Identity/Pages/Account/Login.cshtml.cs#L129) file) and access those claims from [Razor Component](https://github.com/affableashish/blazor-server-auth/blob/07781afdb20689548ba6e7e04b8eb943c810ebc6/src/HMT.Web.Server/Areas/Identity/Components/TakeABreak.razor#L15).

When you launch the app, login and click 'Break' and add a breakpoint in [this line](https://github.com/affableashish/blazor-server-auth/blob/07781afdb20689548ba6e7e04b8eb943c810ebc6/src/HMT.Web.Server/Areas/Identity/Components/TakeABreak.razor#L15), you should get the new claim set in [this line](https://github.com/affableashish/blazor-server-auth/blob/07781afdb20689548ba6e7e04b8eb943c810ebc6/src/HMT.Web.Server/Areas/Identity/Pages/Account/Login.cshtml.cs#L129).
<img width="1856" alt="image" src="https://user-images.githubusercontent.com/30603497/217914365-10413758-780f-4c46-94a3-1783f2ade15a.png">
