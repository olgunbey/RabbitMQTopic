

using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

ConnectionFactory factory = new ConnectionFactory();
factory.HostName = "localhost";
factory.Port = 5672;

IConnection connection=factory.CreateConnection();
IModel channel = connection.CreateModel();


string _exchange = "fanout-example";
channel.ExchangeDeclare(
    exchange:_exchange,
    ExchangeType.Fanout
    );

Console.Write("Mesaj gir: ");
string _queuename = Console.ReadLine();

channel.QueueDeclare(
    queue: _queuename,
    exclusive:false
    );

channel.QueueBind(
    queue:_queuename,
    exchange:_exchange,
    routingKey:string.Empty
    );

EventingBasicConsumer consumers=new EventingBasicConsumer(channel);

channel.BasicConsume(
       queue: _queuename,
       autoAck: true,
       consumer: consumers
       );

consumers.Received += (a, b) =>
{
   


    string message = Encoding.UTF8.GetString(b.Body.Span);

    Console.WriteLine(message);
};

Console.Read();




