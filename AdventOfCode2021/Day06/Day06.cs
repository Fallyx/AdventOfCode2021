
namespace AdventOfCode2021.Day06;

internal class Day06
{
    const string inputPath = @"Day06/Input.txt";

    public static void Task1and2()
    {
        List<int> fish = File.ReadAllLines(inputPath).First().Split(',').Select(Int32.Parse).ToList();
        Dictionary<int, long> DayFish = new Dictionary<int, long>();
        Dictionary<int, long> DayFish2 = new Dictionary<int, long>();
        int daysTask1 = 80;
        int daysTask2 = 256;

        foreach (int f in fish)
        {
            if(!DayFish.TryAdd(f, 1))
            {
                DayFish[f]++;
                
            }
            if (!DayFish2.TryAdd(f, 1))
            {
                DayFish2[f]++;
            }
        }
        
        DayFish = SimulateDays(DayFish, daysTask1);
        DayFish2 = SimulateDays(DayFish2, daysTask2);

        Console.WriteLine($"Task 1: {DayFish.Sum(n => n.Value)}");
        Console.WriteLine($"Task 2: {DayFish2.Sum(n => n.Value)}");
    }

    private static Dictionary<int, long> SimulateDays(Dictionary<int, long> DayFish, int days)
    {
        for (int i = 0; i < days; i++)
        {
            Dictionary<int, long> newFish = new Dictionary<int, long>();
            foreach (KeyValuePair<int, long> pair in DayFish)
            {
                if (pair.Key == 0)
                {
                    if (!newFish.TryAdd(8, pair.Value))
                    {
                        newFish[8] += pair.Value;
                    }
                    if (!newFish.TryAdd(6, pair.Value))
                    {
                        newFish[6] += pair.Value;
                    }
                }
                else
                {
                    if (!newFish.TryAdd(pair.Key - 1, pair.Value))
                    {
                        newFish[pair.Key - 1] += pair.Value;
                    }
                }
            }
            DayFish = newFish;
        }

        return DayFish;
    }
}

