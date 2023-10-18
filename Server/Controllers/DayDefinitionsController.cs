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
using ProgramPro.Shared.Models.DataTransferObjects;

namespace ProgramPro.Server.Controllers
{
    [ApiKeyAuthorization]
    [Route("api/[controller]")]
    [ApiController]
    public class DayDefinitionsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DayDefinitionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/DayDefinitions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DayDefinition>>> GetDayDefinitions()
        {
            return await _context.DayDefinitions.ToListAsync();
        }

        // GET: api/DayDefinitions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<string>> GetDayDefinition(int id)
        {
            // First, load the TrainingProgram
            var dayDefinition = await _context.DayDefinitions
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            if (dayDefinition != null)
            {
                // Explicitly load the related data for the training program
                await _context.Entry(dayDefinition)
                    .Collection(x => x.WorkoutExerciseDefinitions)
                    .Query()
                    .Include(x => x.SetDefinitions)
                    .Include(x => x.Exercise)
                    .LoadAsync();
            }
            else
            {
                return NotFound();
            }

            return JsonConvert.SerializeObject(dayDefinition, Extensions.JsonOptions.jsonSettings);
        }

        // PUT: api/DayDefinitions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDayDefinition(int id, DayDefinition dayDefinition)
        {
            if (id != dayDefinition.Id)
            {
                return BadRequest();
            }

            _context.Entry(dayDefinition).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DayDefinitionExists(id))
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

        [HttpPost("Overwrite")]
        public async Task<ActionResult<DayDefinition>> OverwriteDayDefinition(OverwriteDayDefinition overwriteDayDefinition)
        {
            if (ModelState.IsValid)
            {
                _context.DayDefinitions.Remove(overwriteDayDefinition.ToDelete);
                _context.DayDefinitions.Add(overwriteDayDefinition.ToAdd);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetDay", new { id = overwriteDayDefinition.ToAdd.Id }, JsonConvert.SerializeObject(overwriteDayDefinition.ToAdd, Extensions.JsonOptions.jsonSettings));
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        // POST: api/DayDefinitions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DayDefinition>> PostDayDefinition(DayDefinition dayDefinition)
        {
            var componentDefinition = await _context.ComponentDefinitions.Include(x => x.DayDefinitions).FirstOrDefaultAsync(x => x.Id == dayDefinition.ComponentDefinitionId);

            foreach (var d in componentDefinition.DayDefinitions)
            {
                if (d.Number > dayDefinition.Number)
                {
                    d.Number = d.Number + 1;
                    _context.DayDefinitions.Update(d);
                }
            }

            _context.DayDefinitions.Add(dayDefinition);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDayDefinition", new { id = dayDefinition.Id }, JsonConvert.SerializeObject(dayDefinition, Extensions.JsonOptions.jsonSettings));
        }

        // DELETE: api/DayDefinitions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDayDefinition(int id)
        {
            var dayDefinition = await _context.DayDefinitions.FindAsync(id);
            if (dayDefinition == null)
            {
                return NotFound();
            }

            var componentDefinition = await _context.ComponentDefinitions.Include(x => x.DayDefinitions).FirstOrDefaultAsync(x => x.Id == dayDefinition.ComponentDefinitionId);
            foreach (var d in componentDefinition.DayDefinitions)
            {
                if (d.Number > dayDefinition.Number)
                {
                    d.Number = d.Number - 1;
                    _context.DayDefinitions.Update(d);
                }
            }

            _context.DayDefinitions.Remove(dayDefinition);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DayDefinitionExists(int id)
        {
            return _context.DayDefinitions.Any(e => e.Id == id);
        }
    }
}
