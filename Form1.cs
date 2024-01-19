namespace Snake_forms
{
    public partial class Form1 : Form
    {    
        public partial class SnakeForm : Form
        {
            private const int GridSize = 25;
            private const int CellSize = 20;
            private List<Point> snake;
            private Point food;
            private Direction direction;

            public SnakeForm()
            {
                InitializeComponent();
                InitializeGame();
            }

            private void InitializeGame()
            {
                snake = new List<Point>
            {
                new Point(GridSize / 2, GridSize / 2)
            };
                GenerateFood();
                direction = Direction.Right;
                timer1.Start();
            }

            private void GenerateFood()
            {
                Random random = new Random();
                food = new Point(random.Next(GridSize), random.Next(GridSize));

                // Ensure the food is not generated on the snake
                while (snake.Contains(food))
                {
                    food = new Point(random.Next(GridSize), random.Next(GridSize));
                }
            }

            private void SnakeForm_Paint(object sender, PaintEventArgs e)
            {
                Graphics g = e.Graphics;
                DrawGrid(g);
                DrawSnake(g);
                DrawFood(g);
            }

            private void DrawGrid(Graphics g)
            {
                Pen pen = new Pen(Color.Black);

                for (int i = 0; i <= GridSize; i++)
                {
                    g.DrawLine(pen, i * CellSize, 0, i * CellSize, GridSize * CellSize);
                    g.DrawLine(pen, 0, i * CellSize, GridSize * CellSize, i * CellSize);
                }
            }

            private void DrawSnake(Graphics g)
            {
                Brush snakeBrush = new SolidBrush(Color.Green);

                foreach (Point point in snake)
                {
                    g.FillRectangle(snakeBrush, point.X * CellSize, point.Y * CellSize, CellSize, CellSize);
                }
            }

            private void DrawFood(Graphics g)
            {
                Brush foodBrush = new SolidBrush(Color.Red);
                g.FillRectangle(foodBrush, food.X * CellSize, food.Y * CellSize, CellSize, CellSize);
            }

            private void SnakeForm_KeyDown(object sender, KeyEventArgs e)
            {
                switch (e.KeyCode)
                {
                    case Keys.Up:
                        direction = Direction.Up;
                        break;
                    case Keys.Down:
                        direction = Direction.Down;
                        break;
                    case Keys.Left:
                        direction = Direction.Left;
                        break;
                    case Keys.Right:
                        direction = Direction.Right;
                        break;
                }
            }

            private void timer1_Tick(object sender, EventArgs e)
            {
                MoveSnake();
                CheckCollision();
                Invalidate();
            }

            private void MoveSnake()
            {
                Point head = snake.First();

                switch (direction)
                {
                    case Direction.Up:
                        head.Y = (head.Y - 1 + GridSize) % GridSize;
                        break;
                    case Direction.Down:
                        head.Y = (head.Y + 1) % GridSize;
                        break;
                    case Direction.Left:
                        head.X = (head.X - 1 + GridSize) % GridSize;
                        break;
                    case Direction.Right:
                        head.X = (head.X + 1) % GridSize;
                        break;
                }

                if (head == food)
                {
                    snake.Insert(0, food);
                    GenerateFood();
                }
                else
                {
                    snake.Insert(0, head);
                    snake.RemoveAt(snake.Count - 1);
                }
            }

            private void CheckCollision()
            {
                Point head = snake.First();

                if (snake.Count > 1 && snake.Skip(1).Contains(head))
                {
                    // Snake collided with itself
                    GameOver();
                }
            }

            private void GameOver()
            {
                timer1.Stop();
                MessageBox.Show("Game Over! Your score: " + (snake.Count - 1), "Game Over", MessageBoxButtons.OK, MessageBoxIcon.Information);
                InitializeGame();
            }
        }

        public enum Direction
        {
            Up,
            Down,
            Left,
            Right
        }
    }
}

