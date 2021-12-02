
namespace AdventOfCode2021.Day01;

internal class Day01
{
    const string inputPath = @"Day01/Input.txt";

    public static void Task1and2()
    {
        List<int> depths = File.ReadAllLines(inputPath).ToList().ConvertAll(n => int.Parse(n.Trim()));

        Console.WriteLine($"Task 1: {CountIncrements(depths, 1)}");
        Console.WriteLine($"Task 2: {CountIncrements(depths, 3)}");
    }

    private static int CountIncrements(List<int> depths, int compareIdx)
    {
        int count = 0;
        for (int i = 0; i < depths.Count - compareIdx; i++)
        {
            if (depths[i] < depths[i + compareIdx])
            {
                count++;
            }
        }

        return count;
    }
}
