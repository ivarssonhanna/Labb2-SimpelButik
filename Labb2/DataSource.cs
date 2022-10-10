using System.Xml.Linq;

namespace Labb2;

public class DataSource
{
    public List<Customer> Customers { get; set; } = new();
    public List<Product> Fruits { get; } = new();

    public DataSource()
    {
        Fruits.AddRange(new[]
        {
            new Product { Id = 1, Name = "lemon", Price = 6.5 },
            new Product { Id = 2, Name = "blueberries", Price = 25 },
            new Product { Id = 3, Name = "avocado", Price = 11.5 },
            new Product { Id = 4, Name = "watermelon", Price = 45 },
            new Product { Id = 5, Name = "raspberries", Price = 34.5 },
            new Product { Id = 6, Name = "mango", Price = 20 }

        });
        
        Customers.AddRange(new []
        {
            new Customer("Knatte", "123"),
            new Customer("Fnatte", "321") { Cart = {} },
            new Customer("Tjatte", "213") { Cart = {} }
        });
    }

}