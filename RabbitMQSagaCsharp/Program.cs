

using RabbitMQ.Client;
using System.Text;

var factory = new ConnectionFactory()
{
    HostName = "localhost",
    Port = 5673,
    UserName = "admin",
    Password = "123"
};
try
{
    var connection = factory.CreateConnection();
    var model = connection.CreateModel();

    //Console.WriteLine("Creating Exchange");
    // Create Exchange
    //model.ExchangeDeclare("demoExchange", ExchangeType.Direct);

    // Create Queue
    //model.QueueDeclare("demoqueue", true, false, false, null);
    //Console.WriteLine("Creating Queue");
    //Console.ReadLine();

    // Bind Queue to Exchange
    //model.QueueBind("demoqueue", "demoExchange", "directexchange_key");
    //Console.WriteLine("Creating Binding");
    //Console.ReadLine();

    var properties = model.CreateBasicProperties();
    properties.Persistent = false;

    byte[] messagebuffer = Encoding.Default.GetBytes("Direct Message");
    model.BasicPublish("demoExchange", "directexchange_key", properties, messagebuffer);
    Console.WriteLine("Message Sent");
    Console.ReadLine();

}
catch (Exception ex)
{
    Console.WriteLine(ex.ToString());
}
Console.ReadLine();
