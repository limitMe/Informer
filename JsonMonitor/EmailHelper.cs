using System;
using System.Net;
using System.Net.Mail;
using System.Xml.Linq;

namespace JsonMonitor
{
    public class EmailHelper
    {
        private SmtpClient client;
        private String senderAddress;
        private String distinationAddress;

        public EmailHelper()
        {
            this.client = new SmtpClient("smtp.exmail.qq.com");
            this.client.UseDefaultCredentials = false;

            /*
             * XML structure
                <EmailAccount>
                    <Username>*</Username>
                    <Password>*</Password>
                    <Destination>*</Destination>
                </EmailAccount>
             */
            XElement accountSetting = XElement.Load(@"AccountSetting.xml");

            this.senderAddress = accountSetting.Element("Username").Value;
            this.distinationAddress = accountSetting.Element("Destination").Value;

            this.client.Credentials = new NetworkCredential(accountSetting.Element("Username").Value, 
                                                            accountSetting.Element("Password").Value);
        }

        internal void SendEmail(String title, String body){

            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(senderAddress);
            mailMessage.To.Add(this.distinationAddress);
            mailMessage.Body = title;
            mailMessage.Subject = body;
            client.Send(mailMessage);
        }
    }
}
