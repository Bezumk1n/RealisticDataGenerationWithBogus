using Bogus;
using System;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RealisticDataGenerationWithBogus
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var billingDetailsFaker = new Faker<BillingDetails>(locale: "ru")
                .RuleFor(x => x.CustomerName, x => x.Person.FullName)
                .RuleFor(x => x.Email, x => x.Person.Email)
                .RuleFor(x => x.Phone, x => x.Person.Phone)
                .RuleFor(x => x.AddressLine, x => x.Person.Address.Street)
                .RuleFor(x => x.City, x => x.Person.Address.City)
                .RuleFor(x => x.PostCode, x => x.Person.Address.ZipCode)
                .RuleFor(x => x.Country, x => x.Address.Country());

            var orderFaker = new Faker<Order>()
                .RuleFor(x => x.Id, Guid.NewGuid)
                .RuleFor(x => x.Currency, x => "руб")
                .RuleFor(x => x.Price, x => x.Finance.Amount(100, 10000))
                .RuleFor(x => x.BillingDetails, x => billingDetailsFaker);

            JsonSerializerOptions options = new JsonSerializerOptions
            {
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                WriteIndented = true
            };

            foreach (var order in orderFaker.GenerateForever())
            {
                var text = JsonSerializer.Serialize(order, options);
                Console.WriteLine(text);
                await Task.Delay(1000);
            }
        }
    }
}
