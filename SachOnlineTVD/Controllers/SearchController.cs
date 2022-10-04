using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SachOnlineTVD.Models;

namespace SachOnlineTVD.Controllers
{
    public class SearchController : Controller
    {
        dbSachOnlineDataContext db = new dbSachOnlineDataContext();
        // GET: Search
        public ActionResult Search(string strSearch)
        {
            ViewData["strSearch"] = strSearch;
            if (!string.IsNullOrEmpty(strSearch))
            {
                var kq = from s in db.SACHes where s.TenSach.Contains(strSearch) || s.MoTa.Contains(strSearch) select s;
                return View(kq);
            }
            return View();
        }
        public ActionResult ThongKe()
        {
            var kq = from s in db.SACHes
                     group s by s.MaCD into g
                     select new ReportInfo
                     {
                         Id = g.Key.ToString(),
                         Count = g.Count(),
                         Sum = g.Sum(n => n.SoLuongBan),
                         Max = g.Max(n => n.SoLuongBan),
                         Min = g.Min(n => n.SoLuongBan),
                         Avg = Convert.ToDecimal(g.Average(n => n.SoLuongBan))
                     };
            return View(kq);
        }
    }
}