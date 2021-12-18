namespace AdventOfCode2021.Day18;

internal class Day18
{
    const string inputPath = @"Day18/Input.txt";
    private static bool actionTaken = false;

    public static void Task1()
    {
        List<string> inputs = File.ReadAllLines(inputPath).ToList();

        int i = 0;
        Pair snailfishNumber = CreateSnailfishNumber(inputs.First(), ref i);

        Console.WriteLine(snailfishNumber);

        for (int j = 1; j < inputs.Count; j++)
        {
            i = 0;
            Pair addition = CreateSnailfishNumber(inputs[j], ref i);
            snailfishNumber = AddSnailfishNumbers(snailfishNumber, addition);

            Console.WriteLine(snailfishNumber);

            do
            {
                actionTaken = false;
                ExplodePair(snailfishNumber);
                if (!actionTaken)
                    SplitPair(snailfishNumber);
            } while (actionTaken);
        }

        Console.WriteLine(snailfishNumber);
    }

    private static Pair CreateSnailfishNumber(string line, ref int i, int nestedLevel = 1, Pair parent = null)
    {
        Pair p = new Pair();
        p.NestedLevel = nestedLevel;
        p.Parent = parent;
        bool isLeft = true;
        while(i < line.Length)
        {
            i++;
            if (line[i] == ']')
            {
                break;
            } 
            else if (line[i] == '[')
            {
                if (isLeft)
                    p.LeftPair = CreateSnailfishNumber(line, ref i, nestedLevel + 1, p);
                else
                    p.RightPair = CreateSnailfishNumber(line, ref i, nestedLevel + 1, p);
            }
            else if (line[i] == ',')
            {
                isLeft = false;
            }
            else
            {
                if (isLeft)
                    p.Left = Int32.Parse(line[i].ToString());
                else
                    p.Right = Int32.Parse(line[i].ToString());
            }
        }

        return p;
    }

    private static void ExplodePair(Pair p)
    {
        if (actionTaken) return;
        if (p.NestedLevel > 4)
        { 
            ExplodeLeft(p.Parent, p.Left);
            ExplodeRight(p.Parent, p, p.Right);

            if (p.Parent.LeftPair == p)
            {
                p.Parent.LeftPair = null;
                p.Parent.Left = 0;
            }
            else if (p.Parent.RightPair == p)
            {
                p.Parent.RightPair = null;
                p.Parent.Right = 0;
            }

            actionTaken = true;
        }
        else
        {
            if (p.LeftPair != null)
                ExplodePair(p.LeftPair);
            if (!actionTaken && p.RightPair != null)
                ExplodePair(p.RightPair);
        }
    }

    private static void SplitPair(Pair p)
    {
        if (actionTaken) return;
        if (p == null) return;
        if (p.Left != null && p.Left >= 10)
        {
            int left = (int)Math.Floor((decimal)(p.Left / (decimal)2));
            int right = (int)Math.Ceiling((decimal)(p.Left / (decimal)2));

            Pair newP = new Pair(left, right);
            newP.NestedLevel = p.NestedLevel + 1;
            newP.Parent = p;
            p.Left = null;
            p.LeftPair = newP;
            actionTaken = true;
        }
        else if (p.LeftPair != null)
        {
            SplitPair(p.LeftPair);
        }
        if (!actionTaken && p.Right != null && p.Right >= 10)
        {
            int left = (int)Math.Floor((decimal)(p.Right / (decimal)2));
            int right = (int)Math.Ceiling((decimal)(p.Right / (decimal)2));

            Pair newP = new Pair(left, right);
            newP.NestedLevel = p.NestedLevel + 1;
            newP.Parent = p;
            p.Right = null;
            p.RightPair = newP;
            actionTaken = true;
        }
        else if (!actionTaken && p.RightPair != null)
        {
            SplitPair(p.RightPair);
        }
    }

    private static void Explode(Pair? p, int? number, bool isLeft)
    {
        if (p == null) return;
        if (isLeft && p.Left != null)
        {
            p.Left += number;
        }
        else if (isLeft && p.Parent != null)
        {
            Explode(p.Parent, number, isLeft);
        }
        else if (!isLeft && p.Right != null)
        {
            p.Right += number;
        }
        else if (!isLeft && p.Parent != null)
        {
            Explode(p.Parent, number, isLeft);
        }
    }

    private static void ExplodeLeft(Pair? p, int? number)
    {
        if (p == null) return;
        if (p.Left != null)
        {
            p.Left += number;
        }
        else if (p.Parent != null)
        {
            ExplodeLeft(p.Parent, number);
        }
    }

    private static void ExplodeLeftReverse(Pair? p, int? number)
    {
        if (p == null) return;
        if (p.Left != null)
        {
            p.Left += number;
        }
        else if (p.LeftPair != null)
        {
            ExplodeLeftReverse(p.LeftPair, number);
        }
    }

    private static void ExplodeRight(Pair? p, Pair? child, int? number)
    {
        if (p == null) return;
        if (p.Right != null)
        {
            p.Right += number;
        }
        else if (p.LeftPair == child && p.RightPair != null)
        {
            ExplodeLeftReverse(p.RightPair, number);
        }
        else if (p.Parent != null)
        {
            if (p.Parent.LeftPair != null && p.Parent.LeftPair == p && p.Parent.RightPair != null)
            {
                Console.WriteLine($"ExplodeLeftReverse {p}");
                ExplodeLeftReverse(p.Parent.RightPair, number);
            }
            else if (p.Parent.RightPair != null && p.Parent.RightPair == p)
            {
                Console.WriteLine($"ExplodeRight again: {p}");
                ExplodeRight(p.Parent, p, number);
            }
        }
    }

    private static Pair AddSnailfishNumbers(Pair? p1, Pair? p2)
    {
        UpdateNestedLevel(p1);
        UpdateNestedLevel(p2);

        Pair root = new Pair();
        p1.Parent = root;
        p2.Parent = root;
        root.LeftPair = p1;
        root.RightPair = p2;

        return root;
    }

    private static void UpdateNestedLevel(Pair? p)
    {
        if (p == null) return;
        p.NestedLevel++;
        if (p.LeftPair != null)
        {
            UpdateNestedLevel(p.LeftPair);
        }
        if (p.RightPair != null)
        {
            UpdateNestedLevel(p.RightPair);
        }
    }

    class Pair
    {
        public int? Left { get; set; }
        public int? Right { get; set; }

        public int? Value { get; set; }

        public Pair? LeftPair { get; set; }
        public Pair? RightPair { get; set; }

        public int NestedLevel { get; set; }

        public Pair? Parent { get; set; }

        public Pair() { }
        public Pair(int l, int r)
        {
            Left = l;
            Right = r;
        }

        public override string ToString()
        {
            if (Value != null)
            {
                return Value.ToString();
            }

            return $"[{LeftPair.ToString()},{RightPair.ToString()}]";
        }
    }
}

