using System.Text.RegularExpressions;

namespace AdventOfCode2021.Day17;

internal class Day17
{
    const string inputPath = @"Day17/Input.txt";

    public static void Task1and2()
    {
        string input = File.ReadAllLines(inputPath).First();
        HashSet<(int x, int y)> initialVectors = new HashSet<(int x, int y)>();

        // 0 = minX, 1 = maxX, 2 = minX, 3 = maxY
        int[] targetEdges = Regex.Matches(input, @"-?\d+").Select(m => Int32.Parse(m.Value)).ToArray();

        // Triangular Num
        int maxY = (Math.Abs(targetEdges[2]) - 1) * Math.Abs(targetEdges[2]) / 2;

        Console.WriteLine($"Task 1: {maxY}");

        for(int x = 1; x <= targetEdges[1]; x++)
        {
            if (x * (x + 1) / 2 < targetEdges[0]) continue;

            for (int y = targetEdges[2]; y <= Math.Abs(targetEdges[2]) - 1; y++)
            {
                (int currentX, int currentY) = (0, 0);
                (int currentVelX, int currentVelY) = (x, y);

                while (currentY >= targetEdges[2])
                {
                    currentX += currentVelX;
                    currentY += currentVelY;

                    if (currentX >= targetEdges[0] && currentX <= targetEdges[1] && currentY >= targetEdges[2] && currentY <= targetEdges[3])
                    {
                        initialVectors.Add((x, y));
                        break;
                    }

                    currentVelX += currentVelX > 0 ? -1 : 0;
                    currentVelY--;
                }
            }
        }

        Console.WriteLine($"Task 2: {initialVectors.Count}");
    }
}
