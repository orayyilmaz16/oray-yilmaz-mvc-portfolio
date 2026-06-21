using Microsoft.AspNetCore.Mvc;
using OrayPortfolio.Web.Models;
using System.Net;
using System.Net.Mail;

namespace OrayPortfolio.Web.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View(new ContactViewModel());
        }

        [HttpPost]
        public IActionResult Index(ContactViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                var mail = new MailMessage();
                mail.From = new MailAddress("orayyilmaz16@gmail.com", model.FullName);
                mail.To.Add("orayyilmaz16@gmail.com");
                mail.Subject = model.Subject;
                mail.Body =
                    $"Gönderen: {model.FullName}\n" +
                    $"Email: {model.Email}\n\n" +
                    $"Mesaj:\n{model.Message}";
                mail.IsBodyHtml = false;

                var smtp = new SmtpClient("smtp.gmail.com", 587)
                {
                    Credentials = new NetworkCredential("orayyilmaz16@gmail.com", "ijrglpesamcbcrmi"),
                    EnableSsl = true
                };

                smtp.Send(mail);

                TempData["Success"] = "Mesajın başarıyla gönderildi!";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Mesaj gönderilirken bir hata oluştu.";
                return View(model);
            }

        }
    }
}
