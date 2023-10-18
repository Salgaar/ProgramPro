using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Model.Map;
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
    public class DaysController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DaysController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Days/GetDays/5
        [HttpGet("{componentId}/GetDays")]
        public async Task<ActionResult<string>> GetDays(int componentId)
        {
            var days = _context.Days
                .Where(x => x.ComponentId == componentId);

            foreach(var day in days)
            {
                await _context.Entry(day)
                    .Collection(x => x.WorkoutExercises)
                    .Query()
                    .Include(x => x.Sets)
                    .Include(x => x.Exercise)
                    .LoadAsync();
            }

            return JsonConvert.SerializeObject(days, Extensions.JsonOptions.jsonSettings);
        }

        // GET: api/Days/5
        [HttpGet("{id}")]
        public async Task<ActionResult<string>> GetDay(int id)
        {
            var day = await _context.Days
                .Include(x => x.Component)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (day != null)
            {
                await _context.Entry(day)
                    .Collection(x => x.WorkoutExercises)
                    .Query()
                    .Include(x => x.Sets)
                    .Include(x => x.Exercise)
                    .LoadAsync();
            }
            else
            {
                return NotFound();
            }

            return JsonConvert.SerializeObject(day, Extensions.JsonOptions.jsonSettings);
        }

        [HttpPost("Overwrite")]
        public async Task<ActionResult<Day>> OverwriteDay(OverwriteDay overwriteDay)
        {
            if (ModelState.IsValid)
            {
                _context.Days.Remove(overwriteDay.ToDelete);
                _context.Days.Add(overwriteDay.ToAdd);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetDay", new { id = overwriteDay.ToAdd.Id }, JsonConvert.SerializeObject(overwriteDay.ToAdd, Extensions.JsonOptions.jsonSettings));
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        // PUT: api/Days/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDay(int id, Day day)
        {
            if (id != day.Id)
            {
                return BadRequest();
            }

            _context.Entry(day).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DayExists(id))
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

        // POST: api/Days
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Day>> PostDay(Day day)
        {
            if (ModelState.IsValid)
            {
                var component = await _context.Components.Include(x => x.Days).FirstOrDefaultAsync(x => x.Id == day.ComponentId);

                var program = await _context.TrainingPrograms
                    .FirstOrDefaultAsync(x => x.Id == component.TrainingProgramId);

                await _context.Entry(program)
                    .Collection(x => x.Components)
                    .Query()
                    .Include(x => x.Days)
                    .LoadAsync();

                var splits = program.Components.Where(x => x.ComponentNumber > component.ComponentNumber).ToList();
                if(splits != null)
                {
                    foreach (var itemSplit in splits)
                    {
                        foreach (var itemDay in itemSplit.Days)
                        {
                            itemDay.Date = itemDay.Date.AddDays(1);
                            _context.Days.Update(itemDay);
                        }
                    }
                }

                _context.Days.Add(day);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetDay", new { id = day.Id }, JsonConvert.SerializeObject(day, Extensions.JsonOptions.jsonSettings));
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        // POST: api/Days
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("PostDays")]
        public async Task<ActionResult<List<Day>>> PostDays(List<Day> days)
        {
            if (ModelState.IsValid)
            {
                var split = await _context.Components.FindAsync(days[0].ComponentId);

                var program = await _context.TrainingPrograms
                    .FirstOrDefaultAsync(x => x.Id == split.TrainingProgramId);

                await _context.Entry(program)
                    .Collection(x => x.Components)
                    .Query()
                    .Include(x => x.Days)
                    .LoadAsync();

                var splits = program.Components.Where(x => x.ComponentNumber > split.ComponentNumber).ToList();
                if (splits != null)
                {
                    foreach (var itemSplit in splits)
                    {
                        foreach (var itemDay in itemSplit.Days)
                        {
                            itemDay.Date = itemDay.Date.AddDays(1);
                            _context.Days.Update(itemDay);
                        }
                    }
                }

                _context.Days.AddRange(days);
                await _context.SaveChangesAsync();

                return CreatedAtAction("PostDays", new { trainingProgramId = days[0].Component.TrainingProgramId }, JsonConvert.SerializeObject(days, Extensions.JsonOptions.jsonSettings));
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        // DELETE: api/Days/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDay(int id)
        {
            var day = await _context.Days.FindAsync(id);
            if (day == null)
            {
                return NotFound();
            }

            var split = await _context.Components.Include(x => x.Days).FirstOrDefaultAsync(x => x.Id == day.ComponentId);

            foreach (var d in split.Days)
            {
                if (d.Date > day.Date)
                {
                    d.Date = d.Date.AddDays(-1);
                    _context.Days.Update(d);
                }
            }

            var program = await _context.TrainingPrograms
                .FirstOrDefaultAsync(x => x.Id == split.TrainingProgramId);

            await _context.Entry(program)
                .Collection(x => x.Components)
                .Query()
                .Include(x => x.Days)
                .LoadAsync();

            var splits = program.Components.Where(x => x.ComponentNumber > split.ComponentNumber).ToList();
            if (splits != null)
            {
                foreach (var itemSplit in splits)
                {
                    foreach (var itemDay in itemSplit.Days)
                    {
                        itemDay.Date = itemDay.Date.AddDays(-1);
                        _context.Days.Update(itemDay);
                    }
                }
            }

            _context.Days.Remove(day);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DayExists(int id)
        {
            return _context.Days.Any(e => e.Id == id);
        }
    }
}
