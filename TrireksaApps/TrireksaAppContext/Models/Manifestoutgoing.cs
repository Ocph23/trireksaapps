using System;
using System.Collections.Generic;

namespace TrireksaAppContext.Models
{
    public partial class Manifestoutgoing
    {
        public Manifestoutgoing()
        {
            Manifestinformation = new HashSet<Manifestinformation>();
            Packinglist = new HashSet<Packinglist>();
        }

        public int Id { get; set; }
        public int AgentId { get; set; }
        public int Code { get; set; }
        public PortType PortType { get; set; }
        public int? ReferenceId { get; set; }
        public int? Origin { get; set; }
        public int? Destination { get; set; }
        public DateTime? OnOriginPort { get; set; }
        public DateTime? OnDestinationPort { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? UserId { get; set; }

        public DateTime? UpdateDate { get; set; }

        public virtual Agent Agent { get; set; }
        public virtual Port DestinationNavigation { get; set; }
        public virtual Port OriginNavigation { get; set; }
        public virtual ICollection<Manifestinformation> Manifestinformation { get; set; }
        public virtual ICollection<Packinglist> Packinglist { get; set; }
    }
}
