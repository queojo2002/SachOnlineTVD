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
    public class ChuDeController : Controller
    {
        dbSachOnlineDataContext db = new dbSachOnlineDataContext();

        // GET: Admin/ChuDe
        public ActionResult Index(int? page)
        {
            if (Session["Admin"] == null || Session["Admin"].ToString() == "")
            {
                return Redirect("~/Admin/Home/Login");
            }
            int iPageNum = (page ?? 1);
            int iPageSize = 7;
            return View(db.CHUDEs.ToList().OrderBy(n => n.MaCD).ToPagedList(iPageNum, iPageSize));
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
        public ActionResult Create(CHUDE cd, FormCollection f)
        {
            if (ModelState.IsValid)
            {
                cd.TenChuDe = f["sTenChuDe"];
                db.CHUDEs.InsertOnSubmit(cd);
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
            var chude = db.CHUDEs.SingleOrDefault(n => n.MaCD == id);
            if (chude == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(chude);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(FormCollection f, HttpPostedFileBase fFileUpload)
        {
            var chude = db.CHUDEs.SingleOrDefault(n => n.MaCD == int.Parse(f["iMaCD"]));
            if (ModelState.IsValid)
            {
                chude.TenChuDe = f["sTenChuDe"];
                db.SubmitChanges();
                return RedirectToAction("Index");
            }
            return View(chude);
        }



        [HttpGet]
        public ActionResult Delete(int id)
        {
            if (Session["Admin"] == null || Session["Admin"].ToString() == "")
            {
                return Redirect("~/Admin/Home/Login");
            }
            var cd = db.CHUDEs.SingleOrDefault(n => n.MaCD == id);
            if (cd == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(cd);
        }



        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirm(int id, FormCollection f)
        {
            var cd = db.CHUDEs.SingleOrDefault(n => n.MaCD == id);
            if (cd == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            var sach = db.SACHes.Where(s => s.MaCD == id);
            if (sach.Count() > 0)
            {
                ViewBag.ThongBao = "CHỦ ĐỀ này đang có trong bảng SACH <br>" +
                    "Nếu muốn xóa thì phải xóa hết CHỦ ĐỀ này trong bảng SACH";
                return View(cd);
            }
            db.CHUDEs.DeleteOnSubmit(cd);
            db.SubmitChanges();
            return RedirectToAction("Index");
        }


        public ActionResult Details(int id)
        {
            var cd = db.CHUDEs.SingleOrDefault(n => n.MaCD == id);
            if (cd == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(cd);
        }







    }
}