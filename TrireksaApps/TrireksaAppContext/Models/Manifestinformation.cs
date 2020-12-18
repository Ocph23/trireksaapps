using System;
using System.Collections.Generic;

namespace TrireksaAppContext.Models
{
    public partial class Manifestinformation
    {
        public int Id { get; set; }
        public ManifestType ManifestType { get; set; }
        public int ManifestId { get; set; }
        public PortType PortType { get; set; }
        public string ArmadaName { get; set; }
        public string CrewName { get; set; }
        public string Contact { get; set; }
        public string Address { get; set; }
        public string ReferenceNumber { get; set; }

        public virtual Manifestoutgoing Manifest { get; set; }
    }
}
