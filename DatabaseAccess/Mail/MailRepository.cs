using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace DatabaseAccess.Mail
{
    public class MailRepository : IMailRepository
    {
        public void SendEmailTest()
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            mail.From = new MailAddress("FROMXXXX@gmail.com");
            mail.To.Add("sio.bureauducolombier@gmail.com");
            mail.Subject = "testest";
            mail.Body = "mail with attachment";

           

            SmtpServer.Port = 587;
            SmtpServer.UseDefaultCredentials = true;
            SmtpServer.Credentials = new System.Net.NetworkCredential("citeutoulouse@gmail.com", "CiteU1234");
            SmtpServer.EnableSsl = true;
            SmtpServer.Send(mail);
        }
    }
}
