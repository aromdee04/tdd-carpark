using CarPark.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarPark.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult CheckIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CheckIn(string PlateNo)
        {
            using (var db = new CarParkDb())
            {
                var t = new Ticket();
                t.PlateNo = PlateNo;
                t.DateIn = DateTime.Now;

                db.Tickets.Add(t);
                db.SaveChanges();
            }
            return RedirectToAction("CheckIn");
        }

        public ActionResult CheckOut()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CheckOut(int TicketId)
        {
            using (var db = new CarParkDb())
            {
                var t = db.Tickets.Find(TicketId);
                if (t == null) return HttpNotFound();

                t.DateOut = DateTime.Now;
                db.SaveChanges();
                TempData["LastTicket"] = t;
            }
            return RedirectToAction("CheckOut");
        }
    }
}