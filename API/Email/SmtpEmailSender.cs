using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace API.Email {
    public class SmtpEmailSender : IEmailSender {

        public int port;
        public string host;
        public string from;
        public string password;

        public SmtpEmailSender(int port, string host, string from, string password) {
            this.port = port;
            this.host = host;
            this.from = from;
            this.password = password;
        }

        public string ToEmail { get; set; }
        public string Body { get; set; }
        public string Subject { get ; set; }

        public void Send() {
            var smtp = new SmtpClient {
                Host = host,
                Port = port,
                EnableSsl = true,
                UseDefaultCredentials = false,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential(from, password)
            };

            using(var msg = new MailMessage(from, ToEmail) {
                 Subject = Subject,
                  Body = Body
            }) {
                smtp.Send(msg);
            }

        }
    }
}
