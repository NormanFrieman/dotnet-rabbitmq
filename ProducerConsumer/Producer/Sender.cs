using RabbitMQ.Client;
using System.Text;

var factory = new ConnectionFactory();
//factory.UserName = "guest";
//factory.Password = "guest";
//factory.VirtualHost = "/";
//factory.HostName = "localhost";
//factory.Port = 5672;

using (var connection = factory.CreateConnection())
{
    using (var channel = connection.CreateModel())
    {
        var queueName = "BasicTest";
        channel.QueueDeclare(queueName, false, false, false, null);

        var message = "Hello world";
        var body = Encoding.UTF8.GetBytes(message);

        channel.BasicPublish("", queueName, null, body);
        Console.WriteLine("Sent message: {0}", message);
        
        Console.ReadLine();
    }
}