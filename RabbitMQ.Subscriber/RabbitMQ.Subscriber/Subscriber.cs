using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

class Subscriber
{
    static void Main(string[] args)
    {
        var factory = new ConnectionFactory()
        {
            Uri = new Uri("")
        };
        
        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();
        
        string queueName = "minha-fila";
        channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
        
        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += async (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            
            Console.WriteLine("Mensagem recebida: " + message);
            
            var httpClient = new HttpClient();
            var url = "http://localhost:5062/api/mensagem";

            var mensagem = new
            {
                Texto = message
            };
            
            var content = new StringContent(JsonSerializer.Serialize(mensagem), Encoding.UTF8, "application/json");
            
            var response = await httpClient.PostAsync(url, content);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Mensagem salva no banco!");
            }
            else
            {
                Console.WriteLine("Falha ao salvar a mensagem no banco.");
            }
        };
        
        channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);
        
        Console.WriteLine("Aguardando mensagens. Pressione [enter] para sair.");
        Console.ReadLine();
    }
}