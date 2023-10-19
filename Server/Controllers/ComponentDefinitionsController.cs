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
        public async Task<ActionResult<string>> GetComponentDefinitions()
        {
            var componentDefinitions = await _context.ComponentDefinitions.ToListAsync();

            if (componentDefinitions != null)
            {
                // Explicitly load the related data for the training program
                for(int i = 0; i < componentDefinitions.Count; i++)
                {
                    await _context.Entry(componentDefinitions[i])
                        .Collection(x => x.DayDefinitions)
                        .Query()
                        .LoadAsync();

                    var dayDefinitions = componentDefinitions[i].DayDefinitions.ToList();
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

            return JsonConvert.SerializeObject(componentDefinitions, Extensions.JsonOptions.jsonSettings);
        }

        // GET: api/SplitDefinitions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<string>> GetComponentDefinition(int id)
        {
            // First, load the TrainingProgram
            var componentDefinition = await _context.ComponentDefinitions
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            if (componentDefinition != null)
            {
                // Explicitly load the related data for the training program
                await _context.Entry(componentDefinition)
                    .Collection(x => x.DayDefinitions)
                    .Query()
                    .LoadAsync();

                // Load related data for WorkoutExercises, Sets, and Exercise
                foreach (var day in componentDefinition.DayDefinitions)
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

            return JsonConvert.SerializeObject(componentDefinition, Extensions.JsonOptions.jsonSettings);
        }

        // PUT: api/SplitDefinitions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutComponentDefinition(int id, ComponentDefinition componentDefinition)
        {
            if (id != componentDefinition.Id)
            {
                return BadRequest();
            }

            _context.Entry(componentDefinition).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ComponentDefinitionExists(id))
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
        public async Task<ActionResult<ComponentDefinition>> PostComponentDefinition(ComponentDefinition componentDefinition)
        {
            _context.ComponentDefinitions.Add(componentDefinition);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetComponentDefinition", new { id = componentDefinition.Id }, componentDefinition);
        }

        // DELETE: api/SplitDefinitions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComponentDefinition(int id)
        {
            var componentDefinition = await _context.ComponentDefinitions.FindAsync(id);
            if (componentDefinition == null)
            {
                return NotFound();
            }

            _context.ComponentDefinitions.Remove(componentDefinition);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ComponentDefinitionExists(int id)
        {
            return _context.ComponentDefinitions.Any(e => e.Id == id);
        }
    }
}
