using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SymulowaneWyzarzanie
{
    public class SymulowaneWyzarzanie
    {
        // Pola
        private int iterMax;
        private double tempPocz;
        private double temp;
        private double epsilon;
        private bool czyWypisywac;

        public double[] waga;
        public double[] wartosc;
        private int ilosc;
        private double wagaMax;

        private bool[] rozw;

        // Konstruktor
        public SymulowaneWyzarzanie(double[] waga, double[] wartosc, int ilosc, double wagaMax, bool czyWypisywac = false)
        {
            iterMax = 100000;
            tempPocz = 10000;
            temp = tempPocz;
            epsilon = 0.001;
            this.ilosc = ilosc;
            this.wagaMax = wagaMax;
            this.czyWypisywac = czyWypisywac;

            rozw = new bool[ilosc];
            for (int i = 0; i < ilosc; i++)
                rozw[i] = false;

            this.waga = new double[ilosc];
            waga.CopyTo(this.waga, 0);

            this.wartosc = new double[ilosc];
            wartosc.CopyTo(this.wartosc, 0);
        }
        public SymulowaneWyzarzanie(int iterMax, double tempPocz, double epsilon, double rozwStart, bool[] rozw, double[] waga, double[] wartosc, int ilosc, double wagaMax, bool czyWypisywac = false)
        {
            this.iterMax = iterMax;
            this.tempPocz = tempPocz;
            temp = tempPocz;
            this.epsilon = epsilon;
            this.ilosc = ilosc;
            this.wagaMax = wagaMax;
            this.czyWypisywac = czyWypisywac;

            rozw = new bool[ilosc];
            for (int i = 0; i < ilosc; i++)
                rozw[i] = false;

            this.waga = new double[ilosc];
            waga.CopyTo(this.waga, 0);

            this.wartosc = new double[ilosc];
            wartosc.CopyTo(this.wartosc, 0);
        }
        // Metody
        private double funkcja(bool[] r)
        {
            double suma = 0;
            for (int i = 0; i < ilosc; i++)
            {
                if (r[i] == true)
                    suma += wartosc[i];
            }
            return suma;
        }
        private void losujRozwiazanie(double prawd, bool[] tmp)
        {
            Random rand = new Random();
            do
            {
                for (int i = 0; i < ilosc; i++)
                {
                    if (rand.NextDouble() < prawd)
                        tmp[i] ^= true;
                }
            } while (sumaWag(tmp) > wagaMax);
        }
        private double zmienTemp(int iter, double temp)
        {
            return temp * Math.Exp(-iter/100.0);
        }
        private bool[] akceptujRozwizanie(bool[] tmpRozw, bool[] rozw)
        {
            Random rand = new Random();
            bool[] result = new bool[ilosc];

            if (funkcja(rozw) <= funkcja(tmpRozw))
                tmpRozw.CopyTo(result, 0);
            else
            {
                if (Math.Exp(-((funkcja(rozw) - funkcja(tmpRozw)) / temp)) > rand.NextDouble())
                    tmpRozw.CopyTo(result, 0);
                else
                    rozw.CopyTo(result, 0);
            }
            return result;
        }
        private double sumaWag(bool[] r)
        {
            double suma = 0;
            for (int i = 0; i < ilosc; i++)
            {
                if (r[i] == true)
                    suma += waga[i];
            }
            return suma;
        }
        public void wypisz(bool[] r)
        {
            Console.Out.Write("Wagi: \t");
            int i = 0;
            foreach (var a in r)
            {
                if (a == true)
                    Console.ForegroundColor = ConsoleColor.Cyan;
                else
                    Console.ResetColor();

                Console.Out.Write("{0:F2} ", waga[i]);
                i++;
            }
            Console.Out.WriteLine();
            Console.ResetColor();

            Console.Out.Write("Wart: \t");
            i = 0;
            foreach (var a in r)
            {
                if (a == true)
                    Console.ForegroundColor = ConsoleColor.Cyan;
                else
                    Console.ResetColor();

                Console.Out.Write("{0:F2} ", wartosc[i]);
                i++;
            }
            Console.Out.WriteLine();
            Console.ResetColor();
        }
        public bool[] rozwiaz()
        {
            bool[] tmpRozw = new bool[ilosc];
            for (int i = 1; (i < iterMax) && (temp > epsilon); i++)
            {
                losujRozwiazanie(0.5, tmpRozw);
                rozw = akceptujRozwizanie(tmpRozw, rozw);

                if (czyWypisywac == true)
                    Console.WriteLine("I: {0}\t Wart: {1:F4}\t Waga: {2:F4}\t T: {3:F4}", i, funkcja(rozw), sumaWag(rozw), temp);

                temp = zmienTemp(i, tempPocz);
            }
            wypisz(rozw);
            
            return rozw;
        }
    }
}