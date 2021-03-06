﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ComputerWebShop.Models;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using ComputerWebShop.Helper;

namespace ComputerWebShop.Controllers
{
    public class ContactUsController : BaseController
    {
        // GET: ContactUs
        public ActionResult ContactUs()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ContactUs(ContactUs c)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    MailMessage msg = new MailMessage();
                    SmtpClient smtp = new SmtpClient();
                    MailAddress from = new MailAddress(c.Email.ToString());
                    StringBuilder sb = new StringBuilder();
                    msg.To.Add("lemiszewski@gmx.com");
                    msg.Subject = "Contact Us";
                    msg.IsBodyHtml = true;
                    smtp.Host = "mail.gmx.com";
                    smtp.Port = 587;
                    smtp.EnableSsl = true;

                    /*sb.Append("First name: " + c.FirstName);
                    sb.Append(Environment.NewLine);
                    sb.Append("Last name: " + c.LastName);
                    sb.Append(Environment.NewLine);
                    sb.Append("Email: " + c.Email);
                    sb.Append(Environment.NewLine);
                    sb.Append("Message: " + c.Comment);*/
                    /*msg.Body = sb.ToString();*/

                    var body = "<h1>Message from Contact Us form</h1>";
                    body += "<h2>Customer</h2>";
                    body += "<p>" + c.FirstName + " " + c.LastName + "</p>";
                    body += "<h2>Email</h2>";
                    body += "<p>" + c.Email + " </p>";
                    body += "<h2>Message</h2>";
                    body += "<p>"+ c.Comment +"</p>";
                    msg.Body = body; 
                    smtp.Send(msg);
                    msg.Dispose();
                    return View("Success");
                }
                catch (Exception)
                {
                    return View("Error");
                }
            }
            return View();
        }

    }
}