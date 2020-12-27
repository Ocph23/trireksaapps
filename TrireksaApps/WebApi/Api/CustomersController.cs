using TrireksaAppContext;

using System;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using TrireksaAppContext.Models;
using System.Threading.Tasks;

namespace WebApi.Api
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiAuthorize]
    public class CustomersController : ControllerBase
    {
        private readonly CustomersContext context;

        // GET: api/customers

        public CustomersController(CustomersContext _context)
        {
            context = _context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(await context.Get());
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorMessage(ex.Message));
            }
        }

        // GET: api/customers/5
        [HttpGet("{id}")]
        [ApiAuthorize]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                return Ok(await context.Get(id));
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorMessage(ex.Message));
            }
        }

        // POST: api/customers

        [HttpPost]
        [ApiAuthorize(Roles = "Administrator, Admin")]
        public async Task<IActionResult> Post([FromBody] Customer value)
        {
            try
            {
                var result = await context.Post(value);
                string username = User.Identity.Name;
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorMessage(ex.Message));
            }
        }

        // PUT: api/customers/5

        [HttpPut("{id}")]
        [ApiAuthorize(Roles = "Administrator, Admin")]
        public async Task<IActionResult> Put(int id, [FromBody] Customer value)
        {
            try
            {
                return Ok(await context.Put(id, value));
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorMessage(ex.Message));
            }
        }

        // DELETE: api/customers/5

        [HttpDelete("{id}")]
        [ApiAuthorize(Roles = "Administrator, Manager")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                return Ok(await context.Delete(id));
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorMessage(ex.Message));
            }
        }
    }
}
