using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MeFit.Models.Data;
using MeFit.Models.Domain;
using MeFit.Models.DTOs.Exercise;
using AutoMapper;

namespace MeFit.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExercisesController : ControllerBase
    {
        private readonly MeFitDbContext _context;
        private readonly IMapper _mapper;

        public ExercisesController(MeFitDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Exercises/all
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<ExerciseDTO>>> GetExercises()
        {
            var exercises = _mapper.Map<List<ExerciseDTO>>(await _context.Exercises.ToListAsync());
            return Ok(exercises);
        }

        // GET: api/Exercises
        [HttpGet]
        public async Task<ActionResult<ExerciseDTO>> GetExercise([FromHeader(Name = "id")] int id)
        {
            var exercise = _mapper.Map<ExerciseDTO>(await _context.Exercises.FindAsync(id));

            if (exercise == null)
            {
                return NotFound();
            }

            return Ok(exercise);
        }

        //PUT: api/Exercises

        [HttpPut]
        public async Task<IActionResult> UpdateExercise([FromBody] Exercise exercise, [FromHeader(Name = "id")] int id)
        {
            if (exercise.Id != id)
                return BadRequest();

            _context.Entry(exercise).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExerciseExists(exercise.Id))
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

        // POST: api/Exercises
        [HttpPost]
        public async Task<ActionResult<ExerciseDTO>> PostExercise([FromBody] ExerciseDTO exerciseDto)
        {
            var exercise = _mapper.Map<Exercise>(exerciseDto);

            try
            {
                _context.Add(exercise);
                await _context.SaveChangesAsync();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            var addedExercise = _mapper.Map<ExerciseDTO>(exercise);
            return CreatedAtAction("GetExercise", new { Id = exercise.Id }, addedExercise);
        }

        // DELETE: api/Exercises/delete
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteExercise([FromHeader(Name = "id")] int id)
        {
            var exercise = await _context.Exercises.FindAsync(id);
            if (exercise == null)
            {
                return NotFound();
            }

            _context.Exercises.Remove(exercise);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ExerciseExists(int id)
        {
            return _context.Exercises.Any(e => e.Id == id);
        }
    }
}
