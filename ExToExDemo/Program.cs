using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;


//https://www.cloudamqp.com/blog/exchange-to-exchange-binding-in-rabbitmq.html
ConnectionFactory factory = new ConnectionFactory();
factory.HostName = "localhost";
factory.VirtualHost = "/";
factory.Port = 5673;
factory.UserName = "admin";
factory.Password = "123";

var conn = factory.CreateConnection();
var channel = conn.CreateModel();

channel.ConfirmSelect();

channel.ExchangeDeclare(
    "exchange1",
    ExchangeType.Direct,
    true,
    false,
    null);

channel.ExchangeDeclare(
    "exchange2",
    ExchangeType.Direct,
    true,
    false,
    null);

channel.QueueDeclare(
    "queue1",
    true,
    false,
    false,
    null);

channel.QueueDeclare(
    "queue2",
    true,
    false,
    false,
    null);

channel.QueueBind("queue1", "exchange1", "key1");
channel.QueueBind("queue2", "exchange2", "key2");

channel.ExchangeBind("exchange2", "exchange1", "key2");


Console.WriteLine("Publisher is ready. Press a key to start sending messages.");
Console.ReadKey();

channel.BasicPublish(
    "exchange1",
    "key1",
    null,
    Encoding.UTF8.GetBytes("Message with routing key key1"));

channel.BasicPublish(
    "exchange1",
    "key2",
    null,
    Encoding.UTF8.GetBytes("Message with routing key key2"));


channel.Close();
conn.Close();

