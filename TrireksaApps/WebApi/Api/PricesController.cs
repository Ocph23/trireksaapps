using TrireksaAppContext;

using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TrireksaAppContext.Models;

namespace WebApi.Api
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiAuthorize]
    public class PricesController : ControllerBase
    {
        private PricesContext context;
        public PricesController(PricesContext _context)
        {
            context = _context;

        }

        [HttpPost("GetPricesByCustomer")]
        public async Task<IActionResult> GetPricesByCustomer(Price prices)
        {
            try
            {
                return Ok(await context.GetPricesByCustomer(prices));
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorMessage(ex.Message));
            }
        }

        [ApiAuthorize(Roles = "Admin, Manager")]
        [HttpPost("SetPrices")]
        public async Task<IActionResult> SetPrices(Price model)
        {
            try
            {
                return Ok(await context.SetPrices(model));
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorMessage(ex.Message));
            }
        }
    }
}
