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

channel.ExchangeDeclare(
    "ex.fanout",
    ExchangeType.Fanout,
    true,
    false,
    null);

channel.ExchangeDeclare(
    "ex.direct",
    ExchangeType.Direct,
    true,
    false,
    new Dictionary<string, object>()
    {
                    { "alternate-exchange", "ex.fanout" }
    });

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

channel.QueueDeclare(
    "my.unrouted",
    true,
    false,
    false,
    null);

channel.QueueBind("my.queue1", "ex.direct", "video");
channel.QueueBind("my.queue2", "ex.direct", "image");
channel.QueueBind("my.unrouted", "ex.fanout", "");

channel.BasicPublish(
    "ex.direct",
    "video",
    null,
    Encoding.UTF8.GetBytes("Message with routing key video"));
Console.WriteLine("SENT:" + "Message with routing key video");


channel.BasicPublish(
    "ex.direct",
    "text",
    null,
    Encoding.UTF8.GetBytes("Message with routing key text"));
Console.WriteLine("SENT:" + "Message with routing key text");



Console.WriteLine("Press a key to exit.");
Console.ReadKey();

channel.Close();
conn.Close();


