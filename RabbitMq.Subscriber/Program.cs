using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading;

namespace RabbitMq.Subscriber
{
     class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory();
            factory.Uri=new Uri("amqps://hkegsmzy:KAzxJXy8JOKCAO9JiQZrUz3vu1nX3xWC@fish" +
                ".rmq.cloudamqp.com/hkegsmzy");

            using var connection = factory.CreateConnection(); //Connection

            var channel = connection.CreateModel(); // Kanal
            channel.BasicQos(0, 1, false);
            //channel.QueueDeclare("hello-queue", true, false, false); //durable-->true olmalı ki memeoride kuyruklar kalsın

            var consumer=new EventingBasicConsumer(channel);
            channel.BasicConsume("hello-queue",false,consumer);

            consumer.Received+=(object sender, BasicDeliverEventArgs e) =>
            {
                var message = Encoding.UTF8.GetString(e.Body.ToArray());
                Thread.Sleep(1500);

                Console.WriteLine("Gelen mesaj" + message) ;
                channel.BasicAck(e.DeliveryTag, false);
            };
            

            Console.ReadLine();
        }

       
    }
}
