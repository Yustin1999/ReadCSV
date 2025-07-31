using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMT.Emailer
{
    using System;
    using System.Net;
    using System.Net.Mail;

public class Emailer
{
        public static void SendEmail(string message, string path)
    {
        try
        {
            string password = File.ReadAllText("C:\\Users\\Justi\\source\\repos\\Yustin1999\\ReadCSV\\ReadCSV\\password.txt").Trim();
                var smtpClient = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential("justinsummers124@gmail.com", password),
                EnableSsl = true,
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress("justinsummers123@gmail.com"),
                Subject = "WMT Morning Checks",
                Body = message,
                IsBodyHtml = true,
                
            };
            mailMessage.Attachments.Add(new Attachment(path));
            mailMessage.To.Add("justin@newparksolutions.com");

            smtpClient.Send(mailMessage);
            Console.WriteLine("Email sent successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error sending email: " + ex.Message);
        }
    }
}
}
