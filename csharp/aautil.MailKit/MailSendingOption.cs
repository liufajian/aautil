using System.Collections.Generic;

namespace aautil.MailKit
{
    public class MailSendingOption
    {
        /// <summary>
        /// [必需]收件人
        /// </summary>
        public string MailTo { get; set; }
        /// <summary>
        /// [必需]邮件主题
        /// </summary>
        public string Subject { get; set; }
        /// <summary>
        /// 邮件内容
        /// </summary>
        public string Body { get; set; }
        /// <summary>
        /// 发件人
        /// </summary>
        public string From { get; set; }
        /// <summary>
        /// 抄送人(多个抄送人以逗号隔开)
        /// </summary>
        public string CC { get; set; }
        /// <summary>
        /// 密送
        /// </summary>
        public string BCC { get; set; }
        /// <summary>
        /// 回复地址
        /// </summary>
        public string ReplyTo { get; set; }
        /// <summary>
        /// 附件
        /// </summary>
        public IEnumerable<string> FilesToAttach { get; set; }
        /// <summary>
        /// 邮件内容是否html格式，默认true
        /// </summary>
        public bool IsBodyHtml { get; set; } = true;
        /// <summary>
        /// 额外的头部信息(暂不支持)
        /// </summary>
        public IEnumerable<string> AdditionalHeaders { get; set; }
        /// <summary>
        /// 邮件内容编码
        /// </summary>
        public string ContentEncoding { get; set; }
        /// <summary>
        /// 邮件报文头部编码
        /// </summary>
        public string HeaderEncoding { get; set; }
        /// <summary>
        /// 优先级
        /// </summary>
        public string Priority { get; set; }
    }
}
