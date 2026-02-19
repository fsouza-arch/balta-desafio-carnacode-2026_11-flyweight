using Flyweight.Domain.Flyweights;

namespace Flyweight.Infrastructure.Factories;

public class StyleFactory
{
    private readonly Dictionary<string, CharacterStyle> _styles = new Dictionary<string, CharacterStyle>();

    public CharacterStyle GetStyle(string fontFamily, int fontSize, string color, bool isBold, bool isItalic, bool isUnderline)
    {
        // Chave única para identificar este estilo específico
        string key = $"{fontFamily}_{fontSize}_{color}_{isBold}_{isItalic}_{isUnderline}";

        if (!_styles.ContainsKey(key))
        {
            Console.WriteLine($"[Factory] Criando novo estilo: {key}");
            _styles[key] = new CharacterStyle(fontFamily, fontSize, color, isBold, isItalic, isUnderline);
        }

        return _styles[key];
    }

    public int GetTotalStylesCreated() => _styles.Count;
}
