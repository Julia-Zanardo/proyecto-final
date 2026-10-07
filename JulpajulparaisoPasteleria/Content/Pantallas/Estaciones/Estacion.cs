using JulpajulparaisoPasteleria.Content.Controladores;
using JulpajulparaisoPasteleria.Content.Gestores;
using JulpajulparaisoPasteleria.Content.Interfaces;
using JulpajulparaisoPasteleria.Content.Logica;
using JulpajulparaisoPasteleria.Content.Objetos;
using JulpajulparaisoPasteleria.Content.Utilidades;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JulpajulparaisoPasteleria.Content.Pantallas.Estaciones
{
    public abstract class Estacion : IPantalla
    {
        protected Texture2D Fondo;
        public bool EsActiva { set; get; }
        public abstract void LoadContent(ContentManager content);
        public abstract void Actualizar(GameTime gameTime, Vector2 posicionVirtual);
        protected GestorDeTickets gestorDeTickets;
        protected GestorDePedidos gestorDePedidos;
        protected RepositorioImagenes r;
        protected List<Torta> tortasEstacion = new List<Torta>();
        public Estacion(GestorDeTickets gestorDeTickets, GestorDePedidos gestorDePedidos, RepositorioImagenes repositorioImagenes)
        {
            this.gestorDeTickets = gestorDeTickets;
            this.gestorDePedidos = gestorDePedidos;
            this.r = repositorioImagenes;
        }
        public virtual void ActualizarEnSegundoPlano(GameTime gameTime)
        {
        }
        public virtual void Dibujar(SpriteBatch spriteBatch)
        {
            Rectangle destino = new Rectangle(0, 0, Constante.ANCHO_VIRTUAL, Constante.ALTO_VIRTUAL);
            spriteBatch.Draw(Fondo, destino, Color.White);
        }
        public void DibujarTickets(SpriteBatch spriteBatch) 
        {
            foreach (Ticket ticket in gestorDeTickets.TicketsActuales)
            {
                ticket.Dibujar(spriteBatch);
            }

        }

    }
}
