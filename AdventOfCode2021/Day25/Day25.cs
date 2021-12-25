namespace AdventOfCode2021.Day25;
internal class Day25
{
    const string inputPath = @"Day25/Input.txt";

    public static void Task1()
    {
        List<string> lines = File.ReadAllLines(inputPath).ToList();
        List<(int y, int x)> eastSeaC = new List<(int y, int x)>();
        List<(int y, int x)> southSeaC = new List<(int y, int x)>();
        int rightEdge = lines[0].Length;
        int bottomEdge = lines.Count;

        for (int y = 0; y < lines.Count; y++)
        {
            for(int x = 0; x < lines[y].Length; x++)
            {
                if (lines[y][x] == '>')
                    eastSeaC.Add((y, x));
                else if (lines[y][x] == 'v')
                    southSeaC.Add((y, x));
            }
        }

        int steps = 0;
        bool didMove;
        List<(int y, int x)> newEastSeaC = new List<(int y, int x)>();
        List<(int y, int x)> newSouthSeaC = new List<(int y, int x)>();

        do
        {
            didMove = false;
            foreach ((int y, int x) coord in eastSeaC)
            {
                int newX = (coord.x + 1) % rightEdge;
                if (eastSeaC.Contains((coord.y, newX)) || southSeaC.Contains((coord.y, newX)))
                    newEastSeaC.Add(coord);
                else
                {
                    newEastSeaC.Add((coord.y, newX));
                    didMove = true;
                }
                    
            }

            eastSeaC = new List<(int y, int x)>(newEastSeaC);
            newEastSeaC.Clear();

            foreach ((int y, int x) coord in southSeaC)
            {
                int newY = (coord.y + 1) % bottomEdge;
                if (eastSeaC.Contains((newY, coord.x)) || southSeaC.Contains((newY, coord.x)))
                    newSouthSeaC.Add(coord);
                else
                {
                    newSouthSeaC.Add((newY, coord.x));
                    didMove = true;
                }
            }

            southSeaC = new List<(int y, int x)>(newSouthSeaC); ;
            newSouthSeaC.Clear();
            steps++;
        } while (didMove);

        Console.WriteLine($"Task 1: {steps}");
    }
}
