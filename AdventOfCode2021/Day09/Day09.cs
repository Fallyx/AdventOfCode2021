namespace AdventOfCode2021.Day09;

internal class Day09
{
    const string inputPath = @"Day09/Input.txt";

    public static void Task1()
    {
        List<string> lines = File.ReadAllLines(inputPath).ToList();
        int[,] heightMap = new int[lines.Count, lines[0].Length];
        List<int> lowPoints = new List<int>();

        for (int y = 0; y < lines.Count; y++)
        {
            for(int x = 0; x < lines[y].Length; x++)
            {
                heightMap[y,x] = Int32.Parse(lines[y][x].ToString());
            }
        }
        
        for(int y = 0; y < heightMap.GetLength(0); y++)
        {
            for(int x = 0; x < heightMap.GetLength(1); x++)
            {
                List<int> adjacents = new List<int>();
                if (y != 0) adjacents.Add(heightMap[y - 1, x]);
                if (y != heightMap.GetLength(0) - 1) adjacents.Add(heightMap[y + 1, x]);
                if (x != 0) adjacents.Add(heightMap[y, x - 1]);
                if (x != heightMap.GetLength(1) - 1) adjacents.Add(heightMap[y, x + 1]);

                if (adjacents.All(n => n > heightMap[y, x])) lowPoints.Add(heightMap[y, x]);
            }
        }

        Console.WriteLine($"Task 1: {lowPoints.Select(n => n + 1).Sum()}");
    }

    public static void Task2()
    {
        List<string> lines = File.ReadAllLines(inputPath).ToList();
        bool[,] heightMap = new bool[lines.Count, lines[0].Length];
        List<int> basinSizes = new List<int>();

        for (int y = 0; y < heightMap.GetLength(0); y++)
        {
            for (int x = 0; x < heightMap.GetLength(1); x++)
            {
                heightMap[y, x] = Int32.Parse(lines[y][x].ToString()) == 9;
            }
        }

        for (int y = 0; y < heightMap.GetLength(0); y++)
        {
            for (int x = 0; x < heightMap.GetLength(1); x++)
            {
                if (!heightMap[y, x])
                {
                    heightMap[y, x] = true;
                    int basinSize = CalculateBasinSize(heightMap, y, x);
                    basinSizes.Add(basinSize);
                }
            }
        }

         Console.WriteLine($"Task 2: {basinSizes.OrderByDescending(n => n).Take(3).Aggregate(1, (tot, next) => tot * next)}");
    }

    private static int CalculateBasinSize(bool[,] heightMap, int y, int x)
    {
        int basinSize = 1;
        heightMap[y, x] = true;

        if (y != 0 && !heightMap[y - 1, x])
            basinSize += CalculateBasinSize(heightMap, y - 1, x);
        if (y != heightMap.GetLength(0) - 1 && !heightMap[y + 1, x])
            basinSize += CalculateBasinSize(heightMap, y + 1, x);
        if (x != 0 && !heightMap[y, x - 1])
            basinSize += CalculateBasinSize(heightMap, y, x - 1);
        if (x != heightMap.GetLength(1) - 1 && !heightMap[y, x + 1])
            basinSize += CalculateBasinSize(heightMap, y, x + 1);

        return basinSize;
    }
}
