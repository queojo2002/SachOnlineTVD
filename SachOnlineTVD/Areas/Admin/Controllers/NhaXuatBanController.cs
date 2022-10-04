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
    public class NhaXuatBanController : Controller
    {
        dbSachOnlineDataContext db = new dbSachOnlineDataContext();

        // GET: Admin/NhaXuatBan
        public ActionResult Index(int? page)
        {
            if (Session["Admin"] == null || Session["Admin"].ToString() == "")
            {
                return Redirect("~/Admin/Home/Login");
            }
            int iPageNum = (page ?? 1);
            int iPageSize = 7;
            return View(db.NHAXUATBANs.ToList().OrderBy(n => n.MaNXB).ToPagedList(iPageNum, iPageSize));
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
        public ActionResult Create(NHAXUATBAN nxb, FormCollection f)
        {
            if (ModelState.IsValid)
            {
                nxb.TenNXB = f["sTenNXB"];
                nxb.DiaChi = f["sDiaChi"];
                nxb.DienThoai = f["nDienThoai"];
                db.NHAXUATBANs.InsertOnSubmit(nxb);
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
            var nxb = db.NHAXUATBANs.SingleOrDefault(n => n.MaNXB == id);
            if (nxb == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(nxb);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(FormCollection f)
        {
            var nxb = db.NHAXUATBANs.SingleOrDefault(n => n.MaNXB == int.Parse(f["iMaNXB"]));
            if (ModelState.IsValid)
            {
                nxb.TenNXB = f["sTenNXB"];
                nxb.DiaChi = f["sDiaChi"];
                nxb.DienThoai = f["nSDT"];
                db.SubmitChanges();
                return RedirectToAction("Index");
            }
            return View(nxb);
        }



        [HttpGet]
        public ActionResult Delete(int id)
        {
            if (Session["Admin"] == null || Session["Admin"].ToString() == "")
            {
                return Redirect("~/Admin/Home/Login");
            }
            var nxb = db.NHAXUATBANs.SingleOrDefault(n => n.MaNXB == id);
            if (nxb == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(nxb);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirm(int id, FormCollection f)
        {
            var nxb = db.NHAXUATBANs.SingleOrDefault(n => n.MaNXB == id);
            if (nxb == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            var sach = db.SACHes.Where(s => s.MaNXB == id);
            if (sach.Count() > 0)
            {
                ViewBag.ThongBao = "NXB này đang có trong bảng SACH <br>" +
                    "Nếu muốn xóa thì phải xóa hết NXB này trong bảng SACH";
                return View(nxb);
            }
            db.NHAXUATBANs.DeleteOnSubmit(nxb);
            db.SubmitChanges();
            return RedirectToAction("Index");
        }


        public ActionResult Details(int id)
        {
            var nxb = db.NHAXUATBANs.SingleOrDefault(n => n.MaNXB == id);
            if (nxb == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(nxb);
        }



    }
}