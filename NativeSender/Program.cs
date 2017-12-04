using System;
using System.Text;
using RabbitMQ.Client;

class Program
{
    static void Main()
    {
        Console.Title = "Case00033267.Sender";
        var connectionFactory = new ConnectionFactory();

        using (var connection = connectionFactory.CreateConnection(Console.Title))
        {
            Console.WriteLine("Press enter to send a message");
            Console.WriteLine("Press any key to exit");

            while (true)
            {
                var key = Console.ReadKey();
                Console.WriteLine();
                if (key.Key != ConsoleKey.Enter)
                {
                    return;
                }
                using (var channel = connection.CreateModel())
                {
                    var properties = channel.CreateBasicProperties();

                    properties.MessageId = "28265d60-d783-11e7-ba8c-3f324bce8036_3150953433";

                    var payload = "{\"exampleId\":\"3150953433\",\"eventDate\":\"2017-12-02T17:06:46.900+0000\"}";

                    channel.BasicPublish(
                        exchange: string.Empty,
                        routingKey: "Case00033267.Receiver",
                        mandatory: false,
                        basicProperties: properties,
                        body: Encoding.UTF8.GetBytes(payload));

                    Console.WriteLine($"Message with id {properties.MessageId} sent to queue Case00033267.Receiver");
                }
            }
        }
    }
}
