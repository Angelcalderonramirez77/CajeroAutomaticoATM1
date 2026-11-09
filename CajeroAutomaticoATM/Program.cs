using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using static CajeroAutomaticoATM.Program;

namespace CajeroAutomaticoATM
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string nombre = "Angel Calderon Ramirez";

            Console.WriteLine("================================");
            Console.WriteLine("            MINI ATM V2.1        ");
            Console.WriteLine("================================");
            // proceso Nombre

            List<Transaccion> historialMovimientos = new List<Transaccion>();            

            ATM.EjercutarOperacion(nombre, historialMovimientos);
        }
       public class ATM
        {
            public bool CuentaActiva { get; set; }
            public ATM(bool cuentaActiva)
            {
                CuentaActiva = cuentaActiva;
            }
            public  void MostrarMenu()
            {
                Pantalla.MensajeConsola(
                    "\n================================\n" +
                    " *******MENU PRINCIPAL**********" +
                    "\n1. Consultar Saldo" +
                    "\n2. Depositar" +
                    "\n3. Retirar" +
                    "\n4. Consultar movimientos" +
                    "\n5. Salir");
            }
            public static void EjercutarOperacion(string nombre, List<Transaccion> historialMovimientos)
            {
                Cuenta miCuenta = new Cuenta
                {
                    Activa = true,
                    Saldo = 10000m
                };

                bool cuentaActiva = miCuenta.EstadoCuenta();
                if (!cuentaActiva)
                {
                    Console.WriteLine("La cuenta está inactiva. No se puede realizar ninguna operación.");
                }
                else
                {
                    Console.WriteLine($"Numero de cuenta activo: {cuentaActiva}");
                    byte opciones;
                    ATM menu = new ATM(cuentaActiva);

                    do
                    {
                        menu.MostrarMenu();
                        opciones = Teclado.LeerOpcion();

                        switch (opciones)
                        {                            
                            case 1:

                                Cuenta.ConsultarSaldo(miCuenta.Saldo);
                                break;
                            case 2:
                                if (Teclado.LeerMonto("Ingrese el monto a depositar: ", out decimal deposito))
                                {
                                    miCuenta.Saldo = miCuenta.Depositar(miCuenta.Saldo,deposito, historialMovimientos);

                                }
                                break;

                            case 3:
                                Cuenta retirado = new Cuenta();

                                if (Teclado.LeerMonto("Ingrese el monto a retirar: ", out decimal retiro))
                                {
                                    miCuenta.Saldo = miCuenta.Retirar(miCuenta.Saldo, retiro, historialMovimientos);
                                }
                                break;
                            case 4:
                                Transaccion.MostrarMovimientos(historialMovimientos);

                                break;
                            case 5:
                                Pantalla.Salida(nombre);
                                break;

                            default:
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Opción incorrecta. Seleccione del 1 al 5.");
                                Console.ResetColor();
                                break;
                        }
                    } while (opciones != 5);

                    Console.ResetColor();
                }
            } 

        }

        // Clase para representar una cuenta bancaria
        public class Cuenta 
        {
            
           public int NumeroCuenta { get; set; }
           public decimal Saldo { get; set ; }
           public bool Activa { get; set; }

            // consurtal cuenta 
            public  bool EstadoCuenta()
            {
                return Activa;
            }
            public static void ConsultarSaldo(decimal Saldo)
            {
                Console.WriteLine($"Su saldo actual es: RD${Saldo:N2}");
            }
            public  bool LeerMonto(string mensaje, out decimal monto)
            {
                Console.WriteLine("\n{0}", mensaje);
                if (!decimal.TryParse(Console.ReadLine(), out monto) || monto <= 0m)
                {
                    Console.WriteLine("Ingrese un monto válido mayor a 0. ");
                    return false;
                }
                return true;
            }
            // deposito
            public  decimal Depositar(decimal saldo, decimal monto, List<Transaccion> historialMovimiento)
            {
                if (monto <= 0 || monto > 50000m)
                {
                    Pantalla.MensajeError(" Monto no permitido.");
                    return saldo;
                }
                saldo += monto;
                Console.ForegroundColor = ConsoleColor.Green;
                Pantalla.MensajeConsola($"\n¡Depósito exitoso!\nMonto depositado: RD${monto:N2}\nNuevo saldo: RD${saldo:N2}");

                // Agregar el movimiento al historial
                Transaccion.RegistrarMovimiento(historialMovimiento, monto);
                Console.ResetColor();
                return saldo;
            }
            // retirar 
            public  decimal Retirar(decimal saldo, decimal monto,List<Transaccion> historialMovimientos)
            {
                if (monto <= 0)
                {
                    Pantalla.MensajeError(" El retiro debe ser debe der mayor a 0.");
                    return saldo;
                }
                else if (monto > saldo)
                {
                    Pantalla.MensajeConsola($" Fondo insuficiente. \n Saldo disponible: RD${saldo:N2}");
                    return saldo;
                }
                else
                {
                    saldo -= monto;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Pantalla.MensajeConsola($"¡Retiro realizado exitosamente! \n Monto retirado: " +
                        $"RD${monto:N2} \n Nuevo saldo: RD${saldo:N2}");
                }
                Transaccion.RegistrarMovimiento(historialMovimientos, -monto);
                Console.ResetColor();
                return saldo;
            }
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
            public static void Salida(string nombre)
            {
                Pantalla.MensajeConsola("\n====================================");
                Pantalla.MensajeConsola($"Gracias por utilizar nuestro cajero, {nombre}. \n ¡Hasta pronto!");
                Pantalla.MensajeConsola("====================================");
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
            public DateTime Fecha { get; set; }

            // Constructor
            public Transaccion(string tipo, decimal monto)
            {
                Tipo = tipo;
                Monto = monto;
                Fecha = DateTime.Now;
            }

            // Registrar movimientos
            public static void RegistrarMovimiento(
                List<Transaccion> historialMovimientos,
                decimal monto)
            {
                string tipo = monto >= 0 ? "Deposito" : "Retiro";

                historialMovimientos.Add(
                    new Transaccion(tipo, Math.Abs(monto))
                );
            }

            // Metodo solicitado por el profesor
            public void MostrarDetalle()
            {
                if (Tipo == "Deposito")
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }

                Console.WriteLine(
                    $"[{Fecha:dd/MM/yyyy HH:mm}] " +
                    $"{Tipo}: RD${Monto:N2}"
                );

                Console.ResetColor();
            }

            // Mostrar historial completo
            public static void MostrarMovimientos(
                List<Transaccion> historialMovimientos)
            {
                Console.WriteLine("\nMOVIMIENTOS REGISTRADOS");
                Console.WriteLine("==============================");

                if (historialMovimientos.Count == 0)
                {
                    Console.WriteLine("No hay movimientos registrados.");
                    return;
                }

                foreach (Transaccion movimiento in historialMovimientos)
                {
                    movimiento.MostrarDetalle();
                }
            }
        }
    }
}