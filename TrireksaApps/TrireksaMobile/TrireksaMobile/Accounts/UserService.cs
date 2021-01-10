﻿using System;
using System.Collections.Generic;
using System.Text; 
using System.Threading.Tasks;
using TrireksaMobile;

namespace Accounts
{


   public interface IUserServices
    {
        Task<bool> Login(UserLogin model);
        Task Logout();
    }



    public class UserService : IUserServices
    {
        readonly string controller = "/api/user";

        public AuthenticateResponse User { get; set; }

        public Task Initialize()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Login(UserLogin model)
        {
            try
            {
                using var client = new RestService();
                var response = await client.PostAsync($"{controller}/login", client.GenerateHttpContent(model));
                if(response.IsSuccessStatusCode)
                {
                    var result =await response.GetResult<AuthenticateResponse>();
                    if (result != null)
                    {
                        await Account.SetUser(result);
                        client.SetToken(result.Token);
                        response = await client.GetAsync($"/api/userprofile/GetProfile");
                        if (response.IsSuccessStatusCode)
                        {
                            var profile = await response.GetResult<Profile>();
                            if (profile != null)
                            {
                                await Account.SetProfile(profile);
                            }
                        }
                        else
                        {
                            throw new SystemException(await client.Error(response));
                        }
                            return true;
                    }
                    return false;
                }else
                    throw  new SystemException(await client.Error(response));
            }
            catch (Exception ex)
            {
                throw new SystemException(ex.Message);
            }
        }

        public Task Logout()
        {
            throw new NotImplementedException();
        }
    }
}
