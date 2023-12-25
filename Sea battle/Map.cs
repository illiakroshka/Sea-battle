using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;

namespace Sea_battle
{
    internal class Map
    {
        public static int[,] createMapArray(int mapSize)
        {
            int[,] Map = new int[mapSize, mapSize];
            return Map;
        }

        public static int[,] generateShips(int[,] map, int cellSize, int delta, Control.ControlCollection control)
        {
            int[,] shipMap = generateDecksShip(map,4,1);
            shipMap = generateDecksShip(shipMap,3,2);
            shipMap = generateDecksShip(shipMap, 2,3);
            shipMap = generateDecksShip(shipMap, 1,4);

            DisplayShips(shipMap, cellSize, control, delta);
            return shipMap;
        }

        public static int[,] generateDecksShip(int[,] map, int decks, int amount)
        {
            Random random = new Random();
            int startX;
            int startY;
            int row = 1;
            bool isDrown = false;
            int counter = 0;
            GetRandomFreePosition(map,random,out startX, out startY);

            while (isDrown == false)
            {
                map = CheckAndPlaceShip(map, startX, startY, decks, row, out isDrown);

                if (isDrown)
                {
                    bool isValid = Validate(map);
                    if (isValid)
                    {
                        map = ReplaceThreesWithOnes(map);
                        map = Transform(map);
                        counter++;
                        if (counter != amount)
                        {
                            isDrown = false;
                        }
                    }
                    else
                    {
                        isDrown = false;
                    }
                }
                GetRandomFreePosition(map, random,out startX, out startY);
            }
            return map;
        }

        public static int[,] GenerateShipByX(int[,] map, int startX, int startY, int row, int decks, int step)
        {
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < decks; j++)
                {
                    map[startX, startY] = 3;
                    startX = startX + step;
                }
            }
            return map;
        }


        public static int[,] GenerateShipByY(int[,] map, int startX, int startY, int row, int decks, int step)
        {
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < decks; j++)
                {
                    map[startX, startY] = 3;
                    startY = startY + step;
                }
            }
            return map;
        }

        public static void GetRandomFreePosition(int[,] map,Random random, out int x, out int y)
        {
            do
            {
                x = random.Next(1, 11);
                y = random.Next(1, 11);
            } while (map[x, y] != 0);
        }

        private static bool CheckAndPlaceShipDirectionX(int[,] map, int startX, int startY, int decks, int step)
        {
            bool isAble = true;

            for (int i = 0; i < decks; i++)
            {
                if (map[startX, startY] == 0)
                {
                    isAble = true;
                }
                else
                {
                    isAble = false;
                    break;
                }
                startX += step;
            }
            return isAble;
        }

        private static bool CheckAndPlaceShipDirectionY(int[,] map, int startX, int startY, int decks, int step)
        {
            bool isAble = true;

            for (int i = 0; i < decks; i++)
            {
                if (map[startX, startY] == 0)
                {
                    isAble = true;
                }
                else
                {
                    isAble = false;
                    break;
                }
                startY += step;
            }
            return isAble;
        }

        public static int[,] CheckAndPlaceShip(int[,] map, int startX, int startY, int decks, int row, out bool isDrown)
        {
            isDrown = false;
            Random random = new Random();
            int direction = random.Next(0, 2);
            if (direction == 0)
            {
                if (startX - decks > 0)
                {
                    bool isAble = CheckAndPlaceShipDirectionX(map, startX, startY, decks, -row);
                    if (isAble)
                    {
                        isDrown = true;
                        map = GenerateShipByX(map, startX, startY, row, decks, -row);
                    }
                }
                else if (startX + decks < 9)
                {
                    bool isAble = CheckAndPlaceShipDirectionX(map, startX, startY, decks, row);
                    if (isAble)
                    {
                        isDrown = true;
                        map = GenerateShipByX(map, startX, startY, row, decks, row);
                    }
                }
            }
            if (direction == 1)
            {
                if (startY - decks > 0)
                {
                    bool isAble = CheckAndPlaceShipDirectionY(map, startX, startY, decks, -row);
                    if (isAble)
                    {
                        isDrown = true;
                        map = GenerateShipByY(map, startX, startY, row, decks, -row);
                    }
                }
                else if (startY + decks < 9)
                {
                    bool isAble = CheckAndPlaceShipDirectionY(map, startX, startY, decks, row);
                    if (isAble)
                    {
                        isDrown = true;
                        map = GenerateShipByY(map, startX, startY, row, decks, row);
                    }
                }
            }
            return map;
        }

        public static void DisplayShips(int[,] map, int cellSize, Control.ControlCollection control, int delta)
        {
            int mapSize = 11;
            for (int i = 0; i < mapSize; i++)
            {
                for (int j = 0; j < mapSize; j++)
                {
                    if (map[i,j] == 1)
                    {
                        int buttonLicationX = j * cellSize;
                        int buttonLicationY = i * cellSize;
                        DrawButton(control, new Point(buttonLicationX + delta, buttonLicationY));
                    }
                }
            }
        }

        public static void DrawButton(Control.ControlCollection Control, Point pointLocation)
        {
            foreach (Control control in Control)
            {
                if (control is Button button && button.Bounds.Contains(pointLocation))
                {
                    button.BackColor = Color.Red;
                }
            }
        }

        public static int[,] Transform(int[,] map)
        {
            int rows = map.GetLength(0);
            int cols = map.GetLength(1);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (map[i, j] == 1)
                    {
                        for (int x = i - 1; x <= i + 1; x++)
                        {
                            for (int y = j - 1; y <= j + 1; y++)
                            {
                                if (x >= 0 && x < rows && y >= 0 && y < cols && map[x, y] != 1)
                                {
                                    map[x, y] = 2;
                                }
                            }
                        }
                    }
                }
            }
            return map;
        }

        public static bool Validate(int[,] map)
        {
            int rows = map.GetLength(0);
            int cols = map.GetLength(1);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (map[i, j] == 3)
                    {
                        for (int x = i - 1; x <= i + 1; x++)
                        {
                            for (int y = j - 1; y <= j + 1; y++)
                            {
                                if (x >= 0 && x < rows && y >= 0 && y < cols && map[x, y] == 1)
                                {
                                    return false;
                                }
                            }
                        }
                    }
                }
            }
            return true;
        }

        static int[,] ReplaceThreesWithOnes(int[,] array)
        {
            int rows = array.GetLength(0);
            int cols = array.GetLength(1);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (array[i, j] == 3)
                    {
                        array[i, j] = 1;
                    }
                }
            }
            return array;
        }

        private static void PlayerShoot(object sender, EventArgs e, int[,] enemyMap, Player firstPlayer, Player secondPlayer)
        {
            Button pressedButton = sender as Button;
            if (firstPlayer.canShoot && firstPlayer.isPlayerTurn)
            {
               bool hit = firstPlayer.Shoot(enemyMap, 30, pressedButton);

                if (!hit)
                {
                    firstPlayer.isPlayerTurn = false;
                    secondPlayer.isPlayerTurn = true;
                    secondPlayer.canShoot = true;
                }
            }

        }

        private static bool Shoot(int[,] map,int cellSize ,Button pressedButton)
        {
            bool hit = false;

            int delta = 0;
            if (pressedButton.Location.X > 350)
            {
                delta = 350;
            }
            if (map[pressedButton.Location.Y / cellSize, (pressedButton.Location.X - delta)/ cellSize] == 1)
            {
                hit = true;
                pressedButton.BackColor = Color.Blue;
                pressedButton.Text = "X";
            }
            else
            {
                hit = false;
                pressedButton.BackColor = Color.Black;
            }
            return hit;
        }

        private static void ConfigureShips(object sender, EventArgs e, int[,] map, int cellSize)
        {
            Button pressedButton = sender as Button;
            pressedButton.BackColor = Color.Red;
            int first = pressedButton.Location.Y / cellSize;
            int second = pressedButton.Location.X / cellSize;
            
            if (map[pressedButton.Location.Y / cellSize, pressedButton.Location.X / cellSize] == 0)
            {
                pressedButton.BackColor = Color.Red;
                map[pressedButton.Location.Y / cellSize, pressedButton.Location.X / cellSize] = 1;
            }
            else
            {
                pressedButton.BackColor = Color.White;
                map[pressedButton.Location.Y / cellSize, pressedButton.Location.X / cellSize] = 0;
            }
        }

        public static void DisplayMaps(int mapSize, int cellSize,int[,] userMap, int[,]enemyMap, Control.ControlCollection control, Player firstPlayer, Player enemyPlayer)
        {
            string alphabet = "ABCDEFGHIJ";
            int[] numbers = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            int value = numbers[9];

            for (int i = 0; i < mapSize; i++)
            {
                for (int j = 0; j < mapSize; j++)
                {
                    Button button = new Button();
                    button.Location = new Point(j * cellSize, i * cellSize);
                    button.Size = new Size(cellSize, cellSize);
                    button.BackColor = Color.White;
                    if (i == 0 || j == 0)
                    {
                        button.BackColor = Color.Gray;
                        if (i == 0 && j > 0)
                        {
                            button.Text = alphabet[j - 1].ToString();
                        }
                        if (j == 0 && i > 0)
                        {
                            button.Text = numbers[i - 1].ToString();
                        }
                    }
                    else
                    {
                        //button.Click += new EventHandler((sender, e) => ConfigureShips(sender, e, userMap, cellSize));
                    }
                    control.Add(button);
                }
            }

            for (int i = 0; i < mapSize; i++)
            {
                for (int j = 0; j < mapSize; j++)
                {
                    Button button = new Button();
                    button.Location = new Point(350 + j * cellSize, i * cellSize);
                    button.Size = new Size(cellSize, cellSize);
                    button.BackColor = Color.White;
                    if (i == 0 || j == 0)
                    {
                        button.BackColor = Color.Gray;
                        if (i == 0 && j > 0)
                        {
                            button.Text = alphabet[j - 1].ToString();
                        }
                        if (j == 0 && i > 0)
                        {
                            button.Text = numbers[i - 1].ToString();
                        }
                    }
                    else
                    {
                        button.Click += new EventHandler((sender, e) => PlayerShoot(sender, e, enemyMap, firstPlayer, enemyPlayer));
                    }
                    control.Add(button);
                }
            }
            DisplayShips(userMap, cellSize,control,0);
        }

        public static void createMaps(int mapSize,int cellSize, int[,] firstMap, int[,] secondMap, Control.ControlCollection control)
        {
            string alphabet = "ABCDEFGHIJ";
            int[] numbers = {0,1,2,3,4,5,6,7,8,9 };
            int value = numbers[9];

            for (int i = 0; i < mapSize; i++)
            {
                for (int j = 0; j < mapSize; j++)
                {
                    firstMap[i, j] = 0;
                    Button button = new Button();
                    button.Location = new Point(j * cellSize, i * cellSize);
                    button.Size = new Size(cellSize, cellSize);
                    button.BackColor = Color.White;
                    if (i == 0 || j == 0)
                    {
                        button.BackColor = Color.Gray;
                        if (i == 0 && j>0)
                        {
                            button.Text = alphabet[j - 1].ToString();
                        }
                        if (j == 0 && i > 0)
                        {
                            button.Text = numbers[i-1].ToString();
                        }
                    }
                    else
                    {
                        button.Click += new EventHandler((sender, e) => ConfigureShips(sender, e, firstMap, cellSize));
                    }
                    control.Add(button);
                }
            }

            for (int i = 0; i < mapSize; i++)
            {
                for (int j = 0; j < mapSize; j++)
                {
                    secondMap[i, j] = 0;
                    Button button = new Button();
                    button.Location = new Point(350 + j * cellSize, i * cellSize);
                    button.Size = new Size(cellSize, cellSize);
                    button.BackColor = Color.White;
                    if (i == 0 || j == 0)
                    {
                        button.BackColor = Color.Gray;
                        if (i == 0 && j > 0)
                        {
                            button.Text = alphabet[j - 1].ToString();
                        }
                        if (j == 0 && i > 0)
                        {
                            button.Text = numbers[i - 1].ToString();
                        }
                    }
                    else
                    {
                       // button.Click += new EventHandler((sender, e) => PlayerShoot(sender, e, secondMap));
                    }
                    control.Add(button);
                }
            }
            generateShips(firstMap, cellSize, 0, control);
            generateShips(secondMap, cellSize,350, control);
        }
    }
}
