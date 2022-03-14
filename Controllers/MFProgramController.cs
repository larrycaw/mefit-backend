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

namespace MeFit.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MFProgramController : ControllerBase
    {
        private readonly MeFitDbContext _context;
        private readonly IMapper _mapper;


        public MFProgramController(MeFitDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Programs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProgramReadDTO>>> GetAllPrograms()
        {
            var programs = _mapper.Map<List<ProgramReadDTO>>(await _context.Programs.Include(p => p.Workouts).ToListAsync());
            return Ok(programs);
        }

        // GET: api/Programs/5
        [HttpGet("ById")]
        public async Task<ActionResult<ProgramReadDTO>> GetProgramById([FromHeader(Name = "id")] int id)
        {
            var program = _mapper.Map<ProgramReadDTO>( await _context.Programs.Include(p => p.Workouts).Where(p => p.Id == id).FirstAsync());

            if (program == null)
            {
                return NotFound();
            }


            return Ok(program);
        }

        // POST: api/Programs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
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

        // DELETE: api/Addresses/5
        [HttpDelete]
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

        [HttpPut]
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

        private bool ProgramExist(int id)
        {
            return _context.Programs.Any(e => e.Id == id);
        }
    }
}
