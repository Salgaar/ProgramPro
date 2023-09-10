﻿using System;
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
    [Route("api/[controller]")]
    [ApiController]
    public class SetsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SetsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Sets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Set>>> GetSet()
        {
            return await _context.Set.ToListAsync();
        }

        // GET: api/Sets/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Set>> GetSet(int id)
        {
            var @set = await _context.Set.FindAsync(id);

            if (@set == null)
            {
                return NotFound();
            }

            return @set;
        }

        // PUT: api/Sets/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSet(int id, Set @set)
        {
            if (id != @set.Id)
            {
                return BadRequest();
            }

            _context.Entry(@set).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SetExists(id))
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

        // POST: api/Sets
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Set>> PostSet(Set @set)
        {
            _context.Set.Add(@set);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSet", new { id = @set.Id }, @set);
        }

        [HttpPost("PostSets")]
        public async Task<ActionResult<IEnumerable<Set>>> PostSets(List<Set> sets)
        
        {
            if (ModelState.IsValid)
            {
                _context.Set.AddRange(sets);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetSet", sets);
            }
            else
            {
                return BadRequest(ModelState);
            }

        }

        // DELETE: api/Sets/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSet(int id)
        {
            var @set = await _context.Set.FindAsync(id);
            if (@set == null)
            {
                return NotFound();
            }

            _context.Set.Remove(@set);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SetExists(int id)
        {
            return _context.Set.Any(e => e.Id == id);
        }
    }
}
