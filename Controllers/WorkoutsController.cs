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
using MeFit.Models.DTOs.Exercise;
using System.Net.Mime;
using Microsoft.AspNetCore.Authorization;

namespace MeFit.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ApiConventionType(typeof(DefaultApiConventions))]
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
        /// </summary>
        /// <returns>List of workouts</returns>
        [HttpGet("all")]
        [Authorize(Policy = "isUser")]
        public async Task<ActionResult<IEnumerable<WorkoutReadDTO>>> GetAllWorkouts()
        {
            var workouts = _mapper.Map<List<WorkoutReadDTO>>(await _context.Workouts.Include(w =>  w.Programs).Include(w => w.Sets).Include(w => w.WorkoutGoals).ToListAsync());
            return Ok(workouts);
        }

        /// <summary>
        /// Get workout by id.
        /// </summary>
        /// <param name="id">Workout id</param>
        /// <returns>A workout</returns>
        [HttpGet]
        [Authorize(Policy = "isUser")]
        public async Task<ActionResult<WorkoutReadDTO>> GetWorkoutById([FromHeader(Name = "id")]int id)
        {
            var workout = _mapper.Map<WorkoutReadDTO>( await _context.Workouts.Include(w => w.Programs).Include(w => w.Sets).Include(w => w.WorkoutGoals).Where(w => w.Id == id).FirstAsync());

            if (workout == null)
            {
                return NotFound();
            }


            return Ok(workout);
        }


        /// <summary>
        /// Post a new workout. Does not assign sets.
        /// </summary>
        /// <param name="workoutDTO">Info to create a new workout</param>
        /// <returns>Newly added workout</returns>
        [HttpPost]
        [Authorize(Policy = "isContributor")]
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
        /// </summary>
        /// <param name="id">Workout id</param>
        /// <returns>HTTP response code</returns>
        [HttpDelete("delete")]
        [Authorize(Policy = "isContributor")]
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
        /// </summary>
        /// <param name="id">Workout id</param>
        /// <param name="workoutDto">Info to update workout with</param>
        /// <returns>HTTP response code</returns>
        [HttpPut]
        [Authorize(Policy = "isContributor")]
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
        /// </summary>
        /// <param name="sets">List of set Id's</param>
        /// <param name="id">Workout id</param>
        /// <returns>HTTP response code</returns>
        [HttpPut("assignSets")]
        [Authorize(Policy = "isUser")]
        public async Task<IActionResult> AssigneSets([FromBody] List<int> sets, [FromHeader(Name = "id")] int id)
        {
            var workout = await _context.Workouts.Include(w => w.Sets).FirstOrDefaultAsync(w => w.Id == id);


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

        /// <summary>
        /// Assign sets to a workout by exercise id's
        /// </summary>
        /// <param name="exercises">List of exercise id's</param>
        /// <param name="id">Workout id</param>
        /// <returns></returns>
        [HttpPut("assignSetsByExercise")]
        [Authorize(Policy = "isUser")]

        public async Task<IActionResult> AssigneSetsByExercise([FromBody] List<int> exercises, [FromHeader(Name = "id")] int id)
        {
            var workout = await _context.Workouts.Include(w => w.Sets).FirstOrDefaultAsync(w => w.Id == id);

            foreach (var set in workout.Sets)
            {
                workout.Sets.Remove(set);
            }

            List<int> sets = new List<int>();
            foreach (var exercise in exercises)
            {
                var query =
                    from s in _context.Sets
                    where s.ExerciseId == exercise
                    select s.Id;

                if(query != null)
                {
                    var qList = query.ToList();
                    foreach (var setId in qList)
                    {
                        sets.Add(setId);
                    }
                }
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

        /// <summary>
        /// Get all exercises in a workout.
        /// </summary>
        /// <param name="id">Workout id</param>
        /// <returns>List of exercises</returns>
        [HttpGet("exercises")]
        [Authorize(Policy = "isUser")]
        public async Task<ActionResult<IEnumerable<ExerciseReadDTO>>> GetExercisesInWorkout([FromHeader(Name = "id")] int id)
        {
            var query =
                from workout in _context.Workouts.Include(w => w.Sets)
                where workout.Id == id
                select workout;

            var workoutSet = _mapper.Map<WorkoutReadDTO>(await query.FirstAsync());
        
            var query2 =
                from s in _context.Sets
                where workoutSet.Sets.Contains(s.Id)
                select s.ExerciseId;

            var query3 =
                from e in _context.Exercises
                where query2.Contains(e.Id)
                select e;

            var exercises = _mapper.Map<List<ExerciseReadDTO>>(await query3.ToListAsync());

            if (exercises == null)
            {
                return NotFound();
            }


            return Ok(exercises);
        }

        private bool WorkoutExists(int id)
        {
            return _context.Workouts.Any(e => e.Id == id);
        }
    }
}
