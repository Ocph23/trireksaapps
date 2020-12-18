using System;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using TrireksaAppContext;
using System.Threading.Tasks;
using TrireksaAppContext.Models;

namespace WebApi.Api
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiAuthorize]
    public class AgentsController : ControllerBase
    {
        private readonly AgentsContext context;
        public AgentsController(AgentsContext _context)
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

        // GET: api/Agents/5

        [HttpGet("{id}")]
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

        // POST: api/Agents
        [HttpPost]
        [ApiAuthorize(Roles = "Administrator, Admin")]
        public async Task<IActionResult> Post(Agent value)
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

        [HttpPut("{id}")]
        // PUT: api/Agents/5
        [ApiAuthorize(Roles = "Administrator, Admin")]
        public async Task<IActionResult> Put(int id, [FromBody] Agent value)
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

        // DELETE: api/Agents/5
        [ApiAuthorize(Roles = "Administrator, Manager")]
        [HttpDelete]
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
