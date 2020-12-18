using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsShared.Models
{
   
    public class CityAgentCanAccess:BaseNotify
    {
        public int Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        public int AgentId
        {
            get => _agentid;
            set => SetProperty(ref _agentid, value);
        }

        public int CityId
        {
            get => _cityid;
            set => SetProperty(ref _cityid, value);
        }

        private int _id;
        private int _agentid;
        private int _cityid;

    }

}
