using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SachOnlineTVD.Models;

namespace SachOnlineTVD.Controllers
{
    public class NhaXuatBanController : Controller
    {
        // GET: NhaXuatBan
        dbSachOnlineDataContext db = new dbSachOnlineDataContext();
        public ActionResult Index()
        {
            return View(db.NHAXUATBANs.ToList());
        }
        public ActionResult XemChiTiet()
        {
            //var kq = Db.NHAXUATBANs.Take(4).SingleOrDefault();
            //var kq = db.NHAXUATBANs.Where(n => n.MaNXB == int.Parse(Request["id"])).SingleOrDefault();
            int id = int.Parse(Request["id"]);
            return View(GetNXB(id));
        }


        [HttpGet]
        public ActionResult Edit(int id)
        {
            return View(GetNXB(id));
        }


        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit()
        {
            if (ModelState.IsValid)
            {
                var nxb = GetNXB(int.Parse(Request.Form["MaNXB"]));
                nxb.TenNXB = Request.Form["TenNXB"];
                nxb.DiaChi = Request.Form["DiaChi"];
                nxb.DienThoai = Request.Form["DienThoai"];
                db.SubmitChanges();
                return RedirectToAction("Index");


            }
            else
            {
                return RedirectToAction("Edit");
            }
        }


        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Add(FormCollection collection, NHAXUATBAN nxb)
        {

            if (ModelState.IsValid)
            {
                nxb.TenNXB = Request.Form["TenNXB"];
                nxb.DiaChi = Request.Form["DiaChi"];
                nxb.DienThoai = Request.Form["DienThoai"];
                db.NHAXUATBANs.InsertOnSubmit(nxb);
                db.SubmitChanges();

                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Add");
            }
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            return View(GetNXB(id));
        }



        [HttpPost]
        public ActionResult Delete()
        {
            if (ModelState.IsValid)
            {
                var nxb = GetNXB(int.Parse(Request.Form["MaNXB"]));
                db.NHAXUATBANs.DeleteOnSubmit(nxb);
                db.SubmitChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Delete");
            }


        }

        public NHAXUATBAN GetNXB(int id)
        {
            return db.NHAXUATBANs.Where(n => n.MaNXB == id).SingleOrDefault();
        }

    }
}