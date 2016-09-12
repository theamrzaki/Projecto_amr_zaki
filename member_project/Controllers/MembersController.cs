using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using member_project.Models;
using member_project.ViewModels;
using System.Collections;

namespace member_project.Controllers
{
    public class MembersController : Controller
    {
        private MyDbContext db = new MyDbContext();

        // GET: Members
        public ActionResult Index(int? id)
        {
            ViewBag.Projects = db.Projects.ToList();

            if (id != null)
            {
                List<Member> list = db.Members.ToList();
                List<Member> Resultlist = new List<Member>();

                foreach (var a in list)
                {
                    if (a.ProjectID == id)
                    {
                        Resultlist.Add(a);
                    }
                }
                ViewBag.selected_project = db.Projects.Find(id).Name;
                return View(Resultlist);
            }
            else
            {
                return View(db.Members.ToList());
            }
           // return View(db.Members.ToList());
        }

        // GET: Members/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.Members.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        // GET: Members/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Members/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Title,ProjectID")] Member member)
        {
            if (ModelState.IsValid)
            {
                db.Members.Add(member);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(member);
        }

        public JsonResult create_member(String idddd,String name, String title)
        {
            if (!Request.IsAjaxRequest())
            {                                                              
                return null;                                               
            }

              Member mem = new Member();
              mem.ProjectID =Convert.ToInt16(idddd);
              mem.Name = name;
              mem.Title = title;
              
             db.Members.Add(mem);
             db.SaveChanges();
            String new_value = "<td>" + name + "</td>" + "<td>" + title + "</td>" ;
            // < td >< a href =\"/Projects/Edit/" + db.Projects.Find(pro).ID + "\">Edit</a></td>
            return new JsonResult { Data = new { new_value = new_value } };
        }


        public JsonResult Edit_member(String iddddd)
        {
            if (!Request.IsAjaxRequest())
            {
                return null;
            }

            Member mem = db.Members.Find(Convert.ToInt16(iddddd));

            return new JsonResult
            {
                Data = new
                {
                    mem_name = mem.Name,
                    mem_title = mem.Title,
                    mem_id = mem.ID
                }
            };
        }
        public JsonResult Edit_member_done(String iddd, String Name, String title)
        {
            Member mem = db.Members.Find(Convert.ToInt16(iddd));
            mem.Name = Name;
            mem.Title = title;
         
            db.Entry(mem).State = EntityState.Modified;
            db.SaveChanges();

            String new_value = "<td>" + Name + "</td>" + "<td>" + title + "</td>" ;
            // < td >< a href =\"/Projects/Edit/" + db.Projects.Find(pro).ID + "\">Edit</a></td>
            return new JsonResult { Data = new { new_value = new_value } };
        }

        public JsonResult details_member(String idddd)
        {
            if (!Request.IsAjaxRequest())
            {
                return null;
            }

            Member mem = db.Members.Find(Convert.ToInt16(idddd));

            return new JsonResult
            {
                Data = new
                {
                    mem_name = mem.Name,
                    mem_title = mem.Title
                }
            };
        }

        public JsonResult delete_member(String iddd)
        {
            if (!Request.IsAjaxRequest())
            {
                return null;
            }

            Member mem = db.Members.Find(Convert.ToInt16(iddd));

            return new JsonResult
            {
                Data = new
                {
                    mem_name = mem.Name,
                }
            };
        }
        public JsonResult delete_done_(int id)
        {
            if (!Request.IsAjaxRequest())
            {
                return null;
            }

            Member mem = db.Members.Find(id);
            db.Members.Remove(mem);
            db.SaveChanges();

            return new JsonResult
            {
                Data = new
                {
                    pro_name = mem.Name,
                }
            };
        }

        public JsonResult change_project(int id)
        {
            if (!Request.IsAjaxRequest())
            {
                return null;
            }

            Project pro = db.Projects.Find(id);

            String new_value = "<tr><td>Name</td><td>Title</td><tr>" ;

            List<Member> list = db.Members.ToList();
            List<Member> Resultlist = new List<Member>();

            foreach (var a in list)
            {
                if (a.ProjectID == id)
                {
                    Resultlist.Add(a);
                }
            }

            foreach (var item in db.Members)
            {            //<a style="cursor:pointer" id="@item.ID." class="Edit_member">@item.Name</a> 

                new_value= new_value + "<tr>" + "<td>" 
                    + "<a style=\"cursor:pointer\" id=\""+item.ID+ "\"class=\"Edit_member\">"+ item.Name 
                    + "</td>" + "<td>" + item.Title + "</td>";

                new_value = new_value +"<td>"+
                 "<a style = \"cursor:pointer\" id = \""+item.ID+"\" class=\"Details_member\">Details</a> | <a style = \"cursor:pointer\" id=\""+item.ID+"\" class=\"Delete_member\">Delete</a>   </td>" + "</tr>";

            }
            return new JsonResult
            {
                Data = new
                {
                    new_value = new_value,
                }
            };
        }


        // GET: Members/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.Members.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        // POST: Members/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Title,ProjectID")] Member member)
        {
            if (ModelState.IsValid)
            {
                db.Entry(member).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(member);
        }

        // GET: Members/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.Members.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        // POST: Members/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Member member = db.Members.Find(id);
            db.Members.Remove(member);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
