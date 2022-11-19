using RabbitMQ.Client;
using System.Text;

namespace PublishRabbitMQ
{
    public class Directmessages
    {
        private const string UName = "admin";
        private const string PWD = "123";
        private const int Port = 5673;
        private const string HName = "localhost";
        public void SendMessage(string message)
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

            byte[] messagebuffer = Encoding.Default.GetBytes(message);
            model.BasicPublish("demoExchange", "directexchange_key", properties, messagebuffer);
            Console.WriteLine($"Message Sent: {message}");
        }
    }
}
