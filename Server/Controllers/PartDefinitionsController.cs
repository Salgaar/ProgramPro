using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProgramPro.Server.Data;
using ProgramPro.Server.Helpers;
using ProgramPro.Shared.Models;

namespace ProgramPro.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PartDefinitionsController : ControllerBase
    {
        private readonly ProgramProDbContext _context;

        public PartDefinitionsController(ProgramProDbContext context)
        {
            _context = context;
        }

        // GET: api/PartDefinitions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PartDefinition>>> GetPartDefinitions()
        {
            return await _context.PartDefinitions.Where(x => x.UserId == UserHelper.GetUserId(User)).ToListAsync();
        }

        // GET: api/PartDefinitions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PartDefinition>> GetPartDefinition(int id)
        {
            var partDefinition = await _context.PartDefinitions.Where(x => x.UserId == UserHelper.GetUserId(User)).FirstOrDefaultAsync(x => x.Id == id);

            if (partDefinition == null)
            {
                return NotFound();
            }

            return partDefinition;
        }

        // PUT: api/PartDefinitions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPartDefinition(int id, PartDefinition partDefinition)
        {
            if (id != partDefinition.Id)
            {
                return BadRequest();
            }

            _context.Entry(partDefinition).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PartDefinitionExists(id))
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

        // POST: api/PartDefinitions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PartDefinition>> PostPartDefinition(PartDefinition partDefinition)
        {
            partDefinition.UserId = UserHelper.GetUserId(User);
            _context.PartDefinitions.Add(partDefinition);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPartDefinition", new { id = partDefinition.Id }, partDefinition);
        }

        // DELETE: api/PartDefinitions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePartDefinition(int id)
        {
            var partDefinition = await _context.PartDefinitions.FindAsync(id);
            if (partDefinition == null)
            {
                return NotFound();
            }

            _context.PartDefinitions.Remove(partDefinition);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PartDefinitionExists(int id)
        {
            return _context.PartDefinitions.Any(e => e.Id == id);
        }
    }
}
