using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.UserSecrets;
using Newtonsoft.Json;
using ProgramPro.Server.Data;
using ProgramPro.Server.Helpers;
using ProgramPro.Shared.Models;

namespace ProgramPro.Server.Controllers
{
    [ApiKeyAuthorization]
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
        public async Task<ActionResult<string>> GetTrainingprograms()
        {
            var programs = _context.TrainingPrograms;
                /*.Where(x => x.ApplicationUserId == UserHelper.GetUserId(User))*/

            foreach (var program in programs)
            {
                await _context.Entry(program)
                        .Collection(x => x.Splits)
                        .Query()
                        .Include(x => x.Days)
                        .LoadAsync();
            }

            return JsonConvert.SerializeObject(programs, Extensions.JsonOptions.jsonSettings);
        }

        // GET: api/Trainingprograms/5
        [HttpGet("{id}")]
        public async Task<ActionResult<string>> GetTrainingprogram(Guid id)
        {
            // First, load the TrainingProgram
            var trainingprogram = await _context.TrainingPrograms
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            if (trainingprogram != null)
            {
                // Explicitly load the related data for the training program
                await _context.Entry(trainingprogram)
                    .Collection(x => x.Splits)
                    .Query()
                    .Include(x => x.Days)
                    .LoadAsync();

                // Load related data for WorkoutExercises, Sets, and Exercise
                foreach (var split in trainingprogram.Splits)
                {
                    foreach(var day in split.Days)
                    {
                        await _context.Entry(day)
                        .Collection(x => x.WorkoutExercises)
                        .Query()
                        .Include(x => x.Sets)
                        .Include(x => x.Exercise)
                        .LoadAsync();
                    }
                }
            }
            else
            {
                return NotFound();
            }

            return JsonConvert.SerializeObject(trainingprogram, Extensions.JsonOptions.jsonSettings);
        }

        // PUT: api/Trainingprograms/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTrainingprogram(Guid id, TrainingProgram trainingprogram)
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
            trainingprogram.Id = Guid.NewGuid();
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

        [HttpDelete]
        public async Task<IActionResult> DeleteTrainingprograms()
        {
            var trainingprograms = _context.TrainingPrograms;
            _context.TrainingPrograms.RemoveRange(trainingprograms);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TrainingprogramExists(Guid id)
        {
            return _context.TrainingPrograms.Any(e => e.Id == id);
        }
    }
}
