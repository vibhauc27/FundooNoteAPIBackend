using Experimental.System.Messaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Modal
{
    public class MSMQmodal
    {
        MessageQueue messageQueue = new MessageQueue();

        private void sendData2Queue()
        {
            messageQueue.Path = @".\private$\Token";

            messageQueue.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
            messageQueue.ReceiveCompleted += billingQ _ReceiveCompleted;
            messageQueue.Send("Desired Messages");
            messageQueue.BeginReceive();
            messageQueue.Close();
        }

    }
}
