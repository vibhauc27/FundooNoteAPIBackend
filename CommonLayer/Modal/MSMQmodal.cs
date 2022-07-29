using Experimental.System.Messaging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace CommonLayer.Modal
{
    public class MSMQmodal
    {
        MessageQueue messageQueue = new MessageQueue();

        public void sendData2Queue(string Token)
        {
            messageQueue.Path = @".\private$\Token";

            if(!MessageQueue.Exists(messageQueue.Path))
{
                //Exists
                MessageQueue.Create(messageQueue.Path);

            }
           

            messageQueue.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
            messageQueue.ReceiveCompleted += MessageQueue_ReceiveCompleted;
            messageQueue.Send(Token);
            messageQueue.BeginReceive();
            messageQueue.Close();
        }

        private void MessageQueue_ReceiveCompleted(object sender, ReceiveCompletedEventArgs e)
        {
            var msg = messageQueue.EndReceive(e.AsyncResult);
            string Token = msg.Body.ToString(); // Process the logic be sending the message //Restart the asynchronous receive operation.
            string subject = "Fundoo notes reset link";
            string body = Token;
            var SMTP = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("vibhaaraoo@gmail.com", "ohghdxzjllowedmi"),
                EnableSsl = true
            };

            SMTP.Send("vibhaaraoo@gmail.com", "vibhaaraoo@gmail.com", subject, body);
            messageQueue.BeginReceive();

        }
    }
}
