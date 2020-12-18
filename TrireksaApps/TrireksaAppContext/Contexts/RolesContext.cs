
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TrireksaAppContext.Models;

namespace TrireksaAppContext
{
    public class RolesContext
    {
        private ApplicationDbContext db;

        public RolesContext(ApplicationDbContext _db)
        {
            db = _db;
        }
        public IEnumerable<Role> Get()
        {
            return db.Role.ToList();
        }
    }
}
