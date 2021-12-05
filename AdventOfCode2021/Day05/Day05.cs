using System.Numerics;

namespace AdventOfCode2021.Day05;

internal class Day05
{
    const string inputPath = @"Day05/Input.txt";

    public static void Task1()
    {
        Dictionary<(int x, int y), int> ventLinesTask1 = new Dictionary<(int x, int y), int>();
        Dictionary<(int x, int y), int> ventLinesTask2 = new Dictionary<(int x, int y), int>();

        using (StreamReader reader = new StreamReader(inputPath))
        {
            string line;
            while((line = reader.ReadLine()) != null)
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
                        if (!ventLinesTask1.ContainsKey((x, y)))
                        {
                            ventLinesTask1.Add((x, y), 1);
                        }
                        else
                        {
                            ventLinesTask1[(x, y)]++;
                        }
                    }

                    if (!ventLinesTask2.ContainsKey((x, y)))
                    {
                        ventLinesTask2.Add((x, y), 1);
                    }
                    else
                    {
                        ventLinesTask2[(x, y)]++;
                    }
                }
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
}