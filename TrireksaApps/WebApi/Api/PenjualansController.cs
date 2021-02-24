using System;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TrireksaAppContext;
using TrireksaAppContext.Models;


namespace WebApi.Api
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiAuthorize]
    public class PenjualansController : ControllerBase
    {
        // GET: api/Penjualans
        readonly PenjualanContext  context;
        public PenjualansController(PenjualanContext _context)
        {
            context = _context;
        }

        [HttpGet("{startDate}/{endDate}")]
        public async Task<IActionResult> Get(DateTime startDate, DateTime endDate)
        {
            try
            {
                return Ok(await context.Get(startDate, endDate));
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorMessage(ex.Message));
            }
        }



        // POST: api/Penjualans
        [HttpGet("NewSTT")]
        public async Task<IActionResult> NewSTT()
        {
            try
            {
                return Ok(await context.NewSTT());
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorMessage(ex.Message));
            }
        }


        [ApiAuthorize(Roles = "Administrator, Admin")]
        [HttpPost]
        public async Task<IActionResult> Post(Penjualan value)
        {
            try
            {
                return Ok(await context.InsertAndGetItem(value));
            }
            catch (Exception ex)
            {

                return BadRequest(new ErrorMessage(ex.Message));
            }
        }


        [HttpPut("{id}")]
        [ApiAuthorize(Roles = "Administrator, Manager")]
        public async Task<IActionResult> Put(int id, Penjualan value)
        {
            try
            {
                return Ok(await context.Update(id, value));
            }
            catch (Exception ex)
            {

                return BadRequest(new ErrorMessage(ex.Message));
            }
        }



        [HttpGet("GetByParameter/{agentId}/{type}")]
        public async Task<IActionResult> GetByParameter(int agentId, PortType type)
        {
            try
            {
                var result = await context.GetByParameter(agentId, type);
                return Ok(result);
            }
            catch (Exception ex)
            {

                return BadRequest(new ErrorMessage(ex.Message));
            }
        }


        [HttpGet("GetBySTT/{id}")]
        public async Task<IActionResult> GetBySTT(int id)
        {
            try
            {
                return Ok(await context.GetBySTT(id));
            }
            catch (Exception ex)
            {

                return BadRequest(new ErrorMessage(ex.Message));
            }
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int Id)
        {
            try
            {
                return Ok(await context.GetById(Id));
            }
            catch (Exception ex)
            {

                return BadRequest(new ErrorMessage(ex.Message));
            }
        }


        [ApiAuthorize(Roles = "Administrator, Manager")]
        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }


        [HttpGet("GetPenjualanNotPaid/{Id}")]
        public async Task<IActionResult> GetPenjualanNotPaid(int Id)
        {
            try
            {
                var data = await context.GetPenjualanNotPaid(Id);
                return Ok(data);
            }
            catch (Exception ex)
            {

                return BadRequest(new ErrorMessage(ex.Message));
            }
        }



        [ApiAuthorize(Roles = "Administrator, Agent, Operational")]
        [HttpPut("UpdateDeliveryStatus/{Id}")]
        public async Task<IActionResult> UpdateDeliveryStatus(int id, Deliverystatus obj)
        {

            try
            {
                return Ok(await context.UpdateDeliveryStatus(id, obj));
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorMessage(ex.Message));
            }
        }

        [ApiAuthorize]
        [HttpPut("UpdateDeliveryStatusBySTT/{id}")]
        public async Task<IActionResult> UpdateDeliveryStatusBySTT(int id, Deliverystatus obj)
        {

            try
            {
                return Ok(await context.UpdateDeliveryStatusBySTT(id, obj));
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorMessage(ex.Message));
            }
        }



        [HttpGet("IsSended/{id}")]
        public async Task<IActionResult> IsSended(int Id)
        {
            try
            {
                return Ok(await context.IsSended(Id));
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorMessage(ex.Message));
            }
        }

        [ApiAuthorize(Roles = "Administrator, Admin, Manager")]
        [HttpGet("GetPenjualanFromTo/{start}/{ended}")]
        public async Task<IActionResult> GetPenjualanFromTo(DateTime start, DateTime ended)
        {
            try
            {
                return Ok(await context.GetPenjualanFromTo(start, ended));
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorMessage(ex.Message));
            }
        }


    }
}
