using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PocketMoney.Data;
using PocketMoney.FileSystem;
using PocketMoney.Util;

namespace PocketMoney.Model.Internal
{
    public class EmailMessage : Entity<EmailMessage, EmailMessageId, Guid>
    {
        protected EmailMessage() { }

        public EmailMessage(Email receiver, User creator, string subject, string body, File attachment)
        {
            this.Receiver = receiver;
            this.Creator = creator;
            this.Subject = subject;
            this.Body = body;
            this.Success = false;
            this.Processed = false; 
            this.CreatedOn = Clock.UtcNow();
            this.SentOn = null;
            this.Attachment = attachment;
            this.Processed = false;
        }

        public EmailMessage(Email receiver, User creator, string subject, string body)
            : this(receiver, creator, subject, body, null)
        {

        }

        public virtual User Creator { get; set; }

        public virtual Email Receiver { get; set; }

        public virtual string Subject { get; set; }

        public virtual string Body { get; set; }

        public virtual bool Processed { get; set; }

        public virtual bool Success { get; set; }

        public virtual DateTime CreatedOn { get; set; }

        public virtual DateTime? SentOn { get; set; }

        public virtual File Attachment { get; set; }

    }


    public class EmailMessageId : GuidIdentity
    {
        public EmailMessageId()
            : base(Guid.Empty, typeof(EmailMessage))
        {
        }

        public EmailMessageId(string id)
            : base(id, typeof(EmailMessage))
        {
        }

        public EmailMessageId(Guid EmailMessageId)
            : base(EmailMessageId, typeof(EmailMessage))
        {
        }
    }

}
