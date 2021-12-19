namespace AdventOfCode2021.Day18;
internal class Day18
{
    const string inputPath = @"Day18/Input.txt";
    public static int explodeCounter = 0;

    public static void Task1()
    {
        List<string> inputs = File.ReadAllLines(inputPath).ToList();
        string snailfishNumber = inputs[0];

        for(int i = 1; i < inputs.Count; i++)
        {
            snailfishNumber = AddSnailfishNumber(snailfishNumber, inputs[i]);
            snailfishNumber = Reduce(snailfishNumber);
        }

        Console.WriteLine($"Task 1: {Magnitude(snailfishNumber)}");
    }

    public static void Task2()
    {
        List<string> inputs = File.ReadAllLines(inputPath).ToList();
        int largestMagnitude = 0;

        for(int left = 0; left < inputs.Count; left++)
        {
            for(int right = 0; right < inputs.Count; right++)
            {
                if (left == right) continue;

                string snailfishNumber = AddSnailfishNumber(inputs[left], inputs[right]);
                snailfishNumber = Reduce(snailfishNumber);

                int magnitude = Int32.Parse(Magnitude(snailfishNumber));
                if (largestMagnitude < magnitude) largestMagnitude = magnitude;
            }
        }

        Console.WriteLine($"Task 2: {largestMagnitude}");
    }

    private static string Magnitude(string line)
    {
        while (line.Contains('['))
        {
            int leftIdx = -1;
            int depth = 0;
            bool midFound = false;

            for (int i = 0; i < line.Length; i++)
            {
                if (line[i] == '[')
                {
                    depth++;
                    leftIdx = -1;
                    midFound = false;
                }
                else if (line[i] == ']')
                {
                    depth--;
                    leftIdx = -1;
                    midFound = false;
                }
                else if (line[i] == ',')
                    midFound = true;
                else if (IsDigit(line[i]) && !midFound && leftIdx == -1)
                    leftIdx = i;
                else if (IsDigit(line[i]) && midFound)
                {
                    line = CalcMagnitude(line, leftIdx, i);
                    break;
                }
            }
        }

        return line;
    }

    private static string CalcMagnitude(string line, int leftIdx, int rightIdx)
    {
        int left = GetNumber(line, leftIdx);
        int right = GetNumber(line, rightIdx);
        string leftStr = line.Substring(0, leftIdx - 1);
        string rightStr = line.Substring(rightIdx + 1 + right.ToString().Length);
        left *= 3;
        right *= 2;
        int value = left + right;

        return $"{leftStr}{value}{rightStr}";
    }

    private static string Reduce(string line)
    {
        int depth = 0;
        for (int i = 0; i < line.Length; i++)
        {
            if (line[i] == '[' && depth == 4)
                return Reduce(Explode(line, i));
            else if (line[i] == '[')
                depth++;
            else if (line[i] == ']')
                depth--;
        }

        for(int i = 0; i < line.Length; i++)
        {
            if (IsDigit(line[i]))
            {
                int number = GetNumber(line, i);
                if (number > 9)
                    return Reduce(Split(line, i, number));
            }
        }

        return line;
    }

    private static string Split(string line, int startIdx, int number)
    {
        int left = (int)Math.Floor(number / (decimal)2);
        int right = (int)Math.Ceiling(number / (decimal)2);
        string splitted = $"[{left},{right}]";
        string leftStr = line.Substring(0, startIdx);
        string rightStr = line.Substring(startIdx + number.ToString().Length);

        return $"{leftStr}{splitted}{rightStr}";
    }

    private static string Explode(string line, int startIdx)
    {
        int depth = 1;
        int mid = 0;
        int leftIdx = -1;
        int rightIdx = -1;
        for (int i = startIdx + 1; i < line.Length; i++)
        {
            if (line[i] == '[')
                depth++;
            else if (line[i] == ']')
                depth--;
            else if (depth == 1 && line[i] == ',')
                mid = i;
            else if (IsDigit(line[i]) && leftIdx == -1 && mid == 0)
                leftIdx = i;
            else if (IsDigit(line[i]) && rightIdx == -1 && mid != 0)
            {
                rightIdx = i;
                break;
            }

            if (depth == 0)
                break;
        }

        int left = GetNumber(line, leftIdx);
        int right = GetNumber(line, rightIdx);
        string pre = line.Substring(0, startIdx);
        string post = line.Substring(rightIdx + right.ToString().Length + 1);
        string pre1 = InsertRight(pre, left);
        string post1 = InsertLeft(post, right);

        return $"{pre1}0{post1}";
    }

    private static String InsertLeft(string line, int value)
    {
        int i = 0;
        int left = -1;

        for(; i < line.Length; i++)
        {
            if (IsDigit(line[i]))
            {
                left = i;
                break;
            }
        }

        if (left == -1)
            return line;

        for (; i < line.Length; i++)
        {
            if (!IsDigit(line[i]))
            {
                string leftStr = line.Substring(0, left);
                int number = GetNumber(line, left);
                value += number;
                string rightStr = line.Substring(i);

                return $"{leftStr}{value}{rightStr}";
            }
        }

        return line;
    }

    private static String InsertRight(string line, int value)
    {
        int i = line.Length - 1;
        int right = -1;

        for (; i >= 0; i--)
        {
            if (IsDigit(line[i]))
            {
                right = i;
                break;
            }
        }

        if (right == -1)
            return line;
        
        for (; i >= 0; i--)
        {
            if (!IsDigit(line[i]))
            {
                string leftStr = line.Substring(0, i + 1);
                int number = GetNumber(line, i + 1);
                value += number;
                string rightStr = line.Substring(right + 1);

                return $"{leftStr}{value}{rightStr}";
            }
        }

        return line;
    }

    private static int GetNumber(string line, int startIdx)
    {
        int i = startIdx;
        while(IsDigit(line[i]))
            i++;

        if (i != startIdx)
            return Int32.Parse(line.Substring(startIdx, i - startIdx));

        return -1;
    }

    private static bool IsDigit(char c)
    {
        return !(c == '[' || c == ']' || c == ',');
    }

    private static string AddSnailfishNumber(string left, string right)
    {
        return $"[{left},{right}]";
    }
}
