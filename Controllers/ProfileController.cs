using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MeFit.Models.Data;
using MeFit.Models.DTOs.Goal;
using MeFit.Models.DTOs.Profile;
using Profile = MeFit.Models.Domain.Profile;
using System.Net.Mime;

namespace MeFit.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class ProfileController : ControllerBase
    {
        
        private readonly MeFitDbContext _context;
        private readonly IMapper _mapper;
        public ProfileController(MeFitDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        
        /// <summary>
         /// Gets all profiles
         /// 
         /// GET: api/Profile/all
         /// </summary>
         /// <returns>List of profiles</returns>
         [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<ProfileReadDTO>>> GetProfiles()
        {
            var profiles = _mapper.Map<List<ProfileReadDTO>>(await _context.Profiles.ToListAsync());
            return Ok(profiles);
         }
         
         [HttpGet]
         public async Task<ActionResult<ProfileReadDTO>> GetProfile([FromHeader(Name = "id")] string id)
         {
             var profile = _mapper.Map<ProfileReadDTO>(await _context.Profiles.FindAsync(id));
             
             if (profile == null)
             {
                 return NotFound();
             }
             
             return Ok(profile);
         }
         
         /// <summary>
         /// Posts profile
         /// 
         /// POST: api/Profile
         /// </summary>
         /// <param name="profileDto">Profile to post</param>
         /// <returns>Newly created profile</returns>
         [HttpPost]
         public async Task<ActionResult<ProfileReadDTO>> PostProfile([FromBody] ProfileCreateDTO profileDto)
         {
             var profile = _mapper.Map<Profile>(profileDto);
             
             try
             {
                 _context.Profiles.Add(profile);
                 await _context.SaveChangesAsync();
             }
             catch
             {
                 return StatusCode(StatusCodes.Status500InternalServerError);
             }
             
             var newProfile = _mapper.Map<ProfileReadDTO>(profile);
             return CreatedAtAction("GetProfile", new { profile.Id }, newProfile);
         }
         
         /// <summary>
         /// Updates profile
         /// 
         /// PUT: api/Profile
         /// </summary>
         /// <param name="profile">Profile object</param>
         /// <param name="id">Profile ID</param>
         /// <returns>HTTP response code</returns>
         [HttpPut]
         public async Task<IActionResult> UpdateProfile([FromBody] ProfileUpdateDTO profile, [FromHeader(Name = "id")] string id)
         {
             if (profile.Id != id)
                 return BadRequest();
             
             var domainProfile = _mapper.Map<Profile>(profile);
             _context.Entry(domainProfile).State = EntityState.Modified;
             
             try
             {
                 await _context.SaveChangesAsync();
             }
             catch (DbUpdateConcurrencyException)
             {
                 if (!ProfileExists(profile.Id))
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
         /// Deletes profile
         /// 
         /// DELETE: api/Profile/delete
         /// </summary>
         /// <param name="id">Profile ID</param>
         /// <returns>HTTP response code</returns>
         [HttpDelete("delete")]
         public async Task<IActionResult> DeleteProfile([FromHeader(Name = "id")] string id)
         {
             var profile = await _context.Profiles.FindAsync(id);
             if (profile == null)
             {
                 return NotFound();
             }
             
             _context.Profiles.Remove(profile);
             await _context.SaveChangesAsync();
             
             return NoContent();
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
             var currentGoal = _mapper.Map<GoalByUserDTO>(await _context.Goals
                 .Include(g => g.Workouts).Where(g => g.ProfileId == userId)
                 .Where(g => g.Achieved == false).FirstOrDefaultAsync());
             
             if (currentGoal == null)
                 return NotFound();
             
             return Ok(currentGoal);
         }
         private bool ProfileExists(string id)
         {
             return _context.Profiles.Any(p => p.Id == id);
         }
         
         private bool ProgramExists(int? id)
         {
             return _context.Programs.Any(p => p.Id == id);
         }
         
         
         private bool WorkoutExists(int? id)
         {
             return _context.Workouts.Any(w => w.Id == id);
         }
         
         private bool SetExists(int? id)
         {
             return _context.Sets.Any(s => s.Id == id);
         }
    }
}