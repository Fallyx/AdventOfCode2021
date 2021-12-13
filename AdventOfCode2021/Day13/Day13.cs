namespace AdventOfCode2021.Day13;
internal class Day13
{
    const string inputPath = @"Day13/Input.txt";

    public static void Task1and2()
    {
        List<string> lines = File.ReadAllLines(inputPath).ToList();
        List<(int x, int y)> coordinates = new List<(int x, int y)>();
        bool isX = false;

        while (lines.Count > 0)
        {
            if (string.IsNullOrEmpty(lines[0]))
            {
                lines.RemoveAt(0);
                break;
            }

            string[] xy = lines[0].Split(',');
            coordinates.Add((Int32.Parse(xy[0]), Int32.Parse(xy[1])));
            lines.RemoveAt(0);
        }

        bool firstFold = true;
        while(lines.Count > 0)
        {
            string[] fold = lines[0].Split('=');
            isX = fold[0][fold[0].Length - 1] == 'x';
            int foldXY = Int32.Parse(fold[1]);

            List<(int x, int y)> temp = new List<(int, int)>();
            for (int i = coordinates.Count - 1; i >= 0; i--)
            {
                if (isX && coordinates[i].x > foldXY)
                {
                    (int newX, int newY) = (foldXY - (coordinates[i].x - foldXY), coordinates[i].y);
                    temp.Add((newX, newY));
                    coordinates.RemoveAt(i);
                }
                else if (!isX && coordinates[i].y > foldXY)
                {
                    (int newX, int newY) = (coordinates[i].x, foldXY - (coordinates[i].y - foldXY));
                    temp.Add((newX, newY));
                    coordinates.RemoveAt(i);
                }
            }
            coordinates.AddRange(temp);
            lines.RemoveAt(0);

            if (firstFold)
            {
                Console.WriteLine($"Task 1: {coordinates.Distinct().Count()}");
                firstFold = false;
            }
        }

        Console.WriteLine("Task 2:");
        for(int y = 0; y <= coordinates.Max(c => c.y); y++)
        {
            for(int x = 0; x <= coordinates.Max(c => c.x); x++)
            {
                char c = (coordinates.Contains((x, y))) ? '#' : '.';
                Console.Write(c);
            }
            Console.WriteLine();
        }
    }
}
