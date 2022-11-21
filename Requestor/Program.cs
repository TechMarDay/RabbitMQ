using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

ConnectionFactory factory = new ConnectionFactory();
factory.HostName = "localhost";
factory.VirtualHost = "/";
factory.Port = 5673;
factory.UserName = "admin";
factory.Password = "123";

IConnection conn = factory.CreateConnection();
IModel channel = conn.CreateModel();

//Cosume response message from responses queue
var consumer = new EventingBasicConsumer(channel);
consumer.Received += (sender, e) =>
{
    string message = System.Text.Encoding.UTF8.GetString(e.Body.ToArray());
    Console.WriteLine("Response received:" + message);
};

channel.BasicConsume("responses", true, consumer);


//Publish request message to requests queue
while (true)
{
    Console.Write("Enter your request:");
    string request = Console.ReadLine();

    if (request == "exit")
        break;

    channel.BasicPublish("", "requests", null, Encoding.UTF8.GetBytes(request));
}

channel.Close();
conn.Close();
