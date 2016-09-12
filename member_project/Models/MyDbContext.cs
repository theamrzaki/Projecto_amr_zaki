using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace member_project.Models
{
    public class MyDbContext :DbContext
    {
        public System.Data.Entity.DbSet<member_project.Models.Project> Projects { get; set; }

        public System.Data.Entity.DbSet<member_project.Models.Member> Members { get; set; }
    }
}