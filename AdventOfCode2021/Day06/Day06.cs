namespace AdventOfCode2021.Day06;

internal class Day06
{
    const string inputPath = @"Day06/Input.txt";

    public static void Task1and2()
    {
        List<int> fish = File.ReadAllLines(inputPath).First().Split(',').Select(Int32.Parse).ToList();
        long[] dayFish = new long[9];
        long[] dayFish2 = new long[9];
        int daysTask1 = 80;
        int daysTask2 = 256;

        foreach (int f in fish)
        {
            dayFish[f]++;
            dayFish2[f]++;
        }
        
        dayFish = SimulateDays(dayFish, daysTask1);
        dayFish2 = SimulateDays(dayFish2, daysTask2);

        Console.WriteLine($"Task 1: {dayFish.Sum()}");
        Console.WriteLine($"Task 2: {dayFish2.Sum()}");
    }

    private static long[] SimulateDays(long[] dayFish, int days)
    {
        for (int day = 0; day < days; day++)
        {
            long[] newFish = new long[9];
            for (int i = 0; i < dayFish.Length; i++)
            {
                if (i == 0)
                {
                    newFish[8] = dayFish[i];
                    newFish[6] = dayFish[i];
                }
                else
                {
                    newFish[i - 1] += dayFish[i];
                }
            }
            dayFish = newFish;
        }

        return dayFish;
    }
}
