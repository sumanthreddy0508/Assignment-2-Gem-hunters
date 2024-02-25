using System;

public class Position
{
    public int X { get; set; }
    public int Y { get; set; }

    public Position(int x, int y)
    {
        X = x;
        Y = y;
    }
}

public class Player
{
    public string Name { get; }
    public Position Position { get; set; }
    public int GemCount { get; set; }

    public Player(string name, Position position)
    {
        Name = name;
        Position = position;
        GemCount = 0;
    }

    public void Move(char direction)
    {
        switch (direction)
        {
            case 'U':
                Position.Y--;
                break;
            case 'D':
                Position.Y++;
                break;
            case 'L':
                Position.X--;
                break;
            case 'R':
                Position.X++;
                break;
            default:
                break;
        }
    }
}

public class Cell
{
    public string Occupant { get; set; }

    public Cell(string occupant = "-")
    {
        Occupant = occupant;
    }
}

public class Board
{
    public Cell[,] Grid { get; }
    public Player Player1 { get; }
    public Player Player2 { get; }

    public Board()
    {
        Grid = new Cell[6, 6];
        Player1 = new Player("P1", new Position(0, 0));
        Player2 = new Player("P2", new Position(5, 5));
        InitializeBoard();
        PlaceObstacles();
        PlaceGems();
    }

    private void InitializeBoard()
    {
        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                Grid[i, j] = new Cell();
            }
        }
    }

    private void PlaceObstacles()
    {
        Random random = new Random();
        for (int i = 0; i < 8; i++)
        {
            int x = random.Next(6);
            int y = random.Next(6);
            Grid[y, x].Occupant = "O";
        }
    }

    private void PlaceGems()
    {
        Random random = new Random();
        for (int i = 0; i < 5; i++)
        {
            int x = random.Next(6);
            int y = random.Next(6);
            while (Grid[y, x].Occupant != "-")
            {
                x = random.Next(6);
                y = random.Next(6);
            }
            Grid[y, x].Occupant = "G";
        }
    }

 public void Display()
{
    int totalGemsPlayer1 = Player1.GemCount;
    int totalGemsPlayer2 = Player2.GemCount;

    Console.WriteLine($"Total gems collected by Player 1: {totalGemsPlayer1}");
    Console.WriteLine($"Total gems collected by Player 2: {totalGemsPlayer2}");

    for (int i = 0; i < 6; i++)
    {
        for (int j = 0; j < 6; j++)
        {
            if (Player1.Position.X == j && Player1.Position.Y == i)
            {
                Console.Write("P1");
            }
            else if (Player2.Position.X == j && Player2.Position.Y == i)
            {
                Console.Write("P2");
            }
            else
            {
                Console.Write(Grid[i, j].Occupant + " ");
            }
        }
        Console.WriteLine();
    }
}


    public bool IsValidMove(Player player, char direction)
    {
        int x = player.Position.X;
        int y = player.Position.Y;
        if (direction == 'U' && y > 0 && Grid[y - 1, x].Occupant != "O")
            return true;
        if (direction == 'D' && y < 5 && Grid[y + 1, x].Occupant != "O")
            return true;
        if (direction == 'L' && x > 0 && Grid[y, x - 1].Occupant != "O")
            return true;
        if (direction == 'R' && x < 5 && Grid[y, x + 1].Occupant != "O")
            return true;
        return false;
    }

    public void CollectGem(Player player)
    {
        int x = player.Position.X;
        int y = player.Position.Y;
        if (Grid[y, x].Occupant == "G")
        {
            player.GemCount++;
            Grid[y, x].Occupant = "-";
        }
    }
}

public class Game
{
    public Board board;
    private Player currentTurn;
    private int totalTurns;

    public Game()
    {
        board = new Board();
        currentTurn = board.Player1;
        totalTurns = 0;
    }

    public void Start()
    {
        while (!IsGameOver())
        {
            board.Display();
            Console.WriteLine($"{currentTurn.Name}'s turn");
            char direction = char.ToUpper(Console.ReadKey().KeyChar);
            Console.WriteLine();
            if (board.IsValidMove(currentTurn, direction))
            {
                currentTurn.Move(direction);
                board.CollectGem(currentTurn);
                totalTurns++;
                SwitchTurn();
            }
            else
            {
                Console.WriteLine("Invalid move! Try again.");
            }
        }
        AnnounceWinner();
    }

    private void SwitchTurn()
    {
        currentTurn = currentTurn == board.Player1 ? board.Player2 : board.Player1;
    }

    private bool IsGameOver()
    {
        return totalTurns >= 30;
    }

    private void AnnounceWinner()
    {
        if (board.Player1.GemCount > board.Player2.GemCount)
        {
            Console.WriteLine("Player 1 wins!");
        }
        else if (board.Player1.GemCount < board.Player2.GemCount)
        {
            Console.WriteLine("Player 2 wins!");
        }
        else
        {
            Console.WriteLine("It's a tie!");
        }
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        Game game = new Game();
        // Display initial positions of players
       Console.WriteLine("Initial positions of players:");
        game.Start();
    }
}

