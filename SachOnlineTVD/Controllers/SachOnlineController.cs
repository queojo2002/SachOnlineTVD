﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SachOnlineTVD.Models;
using PagedList;
using PagedList.Mvc;
namespace SachOnlineTVD.Controllers
{
    public class SachOnlineController : Controller
    {
        dbSachOnlineDataContext data = new dbSachOnlineDataContext();

        private List<SACH> LaySachMoi(int count)
        {
            return data.SACHes.OrderByDescending(a => a.NgayCapNhat).Take(count).ToList();
        }


        private List<SACH> SachBanNhieu(int count)
        {
            return data.SACHes.OrderByDescending(a => a.SoLuongBan).Take(count).ToList();
        }


        // GET: SachOnline
        public ActionResult Index(int? page)
        {
            int iSize = 6;
            int iPageNum = (page ?? 1);
            var listsachmoi = LaySachMoi(20);
            return View(listsachmoi.ToPagedList(iPageNum, iSize));
        }



        /*
        public ActionResult BookByTopic(int id)
        {
            var kq = from s in data.SACHes where s.MaCD == id select s;
            return View(kq);
        }
        */

        public ActionResult BookByTopic(int iMaCD, int? page)
        {
            ViewBag.MaCD = iMaCD;
            int iSize = 3;
            int iPageNum = (page ?? 1);
            var sach = from s in data.SACHes where s.MaCD == iMaCD select s;
            return View(sach.ToPagedList(iPageNum, iSize));
        }


        /*
        public ActionResult BookByNXB(int id)
        {
            var kq = from s in data.SACHes where s.MaNXB == id select s;
            return View(kq);
        }
        */

        public ActionResult BookByNXB(int iMaNXB, int? page)
        {
            ViewBag.MaNXB = iMaNXB;
            int iSize = 3;
            int iPageNum = (page ?? 1);
            var sach = from s in data.SACHes where s.MaNXB == iMaNXB select s;
            return View(sach.ToPagedList(iPageNum, iSize));
        }



        public ActionResult BookDetail(int id)
        {
            var kq = from s in data.SACHes where s.MaSach == id select s;
            return View(kq.Single());
        }


        public ActionResult SachBanNhieuPartial()
        {
            var listsachbn = SachBanNhieu(6);
            return PartialView(listsachbn);
        }

        public ActionResult NavPartial()
        {
            return PartialView();
        }

        public ActionResult SliderPartial()
        {
            return PartialView();
        }

        public ActionResult ChuDePartial()
        {
            var listChuDe = from cd in data.CHUDEs select cd;
            return PartialView(listChuDe);
        }

        public ActionResult NhaXuatBanPartial()
        {
            var listNXB = from cd in data.NHAXUATBANs select cd;
            return PartialView(listNXB);
        }

        public ActionResult LoginLogout()
        {
            return PartialView("LoginLogoutPartial");
        }



    }
}