using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PerfJournal;

namespace PerfJournal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestersController : ControllerBase
    {
        private readonly PerfJournalContext _context;

        public TestersController(PerfJournalContext context)
        {
            _context = context;
        }

        // GET: api/Testers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tester>>> GetTesters()
        {
            return await _context.Testers.ToListAsync();
        }

        // GET: api/Testers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tester>> GetTester(int id)
        {
            var tester = await _context.Testers.FindAsync(id);

            if (tester == null)
            {
                return NotFound();
            }

            return tester;
        }

        // PUT: api/Testers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTester(int id, Tester tester)
        {
            if (id != tester.Id)
            {
                return BadRequest();
            }

            _context.Entry(tester).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TesterExists(id))
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

        // POST: api/Testers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Tester>> PostTester(Tester tester)
        {
            _context.Testers.Add(tester);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTester", new { id = tester.Id }, tester);
        }

        // DELETE: api/Testers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTester(int id)
        {
            var tester = await _context.Testers.FindAsync(id);
            if (tester == null)
            {
                return NotFound();
            }

            _context.Testers.Remove(tester);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TesterExists(int id)
        {
            return _context.Testers.Any(e => e.Id == id);
        }
    }
}
