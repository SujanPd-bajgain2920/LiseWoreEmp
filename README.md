# LiseWoreEmp

## Architecture

This project is built using the ASP.NET Core MVC framework with .NET 8 Long Term Support (LTS). It follows the Model-View-Controller (MVC) design pattern to separate concerns:

- **Models:** Represent the data and business logic. Entity Framework Core is used as the Object-Relational Mapper (ORM) to interact with the SQL Server database.
- **Views:** Handle the user interface and presentation.
- **Controllers:** Manage user input, interact with models, and select views to render.

Entity Framework Core is used for database access and management, with code generated from an existing SQL Server database using scaffolding.

## Setup Steps

**1. Prerequisites:**
   - Install [Visual Studio 2022](https://visualstudio.microsoft.com/vs/)
   - Install [SQL Server Management Studio (SSMS)](https://aka.ms/ssms)
   - Ensure .NET 8 SDK is installed

**2. Setup the Database**
      Open SQL Server Management Studio (SSMS)
      
      Run the script file: EmployeeManagement.sql
      
      This will:
      
      Create the database LiseWoreEmp
      
      Create tables: UserList, Employees

**3. Project Setup:**
   - Open the project in Visual Studio 2022.
   - Install the following NuGet packages (version 8.0.17):
     - `Microsoft.EntityFrameworkCore`
     - `Microsoft.EntityFrameworkCore.SqlServer`
     - `Microsoft.EntityFrameworkCore.Tools`

    or 
    You can clone it: **https://github.com/SujanPd-bajgain2920/LiseWoreEmp.git**
       
**4. Configure Connection String:**
Open appsettings.json or Web.config (depending on project type)

Update your connection string:
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=LiseWoreEmp;Trusted_Connection=True;"
}

**5. Scaffold Database Context and Models:**
   - Open the Package Manager Console in Visual Studio.
   - Run the following command to scaffold the database context and entity models from your existing database:
     ```
     Scaffold-DbContext -Connection "name=con" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -f
     ```
   - This command generates the entity classes and DbContext in the `Models` folder.
   - after this: write this in program.cs
      builder.Services.AddDbContext<YourDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("con")));

**6. Run the Application:**
   - Build and run the project.
   - The MVC application will interact with the database using the generated Entity Framework Core models.
