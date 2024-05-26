using DoctorAppointmentSystem.Entity;
using DoctorAppointmentSystem.WebUI.Models;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MimeKit;

namespace DoctorAppointmentSystem.WebUI.Controllers
{
    public class MailController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public MailController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        
        [HttpGet]
        public async Task<IActionResult> Index(MailRequest mailRequest)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) 
            {
                return NotFound();
            }

            MimeMessage mimeMessage = new MimeMessage();

            MailboxAddress mailboxAddressFrom = new MailboxAddress("Admin","anilalkis89@gmail.com");
            mimeMessage.From.Add(mailboxAddressFrom);

            MailboxAddress mailboxAddressTo = new MailboxAddress(user.FullName,user.Email);

            mimeMessage.To.Add(mailboxAddressTo);



            var bodyBuilder = new BodyBuilder();
            bodyBuilder.TextBody = mailRequest.Body;
            mimeMessage.Body = bodyBuilder.ToMessageBody();

            mimeMessage.Subject = mailRequest.Subject;

            SmtpClient client = new SmtpClient();
            client.Connect("smtp.gmail.com", 587, false);
            client.Authenticate(mailRequest.SenderMail, "oqlnzpqinevkiohl\r\n");
            client.Send(mimeMessage);
            client.Disconnect(true);


            return RedirectToAction("Index","Patient");
        }
    }
}
