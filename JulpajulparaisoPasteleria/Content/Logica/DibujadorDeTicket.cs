using JulpajulparaisoPasteleria.Enumeradores;
using JulpajulparaisoPasteleria.Modelos;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JulpajulparaisoPasteleria.Content.Logica
{
    public class DibujadorDeTicket
    {
        private Dictionary<Enum, Texture2D> iconos;
        private Texture2D imagenTicket;

        public void loadContent(ContentManager content)
        {
            imagenTicket = content.Load<Texture2D>("imagenes/estacionDeOrdenes/ticket");
            iconos = new Dictionary<Enum, Texture2D>();
            iconos.Add(SaborBizcochuelo.CarameloVainilla, content.Load<Texture2D>("imagenes/Iconos/carameloVainilla"));
        }
        public void DibujarTicket(Pedido pedido)
        {
            
        }
    }
}
