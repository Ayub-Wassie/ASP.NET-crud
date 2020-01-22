using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ASP.Net_CRUD.Models;

namespace ASP.Net_CRUD.Controllers
{
    public class TravlerController : Controller
    {
        // GET: Travler
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetData() 
        {
            using (DBModel db = new DBModel()) {
                List<Travler> traList = db.Travlers.ToList<Travler>();
                return Json(new { data = traList }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult AddOrEdit(int id = 0)
        {
            if (id == 0)
                return View(new Travler());
            else
            {
                using (DBModel db = new DBModel())
                {
                    return View(db.Travlers.Where(x => x.TravlerID == id).FirstOrDefault<Travler>());
                }
            }
        }

        [HttpPost]
        public ActionResult AddOrEdit(Travler tr)
        {
            using (DBModel db = new DBModel())
            {
                if (tr.TravlerID == 0)
                {
                    db.Travlers.Add(tr);
                    db.SaveChanges();
                    return Json(new { success = true, message = "Saved Successfully" }, JsonRequestBehavior.AllowGet);
                }
                else {
                    db.Entry(tr).State = EntityState.Modified;
                    db.SaveChanges();
                    return Json(new { success = true, message = "Updated Successfully" }, JsonRequestBehavior.AllowGet);
                }
            }

        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            using (DBModel db = new DBModel())
            {
                Travler tr = db.Travlers.Where(x => x.TravlerID == id).FirstOrDefault<Travler>();
                db.Travlers.Remove(tr);
                db.SaveChanges();
                return Json(new { success = true, message = "Deleted Successfully" }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}