using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;

namespace DirectDemo
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

            channel.ExchangeDeclare
                (
                "ex.direct",
                ExchangeType.Direct,
                true,
                false,
                null);

            channel.QueueDeclare(
                "my.infos",
                true,
                false,
                false,
                null);

            channel.QueueDeclare(
                "my.warnings",
                true,
                false,
                false,
                null);

            channel.QueueDeclare(
                "my.errors",
                true,
                false,
                false,
                null);

            channel.QueueBind("my.infos", "ex.direct", "info");
            channel.QueueBind("my.warnings", "ex.direct", "warning");
            channel.QueueBind("my.errors", "ex.direct", "error");

            channel.BasicPublish(
                "ex.direct",
                "info",
                null,
                Encoding.UTF8.GetBytes("Message with routing key info."));

            channel.BasicPublish(
                "ex.direct",
                "warning",
                null,
                Encoding.UTF8.GetBytes("Message with routing key warning."));

            channel.BasicPublish(
                "ex.direct",
                "error",
                null,
                Encoding.UTF8.GetBytes("Message with routing key error."));

            channel.WaitForConfirms();
        }
    }
}
