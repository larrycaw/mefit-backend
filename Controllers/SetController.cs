using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MeFit.Models.Data;
using MeFit.Models.Domain;
using MeFit.Models.DTOs;
using AutoMapper;
using MeFit.Models.DTOs.Set;
using System.Net.Mime;
using Microsoft.AspNetCore.Authorization;

namespace MeFit.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class SetController : ControllerBase
    {
        private readonly MeFitDbContext _context;
        private readonly IMapper _mapper;

        public SetController(MeFitDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets all sets.
        /// </summary>
        /// <returns>List of sets</returns>
        [HttpGet("all")]
        [Authorize(Policy = "isUser")]
        public async Task<ActionResult<IEnumerable<SetReadDTO>>> GetAllSets()
        {
            var sets = _mapper.Map<List<SetReadDTO>>(await _context.Sets.Include(p => p.Workouts).ToListAsync());
            return Ok(sets);
        }

        /// <summary>
        /// Get set by id.
        /// </summary>
        /// <param name="id">Set id</param>
        /// <returns>Set</returns>
        [HttpGet]
        [Authorize(Policy = "isUser")]
        public async Task<ActionResult<SetReadDTO>> GetSetById([FromHeader(Name = "id")] int id)
        {
            var sets = _mapper.Map<SetReadDTO>( await _context.Sets.Include(s => s.Exercise).Include(s => s.Workouts).Where(s => s.Id == id).FirstAsync());

            if (sets == null)
            {
                return NotFound();
            }


            return Ok(sets);
        }

        /// <summary>
        /// Posts a new set.
        /// </summary>
        /// <param name="setDto">Set to post</param>
        /// <returns>Newly created set</returns>
        [HttpPost]
        [Authorize(Policy = "isUser")]
        public async Task<ActionResult<Set>> PostSet([FromBody] SetCreateDTO setDto)
        {

            var set = _mapper.Map<Set>(setDto);

            try
            {
                _context.Add(set);
                await _context.SaveChangesAsync();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            var newSet = _mapper.Map<SetReadDTO>(set);

            return CreatedAtAction("GetSetById", new { Id = set.Id }, newSet);
        }

        /// <summary>
        /// Delete a set by id.
        /// </summary>
        /// <param name="id">Set id</param>
        /// <returns>HTTP response code</returns>
        [HttpDelete("delete")]
        [Authorize(Policy = "isContributor")]
        public async Task<IActionResult> DeleteSet([FromHeader(Name = "id")] int id)
        {
            var sets = await _context.Sets.FindAsync(id);
            if (sets == null)
            {
                return NotFound();
            }

            _context.Sets.Remove(sets);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Updates a set.
        /// </summary>
        /// <param name="id">Set id</param>
        /// <param name="setDto">New set info</param>
        /// <returns>HTTP response code</returns>
        [HttpPut("updateSet")]
        [Authorize(Policy = "isUser")]
        public async Task<IActionResult> UpdateSets([FromHeader(Name = "id")] int id, [FromBody] SetEditDTO setDto)
        {
            if(id != setDto.Id)
            {
                return BadRequest();
            }

            Set domainSet = _mapper.Map<Set>(setDto);
            _context.Entry(domainSet).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SetExist(id))
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

        private bool SetExist(int id)
        {
            return _context.Sets.Any(e => e.Id == id);
        }
    }
}
