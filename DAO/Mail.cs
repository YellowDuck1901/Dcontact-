using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Threading;

namespace Util
{

    public class Mail
    {
        private string from = "Dteam.FPT@gmail.com";
        private string pass = "Dteam123@";
        static public bool sendThread(string to, string sub, string cont)
        {
            Mail mail = new Mail();
            Thread t = new Thread(
               x => Mail.send(to, sub, cont)
                );
            t.Start();
            return true;
        }

        public static bool send(string to, string sub, string cont) => new Mail().sendMail(to, sub, cont);

        bool sendMail(string to, string supject, string content)
        {
            MailMessage message = new MailMessage(this.from, to);
            message.Subject = supject;
            message.Body = content;
            message.BodyEncoding = System.Text.Encoding.UTF8;
            message.IsBodyHtml = true;
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587); //Gmail smtp    
            System.Net.NetworkCredential basicCredential1 = new
            System.Net.NetworkCredential(this.from, this.pass);
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = basicCredential1;
            try
            {
                client.Send(message);
            }
            catch (Exception ex)
            {
                throw;
            }
            return true;
        }
    }
}
