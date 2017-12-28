//You are given a map of square room, some of the tiles of this room are blocked
//You are standing at starting tile. You can step in four directions (top, right, bottom, left), one tile per step
//Write a code that returns the minimum number of steps required to get from your starting tile to the specific target tile
//
//Input format:
//First line contains a size of the room N  (3 <= N <= 80)
//Next N lines contain map of the room where # means empty tile, * means blocked tile, 0 means starting tile and 1 means target tile
//
//Output format:
//A single integer equals to the minimum number of steps required or -1 in case target tile can't be reached
//
//Sample Input 1:
//5
//####1
//0####
//#####
//#####
//#####
//
//Sample Output 1:
//5
//
//Sample Input 2:
//3
//0##
//***
//##1
//
//Sample Output 2:
//-1

using System;
using System.IO;

namespace Maze
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var file = new StreamReader("input.txt"))
            {
                var n = int.Parse(file.ReadLine());
                var map = new char[n,n];
                for (var i = 0; i < n; i++)
                {
                    var line = file.ReadLine();
                    for (var j = 0; j < n; j++)
                    {
                        map[i, j] = line[j];
                    }
                }
                var result = CalculateShortestPath(n, map);
                Console.WriteLine(result);
            }
            Console.ReadLine();
        }

        private static int CalculateShortestPath(int n, char[,] map)
        {
            //Write your solution here
            throw new NotImplementedException();
        }
    }
}
