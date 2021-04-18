# Sapientia-2021-dotNet-Project (Fitness)

## Setup

### Project

```PowerShell
dotnet new gitignore

dotnet new sln
```

### Blazor Web Assambly Application

```PowerShell
dotnet new blazorwasm -n BlazorApp
dotnet sln add .\BlazorApp\BlazorApp.csproj
# dotnet add .\BlazorApp\BlazorApp.csproj reference StringLibrary/StringLibrary.csproj
# This package at the time of creating the application was available in prerelase form.
cd .\BlazorApp
dotnet add package Microsoft.AspNetCore.Components.Web.Extensions --prerelease
```

### MSSQL Server

The Microsoft SQL server is runing inside a docker container. You can set up using the following comand inside your Terminal.
You may want to change the password: **-e 'SA_PASSWORD=...'**.

```PowerShell
docker run --name mssql_server -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=F8Cc2bswB6H5Bk!u' -p 1433:1433 -d mcr.microsoft.com/mssql/server:2019-latest
```

Inside the Docker conteiner run the sqlcmd tool

```sh
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P <your_password>
```

If the tool started corectly then you are greeted with **1>** prompter, then type:

```SQLCmd
CREATE DATABASE Fitness
GO
```

Now you have created the database. Create the schemas.

```SQL
DROP TABLE IF EXISTS [Fitness].[dbo].[Gyms];
CREATE TABLE [Fitness].[dbo].[Gyms] (
    [Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [Name] VARCHAR(128) NOT NULL,
    [IsDeleted] BIT NOT NULL DEFAULT(0)
);

DROP TABLE IF EXISTS [Fitness].[dbo].[Passes];
CREATE TABLE [Fitness].[dbo].[Passes]
(
    [Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [Name] VARCHAR(128) NOT NULL DEFAULT(''),
    [Price] DECIMAL(10,2) NOT NULL DEFAULT(0),
    [ValidForDays] INT not NULL DEFAULT(0),
    [ValidForEnteries] INT NOT NULL DEFAULT(0),
    [ValidForGymId] INT NOT NULL DEFAULT(0),
    [ValidFrom] INT not null DEFAULT(0),
    [ValidUntil] INT not null DEFAULT(24),
    [ValidPerDay] INT not null DEFAULT(1),
    [IsDeleted] BIT NOT NULL DEFAULT(0)
);

DROP TABLE IF EXISTS [Fitness].[dbo].[Clients];
CREATE TABLE [Fitness].[dbo].[Clients]
(
    [Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [Name] VARCHAR(128) NOT NULL,
    [Phone] VARCHAR(16) NOT NULL,
    [Email] VARCHAR(128) NOT NULL,
    [IdCardNr] VARCHAR(16) NOT NULL,
    -- [photo] ,

    [IsDeleted] BIT NOT NULL DEFAULT(0),
    [Barcode] VARCHAR(64) UNIQUE NOT NULL,
    [InsertedAt] DATETIME NOT NULL default(getdate()),
    [Notes] VARCHAR(256) DEFAULT NULL
);

DROP TABLE IF EXISTS [Fitness].[dbo].[Entries];
CREATE TABLE [Fitness].[dbo].[Entries]
(
    [Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [ClientId] INT NOT NULL
        FOREIGN KEY REFERENCES [Fitness].[dbo].[Clients](Id),
    [PassId] INT NOT NULL
        FOREIGN KEY REFERENCES [Fitness].[dbo].[Passes](Id),
    [GymId] INT NOT NULL
        FOREIGN KEY REFERENCES [Fitness].[dbo].[Gyms](Id),
    [EntryDate] DATETIME NOT NULL DEFAULT(getdate())
);

DROP TABLE IF EXISTS [Fitness].[dbo].[ClientPasses];
CREATE TABLE [Fitness].[dbo].[ClientPasses] (
    [Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [ClientId] INT NOT NULL
        FOREIGN KEY REFERENCES [Fitness].[dbo].[Clients](Id),
    [PassId] INT NOT NULL
        FOREIGN KEY REFERENCES [Fitness].[dbo].[Passes](Id),
    [DateOfPurchase] DATETIME NOT NULL DEFAULT(getdate()),
    [Barcode] VARCHAR(64) UNIQUE NOT NULL,
    [SellingPrice] DECIMAL(10,2) NOT NULL,
    [NumberOfEnteries] INT NOT NULL DEFAULT(0),
    [FirstUsedAt] INT DEFAULT(NULL),
    [IsValid] BIT NOT NULL DEFAULT(1),
);
```

### DataLib

```PowerShell
dotnet new classlib -n DataAccessLayer
dotnet sln add .\DataAccessLayer\DataAccessLayer.csproj


cd .\DataAccessLayer
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet tool install --global dotnet-ef

dotnet ef dbcontext scaffold "server='host.docker.internal';port=1433;database=Fitness;user=sa;pwd='F8Cc2bswB6H5Bk!u;'" "Microsoft.EntityFrameworkCore.SqlServer" -o .\Models -f
```

NuGet Packages:
[Microsoft.EntityFrameworkCore.Design](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.Design/),
[Microsoft.EntityFrameworkCore.SqlServer](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.SqlServer/6.0.0-preview.3.21201.2).
CLI Tools:
[Microsoft.EntityFrameworkCore.Tools](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.Tools/6.0.0-preview.3.21201.2).

## Results

## Resources

[Build a web app with Blazor WebAssembly and Visual Studio Code](https://docs.microsoft.com/en-us/learn/modules/build-blazor-webassembly-visual-studio-code/)
[Create a .NET class library using Visual Studio Code](https://docs.microsoft.com/en-us/dotnet/core/tutorials/library-with-visual-studio-code)
