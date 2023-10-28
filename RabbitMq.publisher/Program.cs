using RabbitMQ.Client;
using System;
using System.Linq;
using System.Text;

namespace RabbitMq.publisher
{
     class Program
    {
        static void Main(string[] args)
        {
            var factory=new ConnectionFactory();

            factory.Uri=new Uri("amqps://hkegsmzy:KAzxJXy8JOKCAO9JiQZrUz3vu1nX3xWC@fish" +
                ".rmq.cloudamqp.com/hkegsmzy");

            using var connection = factory.CreateConnection() ; //Connection

            var channel = connection.CreateModel(); // Kanal
            channel.QueueDeclare("hello-queue", true,false,false); //durable-->true olmalı ki memeoride kuyruklar kalsın

            Enumerable.Range(1, 50).ToList().ForEach(x =>
            {
                string message = $"Message {x}";
                var messageBody = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(string.Empty, "hello-queue", null, messageBody);

                Console.WriteLine($"Mesaj gönderilmiştir : {message}");
            });

           
            Console.ReadLine();
        
        }
    }
}
