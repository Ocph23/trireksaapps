
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TrireksaAppContext;
using TrireksaAppContext.Models;
using System.Linq;

namespace WebApi.Api
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiAuthorize]
    public class ManifestOutgoingController : ControllerBase
    {
        private readonly OutgoingContext context;
        public ManifestOutgoingController(OutgoingContext _context)
        {
            context = _context;
        }

        [HttpDelete("{id}")]
        [ApiAuthorize(Roles = "Administrator, Manager")]
        public bool Delete(int id)
        {
            throw new NotImplementedException();
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

        [HttpGet("{startDate}/{endDate}")]
        public async Task<ActionResult> GetByDate(DateTime startDate, DateTime endDate)
        {
            try
            {
                var data = await context.GetByDate(startDate, endDate);
                return Ok(data.ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorMessage(ex.Message));
            }
        }

        [HttpGet("GetByMount")]
        public async Task<IActionResult> GetByMount(int month)
        {
            try
            {
                return Ok(context.GetByMount(month));
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorMessage(ex.Message));
            }

        }

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

        [HttpPost]
        [ApiAuthorize(Roles = "Administrator, Operational")]
        public async Task<IActionResult> Post(Manifestoutgoing t)
        {
            try
            {
                return Ok(await context.InsertAndGetItem(t));
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorMessage(ex.Message));
            }
        }

        [HttpPost("UpdateInformation")]
        [ApiAuthorize(Roles = "Administrator, Operational")]
        public IActionResult UpdateInformation(Manifestinformation obj)
        {
            try
            {
                var result = context.InsertInformation(obj);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorMessage(ex.Message));
            }
        }

        [HttpGet("ManifestsByPenjualanId/{id}")]
        public async Task<IActionResult> ManifestsByPenjualanId(int Id)
        {
            try
            {
                var result = await context.ManifestsByPenjualanId(Id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorMessage(ex.Message));
            }
        }

        [HttpPut("UpdateOrigin/{id}")]
        [ApiAuthorize(Roles = "Administrator, Operational")]
        public async Task<IActionResult> UpdateOrigin(int id, Manifestoutgoing model)
        {
            try
            {
                return Ok(await context.UpdateOrigin(id, model));

            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorMessage(ex.Message));
            }
        }

        [HttpPut("UpdateDestination/{id}")]
        [ApiAuthorize(Roles = "Administrator, Operational, Agent")]
        public async Task<IActionResult> UpdateDestination(int id, Manifestoutgoing model)
        {
            try
            {
                return Ok(await context.UpdateDestination(id, model));
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorMessage(ex.Message));
            }
        }

        [HttpGet("GetTitipanKapal/{Id}")]
        public async Task<IActionResult> GetTitipanKapal(int Id)
        {
            try
            {
                var result = await context.GetTitipanKapal(Id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorMessage(ex.Message));
            }
        }


        [HttpGet("GetPackingList/{Id}")]
        public async Task<IActionResult> GetPackingList(int Id)
        {
            try
            {
                var result = await context.GetPackingList(Id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorMessage(ex.Message));
            }
        }




    }
}



//eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6ImRkYmZlMzA4MDI3YTAwMDYzOWU4MTczZDhhM2NiNWM4IiwibmFtZSI6IkFkbWluaXN0cmF0b3IiLCJmdWxsbmFtZSI6IkFkbWluaXN0cmF0b3IiLCJyb2xlcyI6IlN5c3RlbS5TdHJpbmdbXSIsIm5iZiI6MTYxMjIwMTc2MSwiZXhwIjoxNjEyODA2NTYxLCJpYXQiOjE2MTIyMDE3NjF9.6SAY7iNKS94L3StcuDxOgbfZzRjNNt95xCd0A6KbDT4
