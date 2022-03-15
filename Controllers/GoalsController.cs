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
using MeFit.Models.DTOs.Goal;

namespace MeFit.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GoalsController : ControllerBase
    {
        private readonly MeFitDbContext _context;
        private readonly IMapper _mapper;

        public GoalsController(MeFitDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

         }

        /// <summary>
        /// Gets all goals, regardless of which user it belongs to
        /// 
        /// GET: api/Goals/all
        /// </summary>
        /// <returns>List of goals</returns>
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<GoalReadDTO>>> GetGoals()
        {
            var goals = _mapper.Map<List<GoalReadDTO>>(await _context.Goals.Include(g => g.Workouts).ToListAsync());
            return Ok(goals);
        }

        /// <summary>
        /// Gets goal given goal ID
        /// 
        /// GET: api/Goals
        /// </summary>
        /// <param name="id">Goal ID</param>
        /// <returns>Goal</returns>
        [HttpGet]
        public async Task<ActionResult<GoalReadDTO>> GetGoal([FromHeader(Name = "id")] int id)
        {
            var goal = _mapper.Map<GoalReadDTO>(await _context.Goals.Include(g => g.Workouts).FirstOrDefaultAsync(g => g.Id == id));

            if (goal == null)
            {
                return NotFound();
            }

            return Ok(goal);
        }

        /// <summary>
        /// Gets all goals of a user
        /// 
        /// GET: api/Goals/user
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <returns>List of goals</returns>
        [HttpGet("user")]
        public async Task<ActionResult<IEnumerable<GoalByUserDTO>>> GetUserGoals([FromHeader(Name = "userId")] string userId)
        {
            var goals = _mapper.Map<List<GoalByUserDTO>>(await _context.Goals.Include(g => g.Workouts).Where(g => g.ProfileId == userId).ToListAsync());
            return Ok(goals);
        }

        /// <summary>
        /// Gets user's current unfinished goal
        /// 
        /// GET: api/Goals/CurrentGoal
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <returns>Current goal</returns>
        [HttpGet("currentGoal")]
        public async Task<ActionResult<GoalByUserDTO>> GetCurrentUserGoal([FromHeader(Name = "userId")] string userId)
        {
            var currentGoal = _mapper.Map<GoalByUserDTO>(await _context.Goals.Include(g => g.Workouts).Where(g => g.ProfileId == userId).Where(g => g.Achieved == false).FirstOrDefaultAsync());

            if (currentGoal == null)
                return NotFound();

            return Ok(currentGoal);
        }


        /// <summary>
        /// Deletes goal given goal ID
        /// 
        /// DELETE: api/Goals/delete
        /// </summary>
        /// <param name="id">Goal ID</param>
        /// <returns>HTTP response code</returns>
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteGoal([FromHeader(Name = "id")] int id)
        {
            var goal = await _context.Goals.FindAsync(id);
            if (goal == null)
            {
                return NotFound();
            }

            _context.Goals.Remove(goal);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Posts a new goal. Does not assign workouts
        /// </summary>
        /// <param name="goalDto">Goal to post</param>
        /// <param name="Workouts">Ids of workouts to be added to goal</param>
        /// <returns>Newly created goal</returns>
        [HttpPost]
        public async Task<ActionResult<GoalReadDTO>> PostGoal([FromBody] GoalCreateDTO goalDto)
        {
            // Check if user already has an active goal
            var activeGoal = _mapper.Map<GoalByUserDTO>(await _context.Goals.Include(g => g.Workouts).Where(g => g.ProfileId == goalDto.ProfileId).Where(g => g.Achieved == false).FirstOrDefaultAsync());

            // User cannot add new goal if it already has an active goal
            if (activeGoal != null)
                return Conflict();

            var goal = _mapper.Map<Goal>(goalDto);

            try
            {
                _context.Add(goal);
                await _context.SaveChangesAsync();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            var addedGoal = _mapper.Map<GoalReadDTO>(goal);
            return CreatedAtAction("GetGoal", new { Id = goal.Id }, addedGoal);
        }

        /// <summary>
        /// Assigns workouts to a goal. Overwrites current workouts assigned to goal
        /// </summary>
        /// <param name="Workouts">Workout IDs</param>
        /// <param name="goalId">Goal ID</param>
        /// <returns>HTTP response message</returns>
        [HttpPut("assignWorkouts")]
        public async Task<ActionResult> AssignWorkoutToGoal([FromBody] List<int> Workouts, [FromHeader(Name = "GoalID")] int goalId)
        {

            var goal = await _context.Goals.Include(g => g.Workouts).FirstOrDefaultAsync(g => g.Id == goalId);

            foreach(var workout in goal.Workouts)
            {
                goal.Workouts.Remove(workout);
            }

            if (goal == null)
            {
                return NotFound();
            }

            foreach (var workoutId in Workouts)
            {
                var tempWorkout = await _context.Workouts.FirstOrDefaultAsync(w => w.Id == workoutId);
                if (tempWorkout != null)
                {
                    goal.Workouts.Add(tempWorkout);
                }
            }

            await _context.SaveChangesAsync();
            return NoContent();
        }

        /// <summary>
        /// Assigns new program to goal
        /// </summary>
        /// <param name="programId">Program ID</param>
        /// <param name="goalId">Goal ID</param>
        /// <returns>HTTP request message</returns>
        [HttpPut("assignProgram")]
        public async Task<ActionResult> AssignProgramToGoal([FromBody] int programId, [FromHeader(Name = "GoalID")] int goalId)
        {
            var goal = await _context.Goals.Include(g => g.Workouts).FirstOrDefaultAsync(g => g.Id == goalId);

            if (goal == null)
            {
                return NotFound();
            }

            var tempProgram = await _context.Programs.FirstOrDefaultAsync(p => p.Id == programId);

            if (tempProgram != null)
                goal.Program = tempProgram;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        /// <summary>
        /// Updates a goal. Does not assign workouts.
        /// </summary>
        /// <param name="goalDto">New goal info</param>
        /// <param name="goalID">Goal ID</param>
        /// <returns>HTTP response message</returns>
        [HttpPut("updateGoal")]
        public async Task<IActionResult> UpdateGoal([FromBody] GoalEditDTO goalDto, [FromHeader(Name = "goalID")] int goalID)
        {
            if (goalID != goalDto.Id || !ProfileExists(goalDto.ProfileId))
            {
                return BadRequest();
            }

            var domainGoal = _mapper.Map<Goal>(goalDto);
            _context.Entry(domainGoal).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GoalExists(goalID))
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

        private bool GoalExists(int id)
        {
            return _context.Goals.Any(e => e.Id == id);
        }

        private bool ProgramExists(int id)
        {
            return _context.Programs.Any(e => e.Id == id);
        }

        private bool ProfileExists(string id)
        {
            return _context.Profiles.Any(e => e.Id == id);
        }
    }
}
