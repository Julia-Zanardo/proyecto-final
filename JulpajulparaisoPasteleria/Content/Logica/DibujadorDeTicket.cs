using JulpajulparaisoPasteleria.Content.Controladores;
using JulpajulparaisoPasteleria.Content.Enumeradores;
using JulpajulparaisoPasteleria.Content.Gestores;
using JulpajulparaisoPasteleria.Content.Interfaces;
using JulpajulparaisoPasteleria.Content.Modelos;
using JulpajulparaisoPasteleria.Content.Utilidades;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JulpajulparaisoPasteleria.Content.Logica
{
    public class DibujadorDeTicket : ObjetoArrastrable
    {
        private int cantidadToppings;
        private SpriteFont numeroDePedido;
        private Texture2D imagenTicket;
        public Rectangle AreaTicketDefaut { get; private set; }
        private RepositorioImagenes r;
        public float Escala { get; private set; } = Constante.ESCALA_TICKET;
        private Pedido pedido;
        public DibujadorDeTicket(Vector2 posicionTicket, Pedido pedido, RepositorioImagenes r) : base(posicionTicket, new Rectangle((int)posicionTicket.X,(int)posicionTicket.Y, 500,500))
        {
            base.Posicion = posicionTicket;
            this.AreaTicketDefaut = new Rectangle((int)posicionTicket.X, (int)posicionTicket.Y, 500, 500);
            this.pedido = pedido;
            this.r = r;
        }
        public void LoadContent(ContentManager content)
        {
            imagenTicket = content.Load<Texture2D>("imagenes/EstacionOrdenes/ticket");
            r.LoadContent(content);
            numeroDePedido = content.Load<SpriteFont>("Fuentes/fuenteEscritura");
        }
        public void Actualizar(Vector2 posicionVirtual)
        {
            this.cantidadToppings = pedido.ToppingsDeseados.Count;
            base.Area = new Rectangle((int)base.Posicion.X, (int)base.Posicion.Y, (int)(imagenTicket.Width * Escala), (int)(imagenTicket.Height * Escala));
            base.ActualizarArrastre(posicionVirtual);
        }
        public void Dibujar( SpriteBatch spriteBatch)
        {
            string texto = "Pedido #" + pedido.Id;
            spriteBatch.Draw(imagenTicket, base.Posicion, null, Color.White, 0f, Vector2.Zero, Escala, SpriteEffects.None, 1);
            spriteBatch.DrawString( numeroDePedido, texto, base.Posicion + new Vector2(18 * Escala, 15 * Escala), Color.Black, 0f,Vector2.Zero, Escala * 0.7f,SpriteEffects.None,0.95f);
            spriteBatch.Draw(r.IconosBizcochuelo[pedido.SaborBizcochuelo], base.Posicion + new Vector2(20 * Escala, 135 * Escala),null,Color.White,0f,Vector2.Zero,Escala * 0.8f, SpriteEffects.None, 0.9f );
            spriteBatch.Draw(r.IconosRelleno[pedido.SaborRelleno], base.Posicion + new Vector2(7 * Escala, 110 * Escala), null, Color.White, 0f, Vector2.Zero, Escala * 0.8f, SpriteEffects.None, 0.9f);
            spriteBatch.Draw(r.IconosMoldes[pedido.FormaBizcochuelo], base.Posicion + new Vector2(8 * Escala, 163 * Escala), null, Color.White, 0f, Vector2.Zero, Escala*0.1f, SpriteEffects.None, 0.9f);
            spriteBatch.Draw(r.IconosCoberturas[pedido.Cobertura], base.Posicion + new Vector2(57 * Escala, 163 * Escala), null, Color.White, 0f, Vector2.Zero, Escala * 0.25f, SpriteEffects.None, 0.9f);
            int anchoTopping = (int)(24 * Escala);
            int altoTopping = (int)(24 * Escala);

            switch (cantidadToppings)
            {
                case 1:
                    spriteBatch.Draw(r.IconosToppings[pedido.ToppingsDeseados[0]], new Rectangle((int)(base.Posicion.X + 32 * Escala), (int)(base.Posicion.Y + 85 * Escala), anchoTopping, altoTopping), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.9f);
                    break;
                case 2:
                    spriteBatch.Draw(r.IconosToppings[pedido.ToppingsDeseados[0]], new Rectangle((int)(base.Posicion.X + 15 * Escala), (int)(base.Posicion.Y + 85 * Escala), anchoTopping, altoTopping), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.9f);
                    spriteBatch.Draw(r.IconosToppings[pedido.ToppingsDeseados[1]], new Rectangle((int)(base.Posicion.X + 47 * Escala), (int)(base.Posicion.Y + 85 * Escala), anchoTopping, altoTopping), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.9f);
                    break;
                case 3:
                    spriteBatch.Draw(r.IconosToppings[pedido.ToppingsDeseados[0]], new Rectangle((int)(base.Posicion.X + 8 * Escala), (int)(base.Posicion.Y + 85 * Escala), anchoTopping, altoTopping), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.9f);
                    spriteBatch.Draw(r.IconosToppings[pedido.ToppingsDeseados[1]], new Rectangle((int)(base.Posicion.X + 32 * Escala), (int)(base.Posicion.Y + 85 * Escala), anchoTopping, altoTopping), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.9f);
                    spriteBatch.Draw(r.IconosToppings[pedido.ToppingsDeseados[2]], new Rectangle((int)(base.Posicion.X + 56 * Escala), (int)(base.Posicion.Y + 85 * Escala), anchoTopping, altoTopping), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.9f);
                    break;
            }
        
        
        }
        public void AcomodarTicket(Rectangle posicion,float escala)
        {
            base.Posicion = new Vector2 (posicion.X, posicion.Y);
            this.Escala = escala;
        }

    }
}
