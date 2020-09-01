using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Test.Sujith.Krishantha.Models;

namespace Test.Sujith.Krishantha.Controllers
{
    public class StudentsController : Controller
    {
        private CRUDContext context = new CRUDContext();
        // GET: Students
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetAllStudents()
        {

            var draw = Request.Form.GetValues("draw").FirstOrDefault();
            var start = Request.Form.GetValues("start").FirstOrDefault();
            var length = Request.Form.GetValues("length").FirstOrDefault();
            var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
            var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
            var searchValue = Request.Form.GetValues("search[value]").FirstOrDefault();

            //Paging Size (10,20,50,100)  
            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = 0;

            var studentData = (from tempstudent in context.Students
                                select tempstudent);

            //Sorting  
            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
            {

                if (sortColumn == "Id")
                {
                    studentData = sortColumnDir == "asc" ? studentData.OrderBy(m => m.Id) : studentData.OrderByDescending(m => m.Id);
                }
                else if (sortColumn == "Name")
                {
                    studentData = sortColumnDir == "asc" ? studentData.OrderBy(m => m.Name) : studentData.OrderByDescending(m => m.Name);
                }
                else if (sortColumn == "Email")
                {
                    studentData = sortColumnDir == "asc" ? studentData.OrderBy(m => m.Email) : studentData.OrderByDescending(m => m.Email);
                }
                else if (sortColumn == "Contact")
                {
                    studentData = sortColumnDir == "asc" ? studentData.OrderBy(m => m.Contact) : studentData.OrderByDescending(m => m.Contact);
                }

            }

            //Search  
            if (!string.IsNullOrEmpty(searchValue))
            {
                studentData = studentData.Where(m => m.Name == searchValue
                || m.Contact == searchValue || m.Email == searchValue);
            }

            //total number of rows count   
            recordsTotal = studentData.Count();
            //Paging   
            var data = studentData.Skip(skip).Take(pageSize).ToList();

            return Json(new { recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data }, JsonRequestBehavior.AllowGet);

        }

        public JsonResult edit(int id)
        {
            List<Student> lst = context.Students.Where(o => o.Id == id).ToList();
            return Json(lst, JsonRequestBehavior.AllowGet);
        }

        public JsonResult delete(int id)
        {
            Student lst = context.Students.Where(o => o.Id == id).FirstOrDefault() as Student;
            context.Students.Remove(lst);
            context.SaveChanges();
            return Json(lst, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult save(Student student)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    context.Students.Add(student);
                    context.SaveChanges();
                    return Json(new { success = true, data = student });
                }
                else
                {
                    return Json(new { success = false });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public JsonResult update(Student student)
        {
            if (ModelState.IsValid)
            {
                context.Entry(student).State = EntityState.Modified;
                context.SaveChanges();
                return Json(new { success = true, data = student });
            }
            else
            {
                return Json(new { success = false });
            }
        }
    }
}