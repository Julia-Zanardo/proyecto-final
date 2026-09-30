using JulpajulparaisoPasteleria.Content.Enumeradores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JulpajulparaisoPasteleria.Content.Objetos
{
    internal class Torta
    {
        public  SaborBizcochuelo SaborBizcochuelo { get; private set; }
        public SaborRelleno Relleno { get; private set; }
        public TipoCobertura Cobertura { get; private set; }
        public List<Topping> Decoracion { get; private set;     } = new List<Topping>();
        public FormaBizcochuelo FormaBizcochuelo { get; private set; }

        public void AgregarTopping(Topping topping)
        {
                Decoracion.Add(topping);
        }

        public void LimpiarTorta()
        {
            
        }
        public void AgregarSaborBizcochuelo(SaborBizcochuelo sabor)
        {
            if (SaborBizcochuelo == default) 
            {
                SaborBizcochuelo = sabor;
            }
        }
    }
}

