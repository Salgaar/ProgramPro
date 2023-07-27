using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProgramPro.Server.Data;
using ProgramPro.Shared.Models;

namespace ProgramPro.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class WeeksController : ControllerBase
    {
        private readonly ProgramProDbContext _context;

        public WeeksController(ProgramProDbContext context)
        {
            _context = context;
        }

        // GET: api/Weeks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Week>>> GetWeeks()
        {
            return await _context.Weeks.ToListAsync();
        }

        // GET: api/Weeks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Week>> GetWeek(int id)
        {
            var week = await _context.Weeks.FindAsync(id);

            if (week == null)
            {
                return NotFound();
            }

            return week;
        }

        // PUT: api/Weeks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWeek(int id, Week week)
        {
            if (id != week.Id)
            {
                return BadRequest();
            }

            _context.Entry(week).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WeekExists(id))
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

        // POST: api/Weeks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Week>> PostWeek(Week week)
        {
            _context.Weeks.Add(week);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWeek", new { id = week.Id }, week);
        }

        // DELETE: api/Weeks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWeek(int id)
        {
            var week = await _context.Weeks.FindAsync(id);
            if (week == null)
            {
                return NotFound();
            }

            _context.Weeks.Remove(week);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool WeekExists(int id)
        {
            return _context.Weeks.Any(e => e.Id == id);
        }
    }
}
