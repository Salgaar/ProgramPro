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
    public class ExerciseStatisticsController : ControllerBase
    {
        private readonly ProgramProDbContext _context;

        public ExerciseStatisticsController(ProgramProDbContext context)
        {
            _context = context;
        }

        // GET: api/ExerciseStatistics
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExerciseStatistics>>> GetExerciseStatistics()
        {
            return await _context.ExerciseStatistics.ToListAsync();
        }

        // GET: api/ExerciseStatistics/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ExerciseStatistics>> GetExerciseStatistics(int id)
        {
            var exerciseStatistics = await _context.ExerciseStatistics.FindAsync(id);

            if (exerciseStatistics == null)
            {
                return NotFound();
            }

            return exerciseStatistics;
        }

        // PUT: api/ExerciseStatistics/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExerciseStatistics(int id, ExerciseStatistics exerciseStatistics)
        {
            if (id != exerciseStatistics.Id)
            {
                return BadRequest();
            }

            _context.Entry(exerciseStatistics).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExerciseStatisticsExists(id))
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

        // POST: api/ExerciseStatistics
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ExerciseStatistics>> PostExerciseStatistics(ExerciseStatistics exerciseStatistics)
        {
            _context.ExerciseStatistics.Add(exerciseStatistics);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetExerciseStatistics", new { id = exerciseStatistics.Id }, exerciseStatistics);
        }

        // DELETE: api/ExerciseStatistics/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExerciseStatistics(int id)
        {
            var exerciseStatistics = await _context.ExerciseStatistics.FindAsync(id);
            if (exerciseStatistics == null)
            {
                return NotFound();
            }

            _context.ExerciseStatistics.Remove(exerciseStatistics);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ExerciseStatisticsExists(int id)
        {
            return _context.ExerciseStatistics.Any(e => e.Id == id);
        }
    }
}
