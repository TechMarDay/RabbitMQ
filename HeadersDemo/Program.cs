using System.Text;
using RabbitMQ.Client;

IConnection conn;
IModel channel;

ConnectionFactory factory = new ConnectionFactory();
factory.HostName = "localhost";
factory.VirtualHost = "/";
factory.Port = 5673;
factory.UserName = "admin";
factory.Password = "123";

conn = factory.CreateConnection();
channel = conn.CreateModel();


channel.ExchangeDeclare(
    "ex.headers",
    ExchangeType.Headers,
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

channel.QueueBind(
    "my.queue1",
    "ex.headers",
    "",
    new Dictionary<string, object>()
    {
                    {"x-match","all" },
                    {"job","convert" },
                    {"format","jpeg" }
    });

channel.QueueBind(
    "my.queue2",
    "ex.headers",
    "",
    new Dictionary<string, object>()
    {
                    {"x-match","any" },
                    {"job","convert" },
                    {"format","jpeg" }
    });

IBasicProperties props = channel.CreateBasicProperties();
props.Headers = new Dictionary<string, object>();
props.Headers.Add("job", "convert");
props.Headers.Add("format", "jpeg");

channel.BasicPublish(
    "ex.headers",
    "",
    props,
    Encoding.UTF8.GetBytes("Message 1"));

Console.WriteLine("Send Message 1");

props = channel.CreateBasicProperties();
props.Headers = new Dictionary<string, object>();
props.Headers.Add("job", "convert");
props.Headers.Add("format", "bmp");

channel.BasicPublish(
    "ex.headers",
    "",
    props,
    Encoding.UTF8.GetBytes("Message 2"));

Console.WriteLine("Send Message 2");
Console.ReadLine();
