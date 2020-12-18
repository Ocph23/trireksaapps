using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;

using TrireksaAppContext;
using TrireksaAppContext.Models;

namespace WebApi.Api
{

    [Route("api/[controller]")]
    [ApiController]
    [ApiAuthorize]
    public class ShipsController : ControllerBase
    {
        private ShipsContext context;
        public ShipsController(ShipsContext _context)
        {
            context = _context;

        }
        // GET: api/Ships
        [ApiAuthorize(Roles = "Manager")]
        [HttpDelete]
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

        [HttpGet("{id}")]
        public IActionResult Get(int Id)
        {
            try
            {
                return Ok(context.GetById(Id));
            }
            catch (Exception ex)
            {

                return BadRequest(new ErrorMessage(ex.Message));
            }
        }



        [HttpPost]
        [ApiAuthorize(Roles = "Admin")]
        public IActionResult Post([FromBody] Ships t)
        {
            try
            {
                return Ok(context.InsertAndGetItem(t));
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorMessage(ex.Message));
            }
        }


        [HttpPut("{id}")]
        [ApiAuthorize(Roles = "Admin, Manager")]
        public IActionResult Put(int id, [FromBody] Ships value)
        {
            try
            {
                return Ok(context.UpdateAndGetItem(value));
            }
            catch (Exception ex)
            {

                return BadRequest(new ErrorMessage(ex.Message));
            }
        }


    }
}
