using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;

namespace DefaultDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            IConnection conn;
            IModel channel;

            ConnectionFactory factory = new ConnectionFactory();
            factory.HostName = "localhost";
            factory.VirtualHost = "/";
            factory.Port = 5673;
            factory.UserName = "admin";
            factory.Password = "123";

            conn = factory.CreateConnection();
            channel = conn.CreateModel();

            channel.ConfirmSelect();

            channel.QueueDeclare(
                "my.queue1",
                true,
                false,
                false,
                null);

            channel.QueueDeclare(
                "my.queue2",
                true,
                false,
                false,
                null);

            channel.BasicPublish(
                "",
                "my.queue1",
                null,
                Encoding.UTF8.GetBytes("Message with routing key my.queue1"));

            channel.BasicPublish(
                "",
                "my.queue2",
                null,
                Encoding.UTF8.GetBytes("Message with routing key my.queue2"));

            channel.WaitForConfirms();
        }
    }
}
