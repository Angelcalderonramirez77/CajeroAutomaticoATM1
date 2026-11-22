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
            List<Cuenta>  numeroCuentaActivo= new List<Cuenta>
            {
                  new Cuenta
                {
                    NumeroCuenta = 13245,
                    Saldo = 10000.00m,
                    Activa = true
                }
            };

            ATM.EjercutarOperacion(nombre, numeroCuentaActivo, historialMovimientos);
        }   
    }
}