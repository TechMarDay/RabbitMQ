using RabbitMQ.Client;
using System.Text;

namespace PublishRabbitMQ
{
    public class Fanoutmessages
    {
        private const string UName = "admin";
        private const string PWD = "123";
        private const int Port = 5673;
        private const string HName = "localhost";

        public void SendMessage()
        {
            //Main entry point to the RabbitMQ .NET AMQP client
            var connectionFactory = new ConnectionFactory()
            {
                UserName = UName,
                Password = PWD,
                HostName = HName,
                Port = Port
            };

            var connection = connectionFactory.CreateConnection();
            var model = connection.CreateModel();
            var properties = model.CreateBasicProperties();
            properties.Persistent = false;
            byte[] messagebuffer = Encoding.Default.GetBytes("Message is of fanout Exchange type");
            model.BasicPublish("fanout.exchange", "", properties, messagebuffer);
            Console.WriteLine("Message Sent From : fanout.exchange");
            Console.WriteLine("Routing Key :  Routing key is not required for fanout exchange");
            Console.WriteLine("Message Sent");
        }

    }
}
