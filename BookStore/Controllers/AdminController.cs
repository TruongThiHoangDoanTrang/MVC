using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookStore.Models;
using PagedList;
using PagedList.Mvc;

namespace BookStore.Controllers
{
    public class AdminController : Controller
    {
        QLBANSACHEntities2 db = new QLBANSACHEntities2();
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Sach(int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 7;
            //return View(db.SACHes.ToList());
            return View(db.SACHes.ToList().OrderBy(n => n.Masach).ToPagedList(pageNumber, pageSize));
        }
        [HttpGet]
        public ActionResult ThemSach()
        {
            //Load dropdownlist nhà xuất bản
            ViewBag.MaNXB = new SelectList(db.NHAXUATBANs.OrderBy(n => n.TenNXB), "MaNXB", "TenNXB");
            ViewBag.MaCD = new SelectList(db.CHUDEs.OrderBy(n => n.TenChuDe), "MaCD", "TenChuDe");
            return View();
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult ThemSach(SACH s, HttpPostedFileBase Anhbia)
        {
            ViewBag.MaNXB = new SelectList(db.NHAXUATBANs.OrderBy(n => n.TenNXB), "MaNXB", "TenNXB");
            ViewBag.MaCD = new SelectList(db.CHUDEs.OrderBy(n => n.TenChuDe), "MaCD", "TenChuDe");
            //Kiểm tra hình ảnh tồn tại chưa
            if (Anhbia.ContentLength > 0)
            {
                //lấy tên hình ảnh
                var fileName = Path.GetFileName(Anhbia.FileName);
                //lấy hình chuyển vào thư mục hình ảnh sản phẩm
                var path = Path.Combine(Server.MapPath("~/HinhSanPham/"), fileName);
                //nếu hình ảnh đã có trong thư mục thì xuất ra thông báo
                if (System.IO.File.Exists(path))
                {
                    ViewBag.upload = "Hình đã tồn tại";
                    return View();
                }
                else
                {
                    Anhbia.SaveAs(path);
                    //Session["tenhinh"] = Anhbia.FileName;
                    //ViewBag.tenhinh = "";
                    s.Anhbia = fileName;
                }
            }
            db.SACHes.Add(s);
            db.SaveChanges();
            return RedirectToAction("Index", "Admin");
        }
        public ActionResult ChiTietSach(int id)
        {
            //lấy ra đối tượng sách theo mã
            SACH sach = db.SACHes.SingleOrDefault(n => n.Masach == id);
            ViewBag.Masach = sach.Masach;
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(sach);

        }
        public ActionResult XoaSach(int id)
        {
            //lấy ra đối tượng sách theo mã
            SACH sach = db.SACHes.SingleOrDefault(n => n.Masach == id);
            ViewBag.Masach = sach.Masach;
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(sach);

        }
        [HttpPost, ActionName("XoaSach")]
        public ActionResult XacNhanXoa(int id)
        {
            //lấy ra đối tượng sách cần xóa theo mã
            SACH sach = db.SACHes.SingleOrDefault(n => n.Masach == id);
            ViewBag.Masach = sach.Masach;
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.SACHes.Remove(sach);
            db.SaveChanges();
            return RedirectToAction("Sach");
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection collection)
        {
            //Gán các giá trị thường dùng nhập liệu cho các biên
            var tendn = collection["username"];
            var matkhau = collection["password"];
            if (String.IsNullOrEmpty(tendn))
            {
                ViewData["Loi1"] = "Phải nhập tên đăng nhập";
            }
            else if (String.IsNullOrEmpty(matkhau))
            {
                ViewData["Loi2"] = "Phải nhập mật khẩu";
            }
            else
            {
                //Gán giá trị cho đối tượng tạo mới (ad)

                admin ad = db.admins.SingleOrDefault(n => n.UserAdmin == tendn && n.PassAdmin == matkhau);
                if (ad != null)
                {
                    //ViewBag.Thongbao = "Chúc mừng đăng nhập thành công";
                    Session["Taikhoanadmin"] = ad;
                    return RedirectToAction("Index", "Admin");
                }
                else
                    ViewBag.Thongbao = "Tên đăng nhập hoặc mật khẩu không đúng";
            }
            return View();
        }
        [HttpGet]
        public ActionResult ThemmoiSach()
        {
            // Đưa dữ liệu vào DropdownList
            // Lấy danh sách từ Table chủ đề, sắp xếp tăng dần theo tên chủ đề, chọn lấy giá trị Ma CD, hiển thị Tenchude
            ViewBag.MaCD = new SelectList(db.CHUDEs.ToList().OrderBy(n => n.TenChuDe), "MaCD", "Tenchude");
            ViewBag.MaNXB = new SelectList(db.NHAXUATBANs.ToList().OrderBy(n => n.TenNXB), "MaNXB", "TenNXB");
            return View();
        }
        //[HttpPost]
        //public ActionResult Login(FormCollection collection)
        //{
        //    var tendn = collection["username"];
        //    var matkhau = collection["password"];
        //    if (String.IsNullOrEmpty(tendn))
        //    {
        //        ViewData["Loi1"] = "Phải nhập tên đăng nhập";
        //    }
        //    else if (String.IsNullOrEmpty(matkhau))
        //    {
        //        ViewData["Loi2"] = "Phải nhập mật khẩu";
        //    }
        //    else
        //    {
        //        //gán giá trị cho đối tượng được tạo mới
        //        Admin ad = db.Admins.SingleOrDefault(n => n.UserAdmin == tendn && n.PassAdmin == matkhau);
        //        if (ad != null)
        //        {
        //            //ViewBag.Thongbao = "Chúc mừng bạn đăng nhập thành công"
        //            Session["Taikhoanadmin"] = ad;
        //            return RedirectToAction("Index", "Admin");
        //        }
        //        else
        //            ViewBag.Thongbao = "Tên đăng nhập hoặc mật khảu không đúng";
        //    }
        //    return View();
        //}
    }
}