using System;
using Microsoft.AspNetCore.Mvc;
using TrireksaAppContext;
using System.Threading.Tasks;
using TrireksaAppContext.Models;

namespace WebApi.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppVersionController : ControllerBase
    {
        private readonly AppVersionContext context;
        public AppVersionController(AppVersionContext _context)
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

        // GET: api/AppVersions/5

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

        [HttpGet("last")]
        public async Task<IActionResult> GetLast()
        {
            try
            {
                return Ok(await context.GetLast());

            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorMessage(ex.Message));
            }
        }


        // POST: api/AppVersions
        [HttpPost]
        public async Task<IActionResult> Post(AppVersion value)
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
        // PUT: api/AppVersions/5
        public async Task<IActionResult> Put(int id, [FromBody] AppVersion value)
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

     




    }
}
