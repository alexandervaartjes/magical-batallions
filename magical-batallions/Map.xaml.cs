using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace magical_batallions
{
    public partial class Map : Window
    {
        private const int Rows = 20;
        private const int Columns = 30;
        private const int CellSize = 20;

        private Random random = new Random();

        private const int MaxRooms = 8;
        private const int MinRoomSize = 3;
        private const int MaxRoomSize = 7;

        private const int MaxEnemies = 5;
        private const int MaxItems = 3;

        private int[,] mapLayout;
        private List<Room> rooms = new List<Room>();
        private List<Enemy> enemies = new List<Enemy>();
        private List<Item> items = new List<Item>();

        private Player player;

        public Map()
        {
            InitializeComponent();
            GenerateProceduralMap();
            this.KeyDown += OnKeyDown;
        }

        private void GenerateProceduralMap()
        {
            mapLayout = new int[Rows, Columns];
            for (int row = 0; row < Rows; row++)
            {
                for (int col = 0; col < Columns; col++)
                {
                    mapLayout[row, col] = 1;
                }
            }

            for (int i = 0; i < MaxRooms; i++)
            {
                CreateRoom();
            }

            ConnectRoomsWithCorridors();

            PlaceEnemies();
            PlaceItems();


            PlacePlayer();

            DrawMap();
        }

        private void CreateRoom()
        {
            int roomWidth = random.Next(MinRoomSize, MaxRoomSize);
            int roomHeight = random.Next(MinRoomSize, MaxRoomSize);
            int startX = random.Next(1, Columns - roomWidth - 1);
            int startY = random.Next(1, Rows - roomHeight - 1);

            for (int y = startY; y < startY + roomHeight; y++)
            {
                for (int x = startX; x < startX + roomWidth; x++)
                {
                    mapLayout[y, x] = 0;
                }
            }

            rooms.Add(new Room(startX, startY, roomWidth, roomHeight));
        }

        private void ConnectRoomsWithCorridors()
        {
            for (int i = 0; i < rooms.Count - 1; i++)
            {
                Room roomA = rooms[i];
                Room roomB = rooms[i + 1];

                int roomACenterX = roomA.CenterX();
                int roomACenterY = roomA.CenterY();
                int roomBCenterX = roomB.CenterX();
                int roomBCenterY = roomB.CenterY();

                if (random.Next(0, 2) == 0)
                {
                    CreateHorizontalCorridor(roomACenterX, roomBCenterX, roomACenterY);
                    CreateVerticalCorridor(roomACenterY, roomBCenterY, roomBCenterX);
                }
                else
                {
                    CreateVerticalCorridor(roomACenterY, roomBCenterY, roomACenterX);
                    CreateHorizontalCorridor(roomACenterX, roomBCenterX, roomBCenterY);
                }
            }
        }

        private void CreateHorizontalCorridor(int x1, int x2, int y)
        {
            for (int x = Math.Min(x1, x2); x <= Math.Max(x1, x2); x++)
            {
                mapLayout[y, x] = 0;
            }
        }

        private void CreateVerticalCorridor(int y1, int y2, int x)
        {
            for (int y = Math.Min(y1, y2); y <= Math.Max(y1, y2); y++)
            {
                mapLayout[y, x] = 0;
            }
        }


        private void PlaceEnemies()
        {
            for (int i = 0; i < MaxEnemies; i++)
            {
                Room room = rooms[random.Next(rooms.Count)];
                int enemyX = random.Next(room.X, room.X + room.Width);
                int enemyY = random.Next(room.Y, room.Y + room.Height);

                enemies.Add(new Enemy(enemyX, enemyY));
            }
        }


        private void PlaceItems()
        {
            for (int i = 0; i < MaxItems; i++)
            {
                Room room = rooms[random.Next(rooms.Count)];
                int itemX = random.Next(room.X, room.X + room.Width);
                int itemY = random.Next(room.Y, room.Y + room.Height);

                items.Add(new Item(itemX, itemY));
            }
        }


        private void PlacePlayer()
        {
            Room room = rooms[random.Next(rooms.Count)];
            int playerX = random.Next(room.X, room.X + room.Width);
            int playerY = random.Next(room.Y, room.Y + room.Height);

            player = new Player(playerX, playerY);
        }


        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            int newX = player.X;
            int newY = player.Y;

            switch (e.Key)
            {
                case Key.Up:
                    newY = player.Y - 1;
                    break;
                case Key.Down:
                    newY = player.Y + 1;
                    break;
                case Key.Left:
                    newX = player.X - 1;
                    break;
                case Key.Right:
                    newX = player.X + 1;
                    break;
            }


            if (newX >= 0 && newX < Columns && newY >= 0 && newY < Rows && mapLayout[newY, newX] == 0)
            {
                player.X = newX;
                player.Y = newY;


                CheckInteractions();

 
                DrawMap();
            }
        }


        private void CheckInteractions()
        {

            foreach (var enemy in enemies.ToArray())
            {
                if (enemy.X == player.X && enemy.Y == player.Y)
                {
                    MessageBox.Show("Je hebt een vijand verslagen!");
                    enemies.Remove(enemy);
                }
            }

            foreach (var item in items.ToArray())
            {
                if (item.X == player.X && item.Y == player.Y)
                {
                    MessageBox.Show("Je hebt een item opgepakt!");
                    items.Remove(item);
                }
            }
        }

        private void DrawMap()
        {
            MapGrid.Children.Clear();

            for (int i = 0; i < Rows; i++)
            {
                MapGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(CellSize) });
            }

            for (int i = 0; i < Columns; i++)
            {
                MapGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(CellSize) });
            }

            for (int row = 0; row < Rows; row++)
            {
                for (int col = 0; col < Columns; col++)
                {
                    Rectangle rect = new Rectangle
                    {
                        Width = CellSize,
                        Height = CellSize,
                        Stroke = Brushes.Black,
                        StrokeThickness = 1
                    };

                    if (mapLayout[row, col] == 1)
                    {
                        rect.Fill = Brushes.Gray;
                    }
                    else
                    {
                        rect.Fill = Brushes.DarkSlateGray;
                    }

                    Grid.SetRow(rect, row);
                    Grid.SetColumn(rect, col);
                    MapGrid.Children.Add(rect);
                }
            }


            foreach (var enemy in enemies)
            {
                DrawEntity(enemy.X, enemy.Y, Brushes.Red);
            }


            foreach (var item in items)
            {
                DrawEntity(item.X, item.Y, Brushes.Blue); 
            }


            DrawEntity(player.X, player.Y, Brushes.Green);
        }

        private void DrawEntity(int x, int y, Brush color)
        {
            Rectangle entity = new Rectangle
            {
                Width = CellSize,
                Height = CellSize,
                Fill = color
            };

            Grid.SetRow(entity, y);
            Grid.SetColumn(entity, x);
            MapGrid.Children.Add(entity);
        }
    }


    public class Player
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Player(int x, int y)
        {
            X = x;
            Y = y;
        }
    }


    public class Enemy
    {
        public int X { get; }
        public int Y { get; }

        public Enemy(int x, int y)
        {
            X = x;
            Y = y;
        }
    }


    public class Item
    {
        public int X { get; }
        public int Y { get; }

        public Item(int x, int y)
        {
            X = x;
            Y = y;
        }
    }

    public class Room
    {
        public int X { get; }
        public int Y { get; }
        public int Width { get; }
        public int Height { get; }

        public Room(int x, int y, int width, int height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public int CenterX() => X + Width / 2;
        public int CenterY() => Y + Height / 2;
    }
}