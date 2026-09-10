using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using JulpajulparaisoPasteleria.Content.Enumeradores;
using JulpajulparaisoPasteleria.Content.Controladores;

namespace JulpajulparaisoPasteleria.Content.Logica
{
    public class DibujadorDeTicket
    {
        private Dictionary<SaborBizcochuelo, Texture2D> iconos;
        private Texture2D imagenTicket;
        private Vector2 posicionTicket = new Vector2(1450, 00);
        public Rectangle AreaTicket { get; private set; }
        public Rectangle AreaTicketDefaut { get; private set; }
        public bool EstaSiendoArrastrado { get; private set; }
        private Vector2 desplazamiento;
        public float Escala { get; private set; } = 5;
        public DibujadorDeTicket()
        {
            this.AreaTicketDefaut = new Rectangle(1450, 0, 500, 500);
        }
        public void LoadContent(ContentManager content)
        {
            imagenTicket = content.Load<Texture2D>("imagenes/EstacionOrdenes/ticket");
            iconos = new Dictionary<SaborBizcochuelo, Texture2D>();
            //iconos.Add(SaborBizcochuelo.CarameloVainilla, content.Load<Texture2D>("imagenes/Iconos/carameloVainilla"));

        }
        public void Actualizar(Vector2 posicionVirtual)
        {
            AreaTicket = new Rectangle((int)posicionTicket.X, (int)posicionTicket.Y, (int)(imagenTicket.Width * Escala), (int)(imagenTicket.Height * Escala));
            if (ManejoEntrada.ElementoPresionado() && AreaTicket.Contains(posicionVirtual))
            {
                EstaSiendoArrastrado = true;
                desplazamiento = posicionTicket - posicionVirtual;
            }

            if (EstaSiendoArrastrado)
            {
                posicionTicket = posicionVirtual + desplazamiento;
                if (ManejoEntrada.ElementoSoltado())
                {
                    EstaSiendoArrastrado = false;
                }
            }
        }
        public void Dibujar( SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(imagenTicket, posicionTicket, null, Color.White, 0f, Vector2.Zero, Escala, SpriteEffects.None, 1);
        }
        public void acomodarTicket(Rectangle posicion,float escala)
        {
            posicionTicket = new Vector2 (posicion.X, posicion.Y);
            this.Escala = escala;
        }
    }
}
