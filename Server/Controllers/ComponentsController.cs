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
    [Route("api/[controller]")]
    [ApiController]
    public class ComponentsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ComponentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Splits
        [HttpGet]
        public async Task<ActionResult<string>> GetComponents()
        {
            var components = await _context.Components.ToListAsync();
            /*.Where(x => x.ApplicationUserId == UserHelper.GetUserId(User))*/

            foreach (var component in components)
            {
                await _context.Entry(component)
                        .Collection(x => x.Days)
                        .Query()
                        .Include(x => x.WorkoutExercises)
                        .LoadAsync();

                foreach(var day in component.Days)
                {
                    await _context.Entry(day)
                        .Collection(x => x.WorkoutExercises)
                        .Query()
                        .Include(x => x.Sets)
                        .Include(x => x.Exercise)
                        .LoadAsync();
                }
            }

            return JsonConvert.SerializeObject(components, Extensions.JsonOptions.jsonSettings);
        }

        // GET: api/Splits/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Component>> GetComponent(int id)
        {
            var component = await _context.Components.FindAsync(id);

            if (component == null)
            {
                return NotFound();
            }

            return component;
        }

        // PUT: api/Splits/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutComponent(int id, Component component)
        {
            if (id != component.Id)
            {
                return BadRequest();
            }

            _context.Entry(component).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ComponentExists(id))
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

        // POST: api/Splits
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Component>> PostComponent(Component component)
        {
            _context.Components.Add(component);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetComponent", new { id = component.Id }, component);
        }

        // DELETE: api/Splits/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComponent(int id)
        {
            var component = await _context.Components.Include(x => x.Days).FirstOrDefaultAsync(x => x.Id == id);
            if (component == null)
            {
                return NotFound();
            }

            var components = await _context.Components.Include(x => x.Days).Where(x => x.TrainingProgramId == component.TrainingProgramId).ToListAsync();
            foreach(var item in components)
            {
                if(item.ComponentNumber > component.ComponentNumber)
                {
                    foreach(var day in item.Days)
                    {
                        day.Date = day.Date.AddDays(-component.Days.Count);
                        _context.Days.Update(day);
                    }
                    item.ComponentNumber -= 1;
                    _context.Components.Update(item);
                }
            }

            _context.Components.Remove(component);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ComponentExists(int id)
        {
            return _context.Components.Any(e => e.Id == id);
        }
    }
}
