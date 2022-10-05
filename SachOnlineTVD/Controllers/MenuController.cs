using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SachOnlineTVD.Models;
using PagedList;
using PagedList.Mvc;
namespace SachOnlineTVD.Controllers
{
    public class MenuController : Controller
    {
        dbSachOnlineDataContext data = new dbSachOnlineDataContext();

        // GET: Menu
        public ActionResult Index()
        {
            return View();
        }

 

    }
}