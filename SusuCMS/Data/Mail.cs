using System;

namespace SusuCMS.Data
{
    public class Mail : ISiteId
    {
        public int Id { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public string From { get; set; }

        public string FromName { get; set; }

        public string To { get; set; }

        public DateTime CreationTime { get; set; }

        public bool IsSent { get; set; }

        public string ReplyTo { get; set; }

        public int SiteId { get; set; }
    }
}
