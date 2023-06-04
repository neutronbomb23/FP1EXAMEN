using System;

namespace MyApp // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const int MAX_FALLOS = 10;
            int fallos = 0;
            bool acierto = false;
            string pal;
            Selecciona("Palabras.txt", out pal);
            string pal1 = EligePlabra();
            bool[] descubiertas = new bool[pal.Length];
            int tama = pal.Length;
            bool[] probadas = new bool[((int)'Z') - ((int)'A') + 1];
            for (int i = 0; i < probadas.Length; i++) { probadas[i] = false; }
            Console.WriteLine("La palabra tiene {0} letras", tama);
            Console.Write(pal1);
            while (!PalabraAcertada(descubiertas) && fallos < MAX_FALLOS)
            {
                acierto = false;
                Muestra(pal, descubiertas, fallos);
                char let = LeeLetra2(probadas);
                DescubreLetras(pal, descubiertas, let, ref acierto);
                if (!acierto)
                {
                    fallos++;
                    Console.WriteLine(" ¡Letra incorrecta! Llevas {0} fallos.", fallos);
                }
            }

            if (PalabraAcertada(descubiertas))
            {
                Console.WriteLine("\n Palabra acertada: {0}", pal);
            }
            else if (fallos >= MAX_FALLOS)
            {
                Console.WriteLine("Has alcanzado el número máximo de fallos!");
            }
        }

        public static void Muestra(string pal, bool[] descubiertas, int fallos)
        {
            for (int i = 0; i < pal.Length; i++)
            {
                if (descubiertas[i] == true)
                {
                    Console.Write(pal[i]);
                }
                else
                {
                    Console.Write("-");
                }
            }
            Console.WriteLine("  Número de fallos: {0}", fallos);
        }

        public static bool PalabraAcertada(bool[] descubiertas)
        {
            for (int i = 0; i < descubiertas.Length; i++)
            {
                if (!descubiertas[i])
                {
                    return false;
                }
            }
            return true;
        }

        public static char LeeLetra()
        {
            char c;
            Console.WriteLine("Cuál es tu siguiente letra?");
            c = Console.ReadLine().ToUpper()[0];
            if (c == ' ')
            {
                Console.WriteLine("Introduce una letra por favor.");
            }
            return c;
        }

        public static void Selecciona(string file, out string palabra)
        {
            int numPalabras;
            string[] palabras;

            using (StreamReader reader = new StreamReader(file))
            {
                numPalabras = int.Parse(reader.ReadLine());
                palabras = new string[numPalabras];

                for (int i = 0; i < numPalabras; i++)
                {
                    palabras[i] = reader.ReadLine();
                }
            }

            Random random = new Random();
            int indice = random.Next(numPalabras);
            palabra = palabras[indice];
        }

        public static void DescubreLetras(string pal, bool[] descubiertas, char let, ref bool acierto)
        {
            for (int i = 0; i < pal.Length; i++)
            {
                if (pal[i] == let)
                {
                    descubiertas[i] = true;
                    acierto = true;
                }
            }
        }

        public static string EligePlabra()
        {
            Random rnd = new Random();
            string[] palabras = { "PACO", "PACA", "ARRDEE", "CENTRALCEE", "NEWCASTLE", "ISAK", "NICK", "POPE" };
            int a = rnd.Next(0, palabras.Length);
            return palabras[a];
        }

        public static char LeeLetra2(bool[] probadas)
        {
            char letra = ' '; // Inicializamos con un carácter nulo
            Console.WriteLine("Disponibles:");

            char letraDisponible = 'A'; // Iniciamos con la letra 'A'

            for (int i = 0; i < probadas.Length; i++)
            {
                if (!probadas[i])
                {
                    Console.Write(" " + letraDisponible);
                }
                letraDisponible++; // Pasamos a la siguiente letra
            }
            Console.WriteLine();

            bool letraValida = false;

            while (!letraValida)
            {
                Console.Write("Ingresa una letra: ");
                letra = Console.ReadLine().ToUpper()[0];

                if (letra >= 'A' && letra <= 'Z')
                {
                    int indice = letra - 'A';

                    if (!probadas[indice])
                    {
                        probadas[indice] = true;
                        letraValida = true;
                    }
                    else
                    {
                        Console.WriteLine("Letra ya ha sido probada. Intenta nuevamente.");
                    }
                }
                else
                {
                    Console.WriteLine("Letra no válida. Intenta nuevamente.");
                }
            }
            return letra;
        }

    }
}