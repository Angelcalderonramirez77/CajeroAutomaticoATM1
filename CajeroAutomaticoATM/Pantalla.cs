using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CajeroAutomaticoATM
{
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
}
