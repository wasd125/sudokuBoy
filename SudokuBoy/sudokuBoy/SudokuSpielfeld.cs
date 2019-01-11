using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuBoy
{
    public class SudokuSpielfeld
    {
        public SudokuSpielfeld()
        {
            _zellen = new SudokuZelle[9, 9];
            _ui = new Ui();
            for (int x = 0; x < 9; x++)
                for (int y = 0; y < 9; y++)
                    _zellen[x, y] = new SudokuZelle(x,y);
        }

        public SudokuSpielfeld(SudokuSpielfeld spielfeld)
        {
            _zellen = new SudokuZelle[9, 9];
            _ui = new Ui();
            for (int x = 0; x < 9; x++)
                for (int y = 0; y < 9; y++)
                    _zellen[x, y] = new SudokuZelle(spielfeld._zellen[x, y],x,y);
        }

        private SudokuZelle[,] _zellen;
        private Ui _ui;

        private static bool _geloest = false;

        public static implicit operator int[,] (SudokuSpielfeld spielfeld)
        {
            var feldAlsInt = new int[9, 9];

            for (int x = 0; x < 9; x++)
                for (int y = 0; y < 9; y++)
                    feldAlsInt[x, y] = spielfeld._zellen[x, y];

            return feldAlsInt;
        }

        public void SetzeZahlen(int[,] zahlen)
        {
            for (int x = 0; x < 9; x++)
                for (int y = 0; y < 9; y++)
                    _zellen[y, x].SetzeWert(zahlen[x, y]);

            Console.WriteLine("Initiales Sudoku:");
            _ui.ZeicheGrid(this);
        }

        public void SetzeZahl(int x, int y, int wert)
        {
            _zellen[x,y].SetzeWert(wert);
        }

        public bool Loesen()
        {
            do
            {
                var zelleGeaendert = false;
                for (int x = 0; x < 9; x++)
                    for (int y = 0; y < 9; y++)
                    {
                        var zelle = _zellen[x, y];
                        if (zelle.IstFertig)
                            continue;

                        var aktulleZelleGeaendert  = ZellePruefen(x, y, zelle);

                         if (zelleGeaendert == false && aktulleZelleGeaendert)
                            zelleGeaendert = true;
                    }

                if (GetZellenNachAnzahlWerten().Any(p => p.IstUnloesbar))
                    return false;

                if (zelleGeaendert == false)
                {
                    LoeseRekursiv();
                    if (_geloest)
                        return true;
                }

            } while (HatUnfertigeZelle);

            return true;
        }

        private void LoeseRekursiv()
        {
            var zellen = GetZellenNachAnzahlWerten();

            foreach (var zelle in zellen)
            {               
                foreach (var wert in zelle.MoeglicheWerte)
                {
                    var spielfeldKopie = new SudokuSpielfeld(this);
                    spielfeldKopie.SetzeZahl(zelle.PositionX, zelle.PositionY, wert);
                    _geloest = spielfeldKopie.Loesen();

                    if (_geloest)
                    {
                        Console.WriteLine("");
                        Console.WriteLine("Gelöstes Sudoku:");
                        _ui.ZeicheGrid(spielfeldKopie);
                        _geloest = true;
                        return;
                    }
                }
            }
        }

        private bool ZellePruefen(int posX, int posY, SudokuZelle zelle)
        {
            var moeglicheWerte = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            ReihePruefen(posX, posY, moeglicheWerte);
            SpaltePruefen(posX, posY, moeglicheWerte);
            BlockPruefen(posX, posY, moeglicheWerte);

            zelle.MoeglicheWerte = moeglicheWerte;

            if (moeglicheWerte.Count == 1)
            {
                zelle.SetzeWert(moeglicheWerte.FirstOrDefault());
                return true;
            }

            return false;
        }

        private void ReihePruefen(int posX, int posY, List<int> moeglicheWerte)
        {
            for (int x = 0; x < 9; x++)        
            {
                if (posX == x)
                    continue;
                var nachbarZelle = _zellen[x, posY];
                if (nachbarZelle.IstFertig && moeglicheWerte.Contains(nachbarZelle.Wert))
                    moeglicheWerte.Remove(nachbarZelle.Wert);
            }
        }

        private void SpaltePruefen(int posX, int posY, List<int> moeglicheWerte)
        {
            for (int y = 0; y < 9; y++)
            {
                if (posY == y)
                    continue;
                var nachbarZelle = _zellen[posX, y];
                if (nachbarZelle.IstFertig && moeglicheWerte.Contains(nachbarZelle.Wert))
                    moeglicheWerte.Remove(nachbarZelle.Wert);
            }
        }

        private void BlockPruefen(int posX, int posY, List<int> moeglicheWerte)
        {
            int startX = 0;
            int startY = 0;

            if (posX <= 2)
                startX = 0;
            else if (posX <= 5)
                startX = 3;
            else
                startX = 6;

            if (posY <= 2)
                startY = 0;
            else if (posY <= 5)
                startY = 3;
            else
                startY = 6;

            for (int x = startX; x < startX + 3; x++)
                for (int y = startY; y < startY + 3; y++)
                {
                    if (posX == y || posY == y)
                        continue;

                    var nachbarZelle = _zellen[x, y];
                    if (nachbarZelle.IstFertig && moeglicheWerte.Contains(nachbarZelle.Wert))
                        moeglicheWerte.Remove(nachbarZelle.Wert);
                }

        }

        private bool HatUnfertigeZelle
        {
            get
            {
                for (int x = 0; x < 9; x++)
                    for (int y = 0; y < 9; y++)
                        if (_zellen[x, y].IstFertig == false)
                            return true;

                return false;
            }
        }

        private List<SudokuZelle> GetZellenNachAnzahlWerten()
        {
            var sudokuZellen = new List<SudokuZelle>();
            for (int x = 0; x < 9; x++)
            {
                for (int y = 0; y < 9; y++)
                {
                    var zelle = _zellen[x, y];
                    if (zelle.IstFertig)
                        continue;

                    sudokuZellen.Add(zelle);
                }
            }
            return sudokuZellen.OrderBy(p => p.AnzahlWerte).ToList();
        }
    }
}
