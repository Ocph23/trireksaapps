using TrireksaAppContext;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebApi.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class TracingController : ControllerBase
    {
        private readonly TracingContext context;

        public TracingController(TracingContext _context)
        {
            context = _context;

        }

        [HttpGet("GetPenjualan/{stt}")]
        public async Task<IActionResult> GetPenjualan(int stt)
        {
            try
            {
                return Ok(await context.GetPenjualan(stt));
            }
            catch (Exception ex)
            {

                return BadRequest(new ErrorMessage(ex.Message));
            }
        }
    }
}
