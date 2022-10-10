using System.Threading.Channels;

namespace Labb2;

public class Customer
{
    public string Name { get; private set; }
    private string Password { get; set; }
    private List<Product> _cart;
    public List<Product> Cart { get { return _cart; } }
    public double Discount { get; set; }

    public Customer(string name, string password)
    {
        Name = name;
        Password = password;
        _cart = new List<Product>();
        Discount = 1;
    }
    public Customer() { }

    public override string ToString()
    {
        var customerCart = string.Empty;
        customerCart += $"Items:\n";
        foreach (var item in Cart)
        {
            customerCart += $"{item},";
        }
        customerCart += "\n";
        return customerCart;
    }
    public string VerifyPassword(string inputPassword)
    {
        bool wrongPassword = true;
        while (wrongPassword)
        {
            if (Password == inputPassword)
            {
                Console.WriteLine($"You are logged in as {Name}");
                wrongPassword = false;
            }
            else
            {
                Console.WriteLine("Wrong password, try again.");
                Console.Write("Password: ");
                inputPassword = Console.ReadLine();
            }
        }
        return $"";
    }
    public void TotalPrice()
    {
        double totalPrice = 0;
        foreach (var item in Cart)
        {
            totalPrice += item.Price;
        }

        totalPrice *= Discount;
        Console.WriteLine($"Total price: {totalPrice} SEK");

    }


}

