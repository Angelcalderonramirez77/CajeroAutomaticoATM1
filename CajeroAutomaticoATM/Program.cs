using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CajeroAutomaticoATM
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int opciones = 0;

    
            do
            {
                Console.WriteLine("================================");
                Console.WriteLine("     BIENVENIDO AL SISTEMA       ");
                Console.WriteLine("================================");
                Console.WriteLine(" *******MENU PRINCIPAL**********");
                Console.WriteLine("1. Consultar Saldo");
                Console.WriteLine("2. Depositar");
                Console.WriteLine("3. Retirar");
                Console.WriteLine("4. Salir");
                Console.Write("Ingrese una Opcion: ");
                if(!int.TryParse(Console.ReadLine(), out opciones))
                {
                    Console.WriteLine("Error: Por favor, ingrese un número válido.");
                    return;
                }

                switch (opciones)
                {
                    case 1:
                        Console.WriteLine("Consultar Saldo");
                    
                    break;


                    case 2:
                        Console.WriteLine("Has selecionado la opcion Depositar");


                     break;


                    case 3:
                        Console.WriteLine("Has selecionado la opcion Retirar");

                    break;


                    case 4:
                        Console.WriteLine("Has selecionado la opcion Salir");
                    break;


                    default:
                        Console.WriteLine("Opcion no valida");
                    break;

                }

                
                
            } 
            while (true);

        }
    }
}
