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
    [Route("api/[controller]")]
    [ApiController]
    public class BodyStatisticsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BodyStatisticsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/BodyStatistics
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BodyStatistics>>> GetBodyStatistics()
        {
            return await _context.BodyStatistics.ToListAsync();
        }

        // GET: api/BodyStatistics/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BodyStatistics>> GetBodyStatistics(int id)
        {
            var bodyStatistics = await _context.BodyStatistics.FindAsync(id);

            if (bodyStatistics == null)
            {
                return NotFound();
            }

            return bodyStatistics;
        }

        // PUT: api/BodyStatistics/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBodyStatistics(int id, BodyStatistics bodyStatistics)
        {
            if (id != bodyStatistics.Id)
            {
                return BadRequest();
            }

            _context.Entry(bodyStatistics).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BodyStatisticsExists(id))
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

        // POST: api/BodyStatistics
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BodyStatistics>> PostBodyStatistics(BodyStatistics bodyStatistics)
        {
            _context.BodyStatistics.Add(bodyStatistics);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBodyStatistics", new { id = bodyStatistics.Id }, bodyStatistics);
        }

        // DELETE: api/BodyStatistics/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBodyStatistics(int id)
        {
            var bodyStatistics = await _context.BodyStatistics.FindAsync(id);
            if (bodyStatistics == null)
            {
                return NotFound();
            }

            _context.BodyStatistics.Remove(bodyStatistics);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BodyStatisticsExists(int id)
        {
            return _context.BodyStatistics.Any(e => e.Id == id);
        }
    }
}
