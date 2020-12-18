using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace TrireksaAppContext
{

    public interface ITrireksaAppHub
    {
        void RemoveData(object t);
        void AddNewData(object t);
    }


    public class TrireksaAppHub:Hub<ITrireksaAppHub>
    {

        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }


        public override Task OnDisconnectedAsync(Exception exception)
        {
            return base.OnDisconnectedAsync(exception);
        }
    }
}