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

        /// <summary>
        /// Get all workouts.
        /// 
        /// GET: api/Workouts/all
        /// </summary>
        /// <returns>List of workouts</returns>
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<WorkoutReadDTO>>> GetAllWorkouts()
        {
            var workouts = _mapper.Map<List<WorkoutReadDTO>>(await _context.Workouts.Include(w =>  w.Programs).Include(w => w.Sets).Include(w => w.Goals).ToListAsync());
            return Ok(workouts);
        }

        /// <summary>
        /// Get workout by id.
        /// 
        /// GET: api/Workouts
        /// </summary>
        /// <param name="id">Workout id</param>
        /// <returns>A workout</returns>
        [HttpGet]
        public async Task<ActionResult<WorkoutReadDTO>> GetWorkoutById([FromHeader(Name = "id")]int id)
        {
            var workout = _mapper.Map<WorkoutReadDTO>( await _context.Workouts.Include(w => w.Programs).Include(w => w.Sets).Include(w => w.Goals).Where(w => w.Id == id).FirstAsync());

            if (workout == null)
            {
                return NotFound();
            }


            return Ok(workout);
        }


        /// <summary>
        /// Post a new workout. Does not assign sets.
        /// 
        /// POST: api/Workouts
        /// </summary>
        /// <param name="workoutDTO">Info to create a new workout</param>
        /// <returns>Newly added workout</returns>
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

        /// <summary>
        /// Delete a workout by id.
        /// 
        /// DELETE: api/Workouts/delete
        /// </summary>
        /// <param name="id">Workout id</param>
        /// <returns>HTTP response code</returns>
        [HttpDelete("delete")]
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

        /// <summary>
        /// Updates a workout. Does not assign sets.
        /// 
        /// PUT: api/Workouts
        /// </summary>
        /// <param name="id">Workout id</param>
        /// <param name="workoutDto">Info to update workout with</param>
        /// <returns>HTTP response code</returns>
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

        /// <summary>
        /// Assigne sets to a workout.
        /// 
        /// POST: api/Workouts/assignSets
        /// </summary>
        /// <param name="sets">List of set Id's</param>
        /// <param name="id">Workout id</param>
        /// <returns>HTTP response code</returns>
        [HttpPost("assignSets")]
        public async Task<IActionResult> AssigneSets([FromBody] List<int> sets, int id)
        {
            var workout = await _context.Workouts.Include(w => w.Sets).FirstOrDefaultAsync(w => w.Id == id);

            foreach (var set in workout.Sets)
            {
                workout.Sets.Remove(set);
            }

            if (workout == null)
            {
                return NotFound();
            }

            foreach (var setId in sets)
            {
                var tempSet = await _context.Sets.FirstOrDefaultAsync(w => w.Id == setId);
                if (tempSet != null)
                {
                    workout.Sets.Add(tempSet);
                }
            }

            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool WorkoutExists(int id)
        {
            return _context.Workouts.Any(e => e.Id == id);
        }
    }
}
