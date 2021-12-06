﻿using System.Diagnostics;

namespace AdventOfCode2021;

class Program
{
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
            Console.WriteLine("Run a day [1-25]:");
            input = Console.ReadLine();
            Console.WriteLine("---------------------------------\n");
        }

        bool success = int.TryParse(input, out int day);

        if (!success)
        {
            return;
        }

        SingleRun(day);
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
            case 4:
                Day04.Day04.Task1and2();
                break;
            case 5:
                Day05.Day05.Task1and2();
                break;
            case 6:
                Day06.Day06.Task1and2();
                break;
            default:
                break;
        }

        swDay.Stop();
        Console.WriteLine($"Day {day:D2} elapsed time: {swDay.Elapsed}\n");
    }
}