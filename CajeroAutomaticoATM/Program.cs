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
            decimal saldo = 1000m;
            string nombre = string.Empty;

            List<decimal> historialMovimientos = new List<decimal>();

            Console.WriteLine("================================");
            Console.WriteLine("            MINI ATM V2          ");
            Console.WriteLine("================================");
            // proceso Nombre
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
                    opciones = LeerOpcion();

                    switch (opciones)
                    {
                        case 1:
                            ConsultarSaldo(saldo);

                            break;

                        case 2:
                            if(LeerMonto("Ingrese el monto a depositar: ", out decimal deposito))
                            {
                                saldo = Depositar(saldo, deposito, historialMovimientos);
                            }
                           
                            break;
                        case 3:
                            if (LeerMonto( "Ingrese el monto a retirar: ", out decimal retiro))
                            {
                                saldo = Retirar(saldo, retiro, historialMovimientos);
                                break;
                            }
                           break;
                        case 4:
                            MostrarMovimientos(historialMovimientos);

                           break;
                        case 5:
                            Salidad(nombre);
                            break;

                        default:
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Opción incorrecta. Seleccione del 1 al 4.");
                            break;
                    }
                } while (opciones != 5);

                Console.ResetColor();
            }
        }
         // === Futuras Mejoras ===//

        /*
        // Clase para representar una cuenta bancaria
        public class Cuenta {
            
           public int NumeroCuenta { get; set; }
           public decimal Saldo { get; set; }
           public bool Activa { get; set; }

        }
        // Clase para mostrar mensajes en la pantalla
        public class Pantalla
        {
            public string MostrarMensaje(string mensaje)
            {
                Console.WriteLine(mensaje);
                return mensaje;
            }

            public string MostrarSaldo(decimal saldo)
            {
                Console.WriteLine($"Su saldo actual es: RD${saldo:N2}");
                return $"Su saldo actual es: RD${saldo:N2}";
            }
        }
         // CLASE entrada de datos 
         
        public class Teclado
        {
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

        }
         
        public class DispensadorEfectivo
        {
            public decimal EfectivoDisponible { get; set; }

            public void DisepensarEfectivo()
            {
                // Lógica para dispensar efectivo
            }
        }
     
        public class Transaccion
        {
            public string Tipo { get; set; }
            public decimal Monto { get; set; }
            public DateTime fecha { get; set; }
         }
        */

        // VERSION 1  //

        // Metodo para verificar el estado de la cuenta
        public static bool EstadoCuenta(bool estado)
        {
            return estado;
        }
        // Metodo para mostrar el menu de opciones
        public static void MostrarMenu()
        {
            MensajeConsola("\n================================\n *******MENU PRINCIPAL**********" +
                "\n1.Consultar Saldo\n2. Depositar\n3. Retirar\n4. Consultar movimientos\n5. Salir");
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
        public static void ConsultarSaldo(decimal saldo)
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
        public static decimal Depositar(decimal saldo, decimal monto, List<decimal> historialMovimientos)
        {
            if (monto <= 0)
            {
                MensajeError(" El monto a depositar debe ser mayor a 0.");
                return saldo;
            }
            else if (monto > 50000m)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                MensajeError("Monto depositado fuera de lo normal");
                MensajeConsola("Monto Validado son RD$ 50,000");
                Console.ResetColor();
                return saldo;
            }
            saldo += monto;
            Console.ForegroundColor = ConsoleColor.Green;
            MensajeConsola($"\n¡Depósito exitoso!\nMonto depositado: RD${monto:N2}\nNuevo saldo: RD${saldo:N2}");

            // Agregar el movimiento al historial
            RegistrarMovimiento(historialMovimientos, monto);
            Console.ResetColor();
            return saldo;
        }
        public static decimal Retirar(decimal saldo, decimal monto, List<decimal> historialMovimientos)
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
                Console.ForegroundColor = ConsoleColor.Red;
                MensajeConsola($"¡Retiro realizado exitosamente! \n Monto retirado: " +
                    $"RD${monto:N2} \n Nuevo saldo: RD${saldo:N2}");
            }
            RegistrarMovimiento(historialMovimientos, -monto);
            Console.ResetColor();
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
        // Version 2 Agregando Listado A la Nueva Version //

        public static void RegistrarMovimiento(List<decimal> historialMovimientos, decimal monto)
        {   
            historialMovimientos.Add(monto);

        }

        public static void MostrarMovimientos(List<decimal> historialMovimientos)
        {
            Console.WriteLine("\n Movimientos registrados");
            Console.WriteLine("==============================");
            foreach (var movimiento in historialMovimientos)
            {
                if (movimiento > 0)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($" Retiro: RD${(movimiento):N2}");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($" Deposito: RD${(movimiento):N2}");
                   

                }
                Console.ResetColor();

            }


        }

    }
}
