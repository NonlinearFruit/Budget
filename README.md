# Budget
[![GitHub Workflow Status](https://img.shields.io/github/workflow/status/NonlinearFruit/Budget/.NET%20Core)](https://github.com/NonlinearFruit/Budget/actions/workflows/dotnet-core.yml)

| Resource                | Description            |
|-------------------------|------------------------|
| [Swagger][swag]         | Endpoint documentation |
| `http://localhost:7162` | Base url for api       |
| `http://localhost:7098` | Base url for web app   |

## Helpful Filler Commands

 - Check all forecast amounts vs spent amounts
   - `curl https://localhost:7162/api/Forecast -ks | jq .[].id | xargs -I _ curl https://localhost:7162/api/Forecast/_/Test -ks | jq '{name: .forecast.category.name, forecast: .forecast.amount, spent: .spentAmount, spentIsLess: .spentLessThan$orecasted}'`


[swag]: https://localhost:7162/swagger/index.html
