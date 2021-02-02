using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using TLSharp.Core;
using TLSharp.Core.Network;

namespace WebApi.Models
{
    public class TelegramConfig
    {

        public int ApiId { get; set; }
        public string ApiHash { get; set; }
        public string ApiToken { get; set; }


        public  TLSharp.Core.Session Session
        {
            get
            {
                var store = new FileSessionStore();
                return Session.TryLoadOrCreateNew(store, "session");
            }
        }

        public TcpTransport Transport
        {
            get
            {
                return new TcpTransport("149.154.167.40", 443);
            }
        }
    }



    


    public class TcpTransport
    {
        private TcpClient _tcpClient;

        public TcpTransport(string address, int port, TcpClientConnectionHandler handler = null)
        {
            if (handler == null)
            {
                var ipAddress = IPAddress.Parse(address);
                var endpoint = new IPEndPoint(ipAddress, port);

                try
                {
                    Console.WriteLine($"Testing connection by EndPoint: {address}:{port}");
                    _tcpClient = new TcpClient(endpoint);
                    _tcpClient.Connect(endpoint);
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

                    _tcpClient = new TcpClient(address, port); //HERE CONNECTION OK
                    if (!_tcpClient.Connected) _tcpClient.Connect(endpoint);
                    Console.WriteLine("¡CONECTADO!" + Environment.NewLine);
                }
            }
            else
                _tcpClient = handler(address, port);
        }
    }

   
}
