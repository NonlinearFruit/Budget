using Budget.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Budget.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly IDatabaseContext _context;

        public TransactionController(IDatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Transaction
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Transaction>>> GetTransactions(
            [FromQuery] int? year = null,
            [FromQuery] int? month = null,
            [FromQuery] int? category = null,
            [FromQuery] int? account = null,
            [FromQuery] int? tag = null
        )
        {
            await _context.Tags.LoadAsync();
            await _context.Categories.LoadAsync();
            await _context.BankAccounts.LoadAsync();
            var transactions = _context.Transactions as IQueryable<Transaction>;
            if (year.HasValue)
                transactions = transactions.Where(t => t.When.Year == year);
            if (month.HasValue)
                transactions = transactions.Where(t => t.When.Month == month);
            if (category.HasValue)
                transactions = transactions.Where(t => t.CategoryId == category);
            if (account.HasValue)
                transactions = transactions.Where(t => t.AccountId == account);
            if (tag.HasValue)
                transactions = transactions.Where(t => t.TagId == tag);
            var list = await transactions.ToListAsync();
            foreach (var transaction in list)
            {
                if (transaction.Account != null) transaction.Account.Transactions = null;
                if (transaction.Category != null) transaction.Category.Transactions = null;
                if (transaction.Tag != null) transaction.Tag.Transactions = null;
            }
            return list;
        }

        // GET: api/Transaction/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Transaction>> GetTransaction(long id)
        {
            var transaction = await _context.Transactions.FindAsync(id);

            if (transaction == null)
            {
                return NotFound();
            }

            return transaction;
        }

        // PUT: api/Transaction/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTransaction(long id, Transaction transaction)
        {
            if (id != transaction.Id)
            {
                return BadRequest();
            }

            _context.Entry(transaction).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TransactionExists(id))
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

        // POST: api/Transaction
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Transaction>> PostTransaction(Transaction transaction)
        {
            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTransaction", new { id = transaction.Id }, transaction);
        }

        // DELETE: api/Transaction/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransaction(long id)
        {
            var transaction = await _context.Transactions.FindAsync(id);
            if (transaction == null)
            {
                return NotFound();
            }

            _context.Transactions.Remove(transaction);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TransactionExists(long id)
        {
            return _context.Transactions.Any(e => e.Id == id);
        }
    }
}
