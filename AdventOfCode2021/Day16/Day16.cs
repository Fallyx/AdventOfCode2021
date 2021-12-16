namespace AdventOfCode2021.Day16;

internal class Day16
{
    const string inputPath = @"Day16/Input.txt";
    static int versionSum = 0;

    public static void Task1()
    {
        string hexStr = File.ReadAllLines(inputPath).First();

        string binarystring = String.Join(String.Empty,
          hexStr.Select(
            c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')
          )
        );

        (long value, _) = ParsePacket(binarystring);
        Console.WriteLine($"Task 1: {versionSum}");
        Console.WriteLine($"Task 2: {value}");
    }

    private static (long value, int parsedBits) ParsePacket(string packet)
    {
        int i = 0;
        string versionStr = packet.Substring(i, 3);
        string typeIdStr = packet.Substring(i + 3, 3);
        int version = Convert.ToInt32(versionStr, 2);
        int typeId = Convert.ToInt32(typeIdStr, 2);
        long value = 0;
        versionSum += version;
        i += 6;

        if (typeId == 4)
        {
            (long valueReturned, int parsed) = ParseLiteralValue(packet[i..]);
            value += valueReturned;
            i += parsed;
        }
        else
        {
            (long valueReturned, int parsed) = ParseOperator(packet[i..], typeId);
            value += valueReturned;
            i += parsed;
        }

        return (value, i);
    }

    private static (long value, int parsedBits) ParseLiteralValue(string packet)
    {
        bool isLast = false;
        string literalValueStr = "";
        int i = 0;

        while (!isLast)
        {
            string group = packet.Substring(i, 5);
            literalValueStr += group[1..];

            if (group[0] == '0')
                isLast = true;

            i += 5;
        }

        return (Convert.ToInt64(literalValueStr, 2), i);
    }

    private static (long value, int parsedBits) ParseOperator(string packet, int typeId)
    {
        int i = 0;
        long value = 0;
        string lengthTypeId = packet.Substring(i, 1);
        List<long> values = new List<long>();
        i++;
        if (lengthTypeId == "0")
        {
            string lenSubPacketsStr = packet.Substring(i, 15);
            int lenSubPackets = Convert.ToInt32(lenSubPacketsStr, 2);
            i += 15;
            int parsedBits = 0;
            string subPacketsStr = packet.Substring(i, lenSubPackets);
            while (lenSubPackets - parsedBits >= 11)
            {
                (long returnedVal, int parsed) =  ParsePacket(subPacketsStr[parsedBits..]);
                values.Add(returnedVal);
                parsedBits += parsed;
                i += parsed;
            }
        }
        else
        {
            string amountSubPacketsStr = packet.Substring(i, 11);
            int amountSubPackets = Convert.ToInt32(amountSubPacketsStr, 2);
            i += 11;
            int parsedBits = 0;
            string subPacketsStr = packet.Substring(i);

            for(int x = 0; x < amountSubPackets; x++)
            {
                (long returnedVal, int parsed) = ParsePacket(subPacketsStr[parsedBits..]);
                values.Add(returnedVal);
                parsedBits += parsed;
                i += parsed;
            }
        }

        if (typeId == 0)
            value = values.Aggregate(0L, (n1, n2) => n1 + n2);
        else if (typeId == 1)
            value = values.Aggregate(1L, (n1, n2) => n1 * n2);
        else if (typeId == 2)
            value = values.Min();
        else if (typeId == 3)
            value = values.Max();
        else if (typeId == 5)
            value = values[0] > values[1] ? 1 : 0;
        else if (typeId == 6)
            value = values[0] < values[1] ? 1 : 0;
        else if (typeId == 7)
            value = values[0] == values[1] ? 1 : 0;

        return (value, i);
    }
}
