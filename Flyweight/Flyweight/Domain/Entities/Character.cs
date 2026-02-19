using Flyweight.Domain.Flyweights;

namespace Flyweight.Domain.Entities;

public class Character
{
    private readonly char _symbol;
    private readonly int _row;
    private readonly int _column;
    private readonly CharacterStyle _style;

    public Character(char symbol, int row, int column, CharacterStyle style)
    {
        _symbol = symbol;
        _row = row;
        _column = column;
        _style = style;
    }

    public void Render() => _style.Render(_symbol, _row, _column);

    // Memória: ~18 bytes
    public int GetMemorySize() => 2 + 4 + 4 + 8;
}
