# Budget
[![GitHub Workflow Status](https://img.shields.io/github/workflow/status/NonlinearFruit/Budget/.NET%20Core)](https://github.com/NonlinearFruit/Budget/actions/workflows/dotnet-core.yml)

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

[.net]: https://dotnet.microsoft.com/en-us/download/dotnet/6.0
[pgAdmin]: https://www.pgadmin.org
[swag]: https://localhost:7162/swagger/index.html
