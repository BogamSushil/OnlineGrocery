using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Mail;

namespace Data.Common
{
    public static class Utility
    {
        public static string[] Split(string value, params char[] separator)
        {
            var result = new List<string>();
            if (!string.IsNullOrEmpty(value))
            {
                var values = value.Split(separator);
                result.AddRange(values);
            }
            return result.ToArray();
        }
        public static string[] Split(string value)
        {
            return Split(value, ',');
        }

        public static string TrimString(string content)
        {
            if (!string.IsNullOrWhiteSpace(content))
            {
                return content.Trim();
            }
            return string.Empty;
        }

        #region "Random Number Generation"

        // Will be executed as if in static contructor
        private static Random _Random = new Random();
        private static object _RandomSyncLock = new object();

        /// <summary>
        /// Returns a non-negative random number.
        /// </summary>
        public static int GetRandom()
        {
            lock (_RandomSyncLock)
            {
                return _Random.Next();
            }
        }

        /// <summary>
        /// Returns a non-negative random number less than or equal to given
        /// upper bound.
        /// </summary>
        public static int GetRandom(int maxValue)
        {
            lock (_RandomSyncLock)
            {
                return _Random.Next(maxValue);
            }
        }

        #endregion

        public static string GetConfigKey(string key)
        {
            var val = ConfigurationManager.AppSettings[key];
            if (null != val)
            {
                return val;
            }
            return "";
        }

        #region "Email"

        public static void SendMail(string to, string cc, string bcc,
            string subject, string body, string attachmentFileName)
        {
            try
            {
                var mSmtpClient = new SmtpClient();
                string adminEmail = GetConfigKey("AdminMailAddress");
                // Prepare the mail message
                var mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(adminEmail);
                mailMessage.Subject = subject;
                AddAddresses(mailMessage.To, Utility.Split(to));
                if (!string.IsNullOrEmpty(cc))
                {
                    AddAddresses(mailMessage.CC, Utility.Split(cc));
                }
                if (!string.IsNullOrEmpty(bcc))
                {
                    AddAddresses(mailMessage.Bcc, Utility.Split(bcc));
                }
                mailMessage.Body = body;
                mailMessage.IsBodyHtml = true;
                if (!string.IsNullOrEmpty(attachmentFileName))
                {
                    mailMessage.Attachments.Add(new Attachment(attachmentFileName));
                }
                mSmtpClient.Send(mailMessage);
            }
            catch (Exception e)
            {
                //Logger.Error(e);
            }
        }

        private static void AddAddresses(MailAddressCollection addressCollection,
            string[] addresses)
        {
            if (null == addresses)
            {
                return;
            }
            for (int i = 0; i < addresses.Length; i++)
            {
                addressCollection.Add(addresses[i]);
            }
        }
        #endregion
    }
}
