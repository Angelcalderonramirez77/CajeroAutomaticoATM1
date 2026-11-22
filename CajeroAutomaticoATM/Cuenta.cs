using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CajeroAutomaticoATM
{
    public class Cuenta
    {
        public int NumeroCuenta { get; set; }
        public decimal Saldo { get; set; }
        public bool Activa { get; set; }

        public Cuenta()
        {
        }

        public Cuenta(int numeroCuenta, decimal saldo, bool activa)
        {
            NumeroCuenta = numeroCuenta;
            Saldo = saldo;
            Activa = activa;
        }

        public bool EstadoCuenta()
        {
            return Activa;
        }
        public static void ConsultarSaldo(decimal Saldo)
        {
            Console.WriteLine($"Su saldo actual es: RD${Saldo:N2}");
        }

  

        // deposito
        public decimal Depositar(decimal saldo, decimal monto, List<Transaccion> historialMovimiento)
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
        public decimal Retirar(decimal saldo, decimal monto, List<Transaccion> historialMovimientos)
        {
            const decimal saldoMinimo = 10000m;

            if (monto <= 0)
            {
                Pantalla.MensajeError(" El retiro debe ser debe der mayor a 0.");
                return saldo;
            }
            else if (monto > saldo - saldoMinimo)
            {
                Pantalla.MensajeConsola($"No puede retirar ese monto. Debe conservar un saldo mínimo de RD${saldoMinimo:N2}.");
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
}
