using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using TrireksaAppContext.Models;
using TrireksaAppContext;

namespace WebApi.Middlewares
{
    public interface IUserService
    {
        Task<AuthenticateResponse> Authenticate(UserLogin model);
        Task<Users> GetById(string id);
        Task<string> AuthenticateUSerProvider(Users user);
        Task<string> GenerateToken(Users user);
        Task<Users> Register(RegisterModel user);
        Task<bool> AddToRole(Users administrator, string v);
    }

    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;

        // users hardcoded for simplicity, store in a db with hashed passwords in production applications

        private readonly AppSettings _appSettings;

        public UserService(IOptions<AppSettings> appSettings,  ApplicationDbContext dbcontext)
        {
            _context = dbcontext;
            _appSettings = appSettings.Value;
        }

        public async Task<AuthenticateResponse> Authenticate(UserLogin model)
        {
            try
            {
                var user = _context.Users.Where(x => (x.UserName == model.UserName || x.Email == model.UserName) && x.PasswordHash 
                    == GeneratePasswordHash(model.Password))
                     .Include(x => x.Userrole).ThenInclude(x => x.Role).AsNoTracking().SingleOrDefault();
                if (user != null)
                {
                    var token = await GenerateJwtToken(user);

                    return new AuthenticateResponse(user, token);
                }
                throw new SystemException($"Your Account {model.UserName} Tidak Ditemukan !");
            }
            catch (System.Exception ex)
            {
                throw new SystemException(ex.Message);
            }
        }


        public async Task<Users> GetById(string id)
        {
            var user = await _context.Users.Where(x => x.Id == id)
                     .Include(x => x.Userrole).ThenInclude(x => x.Role).SingleOrDefaultAsync();

            return user;
        }

        public async Task<string> AuthenticateUSerProvider(Users user)
        {
            try
            {
                var token = await GenerateJwtToken(user);
                if (string.IsNullOrEmpty(token))
                    throw new SystemException("You Not Have Access");
                return token;

            }
            catch (System.Exception ex)
            {
                throw new SystemException(ex.Message);
            }
        }

        private Task<string> GenerateJwtToken(Users user)
        {
            // generate token that is valid for 7 days

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("id", user.Id.ToString()),
                    new Claim("name", user.UserName),
                    new Claim("fullname", user.FullName??user.UserName),
                    new Claim("roles", user.Userrole.Select(x=>x.Role.Name).ToArray().ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return Task.FromResult(tokenHandler.WriteToken(token));
        }

        public async Task<string> GenerateToken(Users user)
        {
            return await GenerateJwtToken(user);
        }

        public async Task<Users> Register(RegisterModel model)
        {
            try
            {
                Users user = new Users{ Id=GeneratePasswordHash(DateTime.Now.ToString()), Email = model.Email, UserName = model.UserName, FullName=model.FullName, PasswordHash = GeneratePasswordHash(model.Password) };
                _context.Users.Add(user);

                var savedResult = await _context.SaveChangesAsync();
                if (savedResult <= 0)
                    throw new SystemException("Register Users Gagal !");
                return user;
            }
            catch (Exception ex)
            {

                throw new MySqlServiceException(ex);

            }
        }

        private static string GeneratePasswordHash(string password)
        {

            MD5 md5 = new MD5CryptoServiceProvider();

            //compute hash from the bytes of text  
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(password));

            //get hash result after compute it  
            byte[] result = md5.Hash;

            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                //change it into 2 hexadecimal digits  
                //for each byte  
                strBuilder.Append(result[i].ToString("x2"));
            }

            return strBuilder.ToString();
        }

        public Task<bool> AddToRole(Users user, string roleName)
        {
            try
            {
                var role = _context.Role.SingleOrDefault(x => x.Name.ToLower() == roleName.ToLower());
                if(role!=null)
                {
                    _context.Userrole.Add(new Userrole { RoleId = role.Id, UserId = user.Id });
                    return Task.FromResult(true);
                }
                throw new SystemException();
            }
            catch 
            {
                return Task.FromResult(false);
            }
        }
    }
}