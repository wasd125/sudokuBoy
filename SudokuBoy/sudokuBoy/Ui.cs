using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuBoy
{
    public class Ui
    {
        public void ZeicheGrid(int[,] zahlen)
        {
            Console.Clear();

            for (int y = 0; y < 9; y++)
            {
                if (y > 0 && y % 3 == 0)
                    Console.WriteLine("-----------");

                var zeile = "";
                for (int x = 0; x < 9; x++)
                {
                    if (x > 0 && x % 3 == 0)
                        zeile += "|";
                    zeile += zahlen[x, y];                    
                }
                Console.WriteLine(zeile);
            }
        }
    }
}
