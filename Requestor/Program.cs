using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Requestor
{
    class Program
    {
        static void Main(string[] args)
        {
            ConnectionFactory factory = new ConnectionFactory();
            factory.HostName = "localhost";
            factory.VirtualHost = "/";
            factory.Port = 5673;
            factory.UserName = "admin";
            factory.Password = "123";

            IConnection conn = factory.CreateConnection();
            IModel channel = conn.CreateModel();

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (sender, e) =>
            {
                string message = System.Text.Encoding.UTF8.GetString(e.Body.ToArray());
                Console.WriteLine("Response received:" + message);
            };

            channel.BasicConsume("responses", true, consumer);

            while (true)
            {
                Console.Write("Enter your request:");
                string request = Console.ReadLine();

                if (request == "exit")
                    break;

                channel.BasicPublish("", "requests", null, Encoding.UTF8.GetBytes(request));
            }

            channel.Close();
            conn.Close();
        }
    }
}
