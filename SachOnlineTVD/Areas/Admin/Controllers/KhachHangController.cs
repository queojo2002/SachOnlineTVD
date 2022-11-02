using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SachOnlineTVD.Models;
using PagedList;
using PagedList.Mvc;
using System.IO;
namespace SachOnlineTVD.Areas.Admin.Controllers
{
    public class KhachHangController : Controller
    {
        dbSachOnlineDataContext db = new dbSachOnlineDataContext();

        // GET: Admin/KhachHang


        public ActionResult Index(int? page)
        {
            if (Session["Admin"] == null || Session["Admin"].ToString() == "")
            {
                return Redirect("~/Admin/Home/Login");
            }
            int iPageNum = (page ?? 1);
            int iPageSize = 7;
            return View(db.KHACHHANGs.ToList().OrderBy(n => n.MaKH).ToPagedList(iPageNum, iPageSize));
        }

        [HttpGet]
        public ActionResult Create()
        {
            if (Session["Admin"] == null || Session["Admin"].ToString() == "")
            {
                return Redirect("~/Admin/Home/Login");
            }
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(KHACHHANG kh, FormCollection f)
        {
            if (ModelState.IsValid)
            {
                kh.HoTen = f["sHoTen"];
                kh.TaiKhoan = f["sTaiKhoan"];
                kh.MatKhau = f["sMatKhau"];
                kh.Email = f["sEmail"];
                kh.DiaChi = f["sDiaChi"];
                kh.DienThoai = f["sDienThoai"];
                kh.NgaySinh = DateTime.Parse(f["dNgaySinh"]);
                db.KHACHHANGs.InsertOnSubmit(kh);
                db.SubmitChanges();
                return RedirectToAction("Index");
            }
            return View();
        }


        [HttpGet]
        public ActionResult Edit(int id)
        {

            if (Session["Admin"] == null || Session["Admin"].ToString() == "")
            {
                return Redirect("~/Admin/Home/Login");
            }
            var kh = db.KHACHHANGs.SingleOrDefault(n => n.MaKH == id);
            if (kh == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(kh);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(FormCollection f)
        {
            var kh = db.KHACHHANGs.SingleOrDefault(n => n.MaKH == int.Parse(f["iMaKH"]));
            if (ModelState.IsValid)
            {
                kh.HoTen = f["sHoTen"];
                kh.TaiKhoan = f["sTaiKhoan"];
                kh.MatKhau = f["sMatKhau"];
                kh.Email = f["sEmail"];
                kh.DiaChi = f["sDiaChi"];
                kh.DienThoai = f["nDienThoai"];
                kh.NgaySinh = DateTime.Parse(f["dNgaySinh"]);
                db.SubmitChanges();
                return RedirectToAction("Index");
            }
            return View(kh);
        }


        [HttpGet]
        public ActionResult Delete(int id)
        {
            if (Session["Admin"] == null || Session["Admin"].ToString() == "")
            {
                return Redirect("~/Admin/Home/Login");
            }
            var kh = db.KHACHHANGs.SingleOrDefault(n => n.MaKH == id);
            if (kh == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(kh);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirm(int id, FormCollection f)
        {
            var kh = db.KHACHHANGs.SingleOrDefault(n => n.MaKH == id);
            if (kh == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            var ddh = db.DONDATHANGs.Where(s => s.MaKH == id);
            if (ddh.Count() > 0)
            {
                ViewBag.ThongBao = "Khách hàng này đang có trong bảng DONDATHANG <br>" +
                    "Nếu muốn xóa thì phải xóa hết Khách hàng này trong bảng DONDATHANG";
                return View(kh);
            }
            db.KHACHHANGs.DeleteOnSubmit(kh);
            db.SubmitChanges();
            return RedirectToAction("Index");
        }


        public ActionResult Details(int id)
        {
            if (Session["Admin"] == null || Session["Admin"].ToString() == "")
            {
                return Redirect("~/Admin/Home/Login");
            }
            var kh = db.KHACHHANGs.SingleOrDefault(n => n.MaKH == id);
            if (kh == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(kh);
        }


    }
}