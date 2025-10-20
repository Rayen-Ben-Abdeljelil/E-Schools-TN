using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolAPI.Models;
using SchoolAPI.DTOs;
using Microsoft.AspNetCore.Http.HttpResults;

namespace SchoolAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchoolsController : ControllerBase
    {
        private readonly SchoolDbContext _context;
        private readonly IMapper _mapper;

        public SchoolsController(SchoolDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("list-schools")]
        public async Task<ActionResult<IEnumerable<SchoolDto>>> ListSchools()
        {
            return  Ok(_context.Schools.Select(school => _mapper.Map<SchoolDto>(school)));
        }

        [HttpPost("add-new-school")]
        public async Task<IActionResult> AddSchool(SchoolDto newschool)
        {
            
            var school = _mapper.Map<School>(newschool);
            _context.Schools.Add(school);
            await _context.SaveChangesAsync();

            return Ok();
        }

        // GET: api/Schools
        [HttpGet("get-all-schools")]
        public async Task<ActionResult<IEnumerable<School>>> GetSchools()
        {
            return await _context.Schools.ToListAsync();
        }

        // GET: api/Schools/id
        [HttpGet("get-school-by-id/{id}")]
        public async Task<ActionResult<School>> GetSchool(int id)
        {
            var school = await _context.Schools.FindAsync(id);

            if (school == null)
            {
                return NotFound();
            }

            return school;
        }

        // GET: api/Schools/search-by-name
        [HttpGet("search-by-name")]
        public async Task<ActionResult<IEnumerable<School>>> SearchByName([FromQuery] string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return BadRequest("Le nom de recherche ne peut pas être vide.");
            }

            var schools = await _context.Schools
                .Where(s => s.Name.Contains(name))
                .ToListAsync();

            if (schools.Count == 0)
            {
                return NotFound($"Aucune école trouvée avec le nom contenant '{name}'.");
            }

            return schools;
        }

        // PUT: api/Schools/id
        [HttpPut("edit-school/{id}")]
        public async Task<IActionResult> PutSchool(int id, School school)
        {
            if (id != school.Id)
            {
                return BadRequest();
            }

            _context.Entry(school).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SchoolExists(id))
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

        // POST: api/Schools
        [HttpPost("create-school")]
        public async Task<ActionResult<School>> PostSchool(School school)
        {
            _context.Schools.Add(school);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSchool", new { id = school.Id }, school);
        }

        // DELETE: api/Schools/id
        [HttpDelete("delete-school/{id}")]
        public async Task<IActionResult> DeleteSchool(int id)
        {
            var school = await _context.Schools.FindAsync(id);
            if (school == null)
            {
                return NotFound();
            }

            _context.Schools.Remove(school);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SchoolExists(int id)
        {
            return _context.Schools.Any(e => e.Id == id);
        }
    }
}
