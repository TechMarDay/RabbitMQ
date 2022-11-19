using RabbitMQ.Client;
using RabbitMQConsumer;
using System.Text;

var factory = new ConnectionFactory()
{
    HostName = "localhost",
    Port = 5673,
    UserName = "guest",
    Password = "guest"
};

//UserName = "admin",
//    Password = "123"
var connection = factory.CreateConnection();
var channel = connection.CreateModel();

channel.BasicQos(0, 1, false);
MessageReceiver messageReceiver = new MessageReceiver(channel);
channel.BasicConsume("demoqueue", false, messageReceiver);
Console.ReadLine();