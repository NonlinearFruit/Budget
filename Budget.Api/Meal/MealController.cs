using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Budget.Api.Meal
{
    [Route("api/[controller]")]
    [ApiController]
    public class MealController : ControllerBase
    {
        private readonly IMealContext _context;

        public MealController(IMealContext context)
        {
            _context = context;
        }

        // GET: api/Meal
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Shared.Meal.Meal>>> GetMeals()
        {
            if (_context.Meals == null)
            {
                return NotFound();
            }

            return await _context
                .Meals
                .OrderByDescending(m => m.Date.Date)
                .ThenByDescending(m => m.MealTime)
                .ToListAsync();
        }

        // GET: api/Meal/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Shared.Meal.Meal>> GetMeal(long id)
        {
            if (_context.Meals == null)
            {
                return NotFound();
            }

            var meal = await _context.Meals.FindAsync(id);

            if (meal == null)
            {
                return NotFound();
            }

            return meal;
        }

        // PUT: api/Meal/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMeal(long id, Shared.Meal.Meal meal)
        {
            if (id != meal.Id)
            {
                return BadRequest();
            }

            _context.Entry(meal).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MealExists(id))
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

        // POST: api/Meal
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Shared.Meal.Meal>> PostMeal(Shared.Meal.Meal meal)
        {
            if (_context.Meals == null)
            {
                return Problem("Entity set 'MealHistoryContext.Meals'  is null.");
            }

            _context.Meals.Add(meal);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMeal", new {id = meal.Id}, meal);
        }

        // DELETE: api/Meal/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMeal(long id)
        {
            if (_context.Meals == null)
            {
                return NotFound();
            }

            var meal = await _context.Meals.FindAsync(id);
            if (meal == null)
            {
                return NotFound();
            }

            _context.Meals.Remove(meal);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MealExists(long id)
        {
            return (_context.Meals?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}