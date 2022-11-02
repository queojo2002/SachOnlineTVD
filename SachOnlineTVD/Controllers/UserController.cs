using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SachOnlineTVD.Models;
using System.Web.Security;
using System.Security.Cryptography;
using System.Text;
namespace SachOnlineTVD.Controllers
{
    public class UserController : Controller
    {
        dbSachOnlineDataContext db = new dbSachOnlineDataContext();
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult DangKy()
        {
            return View();
        }

        [HttpGet]
        public ActionResult DangKy_Model()
        {
            return View();
        }


        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DangNhap(FormCollection collection)
        {
            var sTDN = collection["TenDN"];
            var sMK = collection["MatKhau"];
            var Remember_Me = collection["rememberMe"];
            int state = 1;
            if (Request.QueryString.Count > 0)
            {
                if (Request.QueryString["id"] != null)
                {
                    state = int.Parse(Request.QueryString["id"]);
                }
            }


            if (String.IsNullOrEmpty(sTDN))
            {
                ViewData["Err1"] = "Chưa nhập tên đăng nhập kìa.";
            }
            else if (String.IsNullOrEmpty(sMK))
            {
                ViewData["Err2"] = "Phải nhập mật khẩu.";
            }
            else
            {

                KHACHHANG kh = db.KHACHHANGs.SingleOrDefault(n => n.TaiKhoan == sTDN && n.MatKhau == sMK);
                if (kh != null)
                {
                    if (Remember_Me.ToString() == "true,false")
                    {
                        Response.Cookies["Username"].Value = sTDN;
                        Response.Cookies["Password"].Value = sMK;
                        Response.Cookies["Username"].Expires = DateTime.Now.AddDays(30);
                        Response.Cookies["Password"].Expires = DateTime.Now.AddDays(30);
                    }
                    else
                    {
                        Response.Cookies["Username"].Value = "";
                        Response.Cookies["Password"].Value = "";
                        Response.Cookies["Username"].Expires = DateTime.Now.AddDays(-32);
                        Response.Cookies["Password"].Expires = DateTime.Now.AddDays(-32);
                    }
                    Session["TaiKhoan"] = kh;
                    if (state == 1)
                    {
                        return RedirectToAction("Index", "SachOnline");
                    }
                    else
                    {
                        return RedirectToAction("DatHang", "GioHang");
                    }


                }
                else
                {
                    ViewBag.ThongBao = "Tên đăng nhập hoặc mật khẩu hông đúng.";
                }
            }


            return View();
        }


        [HttpPost]
        public ActionResult DangKy(FormCollection collection, KHACHHANG kh)
        {
            var sHoTen = collection["HoTen"];
            var sTk = collection["TenDN"];
            var sMk = collection["MatKhau"];
            var sMkNl = collection["MatKhauNL"];
            var sEmail = collection["Email"];
            var sDiaChi = collection["DiaChi"];
            var sDienThoai = collection["DienThoai"];
            var sNgaySinh = String.Format("{0:MM/dd/yyyy}", collection["NgaySinh"]);
            if (String.IsNullOrEmpty(sHoTen))
            {
                ViewData["err1"] = "Họ tên hông dược để rỗng";
            }
            else if (String.IsNullOrEmpty(sTk))
            {
                ViewData["err2"] = "Tên đăng nhập hông dược để rỗng";
            }
            else if (String.IsNullOrEmpty(sMk))
            {
                ViewData["err3"] = "Mật khẩu hông dược để rỗng";
            }
            else if (String.IsNullOrEmpty(sMkNl))
            {
                ViewData["err4"] = "Phải nhập lại mật khẩu";
            }
            else if (sMk != sMkNl)
            {
                ViewData["err4"] = "Mật khẩu nhập lại hổng đúng";
            }
            else if (String.IsNullOrEmpty(sEmail))
            {
                ViewData["err5"] = "Email hông dược để rỗng";
            }
            else if (String.IsNullOrEmpty(sDienThoai))
            {
                ViewData["err6"] = "SDT hông dược để rỗng";
            }
            else if (db.KHACHHANGs.SingleOrDefault(n => n.TaiKhoan == sTk) != null)
            {
                ViewBag.ThongBao = "Tài khoản đã được dùng òi";
            }
            else if (db.KHACHHANGs.SingleOrDefault(n => n.Email == sEmail) != null)
            {
                ViewBag.ThongBao = "Email đã được dùng òi";
            }
            else if (ModelState.IsValid)
            {
                kh.HoTen = sHoTen;
                kh.TaiKhoan = sTk;
                kh.MatKhau = sMk;
                kh.Email = sEmail;
                kh.DiaChi = sDiaChi;
                kh.DienThoai = sDienThoai;
                kh.NgaySinh = DateTime.Parse(sNgaySinh);
                db.KHACHHANGs.InsertOnSubmit(kh);
                db.SubmitChanges();
                return Redirect("~/User/DangNhap?id=1");
            }






            return this.DangKy();



        }



        [HttpPost]
        public ActionResult DangKy_Model(FormCollection collection, KHACHHANG kh)
        {
            var sHoTen = collection["HoTen"];
            var sTk = collection["TaiKhoan"];
            var sMk = collection["MatKhau"];
            var sMkNl = collection["MatKhauNL"];
            var sEmail = collection["Email"];
            var sDiaChi = collection["DiaChi"];
            var sDienThoai = collection["DienThoai"];
            var sNgaySinh = String.Format("{0:MM/dd/yyyy}", collection["NgaySinh"]);
            if (String.IsNullOrEmpty(sMkNl))
            {
                ViewData["err4"] = "Phải nhập lại mật khẩu";
            }
            else if (sMk != sMkNl)
            {
                ViewData["err4"] = "Mật khẩu nhập lại hổng đúng";
            }
            else if (db.KHACHHANGs.SingleOrDefault(n => n.TaiKhoan == sTk) != null)
            {
                ViewBag.ThongBao = "Tài khoản đã được dùng òi";
            }
            else if (db.KHACHHANGs.SingleOrDefault(n => n.Email == sEmail) != null)
            {
                ViewBag.ThongBao = "Email đã được dùng òi";
            }
            else if (ModelState.IsValid)
            {
                kh.HoTen = sHoTen;
                kh.TaiKhoan = sTk;
                kh.MatKhau = GetMD5(sMk);
                kh.Email = sEmail;
                kh.DiaChi = sDiaChi;
                kh.DienThoai = sDienThoai;
                kh.NgaySinh = DateTime.Parse(sNgaySinh);
                db.KHACHHANGs.InsertOnSubmit(kh);
                db.SubmitChanges();
                return Redirect("~/User/DangNhap?id=1");
            }
            return this.DangKy();
        }



        public ActionResult DangXuat()
        {
            FormsAuthentication.SignOut();
            //Session.Abandon();
            Session.Remove("TaiKhoan");
            return RedirectToAction("Index", "SachOnline");
        }


        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromdata = Encoding.UTF8.GetBytes(str);
            byte[] targetdata = md5.ComputeHash(fromdata);
            string byte2string = null;
            for (int i = 0; i < targetdata.Length; i++)
            {
                byte2string += targetdata[i].ToString("x2");
            }
            return byte2string;
        }
    }
}