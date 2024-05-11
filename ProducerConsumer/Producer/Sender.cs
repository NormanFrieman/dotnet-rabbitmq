using RabbitMQ.Client;
using System.Text;

var factory = new ConnectionFactory
{
    UserName = "guest",
    Password = "guest",
    VirtualHost = "/",
    HostName = "localhost",
    Port = 5672
};

using (var connection = factory.CreateConnection())
using (var channel = connection.CreateModel())
{
    var queueName = "BasicTest";
    channel.QueueDeclare(queueName, false, false, false, null);

    string? message;

    Console.WriteLine("Enter the message:");
    while ((message = Console.ReadLine()) != null){
        var body = Encoding.UTF8.GetBytes(message);

        channel.BasicPublish("", queueName, null, body);
        Console.WriteLine("Sent message: {0}", message);

        Console.WriteLine("Enter the message:");
    }
}