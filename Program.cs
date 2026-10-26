using semana_11;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Semana_11
{
    internal class Program
    {
        static int Maxima_Estudiante = 40;
        static int Maxima_Cursos = 4;
        static string[] Nombre_De_Cursos = { "Matematicas", "Fisica", "Base de datos", "Programacion" };
        static string[] Nombres_Estudiante = new string[Maxima_Estudiante];
        static double[,] Matriz_Notas_Estudiante = new double[Maxima_Estudiante, Maxima_Cursos];
        static int Total_Estudiante = 0;
        static string Ruta_Archivo = "Notas_Cursos.txt";
        public static void Registrar_Estudiante()
        {
            Console.WriteLine("********  Registro de Estudiante  ***********");
            if ( Total_Estudiante >= Maxima_Estudiante)
            {
                Console.WriteLine("▓▓▓ Error: Se ha alcanzado el limite de estudiantes ▓▓▓");
                return;
            }
            string Nombre = Validador.LeerTexto("Ingresar el Nombres y Apellidos del Estudiante: ").ToUpper();
            Nombres_Estudiante[Total_Estudiante] = Nombre;
            Console.WriteLine("Ingresar notas para cada curso [0 - 20]");
            for ( int i = 0; i < Maxima_Cursos; i++)
            {
                Matriz_Notas_Estudiante[Total_Estudiante, i] = Validador.leerDecimal($"Ingrese la nota del curso {Nombre_De_Cursos[i]}: ");
            }
            Total_Estudiante++;
            Console.WriteLine("--> ✅ Estudiante Registrado dentro del Programa ✅ <--");
        }
        public static void Guarda_Archivo()
        {
            Console.WriteLine(new string('=', 50));
            Console.WriteLine("********  Guardar Archivo de Notas  ***********");
            Console.WriteLine(new string('=', 50));
            if (Total_Estudiante == 0)
            {
                Console.WriteLine("▓▓▓ Error: No hay estudiantes registrados para guardar ▓▓▓");
                return;
            }
            using (StreamWriter escribir = new StreamWriter(Ruta_Archivo))
            {
                for ( int i = 0; i < Total_Estudiante; i++)
                {
                    string Linea = Nombres_Estudiante[i];
                    for ( int j = 0; j < Maxima_Cursos; j++)
                    {
                        Linea += $",{Matriz_Notas_Estudiante[i, j]}";
                    }
                    escribir.WriteLine(Linea);
                }
            }
            Console.WriteLine($"--> ✅ Archivo de Notas Guardado en {Ruta_Archivo} ✅ <--");
        }
        public static void Cargar_Registro()
        {
            Console.WriteLine(new string('=', 50));
            Console.WriteLine("********  Cargar Registro de Notas  ***********");
            Console.WriteLine(new string('=', 50));
            if (!File.Exists(Ruta_Archivo))
            {
                Console.WriteLine("▓▓▓ Error: No se encontro el archivo de notas ▓▓▓");
                return;
            }
            Total_Estudiante = 0;
            using (StreamReader Lector = new StreamReader(Ruta_Archivo))
            {
                string Linea;
                while ((Linea = Lector.ReadLine()) !=null && Total_Estudiante < Maxima_Estudiante){
                    string[] Partes = Linea.Split(',');
                    if ( Partes.Length == Maxima_Cursos + 1)
                    {
                        Nombres_Estudiante[Total_Estudiante] = Partes[0];
                        for ( int i = 0; i <Maxima_Cursos; i++)
                        {
                            Matriz_Notas_Estudiante[Total_Estudiante, i] = Convert.ToDouble(Partes[i + 1]);
                        }
                        Total_Estudiante++;
                    }
                }
            }
            Console.WriteLine($"--> ✅ Registro de Notas Cargado desde {Ruta_Archivo} ✅ <--");
        }
        public static void Mostrar_Registro()
        {
            Console.WriteLine(new string('=', 50));
            Console.WriteLine("********  Mostrar Registro de Notas  ***********");
            Console.WriteLine(new string('=', 50));
            if (Total_Estudiante == 0)
            {
                Console.WriteLine("▓▓▓ Error: No hay estudiantes registrados para mostrar ▓▓▓");
                return;
            }
            Console.Write($"{"Estudiante", -25}|" );
            for ( int i = 0; i < Maxima_Cursos; i++)
            {
                Console.Write($"{Nombre_De_Cursos[i],-12}|");
            }
            Console.WriteLine("Promedio");
            Console.WriteLine(new string('-', 100));
            for (int i = 0; i < Maxima_Cursos; i++)
            {
                Console.Write($"{Nombres_Estudiante[i],-25}|");
                double suma = 0;
                for ( int j = 0; j < Maxima_Cursos; j++)
                {
                    Console.Write($"{Matriz_Notas_Estudiante[i, j],-12:F1}|");
                    suma += Matriz_Notas_Estudiante[i, j];
                }
                double Promedio = suma / Maxima_Cursos;
                string Estado = Promedio >= 12 ? "--> ✅ APROBADO ✅ <--" : "--> ❎ DESAPORBADO ❎ <--";
                Console.WriteLine($"{Promedio:F2}({Estado})");
            }

        }
        static void Main(string[] args)
        {
            int opc = 0;
            while (opc != 5)
            {
                Console.Clear();
                Console.WriteLine("********  Menu de Opciones  ***********");
                Console.WriteLine("1. Registrar Estudiante");
                Console.WriteLine("2. Guardar Archivo de Notas");
                Console.WriteLine("3. Cargar Archivo");
                Console.WriteLine("4. Mostrar Registros");
                Console.WriteLine("5. Salir del Programa");
                opc = Validador.leerEnteros("Seleccione la opcion que quiera realizar [1-5]: ");
                switch (opc)
                {
                    case 1:
                        Console.Clear();
                        Registrar_Estudiante();
                        break;
                    case 2:
                        Console.Clear();
                        Guarda_Archivo();
                        break;
                    case 3:
                        Console.Clear();
                        Cargar_Registro();
                        break;
                    case 4:
                        Console.Clear();
                        Mostrar_Registro();
                        break;
                    case 5:
                        Console.Clear();
                        Console.WriteLine("--> Saliendo del Programa... <--");
                        return;
                    default:
                        Console.WriteLine("▓▓▓ Error: Opcion no valida ▓▓▓");
                        break;
                }
                Console.ReadKey();
            }
        }
    }
}
