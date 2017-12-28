



















//Given the integer N (1 <= N <= 35) draw in a console a symmetric eight-pointed star inscribed in a square of size 2 * N - 1
//Use 'space' ( ) symbol for an empty square and a 'star' (*) symbol for a star-related square. See sample input and output for details
//
//Input Format:
//One line containing integer N
//
//Output Format:
//2 * N - 1 lines of the star image
//
//Sample Input 1:
//4
//Sample Output 1:
//*  *  * 
// * * *
//  ***
//*******
//  ***
// * * *
//*  *  *
//
//Sample Input 2:
//5
//*   *   *
// *  *  *
//  * * *
//   ***
//*********
//   ***
//  * * *
// *  *  *
//*   *   *

using System;
using System.IO;

namespace UnionJack
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var file = new StreamReader("input.txt"))
            {
                var n = int.Parse(file.ReadLine());
                DrawUnionJack(n);
            }
            Console.ReadLine();
        }

        static void DrawUnionJack(int n)
        {
            //Write your solution here

            int dim = n * 2 - 1;

            char[,] square = new char[dim, dim];


            for (int i = 0; i < dim; i++)
            {
                //fill center horizontal
                square[i, n - 1] = '*';

                
                //fill center vertical
                square[n - 1, i] = '*';

               
                //fill (/) rise line
                square[i, i] = '*';
                
                //fill (\) down line
                square[dim-1-i, i] = '*';                
            }

            PrintJack(square);
        }

        static void PrintJack(char[,] square)
        {
            for (int i = 0; i < square.GetLength(0); i++)
            {
                for (int j = 0; j < square.GetLength(1); j++)
                {
                    Console.Write((square[i, j] == '\0' )? ' ': square[i, j]);
                }

                Console.Write('\n');
            }
        }
    }
}
