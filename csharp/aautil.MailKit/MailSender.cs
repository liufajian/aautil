using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using SecureSocketOptions = MailKit.Security.SecureSocketOptions;

namespace aautil.MailKit
{
    /// <summary>
    /// 邮件发送类
    /// </summary>
    public static class MailSender
    {
        /// <summary>
        /// 发送邮件
        /// </summary>
        public static void Send(SmtpConfig smtpConfig, MailSendingOption option)
        {
            if (smtpConfig == null)
            {
                throw new ArgumentNullException(nameof(smtpConfig));
            }

            if (string.IsNullOrWhiteSpace(smtpConfig.Server))
            {
                throw new ArgumentException("未设置SMTP Server");
            }

            if (string.IsNullOrWhiteSpace(smtpConfig.UserName))
            {
                throw new ArgumentException("未设置SMTP UserName");
            }

            if (option == null)
            {
                throw new ArgumentNullException(nameof(option));
            }

            if (string.IsNullOrWhiteSpace(option.MailTo))
            {
                throw new ArgumentException("未设置收件人");
            }

            if (string.IsNullOrWhiteSpace(option.Subject))
            {
                throw new ArgumentException("未设置邮件主题");
            }

            var from = option.From.TrimToNull() ?? smtpConfig.From.TrimToNull() ?? smtpConfig.UserName.TrimToNull() ?? throw new ArgumentNullException("未设置发件人");

            var separator = new[] { ',', ';' };

            var message = new MimeMessage();

            message.From.Add(new MailboxAddress(string.Empty, from));
            message.To.Add(new MailboxAddress(string.Empty, option.MailTo.Trim()));
            message.Subject = option.Subject;

            if (!string.IsNullOrWhiteSpace(option.CC))
            {
                var addrArr = option.CC.Split(separator, StringSplitOptions.RemoveEmptyEntries)
                    .Select(n => new MailboxAddress(string.Empty, n.Trim()));
                message.Cc.AddRange(addrArr);
            }

            if (!string.IsNullOrWhiteSpace(option.BCC))
            {
                var addrArr = option.BCC.Split(separator, StringSplitOptions.RemoveEmptyEntries)
                    .Select(n => new MailboxAddress(string.Empty, n.Trim()));
                message.Bcc.AddRange(addrArr);
            }

            if (option.AdditionalHeaders != null)
            {

            }

            if (!string.IsNullOrWhiteSpace(option.HeaderEncoding))
            {
                message.Headers.Add(HeaderId.Encoding, option.HeaderEncoding.Trim());
            }

            if (!string.IsNullOrWhiteSpace(option.Priority) &&
                Enum.TryParse(option.Priority.Trim(), out MessagePriority prri))
            {
                message.Priority = prri;
            }

            if (!string.IsNullOrWhiteSpace(option.ReplyTo))
            {
                message.ReplyTo.Add(new MailboxAddress(string.Empty, option.ReplyTo.Trim()));
            }

            var builder = new BodyBuilder();
            if (option.IsBodyHtml)
            {
                builder.HtmlBody = option.Body;
            }
            else
            {
                builder.TextBody = option.Body;
            }

            if (option.FilesToAttach != null && option.FilesToAttach.Any(n => !string.IsNullOrWhiteSpace(n)))
            {
                var attaches = option.FilesToAttach.Where(n => !string.IsNullOrWhiteSpace(n)).ToArray();

                foreach (var attach in attaches)
                {
                    builder.Attachments.Add(attach);
                }

                if (smtpConfig.ZipAttach)
                {
                    using (var zipStream = new MemoryStream())
                    {
                        using (var archive = new ZipArchive(zipStream, ZipArchiveMode.Update))
                        {
                            foreach (var attach in attaches)
                            {
                                var fileInfo = new FileInfo(attach);
                                var entry = archive.CreateEntry(fileInfo.Name);
                                using (var stream = entry.Open())
                                {
                                    var bytes = File.ReadAllBytes(fileInfo.FullName);
                                    stream.Write(bytes, 0, bytes.Length);
                                }
                            }
                        }
                        builder.Attachments.Add(option.Subject + ".zip", zipStream.ToArray());
                    }
                }
            }

            message.Body = builder.ToMessageBody();

            if (!string.IsNullOrWhiteSpace(option.ContentEncoding))
            {
                message.Body.Headers.Add(HeaderId.Encoding, option.ContentEncoding.Trim());
            }

            //以outlook名义发送邮件，不会被当作垃圾邮件 
            message.Headers.Add("X-Priority", "3");
            message.Headers.Add("X-MSMail-Priority", "Normal");
            message.Headers.Add("X-Mailer", "Microsoft Outlook Express 6.00.2900.2869");
            message.Headers.Add("X-MimeOLE", "Produced By Microsoft MimeOLE V6.00.2900.2869");
            message.Headers.Add("ReturnReceipt", "1");
            if (smtpConfig.Receipt)
            {
                message.Headers.Add("Disposition-Notification-To", from);
            }
            using (var client = new SmtpClient())
            {
                // For demo-purposes, accept all SSL certificates (in case the server supports STARTTLS)
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                client.Connect(smtpConfig.Server, smtpConfig.Port,
                    smtpConfig.EnableSsl ? SecureSocketOptions.SslOnConnect : SecureSocketOptions.None);

                // Note: since we don't have an OAuth2 token, disable
                // the XOAUTH2 authentication mechanism.
                client.AuthenticationMechanisms.Remove("XOAUTH2");

                if (!string.IsNullOrWhiteSpace(smtpConfig.Password))
                {
                    // Note: only needed if the SMTP server requires authentication
                    client.Authenticate(new Credentials(smtpConfig));
                }

                client.Send(message);
                client.Disconnect(true);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        static string TrimToNull(this string str)
        {
            return string.IsNullOrWhiteSpace(str) ? null : str.Trim();
        }

        class Credentials : ICredentials
        {
            readonly SmtpConfig _config;
            public Credentials(SmtpConfig config)
            {
                _config = config ?? throw new ArgumentNullException(nameof(config));
            }

            public NetworkCredential GetCredential(Uri uri, string authType)
            {
                return new NetworkCredential(_config.UserName, _config.Password);
            }
        }
    }
}
