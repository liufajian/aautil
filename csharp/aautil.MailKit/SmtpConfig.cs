namespace aautil.MailKit
{
    /// <summary>
    /// stmp服务器配置
    /// </summary>
    public class SmtpConfig
    {
        /// <summary>
        /// 获取或设置用于传送电子邮件的 SMTP 服务器的名称。
        /// </summary>
        public string Server { get; set; }

        /// <summary>
        /// 获取或设置用于 SMTP 事务的端口。
        /// </summary>
        public int Port { get; set; } = 25;

        /// <summary>
        /// 获取或设置用于发送电子邮件的电子邮件帐户名。
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 获取或设置发件人的电子邮件帐户的密码。
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string From { get; set; }

        /// <summary>
        /// 获取或设置一个值，该值指示在发送电子邮件时是否使用安全套接字层 (SSL) 来加密连接。
        /// </summary>
        public bool EnableSsl { get; set; }

        /// <summary>
        /// 是否压缩附件
        /// </summary>
        public bool ZipAttach { get; set; }

        /// <summary>
        /// 是否要求回执
        /// </summary>
        public bool Receipt { get; set; }
    }
}
