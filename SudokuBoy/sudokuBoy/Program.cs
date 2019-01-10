using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuBoy
{
    class Program
    {
        static void Main(string[] args)
        {
            var spielfeld = new SudokuSpielfeld();

            var zahlen = new int[,]
            {
                { 4,1,0,0,6,5,0,0,7},
                { 0,0,6,0,0,7,4,8,0},
                { 2,0,7,4,9,0,0,0,6},
                { 0,6,0,0,7,0,1,0,0},
                { 3,0,1,5,0,0,0,7,2},
                { 0,9,0,0,4,2,3,0,8},
                { 1,0,8,6,0,0,0,2,9},
                { 0,2,0,0,1,8,6,4,0},
                { 6,0,0,3,0,0,0,1,0}
            };

            spielfeld.SetzeZahlen(zahlen);

            spielfeld.Loesen();
            Console.ReadKey();
        }
    }
}
