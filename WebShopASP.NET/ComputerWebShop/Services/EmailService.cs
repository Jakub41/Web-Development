using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;

namespace ComputerWebShop.Services
{
    public class EmailService
    {

        public string CreateConfirmationToken()
        {
            Guid g = Guid.NewGuid();
            return g.ToString();

        }


        public void SendEmailConfirmation(string to, string username, string confirmationToken)
        {
            MailMessage m = new MailMessage();
            m.To.Add(to);
            m.From = new MailAddress("lemiszewski@gmx.com");
            m.Subject = "Confirm Your Account";
            string link = "http://localhost:2818/Account/RegisterConfirmation/" + confirmationToken + "";
            m.Body = "<a href=" + link + ">Click Here to Confirm</a>";
            m.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();


            smtp.EnableSsl = true;

            smtp.Send(m);

        }
        public void SendEmailConfirmation(string to, string confirmationToken)
        {
            MailMessage m = new MailMessage();
            m.To.Add(to);
            m.From = new MailAddress("lemiszewski@gmx.com");
            m.Subject = "Reset your Password";
            string link = "http://localhost:2818/Account/ChangePassword/" + confirmationToken + "";
            m.Body = "<a href=" + link + ">Click here to reset </a>";
            m.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();


            smtp.EnableSsl = true;

            smtp.Send(m);

        }
        public void SendEmailConfirmation(string to)
        {
            MailMessage m = new MailMessage();
            m.To.Add(to);
            m.From = new MailAddress("lemiszewski@gmx.com");
            m.Subject = "Confirm Your Account";
            // string link = "http://localhost:2818/Account/RegisterConfirmation/" + confirmationToken + "";
            m.Body = "Your order has been successfully confirmed thank you for buying at computer webshop";
            m.Body = "Please discover some more offers on our shop online";
            m.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();


            smtp.EnableSsl = true;

            smtp.Send(m);

        }
    }
}