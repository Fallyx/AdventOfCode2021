namespace AdventOfCode2021.Day04;

internal class Day04
{
    const string inputPath = @"Day04/Input.txt";

    public static void Task1and2()
    {
        List<string> lines = File.ReadAllLines(inputPath).ToList();        
        List<int> pullingNums = lines.First().Split(',').Select(Int32.Parse).ToList();
        List<BingoBoard> boards = CreateBoards(lines);
        bool firstBingoFound = false;

        foreach(int num in pullingNums)
        {
            for(int boardNum = boards.Count - 1; boardNum >= 0; boardNum--)
            {
                for (int y = 0; y < boards[boardNum].Rows; y++)
                {
                    for (int x = 0; x < boards[boardNum].Cols; x++)
                    {
                        if (boards[boardNum].Board[y, x].num == num)
                        {
                            boards[boardNum].Board[y, x].mark = true;
                        }
                    }
                }

                if (boards[boardNum].HasBingo())
                {
                    if (!firstBingoFound)
                    {
                        Console.WriteLine($"Task 1: {boards[boardNum].SumOfUnmarked() * num}");
                        firstBingoFound = true;
                    }
                    else if (boards.Count == 1)
                    {
                        Console.WriteLine($"Task 2: {boards[boardNum].SumOfUnmarked() * num}");
                        return;
                    }

                    boards.RemoveAt(boardNum);
                }
            }
        }
    }

    private static List<BingoBoard> CreateBoards(List<string> lines, int boardSize = 5)
    {
        List<BingoBoard> boards = new List<BingoBoard>();
        lines = lines.Skip(2).ToList();
        int y = 0;

        BingoBoard board = new BingoBoard(boardSize, boardSize);
        foreach (string line in lines)
        {
            if (line == string.Empty)
            {
                boards.Add(board);
                y = 0;
                board = new BingoBoard(boardSize, boardSize);
                continue;
            }

            int[] rowNums = line.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(Int32.Parse).ToArray();
            for (int x = 0; x < boardSize; x++)
            {
                board.Board[y, x].num = rowNums[x];
                board.Board[y, x].mark = false;
            }
            y++;
        }
        boards.Add(board);

        return boards;
    }
}

internal class BingoBoard
{
    public (int num, bool mark)[,] Board { get; private set; }
    public int Rows { get; private set; }
    public int Cols { get; private set; }
    public bool Bingo { get; private set; }

    public BingoBoard(int rows, int cols)
    {
        Bingo = false;
        Rows = rows;
        Cols = cols; 
        Board = new (int num, bool mark)[Rows, Cols];
    }

    public int SumOfUnmarked()
    {
        int sum = 0;
        for(int row = 0; row < Rows; row++)
        {
            for(int col = 0; col < Cols; col++)
            {
                sum += (!Board[row, col].mark) ? Board[row, col].num : 0;
            }
        }

        return sum;
    }

    public bool HasBingo()
    {
        for (int row = 0; row < Rows; row++)
        {
            if (Board[row, 0].mark && Board[row, 1].mark && Board[row, 2].mark && Board[row, 3].mark && Board[row, 4].mark)
            {
                Bingo = true;
                return Bingo;
            }
        }

        for (int col = 0; col < Cols; col++)
        {
            if (Board[0, col].mark && Board[1, col].mark && Board[2, col].mark && Board[3, col].mark && Board[4, col].mark)
            {
                Bingo = true;
                return Bingo;
            }
        }

        return Bingo;
    }
}