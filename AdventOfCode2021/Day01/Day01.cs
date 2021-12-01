

namespace AdventOfCode2021.Day01;

internal class Day01
{
    const string inputPath = @"Day01/Input.txt";

    public static void Task1and2()
    {
        //var depths = File.ReadAllLines(inputPath);
        List<int> depths = new List<int>();
        int countIncr = 0;
        int countSumIncr = 0;

        using (StreamReader reader = new StreamReader(inputPath))
        {
            string line;
            while((line = reader.ReadLine()) != null)
            {
                depths.Add(int.Parse(line));
            }
        }

        for (int i = 0; i < depths.Count - 1; i++)
        {
            if (depths[i] < depths[i + 1])
            {
                countIncr++;
            }
        }

        for (int i = 0; i < depths.Count - 3; i++)
        {
            if (depths[i] + depths[i+1] + depths[i+2] < depths[i + 1] + depths[i + 2] + depths[i + 3])
            {
                countSumIncr++;
            }
        }

        Console.WriteLine($"Task 1: {countIncr}");
        Console.WriteLine($"Task 2: {countSumIncr}");
    }
}
