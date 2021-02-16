using TrireksaAppContext;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TrireksaAppContext.Models;

namespace WebApi.Api
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiAuthorize]
    public class InvoicesController : ControllerBase
    {
        private readonly InvoicesContext context;

        public InvoicesController(InvoicesContext _context)
        {
            context = _context;

        }

        // GET: api/Invoices
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


        [HttpGet("{start}/{end}")]
        public async Task<IActionResult> Get(DateTime start, DateTime end)
        {
            try
            {
                return Ok(await context.Get(start, end));
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorMessage(ex.Message));
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult>Get(int Id)
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
        [ApiAuthorize(Roles = "Administrator, Accounting")]
        public async Task<IActionResult> Post(Invoices t)
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


        [HttpPut("{id}")]
        [ApiAuthorize(Roles = "Administrator, Accounting")]
        public async Task<IActionResult> Put(int id, Invoices model)
        {
            try
            {
                return Ok(await context.Put(id, model));
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorMessage(ex.Message));
            }
        }

        [HttpPut("UpdateDeliveryDataAction/{Id}")]
        [ApiAuthorize(Roles = "Administrator, Accounting")]
        public async Task<IActionResult> UpdateDeliveryDataAction(int Id, Invoices t)
        {
            try
            {
                return Ok(await context.UpdateDeliveryDataAction(Id, t));
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorMessage(ex.Message));
            }
        }

        [HttpPut("UpdateInvoiceStatusAction/{Id}")]
        [ApiAuthorize(Roles = "Administrator, Accounting")]
        public async Task<IActionResult> UpdateInvoiceStatusAction(int Id, Invoices t)
        {
            try
            {
                return Ok(await context.UpdateInvoiceStatusAction(Id, t));
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorMessage(ex.Message));
            }
        }


        [HttpGet("GetInvoiceForPenjualanInfo/{Id}")]
        public async Task<IActionResult> GetInvoiceForPenjualanInfo(int Id)
        {
            try
            {
                return Ok(await context.GetInvoiceForPenjualanInfo(Id));
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorMessage(ex.Message));
            }
        }


        [HttpGet("GetInvoiceReport/{id}")]
        public async Task<IActionResult> GetInvoiceReport(int id)
        {
            try
            {
                return Ok(await context.GetInvoiceReport(id));
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorMessage(ex.Message));
            }
        }

    }
}
