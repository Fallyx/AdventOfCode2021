
namespace AdventOfCode2021.Day15;
internal class Day15
{
    const string inputPath = @"Day15/Input.txt";
    static readonly (int y, int x)[] adjacents =
    {
        (-1,  0),
        ( 0, -1),
        ( 0, +1),
        (+1,  0)
    };

    public static void Task1()
    {
        List<string> lines = File.ReadAllLines(inputPath).ToList();
        Dictionary<(int y, int x), CaveNode> nodes = new Dictionary<(int y, int x), CaveNode>();
        (int X, int Y) target = (lines[0].Length - 1, lines.Count - 1);
        for(int y = 0; y < lines.Count; y++)
        {
            for (int x = 0; x < lines[y].Length; x++)
            {
                nodes.Add((y, x), new CaveNode(x, y, Int32.Parse(lines[y][x].ToString())));
            }
        }

        nodes[(0, 0)].DistanceFromStart = 0;

        Console.WriteLine();

        Queue<CaveNode> queue = new Queue<CaveNode>();
        HashSet<CaveNode> visited = new HashSet<CaveNode>();

        queue.Enqueue(nodes[(0, 0)]);
        visited.Add(nodes[(0, 0)]);

        while(queue.Count > 0)
        {
            CaveNode currentNode = queue.Dequeue();

            

            if (currentNode.X == target.X && currentNode.Y == target.Y)
            {
                Console.WriteLine(currentNode.DistanceFromStart);
                break;
            }

            for(int i = 0; i < adjacents.Length; i++)
            {
                int newY = currentNode.Y + adjacents[i].y;
                int newX = currentNode.X + adjacents[i].x;

                if (newY < 0 || newX < 0 || newY > target.Y || newX > target.X) continue;

                CaveNode nextNode = nodes[(newY, newX)];
                int distanceToNext = currentNode.DistanceFromStart + nextNode.RiskLevel;

                if (visited.Add(nextNode) || nextNode.DistanceFromStart > distanceToNext)
                {
                    nextNode.DistanceFromStart = distanceToNext;
                 //   Console.WriteLine($"Current Node X: {currentNode.X} Y: {currentNode.Y} Risk level: {currentNode.RiskLevel} Distance: {currentNode.DistanceFromStart}");
                 //   Console.WriteLine($"Next Node    X: {nextNode.X} Y: {nextNode.Y} Risk level: {nextNode.RiskLevel} Distance: {nextNode.DistanceFromStart}\n");
                    queue.Enqueue(nextNode);
                }
            }
        }

        Console.WriteLine();
    }
}

internal class CaveNode
{
    public int X { get; set; }
    public int Y { get; set; }
    public int RiskLevel { get; set; }
    public int DistanceFromStart { get; set; }

    public CaveNode(int x, int y, int riskLevel)
    {
        X = x; 
        Y = y; 
        RiskLevel = riskLevel; 
        DistanceFromStart = int.MaxValue;
    }
}