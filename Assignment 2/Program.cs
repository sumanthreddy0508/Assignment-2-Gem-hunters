using System;

// Class representing a position on the board
public class Position
{
    public int X { get; set; } // X coordinate
    public int Y { get; set; } // Y coordinate

    // Constructor to initialize the position with given coordinates
    public Position(int x, int y)
    {
        X = x;
        Y = y;
    }
}

// Class representing a player
public class Player
{
    public string Name { get; } // Player's name
    public Position Position { get; set; } // Player's current position on the board
    public int GemCount { get; set; } // Number of gems collected by the player

    // Constructor to initialize the player with a name and starting position
    public Player(string name, Position position)
    {
        Name = name;
        Position = position;
        GemCount = 0; // Initialize gem count to 0
    }

    // Method to move the player in a given direction
    public void Move(char direction)
    {
        switch (direction)
        {
            case 'U': // Move up
                Position.Y--;
                break;
            case 'D': // Move down
                Position.Y++;
                break;
            case 'L': // Move left
                Position.X--;
                break;
            case 'R': // Move right
                Position.X++;
                break;
            default:
                break;
        }
    }
}

// Class representing a cell on the board
public class Cell
{
    public string Occupant { get; set; } // Object occupying the cell ("P1", "P2", "G", "O", "-")

    // Constructor to initialize the cell with a default occupant ("-")
    public Cell(string occupant = "-")
    {
        Occupant = occupant;
    }
}

// Class representing the game board
public class Board
{
    public Cell[,] Grid { get; } // 2D array representing the board
    public Player Player1 { get; } // Player 1
    public Player Player2 { get; } // Player 2

    // Constructor to initialize the board, players, obstacles, and gems
    public Board()
    {
        Grid = new Cell[6, 6]; // Initialize the grid with size 6x6
        Player1 = new Player("P1", new Position(0, 0)); // Initialize Player 1 at the top-left corner
        Player2 = new Player("P2", new Position(5, 5)); // Initialize Player 2 at the bottom-right corner
        InitializeBoard(); // Initialize the board with empty cells
        PlaceObstacles(); // Place obstacles randomly on the board
        PlaceGems(); // Place gems randomly on the board
    }

    // Method to initialize the board with empty cells
    private void InitializeBoard()
    {
        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                Grid[i, j] = new Cell(); // Each cell is initially empty ("-")
            }
        }
    }

    // Method to randomly place obstacles on the board
    private void PlaceObstacles()
    {
        Random random = new Random();
        for (int i = 0; i < 8; i++)
        {
            int x = random.Next(6); // Random X coordinate
            int y = random.Next(6); // Random Y coordinate
            Grid[y, x].Occupant = "O"; // Place an obstacle at the chosen location
        }
    }

    // Method to randomly place gems on the board
    private void PlaceGems()
    {
        Random random = new Random();
        for (int i = 0; i < 5; i++)
        {
            int x = random.Next(6); // Random X coordinate
            int y = random.Next(6); // Random Y coordinate
            while (Grid[y, x].Occupant != "-") // Keep choosing new coordinates until an empty cell is found
            {
                x = random.Next(6);
                y = random.Next(6);
            }
            Grid[y, x].Occupant = "G"; // Place a gem at the chosen location
        }
    }

    // Method to display the board
    public void Display()
    {
        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                Console.Write(Grid[i, j].Occupant + " "); // Display the occupant of each cell
            }
            Console.WriteLine(); // Move to the next row
        }
    }

    // Method to check if a move is valid for the given player in the specified direction
    public bool IsValidMove(Player player, char direction)
    {
        int x = player.Position.X;
        int y = player.Position.Y;
        if (direction == 'U' && y > 0 && Grid[y - 1, x].Occupant != "O")
            return true; // Valid move up
        if (direction == 'D' && y < 5 && Grid[y + 1, x].Occupant != "O")
            return true; // Valid move down
        if (direction == 'L' && x > 0 && Grid[y, x - 1].Occupant != "O")
            return true; // Valid move left
        if (direction == 'R' && x < 5 && Grid[y, x + 1].Occupant != "O")
           
        }
}
