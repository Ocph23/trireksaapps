using System;
using System.Collections.Generic;


namespace TrireksaAppContext.Models
{
    public partial class AppVersion
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Version { get; set; }
        public DateTime Created{ get; set; }
    }
}
