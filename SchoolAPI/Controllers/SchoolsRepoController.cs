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
using SchoolAPI.Repositories;
using NuGet.Protocol.Core.Types;

namespace SchoolAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchoolsRepoController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUniversityRepository _universityRepository;

        public SchoolsRepoController(IUniversityRepository universityRepository, IMapper mapper)
        {
            _universityRepository = universityRepository;
            _mapper = mapper;
        }

        [HttpGet("list-schools")]
        public async Task<ActionResult<IEnumerable<SchoolDto>>> ListSchools()
        {
            // Appel au Repository (au lieu de _context.Schools)
            var schools = _universityRepository.GetSchools();


            var schoolDtos = schools
                .Select(school => _mapper.Map<SchoolDto>(school))
                .ToList();

            return Ok(schoolDtos);
        }
            [HttpPost("add-new-school")]
        public async Task<IActionResult> AddSchool(SchoolDto newschool)
        {
            
            var school = _mapper.Map<School>(newschool);
            _universityRepository.AddSchool(school);
        
            return Ok();
        }

        // GET: api/Schools
        [HttpGet("get-all-schools")]
        public async Task<ActionResult<IEnumerable<School>>> GetSchools()
        {
            var schools = _universityRepository.GetSchools();


            var schoolDtos = schools
                .Select(school => _mapper.Map<SchoolDto>(school))
                .ToList();

            return Ok(schoolDtos);
        }

        // GET: api/Schools/id
        [HttpGet("get-school-by-id/{id}")]
        public async Task<ActionResult<School>> GetSchool(int id)
        {
            var school = _universityRepository.GetSchoolById(id) ;

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

            var schools = _universityRepository.GetSchoolsByName(name);
                
            if (schools.Count() == 0)
            {
                return NotFound($"Aucune école trouvée avec le nom contenant '{name}'.");
            }

            return schools.ToList();
        }

        // PUT: api/Schools/id
        [HttpPut("edit-school/{id}")]
        public async Task<IActionResult> PutSchool(int id, School school)
        {
            if (id != school.Id)
            {
                return BadRequest();
            }

            try
            {
                _universityRepository.UpdateSchool(school);
            }
            catch (Exception)
            {
                if (_universityRepository.GetSchoolById(id) == null)
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
            _universityRepository.AddSchool(school);
            

            return CreatedAtAction("GetSchool", new { id = school.Id }, school);
        }

        // DELETE: api/Schools/id
        [HttpDelete("delete-school/{id}")]
        public async Task<IActionResult> DeleteSchool(int id)
        {
            var school = _universityRepository.GetSchoolById(id);
            if (school == null)
            {
                return NotFound();
            }

            _universityRepository.DeleteSchool(id);

            return NoContent();
        }

    }
}
