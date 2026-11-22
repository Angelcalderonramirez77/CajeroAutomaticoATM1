using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CajeroAutomaticoATM
{

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
