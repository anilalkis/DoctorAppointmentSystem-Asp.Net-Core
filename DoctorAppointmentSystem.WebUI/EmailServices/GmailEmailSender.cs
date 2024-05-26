using Azure.Core;
using DoctorAppointmentSystem.WebUI.Models;
using MailKit.Net.Smtp;
using MimeKit;

namespace DoctorAppointmentSystem.WebUI.EmailServices
{
    public class GmailEmailSender : IEmailService
    {
       
        public void Execute(string email, string subject, string message)
        {
            MimeMessage mimeMessage = new MimeMessage();

            MailboxAddress mailboxAddressFrom = new MailboxAddress("Medisen", "anilalkis89@gmail.com");
            mimeMessage.From.Add(mailboxAddressFrom);

            MailboxAddress mailboxAddressTo = new MailboxAddress("user", email);

            mimeMessage.To.Add(mailboxAddressTo);

            var bodyBuilder = new BodyBuilder();
            bodyBuilder.TextBody = message;
            mimeMessage.Body = bodyBuilder.ToMessageBody();

            mimeMessage.Subject = subject;

            SmtpClient client = new SmtpClient();
            client.Connect("smtp.gmail.com", 587, false);
            client.Authenticate("anilalkis89@gmail.com", "oqlnzpqinevkiohl");
            client.Send(mimeMessage);
            client.Disconnect(true);
        }

    }
}
