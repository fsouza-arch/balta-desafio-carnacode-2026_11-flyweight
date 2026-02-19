using Flyweight.Domain.Entities;
using Flyweight.Infrastructure.Factories;

namespace Flyweight.Application;

public class Document
{
    private readonly List<Character> _characters = new List<Character>();
    private readonly StyleFactory _styleFactory = new StyleFactory();

    public void AddCharacter(char symbol, string fontFamily, int fontSize, string color,
                            bool isBold, bool isItalic, bool isUnderline, int row, int column)
    {
        var style = _styleFactory.GetStyle(fontFamily, fontSize, color, isBold, isItalic, isUnderline);

        _characters.Add(new Character(symbol, row, column, style));
    }

    public void PrintMemoryUsage()
    {
        long totalMemory = 0;
        foreach (var c in _characters) totalMemory += c.GetMemorySize();

        long styleMemory = _styleFactory.GetTotalStylesCreated() * 71;

        Console.WriteLine($"\n=== Uso de Memória (Flyweight) ===");
        Console.WriteLine($"Total de caracteres: {_characters.Count}");
        Console.WriteLine($"Estilos únicos criados: {_styleFactory.GetTotalStylesCreated()}");
        Console.WriteLine($"Memória total aproximada: {totalMemory + styleMemory:N0} bytes");
        Console.WriteLine($"Economia estimada: ~75% em relação à versão original");
    }
}
