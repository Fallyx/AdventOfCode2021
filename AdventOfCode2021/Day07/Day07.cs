
namespace AdventOfCode2021.Day07;

internal class Day07
{
    const string inputPath = @"Day07/Input.txt";

    public static void Task1()
    {
        List<int> crabs = File.ReadAllLines(inputPath).First().Split(',').Select(Int32.Parse).ToList();
        crabs.Sort();

        int mid = crabs.Count / 2;
        int median = ((crabs.Count % 2) != 0) ? crabs[mid] : (crabs[mid - 1] + crabs[mid]) / 2;

        List<int> movedCrabs = crabs.Select(n => Math.Abs(n - median)).ToList();
        Console.WriteLine($"Task 1: {movedCrabs.Sum()}");
    }

    public static void Task2()
    {
        List<int> crabs = File.ReadAllLines(inputPath).First().Split(',').Select(Int32.Parse).ToList();
        crabs.Sort();
        int lowestFuelUsed = int.MaxValue;

        int meanFloor = crabs.Sum() / crabs.Count;
        int meanCeiling = (crabs.Sum() / crabs.Count + 1);
        
        for(int moves = meanFloor; moves < meanCeiling; moves++)
        {
            int fuelUsed = 0;
            for (int i = 0; i < crabs.Count; i++)
            {
                int steps = Math.Abs(crabs[i] - moves);
                fuelUsed += steps * (steps + 1) / 2;
            }
            if (fuelUsed < lowestFuelUsed) lowestFuelUsed = fuelUsed;
        }
        Console.WriteLine($"Task 2: {lowestFuelUsed}");
    }
}
