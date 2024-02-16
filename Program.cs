using System;
using System.Collections.Generic;
using System.Threading;
namespace Snake;
class Program
{
    
    static int width = 40;
    static int height = 20;

    // Position des Snakes anzeigen
    static int snakeX = 10;
    static int snakeY = 10;
    static int dx = 1;
    static int dy = 0;

    // Position der Frucht anzeigen
    static int fruitX;
    static int fruitY;
    static Random random = new Random();

    // Speichert den Körper des Snakes um es in der Liste zu speichern
    static List<int> snakeXPositions = new List<int>();
    static List<int> snakeYPositions = new List<int>();

    
    static bool gameOver = false;
    static int score = 0;

    static void Main(string[] args)
    {
        Console.CursorVisible = false;

        // Position der Frucht
        PlaceFruit();

        while (!gameOver)
        {
            Console.Clear();
            DrawBorder();
            HandleInput();
            Update();
            Draw();
            Thread.Sleep(100);
        }

        Console.SetCursorPosition(width / 2 - 5, height / 2);
        Console.WriteLine("Game Over!");
        Console.WriteLine($"Your Score: {score}");
        Console.ReadKey();
    }

    static void HandleInput()
    {
        if (Console.KeyAvailable)
        {
            var key = Console.ReadKey(true).Key;
            if (key == ConsoleKey.W && dy != 1)
            {
                dx = 0;
                dy = -1;
            }
            else if (key == ConsoleKey.S && dy != -1)
            {
                dx = 0;
                dy = 1;
            }
            else if (key == ConsoleKey.A && dx != 1)
            {
                dx = -1;
                dy = 0;
            }
            else if (key == ConsoleKey.D && dx != -1)
            {
                dx = 1;
                dy = 0;
            }
        }
    }

    static void Update()
    {
        // Snake bewegen
        snakeX += dx;
        snakeY += dy;

        // überprüfen ob der snake die furcht isst
        if (snakeX == fruitX && snakeY == fruitY)
        {
            score++;
            PlaceFruit();
        }

        // überprüfen ob der snake in eine wand reinfahrt
        if (snakeX < 1 || snakeX >= width - 1 || snakeY < 1 || snakeY >= height - 1)
            gameOver = true;

        // überprüfen ob der snake in sich selber reinfahrt
        for (int i = 0; i < snakeXPositions.Count; i++)
        {
            if (snakeX == snakeXPositions[i] && snakeY == snakeYPositions[i])
                gameOver = true;
        }

        // position des snakes in echtzeit speichern
        snakeXPositions.Add(snakeX);
        snakeYPositions.Add(snakeY);

        
        if (snakeXPositions.Count > score)
        {
            snakeXPositions.RemoveAt(0);
            snakeYPositions.RemoveAt(0);
        }
    }

    static void Draw()
    {
        // Snake kopf zeichnen
        Console.SetCursorPosition(snakeX, snakeY);
        Console.Write("O");

        // Snake körper zeichnen
        for (int i = 1; i < snakeXPositions.Count; i++)
        {
            Console.SetCursorPosition(snakeXPositions[i], snakeYPositions[i]);
            Console.Write("o");
        }

        // Frucht zeichnen
        Console.SetCursorPosition(fruitX, fruitY);
        Console.Write("*");

        // Score darstellen
        Console.SetCursorPosition(0, height);
        Console.Write($"Score: {score}");
    }

    static void PlaceFruit()
    {
        fruitX = random.Next(1, width - 1);
        fruitY = random.Next(1, height - 1);
    }

    static void DrawBorder()
    {
        Console.SetCursorPosition(0, 0);
        for (int i = 0; i < width; i++)
        {
            Console.Write("-");
        }

        for (int i = 1; i < height - 1; i++)
        {
            Console.SetCursorPosition(0, i);
            Console.Write("|");
            Console.SetCursorPosition(width - 1, i);
            Console.Write("|");
        }

        Console.SetCursorPosition(0, height - 1);
        for (int i = 0; i < width; i++)
        {
            Console.Write("-");
        }
    }
}
