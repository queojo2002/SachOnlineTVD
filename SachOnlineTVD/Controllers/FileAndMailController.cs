using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SachOnlineTVD.Models;
using System.Net.Mail;
using System.Net;
using System.Net.Mime;
namespace SachOnlineTVD.Controllers
{
    public class FileAndMailController : Controller
    {
        // GET: FileAndMail
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult SendMail()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SendMail(Mail model)
        {
            var mail = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential("2024802010403@student.tdmu.edu.vn", "01656461203DucDz11012002"),
                EnableSsl = true
            };
            var mess = new MailMessage();
            mess.From = new MailAddress(model.From);
            mess.ReplyToList.Add(model.From);
            mess.To.Add(new MailAddress(model.To));
            mess.Subject = model.Subject;
            mess.Body = model.Notes;
            var f = Request.Files["attachment"];
            var path = Path.Combine(Server.MapPath("~/UploadFile"), f.FileName);
            if (!System.IO.File.Exists(path))
            {
                f.SaveAs(path);
            }
            Attachment data = new Attachment(Server.MapPath("~/UploadFile/" + f.FileName), MediaTypeNames.Application.Octet);
            mess.Attachments.Add(data);
            mail.Send(mess);
            return View("SendMail");
        }
    }
}
