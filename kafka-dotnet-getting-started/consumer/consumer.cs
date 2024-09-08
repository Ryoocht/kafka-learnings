using Confluent.Kafka;
using System;
using System.Threading;

class Consumer
{
    static void Main(string[] args)
    {
        var config = new ConsumerConfig
        {
            BootstrapServers = "<BOOTSTRAP SERVERS>",
            SaslUsername = "<CLUSTER API KEY>",
            SaslPassword = "<CLUSTER API SECRET>",
        };
    }
}