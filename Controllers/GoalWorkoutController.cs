using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MeFit.Models.Data;
using MeFit.Models.Domain;
using MeFit.Models.DTOs.WorkoutGoals;
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
    public class GoalWorkoutController : ControllerBase
    {
        private readonly MeFitDbContext _context;
        private readonly IMapper _mapper;

        public GoalWorkoutController(MeFitDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets all WorkoutGoals
        /// </summary>
        /// <returns>List of WorkoutGoals</returns>
        [HttpGet("all")]
        [Authorize(Policy = "isAdmin")]
        public async Task<ActionResult<IEnumerable<WorkoutGoalsReadDTO>>> GetWorkoutGoals()
        {
            var workoutGoals = _mapper.Map<List<WorkoutGoalsReadDTO>>(await _context.GoalWorkouts.ToListAsync());
            return Ok(workoutGoals);
        }

        /// <summary>
        /// Gets a goal by goal ID
        /// </summary>
        /// <param name="goalId">Goal ID</param>
        /// <returns></returns>
        [HttpGet("byGoalId")]
        [Authorize(Policy = "isUser")]
        public async Task<ActionResult<IEnumerable<WorkoutGoalsReadDTO>>> GetWorkoutGoalsById([FromHeader(Name = "goalId")] int goalId)
        {
            var workoutGoals = _mapper.Map<List<WorkoutGoalsReadDTO>>(await _context.GoalWorkouts.Where(gw => gw.GoalId == goalId).ToListAsync());
            return Ok(workoutGoals);
        }

        /// <summary>
        /// Create a new link between goal and workout.
        /// </summary>
        /// <param name="WorkoutGoals">The new workout goal</param>
        /// <returns></returns>
        [HttpPut]
        [Authorize(Policy = "isUser")]
        public async Task<ActionResult> AssignWorkoutGoal([FromBody] WorkoutGoalsCreateDTO WorkoutGoals)
        {
            var goal = _mapper.Map<GoalWorkouts>(WorkoutGoals);

            try
            {
                _context.Add(goal);
                await _context.SaveChangesAsync();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            var newWorkoutGoal = _mapper.Map<WorkoutGoalsCreateDTO>(goal);
            return Ok();
        }

        /// <summary>
        /// Updates a workout goal
        /// </summary>
        /// <param name="goalId">Goal ID</param>
        /// <param name="workoutId">Workout ID</param>
        /// <param name="workoutGoal">Workout Edit DTO (see schema)</param>
        /// <returns></returns>
        [HttpPut("update")]
        [Authorize(Policy = "isUser")]
        public async Task<IActionResult> UpdateWorkoutGoal([FromHeader(Name = "goalId")] int goalId, [FromHeader(Name = "workoutId")] int workoutId, [FromBody] WorkoutGoalsEditDTO workoutGoal)
        {
            if (goalId != workoutGoal.GoalId)
            {
                return BadRequest();
            }

            if (workoutId != workoutGoal.WorkoutId)
            {
                return BadRequest();
            }

            var domainWorkout = _mapper.Map<GoalWorkouts>(workoutGoal);
            _context.Entry(domainWorkout).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            return NoContent();
        }
    }
}
