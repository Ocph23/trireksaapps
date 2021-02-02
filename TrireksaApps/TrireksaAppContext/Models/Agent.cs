using System;
using System.Collections.Generic;


namespace TrireksaAppContext.Models
{
    public partial class Agent
    {
        public Agent()
        {
           // Cityagentcanaccess = new HashSet<Cityagentcanaccess>();
            //Manifestoutgoing = new HashSet<Manifestoutgoing>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Npwp { get; set; }
        public string ContactName { get; set; }
        public string Phone { get; set; }
        public string Handphone { get; set; }
        public string Email { get; set; }
        public int CityId { get; set; }

        public virtual ICollection<Cityagentcanaccess> Cityagentcanaccess { get; set; }
        public virtual ICollection<Manifestoutgoing> Manifestoutgoing { get; set; }
    }
}
