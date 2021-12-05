namespace AdventOfCode2021.Day05;

internal class Day05
{
    const string inputPath = @"Day05/Input.txt";

    public static void Task1and2()
    {
        Dictionary<(int x, int y), int> ventLinesTask1 = new Dictionary<(int x, int y), int>();
        Dictionary<(int x, int y), int> ventLinesTask2 = new Dictionary<(int x, int y), int>();
        List<string> lines = File.ReadAllLines(inputPath).ToList();

        foreach (string line in lines)
        {
            string[] ventCoords = line.Split(" -> ");
            int[] ventCoordsStart = ventCoords[0].Split(',').Select(Int32.Parse).ToArray();
            int[] ventCoordsEnd = ventCoords[1].Split(',').Select(Int32.Parse).ToArray();
            int length = Math.Max(Math.Abs(ventCoordsStart[0] - ventCoordsEnd[0]), Math.Abs(ventCoordsStart[1] - ventCoordsEnd[1]));

            for (int i = 0; i <= length; i++)
            {
                int x = (ventCoordsStart[0] < ventCoordsEnd[0]) ? ventCoordsStart[0] + i : ((ventCoordsStart[0] > ventCoordsEnd[0]) ? ventCoordsStart[0] - i : ventCoordsStart[0]);
                int y = (ventCoordsStart[1] < ventCoordsEnd[1]) ? ventCoordsStart[1] + i : ((ventCoordsStart[1] > ventCoordsEnd[1]) ? ventCoordsStart[1] - i : ventCoordsStart[1]);

                if (ventCoordsStart[0] == ventCoordsEnd[0] || ventCoordsStart[1] == ventCoordsEnd[1])
                {
                    AddVent(ventLinesTask1, x, y);
                }

                AddVent(ventLinesTask2, x, y);
            }
        }

        int twoOrMore = 0;

        foreach(KeyValuePair<(int x, int y), int> keyValuePair in ventLinesTask1)
        {
            if (keyValuePair.Value >= 2) twoOrMore++;
        }

        Console.WriteLine($"Task 1: {twoOrMore}");

        twoOrMore = 0;

        foreach (KeyValuePair<(int x, int y), int> keyValuePair in ventLinesTask2)
        {
            if (keyValuePair.Value >= 2) twoOrMore++;
        }

        Console.WriteLine($"Task 2: {twoOrMore}");
    }

    private static void AddVent(Dictionary<(int x, int y), int> ventLines, int x, int y)
    {
        if(!ventLines.TryAdd((x,y), 1))
        {
            ventLines[(x, y)]++;
        }
    }
}