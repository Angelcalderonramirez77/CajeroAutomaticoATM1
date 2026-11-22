using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CajeroAutomaticoATM
{

    public class ATM
    {

        //bool activa = false;
        public void MostrarMenu()
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
        public static void EjercutarOperacion(string nombre,List<Cuenta> cuentas, List<Transaccion> historialMovimientos)
        {
            int numeroIngresado;
            Cuenta miCuenta;

            while (true)
            {
                Console.Write("Ingrese su número de cuenta: ");

                if (!int.TryParse(Console.ReadLine(), out numeroIngresado))
                {
                    Console.WriteLine("Número inválido. Intente nuevamente.");
                    continue;
                }

                miCuenta = cuentas.Find(c =>
                    c.NumeroCuenta == numeroIngresado && c.Activa);

                if (miCuenta != null)
                {
                    break;
                }

                Console.WriteLine("La cuenta no existe o está inactiva. Intente nuevamente.");
            }

            if (!miCuenta.Activa)
            {
                Console.WriteLine("La cuenta está inactiva. No se puede realizar ninguna operación.");
            }
            else
            {
                Console.WriteLine($"Numero de cuenta activo: {miCuenta.NumeroCuenta}");
                byte opciones;
                ATM menu = new ATM();
                DispensadorEfectivo dispensador = new DispensadorEfectivo(50000m);

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
                                miCuenta.Saldo = miCuenta.Depositar(miCuenta.Saldo, deposito, historialMovimientos);

                            }
                            break;

                        case 3:
                            if (Teclado.LeerMonto("Ingrese el monto a retirar: ", out decimal retiro))
                            {
                                const decimal saldoMinimo = 10000m;

                                if (retiro > miCuenta.Saldo - saldoMinimo)
                                {
                                    Pantalla.MensajeConsola($"No puede retirar ese monto. Debe conservar un saldo mínimo de RD${saldoMinimo:N2}.");
                                }
                                else if (retiro > dispensador.EfectivoDisponible)
                                {
                                    Pantalla.MensajeConsola($"El cajero no tiene suficiente efectivo. Disponible: RD${dispensador.EfectivoDisponible:N2}");
                                }
                                else
                                {
                                    miCuenta.Saldo = miCuenta.Retirar(miCuenta.Saldo, retiro, historialMovimientos);
                                    dispensador.DispensarEfectivo(retiro);
                                }
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
}
