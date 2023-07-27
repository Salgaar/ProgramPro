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
    public class TrainingprogramsController : ControllerBase
    {
        private readonly ProgramProDbContext _context;

        public TrainingprogramsController(ProgramProDbContext context)
        {
            _context = context;
        }

        // GET: api/Trainingprograms
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Trainingprogram>>> GetTrainingprograms()
        {
            return await _context.Trainingprograms.ToListAsync();
        }

        // GET: api/Trainingprograms/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Trainingprogram>> GetTrainingprogram(int id)
        {
            var trainingprogram = await _context.Trainingprograms.FindAsync(id);

            if (trainingprogram == null)
            {
                return NotFound();
            }

            return trainingprogram;
        }

        // PUT: api/Trainingprograms/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTrainingprogram(int id, Trainingprogram trainingprogram)
        {
            if (id != trainingprogram.Id)
            {
                return BadRequest();
            }

            _context.Entry(trainingprogram).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TrainingprogramExists(id))
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

        // POST: api/Trainingprograms
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Trainingprogram>> PostTrainingprogram(Trainingprogram trainingprogram)
        {
            _context.Trainingprograms.Add(trainingprogram);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTrainingprogram", new { id = trainingprogram.Id }, trainingprogram);
        }

        // DELETE: api/Trainingprograms/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrainingprogram(int id)
        {
            var trainingprogram = await _context.Trainingprograms.FindAsync(id);
            if (trainingprogram == null)
            {
                return NotFound();
            }

            _context.Trainingprograms.Remove(trainingprogram);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TrainingprogramExists(int id)
        {
            return _context.Trainingprograms.Any(e => e.Id == id);
        }
    }
}
