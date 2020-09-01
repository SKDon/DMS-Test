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
        // GET: Batches
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult getAll()
        {

            return Json(context.Classes.Select(x => new
            {
                ID = x.Id,
                Batch = x.Batch
            }).ToList(), JsonRequestBehavior.AllowGet);
        }
    }
}