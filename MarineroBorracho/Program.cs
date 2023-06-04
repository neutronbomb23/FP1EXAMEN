using System;

namespace MarineroBorracho // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Random rnd = new Random();
            int ancho;
            int largo;
            int pasos = 0;
            bool choff = false; 
            bool isPlaying = true;
            ancho = pideEntero("Ancho de la pista ", 1, 100);
            largo = pideEntero("largo de la pista ", 1, 300);
            int posicion = ancho / 2;
            dibuja(ancho, posicion, choff);
            while (!choff && isPlaying)
            {
              posicion = rnd.Next(0, ancho - 1);            
                if (posicion > ancho)
                {
                    choff = true;
                    isPlaying = false;
                }
                if (posicion < 1)
                {
                    choff = true;
                    isPlaying = false;
                } 

              dibuja(ancho,posicion, choff); 
              pasos++;

                if (pasos >= largo)
                {
                    Console.WriteLine("\nHAS GANADO!"); 
                    isPlaying = false;
                }
            }           
        }

        public static int pideEntero(string s, int min, int max)
        {
            int digit;
            Console.WriteLine(s);
            digit = int.Parse(Console.ReadLine());
            if(digit < min || digit > max)
            {
                Console.WriteLine("Número inválido.");
            }
            return digit;
            
        }

        public static void dibuja(int ancho, int posicion, bool choff)
        {
            for (int i = 0; i < ancho; i++)
            {
                if (i == posicion)
                {
                    if (choff)
                    {
                        Console.Write("choff");
                    }
                    else
                    {
                        Console.Write("0");
                    }
                }
                else if (!choff)
                {
                    Console.Write("-");
                }
            }
            Console.WriteLine();
        }
    }
}