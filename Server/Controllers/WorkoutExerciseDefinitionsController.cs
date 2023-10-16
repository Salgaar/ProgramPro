using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ProgramPro.Server.Data;
using ProgramPro.Shared.Models;

namespace ProgramPro.Server.Controllers
{
    [ApiKeyAuthorization]
    [Route("api/[controller]")]
    [ApiController]
    public class WorkoutExerciseDefinitionsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public WorkoutExerciseDefinitionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/WorkoutExerciseDefinitions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkoutExerciseDefinition>>> GetWorkoutExerciseDefinitions()
        {
            return await _context.WorkoutExerciseDefinitions.ToListAsync();
        }

        // GET: api/WorkoutExerciseDefinitions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<string>> GetWorkoutExerciseDefinition(int id)
        {
            var workoutExerciseDefinition = await _context.WorkoutExerciseDefinitions.Include(x => x.SetDefinitions).Include(x => x.Exercise).FirstOrDefaultAsync(x => x.Id == id);

            if (workoutExerciseDefinition == null)
            {
                return NotFound();
            }

            return JsonConvert.SerializeObject(workoutExerciseDefinition, Extensions.JsonOptions.jsonSettings);
        }

        // PUT: api/WorkoutExerciseDefinitions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWorkoutExerciseDefinition(int id, WorkoutExerciseDefinition workoutExerciseDefinition)
        {
            if (id != workoutExerciseDefinition.Id)
            {
                return BadRequest();
            }

            _context.Entry(workoutExerciseDefinition).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkoutExerciseDefinitionExists(id))
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

        // POST: api/WorkoutExerciseDefinitions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<WorkoutExerciseDefinition>> PostWorkoutExerciseDefinition(WorkoutExerciseDefinition workoutExerciseDefinition)
        {
            _context.WorkoutExerciseDefinitions.Add(workoutExerciseDefinition);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWorkoutExerciseDefinition", new { id = workoutExerciseDefinition.Id }, workoutExerciseDefinition);
        }

        // DELETE: api/WorkoutExerciseDefinitions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkoutExerciseDefinition(int id)
        {
            var workoutExerciseDefinition = await _context.WorkoutExerciseDefinitions.FindAsync(id);
            if (workoutExerciseDefinition == null)
            {
                return NotFound();
            }

            _context.WorkoutExerciseDefinitions.Remove(workoutExerciseDefinition);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool WorkoutExerciseDefinitionExists(int id)
        {
            return _context.WorkoutExerciseDefinitions.Any(e => e.Id == id);
        }
    }
}
