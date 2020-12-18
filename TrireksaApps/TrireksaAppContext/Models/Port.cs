using System;
using System.Collections.Generic;

namespace TrireksaAppContext.Models
{
    public partial class Port
    {
        public Port()
        {
            ManifestoutgoingDestinationNavigation = new HashSet<Manifestoutgoing>();
            ManifestoutgoingOriginNavigation = new HashSet<Manifestoutgoing>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public PortType PortType { get; set; }
        public string Code { get; set; }
        public int CityId { get; set; }

        public virtual City City { get; set; }
        public virtual ICollection<Manifestoutgoing> ManifestoutgoingDestinationNavigation { get; set; }
        public virtual ICollection<Manifestoutgoing> ManifestoutgoingOriginNavigation { get; set; }

    }
}
