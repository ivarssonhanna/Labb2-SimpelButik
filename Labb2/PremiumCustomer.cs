using System.Xml.Linq;

namespace Labb2;

public class PremiumCustomer : Customer
{
    public PremiumCustomer(double totalPrice)
    {
        Discount = 0.85;
    }
}