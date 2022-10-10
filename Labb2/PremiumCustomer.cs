using System.Xml.Linq;

namespace Labb2;

public class PremiumCustomer : Customer
{
    public PremiumCustomer(double totalPrice)
    {
        if (totalPrice >= 500)
        {
            Discount = 0.85;
        }
        else if (totalPrice >= 400)
        {
            Discount = 0.9;
        }
        else if (totalPrice >= 300)
        {
            Discount = 0.95;
        }
        else
        {
            Discount = 1;
        }
    }
    
}