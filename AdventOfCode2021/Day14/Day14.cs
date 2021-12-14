namespace AdventOfCode2021.Day14;

internal class Day14
{
    const string inputPath = @"Day14/Input.txt";

    public static void Task1()
    {
        ProcessPolymer(1, 10);
    }

    public static void Task2()
    {
        ProcessPolymer(2, 40);
    }

    private static void ProcessPolymer(int taskNum, int steps)
    {
        List<string> lines = File.ReadAllLines(inputPath).ToList();
        HashSet<char> chars = new HashSet<char>();
        Dictionary<string, string> polymerReactions = GeneratePolymerReactions(lines.Skip(2).ToList(), chars);
        Dictionary<string, long> polymerPairs = GeneratePolymerPairs(polymerReactions, lines[0], steps);
        Console.WriteLine($"Task {taskNum}: {CalculateResult(polymerPairs, lines[0][0], chars)}");
    }

    private static Dictionary<string, string> GeneratePolymerReactions(List<string> lines, HashSet<char> chars)
    {
        Dictionary<string, string> polymerReactions = new Dictionary<string, string>();
        for (int i = 0; i < lines.Count; i++)
        {
            string[] pairInsertion = lines[i].Split(" -> ");
            polymerReactions.Add(pairInsertion[0], pairInsertion[1]);
            chars.Add(pairInsertion[0][0]);
            chars.Add(pairInsertion[0][1]);
            chars.Add(pairInsertion[1][0]);
        }

        return polymerReactions;
    }

    private static Dictionary<string, long> GeneratePolymerPairs(Dictionary<string, string> polymerReactions, string template, int steps)
    {
        Dictionary<string, long> polymerPairs = new Dictionary<string, long>();
        for (int i = 0; i < template.Length - 1; i++)
        {
            string key = $"{template[i]}{template[i + 1]}";
            if (!polymerPairs.TryAdd(key, 1))
                polymerPairs[key]++;
        }

        for (int i = 0; i < steps; i++)
        {
            Dictionary<string, long> temp = new Dictionary<string, long>();
            foreach (string key in polymerPairs.Keys)
            {
                long amount = polymerPairs[key];
                string key1 = key[0] + polymerReactions[key];
                string key2 = polymerReactions[key] + key[1];
                if (!temp.TryAdd(key1, amount))
                    temp[key1] += amount;
                if (!temp.TryAdd(key2, amount))
                    temp[key2] += amount;
            }

            polymerPairs = temp;
        }

        return polymerPairs;
    }

    private static long CalculateResult(Dictionary<string, long> polymerPairs, char firstChar, HashSet<char> chars)
    {
        long minChars = long.MaxValue;
        long maxChars = long.MinValue;

        foreach (char c in chars)
        {
            long count = 0;
            foreach (string key in polymerPairs.Keys)
            {
                if (!key.Contains(c)) continue;

                if (key[1] == c)
                    count += polymerPairs[key];
            }

            if (firstChar == c) count++;
            if (count < minChars) minChars = count;
            if (count > maxChars) maxChars = count;
        }

        return maxChars - minChars;
    }
}
