using System.Runtime.CompilerServices;

namespace Labb2;

public record Product
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public double Price { get; set; }

    public override string ToString()
    {
        return $"[{Id}] {Name} {Price} kr";
    }
}
