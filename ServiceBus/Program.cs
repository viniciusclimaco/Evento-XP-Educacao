using Entities;
using FestivalMicrosoft.Config;
using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using System.Text;

await SendCoffeOrderAsync();

static async Task SendCoffeOrderAsync()
{
    WriteLine("SendCoffeOrderAsync", ConsoleColor.Cyan);

    var order = new CoffeOrder()
    {
        Name = "Vinicius",
        Type = "Expresso",
        Size = "Large",
        Data = DateTime.Now
        
    };
        
    var jsonCoffeOrder = JsonConvert.SerializeObject(order);        
    var message = new Message(Encoding.UTF8.GetBytes(jsonCoffeOrder))
    {
        Label = "CoffeOrder",
        ContentType = "application/json"
    };

    // Send the message...
    var client = new QueueClient(Settings.ConnectionString, Settings.QueueName);
    Write("Sending order...", ConsoleColor.Green);
    await client.SendAsync(message);
    WriteLine("Done!", ConsoleColor.Green);
    Console.WriteLine();
    await client.CloseAsync();
}

#region ACESSÓRIOS 

static void WriteLine(string text, ConsoleColor color)
{
    var tempColor = Console.ForegroundColor;
    Console.ForegroundColor = color;
    Console.WriteLine(text);
    Console.ForegroundColor = tempColor;
}

static void Write(string text, ConsoleColor color)
{
    var tempColor = Console.ForegroundColor;
    Console.ForegroundColor = color;
    Console.Write(text);
    Console.ForegroundColor = tempColor;
}

#endregion



