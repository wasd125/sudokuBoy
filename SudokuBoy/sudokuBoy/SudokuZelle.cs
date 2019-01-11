using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuBoy
{
    public class SudokuZelle
    {
        public SudokuZelle(int posX, int posY)
        {
            MoeglicheWerte = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            PositionX = posX;
            PositionY = posY;
        }

        public SudokuZelle(SudokuZelle zelle,int posX, int posY)
            :this(posX,posY)
        {
            MoeglicheWerte = new List<int>();
            MoeglicheWerte.AddRange(zelle.MoeglicheWerte);
        }

        public int PositionX { get; private set; }
        public int PositionY { get; private set; }

        public static implicit operator int(SudokuZelle zelle)
        {
            return zelle.Wert;
        }

        public int AnzahlWerte
        {
            get { return MoeglicheWerte.Count; }
        }

        public List<int> MoeglicheWerte { get; set; }

        public void EntferneWert(int wert)
        {
            if (MoeglicheWerte.Contains(wert))
                MoeglicheWerte.Remove(wert);
        }

        public void SetzeWert(int wert)
        {
            if (wert == 0)
                return;

            MoeglicheWerte = new List<int> { wert };
        }

        public bool IstFertig
        {
            get { return MoeglicheWerte.Count == 1; }
        }

        public bool IstUnloesbar
        {
            get { return MoeglicheWerte.Count == 0; }
        }

        public int Wert
        {
            get
            {
                if (IstFertig)
                    return MoeglicheWerte.FirstOrDefault();
                else
                    return 0;
            }
        }
    }
}
