namespace AdventOfCode2021.Day14;

internal class Day14
{
    const string inputPath = @"Day14/Input.txt";

    public static void Task1()
    {
        List<string> lines = File.ReadAllLines(inputPath).ToList();
        string polymerTemplate = lines[0];
        Dictionary<string, string> polymerReaction = new Dictionary<string, string>();
        int steps = 10;

        for(int i = 2; i < lines.Count; i++)
        {
            string[] pairInsertion = lines[i].Split(" -> ");
            polymerReaction.Add(pairInsertion[0], pairInsertion[1]);
        }

        for (int i = 0; i < steps; i++)
        {
            for (int x = polymerTemplate.Length - 1; x >= 1; x--)
            {
                polymerTemplate = polymerTemplate.Insert(x, polymerReaction[polymerTemplate.Substring(x - 1, 2)]);
            }
        }

        char[] chars = polymerTemplate.Distinct().ToArray();

        int minChars = int.MaxValue;
        int maxChars = int.MinValue;

        foreach(char c in chars)
        {
            int count = polymerTemplate.Count(s => s == c);
            if (count < minChars) minChars = count;
            if (count > maxChars) maxChars = count;
        }

        Console.WriteLine($"Task 1: {maxChars - minChars}");
    }

    public static void Task2()
    {
        List<string> lines = File.ReadAllLines(inputPath).ToList();
        Dictionary<string, long> polymerPairs = new Dictionary<string, long>();
        Dictionary<string, string> polymerReaction = new Dictionary<string, string>();
        int steps = 40;

        for (int i = 0; i < lines[0].Length - 1; i++)
        {
            string key = lines[0][i] + "" + lines[0][i + 1];
            if (!polymerPairs.TryAdd(key, 1))
                polymerPairs[key]++;
        }

        for (int i = 2; i < lines.Count; i++)
        {
            string[] pairInsertion = lines[i].Split(" -> ");
            polymerReaction.Add(pairInsertion[0], pairInsertion[1]);
        }

        for (int i = 0; i < steps; i++)
        {
            Dictionary<string, long> temp = new Dictionary<string, long>();
            foreach (string key in polymerPairs.Keys)
            {
                long amount = polymerPairs[key];
                string key1 = key[0] + polymerReaction[key];
                string key2 = polymerReaction[key] + key[1];
                if (!temp.TryAdd(key1, amount))
                    temp[key1]+= amount;
                if (!temp.TryAdd(key2, amount))
                    temp[key2]+=amount;
            }

            polymerPairs = temp;
        }

        long minChars = long.MaxValue;
        long maxChars = long.MinValue;

        HashSet<char> chars = new HashSet<char>();

        foreach (string key in polymerPairs.Keys)
        {
            chars.Add(key[0]);
            chars.Add(key[1]);
        }

        foreach (char c in chars)
        {
            long count = 0;
            foreach (string key in polymerPairs.Keys)
            {
                if (!key.Contains(c)) continue;

                if (key[1] == c)
                    count += polymerPairs[key];
            }

            if (lines[0][0] == c) count++;

            if (count < minChars) minChars = count;
            if (count > maxChars) maxChars = count;
        }

        Console.WriteLine($"Task 2: {maxChars - minChars}");
    }
}
