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

        private static string alphabet = "ABCDEFGHIJ";
        private static int[] numbers = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        public static readonly int numberOneDeckShip = 4;
        public static readonly int numberTwoDecksShips = 3;
        public static readonly int numberThreeDecksShips = 2;
        public static readonly int numberFourDecksShips = 1;


        public static int[,] createMapArray(int mapSize)
        {
            int[,] Map = new int[mapSize, mapSize];
            return Map;
        }

        public static int[,] generateShips(int[,] map)
        {
            int[,] shipMap = generateDecksShip(map,4, numberFourDecksShips);
            shipMap = generateDecksShip(shipMap,3, numberThreeDecksShips);
            shipMap = generateDecksShip(shipMap, 2, numberTwoDecksShips);
            shipMap = generateDecksShip(shipMap, 1, numberOneDeckShip);

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
            ShipMatrix.GetRandomFreePosition(map, random, out startX, out startY);

            while (isDrown == false)
            {
                map = ShipMatrix.CheckAndPlaceShip(map, startX, startY, decks, row, out isDrown);

                if (isDrown)
                {
                    bool isValid = ShipMatrix.ValidateShip(map);
                    if (isValid)
                    {
                        map = ShipMatrix.replaceTemporaryShipWithPermanent(map);
                        map = ShipMatrix.Transform(map);
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
                ShipMatrix.GetRandomFreePosition(map, random,out startX, out startY);
            }
            return map;
        }

        public static void DisplayShips(int[,] map, int cellSize, Control.ControlCollection control, int delta)
        {
            int mapSize = map.GetLength(0);
            for (int i = 0; i < mapSize; i++)
            {
                for (int j = 0; j < mapSize; j++)
                {
                    if (map[i,j] == ShipMatrix.shipIndex)
                    {
                        int buttonLicationX = j * cellSize;
                        int buttonLicationY = i * cellSize;
                        DrawButton(control, new Point(buttonLicationX + delta, buttonLicationY), Color.Red);
                    }
                }
            }
        }

        public static void DisplayFields(int[,] map, int cellSize, Control.ControlCollection control, int delta)
        {
            int mapSize = map.GetLength(0);

            for (int i = 0; i < mapSize; i++)
            {
                for (int j = 0; j < mapSize; j++)
                {
                    if (map[i, j] == ShipMatrix.destroyedShipCornerIndex)
                    {
                        int buttonLicationX = j * cellSize;
                        int buttonLicationY = i * cellSize;
                        DrawButton(control, new Point(buttonLicationX + delta, buttonLicationY), Color.Black);
                    }
                }
            }
        }

        public static void DrawButton(Control.ControlCollection Control, Point pointLocation, Color color)
        {
            foreach (Control control in Control)
            {
                if (control is Button button && button.Bounds.Contains(pointLocation))
                {
                    button.BackColor = color;
                }
            }
        }

        private static void Shoot(object sender, EventArgs e, int[,] enemyMap,int cellSize, Player firstPlayer, Player secondPlayer, Control.ControlCollection control, Label label, IWeapon weapon)
        {
            Button pressedButton = sender as Button;
            if (firstPlayer.canShoot && firstPlayer.isPlayerTurn)
            {
                bool hit = firstPlayer.ShootWeapon(enemyMap, cellSize, pressedButton, control, label, weapon);

                if (!hit)
                {
                    firstPlayer.isPlayerTurn = false;
                    secondPlayer.isPlayerTurn = true;
                    secondPlayer.canShoot = true;
                }
            }
            
        }

        public static void Display(int mapSize, int cellSize,int[,] userMap, int[,]enemyMap, Control.ControlCollection control, Player firstPlayer, Player enemyPlayer, Label label, IWeapon weapon)
        {
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
                        button.Click += new EventHandler((sender, e) => Shoot(sender, e, enemyMap, cellSize, firstPlayer, enemyPlayer, control, label, weapon));
                    }
                    control.Add(button);
                }
            }
            DisplayShips(userMap, cellSize,control,0);
        }

        public static void createMaps(int mapSize,int cellSize, int[,] firstMap, int[,] secondMap, Control.ControlCollection control)
        {
            for (int i = 0; i < mapSize; i++)
            {
                for (int j = 0; j < mapSize; j++)
                {
                    firstMap[i, j] = 0;
                    if (i == 0 && j == 0)
                    {
                        firstMap[i, j] = ShipMatrix.technicalFieldIndex;
                    }
                    if (i == 0 || j == 0)
                    {
                        if (i == 0 && j>0)
                        {
                            firstMap[i, j] = ShipMatrix.technicalFieldIndex;
                        }
                        if (j == 0 && i > 0)
                        {
                            firstMap[i, j] = ShipMatrix.technicalFieldIndex;
                        }
                    }
                }
            }

            for (int i = 0; i < mapSize; i++)
            {
                for (int j = 0; j < mapSize; j++)
                {
                    secondMap[i, j] = 0;
                    if (i == 0 && j == 0)
                    {
                        secondMap[i, j] = ShipMatrix.technicalFieldIndex;
                    }
                    if (i == 0 || j == 0)
                    {
                        if (i == 0 && j > 0)
                        {
                            secondMap[i, j] = ShipMatrix.technicalFieldIndex;
                        }
                        if (j == 0 && i > 0)
                        {
                            secondMap[i, j] = ShipMatrix.technicalFieldIndex;
                        }
                    }
                }
            }
            generateShips(firstMap);
            generateShips(secondMap);
        }
    }
}
