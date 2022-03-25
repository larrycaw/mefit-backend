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
using System.Net.Mime;
using Microsoft.AspNetCore.Authorization;

namespace MeFit.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class ExercisesController : ControllerBase
    {
        private readonly MeFitDbContext _context;
        private readonly IMapper _mapper;

        public ExercisesController(MeFitDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets all exercises
        /// </summary>
        /// <returns>List of exercises</returns>
        [HttpGet("all")]
        [Authorize(Policy = "isUser")]
        public async Task<ActionResult<IEnumerable<ExerciseReadDTO>>> GetExercises()
        {
            var exercises = _mapper.Map<List<ExerciseReadDTO>>(await _context.Exercises.ToListAsync());
            return Ok(exercises);
        }

        /// <summary>
        /// Gets exercise given exercise ID
        /// </summary>
        /// <param name="id">Exercise ID</param>
        /// <returns>Exercise</returns>
        [HttpGet]
        [Authorize(Policy = "isUser")]
        public async Task<ActionResult<ExerciseReadDTO>> GetExercise([FromHeader(Name = "id")] int id)
        {
            var exercise = _mapper.Map<ExerciseReadDTO>(await _context.Exercises.FindAsync(id));

            if (exercise == null)
            {
                return NotFound();
            }

            return Ok(exercise);
        }

        /// <summary>
        /// Updates exercise
        /// </summary>
        /// <param name="exercise">Exercise object</param>
        /// <param name="id">Exercise ID</param>
        /// <returns>HTTP response code</returns>

        [HttpPut]
        [Authorize(Policy = "isContributor")]
        public async Task<IActionResult> UpdateExercise([FromBody] ExerciseUpdateDTO exercise, [FromHeader(Name = "id")] int id)
        {
            if (exercise.Id != id)
                return BadRequest();

            Exercise domainExercise = _mapper.Map<Exercise>(exercise);
            _context.Entry(domainExercise).State = EntityState.Modified;

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

        /// <summary>
        /// Posts exercise
        /// </summary>
        /// <param name="exerciseDto">Exercise to post</param>
        /// <returns>Newly created exercise</returns>
        [HttpPost]
        [Authorize(Policy = "isContributor")]
        public async Task<ActionResult<ExerciseReadDTO>> PostExercise([FromBody] ExerciseCreateDTO exerciseDto)
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

            var addedExercise = _mapper.Map<ExerciseReadDTO>(exercise);
            return CreatedAtAction("GetExercise", new { Id = exercise.Id }, addedExercise);
        }

        /// <summary>
        /// Deletes exercise
        /// </summary>
        /// <param name="id">Exercise ID</param>
        /// <returns>HTTP response code</returns>
        [HttpDelete("delete")]
        [Authorize(Policy = "isContributor")]
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
