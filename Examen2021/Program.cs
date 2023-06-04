using System;
using System.IO;

namespace takuzu
{
    class Program
    {
        static void Main(string[] args)
        {
            // Declaración e inicialización del tablero y las celdas fijas (predefinidas)
            const int N = 4;
            char[,] tab;
            tab = new char[N, N];
            bool[,] fijas = new bool[N, N];
            int fil = 0, col = 0;

            Console.WriteLine("¿Desea cargar un tablero desde un archivo? (s/n)");
            char opcion = Console.ReadKey(true).KeyChar;

            if (opcion == 's')
            {
                Console.WriteLine("Ingrese la ruta del archivo:");
                string archivo = Console.ReadLine();
                LeeArchivo(archivo, ref tab);
            }
            else
            {
                tab = new char[N, N] {
            {'.','1','.','0'},
            {'.','.','0','.'},
            {'.','0','.','.'},
            {'1','1','.','0'}
        };
            }

            // Marcar las celdas fijas (predefinidas) en el tablero
            InicializaFijas(tab, fijas, fil, col);

            // Bucle principal del juego: continuar mientras el tablero no esté completo
            while (!TabLleno(tab))
            {
                // Dibujar el tablero en la consola
                Renderiza(tab, fijas, fil, col);

                // Leer la entrada del usuario (teclado)
                char input = LeeInput();

                // Salir del juego si el usuario presiona 'q'
                if (input == 'q')
                {
                    break;
                }

                // Procesar la entrada del usuario y actualizar el tablero
                ProcesaInput(input, tab, fijas, ref fil, ref col);
            }

            // Mostrar el resultado al final del juego
            MuestraResultado(tab);
        }

        // También se puede hacer así:
        public static void InicializaFijas(char[,] tab, bool[,] fijas, int fil, int col)
        {
            for (int i = 0; i < tab.GetLength(0); i++)
            {
                for (int j = 0; j < tab.GetLength(1); j++)
                {
                    if (tab[i, j] != '.')
                    {
                        fijas[i, j] = true;
                    }
                }
            }

            fil = 0;
            col = 0;
        }

        // Dibuja el tablero en la consola, resaltando las celdas fijas y la posición del cursor
        public static void Renderiza(char[,] tab, bool[,] fijas, int fil, int col)
        {
            Console.Clear();
            int N = tab.GetLength(0);
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    if (fijas[i, j])
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    }
                    Console.Write(" " + tab[i, j]);
                }
                Console.WriteLine();
            }
            Console.SetCursorPosition(col * 2, fil);
            Console.ResetColor();
        }

        // Procesa la entrada del usuario y actualiza el tablero y la posición del cursor
        public static void ProcesaInput(char c, char[,] tab, bool[,] fijas, ref int fil, ref int col)
        {
            int N = tab.GetLength(0);
            switch (c)
            {
                case 'u':
                    if (fil > 0) fil--;
                    break;
                case 'd':
                    if (fil < N - 1) fil++;
                    break;
                case 'l':
                    if (col > 0) col--;
                    break;
                case 'r':
                    if (col < N - 1) col++;
                    break;
                case '0':
                case '1':
                case '.':
                    if (!fijas[fil, col])
                    {
                        tab[fil, col] = c;
                    }
                    break;
            }
        }

        // Verifica si el tablero está lleno (sin celdas vacías)
        public static bool TabLleno(char[,] tab)
        {
            int N = tab.GetLength(0);
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    if (tab[i, j] == '.')
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        // Extrae las filas y columnas del tablero en los arreglos filk y colk
        public static void SacaFilCol(int k, char[,] tab, char[] filk, char[] colk)
        {
            int N = tab.GetLength(0);
            for (int i = 0; i < N; i++)
            {
                filk[i] = tab[k, i];
                colk[i] = tab[i, k];
            }
        }

        // Verifica si hay tres elementos iguales seguidos en un arreglo
        public static bool TresSeguidos(char[] v)
        {
            for (int i = 0; i < v.Length - 2; i++)
            {
                if (v[i] == v[i + 1] && v[i + 1] == v[i + 2])
                {
                    return true;
                }
            }
            return false;
        }

        // Verifica si la cantidad de ceros y unos en un arreglo es igual
        public static bool IgCerosUnos(char[] v)
        {
            int count0 = 0, count1 = 0;
            foreach (char c in v)
            {
                if (c == '0')
                {
                    count0++;
                }
                else if (c == '1')
                {
                    count1++;
                }
            }
            return count0 == count1;
        } // Intentar hacer esto sin un foreach.

        // Muestra el resultado al final del juego, verificando si el tablero cumple con las reglas
        public static void MuestraResultado(char[,] tab)
        {
            int N = tab.GetLength(0);
            bool correcto = true;

            for (int k = 0; k < N; k++)
            {
                char[] filk = new char[N];
                char[] colk = new char[N];
                SacaFilCol(k, tab, filk, colk);

                if (TresSeguidos(filk) || TresSeguidos(colk) || !IgCerosUnos(filk) || !IgCerosUnos(colk))
                {
                    correcto = false;
                    Console.WriteLine($"La fila {k} o columna {k} no cumplen con las reglas.");
                }
            }

            if (correcto)
            {
                Console.WriteLine("\n");
                Console.WriteLine("\n ¡Felicitaciones! El tablero es correcto.");
            }
            else
            {
                Console.WriteLine("\n");
                Console.WriteLine("El tablero no es correcto.");
            }
        }

        // Lee el contenido de un archivo y carga el tablero desde él
        public static void LeeArchivo(string file, ref char[,] tab)
        {
            using (StreamReader sr = new StreamReader(file))
            {
                int N = int.Parse(sr.ReadLine());
                tab = new char[N, N];
                for (int i = 0; i < N; i++)
                {
                    string line = sr.ReadLine();
                    for (int j = 0; j < N; j++)
                    {
                        tab[i, j] = line[j];
                    }
                }
            }
        }

        // Lee la entrada del usuario y devuelve un carácter que representa la acción
        static char LeeInput()
        {
            char d = ' ';
            while (d == ' ')
            {
                if (Console.KeyAvailable)
                {
                    string tecla = Console.ReadKey(true).Key.ToString();
                    switch (tecla)
                    {
                        case "LeftArrow": d = 'l'; break;
                        case "UpArrow": d = 'u'; break;
                        case "RightArrow": d = 'r'; break;
                        case "DownArrow": d = 'd'; break;
                        case "D0": d = '0'; break;  // dígito 0
                        case "D1": d = '1'; break;  // dígito 1
                        case "Spacebar": d = '.'; break;  // casilla vacía
                        case "Escape": d = 'q'; break;  // terminar
                        default: d = ' '; break;
                    }
                }
            }
            return d;
        } // LeeInput
    }
}

