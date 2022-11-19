using RabbitMQ.Client;
using System;
using System.Text;

namespace RabbitMQNetCore
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                Port = 5673,
                UserName = "admin",
                Password = "123",
                VirtualHost = "/"
            };
            try
            {
                var connection = factory.CreateConnection();
                var model = connection.CreateModel();

                var properties = model.CreateBasicProperties();
                properties.Persistent = false;

                byte[] messagebuffer = Encoding.Default.GetBytes("Direct Message");
                model.BasicPublish("demoExchange", "directexchange_key", properties, messagebuffer);
                Console.WriteLine("Message Sent");

                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            Console.ReadLine();
        }
    }
}