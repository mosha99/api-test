using api_test.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace api_test.MyClass
{
    public class iD
    {
        public static string id;
    }

    public class Mail
    {
        public static string sender(mail model)
        {

            if (iD.id != model.body)
            {
                iD.id = model.body;
                try

                {

                    System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();

                    SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                    mail.From = new MailAddress("mosha.vstudio@gmail.com");

                    mail.To.Add(model.sendTo);

                    mail.Subject = model.head;

                    mail.Body = model.body;

                    SmtpServer.Port = 587;

                    SmtpServer.Credentials = new System.Net.NetworkCredential("mosha.vstudio@gmail.com", "wxvycnhpvsywzflb");

                    SmtpServer.EnableSsl = true;

                    SmtpServer.Send(mail);
                    return "true";


                }

                catch (Exception ex)

                {
                    return ex.ToString();
                }

            }
            return "ft";

        }
    }
}