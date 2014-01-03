using System;
using System.Configuration;
using System.Linq;
using System.Net.Configuration;
using System.Net.Mail;
using System.Runtime.CompilerServices;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Threading;
using Castle.Services.Transaction;
using Microsoft.Practices.ServiceLocation;
using NHibernate;
using PocketMoney.Data;
using PocketMoney.Model;
using PocketMoney.Model.External.Requests;
using PocketMoney.Model.Internal;
using PocketMoney.Service.Behaviors;
using PocketMoney.Service.Interfaces;
using PocketMoney.Util;
using PocketMoney.Util.ExtensionMethods;

namespace PocketMoney.Service
{
    [Transactional]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class MessageService : BaseService, IMessageService
    {
        private readonly IRepository<Email, EmailId, Guid> _emailRepository;
        private readonly IRepository<EmailMessage, EmailMessageId, Guid> _emailMessageRepository;

        public MessageService(
            IRepository<User, UserId, Guid> userRepository,
            IRepository<Family, FamilyId, Guid> familyRepository,
            IRepository<Email, EmailId, Guid> emailRepository,
            IRepository<EmailMessage, EmailMessageId, Guid> emailMessageRepository)
            : base(userRepository, familyRepository)
        {
            _emailRepository = emailRepository;
            _emailMessageRepository = emailMessageRepository;
        }


        [MethodImpl(MethodImplOptions.Synchronized)]
        [OperationBehavior(TransactionScopeRequired = true)]
        public virtual Result SendEmail(EmailMessageRequest model)
        {
            var result = this.SaveEmailMessage(model);

            if (result.Success)
            {
               ThreadPool.QueueUserWorkItem(c => { MessageService.StartEmailQueue(); });
            }
            return result;
        }

        [Process, Transaction(TransactionMode.Requires)]
        public virtual Result SaveEmailMessage(EmailMessageRequest model)
        {
            //return this.Process<EmailMessageRequest, Result>(
            //    ref model,
            //    () => new Result(),
            //    (ref Result result) =>
            //    {
            var member = model.Sender.To();
            var email = _emailRepository.FindOne(x => x.Address == model.Email);
            if (email == null)
                throw new InvalidDataException("Email address not found");

            var message = new EmailMessage(email, member, model.Subject, model.Text);

            if (model.Attachment != null)
            {
                var file = model.Attachment.To();

                message.Attachment = file;
            }
            _emailMessageRepository.Add(message);

            return new Result();
            //});
        }

        public static void StartEmailQueue()
        {
            try
            {
                var queueRepository = ServiceLocator.Current.GetInstance<IRepository<EmailMessage, EmailMessageId, Guid>>();
                foreach (var email in queueRepository.FindAll(x => x.Processed == false)
                                                    .OrderBy(x => x.CreatedOn))
                {
                    bool success = false;
                    if (email.Attachment != null)
                        success = SendMail(email.Receiver.Address, email.Subject, email.Body, email.Attachment.FileNameWithExtension, email.Attachment.Content);
                    else
                        success = SendMail(email.Receiver.Address, email.Subject, email.Body);

                    email.Processed = true;
                    email.Success = success;
                    if (success)
                    {
                        email.SentOn = Clock.UtcNow();
                    }
                    ISession session = (ISession)queueRepository.GetSession();
                    var transaction = session.BeginTransaction();
                    session.SaveOrUpdate(email);
                    transaction.Commit();
                }
            }
            catch (Exception ex)
            {
                ex.LogError();
            }
        }

        #region SMTP Methods

        public static bool SendMail(string email, string subject, string body)
        {
            try
            {
                SmtpSection config = ConfigurationManager.GetSection("system.net/mailSettings/smtp") as SmtpSection;
                if (config != null && !string.IsNullOrWhiteSpace(config.From))
                {
                    using (SmtpClient mailClient = new SmtpClient())
                    {
                        using (MailMessage newMessage = new MailMessage(config.From, email, subject, body))
                        {
                            mailClient.Send(newMessage);
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                ex.LogError();
                return false;
            }
        }

        public static bool SendMail(string email, string subject, string body, string fileName, byte[] fileContent)
        {
            try
            {
                SmtpSection config = ConfigurationManager.GetSection("system.net/mailSettings/smtp") as SmtpSection;
                if (config != null && !string.IsNullOrWhiteSpace(config.From))
                {
                    using (SmtpClient mailClient = new SmtpClient())
                    {
                        using (MailMessage newMessage = new MailMessage(config.From, email, subject, body))
                        {
                            using (System.IO.MemoryStream mem = new System.IO.MemoryStream(fileContent))
                            {
                                newMessage.Attachments.Add(new Attachment(mem, fileName));
                                mailClient.Send(newMessage);
                            }
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                ex.LogError();
                return false;
            }
        }
        #endregion


    }
}
