//using TrireksaAppContext;
//using System;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;

//namespace WebApi.Api
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    [ApiAuthorize]
//    public class DashboardController : ControllerBase
//    {
//        DashboardContext context;
//        public DashboardController(DashboardContext _context)
//        {
//            context = _context;
//        }

//        [HttpGet("GetInvoiceNotYetPaid")]
//        public async Task<IActionResult> GetInvoiceNotYetPaid()
//        {
//            try
//            {
//                return Ok(await context.GetInvoiceNotYetPaid());
//            }
//            catch (Exception ex)
//            {
//                return BadRequest(new ErrorMessage(ex.Message));
//            }
//        }

//        [HttpGet("GetPenjualanBulan")]
//        public async Task<IActionResult> GetPenjualanBulan(int month, int year)
//        {
//            try
//            {
//                return Ok(await context.GetPenjualanBulan(month, year));
//            }
//            catch (Exception ex)
//            {
//                return BadRequest(new ErrorMessage(ex.Message));
//            }
//        }

//        [HttpGet("GetPenjualanNotHaveStatus")]
//        public async Task<IActionResult> GetPenjualanNotHaveStatus()
//        {
//            try
//            {
//                return Ok(await context.GetPenjualanNotHaveStatus());
//            }
//            catch (Exception ex)
//            {
//                return BadRequest(new ErrorMessage(ex.Message));
//            }

//        }

//        [HttpGet("GetPenjualanNotYetSend")]
//        public async Task<IActionResult> GetPenjualanNotYetSend()
//        {
//            try
//            {
//                return Ok(await context.GetPenjualanNotYetSend());
//            }
//            catch (Exception ex)
//            {
//                return BadRequest(new ErrorMessage(ex.Message));
//            }


//        }

//        [HttpGet("GetPenjualanNotPaid")]
//        public async Task<IActionResult> GetPenjualanNotPaid()
//        {
//            try
//            {
//                return Ok(await context.GetPenjualanNotPaid());
//            }
//            catch (Exception ex)
//            {
//                return BadRequest(new ErrorMessage(ex.Message));
//            }
//        }
//        [HttpGet("GetInvoiceNotYetDelivery")]
//        public async Task<IActionResult> GetInvoiceNotYetDelivery()
//        {
//            try
//            {
//                return Ok(await context.GetInvoiceNotYetDelivery());
//            }
//            catch (Exception ex)
//            {
//                return BadRequest(new ErrorMessage(ex.Message));
//            }

//        }
//        [HttpGet("GetInvoiceJatuhTempo")]
//        public async Task<IActionResult> GetInvoiceJatuhTempo()
//        {
//            try
//            {
//                return Ok(await context.GetInvoiceJatuhTempo());
//            }
//            catch (Exception ex)
//            {
//                return BadRequest(new ErrorMessage(ex.Message));
//            }

//        }
//        [HttpGet("GetInvoiceNotYetRecive")]
//        public async Task<IActionResult> GetInvoiceNotYetRecive()
//        {
//            try
//            {
//                return Ok(await context.GetInvoiceNotYetRecive());
//            }
//            catch (Exception ex)
//            {
//                return BadRequest(new ErrorMessage(ex.Message));
//            }

//        }

//        [HttpGet("GetPenjualanThreeYear")]
//        public async Task<IActionResult> GetPenjualanThreeYear()
//        {
//            try
//            {
//                return Ok(await context.GetPenjualanThreeYear());
//            }
//            catch (Exception ex)
//            {
//                return BadRequest(new ErrorMessage(ex.Message));
//            }
//        }
//        [HttpGet("GetPenjualanOfMonth")]
//        private async Task<IActionResult> GetPenjualanOfMonth(int tahun)
//        {
//            try
//            {
//                return Ok(await context.GetInvoiceNotYetPaid());
//            }
//            catch (Exception ex)
//            {
//                return BadRequest(new ErrorMessage(ex.Message));
//            }
//        }


//    }
//}
