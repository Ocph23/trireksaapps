using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using TeleSharp.TL;
using TLSharp.Core;
using WebApi.Models;

namespace WebApi.Services
{
    public class TelegramService
    {
        private TelegramClient client;
        private string hash;
        private string _phoneNumber;

        public TelegramService(IOptionsMonitor<TelegramConfig> option)
        {
            client = new TelegramClient(option.CurrentValue.ApiId, option.CurrentValue.ApiHash,null, null, deleGateHandler); 
        }

        private TcpClient deleGateHandler(string address, int port)
        {
            TcpClient _tcpClient = new TcpClient();

            var ipAddress = IPAddress.Parse(address);
            var endpoint = new IPEndPoint(ipAddress, port);

            try
            {
                Console.WriteLine($"Testing connection by EndPoint: {address}:{port}");
                _tcpClient = new TcpClient(endpoint); //THIS LINE GAVE ME ERROR ALWAYS (even when I deactivate IPv6 support from my network config)
                _tcpClient.Connect(endpoint);

                return _tcpClient;
            }
            catch (Exception e)
            {
                //CONSOLE FOR TESTING PURPOSES
                Console.WriteLine("------------CONEXIÓN TCP------------");
                Console.WriteLine("Endpoint failed!");
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
                Console.WriteLine("------------------------------------");
                Console.WriteLine($"Alternative connection ({address}, {port})...");


                _tcpClient = new TcpClient("149.154.167.40", port); //HERE CONNECTION OK
                if (!_tcpClient.Connected) 
                    _tcpClient.Connect(endpoint);
                Console.WriteLine("¡CONECTADO!" + Environment.NewLine);
                return _tcpClient;
            }
        }

        internal async Task<TeleSharp.TL.Contacts.TLContacts> MakeAuth(int id)
        {
            try
            {
                var user = await client.MakeAuthAsync(_phoneNumber, hash, id.ToString());
                var result = await client.GetContactsAsync();
                return result;
            }
            catch (Exception ex)
            {

                try
                {
                    var user = await client.SignUpAsync(_phoneNumber, hash, id.ToString(), "Trireksa", "Papua");

                    if (user != null)
                    {
                        var result = await client.GetContactsAsync();
                        return result;
                    }
                    throw new SystemException("Error");
                }
                catch (Exception exx)
                {
                    throw new SystemException(exx.Message);
                }


            }
        }

        public async Task Auth(string phoneNumber)
        {
            try
            {
                _phoneNumber = phoneNumber;
                await client.ConnectAsync();
                hash = await client.SendCodeRequestAsync(phoneNumber);
               
            }
            catch (Exception ex)
            {
                throw new SystemException(ex.Message);
            }
        }



        //private void BotClient_OnReceiveError(object sender, Telegram.Bot.Args.ReceiveErrorEventArgs e)
        //{
        //    try
        //    {

        //    }
        //    catch (Exception ex)
        //    {

        //        Console.WriteLine(ex.Message);
        //    }
        //}

        //private void BotClient_OnUpdate(object sender, Telegram.Bot.Args.UpdateEventArgs e)
        //{
        //   try
        //    {

        //    }
        //    catch (Exception ex)
        //    {

        //        Console.WriteLine(ex.Message);
        //    }
        //}

        //private void BotClient_OnMessage(object sender, Telegram.Bot.Args.MessageEventArgs e)
        //{
        //   try
        //    {

        //    }
        //    catch (Exception ex)
        //    {

        //        Console.WriteLine(ex.Message);
        //    }
        //}


        
    }
}
