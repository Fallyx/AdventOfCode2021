using System.Text.RegularExpressions;

namespace AdventOfCode2021.Day12;

internal class Day12
{
    const string inputPath = @"Day12/Input.txt";

    public static void Task1()
    {
        Dictionary<string, List<string>> links = new Dictionary<string, List<string>>();
        List<string> lines = File.ReadAllLines(inputPath).ToList();

        foreach (string line in lines)
        {
            string[] link = line.Split('-');
            if(!links.TryAdd(link[0], new List<string> { link[1] }))
                links[link[0]].Add(link[1]);
            if(!links.TryAdd(link[1], new List<string> { link[0] }))
                links[link[1]].Add(link[0]);
        }

        List<string> paths = new List<string>();
        GetPath(links, "start", "start", paths);
        Console.WriteLine($"Task 1: {paths.Count}");

        List<string> paths2 = new List<string>();

        foreach(string key in links.Keys)
        {
            if (key != "start" && key != "end" && key.Any(char.IsLower))
            {
                GetPathTask2(links, "start", "start", paths2, key);
            } 
        }

        Console.WriteLine($"Task 2: {paths2.Distinct().Count()}");
    }

    private static void GetPath(Dictionary<string, List<string>> links, string key, string currentPath, List<string> pathsToEnd)
    {
        for(int i = 0; i < links[key].Count; i++)
        {
            string cave = links[key][i];
            if (cave == "end")
            {
                pathsToEnd.Add(currentPath + $",{cave}");
            }
            else if (cave.Any(char.IsLower))
            {
                if (!currentPath.Contains(cave))
                    GetPath(links, cave, $"{currentPath},{cave}", pathsToEnd);
            }
            else
            {
                GetPath(links, cave, $"{currentPath},{cave}", pathsToEnd);
            }
        }
    }

    private static void GetPathTask2(Dictionary<string, List<string>> links, string key, string currentPath, List<string> pathsToEnd, string smallCaveTwice)
    {
        for (int i = 0; i < links[key].Count; i++)
        {
            string cave = links[key][i];
            if (cave == "end")
            {
                pathsToEnd.Add(currentPath + $",{cave}");
            }
            else if (cave == smallCaveTwice && Regex.Matches(currentPath, cave).Count == 1)
            { 
                    GetPathTask2(links, cave, $"{currentPath},{cave}", pathsToEnd, smallCaveTwice);
            }
            else if (cave.Any(char.IsLower))
            {
                if (!currentPath.Contains(cave))
                    GetPathTask2(links, cave, $"{currentPath},{cave}", pathsToEnd, smallCaveTwice);
            }
            else
            {
                GetPathTask2(links, cave, $"{currentPath},{cave}", pathsToEnd, smallCaveTwice);
            }
        }
    }
}
