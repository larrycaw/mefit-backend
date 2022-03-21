using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MeFit.Models.Data;
using MeFit.Models.Domain;
using MeFit.Models.DTOs.Address;
using System.Net.Mime;
using Microsoft.AspNetCore.Authorization;

namespace MeFit.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class AddressesController : ControllerBase
    {
        private readonly MeFitDbContext _context;
        private readonly IMapper _mapper;

        public AddressesController(MeFitDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // TODO: remove testing endpoints
        [HttpGet("authorizationAny")]
        [Authorize(Policy = "isUser")]
        public string TestAuthorizationAny()
        {
            return "user auth";
        }

        [HttpGet("authorizationContributor")]
        [Authorize(Policy = "isContributor")]
        public string TestAuthorizationContributor()
        {
            return "contibutor auth";
        }

        [HttpGet("authorizationAdmin")]
        [Authorize(Policy = "isAdministrator")]
        public string TestAuthorizationAdmin()
        {
            return "admin auth";
        }

        /// <summary>
        /// Gets all addresses, regardless of which user it belongs to
        /// 
        /// GET: api/Addresses/all
        /// </summary>
        /// <returns>List of addresses</returns>
        [HttpGet("all")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<AddressReadDTO>>> GetAddresses()
        {
            var addresses = _mapper.Map<List<AddressReadDTO>>(await _context.Addresses.ToListAsync());
            return Ok(addresses);
        }
        
        /// <summary>
        /// Gets address given address ID
        /// 
        /// GET: api/Address
        /// </summary>
        /// <param name="id">Address ID</param>
        /// <returns>Address</returns>
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<AddressReadDTO>> GetAddress([FromHeader(Name = "id")] int id)
        {
            var address = _mapper.Map<AddressReadDTO>(await _context.Addresses.FindAsync(id));

            if (address == null)
            {
                return NotFound();
            }

            return Ok(address);
        }

        /// <summary>
        /// Updates address
        /// 
        /// PUT: api/Address
        /// </summary>
        /// <param name="address">Address object</param>
        /// <param name="id">Address ID</param>
        /// <returns>HTTP response code</returns>
        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdateAddress([FromBody] AddressUpdateDTO address, [FromHeader(Name = "id")] int id)
        {
            if (address.Id != id)
                return BadRequest();

            Address domainAddress = _mapper.Map<Address>(address);
            _context.Entry(domainAddress).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AddressExists(address.Id))
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
        /// Posts address
        /// 
        /// POST: api/Address
        /// </summary>
        /// <param name="addressDto">Address to post</param>
        /// <returns>Newly created exercise</returns>
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<AddressReadDTO>> PostAddress([FromBody] AddressCreateDTO addressDto)
        {
            var address = _mapper.Map<Address>(addressDto);

            try
            {
                _context.Add(address);
                await _context.SaveChangesAsync();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            var addedAddress = _mapper.Map<AddressReadDTO>(address);
            return CreatedAtAction("GetAddress", new { address.Id }, addedAddress);
        }
        
        /// <summary>
        /// Deletes address
        /// 
        /// DELETE: api/Address/delete
        /// </summary>
        /// <param name="id">Address ID</param>
        /// <returns>HTTP response code</returns>
        [HttpDelete("delete")]
        [Authorize]
        public async Task<IActionResult> DeleteAddress([FromHeader(Name = "id")] int id)
        {
            var address = await _context.Addresses.FindAsync(id);
            if (address == null)
            {
                return NotFound();
            }

            _context.Addresses.Remove(address);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        
        private bool AddressExists(int id)
        {
            return _context.Addresses.Any(e => e.Id == id);
        }
    }
}
