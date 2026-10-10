using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JulpajulparaisoPasteleria.Content.Logica
{
    public class SkinCliente
    {
        public Texture2D Caminando { get; private set; }
        public Texture2D Esperando { get; private set; }
        public Texture2D Enojado { get; private set; }
        public Texture2D Hablando { get; private set; }
        public ConfiguracionSkin ConfiguracionSkin { get; private set; }
        public SkinCliente(Texture2D caminando, Texture2D esperando,Texture2D enojado,Texture2D hablando, ConfiguracionSkin configuracion)
        {
            this.Caminando = caminando;
            this.Esperando = esperando;
            this.Enojado = enojado;
            this.Hablando = hablando;
            this.ConfiguracionSkin = configuracion;
        }
    }
}
