using Flyweight.Application;

Console.WriteLine("=== Editor de Texto Otimizado (Padrão Flyweight) ===\n");

var document = new Document();

string text = "Hello World! IMPORTANT: This is a Flyweight example.";

for (int i = 0; i < text.Length; i++)
{
    document.AddCharacter(text[i], "Arial", 12, "Black", false, false, false, 1, i + 1);
}

string important = "ALERT";
for (int i = 0; i < important.Length; i++)
{
    document.AddCharacter(important[i], "Arial", 12, "Red", true, false, false, 2, i + 1);
}

document.PrintMemoryUsage();