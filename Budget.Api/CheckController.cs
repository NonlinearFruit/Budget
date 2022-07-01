using Budget.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Budget.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckController : ControllerBase
    {
        private readonly IBudgetContext _context;

        public CheckController(IBudgetContext context)
        {
            _context = context;
        }

        // GET: api/Check
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Check>>> GetChecks()
        {
            return await _context.Checks.OrderBy(c => c.CheckNumber).ToListAsync();
        }

        // GET: api/Check/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Check>> GetCheck(long id)
        {
            var check = await _context.Checks.FindAsync(id);

            if (check == null)
            {
                return NotFound();
            }

            return check;
        }

        // PUT: api/Check/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCheck(long id, Check check)
        {
            if (id != check.Id)
            {
                return BadRequest();
            }

            _context.Entry(check).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CheckExists(id))
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

        // POST: api/Check
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Check>> PostCheck(Check check)
        {
            _context.Checks.Add(check);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCheck", new { id = check.Id }, check);
        }

        // DELETE: api/Check/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCheck(long id)
        {
            var check = await _context.Checks.FindAsync(id);
            if (check == null)
            {
                return NotFound();
            }

            _context.Checks.Remove(check);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CheckExists(long id)
        {
            return _context.Checks.Any(e => e.Id == id);
        }
    }
}
