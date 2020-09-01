using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Test.Sujith.Krishantha.Models;

namespace Test.Sujith.Krishantha.Controllers
{
    public class BatchesController : Controller
    {
        private CRUDContext context = new CRUDContext();

        public ActionResult getAll()
        {
            
            return Json(context.Classes.Select(x => new
            {
                x.Id,
                x.Batch
            }).ToList(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Delete(int Id)
        {
            var rec = from stu in context.Students
                      join cl in context.Classes on stu.Class_Id equals cl.Id
                      where stu.Class_Id == Id
                      select new { stu, cl};

            foreach (var i in rec)
            {
                context.Students.Remove(i.stu);
                //context.Classes.Remove(i.cl);
            }
            context.SaveChanges();
            return Json(rec, JsonRequestBehavior.AllowGet);
        }
    }
}