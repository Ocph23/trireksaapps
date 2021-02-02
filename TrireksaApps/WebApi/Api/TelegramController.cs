using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class TelegramController : ControllerBase
    {
        private TelegramService _telegram;

        public TelegramController(TelegramService telegaram)
        {
            _telegram = telegaram;
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<TelegramController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {

            try
            {
                
                return Ok(await _telegram.GetUserProfilePhotosAsync(id));
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
          
        }


        [HttpGet("auth/{id}")]
        public async Task<IActionResult> GetAuth(string id)
        {
            try
            {
                return Ok(await _telegram.GetMeAsync());
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        // POST api/<TelegramController>
        [HttpPost("send")]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<TelegramController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TelegramController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
