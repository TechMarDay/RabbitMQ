using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;

ConnectionFactory factory = new ConnectionFactory();
factory.HostName = "localhost";
factory.VirtualHost = "/";
factory.Port = 5673;
factory.UserName = "admin";
factory.Password = "123";

var conn = factory.CreateConnection();
var channel = conn.CreateModel();

channel.ExchangeDeclare(
    "ex.fanout",
    ExchangeType.Fanout,
    true,
    false,
    null);

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

channel.QueueBind("my.queue1", "ex.fanout", "");
channel.QueueBind("my.queue2", "ex.fanout", "");

//No need queue when sending messages with fanout exchange
channel.BasicPublish(
    "ex.fanout",
    "",
    null,
    Encoding.UTF8.GetBytes("Message 1")
    );
Console.WriteLine("Send Message 1");

channel.BasicPublish(
    "ex.fanout",
    "",
    null,
    Encoding.UTF8.GetBytes("Message 2")
    );
Console.WriteLine("Send Message 2");

Console.WriteLine("Press a key to exit.");
Console.ReadKey();

//channel.QueueDelete("my.queue1");
//channel.QueueDelete("my.queue2");
//channel.ExchangeDelete("ex.fanout");

channel.Close();
conn.Close();
