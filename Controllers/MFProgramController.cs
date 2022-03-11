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
        [HttpGet("byId")]
        public async Task<IActionResult> GetProgramById([FromBody] int id)
        {
            var program = await _context.Programs.FindAsync(id);

            if (program == null)
            {
                return NotFound();
            }

            var programDto = _mapper.Map<ProgramReadDTO>(program);

            return Ok(programDto);
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

        // POST: api/Addresses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Address>> PostAddress(Address address)
        {
            _context.Addresses.Add(address);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAddress", new { id = address.Id }, address);
        }

        // DELETE: api/Addresses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAddress(int id)
        {
            var address = await _context.Addresses.FindAsync(id);
            if (address == null)
            {
                return NotFound();
            }

            _context.Addresses.Remove(address);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProgramExist(int id)
        {
            return _context.Programs.Any(e => e.Id == id);
        }
    }
}
