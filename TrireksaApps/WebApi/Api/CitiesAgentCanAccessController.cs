using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TrireksaAppContext.Models;
using TrireksaAppContext;

namespace WebApi.Api
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiAuthorize(Roles = "Administrator, Admin")]
    public class CitiesAgentCanAccessController : ControllerBase
    {
        private readonly CitiesAgentCanAccessContext context;
        public CitiesAgentCanAccessController(CitiesAgentCanAccessContext _context)
        {
            context = _context;
        }

        [HttpGet]
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

        // POST: api/CitiesAgentCanAccess

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Cityagentcanaccess value)
        {
            try
            {
                return Ok(await context.Post(value));
            }
            catch (Exception ex)
            {

                return BadRequest(new ErrorMessage(ex.Message));
            }
        }



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



        [HttpPost("OnChangeItemTrue")]
        public async Task<IActionResult> OnChangeItemTrue(Cityagentcanaccess obj)
        {
            try
            {
                return Ok(await context.OnChangeItemTrue(obj));
            }
            catch (Exception ex)
            {

                return BadRequest(new ErrorMessage(ex.Message));
            }

        }


        [HttpPost("OnChangeItemFalse")]
        public async Task<IActionResult> OnChangeItemFalse(Cityagentcanaccess obj)
        {

            try
            {
                return Ok(await context.OnChangeItemFalse(obj));
            }
            catch (Exception ex)
            {

                return BadRequest(new ErrorMessage(ex.Message));
            }

        }

    }
}
