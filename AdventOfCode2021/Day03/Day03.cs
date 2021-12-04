namespace AdventOfCode2021.Day03;
internal class Day03
{
    const string inputPath = @"Day03/Input.txt";

    public static void Task1()
    {
        string[] binaryNums = File.ReadAllLines(inputPath);

        string gamma = "";
        string epsilon = "";

        for (int i = 0; i < binaryNums[0].Length; i++)
        {
            int count0 = 0;
            int count1 = 0;
            for (int x = 0; x < binaryNums.Length; x++)
            {
                _ = (binaryNums[x][i] == '0') ? count0++ : count1++;
            }

            if (count0 > count1)
            {
                gamma += "0";
                epsilon += "1";
            }
            else
            {
                gamma += "1";
                epsilon += "0";
            }
        }

        Console.WriteLine($"Task 1: {Convert.ToInt32(gamma, 2) * Convert.ToInt32(epsilon, 2)}");
    }

    public static void Task2()
    {
        int oxygen = findRating(File.ReadAllLines(inputPath).ToList(), true);
        int co2 = findRating(File.ReadAllLines(inputPath).ToList(), false);


        Console.WriteLine($"Task 2 : {oxygen * co2}");
    }

    private static int findRating(List<string> binaryNumsList, bool isOxygen)
    {
        for (int i = 0; i < binaryNumsList[0].Length; i++)
        {
            if (binaryNumsList.Count == 1)
            {
                break;
            }

            int count0 = 0;
            int count1 = 0;
            foreach (string num in binaryNumsList)
            {
                _ = (num[i] == '0') ? count0++ : count1++;
            }

            if(isOxygen)
            {
                if (count1 >= count0)
                {
                    binaryNumsList = binaryNumsList.FindAll(n => n[i] == '1');
                }
                else
                {
                    binaryNumsList = binaryNumsList.FindAll(n => n[i] == '0');
                }
            }
            else
            {
                if (count0 <= count1)
                {
                    binaryNumsList = binaryNumsList.FindAll(n => n[i] == '0');
                }
                else
                {
                    binaryNumsList = binaryNumsList.FindAll(n => n[i] == '1');
                }
            }
        }

        return Convert.ToInt32(binaryNumsList[0], 2);
    }
}
