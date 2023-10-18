using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
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
    public class ComponentDefinitionsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ComponentDefinitionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/SplitDefinitions
        [HttpGet]
        public async Task<ActionResult<string>> GetSplitDefinitions()
        {
            var splitDefinitions = await _context.ComponentDefinitions.ToListAsync();

            if (splitDefinitions != null)
            {
                // Explicitly load the related data for the training program
                for(int i = 0; i < splitDefinitions.Count; i++)
                {
                    await _context.Entry(splitDefinitions[i])
                        .Collection(x => x.DayDefinitions)
                        .Query()
                        .LoadAsync();

                    var dayDefinitions = splitDefinitions[i].DayDefinitions.ToList();
                    for(int j = 0; j < dayDefinitions.Count; j++)
                    {
                        await _context.Entry(dayDefinitions[j])
                        .Collection(x => x.WorkoutExerciseDefinitions)
                        .Query()
                        .Include(x => x.SetDefinitions)
                        .Include(x => x.Exercise)
                        .LoadAsync();
                    }
                }
            }
            else
            {
                return NotFound();
            }

            return JsonConvert.SerializeObject(splitDefinitions, Extensions.JsonOptions.jsonSettings);
        }

        // GET: api/SplitDefinitions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<string>> GetSplitDefinition(int id)
        {
            // First, load the TrainingProgram
            var splitDefinition = await _context.ComponentDefinitions
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            if (splitDefinition != null)
            {
                // Explicitly load the related data for the training program
                await _context.Entry(splitDefinition)
                    .Collection(x => x.DayDefinitions)
                    .Query()
                    .LoadAsync();

                // Load related data for WorkoutExercises, Sets, and Exercise
                foreach (var day in splitDefinition.DayDefinitions)
                {
                    await _context.Entry(day)
                        .Collection(x => x.WorkoutExerciseDefinitions)
                        .Query()
                        .Include(x => x.SetDefinitions)
                        .Include(x => x.Exercise)
                        .LoadAsync();
                }
            }
            else
            {
                return NotFound();
            }

            return JsonConvert.SerializeObject(splitDefinition, Extensions.JsonOptions.jsonSettings);
        }

        // PUT: api/SplitDefinitions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSplitDefinition(int id, ComponentDefinition splitDefinition)
        {
            if (id != splitDefinition.Id)
            {
                return BadRequest();
            }

            _context.Entry(splitDefinition).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SplitDefinitionExists(id))
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

        // POST: api/SplitDefinitions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ComponentDefinition>> PostSplitDefinition(ComponentDefinition splitDefinition)
        {
            _context.ComponentDefinitions.Add(splitDefinition);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSplitDefinition", new { id = splitDefinition.Id }, splitDefinition);
        }

        // DELETE: api/SplitDefinitions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSplitDefinition(int id)
        {
            var splitDefinition = await _context.ComponentDefinitions.FindAsync(id);
            if (splitDefinition == null)
            {
                return NotFound();
            }

            _context.ComponentDefinitions.Remove(splitDefinition);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SplitDefinitionExists(int id)
        {
            return _context.ComponentDefinitions.Any(e => e.Id == id);
        }
    }
}
