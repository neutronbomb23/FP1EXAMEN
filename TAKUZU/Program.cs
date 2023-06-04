// Nombre Apellido1 Apellido2
// Laboratorio:    Puesto:   

//*******************


using System;
using System.IO;

namespace takuzu
{
    class Program
    {
        static void Main(string[] args)
        {
            int N; // YA NO PUEDE SER CONSTANTE :/
            bool isPlaying = true;
            char[,] tab;
            bool[,] fijas;
            Console.WriteLine("Quieres jugar con un tablero de 4x4, 6x6 o 8x8? 4/6/8");
            int eleccion = int.Parse(Console.ReadLine());
            if (eleccion == 4)
            {
                N = 4;
                tab = new char[4, 4] {    // tablero del ejemplo   
                 {'.','1','.','0'},  // fila 0 
                 {'.','.','0','.'},  // fila 1
                 {'.','0','.','.'},  // etc
                 {'1','1','.','0'} };
                fijas = new bool[4, 4]; // matriz de posiciones fijas
            }
            else if (eleccion == 6)
            {
                tab = new char[6, 6];
                fijas = new bool[6, 6];
                LeeArchivo("takuzu6.txt", tab);
            }
            else
            {
                tab = new char[8, 8];
                fijas = new bool[8, 8];
                LeeArchivo("takuzu8.txt", tab);
            }
            
            
            int fil = 0, col = 0; // posición del cursor
            InicializaFijas(tab, fijas, fil, col);
            while (!TabLleno(tab) && isPlaying)
            { 
            Renderiza(tab, fijas, fil, col);
            char let = LeeInput();
            if(let == 'q')
            {
               isPlaying = false;
            }
            ProcesaInput(let, tab, fijas, ref fil, ref col);
            }
            MuestraResultado(tab);
        }

        public static void InicializaFijas(char[,] tab, bool[,] fijas, int fil, int col)
        {
            for(int i = 0; i < tab.GetLength(0); i++)
            {
                for(int j = 0; j < tab.GetLength(1); j++)
                {
                    if (tab[i,j] == '.')
                    {
                        fijas[i, j] = false;
                    }
                    else 
                    {
                        fijas[i, j] = true;
                    }
                }
            }
            fil = 0;
            col = 0;
        }

        public static void Renderiza(char[,] tab, bool[,] fijas, int fil, int col)
        {
            Console.Clear();
            for(int i = 0; i < tab.GetLength(0); i++)
            {
                for(int j = 0; j < tab.GetLength(0); j++)
                {
                    if (fijas[i, j])
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                    }
                    Console.Write(" " + tab[i,j]);
                }
                Console.WriteLine();
            }
            Console.ResetColor();
            Console.SetCursorPosition(col * 2, fil);
        }

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

        public static bool TabLleno(char[,] tab)
        {
            for(int i = 0;  i < tab.GetLength(0); i++)
            {
                for(int j = 0;  j < tab.GetLength(0); j++)
                {
                    if (tab[i,j] == '.')
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

        public static bool TresSeguidos(char[] v)
        {
            for(int i = 0; i < v.Length - 2; i++)
            {
                if (v[i] == v[i + 1] && v[i+1] == v[i + 2])
                {
                    return true;
                }
            }
            return false;
        }

        public static bool IgCerosUnos(char[] v)
        {
            int ceros = 0;
            int unos = 0;
            for(int i = 0; i < v.Length; i++)
            {
                if (v[i] == '0')
                {
                    ceros++;
                }
                else if (v[i] == '1')
                {
                    unos++;
                }
            }
            return ceros == unos;
        }


        // Muestra el resultado al final del juego, verificando si el tablero cumple con las reglas
        public static void MuestraResultado(char[,] tab)
        {
            int N = tab.GetLength(0);
            bool correcto = true;
            Console.SetCursorPosition(0,10);
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

        public static void LeeArchivo(string file, char[,] tab)
        {
            string[] lineas = File.ReadAllLines(file);
            int N = int.Parse(lineas[0]); // Tamaño del nuevo tablero
            for (int i = 0; i < N; i++)
            {
                string linea = lineas[i + 1]; // Ignorar la primera línea con el tamaño
                for (int j = 0; j < N; j++)
                {
                    tab[i, j] = linea[j];
                }
            }
        }

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
                        case "Spacebar": d = '.'; break;  // casilla vacia 
                        case "Escape": d = 'q'; break;  // terminar					
                        default: d = ' '; break;
                    }
                }
            }
          return d;
        }
    }
}
