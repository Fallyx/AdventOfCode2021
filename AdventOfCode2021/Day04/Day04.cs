
namespace AdventOfCode2021.Day04;

internal class Day04
{
    const string inputPath = @"Day04/Input.txt";

    public static void Task1and2()
    {
        List<string> lines = File.ReadAllLines(inputPath).ToList();        
        List<int> pullingNums = lines.First().Split(',').Select(Int32.Parse).ToList();
        List<BingoBoard> boards = CreateBoards(lines);
        bool firstWinFound = false;

        foreach(int num in pullingNums)
        {
            for(int boardNum = boards.Count - 1; boardNum > 0; boardNum--)
            {
                for (int y = 0; y < 5; y++)
                {
                    for (int x = 0; x < 5; x++)
                    {
                        if (boards[boardNum].GetBoardNum(y, x) == num)
                        {
                            boards[boardNum].SetBoardMark(y, x, true);
                        }
                    }
                }

                if (boards[boardNum].HasWin())
                {
                    if (!firstWinFound)
                    {
                        Console.WriteLine($"Task 1: {boards[boardNum].SumOfUnmarked() * num}");
                        firstWinFound = true;
                    }
                    else if (boards.Count == 2) // Correct answer when 2 boards left???
                    {
                        Console.WriteLine($"Task 2: {boards[boardNum].SumOfUnmarked() * num}");
                        return;
                    }

                    boards.RemoveAt(boardNum);
                }
            }
        }
    }

    private static List<BingoBoard> CreateBoards(List<string> lines)
    {
        List<BingoBoard> boards = new List<BingoBoard>();
        lines = lines.Skip(2).ToList();
        int y = 0;

        BingoBoard board = new BingoBoard(5, 5);
        foreach (string line in lines)
        {
            if (line == string.Empty)
            {
                boards.Add(board);
                y = 0;
                board = new BingoBoard(5, 5);
                continue;
            }

            int[] rowNums = line.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(Int32.Parse).ToArray();
            for (int x = 0; x < 5; x++)
            {
                board.SetBoardNum(y, x, rowNums[x]);
                board.SetBoardMark(y, x, false);
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
    public bool Win { get; private set; }

    public BingoBoard(int rows, int cols)
    {
        Win = false;
        Rows = rows;
        Cols = cols; 
        Board = new (int num, bool mark)[Rows, Cols];
    }

    public int GetBoardNum(int row, int col)
    {
        return Board[row, col].num;
    }

    public bool GetBoardMark(int row, int col)
    {
        return Board[row, col].mark;
    }

    public void SetBoardNum(int row, int col, int num)
    {
        Board[row, col].num = num;
    }

    public void SetBoardMark(int row, int col, bool mark)
    {
        Board[row, col].mark = mark;
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

    public bool HasWin()
    {
        for (int row = 0; row < Rows; row++)
        {
            if (Board[row, 0].mark && Board[row, 1].mark && Board[row, 2].mark && Board[row, 3].mark && Board[row, 4].mark)
            {
                Win = true;
                return Win;
            }
        }

        for (int col = 0; col < Cols; col++)
        {
            if (Board[0, col].mark && Board[1, col].mark && Board[2, col].mark && Board[3, col].mark && Board[4, col].mark)
            {
                Win = true;
                return Win;
            }
        }

        return Win;
    }
}