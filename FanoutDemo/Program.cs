using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace FanoutConsumer
{
    class Program
    {
        static IConnection conn;
        static IModel channel;

        static void Main(string[] args)
        {
            ConnectionFactory factory = new ConnectionFactory();
            factory.HostName = "localhost";
            factory.VirtualHost = "/";
            factory.Port = 5673;
            factory.UserName = "admin";
            factory.Password = "123";

            conn = factory.CreateConnection();
            channel = conn.CreateModel();

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += Consumer_Received;

            var consumerTag = channel.BasicConsume("my.queue2", false, consumer);

            Console.WriteLine("Waiting for messages. Press any key to exit.");
            Console.ReadKey();

        }

        private static void Consumer_Received(object sender, BasicDeliverEventArgs e)
        {
            string message = Encoding.UTF8.GetString(e.Body.ToArray());
            Console.WriteLine("Message:" + message);

            channel.BasicNack(e.DeliveryTag, false, false);
        }
    }
}
