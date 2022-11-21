using ConsumerRabbitMQ;
using RabbitMQ.Client;

var connectionFactory = new ConnectionFactory()
{
    HostName = "localhost",
    Port = 5673,
    UserName = "admin",
    Password = "123"
};

var connection = connectionFactory.CreateConnection();

var channel = connection.CreateModel();

// Set Prefetch Count value to limit the number of messages received at the same time.
channel.BasicQos(0, 1, false);

MessageReceiver messageReceiver = new MessageReceiver(channel);

channel.BasicConsume("demoqueue", false, messageReceiver);


//Set Auto Ack is true to prevent mistakes and save the waiting time.

//channel.BasicConsume("topic.bombay.queue", false, messageReceiver);

//Console.WriteLine("Queue 1");
//channel.BasicConsume("Queue 1", false, messageReceiver);

//Console.WriteLine("Queue 2");
//channel.BasicConsume("Queue 2", false, messageReceiver);

//Console.WriteLine("Queue 3");
//channel.BasicConsume("Queue3", false, messageReceiver);

//channel.BasicConsume("ReportPDF", false, messageReceiver);

Console.ReadLine();

