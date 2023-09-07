using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProgramPro.Server.Data;
using ProgramPro.Shared.Models;

namespace ProgramPro.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PersonalRecordsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PersonalRecordsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/PersonalRecords
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonalRecord>>> GetPersonalRecords()
        {
            return await _context.PersonalRecords.ToListAsync();
        }

        // GET: api/PersonalRecords/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PersonalRecord>> GetPersonalRecord(int id)
        {
            var personalRecord = await _context.PersonalRecords.FindAsync(id);

            if (personalRecord == null)
            {
                return NotFound();
            }

            return personalRecord;
        }

        // PUT: api/PersonalRecords/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPersonalRecord(int id, PersonalRecord personalRecord)
        {
            if (id != personalRecord.Id)
            {
                return BadRequest();
            }

            _context.Entry(personalRecord).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonalRecordExists(id))
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

        // POST: api/PersonalRecords
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PersonalRecord>> PostPersonalRecord(PersonalRecord personalRecord)
        {
            _context.PersonalRecords.Add(personalRecord);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPersonalRecord", new { id = personalRecord.Id }, personalRecord);
        }

        // DELETE: api/PersonalRecords/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePersonalRecord(int id)
        {
            var personalRecord = await _context.PersonalRecords.FindAsync(id);
            if (personalRecord == null)
            {
                return NotFound();
            }

            _context.PersonalRecords.Remove(personalRecord);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PersonalRecordExists(int id)
        {
            return _context.PersonalRecords.Any(e => e.Id == id);
        }
    }
}
