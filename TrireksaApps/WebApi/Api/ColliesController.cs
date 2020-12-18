
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net;
//using System.Net.Http;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using TrireksaAppContext;

//namespace WebApi.Api
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    [ApiAuthorize]
//    public class ColliesController : ControllerBase
//    {

//        private ColliesContext context;
//        public ColliesController(ColliesContext _context)
//        {
//            context = _context;
//        }

//        [HttpGet]
//        public async Task<IActionResult> Get(int id)
//        {
//            try
//            {
//                return Ok(await context.Get(id));
//            }
//            catch (Exception ex)
//            {
//                return BadRequest(ex.Message);
//            }
//        }


//        [HttpPost]
//        public async Task<IActionResult> Post(Colly value)
//        {
//            try
//            {
//                return Ok(await context.Post(value));
//            }
//            catch (Exception ex)
//            {
//                return BadRequest(ex.Message);
//            }
//        }

//        PUT: api/Collies/5

//        [HttpPut]
//        public async Task<IActionResult> Put(int id, Colly value)
//        {
//            try
//            {
//                return Ok(await context.Put(value));
//            }
//            catch (Exception ex)
//            {
//                return BadRequest(ex.Message);
//            }
//        }

//        DELETE: api/Collies/5

//        [HttpDelete]
//        public async Task<IActionResult> Delete(int id)
//        {
//            try
//            {
//                return Ok(await context.Delete(id));
//            }
//            catch (Exception ex)
//            {
//                return BadRequest(ex.Message);
//            }
//        }
//    }
//}
