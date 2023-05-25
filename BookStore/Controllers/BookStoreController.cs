using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookStore.Models;
namespace BookStore.Controllers
{
    public class BookStoreController : Controller
    {
        QLBANSACHEntities db = new QLBANSACHEntities();
        // GET: BookStore
        private List<SACH> Laysachmoi(int count)
        {
            return db.SACHes.OrderByDescending(a => a.Ngaycapnhat).Take(count).ToList();
        }
        public ActionResult Index()
        {
            var sachmoi = Laysachmoi(5);
            return View(sachmoi);
        }
        public ActionResult Chude()
        {
            var chude = from cd in db.CHUDEs select cd;
            return PartialView(chude);
        }
        public ActionResult NhaXuatBan()
        {
            var NhaXuatBan = from nxb in db.NHAXUATBANs select nxb;
            return PartialView(NhaXuatBan);
        }
        public ActionResult SPTheochude(int id)
        {
            var sach = from s in db.SACHes where s.MaCD == id select s;
            return View(sach);
        }
        public ActionResult SPTheoNXB(int id)
        {
            var sach = from s in db.SACHes where s.MaNXB == id select s;
            return View(sach);
        }
        public ActionResult Details(int id)
        {
            var sach = from s in db.SACHes
                       where s.Masach == id
                       select s;
            return View(sach.Single());
        }
    }
}