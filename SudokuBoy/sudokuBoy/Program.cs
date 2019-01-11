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
                { 0,0,0,2,0,6,0,0,0},
                { 0,0,1,0,9,0,8,0,0},
                { 0,8,0,1,4,5,0,3,0},
                { 8,0,3,0,0,0,5,0,4},
                { 0,1,9,0,0,0,6,2,0},
                { 4,0,7,0,0,0,1,0,9},
                { 0,5,0,7,3,1,0,4,0},
                { 0,0,4,0,6,0,7,0,0},
                { 0,0,0,4,0,2,0,0,0}
            };

            spielfeld.SetzeZahlen(zahlen);

            spielfeld.Loesen();
            Console.ReadKey();
        }
    }
}
