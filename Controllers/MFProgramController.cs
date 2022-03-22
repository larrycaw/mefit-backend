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
using MeFit.Models.DTOs.Program;
using System.Net.Mime;

namespace MeFit.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class MFProgramController : ControllerBase
    {
        private readonly MeFitDbContext _context;
        private readonly IMapper _mapper;


        public MFProgramController(MeFitDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets all programs.
        /// 
        /// GET: api/MFProgram/all
        /// </summary>
        /// <returns>List of programs</returns>
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<ProgramReadDTO>>> GetAllPrograms()
        {
            var programs = _mapper.Map<List<ProgramReadDTO>>(await _context.Programs.Include(p => p.Workouts).ToListAsync());
            return Ok(programs);
        }

        /// <summary>
        /// Get program by id.
        /// 
        /// GET: api/MFProgram
        /// </summary>
        /// <param name="id">Program id</param>
        /// <returns>Program</returns>
        [HttpGet]
        public async Task<ActionResult<ProgramReadDTO>> GetProgramById([FromHeader(Name = "id")] int id)
        {
            var program = _mapper.Map<ProgramReadDTO>( await _context.Programs.Include(p => p.Workouts).Where(p => p.Id == id).FirstAsync());

            if (program == null)
            {
                return NotFound();
            }


            return Ok(program);
        }

        /// <summary>
        /// Posts a new program. Does not assign workouts.
        /// 
        /// POST: api/MFProgram
        /// </summary>
        /// <param name="programDto">Program to post</param>
        /// <returns>Newly created program</returns>
        [HttpPost]
        public async Task<ActionResult<MFProgram>> PostProgram([FromBody] ProgramCreateDTO programDto)
        {

            var program = _mapper.Map<MFProgram>(programDto);

            try
            {
                _context.Add(program);
                await _context.SaveChangesAsync();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            var newProgram = _mapper.Map<ProgramCreateDTO>(program);

            return CreatedAtAction("GetProgramById", new { Id = program.Id }, newProgram);
        }

        /// <summary>
        /// Delete a program by id.
        /// 
        /// DELETE: api/MFProgram/delete
        /// </summary>
        /// <param name="id">Program id</param>
        /// <returns>HTTP response code</returns>
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteProgram([FromHeader(Name = "id")] int id)
        {
            var programs = await _context.Programs.FindAsync(id);
            if (programs == null)
            {
                return NotFound();
            }

            _context.Programs.Remove(programs);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Updates a program. Does not assign workouts.
        /// 
        /// PUT: api/MFProgram/updateProgram
        /// </summary>
        /// <param name="id">Program id</param>
        /// <param name="programOtd">New program info</param>
        /// <returns>HTTP response code</returns>
        [HttpPut("updateProgram")]
        public async Task<IActionResult> UpdateProgram([FromHeader(Name = "id")]int id, [FromBody] ProgramEditDTO programOtd)
        {
            if(id != programOtd.Id)
            {
                return BadRequest();
            }
            MFProgram domainProgram = _mapper.Map<MFProgram>(programOtd);
            _context.Entry(domainProgram).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProgramExist(id))
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

        /// <summary>
        /// Assign new wokrouts to program.
        /// </summary>
        /// <param name="workouts">List of workout id's</param>
        /// <param name="id">Program id</param>
        /// <returns>HTTP response code</returns>
        [HttpPost("assignWorkouts")]
        public async Task<IActionResult> AssigneWorkouts([FromBody] List<int> workouts, [FromHeader(Name = "id")] int id)
        {
            var program = await _context.Programs.Include(p => p.Workouts).FirstOrDefaultAsync(p => p.Id == id);


            foreach (var wokrout in program.Workouts)
            {
                program.Workouts.Remove(wokrout);
            }

            if (program == null)
            {
                return NotFound();
            }

            foreach (var workoutId in workouts)
            {
                var tempWorkout = await _context.Workouts.FirstOrDefaultAsync(w => w.Id == workoutId);
                if(tempWorkout != null)
                {
                    program.Workouts.Add(tempWorkout);
                }
            }

            await _context.SaveChangesAsync();
            return NoContent();
        }



        private bool ProgramExist(int id)
        {
            return _context.Programs.Any(e => e.Id == id);
        }
    }
}
