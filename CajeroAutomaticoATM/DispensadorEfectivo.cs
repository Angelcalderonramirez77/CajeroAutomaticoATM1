using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CajeroAutomaticoATM
{
    public class DispensadorEfectivo
    {
        public decimal EfectivoDisponible { get; private set; }

        public DispensadorEfectivo(decimal efectivoInicial)
        {
            if (efectivoInicial < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(efectivoInicial), "El efectivo inicial no puede ser negativo.");
            }

            EfectivoDisponible = efectivoInicial;
        }

        public bool DispensarEfectivo(decimal monto)
        {
            if (monto <= 0 || monto > EfectivoDisponible)
            {
                return false;
            }

            EfectivoDisponible -= monto;
            return true;
        }
    }
} 
