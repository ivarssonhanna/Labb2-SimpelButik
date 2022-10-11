using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Channels;
using System.Xml.Schema;
using System.Xml.Serialization;
using Labb2;
// console.clear
var data = new DataSource();

Customer? _currentCustomer = null;

MainMenu();

void MainMenu()
{
    bool run = true;

    while (run)
    {
        Console.Clear();
        Console.WriteLine("MAIN MENU");
        Console.WriteLine("-----------------------------");
        Console.WriteLine("1. Sign in");
        Console.WriteLine("2. Register new customer");
        Console.WriteLine("3. Close program");

        var choice = Console.ReadLine();
        switch (choice)
        {
            case "1":
                SignIn();
                MenuTwo();
                break;
            case "2":
                Register();
                break;
            case "3":
                run = false;
                break;
            default:
                Console.WriteLine("Try again.\nPress enter to continue.");
                Console.ReadKey();
                break;
        }
    }
}

void MenuTwo()
{
    string choice2;

    if (_currentCustomer != null)
    {
        do
        {
            Console.Clear();
            Console.WriteLine("MAKE A CHOICE");
            Console.WriteLine("-----------------------------");
            Console.WriteLine("a. Add items to cart");
            Console.WriteLine("b. View cart");
            Console.WriteLine("c. Pay");
            Console.WriteLine("d. Sign out");
            choice2 = Console.ReadLine().ToLower();
            switch (choice2)
            {
                case "a":
                    AddItemToCart();
                    break;
                case "b":
                    ViewCart();
                    break;
                case "c":
                    Pay();
                    break;
                case "d":
                    SignOut();
                    break;
                default:
                    Console.WriteLine("Try again");
                    break;
            }
        } while (choice2 != "d");
    }
}

void SignIn()
{
    Console.Clear();
    Console.WriteLine("SIGN IN");
    Console.WriteLine("-----------------------------");
    Console.Write("Name: ");
    var inputName = Console.ReadLine();
    _currentCustomer = data.Customers.FirstOrDefault(p => p.Name.ToLower().Equals(inputName.ToLower()));
    if (_currentCustomer != null)
    {
        Console.Write("Password: ");
        var inputPassword = Console.ReadLine();
        _currentCustomer.VerifyPassword(inputPassword);
    }
    else if (data.Customers.Contains(_currentCustomer) == false)
    {
        Console.WriteLine($"{inputName} doesn't exist, you need to become a member before you sign in.");
    }
    Console.WriteLine("Press enter to continue");
    Console.ReadKey();
}

void Register()
{
    Console.Clear();
    Console.WriteLine("REGISTER");
    Console.WriteLine("-----------------------------");
    Console.Write("Name: ");
    var inputName = Console.ReadLine();
    var inputPassword = String.Empty;
    if (inputName != string.Empty)
    {
        while (inputPassword == String.Empty)
        {
            Console.Write("Password: ");
            inputPassword = Console.ReadLine();
            if (inputPassword == String.Empty)
            {
                Console.WriteLine("You need to enter a password.");
            }
        }
        data.Customers.AddRange(new[] { new Customer(inputName, inputPassword) });
        Console.WriteLine($"\nWelcome {inputName}!\nSign in to shop.\nPress enter to continue");
        Console.ReadKey();
    }
}

void SignOut()
{
    Console.Clear();
    Console.WriteLine("SIGN OUT");
    Console.WriteLine("-----------------------------");
    Console.WriteLine($"Welcome back {_currentCustomer.Name}!");
    Console.WriteLine("Press enter to continue");
    Console.ReadKey();
    _currentCustomer = null;
}

void AddItemToCart()
{
    Console.Clear();
    Console.WriteLine("ADD ITEMS TO CART");
    Console.WriteLine("-----------------------------");
    foreach (var item in data.Fruits)
    {
        Console.WriteLine($"{item}");
    }
    Console.WriteLine("-----------------------------");
    Console.WriteLine("Pick an item: ");
    string wantedItem = string.Empty;
    bool bContinue = true;
    while (bContinue)
    {
        wantedItem = Console.ReadLine();
        int id = 0;
        bool correctId = Int32.TryParse(wantedItem, out id);
        if (correctId)
        {
            bContinue = false;
        }
        else
        {
            Console.WriteLine("Wrong ID, try again.");
            Console.WriteLine("Pick an item: ");
        }
    }

    var wantedItemId = int.Parse(wantedItem);
    var wrongItem = true;
    while (wrongItem)
    {
        if (data.Fruits.Any(p => p.Id == wantedItemId))
        {
            wrongItem = false;
        }
        else
        {
            Console.WriteLine("Wrong ID, try again.");
            Console.WriteLine("Pick an item: ");
            wantedItemId = int.Parse(Console.ReadLine());
        }
    }

    Console.WriteLine("How many? ");
    var numberOfItem = int.Parse(Console.ReadLine());

    foreach (var item in data.Fruits)
    {
        if (wantedItemId == item.Id)
        {
            for (int i = 0; i < numberOfItem; i++)
            {
                _currentCustomer.Cart.Add(item);
            }
            Console.WriteLine($"{numberOfItem} pcs of {item.Name} is added to your cart");
        }
    }
    Console.WriteLine("Press enter to continue");
    Console.ReadKey();
}

void ViewCart()
{
    Console.Clear();
    Console.WriteLine("VIEW CART");
    Console.WriteLine("-----------------------------");

    var uniqueItem = _currentCustomer.Cart.Select(p => p).Distinct().ToList();

    foreach (var item in uniqueItem)
    {
        var number = _currentCustomer.Cart.Count(p => p == item);
        var uniqueItemsPrices = number * item.Price;
        Console.WriteLine($"{number} pcs - {item} - total: {uniqueItemsPrices} SEK");
    }

    var numberOfItems = _currentCustomer.Cart.Count;
    Console.WriteLine("-----------------------------");
    Console.WriteLine($"Total items: {numberOfItems} pcs");
    _currentCustomer.TotalPrice();
    Console.WriteLine("-----------------------------");
    Console.WriteLine("Press enter to continue");
    Console.ReadKey();
}

void Pay()
{
    Console.Clear();
    Console.WriteLine("PAY");
    Console.WriteLine("-----------------------------");
    _currentCustomer.TotalPrice();
    Console.WriteLine("-----------------------------\n");
    if (_currentCustomer.Cart == null)
    {
        Console.WriteLine("Your cart is empty.");
    }
    else
    {
        Random random = new Random();
        int randomPay = random.Next(1, 3);
        switch (randomPay)
        {
            case 1:
                Console.WriteLine($"{_currentCustomer.Name} have money today and can pay!");
                break;
            case 2:
                Console.WriteLine($"{_currentCustomer.Name} don't have enough money today!");
                break;
        }
    }
    Console.WriteLine("\nPress enter to continue");
    Console.ReadKey();
}
