﻿using System;
using Microsoft.AspNetCore.Mvc;
using TrireksaAppContext;
using TrireksaAppContext.Models;


namespace WebApi.Api
{
    [Route("api/[controller]")]
    [ApiController]
 //   [ApiAuthorize]
    public class CityController : ControllerBase
    {
        private CityContext context;
        public CityController(CityContext _context)
        {
            context = _context;
        }


        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(context.Get());
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorMessage(ex.Message));
            }

        }

        // GET: api/City/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                return Ok(context.Get(id));
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorMessage(ex.Message));
            }
        }

        // POST: api/City

        [HttpPost]
        [ApiAuthorize(Roles = "Admin")]
        public IActionResult Post([FromBody]City value)
        {
            try
            {
                var result = context.Post(value);
                string username = User.Identity.Name;
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorMessage(ex.Message));
            }

        }

        // PUT: api/City/5
        [HttpPut("{id}")]
        [ApiAuthorize(Roles = "Admin")]
        public IActionResult Put(int id, [FromBody]City value)
        {
            try
            {
                return Ok(context.Put(id,value));
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorMessage(ex.Message));
            }
        }

        // DELETE: api/City/5
        [HttpDelete("{id}")]
        [ApiAuthorize(Roles = "Manager, Admin")]
        public IActionResult Delete(int id)
        {
            try
            {
                return Ok(context.Delete(id));
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorMessage(ex.Message));
            }
        }
    }
}
