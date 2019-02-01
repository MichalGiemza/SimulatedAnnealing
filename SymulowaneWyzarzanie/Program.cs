using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SymulowaneWyzarzanie
{
    public class Program
    {
        static void Main(string[] args)
        {
            int ilosc = 10;
            double wagaMax = 20;
            var waga = new double[] { 1.0, 2.0, 3.0, 4.0, 5.0, 6.0, 7.0, 8.0, 9.0, 10.0 };
            var wartosc = new double[] { 1.0, 2.0, 3.0, 4.0, 5.0, 6.0, 7.0, 8.0, 9.0, 10.0 };

            SymulowaneWyzarzanie sa = new SymulowaneWyzarzanie(waga, wartosc, ilosc, wagaMax, true);

            sa.rozwiaz();

            Console.Read();
        }
    }
}
