using System.Text;
using RabbitMQ.Client;

ConnectionFactory factory = new ConnectionFactory();
factory.HostName = "localhost";
factory.VirtualHost = "/";
factory.Port = 5673;
factory.UserName = "admin";
factory.Password = "123";

var conn = factory.CreateConnection();
var channel = conn.CreateModel();


channel.ExchangeDeclare("exchange.demo.test", ExchangeType.Direct);
channel.ExchangeDeclare("fail.exchange.name", ExchangeType.Fanout);

channel.QueueDeclare("demo.queue.fail", false, false, false, null);
channel.QueueBind("demo.queue.fail", "fail.exchange.name", "");

var queueArguments = new Dictionary<string, object>
{
    {"x-message-ttl", 15000 },
    {"x-dead-letter-exchange", "fail.exchange.name" }
};
channel.QueueDeclare("demo.queue.name", false, false, false, queueArguments);
channel.QueueBind("demo.queue.name", "exchange.demo.test", "demo.test.routingKey");


var messsage = "Message test.";
channel.BasicPublish(
    "exchange.demo.test",
    "demo.test.routingKey",
    null,
    Encoding.UTF8.GetBytes(messsage));
Console.WriteLine($"Message Sent: {messsage}");






