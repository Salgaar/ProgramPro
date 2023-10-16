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
    public class SplitsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SplitsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Splits
        [HttpGet]
        public async Task<ActionResult<string>> GetSplits()
        {
            var splits = _context.Splits;
            /*.Where(x => x.ApplicationUserId == UserHelper.GetUserId(User))*/

            foreach (var split in splits)
            {
                await _context.Entry(split)
                        .Collection(x => x.Days)
                        .Query()
                        .Include(x => x.WorkoutExercises)
                        .LoadAsync();
                foreach(var day in split.Days)
                {
                    await _context.Entry(day)
                        .Collection(x => x.WorkoutExercises)
                        .Query()
                        .Include(x => x.Sets)
                        .LoadAsync();
                }
            }

            return JsonConvert.SerializeObject(splits, Extensions.JsonOptions.jsonSettings);
        }

        // GET: api/Splits/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Split>> GetSplit(int id)
        {
            var split = await _context.Splits.FindAsync(id);

            if (split == null)
            {
                return NotFound();
            }

            return split;
        }

        // PUT: api/Splits/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSplit(int id, Split split)
        {
            if (id != split.Id)
            {
                return BadRequest();
            }

            _context.Entry(split).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SplitExists(id))
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
        public async Task<ActionResult<Split>> PostSplit(Split split)
        {
            _context.Splits.Add(split);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSplit", new { id = split.Id }, split);
        }

        // DELETE: api/Splits/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSplit(int id)
        {
            var split = await _context.Splits.FindAsync(id);
            if (split == null)
            {
                return NotFound();
            }

            _context.Splits.Remove(split);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SplitExists(int id)
        {
            return _context.Splits.Any(e => e.Id == id);
        }
    }
}
