
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TrireksaAppContext;
using TrireksaAppContext.Models;

namespace WebApi.Api
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiAuthorize]
    public class ManifestOutgoingController : ControllerBase
    {
        private OutgoingContext context;
        public ManifestOutgoingController(OutgoingContext _context)
        {
            context = _context;
        }

        [HttpDelete("{id}")]
        [ApiAuthorize(Roles = "Manager")]
        public bool Delete(int id)
        {
            throw new NotImplementedException();
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

        [HttpGet("GetByMount")]
        public IActionResult GetByMount(int month)
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
        public async Task<IActionResult> Get(int Id)
        {
            try
            {
                return Ok(await context.Get(Id));
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorMessage(ex.Message));
            }
        }

        [HttpPost]
        [ApiAuthorize(Roles = "Operational")]
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
        [ApiAuthorize(Roles = "Operational")]
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
        public IActionResult ManifestsByPenjualanId(int Id)
        {
            try
            {
                var result = context.ManifestsByPenjualanId(Id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorMessage(ex.Message));
            }
        }

        [HttpPut("UpdateOrigin")]
        [ApiAuthorize(Roles = "Operational")]
        public async Task<IActionResult> UpdateOrigin(Manifestoutgoing model)
        {
            try
            {
                return Ok(await context.UpdateOrigin(model));

            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorMessage(ex.Message));
            }
        }

        [HttpPut("UpdateDestination")]
        [ApiAuthorize(Roles = "Operational, Agent")]
        public async Task<IActionResult> UpdateDestination(Manifestoutgoing model)
        {
            try
            {
                return Ok(await context.UpdateDestination(model));
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorMessage(ex.Message));
            }
        }

        [HttpGet("GetTitipanKapal")]
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


        [HttpGet("GetPackingList")]
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
