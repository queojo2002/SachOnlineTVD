using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SachOnline.Areas.Admin.Controllers;
using SachOnlineTVD.Models;

namespace SachOnlineTVD.Areas.Admin.Controllers
{
    public class TrangTinController : Controller
    {
        dbSachOnlineDataContext data = new dbSachOnlineDataContext();

        // GET: Admin/TrangTin
        public ActionResult Index()
        {
            return View(data.TRANGTINs.ToList());
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(TRANGTIN tt)
        {
            if (ModelState.IsValid)
            {
                tt.MetaTitle = tt.TenTrang.RemoveDiacritics().Replace(" ", "-");
                tt.NgayTao = DateTime.Now;
                data.TRANGTINs.InsertOnSubmit(tt);
                data.SubmitChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var kq = data.TRANGTINs.Where(t => t.MaTT == id);
            return View(kq.SingleOrDefault());
        }

        [HttpPost]
        public ActionResult Edit(FormCollection f)
        {
            if (ModelState.IsValid)
            {
                var tt = data.TRANGTINs.Where(t => t.MaTT == int.Parse(f["MaTT"])).SingleOrDefault();
                tt.TenTrang = f["TenTrang"];
                tt.NoiDung = f["NoiDung"];
                tt.NgayTao = Convert.ToDateTime(f["NgayTao"]);
                tt.MetaTitle = f["TenTrang"].RemoveDiacritics().Replace(" ", "-");
                data.SubmitChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Edit");
            }

        }


        [HttpGet]
        public ActionResult Delete(int id)
        {
            var kq = data.TRANGTINs.Where(t => t.MaTT == id);
            return View(kq.SingleOrDefault());
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirm(int id)
        {
            var kq = data.TRANGTINs.Where(t => t.MaTT == id).SingleOrDefault();
            data.TRANGTINs.DeleteOnSubmit(kq);
            data.SubmitChanges();
            return RedirectToAction("Index");

        }

    }
}