using IP2Email.Helpers;
using IP2Email.Interfaces;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;

namespace IP2Email.Classes
{
    internal class SendMail : IArgsAction
    {
<<<<<<< HEAD
        private void SendByEmail(AppConfig config, string ip)
=======
        private void SendEmail(AppConfig config, string ip)
>>>>>>> 6c0a5d048f41f68a5de63dee15f0d81e87e6ad0f
        {
            MailMessage message = new MailMessage(from: new MailAddress(config.SenderEmail, TextHelper.SenderDisplayName),
                                                    to: new MailAddress(config.RecipientEmail))
            {
                Subject = TextHelper.EmailSubject,
<<<<<<< HEAD
                Body = $"{TextHelper.EmailBody}: {ip}",
=======
                Body = $"{TextHelper.EmailBody}: {ip}. {config.EmailBody}",
>>>>>>> 6c0a5d048f41f68a5de63dee15f0d81e87e6ad0f
                IsBodyHtml = false
            };

            SmtpClient smtp = new SmtpClient(config.EmailServer, Convert.ToInt16(config.EmailServerPort))
            {
                Credentials = new NetworkCredential(config.SenderEmail, config.SenderPassword),
                EnableSsl = true
            };

            smtp.Send(message);
        }

        public void Do(string internetIP, List<string> localIPs, ref AppExitCodes appExitCode, AppConfig appConfig)
        {
            ConsoleHelper.ShowAppBanner();

            if (appConfig.IsConfigured)
            {
                try
                {
<<<<<<< HEAD
                    SendByEmail(appConfig, internetIP);
=======
                    SendEmail(appConfig, internetIP);
>>>>>>> 6c0a5d048f41f68a5de63dee15f0d81e87e6ad0f
                    ConsoleHelper.EmailSend(senderEmail: appConfig.SenderEmail, recipientEmail: appConfig.RecipientEmail);
                }
                catch (Exception ex)
                {
                    ConsoleHelper.EmailSendException(exception: ex, senderEmail: appConfig.SenderEmail, recipientEmail: appConfig.RecipientEmail);
                    appExitCode = AppExitCodes.EmailSendException;
                }
            }
            else
            {
                ConsoleHelper.NotSetMailData();
                appExitCode = AppExitCodes.SetMailDataException;
            }
        }
    }
}