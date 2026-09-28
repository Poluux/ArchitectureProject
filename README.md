# 3-Tier Architecture Project
A decoupled, N-Tier web application built with **.NET** and designed for both local development and cloud deployment (**Azure**)

# Project architecture
The solution is divided into 3 distinct layers:
*  **WebAPI_ArchitectureProject:** REST API (documented with Swagger) handling the core business logic
*  **MVC_ArchitectureProject:** ASP.NET Core MVC User Interface (Front-end) that consumes the API
*  **DataAccessLayer:** Data persistence using Entity Framework Core, managing two separate databases ("MS_SQLContext" and "PaymentContext")

## Setup

To run the project locally:
1.  Create a ".env" file at the root of both the **WebAPI** and **MVC** projects, using the provided ".env.example" templates
2.  Apply the migrations to generate your local databases
    dotnet ef database update --context MS_SQLContext
    dotnet ef database update --context PaymentContext
3.  Configure Visual Studio to launch **both projects simultaneously** (Multiple Startup Projects)
   
