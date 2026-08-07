using JulpajulparaisoPasteleria.Enumeradores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JulpajulparaisoPasteleria.Modelos
{
    internal class Torta
    {
        public Torta()
        {
            Bizcochuelos = new Bizcochuelo[2];
        }
        public Bizcochuelo[] Bizcochuelos { get; set; }
        public Relleno Relleno { get; set; }
        public Cobertura Cobertura { get; set; }
        public List<Topping> Decoracion { get; set; } = new List<Topping>();
        public FormaBizcochuelo FormaBizcochuelo { get; set; }

        public void AgregarTopping(Topping topping)
        {

                Decoracion.Add(topping);
        }
        public void AgregarBizcochuelo(Bizcochuelo bizcochuelo, int indicePiso)
        {
            if (indicePiso >= 0 && indicePiso < Bizcochuelos.Length)
            {
                Bizcochuelos[indicePiso] = bizcochuelo;
            }
        }

        public bool EstaCompleta()
        {
            return Bizcochuelos[0] != null && Relleno != null && Cobertura != null;
        }

        public void LimpiarTorta()
        {
            Bizcochuelos = new Bizcochuelo[2];
            Relleno = null;
            Cobertura = null;
            Decoracion.Clear();
        }
    }
}

