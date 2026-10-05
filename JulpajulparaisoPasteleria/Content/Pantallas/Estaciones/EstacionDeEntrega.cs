using JulpajulparaisoPasteleria.Content.Enumeradores;
using JulpajulparaisoPasteleria.Content.Gestores;
using JulpajulparaisoPasteleria.Content.Personajes;
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
    public class EstacionDeEntrega : Estacion
    {
        private GestorDeClientes gestorDeClientes;
        private Rectangle[] posicionesTortas;
        private Cliente clienteActual;
        public EstacionDeEntrega(GestorDeTickets gestorDeTickets, GestorDePedidos gestorDePedidos, RepositorioImagenes repositorioImagenes, GestorDeClientes gestorDeClientes) : base(gestorDeTickets, gestorDePedidos, repositorioImagenes)
        {
            this.gestorDeClientes = gestorDeClientes;
        }
        public override void LoadContent(ContentManager content)
        {
            Fondo = content.Load<Texture2D>("imagenes/Fondos/estacionDeEntrega");
        }
        public override void Actualizar(GameTime gameTime, Vector2 posicionVirtual)     
        {
            base.tortasEstacion = gestorDePedidos.getTortasEstacion(EstacionActual.EstacionDeEntrega);
            if (gestorDeClientes.clientesEsperandoEntrega.Count > 0)
            { 
                clienteActual = gestorDeClientes.clientesEsperandoEntrega[0];
            }
            if (tortasEstacion.Count > 0)
            {
                
            }
            base.gestorDeTickets.ActualizarTickets(posicionVirtual);
        }
        public override void ActualizarEnSegundoPlano(GameTime gameTime)
        {
        }
        public override void Dibujar(SpriteBatch spriteBatch)
        {
            base.Dibujar(spriteBatch);
            if (clienteActual != null)
            {
                clienteActual.Dibujar(spriteBatch, new Vector2(800, -300), 10f);
            }
            DibujarTickets(spriteBatch);
        }

    }
}
