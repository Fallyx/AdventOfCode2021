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

        #region day 01
        swDay.Start();
        Day01.Day01.Task1and2();
        swDay.Stop();
        Console.WriteLine($"Day 01 elapsed time: {swDay.Elapsed}\n");
        #endregion

        #region day 02
        swDay.Start();
        Day02.Day02.Task1and2();
        swDay.Stop();
        Console.WriteLine($"Day 02 elapsed time: {swDay.Elapsed}\n");
        #endregion

        #region day 03
        swDay.Start();
        Day03.Day03.Task1();
        Day03.Day03.Task2();
        swDay.Stop();
        Console.WriteLine($"Day 03 elapsed time: {swDay.Elapsed}\n");
        #endregion

        swTot.Stop();
        Console.WriteLine($"\nTotal elapsed time: {swTot.Elapsed}\n");
    }

    private static void SingleRun(int day)
    {
        swDay.Start();

        switch (day)
        {
            case 1:
                Day01.Day01.Task1and2();
                break;
            case 2:
                Day02.Day02.Task1and2();
                break;
            case 3:
                Day03.Day03.Task1();
                Day03.Day03.Task2();
                break;
            default:
                break;
        }

        swDay.Stop();
        Console.WriteLine($"Day {day:D2} elapsed time: {swDay.Elapsed}\n");
    }
}