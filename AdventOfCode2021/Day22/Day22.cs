using System.Text.RegularExpressions;

namespace AdventOfCode2021.Day22;
internal class Day22
{
    const string inputPath = @"Day22/Input.txt";

    public static void Task1()
    {
        List<string> lines = File.ReadAllLines(inputPath).ToList();
        
        Console.WriteLine($"Task 1: {RebootCores(lines)}");
    }

    private static long RebootCores(List<string> lines)
    {
        HashSet<(int x, int y, int z)> onCubes = new HashSet<(int x, int y, int z)>();
        foreach (string line in lines)
        {
            string[] toggle = line.Split(' ');
            int[] coordBoundaries = Regex.Matches(toggle[1], @"-?\d+").Select(m => Int32.Parse(m.Value)).ToArray();

            if (50 < coordBoundaries[0] || -50 > coordBoundaries[1] || 50 < coordBoundaries[1] || -50 > coordBoundaries[2] || 50 < coordBoundaries[3] || -50 > coordBoundaries[4])
                continue;

            coordBoundaries[0] = (coordBoundaries[0] < -50 ? -50 : coordBoundaries[0]);
            coordBoundaries[1] = (coordBoundaries[1] > 50 ? 50 : coordBoundaries[1]);
            coordBoundaries[2] = (coordBoundaries[2] < -50 ? -50 : coordBoundaries[2]);
            coordBoundaries[3] = (coordBoundaries[3] > 50 ? 50 : coordBoundaries[3]);
            coordBoundaries[4] = (coordBoundaries[4] < -50 ? -50 : coordBoundaries[4]);
            coordBoundaries[5] = (coordBoundaries[5] > 50 ? 50 : coordBoundaries[5]);            

            for (int x = coordBoundaries[0]; x <= coordBoundaries[1]; x++)
            {
                for (int y = coordBoundaries[2]; y <= coordBoundaries[3]; y++)
                {
                    for (int z = coordBoundaries[4]; z <= coordBoundaries[5]; z++)
                    {
                        if (toggle[0] == "on")
                            onCubes.Add((x, y, z));
                        else
                            onCubes.Remove((x, y, z));
                    }
                }
            }
        }

        return onCubes.Count;
    }

    public static void Task2()
    {
        List<string> lines = File.ReadAllLines(inputPath).ToList();


    }

    private static void RebootCoresV2(List<string> lines)
    {
        List<(Boundary X, Boundary Y, Boundary Z)> onCubeBoundaries = new List<(Boundary X, Boundary Y, Boundary Z)>();

        foreach (string line in lines)
        {
            string[] toggle = line.Split(' ');
            int[] coordBoundaries = Regex.Matches(toggle[1], @"-?\d+").Select(m => Int32.Parse(m.Value)).ToArray();

            
            //if ()
            
        }
    }

    class Boundary
    {
        int Min { get; set; }
        int Max { get; set; }
    }
}
