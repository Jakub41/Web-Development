using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using ComputerWebShop.Models;
using ComputerWebShop.ViewModel;

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
            string body = "<h1>Hello</h1><stron> " + username.Trim() + "</strong>,";
            body += "<br /><br /><h2>Please click the following link to activate your account</h2>";
            body += "<br /><a href=" + link + " > Click Here to Confirm </a>" ;
            body += "<br /><br />Thank you!";
            m.Body = body;
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
            m.Body = "<a href=" + link + ">Click here to reset your password</a>";
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
            m.Subject = "Order Confirmation";
            var body = "<h1>Congratulation!</h1>";
            body += "<h2>Your order has been successfully confirmed thank you for buying at computer webshop</h2>";
            string link = "http://localhost:2818/home/";
            body += "<p>Your order will be shipped soon.</p>";
            body += "<p>Please check our offers at <a href=" + link + ">Web Shop</a>";
            m.Body = body;
            m.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();


            smtp.EnableSsl = true;

            smtp.Send(m);

        }
    }
}