using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using JulpajulparaisoPasteleria.Enumeradores;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace JulpajulparaisoPasteleria.Content.Objetos
{
    public class Decoracion : Ingrediente
    {
        public Topping Tipo { get; private set; }
        public bool colocadoEnTorta { get; private set; } = false;
        private Rectangle zonaTorta;
        public Decoracion(Topping tipo, Texture2D textura, Vector2 posicionInical, Rectangle zonaTorta)
            : base(tipo.ToString(), textura, posicionInical)
        {
            Tipo = tipo;
            this.zonaTorta = zonaTorta;

        }
        public override void AlSoltar()
        {
            if (zonaTorta.Contains(ManejoEntrada.PosicionMouse))
            {
                colocadoEnTorta = true;
            }
            else
            {
                colocadoEnTorta = false;
                base.AlSoltar();
            }
        }
    }
}
