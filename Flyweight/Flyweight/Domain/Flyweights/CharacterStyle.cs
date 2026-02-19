namespace Flyweight.Domain.Flyweights;

public class CharacterStyle
{
    public string FontFamily { get; }
    public int FontSize { get; }
    public string Color { get; }
    public bool IsBold { get; }
    public bool IsItalic { get; }
    public bool IsUnderline { get; }

    public CharacterStyle(string fontFamily, int fontSize, string color, bool isBold, bool isItalic, bool isUnderline)
    {
        FontFamily = fontFamily;
        FontSize = fontSize;
        Color = color;
        IsBold = isBold;
        IsItalic = isItalic;
        IsUnderline = isUnderline;
    }

    public void Render(char symbol, int row, int column)
    {
        var style = "";
        if (IsBold) style += "B";
        if (IsItalic) style += "I";
        if (IsUnderline) style += "U";

        Console.WriteLine($"[{row},{column}] '{symbol}' {FontFamily} {FontSize}pt {Color} {style}");
    }

    // Tamanho na memória
    public int GetMemorySize() => 32 + 4 + 32 + 3; // ~71 bytes
}
