using TrireksaAppContext;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TrireksaAppContext.Models;

namespace WebApi.Api
{

    [Route("api/[controller]")]
    [ApiController]
    [ApiAuthorize]
    public class PortsController : ControllerBase
    {
        // GET: api/Ports

        private PortsContext context;

        // GET: api/customers

        public PortsController(PortsContext _context)
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

        // GET: api/Ports/5

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

        // POST: api/Ports
        [HttpPost]
        [ApiAuthorize(Roles = "Admin")]
        public async Task<IActionResult> Post(Port value)
        {
            try
            {
                var result = context.Post(value);
                string username = User.Identity.Name;
                return Ok(await result);
            }
            catch (Exception ex)
            {
                 return BadRequest(new ErrorMessage(ex.Message));
            }
        }

        // PUT: api/Ports/5
        [HttpPut]
        [ApiAuthorize(Roles = "Admin")]
        public async Task<IActionResult> Put(int id, Port value)
        {
            try
            {
                return Ok(await context.Put(id,value));
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorMessage(ex.Message));
            }
        }

        // DELETE: api/Ports/5
        [HttpDelete]
        [ApiAuthorize(Roles = "Manager")]
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
