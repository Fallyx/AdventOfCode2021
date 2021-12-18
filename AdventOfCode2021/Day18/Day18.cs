namespace AdventOfCode2021.Day18;

internal class Day18
{
    const string inputPath = @"Day18/Input.txt";
    private static bool actionTaken = false;

    public static void Task1()
    {
        List<string> inputs = File.ReadAllLines(inputPath).ToList();

        int i = 0;
        Pair p = CreateSnailfishNumber(inputs.First(), ref i);

        bool fullyReduced = false;
        do
        {
            actionTaken = false;
            bool didExplode = true;
            while (didExplode)
                didExplode = ExplodePair(p);
            bool didSplit = true;
            if (!actionTaken)
            {
                
                while (didSplit)
                    didSplit = SplitPair(p);
            }
            fullyReduced = !didExplode && !didSplit;
        } while (actionTaken && !fullyReduced);

        /*bool fullyReduced = false;

        while(!fullyReduced)
        {
            bool didExplode = true;
            while (didExplode)
                didExplode = ExplodePair(p);

            bool didSplit = true;
            while (didSplit)
                didSplit = SplitPair(p);

            fullyReduced = !didExplode && !didSplit;
        }*/

        Console.WriteLine(p);
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

    private static bool ExplodePair(Pair p)
    {
        if (p.NestedLevel > 4)
        { 
            ExplodeLeft(p.Parent, p.Left);
            ExplodeRight(p.Parent, p.Right);

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
            return true;
        }
        else
        {
            bool didExplode = false;
            if (p.LeftPair != null)
                didExplode = ExplodePair(p.LeftPair);
            if (!didExplode && p.RightPair != null)
                didExplode = ExplodePair(p.RightPair);

            return didExplode;
        }
    }

    private static bool SplitPair(Pair p)
    {
        bool didSplitLeft = false;
        bool didSplitRight = false;
        if (p == null) return false;
        if (p.Left != null && p.Left >= 10)
        {
            int left = (int)Math.Floor((decimal)(p.Left / (decimal)2));
            int right = (int)Math.Ceiling((decimal)(p.Left / (decimal)2));

            Pair newP = new Pair(left, right);
            newP.NestedLevel = p.NestedLevel + 1;
            p.Left = null;
            p.LeftPair = newP;
            actionTaken = true;
            didSplitLeft = true;
        }
        else if (p.LeftPair != null)
        {
            didSplitLeft = SplitPair(p.LeftPair);
        }
        if (p.Right != null && p.Right >= 10)
        {
            int left = (int)Math.Floor((decimal)(p.Right / (decimal)2));
            int right = (int)Math.Ceiling((decimal)(p.Right / (decimal)2));

            Pair newP = new Pair(left, right);
            newP.NestedLevel = p.NestedLevel + 1;
            p.Right = null;
            p.RightPair = newP;
            actionTaken = true;
            didSplitRight = true;
        }
        else if (p.RightPair != null)
        {
            didSplitRight = SplitPair(p.RightPair);
        }

        return didSplitLeft || didSplitRight;
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
            ExplodeLeft(p.LeftPair, number);
        }
    }

    private static void ExplodeRight(Pair? p, int? number)
    {
        if (p == null) return;
        if (p.Right != null)
        {
            p.Right += number;
        }
        else if (p.Parent != null)
        {
            if (p.Parent.LeftPair != null && p.Parent.LeftPair == p && p.Parent.RightPair != null)
            {
                ExplodeLeftReverse(p.Parent.RightPair, number);
            }
            else if (p.Parent.RightPair != null && p.Parent.RightPair == p)
            {
                ExplodeRight(p.Parent, number);
            }
        }
    }

    class Pair
    {
        public int? Left { get; set; }
        public int? Right { get; set; }

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
            return $"[{(Left != null ? Left.ToString() : LeftPair.ToString())},{(Right != null ? Right.ToString() : RightPair.ToString())}]";
        }
    }
}

