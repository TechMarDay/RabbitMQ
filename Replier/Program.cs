using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

//publish response message to responses queue
var consumer = new EventingBasicConsumer(channel);
consumer.Received += (sender, e) =>
{
    string request = System.Text.Encoding.UTF8.GetString(e.Body.ToArray());
    Console.WriteLine("Request received:" + request);

    string response = "Response for " + request;

    channel.BasicPublish("", "responses", null, Encoding.UTF8.GetBytes(response));
};

//Cosume message from requests queue
channel.BasicConsume("requests", true, consumer);

Console.WriteLine("Press a key to exit.");
Console.ReadKey();

channel.Close();
conn.Close();
