using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace member_project.Models
{
    public class Project
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }

        public DateTime Created_Date { get; set; }
        public DateTime Start_Date { get; set; }
        public DateTime End_Date { get; set; }

    }
}