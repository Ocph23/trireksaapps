﻿using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using TrireksaAppContext.Models;

namespace WebApi.Api
{
    public class TestController : ControllerBase
    {
        // GET: api/Test
        public  IEnumerable<Port> Get()
        {
            return null;
        }

        // GET: api/Test/5
        public IActionResult  Get(int id)
        {
            return Ok("Test");
        }


        public IActionResult GetLocalIpAddress()
        {
            try
            {
                var host = Dns.GetHostEntry(Dns.GetHostName());
                foreach (var ip in host.AddressList)
                {
                    if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    {
                       if( ip.ToString().Split('.')[0] == "192")
                        return Ok(ip.ToString());
                    }
                    
                }
                throw new SystemException("Jaringan Dengan IP4 tidak ditemukan");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: api/Test
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Test/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Test/5
        public void Delete(int id)
        {
        }
    }
}
