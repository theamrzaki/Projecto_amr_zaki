using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using member_project.Models;

namespace member_project.Controllers
{
    public class ProjectsController : Controller
    {
        private MyDbContext db = new MyDbContext();

        // GET: Projects
        public ActionResult Index()
        {
            ViewBag.Projects = db.Projects.ToList();
            return View(db.Projects.ToList());
            
        }

        // GET: Projects/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // GET: Projects/Create
        public ActionResult Create()
        {
            return View();

        }

        // POST: Projects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "ID,Name")] Project project)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Projects.Add(project);
        //        db.SaveChanges();

        //      //  ViewBag.Records = "Name : " + project.Name;
        //      // return PartialView("ProjectMaster");

        //          return RedirectToAction("index");
        //    }

        //     return View(project);
        //}
        //[HttpPost]

        public JsonResult create_project(string value, string create, string start, string end)
        {
            if (!Request.IsAjaxRequest())
            {
                return null;
            }
            Project pro = new Project();

            pro.Name = value;
            
            string[] create_array=  create.Split('-');
            pro.Created_Date = new DateTime(Convert.ToInt16(create_array[0]), Convert.ToInt16(create_array[1]), Convert.ToInt16(create_array[2]));

            string[] start_array = start.Split('-');
            pro.Start_Date = new DateTime(Convert.ToInt16(start_array[0]), Convert.ToInt16(start_array[1]), Convert.ToInt16(start_array[2]));

            string[] end_array = end.Split('-');
            pro.End_Date = new DateTime(Convert.ToInt16(end_array[0]), Convert.ToInt16(end_array[1]), Convert.ToInt16(end_array[2]));


            db.Projects.Add(pro);
            db.SaveChanges();

            string new_value = "<td>" + value + "</td>"+ "<td>" + pro.Created_Date.ToShortDateString() + "</td>"+ "<td>" + pro.Start_Date.ToShortDateString() + "</td>"+ "<td>" + pro.End_Date.ToShortDateString() + "</td>";
           // < td >< a href =\"/Projects/Edit/" + db.Projects.Find(pro).ID + "\">Edit</a></td>
            return new JsonResult { Data = new { new_value = new_value } };
        }


        public JsonResult edit_project(int id)
        {
            if (!Request.IsAjaxRequest())
            {
                return null;
            }

           Project pro= db.Projects.Find(id);
            
           return new JsonResult { Data = new
             { pro_name= pro.Name,
               pro_create = change_date_format(pro.Created_Date),
               pro_start = change_date_format(pro.Start_Date.Date),
               pro_end = change_date_format(pro.End_Date.Date)
           } };
        }
        public string change_date_format(DateTime date)
        {
            string date_string = date.ToString().Split(' ')[0];

            string[] date_array = date_string.ToString().Split('/');
            for (int i = 0; i < date_array.Length; i++)
            {
                if (date_array[i].Length < 2)
                {
                    date_array[i] = "0" + date_array[i];
                }
            }

            return date_array[2] + "-" + date_array[1] + "-" + date_array[0];
        }

        public JsonResult edit_done(int id,string value, string create, string start, string end)
        {
            Project pro = db.Projects.Find(id);
            pro.Name = value;
            pro.Created_Date = new DateTime(Convert.ToInt16(create.Split('-')[0]), Convert.ToInt16(create.Split('-')[1]), Convert.ToInt16(create.Split('-')[2]));
            pro.Start_Date = new DateTime(Convert.ToInt16(start.Split('-')[0]), Convert.ToInt16(start.Split('-')[1]), Convert.ToInt16(start.Split('-')[2]));
            pro.End_Date = new DateTime(Convert.ToInt16(end.Split('-')[0]), Convert.ToInt16(end.Split('-')[1]), Convert.ToInt16(end.Split('-')[2]));
            
            db.Entry(pro).State = EntityState.Modified;
            db.SaveChanges();

            string new_value = "<td>" + value + "</td>" + "<td>" + pro.Created_Date.ToShortDateString() + "</td>" + "<td>" + pro.Start_Date.ToShortDateString() + "</td>" + "<td>" + pro.End_Date.ToShortDateString() + "</td>";
            // < td >< a href =\"/Projects/Edit/" + db.Projects.Find(pro).ID + "\">Edit</a></td>
            return new JsonResult { Data = new { new_value = new_value } };
        }


        public JsonResult details_project(int id)
        {
            if (!Request.IsAjaxRequest())
            {
                return null;
            }

            Project pro = db.Projects.Find(id);

            return new JsonResult
            {
                Data = new
                {
                    pro_name = pro.Name,
                    pro_create = change_date_format(pro.Created_Date),
                    pro_start = change_date_format(pro.Start_Date.Date),
                    pro_end = change_date_format(pro.End_Date.Date)
                }
            };
        }

        public JsonResult delete_project(int id)
        {
            if (!Request.IsAjaxRequest())
            {
                return null;
            }

            Project pro = db.Projects.Find(id);

            return new JsonResult
            {
                Data = new
                {
                    pro_name = pro.Name,
                }
            };
        }
        public JsonResult delete_done_(int id)
        {
            if (!Request.IsAjaxRequest())
            {
                return null;
            }

            Project project = db.Projects.Find(id);
            db.Projects.Remove(project);
            db.SaveChanges();

            return new JsonResult
            {
                Data = new
                {
                    pro_name = project.Name,
                }
            };
        }


        public JsonResult function_mvccc(string value)
        {
            if (!Request.IsAjaxRequest())
            {
                return null;
            }
            Project pro = new Project();
            pro.Name = value;
            db.Projects.Add(pro);
            db.SaveChanges();

            string _double = "DONE !!!";
            return new JsonResult { Data = new { _double = _double } };

        }

        // GET: Projects/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name")] Project project)
        {
            if (ModelState.IsValid)
            {
                db.Entry(project).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(project);
        }

        // GET: Projects/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Project project = db.Projects.Find(id);
            db.Projects.Remove(project);
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
