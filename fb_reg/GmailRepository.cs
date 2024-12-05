using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;

using MailKit.Net.Imap;
using MailKit;
using MailKit.Search;
using System.Net;
using ActiveUp.Net.Mail;
using System.Linq;

namespace fb_reg
{
    public class GmailRepository
    {
        private Imap4Client client;

        public GmailRepository(string mailServer, int port, bool ssl, string login, string password)
        {
            if (ssl)
                Client.ConnectSsl(mailServer, port);
            else
                Client.Connect(mailServer, port);
            Client.Login(login, password);
        }

        public IEnumerable<Message> GetAllMails(string mailBox)
        {
            return GetMails(mailBox, "ALL").Cast<Message>();
        }

        public IEnumerable<Message> GetUnreadMails(string mailBox)
        {
            return GetMails(mailBox, "UNSEEN").Cast<Message>(); //test
        }

        protected Imap4Client Client
        {
            get { return client ?? (client = new Imap4Client()); }
        }

        private MessageCollection GetMails(string mailBox, string searchPhrase)
        {
            Mailbox mails = Client.SelectMailbox(mailBox);
            MessageCollection messages = mails.SearchParse(searchPhrase);
            return messages;
        }
    }
}
