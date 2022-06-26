# Budget
[![GitHub Build/Test Workflow Status](https://img.shields.io/github/workflow/status/NonlinearFruit/Budget/build-and-unit-test?label=tests)](https://github.com/NonlinearFruit/Budget/actions/workflows/build-test.yml)
[![GitHub Up-to-date Workflow Status](https://img.shields.io/github/workflow/status/NonlinearFruit/Budget/any-outdated-dependencies?label=up-to-date)](https://github.com/NonlinearFruit/Budget/actions/workflows/up-to-date.yml)
[![GitHub Vulnerabilities Workflow Status](https://img.shields.io/github/workflow/status/NonlinearFruit/Budget/vulnerabilities?label=vulnerabilities)](https://github.com/NonlinearFruit/Budget/actions/workflows/vulnerabilities.yml)

| Resource                | Description            |
|-------------------------|------------------------|
| [Swagger][swag]         | Endpoint documentation |
| `http://localhost:7162` | Base url for api       |
| `http://localhost:7098` | Base url for web app   |

## How to Setup Budget for Use

- Install [pgAdmin][pgAdmin] for creating/hosting your postgres database
- Create a database via pgAdmin
- Install [.Net 6+ SDK][.net]
- Clone our repo
- Edit the appsettings' ConnectionString to point to your database
- `dotnet run --project Budget.Api/Budget.Api.csproj` to start the [Api][swag]
- `dotnet run --project Budget.Blazor/Budget.Blazor.csproj` to start the [Web App](http://localhost:7098)
- Budget!

## How to Backup

 1. Open PgAdmin
 2. Right click database
 3. Select 'Backup...'
 4. Give it a path/filename (eg: `/path/to/somewhere/Budget.22.03.14.backup`)
 5. Format: 'Plain'
 6. Click 'Backup'

 OR

 ```
"C:\Users\bbolen\AppData\Local\Programs\pgAdmin 4\v6\runtime\pg_dump.exe" --file "C:\\Users\\bbolen\\BUDGET-22-06-25.BAC" --host "localhost" --port "5432" --username "postgres" --verbose --format=p "Budget" 
 ```

## How to Create Migrations

```
dotnet ef migrations add --project Budget.Api/Budget.Api.csproj --context Budget.Api.MealHistory.MealHistoryContext <MIGRATIONNAME>
```

## How to Restore Backup
 
 ```
 psql.exe Budget postgres < Budget.22.03.14.backup
 ```

## How to setup Playwright integration tests

- [Prerequisite] Install dotnet sdk
- [Prerequisite] Install dotnet pwsh: dotnet tool install -g PowerShell
- Do a build: dotnet restore && dotnet build
- Install playwright browsers: pwsh.exe Budget.Tests/bin/Debug/net6.0/playwright.ps1 install
    - Getting started guide: https://playwright.dev/dotnet/docs/intro#installation
- Setup the settings.json and secrets.json for what is being tested
- Run tests: dotnet test 

[.net]: https://dotnet.microsoft.com/en-us/download/dotnet/6.0
[pgAdmin]: https://www.pgadmin.org
[swag]: https://localhost:7162/swagger/index.html
