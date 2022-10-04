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
    public class SachController : Controller
    {
        dbSachOnlineDataContext db = new dbSachOnlineDataContext();
        // GET: Admin/Sach
        public ActionResult Index(int ? page)
        {
            int iPageNum = (page ?? 1);
            int iPageSize = 7;
            

            return View(db.SACHes.ToList().OrderBy(n => n.MaSach).ToPagedList(iPageNum,iPageSize));
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.MaCD = new SelectList(db.CHUDEs.ToList().OrderBy(n => n.TenChuDe),"MaCD","TenChuDe");
            ViewBag.MaNXB = new SelectList(db.NHAXUATBANs.ToList().OrderBy(n => n.MaNXB), "MaNXB", "TenNXB");
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(SACH sach, FormCollection f, HttpPostedFileBase fFileUpload)
        {
            ViewBag.MaCD = new SelectList(db.CHUDEs.ToList().OrderBy(n => n.TenChuDe), "MaCD", "TenChuDe");
            ViewBag.MaNXB = new SelectList(db.NHAXUATBANs.ToList().OrderBy(n => n.MaNXB), "MaNXB", "TenNXB");
            if (fFileUpload == null)
            {
                ViewBag.ThongBao = "Hãy chọn Ảnh Bìa";

                ViewBag.TenSach = f["sTenSach"];
                ViewBag.MoTa = f["sMoTa"];
                ViewBag.SoLuong = f["iSoLuong"];
                ViewBag.GiaBan = f["mGiaBan"];
                ViewBag.MaCD = new SelectList(db.CHUDEs.ToList().OrderBy(n => n.TenChuDe), "MaCD", "TenChuDe", int.Parse(f["MaCD"]));
                ViewBag.MaNXB = new SelectList(db.NHAXUATBANs.ToList().OrderBy(n => n.MaNXB), "MaNXB", "TenNXB", int.Parse(f["MaNXB"]));
            }else
            {
                if (ModelState.IsValid)
                {
                    var fFileName = Path.GetFileName(fFileUpload.FileName);
                    var path = Path.Combine(Server.MapPath("~/Images"), fFileName);
                    if (!System.IO.File.Exists(path))
                    {
                        fFileUpload.SaveAs(path);
                    }
                    sach.TenSach = f["sTenSach"];
                    sach.MoTa = f["sMoTa"].Replace("<p>","").Replace("</p>", "\n");
                    sach.AnhBia = fFileName;
                    sach.NgayCapNhat = Convert.ToDateTime(f["dNgayCapNhat"]);
                    sach.SoLuongBan = int.Parse(f["iSoLuong"]);
                    sach.GiaBan = decimal.Parse(f["mGiaBan"]);
                    sach.MaCD = int.Parse(f["MaCD"]);
                    sach.MaNXB = int.Parse(f["MaNXB"]);
                    db.SACHes.InsertOnSubmit(sach);
                    db.SubmitChanges();
                    return RedirectToAction("Index");
                }
            }
            return View();
        }
    }
}