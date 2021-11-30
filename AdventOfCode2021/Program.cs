using System;
using System.Diagnostics;


namespace AdventOfCode2021;

class Program
{
    private static Stopwatch swTot = new Stopwatch();
    private static Stopwatch swDay = new Stopwatch();

    static void Main(string[] args)
    {
        string input;
        if (args.Length != 0)
        {
            input = args[0];
        }
        else
        {
            Console.WriteLine("Run a single day [1-25] or [a]ll:");
            input = Console.ReadLine();
            Console.WriteLine("---------------------------------\n");
        }

        if (input == "a")
        {
            FullRun();
        }

        int day;
        bool success = int.TryParse(input, out day);

        if (!success)
        {
            return;
        }

        SingleRun(day);
    }

    private static void FullRun()
    {
        swTot.Start();

        swTot.Stop();
        Console.WriteLine($"\nTotal elapsed time: {swTot.Elapsed}\n");
    }

    private static void SingleRun(int day)
    {
        swDay.Start();

        switch (day)
        {
            default:
                break;
        }

        swDay.Stop();
        Console.WriteLine($"Day {day.ToString("D2")} elapsed time: {swDay.Elapsed}\n");
    }
}