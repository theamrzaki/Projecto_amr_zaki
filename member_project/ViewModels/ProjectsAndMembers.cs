using member_project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace member_project.ViewModels
{
    public class ProjectsAndMembers
    {
        public IEnumerable<Member> members { get; set; }
        public IEnumerable<Project> projects { get; set; }
    }
}