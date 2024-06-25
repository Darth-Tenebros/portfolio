using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using portfolio.Interfaces;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace portfolio.EmailService
{
    public class EmailService : IEmailService
    {
        public EmailService()
        {
            
        }
        public void Send(string from, string to, string subject, string message, string key)
        {
            var client = new SendGridClient(key);
            var from_addr = new EmailAddress(from, "Example User");
            var to_addr = new EmailAddress(to, "Example User");
            var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
            var email = MailHelper.CreateSingleEmail(from_addr, to_addr, subject, message, htmlContent);
            var res = client.SendEmailAsync(email);
            Console.WriteLine(res.ToString());
            Console.WriteLine(res.Status);
            Console.WriteLine("EMAIL SHOULD BE SENT!!!!!!");    
        }
    }
}