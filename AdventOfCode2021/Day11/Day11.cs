namespace AdventOfCode2021.Day11;
internal class Day11
{
    const string inputPath = @"Day11/Input.txt";
    static readonly (int y, int x)[] adjacents =
    {
        (-1, -1),
        (-1,  0),
        (-1, +1),
        ( 0, -1),
        ( 0, +1),
        (+1, -1),
        (+1,  0),
        (+1, +1)
    };
    static int rows = 0;
    static int cols = 0;

    public static void Task1()
    {
        int steps = 100;
        int flashCount = 0;
        List<string> lines = File.ReadAllLines(inputPath).ToList();
        rows = lines.Count;
        cols = lines[0].Length;
        DumboOctopus[] dumboOctopi = new DumboOctopus[rows * cols];

        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < cols; x++)
            {
                dumboOctopi[y * cols + x] = new DumboOctopus(lines[y][x] - '0');
            }
        }

        for (int i = 0; i < steps; i++)
        {
            // Increase energy level by one on all octopuses
            for (int y = 0; y < rows; y++)
            {
                for (int x = 0; x < cols; x++)
                {
                    dumboOctopi[y * cols + x].EnergyLevel++;
                }
            }

            // Calculate flash
            for (int y = 0; y < rows; y++)
            {
                for (int x = 0; x < cols; x++)
                {
                    if (dumboOctopi[y * cols + x].EnergyLevel > 9 && !dumboOctopi[y * cols + x].HasFlashed)
                    {
                        flashCount += CalculateFlash(dumboOctopi, y, x);
                    }
                }
            }

            // Reset energy level and flash status
            for (int y = 0; y < rows; y++)
            {
                for (int x = 0; x < cols; x++)
                {
                    if (dumboOctopi[y * cols + x].EnergyLevel > 9)
                    {
                        dumboOctopi[y * cols + x].EnergyLevel = 0;
                        dumboOctopi[y * cols + x].HasFlashed = false;
                    }
                }
            }
        }

        Console.WriteLine($"Task 1: {flashCount}");
    }

    public static void Task2()
    {
        List<string> lines = File.ReadAllLines(inputPath).ToList();
        DumboOctopus[] dumboOctopi = new DumboOctopus[lines.Count * lines[0].Length];
        rows = lines.Count;
        cols = lines[0].Length;

        for (int y = 0; y < lines.Count; y++)
        {
            for (int x = 0; x < lines[y].Length; x++)
            {
                dumboOctopi[y * lines.Count + x] = new DumboOctopus(lines[y][x] - '0');
            }
        }

        int steps = 0;
        while (dumboOctopi.Distinct().Count() != 1)
        {
            // Increase energy level by one on all octopuses
            for (int y = 0; y < rows; y++)
            {
                for (int x = 0; x < cols; x++)
                {
                    dumboOctopi[y * cols + x].EnergyLevel++;
                }
            }

            // Calculate flash
            for (int y = 0; y < rows; y++)
            {
                for (int x = 0; x < cols; x++)
                {
                    if (dumboOctopi[y * cols + x].EnergyLevel > 9 && !dumboOctopi[y * cols + x].HasFlashed)
                    {
                        CalculateFlash(dumboOctopi, y, x);
                    }
                }
            }

            // Reset energy level and flash status
            for (int y = 0; y < rows; y++)
            {
                for (int x = 0; x < cols; x++)
                {
                    if (dumboOctopi[y * cols + x].EnergyLevel > 9)
                    {
                        dumboOctopi[y * cols + x].EnergyLevel = 0;
                        dumboOctopi[y * cols + x].HasFlashed = false;
                    }
                }
            }

            steps++;
        }

        Console.WriteLine($"Task 2: {steps}");
    }

    private static int CalculateFlash(DumboOctopus[] dumboOctopi, int y, int x)
    {
        int flash = 1;
        
        dumboOctopi[y * cols + x].HasFlashed = true;

        foreach ((int adj_y, int adj_x) in adjacents)
        {
            int i = (y + adj_y) * cols + x + adj_x;

            if (i >= 0 && i < dumboOctopi.Length && i < (y + adj_y) * cols + rows && i >= (y + adj_y) * cols && !dumboOctopi[i].HasFlashed)
            {
                dumboOctopi[i].EnergyLevel++;
                if (dumboOctopi[i].EnergyLevel > 9)
                    flash += CalculateFlash(dumboOctopi, y + adj_y, x + adj_x);
            }   
        }

        return flash;
    }

    struct DumboOctopus
    {
        public DumboOctopus(int energyLevel)
        {
            EnergyLevel = energyLevel;
            HasFlashed = false;
        }
        public int EnergyLevel { get; set; }
        public bool HasFlashed { get; set; } 
    }
}
