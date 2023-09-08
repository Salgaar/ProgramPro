using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.UserSecrets;
using ProgramPro.Server.Data;
using ProgramPro.Server.Helpers;
using ProgramPro.Shared.Models;

namespace ProgramPro.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainingprogramsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TrainingprogramsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Trainingprograms
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TrainingProgram>>> GetTrainingprograms()
        {
            var programs = await _context.TrainingPrograms/*.Where(x => x.ApplicationUserId == UserHelper.GetUserId(User))*/.ToListAsync();
            return programs;
        }

        // GET: api/Trainingprograms/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TrainingProgram>> GetTrainingprogram(int id)
        {
            var trainingprogram = await _context.TrainingPrograms/*.Where(x => x.ApplicationUserId == UserHelper.GetUserId(User))*/.FirstOrDefaultAsync(x => x.Id == id);

            if (trainingprogram == null)
            {
                return NotFound();
            }

            return trainingprogram;
        }

        // PUT: api/Trainingprograms/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTrainingprogram(int id, TrainingProgram trainingprogram)
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
        public async Task<ActionResult<TrainingProgram>> PostTrainingprogram(TrainingProgram trainingprogram)
        {
            //trainingprogram.ApplicationUserId = UserHelper.GetUserId(User);
            _context.TrainingPrograms.Add(trainingprogram);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTrainingprogram", new { id = trainingprogram.Id }, trainingprogram);
        }

        // DELETE: api/Trainingprograms/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrainingprogram(int id)
        {
            var trainingprogram = await _context.TrainingPrograms.FindAsync(id);
            if (trainingprogram == null)
            {
                return NotFound();
            }

            _context.TrainingPrograms.Remove(trainingprogram);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TrainingprogramExists(int id)
        {
            return _context.TrainingPrograms.Any(e => e.Id == id);
        }
    }
}
