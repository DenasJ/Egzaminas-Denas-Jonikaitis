using System;
using System.Threading.Tasks;

namespace SlaptazodzioAtkurimas
{
    public class bruteforceatkoduotojas
    {
        private slaptazodziomanageris slaptazodziomanageris;
        private readonly char[] simboliai = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789".ToCharArray();
        private readonly int MaxIlgis = 6;
        private string atkoduotasSlaptazodis;

        public bruteforceatkoduotojas()
        {
            slaptazodziomanageris = new slaptazodziomanageris();
        }

        public string atkoduotiSlaptazodi(string uzkoduotasSlaptazodis)
        {
            atkoduotasSlaptazodis = null;

            Parallel.For(1, MaxIlgis + 1, new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount }, length =>
            {
                if (atkoduotasSlaptazodis == null)
                {
                    rekursijaSlaptazodis(new char[length], 0, length, uzkoduotasSlaptazodis);
                }
            });

            return atkoduotasSlaptazodis ?? "Nerasta";
        }

        private void rekursijaSlaptazodis(char[] prefix, int position, int length, string uzkoduotasSlaptazodis)
        {
            if (atkoduotasSlaptazodis != null || position == length)
            {
                return;
            }

            foreach (char character in simboliai)
            {
                prefix[position] = character;
                string bandymas = new string(prefix);
                if (slaptazodziomanageris.PatvirtintiSlaptazodi(bandymas, uzkoduotasSlaptazodis))
                {
                    atkoduotasSlaptazodis = bandymas;
                    return;
                }
                if (atkoduotasSlaptazodis == null)
                {
                    rekursijaSlaptazodis(prefix, position + 1, length, uzkoduotasSlaptazodis);
                }
            }
        }
    }
}