// DESAFIO: Editor de Texto com Formatação de Caracteres
// PROBLEMA: Um editor de texto precisa renderizar milhões de caracteres, cada um com
// propriedades de formatação (fonte, tamanho, cor, estilo). Criar um objeto para cada
// caractere consome memória excessiva e degrada performance

using System;
using System.Collections.Generic;

namespace DesignPatternChallenge
{
    // Contexto: Editor que precisa representar documentos grandes com formatação rica
    // Cada caractere tem estado intrínseco (compartilhável) e extrínseco (único)
    
    // Problema: Abordagem ingênua - um objeto completo para cada caractere
    public class Character
    {
        // Estado intrínseco (compartilhável entre caracteres iguais)
        public char Symbol { get; set; }
        public string FontFamily { get; set; }
        public int FontSize { get; set; }
        public string Color { get; set; }
        public bool IsBold { get; set; }
        public bool IsItalic { get; set; }
        public bool IsUnderline { get; set; }
        
        // Estado extrínseco (único para cada posição)
        public int Row { get; set; }
        public int Column { get; set; }

        public Character(char symbol, string fontFamily, int fontSize, string color, 
                        bool isBold, bool isItalic, bool isUnderline, int row, int column)
        {
            Symbol = symbol;
            FontFamily = fontFamily;
            FontSize = fontSize;
            Color = color;
            IsBold = isBold;
            IsItalic = isItalic;
            IsUnderline = isUnderline;
            Row = row;
            Column = column;
        }

        public void Render()
        {
            var style = "";
            if (IsBold) style += "B";
            if (IsItalic) style += "I";
            if (IsUnderline) style += "U";
            
            Console.WriteLine($"[{Row},{Column}] '{Symbol}' {FontFamily} {FontSize}pt {Color} {style}");
        }

        // Cada instância ocupa aproximadamente 80-100 bytes na memória
        public int GetMemorySize()
        {
            return sizeof(char) +         // Symbol: 2 bytes
                   32 +                   // FontFamily: ~32 bytes (string)
                   sizeof(int) +          // FontSize: 4 bytes
                   32 +                   // Color: ~32 bytes (string)
                   3 * sizeof(bool) +     // Booleans: 3 bytes
                   2 * sizeof(int);       // Row, Column: 8 bytes
        }
    }

    public class Document
    {
        private List<Character> _characters;

        public Document()
        {
            _characters = new List<Character>();
        }

        public void AddCharacter(char symbol, string fontFamily, int fontSize, string color,
                                bool isBold, bool isItalic, bool isUnderline, int row, int column)
        {
            var character = new Character(symbol, fontFamily, fontSize, color, 
                                        isBold, isItalic, isUnderline, row, column);
            _characters.Add(character);
        }

        public void Render()
        {
            foreach (var character in _characters)
            {
                character.Render();
            }
        }

        public void PrintMemoryUsage()
        {
            long totalMemory = 0;
            foreach (var character in _characters)
            {
                totalMemory += character.GetMemorySize();
            }

            Console.WriteLine($"\n=== Uso de Memória ===");
            Console.WriteLine($"Total de caracteres: {_characters.Count}");
            Console.WriteLine($"Memória aproximada: {totalMemory:N0} bytes ({totalMemory / 1024.0:N2} KB)");
            Console.WriteLine($"Memória por caractere: ~{totalMemory / _characters.Count} bytes");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Editor de Texto - Problema de Memória ===\n");

            var document = new Document();

            // Simulando documento com texto formatado
            // Problema: Repetição massiva de dados compartilháveis
            
            // Linha 1: "Hello World" em Arial 12pt preto
            string text1 = "Hello World";
            for (int i = 0; i < text1.Length; i++)
            {
                // Cada caractere 'l' cria um novo objeto completo
                // mesmo tendo a mesma formatação!
                document.AddCharacter(
                    text1[i],
                    "Arial",      // Repetido em cada caractere
                    12,           // Repetido em cada caractere
                    "Black",      // Repetido em cada caractere
                    false,        // Repetido em cada caractere
                    false,        // Repetido em cada caractere
                    false,        // Repetido em cada caractere
                    1,
                    i + 1
                );
            }

            // Linha 2: "IMPORTANT" em Arial 12pt vermelho, negrito
            string text2 = "IMPORTANT";
            for (int i = 0; i < text2.Length; i++)
            {
                document.AddCharacter(
                    text2[i],
                    "Arial",      // Repetido novamente!
                    12,
                    "Red",
                    true,
                    false,
                    false,
                    2,
                    i + 1
                );
            }

            // Linha 3: Mais texto normal
            string text3 = "This is a sample text";
            for (int i = 0; i < text3.Length; i++)
            {
                document.AddCharacter(
                    text3[i],
                    "Arial",
                    12,
                    "Black",
                    false,
                    false,
                    false,
                    3,
                    i + 1
                );
            }

            Console.WriteLine("Renderizando primeiros 5 caracteres:\n");
            int count = 0;
            foreach (var ch in "Hello")
            {
                var character = new Character(ch, "Arial", 12, "Black", false, false, false, 1, ++count);
                character.Render();
            }

            document.PrintMemoryUsage();

            Console.WriteLine("\n=== PROBLEMAS ===");
            Console.WriteLine("✗ Duplicação massiva: 'Arial', '12', 'Black' repetidos em cada caractere");
            Console.WriteLine("✗ Caractere 'l' aparece 3x mas cria 3 objetos completos idênticos");
            Console.WriteLine("✗ Em documento de 1 milhão de caracteres:");
            Console.WriteLine("  → ~80-100 MB só para armazenar strings repetidas!");
            Console.WriteLine("✗ Criação e garbage collection de milhões de objetos similares");
            Console.WriteLine("✗ Cache miss: objetos espalhados na memória");

            Console.WriteLine("\n=== IMPACTO EM ESCALA ===");
            long charactersIn1MbDoc = 1_000_000;
            long memoryNeeded = charactersIn1MbDoc * 80;
            Console.WriteLine($"Documento com 1 milhão de caracteres:");
            Console.WriteLine($"  → Memória necessária: ~{memoryNeeded / (1024 * 1024):N0} MB");
            Console.WriteLine($"  → Mesmo com 90% de caracteres compartilháveis!");
            Console.WriteLine($"\nPara comparação:");
            Console.WriteLine($"  → Notepad abre arquivos de 1MB usando ~2-3 MB de RAM");
            Console.WriteLine($"  → Nossa implementação usaria ~80 MB!");

            // Perguntas para reflexão:
            // - Como compartilhar estado intrínseco entre múltiplos objetos?
            // - Como separar estado compartilhável (intrínseco) do não-compartilhável (extrínseco)?
            // - Como gerenciar pool de objetos compartilhados?
            // - Como reduzir uso de memória mantendo funcionalidade?
        }
    }
}
