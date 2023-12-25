using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sea_battle
{
    internal class ShipMatrix
    {
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

        public static void GetRandomFreePosition(int[,] map, Random random, out int x, out int y)
        {
            do
            {
                x = random.Next(1, 11);
                y = random.Next(1, 11);
            } while (map[x, y] != 0);
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

        public static bool ValidateNewShip(int[,] map)
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

        public static int[,] replaceTemporaryShipWithPermanent(int[,] array)
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

        private static int[,] GenerateShipByX(int[,] map, int startX, int startY, int row, int decks, int step)
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


        private static int[,] GenerateShipByY(int[,] map, int startX, int startY, int row, int decks, int step)
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
    }
}
