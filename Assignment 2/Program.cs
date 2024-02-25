using System;

// Represents a position on the game board
public class Position
{
    public int X { get; set; } // X-coordinate
    public int Y { get; set; } // Y-coordinate

    // Constructor to initialize X and Y coordinates
    public Position(int x, int y)
    {
        X = x;
        Y = y;
    }
}

// Represents a player in the game
public class Player
{
    public string Name { get; } // Player's name
    public Position Position { get; set; } // Player's current position on the board
    public int GemCount { get; set; } // Number of gems collected by the player

    // Constructor to initialize player's name, position, and gem count
    public Player(string name, Position position)
    {
        Name = name;
        Position = position;
        GemCount = 0; // Initialize gem count to 0
    }

    // Method to move the player in a specified direction
    public void Move(char direction)
    {
        // Update player's position based on the direction
        switch (direction)
        {
            case 'U':
                Position.Y--; // Move up (decrement Y-coordinate)
                break;
            case 'D':
                Position.Y++; // Move down (increment Y-coordinate)
                break;
            case 'L':
                Position.X--; // Move left (decrement X-coordinate)
                break;
            case 'R':
                Position.X++; // Move right (increment X-coordinate)
                break;
            default:
                break;
        }
    }
}

// Represents a cell on the game board
public class Cell
{
    public string Occupant { get; set; } // Object occupying the cell (Player, Gem, Obstacle)

    // Constructor to initialize the occupant of the cell (default is empty)
    public Cell(string occupant = "-")
    {
        Occupant = occupant;
    }
}

// Represents the game board
public class Board
{
    public Cell[,] Grid { get; } // 2D array representing the grid of cells
    public Player Player1 { get; } // Player 1
    public Player Player2 { get; } // Player 2

    // Constructor to initialize the board, players, and other elements
    public Board()
    {
        Grid = new Cell[6, 6]; // Initialize the grid with 6 rows and 6 columns
        Player1 = new Player("P1", new Position(0, 0)); // Initialize Player 1 at position (0, 0)
        Player2 = new Player("P2", new Position(5, 5)); // Initialize Player 2 at position (5, 5)
        InitializeBoard(); // Initialize the board with empty cells
        PlaceObstacles(); // Place obstacles randomly on the board
        PlaceGems(); // Place gems randomly on the board
    }

    // Method to initialize the board with empty cells
    private void InitializeBoard()
    {
        // Loop through each cell in the grid
        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                Grid[i, j] = new Cell(); // Create a new empty cell
            }
        }
    }

    // Method to randomly place obstacles on the board
    private void PlaceObstacles()
    {
        Random random = new Random(); // Random number generator
        // Place 8 obstacles randomly on the board
        for (int i = 0; i < 8; i++)
        {
            int x = random.Next(6); // Random X-coordinate
            int y = random.Next(6); // Random Y-coordinate
            Grid[y, x].Occupant = "O"; // Place an obstacle in the cell
        }
    }

    // Method to randomly place gems on the board
    private void PlaceGems()
    {
        Random random = new Random(); // Random number generator
        // Place 5 gems randomly on the board
        for (int i = 0; i < 8; i++)
        {
            int x = random.Next(6); // Random X-coordinate
            int y = random.Next(6); // Random Y-coordinate
            // Ensure the cell is empty before placing a gem
            while (Grid[y, x].Occupant != "-")
            {
                x = random.Next(6); // Generate new X-coordinate
                y = random.Next(6); // Generate new Y-coordinate
            }
            Grid[y, x].Occupant = "G"; // Place a gem in the cell
        }
    }

    // Method to display the board and player positions
    public void Display()
    {
        int totalGemsPlayer1 = Player1.GemCount; // Total gems collected by Player 1
        int totalGemsPlayer2 = Player2.GemCount; // Total gems collected by Player 2

        Console.WriteLine($"Total gems collected by Player 1: {totalGemsPlayer1}");
        Console.WriteLine($"Total gems collected by Player 2: {totalGemsPlayer2}");

        // Loop through each cell in the grid
        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                // Check if Player 1 is in the current cell
                if (Player1.Position.X == j && Player1.Position.Y == i)
                {
                    Console.Write("P1 "); // Display Player 1
                }
                // Check if Player 2 is in the current cell
                else if (Player2.Position.X == j && Player2.Position.Y == i)
                {
                    Console.Write("P2 "); // Display Player 2
                }
                else
                {
                    Console.Write(Grid[i, j].Occupant + " "); // Display the occupant of the cell
                }
            }
            Console.WriteLine();
        }
    }

    // Method to check if a move is valid for a player
    public bool IsValidMove(Player player, char direction)
    {
        int x = player.Position.X; // Current X-coordinate of the player
        int y = player.Position.Y; // Current Y-coordinate of the player
        // Check if the move is valid based on the direction and cell contents
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

    // Method to collect a gem from the player's current position
    public void CollectGem(Player player)
    {
        int x = player.Position.X; // X-coordinate of the player
        int y = player.Position.Y; // Y-coordinate of the player
        // Check if the current cell contains a gem
        if (Grid[y, x].Occupant == "G")
        {
            player.GemCount++; // Increment the player's gem count
            Grid[y, x].Occupant = "-"; // Remove the gem from the cell
        }
    }
}

// Represents the game
public class Game
{
    public Board board; // Game board
    private Player currentTurn; // Player whose turn it is
    private int totalTurns; // Total number of turns played

    // Constructor to initialize the game board, players, and other elements
    public Game()
    {
        board = new Board(); // Initialize the game board
        currentTurn = board.Player1; // Set Player 1 as the current turn player
        totalTurns = 0; // Initialize total turns to 0
    }

    // Method to start the game
    public void Start()
    {
        // Continue the game until it's over
        while (!IsGameOver())
        {
            board.Display(); // Display the game board
            Console.WriteLine($"{currentTurn.Name}'s turn"); // Display current player's turn
            char direction = char.ToUpper(Console.ReadKey().KeyChar); // Get player input for direction
            Console.WriteLine();
            // Check if the move is valid
            if (board.IsValidMove(currentTurn, direction))
            {
                currentTurn.Move(direction); // Move the player
                board.CollectGem(currentTurn); // Collect gem, if any
                totalTurns++; // Increment total turns
                SwitchTurn(); // Switch to the next player's turn
            }
            else
            {
                Console.WriteLine("Invalid move! Try again."); // Display message for invalid move
            }
        }
        AnnounceWinner(); // Announce the winner once the game is over
    }

    // Method to switch the turn to the next player
    private void SwitchTurn()
    {
        // If current turn is Player 1, switch to Player 2; otherwise, switch to Player 1
        currentTurn = currentTurn == board.Player1 ? board.Player2 : board.Player1;
    }

    // Method to check if the game is over
    private bool IsGameOver()
    {
        return totalTurns >= 30; // Game is over after 30 turns
    }

    // Method to announce the winner or a tie
    private void AnnounceWinner()
    {
        // Compare gem counts of Player 1 and Player 2
        if (board.Player1.GemCount > board.Player2.GemCount)
        {
            Console.WriteLine("Player 1 wins!"); // Player 1 has more gems
        }
        else if (board.Player1.GemCount < board.Player2.GemCount)
        {
            Console.WriteLine("Player 2 wins!"); // Player 2 has more gems
        }
        else
        {
            Console.WriteLine("It's a tie!"); // Gem counts of both players are equal
        }
    }
}

// Main program class
public class Program
{
    // Main method, entry point of the program
    public static void Main(string[] args)
    {
        Game game = new Game(); // Create a new game instance
        Console.WriteLine("Initial positions of players:"); // Display initial positions of players
        game.Start(); // Start the game
    }
}
