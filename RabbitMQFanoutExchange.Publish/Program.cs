

using RabbitMQ.Client;
using System.Text;

ConnectionFactory factory = new();
factory.HostName = "localhost";
factory.Port = 5672;

IModel channel=factory.CreateConnection().CreateModel();


string _exchange = "fanout-example";
channel.ExchangeDeclare(
    exchange:_exchange,
    ExchangeType.Fanout
    );



for (int i = 0; i < 100; i++)
{
    byte[] messagess = Encoding.UTF8.GetBytes($"olgun {i}");

    channel.BasicPublish(
        exchange:_exchange,
        routingKey:string.Empty,
        body:messagess
        );

}

Console.Read();