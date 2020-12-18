
//using Newtonsoft.Json;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using System.Net;
//using System.Net.Http;
//using Microsoft.AspNetCore.Mvc;
//using WebApi.Models;
//using System.Web;
//using TrireksaAppContext;
//using WebApi.Middlewares;

//namespace WebApi.Api
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    [ApiAuthorize]
//    public class UserProfileController : ControllerBase
//    {
//        private UserProfileContext context;
//        private IUserService _userService;

//        public UserProfileController(IUserService userService, UserProfileContext _context)
//        {
//            _userService = userService;
//            context = _context;
//        }

//        [ApiAuthorize(Roles = "Administrator")]
//        [HttpGet]
//        public IActionResult Get()
//        {
//            try
//            {
//                return Ok(context.Get());
//            }
//            catch (Exception ex)
//            {

//                return BadRequest(new ErrorMessage(ex.Message));
//            }

//        }




//        [ApiAuthorize]
//        [HttpGet("GetProfile")]
//        public IActionResult GetProfile()
//        {
//            var identity = User.Identity.Name;
//            var result = context.GetProfile(identity);

//            try
//            {
//                return Ok(result);
//            }
//            catch (Exception ex)
//            {
//                return BadRequest(new ErrorMessage(ex.Message));
//            }

//        }


//        [ApiAuthorize(Roles = "Administrator")]
//        [HttpPost("AddNewRole")]
//        public IActionResult AddNewRole(string userId, Role role)
//        {
//            try
//            {
//                return Ok(context.AddNewRole(userId, role.Id));
//            }
//            catch (Exception ex)
//            {
//                return BadRequest(ex.Message);
//            }

//        }

//        [ApiAuthorize(Roles = "Administrator")]
//        [HttpDelete("RemoveRole")]
//        public IActionResult RemoveRole(string userId, int roleId)
//        {
//            try
//            {
//                return Ok(context.RemoveRole(userId, roleId));
//            }
//            catch (Exception ex)
//            {
//                return BadRequest(new ErrorMessage(ex.Message));
//            }

//        }

//        [HttpPost("RegisterCustomer")]
//        [ApiAuthorize(Roles = "Admin, Manager")]
//        public async Task<IActionResult> RegisterCustomer(Customer cust)
//        {
//            try
//            {
//                var user = new User { UserName = cust.Email, Email = cust.Email };
//                var result = await _userService.Register(new RegisterModel { Password = string.Concat(cust.Email, "#3Rp") });
//                return Ok(result);
//            }
//            catch (Exception ex)
//            {
//                return BadRequest(new ErrorMessage(ex.Message));
//            }
//        }

//        [HttpPost("RegisterAgent")]
//        [ApiAuthorize(Roles = "Admin, Manager")]
//        public async Task<IActionResult> RegisterAgent(Agent cust)
//        {
//            /*var email = cust.Email;
//            string code = string.Empty;
//            var UserManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
//            var SignInManager = HttpContext.Current.GetOwinContext().Get<ApplicationSignInManager>();
//            if (ModelState.IsValid)
//            {
//                var user = new ApplicationUser { UserName = email, Email = email };
//                var result = await UserManager.CreateAsync(user, string.Concat(email, "#3Rp"));
//                if (result.Succeeded)
//                {
//                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

//                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
//                    // Send an email with this link
//                    code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
//                    //  var callbackUrl = Request.GetUrlHelper().Link  Request..Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
//                    await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + "\">here</a>");
//                    await UserManager.AddToRoleAsync(user.Id, "Agent");
//                    return Ok(code);
//                }

//            }
//            return Content(HttpStatusCode.NotAcceptable, code);
//            // If we got this far, something failed, redisplay form*/


//            return Ok(await Task.FromResult(0));


//        }

//        [HttpGet("{id}")]
//        public IActionResult Get(int Id)
//        {
//            var identity = User.Identity.Name;
//            try
//            {
//                return Ok(context.Get(Id, identity));
//            }
//            catch (Exception ex)
//            {

//                return BadRequest(new ErrorMessage(ex.Message));
//            }
//        }

//        [HttpPost]
//        [ApiAuthorize(Roles = "Administrator")]
//        public IActionResult Post([FromBody] userprofile t)
//        {
//            var identity = User.Identity.Name;
//            try
//            {
//                return Ok(context.Post(t, identity));
//            }
//            catch (Exception ex)
//            {

//                return BadRequest(new ErrorMessage(ex.Message));
//            }

//        }

//        [HttpPut]
//        public IActionResult Put(int id, [FromBody] userprofile value)
//        {
//            var identity = User.Identity.Name;
//            try
//            {
//                return Ok(context.Put(id, value, identity));
//            }
//            catch (Exception ex)
//            {

//                return BadRequest(new ErrorMessage(ex.Message));
//            }

//        }

//    }
//}
