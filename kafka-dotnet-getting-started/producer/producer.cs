using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

class Producer
{
    static void Main(string[] args)
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

        IConfiguration config = builder.Build();

        const string topic = "purchases";

        string[] users = ["eabara", "jsmith", "sgarcia", "jbernard", "htanaka", "awalther"];
        string[] items = ["book", "alarm clock", "t-shirts", "gift card", "batteries"];

        var producerConfig = new ProducerConfig
        {
            BootstrapServers = config["Kafka:BootstrapServers"],
            SaslUsername = config["Kafka:SaslUsername"],
            SaslPassword = config["Kafka:SaslPassword"],

            SecurityProtocol = SecurityProtocol.SaslSsl,
            SaslMechanism = SaslMechanism.Plain,
            Acks = Acks.All
        };

        using (var producer = new ProducerBuilder<string, string>(producerConfig).Build())
        {
            var numProduced = 0;
            Random rnd = new Random();
            const int numMessages = 10;
            for (int i = 0; i < numMessages; ++i)
            {
                var user = users[rnd.Next(users.Length)];
                var item = items[rnd.Next(items.Length)];

                producer.Produce(topic, new Message<string, string> { Key = user, Value = item },
                    (deliveryReport) =>
                    {
                        if (deliveryReport.Error.Code != ErrorCode.NoError)
                        {
                            Console.WriteLine($"Failed to deliver message: {deliveryReport.Error.Reason}");
                        }
                        else
                        {
                            Console.WriteLine($"Produced event to topic {topic}: key = {user,-10} value = {item}");
                            numProduced += 1;
                        }
                    });
            }

            producer.Flush(TimeSpan.FromSeconds(10));
            Console.WriteLine($"{numProduced} messages were produced to topic {topic}");
        }
    }
}