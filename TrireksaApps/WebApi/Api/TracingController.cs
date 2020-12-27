using TrireksaAppContext;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult GetPenjualan(int stt)
        {
            try
            {
                return Ok(context.GetPenjualan(stt));
            }
            catch (Exception ex)
            {

                return BadRequest(new ErrorMessage(ex.Message));
            }
        }
    }
}
