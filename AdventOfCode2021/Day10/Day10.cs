namespace AdventOfCode2021.Day10;
internal class Day10
{
    const string inputPath = @"Day10/Input.txt";

    public static void Task1and2()
    {
        Chunk[] chunkValues = new Chunk[]
        {
            new Chunk('(', ')', 3, 1),
            new Chunk('[', ']', 57, 2),
            new Chunk('{', '}', 1197, 3),
            new Chunk('<', '>', 25137, 4)
        };

        List<string> lines = File.ReadAllLines(inputPath).ToList();
        int errorScore = 0;
        bool isCorruptLine = false;
        List<long>autocompleteScores = new List<long>();
        Stack<char> chunk = new Stack<char>();

        foreach(string line in lines)
        {
            foreach(char c in line)
            {
                if (chunkValues.Any(v => v.OpenChunk == c))
                    chunk.Push(c);
                else if (chunkValues.Any(v => v.CloseChunk == c))
                {
                    Chunk foundChunk = chunkValues.First(v => v.CloseChunk == c);
                    char openChunk = chunk.Pop();
                    if (openChunk != foundChunk.OpenChunk)
                    {
                        errorScore += foundChunk.CorruptedScore;
                        isCorruptLine = true;
                        break;
                    }
                }
            }

            if(!isCorruptLine)
            {
                long score = 0;
                while (chunk.Count > 0)
                {
                    char openChunk = chunk.Pop();
                    score = score * 5L + chunkValues.First(v => v.OpenChunk == openChunk).AutocompleteScore;
                }
                autocompleteScores.Add(score);
            }
            isCorruptLine = false;
            chunk.Clear();
        }

        Console.WriteLine($"Task 1: {errorScore}");
        autocompleteScores.Sort();
        Console.WriteLine($"Task 2: {autocompleteScores[autocompleteScores.Count / 2]}");
    }

    struct Chunk
    {
        public char OpenChunk { get; set; }
        public char CloseChunk { get; set; }
        public int CorruptedScore { get; set; }
        public int AutocompleteScore { get; set; }

        public Chunk(char open, char close, int corruptedScore, int autocompleteScore)
        {
            OpenChunk = open;
            CloseChunk = close;
            CorruptedScore = corruptedScore;
            AutocompleteScore = autocompleteScore;
        }
    }
}
