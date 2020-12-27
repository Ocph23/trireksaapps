using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using WebApi.Middlewares;
using WebApi.Models;

namespace WebApi.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserLogin user)
        {
            try
            {
                var response = await _userService.Authenticate(user);
                if (response == null)
                    return BadRequest(new { message = "Username or password is incorrect" });
                return Ok(response);
            }
            catch (System.Exception ex)
            {
                return BadRequest(new ErrorMessage(ex.Message));
            }
        }


        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterModel user)
        {
            try
            {
                var response = await _userService.Register(user);
                if (response == null)
                    return BadRequest(new { message = "Register User Gagal !" });
                return Ok(response);
            }
            catch (System.Exception ex)
            {
                return BadRequest(new ErrorMessage(ex.Message));
            }
        }



        /*[ApiAuthorize]
        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePassword model)
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                if (user.Id != model.UserId)
                {
                    throw new SystemException("Anda Tidak Memiliki Access Untuk Mengubah Password");
                }

                var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);

                if (result.Succeeded)
                {
                    return Ok(true);
                }

                if (result.Errors != null && result.Errors.Count() > 0)
                    return BadRequest(result.Errors.FirstOrDefault());
                else
                    throw new SystemException("Ubah Password Tidak Berhasil !, \r\n Note : Password Harus Mengandung Angka, Huruf Besar, Huruf Kecil dan Karakter Khusus");
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
*/

        /*[ApiAuthorize]
        [HttpGet("JointExternalUser")]
        public async Task<IActionResult> JointExternalUser(string key, string provider, string email)
        {
            *//*try
            {
                var user = await _userManager.GetUserAsync(User);
                UserLoginInfo userLogin = new UserLoginInfo(provider, key, provider);
                var result = await _userManager.AddLoginAsync(user, userLogin);
                if (result.Succeeded)
                {
                    user.Email = email;
                    await _userManager.UpdateAsync(user);
                    return Ok(true);
                }
                else
                {

                    if (result.Errors != null && result.Errors.Count() > 0)
                        return BadRequest(result.Errors.FirstOrDefault());
                    else
                        throw new SystemException("Account Tidak Berhasil Dihubungkan !");
                }
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }*//*
        }


        [ApiAuthorize]
        [HttpGet("notifications")]
        public async Task<IActionResult> GetNotifications()
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                if (user != null)
                {
                    IEnumerable datas = _context.Notifications.Where(x => x.NotificationType == NotificationType.Public || x.UserId == user.Id).OrderByDescending(x => x.Created).ToList();
                    return Ok(datas);
                }
                throw new SystemException("Data Tidak Ada !");
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }*/
    }
}