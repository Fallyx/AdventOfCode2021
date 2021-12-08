namespace AdventOfCode2021.Day08;

internal class Day08
{
    const string inputPath = @"Day08/Input.txt";

    public static void Task1()
    {
        List<string> lines = File.ReadAllLines(inputPath).ToList();
        int counter1478 = 0;

        foreach (string line in lines)
        {
            string[] outputValues = line.Split(" | ").Last().Split(' ');
            foreach(string value in outputValues)
            {
                if (value.Length == 2 || value.Length == 4 || value.Length == 3 || value.Length == 7)
                {
                    counter1478++;
                }
            }
        }

        Console.WriteLine($"Task 1: {counter1478}");
    }

    public static void Task2()
    {
        List<string> lines = File.ReadAllLines(inputPath).ToList();
        int outputValueSum = 0;

        foreach(string line in lines)
        {
            string[] nums = line.Split(" | ").First().Split(' ');
            string[] outputValues = line.Split(" | ").Last().Split(' ');

            // First identify 1, 4, 7, 8
            string[] orderedNums = new string[10];

            List<string> fiveSegments = new List<string>();
            List<string> sixSegments = new List<string>();

            foreach (string num in nums)
            {
                if (num.Length == 2) orderedNums[1] = num;
                else if (num.Length == 4) orderedNums[4] = num;
                else if (num.Length == 3) orderedNums[7] = num;
                else if (num.Length == 7) orderedNums[8] = num;
                else if (num.Length == 5) fiveSegments.Add(num);
                else if (num.Length == 6) sixSegments.Add(num);
            }

            // Identify nums with 5 segments
            foreach(string num in fiveSegments)
            {
                if (num.Contains(orderedNums[1][0]) && num.Contains(orderedNums[1][1])) orderedNums[3] = num;
                else if (IsTwoOrSix(orderedNums[4], num, 2)) orderedNums[2] = num;
                else orderedNums[5] = num;
            }

            // Identify nums with 6 segments
            foreach(string num in sixSegments)
            {
                if (num.Contains(orderedNums[4][0]) && num.Contains(orderedNums[4][1]) && num.Contains(orderedNums[4][2]) && num.Contains(orderedNums[4][3])) 
                    orderedNums[9] = num;
                else if (IsTwoOrSix(orderedNums[1], num, 1)) 
                    orderedNums[6] = num;
                else 
                    orderedNums[0] = num;
            }

            outputValueSum += DecodeOutputValue(orderedNums, outputValues);
        }

        Console.WriteLine($"Task 2: {outputValueSum}");
    }

    // containsConstraint = 2 -> is two | = 1 -> is six
    private static bool IsTwoOrSix(string fourSix, string num, int containsConstraint)
    {
        int containsCounter = 0;

        foreach(char c in fourSix)
        {
            if (num.Contains(c)) containsCounter++;
        }

        return (containsCounter == containsConstraint);
    }

    private static int DecodeOutputValue(string[] orderedNums, string[] outputValues)
    {
        string realValue = "";
        foreach(string val in outputValues)
        {
            char[] valChar = val.ToCharArray();
            Array.Sort(valChar);
            for(int i = 0; i < orderedNums.Length; i++)
            {
                char[] numChar = orderedNums[i].ToCharArray();
                Array.Sort(numChar);
                if (valChar.SequenceEqual(numChar)) realValue += i;
            }
        }

        return Int32.Parse(realValue);
    }
}
