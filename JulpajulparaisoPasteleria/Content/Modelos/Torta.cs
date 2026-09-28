using JulpajulparaisoPasteleria.Content.Enumeradores;
using JulpajulparaisoPasteleria.Content.Objetos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JulpajulparaisoPasteleria.Content.Modelos
{
    internal class Torta
    {
        public Torta()
        {
            Bizcochuelos = new Bizcochuelo[2];
        }
        public Bizcochuelo[] Bizcochuelos { get; private set; }
        public SaborRelleno Relleno { get; private set; }
        public TipoCobertura Cobertura { get; private set; }
        public List<Topping> Decoracion { get; private set;     } = new List<Topping>();
        public FormaBizcochuelo FormaBizcochuelo { get; private set; }

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

        public void LimpiarTorta()
        {
            
        }
    }
}

