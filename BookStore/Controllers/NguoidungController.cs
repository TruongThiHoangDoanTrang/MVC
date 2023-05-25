using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStore.Controllers
{
    public class NguoidungController : Controller
    {
        // GET: Nguoidung
        QLBANSACHEntities db = new QLBANSACHEntities();
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Dangky()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Dangky(KHACHHANG kh)
        {
            db.KHACHHANGs.Add(kh);
            db.SaveChanges();   
            return View();
        }

    }
}