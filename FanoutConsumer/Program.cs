// See https://aka.ms/new-console-template for more information
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

Console.WriteLine("Hello, World!");

IConnection conn;
IModel channel;

ConnectionFactory factory = new ConnectionFactory();
factory.HostName = "localhost";
factory.VirtualHost = "/";
factory.Port = 5672;
factory.UserName = "guest";
factory.Password = "guest";

conn = factory.CreateConnection();
channel = conn.CreateModel();

var consumer = new EventingBasicConsumer(channel);
consumer.Received += Consumer_Received;

var consumerTag =  channel.BasicConsume("my.queue1", true, consumer);

Console.WriteLine("Waiting for messages. Press any key to exit.");
Console.ReadKey();


static void Consumer_Received(object sender, BasicDeliverEventArgs e)
{
    string message = Encoding.UTF8.GetString(e.Body.ToArray());
    Console.WriteLine("Message: " + message);

    //channel.BasicAck(e.DeliveryTag, false);
    //channel.BasicNack(e.DeliveryTag, false, true);
}
