using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace CajeroAutomaticoATM
{
    internal class Program
    {
        static void Main(string[] args)
        {
            byte opciones = 0;
            decimal retiro = 0m;
            decimal deposito = 0m;
            decimal saldo = 1000m;
            string nombre = string.Empty;
            Console.WriteLine("================================");
            Console.WriteLine("     BIENVENIDO AL SISTEMA       ");
            Console.WriteLine("================================");
            // proceso Nombre
            nombre = EntradaCliente();
            bool estado = EstadoCuenta(true);
            if (!estado)
            {
                Console.WriteLine("La cuenta está inactiva. No se puede realizar ninguna operación.");
            }
            else
            {
                Console.WriteLine($"Numero de cuenta activo: {estado}");
                do
                {
                    MostrarMenu();
                    byte opcion = LeerOpcion();

                    switch (opcion)
                    {
                        case 1:
                            ConsultarSaldo(saldo);

                            break;

                        case 2:
                            if(LeerMonto("Ingrese el monto a depositar: ", out decimal monto))
                            {
                                saldo = Depositar(saldo, deposito);
                            }
                            break;
                        case 3:
                            if (LeerMonto( "Ingrese el monto a retirar: ", out retiro))
                            {
                                saldo = Retirar(saldo, retiro);
                                break;
                            }
                           break;
                        case 4:
                            // SALIR DEL SISTEMA
                            Salidad(nombre);
                            break;

                        default:
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Opción incorrecta. Seleccione del 1 al 4.");
                            break;
                    }
                } while (opciones != 3);

                Console.ResetColor();
            }
        }
        // Metodo para ingresar el nombre del cliente
        public static string EntradaCliente()
        {
            string nombre;
            Console.Write("Ingrese su nombre por favor: ");
            nombre = Console.ReadLine();
            Console.WriteLine("\n");
            return nombre;
        }
        // Metodo para verificar el estado de la cuenta
        public static bool EstadoCuenta(bool estado)
        {
            return true;
        }
        // Metodo para mostrar el menu de opciones
        public static void MostrarMenu()
        {
            MensajeConsola("\n================================\n *******MENU PRINCIPAL**********" +
                "\n1.Consultar Saldo\n2. Depositar\n3. Retirar\n4. Salir");
        }
        public static byte LeerOpcion()
        {     
            Console.Write("Ingrese una Opcion: ");
            while (true)
            {
                if (byte.TryParse(Console.ReadLine(), out byte opciones))
                {
                    return opciones;
                }
                Console.Write(" Por favor, ingrese un número válido.");
            }
        }
        private static void ConsultarSaldo(decimal saldo)
        {
            MensajeConsola($"\nEl saldo Disponible es de RD${saldo:N2}");
        }
        public static bool LeerMonto(string mensaje, out decimal monto)
        {
            Console.WriteLine("\n{0}", mensaje);
            if (!decimal.TryParse(Console.ReadLine(), out monto) || monto <= 0m)
            {
                Console.WriteLine("Ingrese un monto válido mayor a 0. "); 
                return false;
            }
            return true;
        }
        public static decimal Depositar(decimal saldo, decimal monto)
        {
            if (monto <0)
            {
                MensajeError(" El monto a depositar debe ser mayor a 0.");
                return saldo;
            }
            else if (monto <= 1000000m)
            {
                MensajeError("Monto depositado fuera de lo normal");
                MensajeConsola("Monto Validado son RD$ 50,000");
                return saldo;
            }
            saldo += monto;
            MensajeConsola($"\n¡Depósito exitoso!\nMonto depositado: RD${monto:N2}\nNuevo saldo: RD${saldo:N2}");
            return saldo;
        }
        public static decimal Retirar(decimal saldo, decimal monto)
        {
            if (monto <= 0)
            {
                MensajeError(" El retiro debe ser debe der mayor a 0.");
                return saldo;
            }
            else if (monto > saldo)
            {
                MensajeConsola($" Fondo insuficiente. \n Saldo disponible: RD${saldo:N2}");
                return saldo;
            }
            else
            {
                saldo -= monto;
                Console.ForegroundColor = ConsoleColor.Green;
                MensajeConsola($"¡Retiro realizado exitosamente! \n Monto retirado: " +
                    $"RD${monto:N2} \n Nuevo saldo: RD${saldo:N2}");
            }
            return saldo;   
        }
        public static string MensajeError(string mensaje)
        {
            Console.WriteLine($"Error: {mensaje}");
            return mensaje;
        }
        public static string MensajeConsola(string imprimir)
        {
            Console.WriteLine($" {imprimir} ");
            return imprimir;
        }
   
        public static void Salidad(string nombre)
        {
            MensajeConsola("\n====================================");
            MensajeConsola($"Gracias por utilizar nuestro cajero, {nombre}. \n ¡Hasta pronto!");
            MensajeConsola("====================================");
        }
    }
}
