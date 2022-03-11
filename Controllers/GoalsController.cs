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

        // GET: api/Goals/all
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<GoalReadDTO>>> GetGoals()
        {
            var goals = _mapper.Map<List<GoalReadDTO>>(await _context.Goals.Include(g => g.Workouts).ToListAsync());
            return Ok(goals);
        }

        // GET: api/Goals
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

        // GET: api/UserGoal
        [HttpGet("userGoal")]
        public async Task<ActionResult<IEnumerable<GoalByUserDTO>>> GetUserGoals([FromHeader(Name = "userId")] string userId)
        {
            var goals = _mapper.Map<List<GoalByUserDTO>>(await _context.Goals.Include(g => g.Workouts).Where(g => g.ProfileId == userId).ToListAsync());
            return Ok(goals);
        }


        // DELETE: api/Goals/delete
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
