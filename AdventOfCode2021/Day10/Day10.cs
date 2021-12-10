namespace AdventOfCode2021.Day10;
internal class Day10
{
    const string inputPath = @"Day10/Input.txt";

    public static void Task1and2()
    {
        Dictionary<char,(char pair, int score)> corruptedScore = new Dictionary<char, (char, int)>();
        corruptedScore.Add(')', ('(', 3));
        corruptedScore.Add(']', ('[', 57));
        corruptedScore.Add('}', ('{', 1197));
        corruptedScore.Add('>', ('<', 25137));

        Dictionary<char, int> incompleteScore = new Dictionary<char, int>();
        incompleteScore.Add('(', 1);
        incompleteScore.Add('[', 2);
        incompleteScore.Add('{', 3);
        incompleteScore.Add('<', 4);

        List<string> lines = File.ReadAllLines(inputPath).ToList();
        int errorScore = 0;
        List<long>autocompleteScores = new List<long>();
        Stack<char> chunk = new Stack<char>();

        for (int i = lines.Count - 1; i >= 0; i--)
        {
            foreach(char c in lines[i])
            {
                if (incompleteScore.ContainsKey(c))
                    chunk.Push(c);
                else if (corruptedScore.ContainsKey(c))
                {
                    char openChunk = chunk.Pop();
                    if (openChunk != corruptedScore[c].pair)
                    {
                        errorScore += corruptedScore[c].score;
                        lines.RemoveAt(i);
                        break;
                    }
                }
            }
            chunk.Clear();
        }

        Console.WriteLine($"Task 1: {errorScore}");

        foreach(string line in lines)
        {
            foreach (char c in line)
            {
                if (incompleteScore.ContainsKey(c))
                    chunk.Push(c);
                else if (corruptedScore.ContainsKey(c))
                    chunk.Pop();
            }

            long score = 0;
            while(chunk.Count > 0)
            {
                char openChunk = chunk.Pop();
                score = score * 5L + incompleteScore[openChunk];
            }

            autocompleteScores.Add(score);
        }
        autocompleteScores.Sort();
        Console.WriteLine($"Task 2: {autocompleteScores[autocompleteScores.Count / 2]}");
    }
}
