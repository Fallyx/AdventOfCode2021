namespace AdventOfCode2021.Day21;
internal class Day21
{
    const string inputPath = @"Day21/Input.txt";
    // Key is roll value, value is the amount of time the roll value exists
    private static Dictionary<int, int> rollValues = new Dictionary<int, int>()
    {
        {3, 1},
        {4, 3},
        {5, 6},
        {6, 7},
        {7, 6},
        {8, 3},
        {9, 1}
    };

    public static void Task1()
    {
        List<string> lines = File.ReadAllLines(inputPath).ToList();
        (int pos, int score)[] players = new (int pos, int score)[lines.Count];

        for(int i = 0; i < lines.Count; i++)
        {
            players[i] = (Int32.Parse(lines[i][lines[i].Length - 1].ToString()), 0);
        }

        int dieSides = 100;
        int currentDieFace = 1;
        int minScoreToStop = 1000;
        int rollCounter = 0;
        int roll;
        bool hasWinner = false;

        while (!hasWinner)
        {
            for (int i = 0; i < players.Length; i++)
            {
                roll = currentDieFace + (currentDieFace + 1 % dieSides) + (currentDieFace + 2 % dieSides);
                currentDieFace += 3;
                rollCounter += 3;
                players[i].pos = (players[i].pos + roll) % 10;
                players[i].score += (players[i].pos == 0 ? 10 : players[i].pos);
                if (players[i].score >= minScoreToStop)
                {
                    hasWinner = true;
                    break;
                }
            }
        }

        Console.WriteLine($"Task 1: {players.Min(p => p.score) * rollCounter}");
    }

    public static void Task2()
    {
        List<string> lines = File.ReadAllLines(inputPath).ToList();
        int[] players = new int[lines.Count];

        for (int i = 0; i < lines.Count; i++)
        {
            players[i] = Int32.Parse(lines[i][lines[i].Length - 1].ToString());
        }

        (long p1Win, long p2Win) result = PlayRound((players[0], 0, players[1], 0), true);

        Console.WriteLine($"Task 2: {Math.Max(result.p1Win, result.p2Win)}");
    }

    private static (long p1Win, long p2Win) PlayRound((int p1Pos, int p1Score, int p2Pos, int p2Score) players, bool p1Turn)
    {
        (long p1Win, long p2Win) result = (0L, 0L);
        int currentPos = (p1Turn ? players.p1Pos : players.p2Pos);
        int currentScore = (p1Turn ? players.p1Score : players.p2Score);

        foreach (KeyValuePair<int, int> roll in rollValues)
        {
            int newPos = (currentPos + roll.Key) % 10;
            int newScore = currentScore + (newPos == 0 ? 10 : newPos);

            if (newScore >= 21)
            {
                if (p1Turn)
                    result.p1Win += roll.Value;
                else
                    result.p2Win += roll.Value;
            }
            else
            {
                (long p1WinResult, long p2WinResult) playResult;
                if (p1Turn)
                    playResult = PlayRound((newPos, newScore, players.p2Pos, players.p2Score), !p1Turn);
                else
                    playResult = PlayRound((players.p1Pos, players.p1Score, newPos, newScore), !p1Turn);

                result.p1Win += playResult.p1WinResult * roll.Value;
                result.p2Win += playResult.p2WinResult * roll.Value;
            }
        }

        return result;
    }
}
