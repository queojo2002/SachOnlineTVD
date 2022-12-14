using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SachOnlineTVD.Models;
using System.IO;


namespace SachOnlineTVD.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        dbSachOnlineDataContext data = new dbSachOnlineDataContext();

        // GET: Admin/Home
        public ActionResult Index()
        {
            if (Session["Admin"] == null || Session["Admin"].ToString() == "")
            {
                return Redirect("~/Admin/Home/Login");
            }
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            if (Session["Admin"] == null || Session["Admin"].ToString() == "")
            {
                return View();
            }
            else
            {
                return Redirect("~/Admin/Home/Index");
            }
        }



        [HttpPost]
        public ActionResult Login(FormCollection f)
        {

            var sTenDN = f["UserName"];
            var sMatKhau = f["Password"];
            ADMIN ad = data.ADMINs.SingleOrDefault(n => n.TenDN == sTenDN && n.MatKhau == sMatKhau);
            if (ad != null)
            {
                Session["Admin"] = ad;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.ThongBao = "Tài khoản hoặc mật khẩu khum chính xác!!";
            }
            return View();
        }
    }
}