using System;
using System.Net;
using System.Net.Mail;
using System.Text;
using SusuCMS.Helpers;
using SusuCMS.Data;

namespace SusuCMS.Services
{
    public class MailService : ServiceBase<DataContext>
    {
        public MailService(DataContext dataContext) : base(dataContext) { }

        public void AddMail(int siteId, MailSetting mailSetting, Mail mail, bool submit = true)
        {
            if (mailSetting == null)
            {
                throw new ArgumentNullException("mailSetting can not be null.");
            }

            mail.From = mailSetting.UserName;
            mail.FromName = mailSetting.UserName;
            mail.CreationTime = DateTime.Now;
            mail.SiteId = siteId;
            DataContext.Mails.Add(mail);

            SaveChanges(submit);
        }

        public void SendMail(MailSetting mailSetting, Mail mail)
        {
            try
            {
                if (mailSetting == null)
                {
                    throw new ArgumentNullException("mailSetting can not be null.");
                }

                var client = new SmtpClient(mailSetting.Host, mailSetting.Port);
                client.EnableSsl = mailSetting.EnableSsl;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(mailSetting.UserName, mailSetting.Password);
                client.DeliveryMethod = SmtpDeliveryMethod.Network;

                client.Send(GetMailMessage(mailSetting, mail));
                mail.IsSent = true;
            }
            catch (Exception ex)
            {
                LogHelper.AddSystemErrorLog(ex.ToString());
            }
        }

        private MailMessage GetMailMessage(MailSetting mailSetting, Mail mail)
        {
            var message = new MailMessage
            {
                IsBodyHtml = true,
                Subject = mail.Subject,
                Body = mail.Body,
                From = new MailAddress(mailSetting.UserName, mailSetting.UserName),
                SubjectEncoding = Encoding.UTF8,
                BodyEncoding = Encoding.UTF8
            };

            var toList = mail.To.Split(";".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            foreach (var item in toList)
            {
                message.To.Add(new MailAddress(item, item));
            }

            if (!String.IsNullOrEmpty(mail.ReplyTo))
            {
                message.ReplyToList.Add(new MailAddress(mail.ReplyTo, mail.ReplyTo));
            }

            return message;
        }
    }
}
