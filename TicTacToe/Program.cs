var field = Enumerable.Repeat(' ', 9).ToArray();
PrintField(field);

while (true)
{
    Console.Write("Your turn (1-9): ");
    var inp = Console.ReadKey();
    Console.WriteLine();

    if (inp.Key == ConsoleKey.Escape) return;
    var ch = inp.KeyChar;
    if (ch < '1' || ch > '9') continue;

    var idx = ch - '1';
    if (field[idx] != ' ')
    {
        Console.WriteLine($"Cell {ch} is occupied. Try again.");
        continue;
    }

    field[idx] = 'X';

    if (OnLine('X', field))
    {
        PrintField(field);
        Console.WriteLine("You won!");
        return;
    }

    var resp = ComputerMove(field);
    if (resp == 0)
    {
        Console.WriteLine("Draw.");
        return;
    }

    field[resp - 1] = 'O';
    if (OnLine('O', field))
    {
        PrintField(field);
        Console.WriteLine("I won!");
        return;
    }

    PrintField(field);
}


void PrintField(char[] field)
{
    for (var y = 0; y < 3; y++)
    {
        if (y > 0) Console.WriteLine($"───┼───┼───");
        Console.WriteLine($" {field[y * 3 + 0]} │ {field[y * 3 + 1]} │ {field[y * 3 + 2]}");
    }
}


// Return:
// 0 - no more moves
// positive 1..9 - 1-based index where to put O
int ComputerMove(char[] field)
{
    if (field.All(ch => ch != ' ')) return 0;

    var empty = field.Select((ch, idx) => (ch, idx)).Where(c => c.ch == ' ').Select(c => c.idx).ToArray();
    var moveIdx = Random.Shared.Next(empty.Length);

    return empty[moveIdx] + 1;
}


bool OnLine(char ch, char[] field)
{
    if (field[0] == field[1] && field[0] == field[2] && field[0] == ch) return true;
    if (field[3] == field[4] && field[3] == field[5] && field[3] == ch) return true;
    if (field[6] == field[7] && field[6] == field[8] && field[6] == ch) return true;
    if (field[0] == field[3] && field[0] == field[6] && field[0] == ch) return true;
    if (field[1] == field[4] && field[1] == field[7] && field[1] == ch) return true;
    if (field[2] == field[5] && field[2] == field[8] && field[2] == ch) return true;
    if (field[0] == field[4] && field[0] == field[8] && field[0] == ch) return true;
    if (field[2] == field[4] && field[2] == field[6] && field[2] == ch) return true;

    return false;
}