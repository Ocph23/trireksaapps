using System;
using System.Collections.Generic;

namespace TrireksaAppContext.Models
{
    public partial class Cityagentcanaccess
    {
        public int Id { get; set; }
        public int AgentId { get; set; }
        public int CityId { get; set; }

        public virtual Agent Agent { get; set; }
    }
}
