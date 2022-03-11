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

        private bool GoalExists(int id)
        {
            return _context.Goals.Any(e => e.Id == id);
        }
    }
}
