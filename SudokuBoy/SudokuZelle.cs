using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuBoy
{
    public class SudokuZelle
    {
        private List<int> _moeglicheWerte = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

        public static implicit operator int(SudokuZelle zelle)
        {
            return zelle.Wert;
        }

        public void EntferneWert(int wert)
        {
            if (_moeglicheWerte.Contains(wert))
                _moeglicheWerte.Remove(wert);
        }

        public void SetzeWert(int wert)
        {
            if (wert == 0)
                return;

            _moeglicheWerte = new List<int> { wert };
        }

        public bool IstFertig
        {
            get { return _moeglicheWerte.Count == 1; }
        }

        public int Wert
        {
            get
            {
                if (IstFertig)
                    return _moeglicheWerte.FirstOrDefault();
                else
                    return 0;
            }
        }
    }
}
