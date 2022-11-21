using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

ConnectionFactory factory = new ConnectionFactory();
factory.HostName = "localhost";
factory.VirtualHost = "/";
factory.Port = 5673;
factory.UserName = "admin";
factory.Password = "123";

var conn = factory.CreateConnection();
var channel = conn.CreateModel();

var consumer = new EventingBasicConsumer(channel);
consumer.Received += Consumer_Received;


//var consumerTag = channel.BasicConsume("my.queue1", false, consumer);

var consumerTag = channel.BasicConsume("my.queue2", false, consumer);


Console.WriteLine("Waiting for messages. Press any key to exit.");
Console.ReadKey();


void Consumer_Received(object sender, BasicDeliverEventArgs e)
{
    string message = Encoding.UTF8.GetString(e.Body.ToArray());
    Console.WriteLine("Message:" + message);

    channel.BasicNack(e.DeliveryTag, false, false);
}
