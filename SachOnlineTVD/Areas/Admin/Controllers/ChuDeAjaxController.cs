using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SachOnlineTVD.Models;
namespace SachOnlineTVD.Areas.Admin.Controllers
{
    public class ChuDeAjaxController : Controller
    {
        dbSachOnlineDataContext db = new dbSachOnlineDataContext();

        // GET: Admin/ChuDeAjax
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult Select_ChuDe(int pageSize, int? page)
        {
            if (page > 0)
            {
                page = page;
            }
            else
            {
                page = 1;
            }
            try
            {
                var dsCD = (from cd in db.CHUDEs
                            select new
                            {
                                cd.MaCD,
                                cd.TenChuDe
                            }

                            ).ToList();


                int start = (int)(page - 1) * pageSize;
                ViewBag.pageCurrent = page;
                int totalPage = dsCD.Count();
                float totalNumsize = (totalPage / (float)pageSize);
                int numSize = (int)Math.Ceiling(totalNumsize);
                ViewBag.numSize = numSize;
                var dataPost = dsCD.Skip(start).Take(pageSize);
                return Json(new { code = 1, dsCD = dataPost, pageCurrent = page, numSize = numSize, msg = "Lấy thành công á :3" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { code = 0, msg = "Lấy danh hổng được, hình như chưa có data á :3" + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpGet]
        public JsonResult Select_ChuDe_By_MaCD(int MaCD)
        {
            try
            {
                var dsCD = (from cd in db.CHUDEs
                            where (cd.MaCD == MaCD)
                            select new
                            {
                                cd.MaCD,
                                cd.TenChuDe
                            }

                            ).SingleOrDefault();

                return Json(new { code = 1, cd = dsCD, msg = "Lấy thành công á :3" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { code = 0, msg = "Lấy danh hổng được, hình như có lỗi á :3" + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpPost]
        public JsonResult Insert_ChuDe(string strTenCD)
        {
            try
            {
                var cd = new CHUDE();
                cd.TenChuDe = strTenCD;
                db.CHUDEs.InsertOnSubmit(cd);
                db.SubmitChanges();
                return Json(new { code = 1, msg = "Thêm thành công nha :3" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { code = 0, msg = "Thêm hổng được, hình như có lỗi á :3" + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult Update_ChuDe_By_MaCD(int MaCD, string strTenCD)
        {
            try
            {
                var cd = db.CHUDEs.SingleOrDefault(c => c.MaCD == MaCD);
                cd.TenChuDe = strTenCD;
                db.SubmitChanges();
                return Json(new { code = 1, msg = "Sửa thành công nha :3" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { code = 0, msg = "Sửa hổng được, hình như có lỗi á :3" + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult Delete_ChuDe_By_MaCD(int MaCD)
        {
            try
            {
                var cd = db.CHUDEs.SingleOrDefault(c => c.MaCD == MaCD);
                db.CHUDEs.DeleteOnSubmit(cd);
                db.SubmitChanges();
                return Json(new { code = 1, msg = "Xóa thành công nha :3" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { code = 0, msg = "Xóa hổng được, hình như có lỗi á :3 \nChi tiết lỗi: " + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

    }
}