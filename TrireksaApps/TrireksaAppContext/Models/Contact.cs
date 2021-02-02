using System;
using System.Collections.Generic;
using System.Text;

namespace TrireksaAppContext.Models
{
   public class Contact
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public long TelegramId { get; set; }

        public int? UserReference { get; set; }

        public UserType UserType { get; set; }

    }
}
