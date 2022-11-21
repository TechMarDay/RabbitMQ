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

//Declare exchange
channel.ExchangeDeclare
    (
    "ex.directDemo",
    ExchangeType.Direct,
    true,
    false,
    null);

//Declare queue
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

//Binding queue to exchange with routing key
channel.QueueBind("my.infos", "ex.directDemo", "info");
channel.QueueBind("my.warnings", "ex.directDemo", "warning");
channel.QueueBind("my.errors", "ex.directDemo", "error");

//Publish message to exchange with routing key
var infoMesssage = "Message with routing key info.";
channel.BasicPublish(
    "ex.directDemo",
    "info",
    null,
    Encoding.UTF8.GetBytes(infoMesssage));
Console.WriteLine($"Message Sent: {infoMesssage}");

var warningMesssage = "Message with routing key warning.";
channel.BasicPublish(
    "ex.directDemo",
    "warning",
    null,
    Encoding.UTF8.GetBytes(warningMesssage));
Console.WriteLine($"Message Sent: {warningMesssage}");

var errorMesssage = "Message with routing key error.";
channel.BasicPublish(
    "ex.directDemo",
    "error",
    null,
    Encoding.UTF8.GetBytes(errorMesssage));
Console.WriteLine($"Message Sent: {errorMesssage}");

/*
channel.QueueDelete("my.infos");
channel.QueueDelete("my.warnings");
channel.QueueDelete("my.errors");
channel.ExchangeDelete("ex.directDemo");
*/

channel.Close();
conn.Close();

Console.ReadLine();

