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
    public class DibujadorDeTicket
    {
        private Dictionary<SaborBizcochuelo, Texture2D> iconosBizcochuelo;
        private Dictionary<SaborRelleno, Texture2D> iconosRelleno;
        private Dictionary<FormaBizcochuelo, Texture2D> iconosMoldes;
        private Dictionary<TipoCobertura, Texture2D> iconosCoberturas;
        private Dictionary<Topping, Texture2D> iconosToppings;
        private int cantidadToppings;
        private SpriteFont numeroDePedido;
        private Texture2D imagenTicket;
        private Vector2 posicionTicket;
        public Rectangle AreaTicket { get; private set; }
        public Rectangle AreaTicketDefaut { get; private set; }
        public bool EstaSiendoArrastrado { get; private set; }
        private Vector2 desplazamiento;
        public float Escala { get; private set; } = Constante.ESCALA_TICKET;
        private Pedido pedido;
        public DibujadorDeTicket(Vector2 posicionTicket, Pedido pedido)
        {
            this.posicionTicket = posicionTicket;
            this.AreaTicketDefaut = new Rectangle((int)posicionTicket.X, (int)posicionTicket.Y, 500, 500);
            this.pedido = pedido;
        }
        public void LoadContent(ContentManager content)
        {
            imagenTicket = content.Load<Texture2D>("imagenes/EstacionOrdenes/ticket");
            iconosBizcochuelo = new Dictionary<SaborBizcochuelo, Texture2D>();
            iconosBizcochuelo.Add(SaborBizcochuelo.CarameloVainilla, content.Load<Texture2D>("imagenes/SaboresBizcochuelo/botonCarameloVainilla"));
            iconosBizcochuelo.Add(SaborBizcochuelo.Chocolate, content.Load<Texture2D>("imagenes/SaboresBizcochuelo/botonChocolate"));
            iconosBizcochuelo.Add(SaborBizcochuelo.Arandano, content.Load<Texture2D>("imagenes/SaboresBizcochuelo/botonArandano"));
            iconosBizcochuelo.Add(SaborBizcochuelo.Frutilla, content.Load<Texture2D>("imagenes/SaboresBizcochuelo/botonFrutilla"));
            iconosRelleno = new Dictionary<SaborRelleno, Texture2D>();
            iconosRelleno.Add(SaborRelleno.Chantilly, content.Load<Texture2D>("imagenes/Rellenos/rellenoChantilly"));
            iconosRelleno.Add(SaborRelleno.Chocolate, content.Load<Texture2D>("imagenes/Rellenos/rellenoChocolate"));
            iconosRelleno.Add(SaborRelleno.Frutilla, content.Load<Texture2D>("imagenes/Rellenos/rellenoFrutilla"));
            iconosRelleno.Add(SaborRelleno.Limon, content.Load<Texture2D>("imagenes/Rellenos/rellenoLimon"));
            iconosMoldes = new Dictionary<FormaBizcochuelo, Texture2D>();
            iconosMoldes.Add(FormaBizcochuelo.Redondo, content.Load<Texture2D>("imagenes/Moldes/moldeRedondo"));
            iconosMoldes.Add(FormaBizcochuelo.Cuadrado, content.Load<Texture2D>("imagenes/Moldes/moldeCuadrado"));
            iconosMoldes.Add(FormaBizcochuelo.Corazon, content.Load<Texture2D>("imagenes/Moldes/moldeCorazon"));
            iconosCoberturas = new Dictionary<TipoCobertura, Texture2D>();
            iconosCoberturas.Add(TipoCobertura.GlaseadoDeBanana, content.Load<Texture2D>("imagenes/Coberturas/glaseadoBanana"));
            iconosCoberturas.Add(TipoCobertura.GlaseadoDeFrutilla, content.Load<Texture2D>("imagenes/Coberturas/glaseadoFrutilla"));
            iconosCoberturas.Add(TipoCobertura.GlaseadoDePistacho, content.Load<Texture2D>("imagenes/Coberturas/glaseadoPistacho"));
            iconosCoberturas.Add(TipoCobertura.GlaseadoDeVainilla, content.Load<Texture2D>("imagenes/Coberturas/glaseadoVainilla"));
            iconosCoberturas.Add(TipoCobertura.GanacheChocolate, content.Load<Texture2D>("imagenes/Coberturas/ganacheChocolate"));
            iconosCoberturas.Add(TipoCobertura.AlgodonDeAzucar, content.Load<Texture2D>("imagenes/Coberturas/algodonAzucar"));
            iconosCoberturas.Add(TipoCobertura.Napolitano, content.Load<Texture2D>("imagenes/Coberturas/napolitana"));
            iconosToppings = new Dictionary<Topping, Texture2D>();
            iconosToppings.Add(Topping.Banana, content.Load<Texture2D>("imagenes/Topings/topingBanana"));
            iconosToppings.Add(Topping.Cereza, content.Load<Texture2D>("imagenes/Topings/topingCereza"));
            iconosToppings.Add(Topping.Oreo, content.Load<Texture2D>("imagenes/Topings/topingOreo"));
            iconosToppings.Add(Topping.Cubanito, content.Load<Texture2D>("imagenes/Topings/topingCubanito"));
            iconosToppings.Add(Topping.Waffle, content.Load<Texture2D>("imagenes/Topings/topingWaffle"));
            numeroDePedido = content.Load<SpriteFont>("Fuentes/fuentePedido");
        }
        public void Actualizar(Vector2 posicionVirtual)
        {
            this.cantidadToppings = pedido.ToppingsDeseados.Count;
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
            string texto = "Pedido #" + pedido.Id;
            spriteBatch.Draw(imagenTicket, posicionTicket, null, Color.White, 0f, Vector2.Zero, Escala, SpriteEffects.None, 1);
            spriteBatch.DrawString( numeroDePedido, texto, posicionTicket + new Vector2(18 * Escala, 15 * Escala), Color.Black, 0f,Vector2.Zero, Escala * 0.7f,SpriteEffects.None,0.95f);
            spriteBatch.Draw(iconosBizcochuelo[pedido.SaborBizcochuelo], posicionTicket + new Vector2(20 * Escala, 135 * Escala),null,Color.White,0f,Vector2.Zero,Escala * 0.8f, SpriteEffects.None, 0.9f );
            spriteBatch.Draw(iconosRelleno[pedido.SaborRelleno], posicionTicket + new Vector2(7 * Escala, 110 * Escala), null, Color.White, 0f, Vector2.Zero, Escala * 0.8f, SpriteEffects.None, 0.9f);
            spriteBatch.Draw(iconosMoldes[pedido.FormaBizcochuelo], posicionTicket + new Vector2(8 * Escala, 163 * Escala), null, Color.White, 0f, Vector2.Zero, Escala*0.1f, SpriteEffects.None, 0.9f);
            spriteBatch.Draw(iconosCoberturas[pedido.Cobertura], posicionTicket + new Vector2(57 * Escala, 163 * Escala), null, Color.White, 0f, Vector2.Zero, Escala * 0.25f, SpriteEffects.None, 0.9f);
            int anchoTopping = (int)(24 * Escala);
            int altoTopping = (int)(24 * Escala);

            switch (cantidadToppings)
            {
                case 1:
                    spriteBatch.Draw(iconosToppings[pedido.ToppingsDeseados[0]], new Rectangle((int)(posicionTicket.X + 32 * Escala), (int)(posicionTicket.Y + 85 * Escala), anchoTopping, altoTopping), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.9f);
                    break;
                case 2:
                    spriteBatch.Draw(iconosToppings[pedido.ToppingsDeseados[0]], new Rectangle((int)(posicionTicket.X + 15 * Escala), (int)(posicionTicket.Y + 85 * Escala), anchoTopping, altoTopping), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.9f);
                    spriteBatch.Draw(iconosToppings[pedido.ToppingsDeseados[1]], new Rectangle((int)(posicionTicket.X + 47 * Escala), (int)(posicionTicket.Y + 85 * Escala), anchoTopping, altoTopping), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.9f);
                    break;
                case 3:
                    spriteBatch.Draw(iconosToppings[pedido.ToppingsDeseados[0]], new Rectangle((int)(posicionTicket.X + 8 * Escala), (int)(posicionTicket.Y + 85 * Escala), anchoTopping, altoTopping), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.9f);
                    spriteBatch.Draw(iconosToppings[pedido.ToppingsDeseados[1]], new Rectangle((int)(posicionTicket.X + 32 * Escala), (int)(posicionTicket.Y + 85 * Escala), anchoTopping, altoTopping), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.9f);
                    spriteBatch.Draw(iconosToppings[pedido.ToppingsDeseados[2]], new Rectangle((int)(posicionTicket.X + 56 * Escala), (int)(posicionTicket.Y + 85 * Escala), anchoTopping, altoTopping), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.9f);
                    break;
            }
        
        
        }
        public void AcomodarTicket(Rectangle posicion,float escala)
        {
            posicionTicket = new Vector2 (posicion.X, posicion.Y);
            this.Escala = escala;
        }

    }
}
