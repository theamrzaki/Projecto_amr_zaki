using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace member_project.Models
{
    public class Member
    {
        [Key]
        public int ID { get; set; }

        public string Name { get; set; }
        public string Title { get; set; }

        public int ProjectID { get; set; }

    }
}