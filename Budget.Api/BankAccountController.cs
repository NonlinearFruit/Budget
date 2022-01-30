using Budget.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;

namespace Budget.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankAccountController : ControllerBase
    {
        private readonly IDatabaseContext _context;

        public BankAccountController(IDatabaseContext context)
        {
            _context = context;
        }

        // GET: api/BankAccount
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BankAccount>>> GetBankAccounts()
        {
            return await _context.BankAccounts.OrderBy(a => a.Name).ToListAsync();
        }

        // GET: api/BankAccount/Test
        [HttpGet("Test")]
        public async Task<ActionResult<IEnumerable<BankAccountTest>>> GetBankAccountTests()
        {
            return await _context
                .BankAccounts
                .Select(a => new BankAccountTest
                {
                    Account = a,
                    LiveMatchesSum = _context
                        .Transactions
                        .Where(t => t.AccountId == a.Id)
                        .Sum(t => t.Amount) == a.LiveTotal
                })
                .ToListAsync();
        }

        // GET: api/BankAccount/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BankAccount>> GetBankAccount(long id)
        {
            var bankAccount = await _context.BankAccounts.FindAsync(id);

            if (bankAccount == null)
            {
                return NotFound();
            }

            return bankAccount;
        }

        // GET: api/BankAccount/5/Test
        [HttpGet("{id}/Test")]
        public async Task<ActionResult<BankAccountTest>> GetBankAccountTest(long id)
        {
            var bankAccount = await _context.BankAccounts.FindAsync(id);
            if (bankAccount == null)
                return NotFound();
            var sum = await _context.Transactions.Where(t => t.AccountId == id).SumAsync(t => t.Amount);
            return new BankAccountTest
            {
                Account = bankAccount,
                LiveMatchesSum = bankAccount.LiveTotal == sum
            };
        }

        // PUT: api/BankAccount/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBankAccount(long id, BankAccount bankAccount)
        {
            if (id != bankAccount.Id)
            {
                return BadRequest();
            }

            _context.Entry(bankAccount).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BankAccountExists(id))
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

        // POST: api/BankAccount
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BankAccount>> PostBankAccount(BankAccount bankAccount)
        {
            _context.BankAccounts.Add(bankAccount);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBankAccount", new { id = bankAccount.Id }, bankAccount);
        }

        // DELETE: api/BankAccount/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBankAccount(long id)
        {
            var bankAccount = await _context.BankAccounts.FindAsync(id);
            if (bankAccount == null)
            {
                return NotFound();
            }

            _context.BankAccounts.Remove(bankAccount);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BankAccountExists(long id)
        {
            return _context.BankAccounts.Any(e => e.Id == id);
        }
    }
}
