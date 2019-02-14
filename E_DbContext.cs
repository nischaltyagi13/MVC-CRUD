using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Employee.Models
{
    public class E_DbContext: DbContext
    {
     
        public DbSet<Employeelist> employees { get; set; }
        public DbSet<Credential> credentials { get; set; }

    }
}