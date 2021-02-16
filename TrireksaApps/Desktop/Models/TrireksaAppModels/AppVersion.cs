using System;


namespace ModelsShared.Models
{
    public class AppVersion
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Version { get; set; }
        public DateTime Created{ get; set; }
    }
}
