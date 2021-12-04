namespace AdventOfCode2021.Day02;

internal class Day02
{
    const string inputPath = @"Day02/Input.txt";

    public static void Task1and2()
    {
        (int hor, int depth) submarinePosition = new (0, 0);
        (int hor, int depth, int aim) submarinePosition2 = new(0, 0, 0);

        List<string> commands = File.ReadAllLines(inputPath).ToList();

        foreach(string command in commands)
        {
            string[] com = command.Split(' ');
            int units = int.Parse(com[1]);
            if (com[0] == "forward")
            {
                submarinePosition.hor += units;
                submarinePosition2.hor += units;
                submarinePosition2.depth += submarinePosition2.aim * units;
            }
            else if (com[0] == "down")
            {
                submarinePosition.depth += units;
                submarinePosition2.aim += units;
            }
            else if (com[0] == "up")
            {
                submarinePosition.depth -= units;
                submarinePosition2.aim -= units;
            }
        }

        Console.WriteLine($"Task 1: {submarinePosition.hor * submarinePosition.depth}");
        Console.WriteLine($"Task 2: {submarinePosition2.hor * submarinePosition2.depth}");
    }
}

