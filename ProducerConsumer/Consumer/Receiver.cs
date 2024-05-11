using RabbitMQ.Client;
using RabbitMQ.Client.Events;
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

    Console.WriteLine("Receiver started...");

    var consumer = new EventingBasicConsumer(channel);
    consumer.Received += (model, ea) =>
    {
        var body = ea.Body.Span;
        var message = Encoding.UTF8.GetString(body);
        Console.WriteLine("Received message: {0}", message);
    };

    channel.BasicConsume(queueName, true, consumer);

    Console.ReadLine();
}