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
    "ex.topic",
    ExchangeType.Topic,
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

channel.QueueDeclare(
    "my.queue3",
    true,
    false,
    false,
    null);

channel.QueueBind("my.queue1", "ex.topic", "*.image.*");
channel.QueueBind("my.queue2", "ex.topic", "#.image");
channel.QueueBind("my.queue3", "ex.topic", "image.#");

channel.BasicPublish(
    "ex.topic",
    "convert.image.bmp",
    null,
    Encoding.UTF8.GetBytes("Routing key is convert.image.bmp"));

Console.WriteLine("Send Routing key is convert.image.bmp");

channel.BasicPublish(
    "ex.topic",
    "convert.bitmap.image",
    null,
    Encoding.UTF8.GetBytes("Routing key is convert.image.bmp"));

Console.WriteLine("Send Routing key is convert.image.bmp");

channel.BasicPublish(
    "ex.topic",
    "image.bitmap.32bit",
    null,
    Encoding.UTF8.GetBytes("Routing key is image.bitmap.32bit"));

Console.WriteLine("Send Routing key is image.bitmap.32bit");

Console.ReadLine();

