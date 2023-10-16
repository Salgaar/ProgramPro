using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProgramPro.Server.Data;
using ProgramPro.Shared.Models;

namespace ProgramPro.Server.Controllers
{
    [ApiKeyAuthorization]
    [Route("api/[controller]")]
    [ApiController]
    public class SetDefinitionsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SetDefinitionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/SetDefinitions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SetDefinition>>> GetSetDefinitions()
        {
            return await _context.SetDefinitions.ToListAsync();
        }

        // GET: api/SetDefinitions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SetDefinition>> GetSetDefinition(int id)
        {
            var setDefinition = await _context.SetDefinitions.FindAsync(id);

            if (setDefinition == null)
            {
                return NotFound();
            }

            return setDefinition;
        }

        // PUT: api/SetDefinitions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSetDefinition(int id, SetDefinition setDefinition)
        {
            if (id != setDefinition.Id)
            {
                return BadRequest();
            }

            _context.Entry(setDefinition).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SetDefinitionExists(id))
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

        // POST: api/SetDefinitions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SetDefinition>> PostSetDefinition(SetDefinition setDefinition)
        {
            _context.SetDefinitions.Add(setDefinition);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSetDefinition", new { id = setDefinition.Id }, setDefinition);
        }

        // DELETE: api/SetDefinitions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSetDefinition(int id)
        {
            var setDefinition = await _context.SetDefinitions.FindAsync(id);
            if (setDefinition == null)
            {
                return NotFound();
            }

            _context.SetDefinitions.Remove(setDefinition);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SetDefinitionExists(int id)
        {
            return _context.SetDefinitions.Any(e => e.Id == id);
        }
    }
}
