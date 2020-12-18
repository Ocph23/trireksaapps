using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using TrireksaAppContext;


namespace WebApi.Api
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiAuthorize]
    public class RolesController : ControllerBase
    {
        RolesContext context;

        public RolesController(RolesContext _context)
        {
            context = _context;

        }
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var res = context.Get();
                return Ok(res);
            }
            catch (Exception ex)
            {

                return BadRequest(new ErrorMessage(ex.Message));
            }
        }



    }
}
