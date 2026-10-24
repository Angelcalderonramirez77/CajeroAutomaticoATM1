using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace CajeroAutomaticoATM
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int opciones = 0;
            decimal saldo = 1000m;
            decimal limiteRetiro= 500m;
            bool cuentaActiva = true;
            string nombre = string.Empty;
            string cuenta = "100245 ";

            Console.WriteLine("================================");
            Console.WriteLine("     BIENVENIDO AL SISTEMA       ");
            Console.WriteLine("================================");
            // proceso Nombre
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("Ingrese su nombre por favor: ");
            nombre = Console.ReadLine();
            Console.Write("\n");
            // ingrese su numero de cuenta
            Console.Write("Ingrese su numero de cuenta por favor: ");
            cuenta = Console.ReadLine();
            Console.WriteLine("\n");

            if (cuenta != "100245")
            {
                while (cuenta != "100245")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Error: Numero de cuenta no activo.");
                    Console.WriteLine("\n");
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("Ingrese su numero de cuenta por favor: ");
                    cuenta = Console.ReadLine();


                }
            }
            else 
            { 

                Console.WriteLine($"Numero de cuenta activo: {cuenta}");
                do
                {
                    Console.WriteLine("\n");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("================================");
                    Console.WriteLine(" *******MENU PRINCIPAL**********");
                    Console.WriteLine("1. Consultar Saldo");
                    Console.WriteLine("2. Depositar");
                    Console.WriteLine("3. Retirar");
                    Console.WriteLine("4. Salir");
                    Console.Write("Ingrese una Opcion: ");
                    if (!int.TryParse(Console.ReadLine(), out opciones))
                    {
                        Console.WriteLine("Error: Por favor, ingrese un número válido.");
                        continue;
                    }

                    switch (opciones)
                    {
                        case 1:
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine($"\nEl saldo Disponible es de RD${saldo:N2}");

                            break;

                        case 2:
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.Write("\n Ingrese el Monto a depositar: ");
                            decimal deposito;
                            if (!decimal.TryParse(Console.ReadLine(), out deposito) || deposito <= 0)
                            {
                                Console.WriteLine("Error: Ingrese un monto válido mayor a 0.");
                                break;
                            }
                            saldo += deposito;
                            Console.WriteLine($"¡Depósito exitoso! Has sumado RD${deposito:N2} a tu cuenta ");
                            break;


                        case 3:
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine("\n ===== RETIRO====");
                            Console.Write("Ingrese el Monto a retirar: ");
                            decimal retiro;
                            if (!decimal.TryParse(Console.ReadLine(), out retiro)
                                 || retiro <= 0)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Ingrese un monto válido mayor que cero.");
                                break;
                            }
                            if (!cuentaActiva)
                            {
                                Console.WriteLine("La cuenta está inactiva.");
                            }
                            else if (retiro > saldo)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Fondos insuficientes.");
                                Console.WriteLine($"Saldo disponible: RD${saldo:N2}");
                            }
                            else
                            {
                                saldo -= retiro;
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("¡Retiro realizado exitosamente!");
                                Console.WriteLine($"Monto retirado: RD${retiro:N2}");
                                Console.WriteLine($"Nuevo saldo: RD${saldo:N2}");
                            }

                            break;

                        case 4:
                            // SALIR DEL SISTEMA
                            Console.ForegroundColor = ConsoleColor.Yellow;

                            Console.WriteLine("\n====================================");
                            Console.WriteLine($"Gracias por utilizar nuestro cajero, {nombre}.");
                            Console.WriteLine($"Numero de cuenta activo: {cuenta}");
                            Console.WriteLine("¡Hasta pronto!");
                            Console.WriteLine("====================================");
                            break;

                        default:
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Opción incorrecta. Seleccione del 1 al 4.");
                            break;
                    }



                } while (opciones != 4);

                Console.ResetColor();

            } 

        }
    }
}
