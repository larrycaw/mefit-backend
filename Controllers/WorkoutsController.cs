using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MeFit.Models.Data;
using MeFit.Models.Domain;
using AutoMapper;
using MeFit.Models.DTOs.Workout;

namespace MeFit.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkoutsController : ControllerBase
    {
        private readonly MeFitDbContext _context;
        private readonly IMapper _mapper;


        public WorkoutsController(MeFitDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }

        // GET: api/Workouts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkoutReadDTO>>> GetAllWorkouts()
        {
            var workouts = _mapper.Map<List<WorkoutReadDTO>>(await _context.Workouts.Include(w =>  w.Programs).Include(w => w.Sets).Include(w => w.Goals).ToListAsync());
            return Ok(workouts);
        }

        // GET: api/Workouts/5
        [HttpGet("ById")]
        public async Task<ActionResult<WorkoutReadDTO>> GetWorkoutById([FromHeader(Name = "id")]int id)
        {
            var workout = _mapper.Map<WorkoutReadDTO>( await _context.Workouts.Include(w => w.Programs).Include(w => w.Sets).Include(w => w.Goals).Where(w => w.Id == id).FirstAsync());

            if (workout == null)
            {
                return NotFound();
            }


            return Ok(workout);
        }


        // POST: api/Workouts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Workout>> PostWorkout([FromBody] WorkoutCreateDTO workoutDTO)
        {
            var workout = _mapper.Map<Workout>(workoutDTO);

            try
            {
                _context.Add(workout);
                await _context.SaveChangesAsync();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            var newWorkout = _mapper.Map<WorkoutCreateDTO>(workout);

            return CreatedAtAction("GetWorkoutById", new { id = workout.Id }, newWorkout);
        }

        // DELETE: api/Workouts/5
        [HttpDelete]
        public async Task<IActionResult> DeleteWorkout([FromHeader(Name = "id")] int id)
        {
            var workout = await _context.Workouts.FindAsync(id);
            if (workout == null)
            {
                return NotFound();
            }

            _context.Workouts.Remove(workout);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateWorkout([FromHeader(Name = "id")] int id, [FromBody] WorkoutEditDTO workoutDto)
        {
            if(id != workoutDto.Id)
            {
                return BadRequest();
            }

            var domainWorkout = _mapper.Map<Workout>(workoutDto);
            _context.Entry(domainWorkout).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkoutExists(id))
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

        private bool WorkoutExists(int id)
        {
            return _context.Workouts.Any(e => e.Id == id);
        }
    }
}
