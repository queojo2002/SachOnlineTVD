using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SachOnlineTVD.Models;

namespace SachOnlineTVD.Areas.Admin.Controllers
{
    public class SliderController : Controller
    {
        dbSachOnlineDataContext db = new dbSachOnlineDataContext();

        // GET: Admin/Slider
        public ActionResult Index()
        {
            /*
            if (Session["Admin"] == null || Session["Admin"].ToString() == "")
            {
                return Redirect("~/Admin/Home/Login");
            }
            */

            var listSlider = from cd in db.Sliders select cd;
            return View(listSlider);

        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Slider sli, FormCollection f, HttpPostedFileBase fFileUpload)
        {
            if (fFileUpload == null)
            {
                ViewBag.ThongBao = "Hãy chọn Ảnh";

                ViewBag.TenSlider = f["sTenSlider"];
                ViewBag.MoTa = f["sMoTa"];
                ViewBag.LoaiSlider = f["sLoaiSlider"];
                ViewBag.LienKet = f["sLienKet"];
                ViewBag.HienThi = f["bHienThi"];
            }
            else
            {
                if (ModelState.IsValid)
                {
                    var fFileName = Path.GetFileName(fFileUpload.FileName);
                    var path = Path.Combine(Server.MapPath("~/Images/Slider"), fFileName);
                    if (!System.IO.File.Exists(path))
                    {
                        fFileUpload.SaveAs(path);
                    }
                    sli.TenSlider = f["sTenSlider"];
                    sli.Anh = fFileName;
                    sli.MoTaSlider = f["sMoTa"].Replace("<p>", "").Replace("</p>", "\n"); ;
                    sli.HienThi = bool.Parse(f["bHienThi"]);
                    sli.LienKet = f["sLienKet"];
                    sli.LoaiSlider = f["sLoaiSlider"];
                    db.Sliders.InsertOnSubmit(sli);
                    db.SubmitChanges();
                    /*
                    sach.TenSach = f["sTenSach"];
                    sach.MoTa = f["sMoTa"].Replace("<p>", "").Replace("</p>", "\n");
                    sach.AnhBia = fFileName;
                    sach.NgayCapNhat = Convert.ToDateTime(f["dNgayCapNhat"]);
                    sach.SoLuongBan = int.Parse(f["iSoLuong"]);
                    sach.GiaBan = decimal.Parse(f["mGiaBan"]);
                    sach.MaCD = int.Parse(f["MaCD"]);
                    sach.MaNXB = int.Parse(f["MaNXB"]);
                    db.SACHes.InsertOnSubmit(sach);
                    db.SubmitChanges();
                    */
                    return RedirectToAction("Index");
                }
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
            var kh = db.Sliders.SingleOrDefault(n => n.MaSlider == id);
            if (kh == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(kh);
        }

    }
}