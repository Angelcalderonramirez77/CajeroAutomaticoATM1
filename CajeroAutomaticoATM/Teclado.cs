using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CajeroAutomaticoATM
{
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
}
