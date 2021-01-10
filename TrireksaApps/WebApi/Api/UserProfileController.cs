using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using TrireksaAppContext;
using WebApi.Middlewares;
using TrireksaAppContext.Models;

namespace WebApi.Api
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiAuthorize]
    public class UserProfileController : ControllerBase
    {
        private readonly UserProfileContext context;
        private readonly IUserService _userService;

        public UserProfileController(IUserService userService, UserProfileContext _context)
        {
            _userService = userService;
            context = _context;
        }

        [ApiAuthorize(Roles = "Administrator")]
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




        [ApiAuthorize]
        [HttpGet("GetProfile")]
        public async Task<IActionResult> GetProfile()
        {
            var identity = User.Identity.Name;
            var result = await context.GetProfile(identity);

            try
            {
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorMessage(ex.Message));
            }

        }


        [ApiAuthorize(Roles = "Administrator")]
        [HttpPost("AddNewRole/{userid}")]
        public async Task<IActionResult> AddNewRole(string userid, Role role)
        {
            try
            {
                return Ok(await context.AddNewRole(userid, role.Id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [ApiAuthorize(Roles = "Administrator")]
        [HttpDelete("RemoveRole/{userid}/roleid")]
        public async Task<IActionResult> RemoveRole(string userid, string roleid)
        {
            try
            {
                return Ok(await context.RemoveRole(userid, roleid));
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorMessage(ex.Message));
            }

        }

        [HttpPost("RegisterCustomer")]
        [ApiAuthorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> RegisterCustomer(Customer cust)
        {
            try
            {
               // var user = new User { UserName = cust.Email, Email = cust.Email };
                var result = await _userService.Register(new RegisterModel { Password = string.Concat(cust.Email, "#3Rp") });
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorMessage(ex.Message));
            }
        }

        [HttpPost("RegisterAgent")]
        [ApiAuthorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> RegisterAgent(Agent cust)
        {
            try
            {
                var email = cust.Email;
                if (ModelState.IsValid)
                {
                    var registerModel = new RegisterModel { UserName = email, Email = email, Password = string.Concat(email, "#3Rp") };
                    var user = await _userService.Register(registerModel);
                    if (user != null)
                    {
                        // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                        // Send an email with this link
                        //code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                        ////  var callbackUrl = Request.GetUrlHelper().Link  Request..Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                        //await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + "\">here</a>");
                        await _userService.AddToRole(user, "Agent");
                        return Ok(user);
                    }

                }
                throw new SystemException("User Not Created !");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string Id)
        {
            try
            {
                return Ok(await context.GetProfile(Id));
            }
            catch (Exception ex)
            {

                return BadRequest(new ErrorMessage(ex.Message));
            }
        }

        [HttpPost]
        [ApiAuthorize(Roles = "Administrator")]
        public async Task<IActionResult> Post([FromBody] Userprofile t)
        {
            try
            {
                return Ok(await context.Post(t));
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorMessage(ex.Message));
            }

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Userprofile value)
        {
            try
            {
                return Ok(await context.Put(id, value));
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorMessage(ex.Message));
            }

        }

    }
}
