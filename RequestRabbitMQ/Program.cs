
using RabbitMQ.Client;

string UserName = "admin";
string Password = "123";
string HostName = "localhost";
int Port = 5673;

//Main entry point to the RabbitMQ .NET AMQP client
var connectionFactory = new ConnectionFactory()
{
    UserName = UserName,
    Password = Password,
    HostName = HostName,
    Port = Port
};

var connection = connectionFactory.CreateConnection();
var model = connection.CreateModel();

// Create Exchange
model.ExchangeDeclare("ExampleExchange", ExchangeType.Direct);
Console.WriteLine("Creating Exchange");

// Create Queue
model.QueueDeclare("QueueWithTTL", true, false, false, new Dictionary<string, object>
{
    {"x-message-ttl", 10000 }
});
Console.WriteLine("Creating Queue");

// Bind Queue to Exchange
model.QueueBind("QueueWithTTL", "ExampleExchange", "exchange_ttl_key");
Console.WriteLine("Creating Binding");

Console.ReadLine();