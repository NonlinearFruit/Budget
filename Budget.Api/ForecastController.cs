using Budget.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Budget.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ForecastController : ControllerBase
    {
        private readonly IDatabaseContext _context;

        public ForecastController(IDatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Forecast
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Forecast>>> GetForecasts()
        {
            return await _context.Forecasts.ToListAsync();
        }

        [HttpGet("Test")]
        public async Task<ActionResult<IEnumerable<ForecastTest>>> GetForecastTests([FromQuery] int year, [FromQuery] int month)
        {
            return await _context
                .Forecasts
                .Where(f => f.Year == year)
                .Where(f => f.Month == month)
                .Select(f => new ForecastTest
                {
                    CategoryName = f.Category.Name,
                    CategoryColor = f.Category.Color,
                    Year = f.Year,
                    Month = f.Month,
                    ForecastedAmount = f.Amount,
                    SpentAmount = f.Category.Transactions.Where(t => t.When.Year == f.Year).Where(t => t.When.Month == f.Month).Sum(t => t.Amount),
                    Notes = f.Notes
                })
                .ToListAsync();
        }

        // GET: api/Forecast/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Forecast>> GetForecast(long id)
        {
            var forecast = await _context.Forecasts.FindAsync(id);

            if (forecast == null)
            {
                return NotFound();
            }

            return forecast;
        }

        // GET: api/Forecast/5/Test
        [HttpGet("{id}/Test")]
        public async Task<ActionResult<ForecastTest>> GetForecastTest(long id)
        {
            var forecast = await _context.Forecasts.FindAsync(id);
            if (forecast == null)
                return NotFound();
            forecast.Category = await _context.Categories.FindAsync(forecast.CategoryId);
            var sumOfTransactions = _context
                .Transactions
                .Where(t => t.CategoryId == forecast.CategoryId)
                .Where(t => t.When.Year == forecast.Year)
                .Where(t => t.When.Month == forecast.Month)
                .Sum(t => t.Amount);
            return new ForecastTest()
            {
                CategoryName = forecast.Category.Name,
                CategoryColor = forecast.Category.Color,
                Year = forecast.Year,
                Month = forecast.Month,
                ForecastedAmount = forecast.Amount,
                SpentLessThanForecasted = sumOfTransactions <= forecast.Amount,
                SpentAmount = sumOfTransactions,
                Notes = forecast.Notes
            };
        }

        // PUT: api/Forecast/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutForecast(long id, Forecast forecast)
        {
            if (id != forecast.Id)
            {
                return BadRequest();
            }

            _context.Entry(forecast).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ForecastExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Forecast
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Forecast>> PostForecast(Forecast forecast)
        {
            _context.Forecasts.Add(forecast);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetForecast", new { id = forecast.Id }, forecast);
        }

        // DELETE: api/Forecast/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteForecast(long id)
        {
            var forecast = await _context.Forecasts.FindAsync(id);
            if (forecast == null)
            {
                return NotFound();
            }

            _context.Forecasts.Remove(forecast);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ForecastExists(long id)
        {
            return _context.Forecasts.Any(e => e.Id == id);
        }
    }
}
