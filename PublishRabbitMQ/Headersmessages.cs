using RabbitMQ.Client;
using System.Text;

namespace PublishRabbitMQ
{
    public class Headersmessages
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
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("format", "pdf");
            properties.Headers = dictionary;
            byte[] messagebuffer = Encoding.Default.GetBytes("Message to Headers Exchange 'format=pdf' ");
            model.BasicPublish("headers.exchange", "", properties, messagebuffer);
            Console.WriteLine("Message Sent From : headers.exchange ");
            Console.WriteLine("Routing Key : Does not need routing key");
            Console.WriteLine("Message Sent");
        }
    }
}
