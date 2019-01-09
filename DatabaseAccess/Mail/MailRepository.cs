using System;
using System.Collections.Generic;
using System.IO;
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
            SmtpServer.Credentials = new NetworkCredential("citeutoulouse@gmail.com", "CiteU1234");
            SmtpServer.EnableSsl = true;
            SmtpServer.Send(mail);
        }

        public void SendEmailInscription(string mailUser)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                mail.From = new MailAddress("citeutoulouse@gmail.com");
                mail.To.Add(mailUser);
                mail.Subject = "Inscription au site Cité U online";
                string currentDirectory = Directory.GetCurrentDirectory();
                string filePath = Path.Combine(currentDirectory, "mailInscription.html");
                string leBody = File.ReadAllText("mailInscription.html"); ;
                leBody = leBody.Replace("{mailUser}", mailUser);
                mail.Body = leBody;
                mail.IsBodyHtml = true;

                SmtpServer.Port = 587;
                SmtpServer.UseDefaultCredentials = true;
                SmtpServer.Credentials = new NetworkCredential("citeutoulouse@gmail.com", "CiteU1234");
                SmtpServer.EnableSsl = true;
                SmtpServer.Send(mail);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
