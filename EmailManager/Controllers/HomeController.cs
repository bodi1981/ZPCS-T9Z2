using EmailManager.Models;
using EmailManager.Models.Domains;
using EmailManager.Models.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace EmailManager.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private EmailSender.Email _emailSender;

        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var emails = EmailRepository.GetEmails(userId);

            return View(emails);
        }

        public ActionResult Sent(int emailId = 0)
        {
            var userId = User.Identity.GetUserId();
            var email = (emailId == 0) ? GetNewEmail(userId) : EmailRepository.GetEmail(emailId, userId);

            var evm = GetEmailViewModel(email);
            
            return View(evm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Sent(EmailViewModel evm)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            _emailSender = new EmailSender.Email(new EmailSender.EmailParams
            {
                HostSmtp = ConfigurationManager.AppSettings["HostSmtp"],
                Port = Convert.ToInt32(ConfigurationManager.AppSettings["Port"]),
                EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSsl"]),
                SenderName = evm.Email.Sender,
                SenderEmail = ConfigurationManager.AppSettings["SenderEmail"],
                SenderEmailPassword = ConfigurationManager.AppSettings["SenderEmailPassword"]
            });

            await _emailSender.Send(evm.Email.Subject, evm.Email.Message, evm.Email.Reciepient);

            EmailRepository.AddEmail(evm.Email);

            return RedirectToAction("Index");
        }

        private Email GetNewEmail(string userId)
        {
            return new Email { UserId = userId };
        }

        private EmailViewModel GetEmailViewModel(Email email)
        {
            return new EmailViewModel
            {
                Email = email,
                Header = (email.Id == 0) ? "Tworzenie wiadomości" : "Podgląd wiadomości"
            };
        }
        
        public ActionResult RemoveEmail(int emailId)
        {
            try
            {
                var userId = User.Identity.GetUserId();
                EmailRepository.RemoveEmail(emailId, userId);
            }
            catch (Exception ex)
            {
                return Json(new { Success = false, Message = ex.Message });
            }

            return Json(new { Success = true });
        }

        [AllowAnonymous]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [AllowAnonymous]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}