namespace AdventOfCode2021.Day20;
internal class Day20
{
    const string inputPath = @"Day20/Input.txt";
    static readonly (int y, int x)[] adjacents =
    {
        (-1, -1),
        (-1,  0),
        (-1,  1),
        ( 0, -1),
        ( 0,  0),
        ( 0,  1),
        ( 1, -1),
        ( 1,  0),
        ( 1,  1)
    };

    public static void Task1()
    {
        Console.WriteLine($"Task 1: {EnhanceImage(2)}");
    }
    
    public static void Task2()
    {
        Console.WriteLine($"Task 2: {EnhanceImage(50)}");
    }

    public static int EnhanceImage(int steps)
    {
        string imgEnhancementAlg = File.ReadAllLines(inputPath).First();
        List<string> inputs = File.ReadAllLines(inputPath).Skip(2).ToList();
        Dictionary<(int y, int x), char> image = new Dictionary<(int y, int x), char>();
        bool firstCharLit = (imgEnhancementAlg[0] == '.' ? false : true);

        for (int y = 0; y < inputs.Count; y++)
        {
            for (int x = 0; x < inputs[y].Length; x++)
            {
                image.Add((y, x), inputs[y][x]);
            }
        }

        int minY = -2;
        int maxY = inputs.Count + 1;
        int minX = -2;
        int maxX = inputs[0].Length + 1;

        for (int i = 0; i < steps; i++)
        {
            Dictionary<(int y, int x), char> processedImage = new Dictionary<(int y, int x), char>();

            for (int y = minY; y <= maxY; y++)
            {
                for (int x = minX; x <= maxX; x++)
                {
                    string binaryNum = "";
                    for (int a = 0; a < adjacents.Length; a++)
                    {
                        int scanY = y + adjacents[a].y;
                        int scanX = x + adjacents[a].x;

                        char c;
                        if (!image.TryGetValue((scanY, scanX), out c))
                        {
                            if (firstCharLit && i % 2 == 0)
                                binaryNum += "0";
                            else if (firstCharLit)
                                binaryNum += "1";
                            else
                                binaryNum += "0";

                            continue;
                        }

                        binaryNum += (c == '.' ? "0" : "1");
                    }

                    int enhanceIdx = Convert.ToInt32(binaryNum, 2);
                    processedImage.Add((y, x), imgEnhancementAlg[enhanceIdx]);
                }
            }

            image = processedImage;
            minX--;
            minY--;
            maxX++;
            maxY++;
        }

        return image.Count(c => c.Value == '#');
    }

    private static void PrintImage(Dictionary<(int y, int x), char> image)
    {
        for(int y = image.Min(k => k.Key.y); y < image.Max(k => k.Key.y); y++)
        {
            for (int x = image.Min(k => k.Key.x); x < image.Max(k => k.Key.x); x++)
            {
                if (image.ContainsKey((y, x)))
                    Console.Write(image[(y, x)]);
                else
                    Console.Write('.');
            }
            Console.WriteLine();
        }
        Console.WriteLine();
    }
}
