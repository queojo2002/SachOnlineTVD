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

    public class MenuController : Controller
    {
        dbSachOnlineDataContext db = new dbSachOnlineDataContext();

        // GET: Admin/Menu
        public ActionResult Index()
        {
            List<MENU> lst = new List<MENU>();
            lst = db.MENUs.Where(m => m.ParentId == null).OrderBy(m => m.OrderNumber).ToList();
            int[] a = new int[lst.Count()];
            for (int i = 0; i < lst.Count; i++)
            {
                var l = db.MENUs.Where(m => m.ParentId == lst[i].Id);
                a[i] = l.Count();
            }
            ViewBag.lst = a;
            /*
            if (Session["Admin"] == null || Session["Admin"].ToString() == "")
            {
                return Redirect("~/Admin/Home/Login");
            }
            int iPageNum = (page ?? 1);
            int iPageSize = 7;
            //var kq = from s in db.MENUs where s.ParentID == null select s;
            return View(kq.ToList().OrderBy(n => n.ID).ToPagedList(iPageNum, iPageSize));
            */
            return View(lst);
        }

        public ActionResult LoadChildMenu(int parentId)
        {
            List<MENU> lst = new List<MENU>();
            lst = db.MENUs.Where(m => m.ParentId == parentId).OrderBy(m => m.OrderNumber).ToList();
            ViewBag.Count = lst.Count();
            int[] a = new int[lst.Count()];
            for (int i = 0; i < lst.Count; i++)
            {
                var l = db.MENUs.Where(m => m.ParentId == lst[i].Id);
                a[i] = l.Count();
            }
            ViewBag.lst = a;
            return PartialView("LoadChildMenu", lst);
        }


        [HttpGet]
        public JsonResult Select_Menu()
        {
            try
            {
                var dsMN = (from cd in db.MENUs
                            select new
                            {
                                cd.Id,
                                cd.MenuName,
                                cd.MenuLink,
                                cd.ParentId,
                                cd.OrderNumber
                            }

                            ).ToList();

                return Json(new { code = 1, cd = dsMN, msg = "Lấy thành công á :3" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { code = 0, msg = "Lấy danh hổng được, hình như có lỗi á :3" + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpGet]
        public JsonResult Select_Menu_By_ID(int ID)
        {
            try
            {
                var dsMN = (from cd in db.MENUs
                            where (cd.Id == ID)
                            select new
                            {
                                cd.Id,
                                cd.MenuName,
                                cd.MenuLink,
                                cd.ParentId,
                                cd.OrderNumber
                            }

                            ).SingleOrDefault();

                return Json(new { code = 1, cd = dsMN, msg = "Lấy thành công á :3" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { code = 0, msg = "Lấy danh hổng được, hình như có lỗi á :3" + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpPost]
        public JsonResult Insert_Menu(string MenuName, string MenuLink, int ParentID, int OrderNumber)
        {
            try
            {
                var cd = new MENU();
                cd.MenuName = MenuName;
                cd.MenuLink = MenuLink;
                cd.ParentId = ParentID;
                cd.OrderNumber = OrderNumber;
                db.MENUs.InsertOnSubmit(cd);
                db.SubmitChanges();
                return Json(new { code = 1, msg = "Thêm thành công nha :3" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { code = 0, msg = "Thêm hổng được, hình như có lỗi á :3" + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

     

        [HttpPost]
        public JsonResult Update_Menu_By_ID(int ID, string MenuName, string MenuLink, int OrderNumber)
        {
            try
            {
                var cd = db.MENUs.SingleOrDefault(c => c.Id == ID);
                cd.MenuName = MenuName;
                cd.MenuLink = MenuLink;
                cd.OrderNumber = OrderNumber;
                db.SubmitChanges();
                return Json(new { code = 1, msg = "Sửa thành công nha :3" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { code = 0, msg = "Sửa hổng được, hình như có lỗi á :3" + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpPost]
        public JsonResult Update_MenuCon_Sang_MenuCha(int ID)
        {
            try
            {
                var cd = db.MENUs.SingleOrDefault(c => c.Id == ID);
                cd.ParentId = null;
                db.SubmitChanges();
                return Json(new { code = 1, msg = "Sửa thành công nha :3" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { code = 0, msg = "Sửa hổng được, hình như có lỗi á :3" + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult Update_MenuCha_Sang_MenuCon(int ID, int ParentID)
        {
            if (ID == ParentID)
            {
                return Json(new { code = 0, msg = "Sửa hổng được, hình như có lỗi á :3" }, JsonRequestBehavior.AllowGet);
            }
            try
            {
                var cd = db.MENUs.SingleOrDefault(c => c.Id == ID);
                cd.ParentId = ParentID;
                db.SubmitChanges();
                return Json(new { code = 1, msg = "Sửa thành công nha :3" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { code = 0, msg = "Sửa hổng được, hình như có lỗi á :3" + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Details(int ID)
        {
            //var kq = from s in db.MENUs where s.ParentID == ID select s;
            //if (kq.Count() <= 0)
            //{
            //return RedirectToAction("Index", "Menu");
            //}
            return View();


        }

    }
}