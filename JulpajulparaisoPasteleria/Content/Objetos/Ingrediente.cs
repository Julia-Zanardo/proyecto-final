using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using System.Text;
using System.Threading.Tasks;
namespace JulpajulparaisoPasteleria.Content.Objetos
{
    public class Ingrediente
    {
        public string Nombre { get; private set; }
        public Texture2D Texture { get; private set; }
        public Vector2 Posicion { get; private set; }
        public Vector2 PosicionInicial { get; private set; }

        public Rectangle limites
        {
            get { return new Rectangle((int)Posicion.X, (int)Posicion.Y, Texture.Width, Texture.Height); }
        }
        protected bool estaSiendoArrastrado;
        private Vector2 desplazamientoMouse;

        public Ingrediente(string nombre, Texture2D texture, Vector2 posicion)
        {
            Nombre = nombre;
            Texture = texture;
            Posicion = posicion;
            PosicionInicial = posicion;
            estaSiendoArrastrado = false;
        }

        public virtual void Actualizar()
        {
            Vector2 posMouse = ManejoEntrada.PosicionMouse;
            if (ManejoEntrada.elementoClickeado() && limites.Contains(posMouse.ToPoint()))  
            {
                estaSiendoArrastrado = true;
                desplazamientoMouse = posMouse - Posicion;
            }
            if (estaSiendoArrastrado && ManejoEntrada.elementoPresionado())
            {
                Posicion = posMouse + desplazamientoMouse;
            }
            if (ManejoEntrada.elementoSoltado() && estaSiendoArrastrado)
            {
                estaSiendoArrastrado = false;
                AlSoltar();
            }
        }
        public virtual void AlSoltar()
        {
            Posicion = PosicionInicial;
        }
        public virtual void Dibujar(SpriteBatch dibujador)
        {
            dibujador.Draw(Texture, Posicion, Color.White);
        }
    }
}
