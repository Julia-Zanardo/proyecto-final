using JulpajulparaisoPasteleria.Content.Controladores;
using JulpajulparaisoPasteleria.Content.Logica;
using JulpajulparaisoPasteleria.Content.Personajes;
using JulpajulparaisoPasteleria.Modelos;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JulpajulparaisoPasteleria.Content.Estaciones
{
    public class EstacionDeOrdenes : Estacion
    {
        private List<SkinCliente> texturasClientes;
        private Texture2D chicaCaminando;
        private Texture2D chicaEsperando;
        private GestorDeJuego gestor;
        private GeneradorDeClientes generadorClientes;
        private int cantidadClientesMaxima;
        private List<Cliente> clientesActivos = new List<Cliente>();
        private Vector2[] posicionesCola = new Vector2[]
        {
        new Vector2(100, 200), 
        new Vector2(300, 200), 
        new Vector2(500, 200)  
        };
        public EstacionDeOrdenes(GestorDeJuego gestor)
        {
            this.gestor = gestor;
        }
        public override void LoadContent(ContentManager content)
        {
            Fondo = content.Load<Texture2D>("imagenes/fondos/estacionOrdenes");
            cantidadClientesMaxima = gestor.CantidadDeClientesPorDia;
            int cantidadPersonajes = Constante.CANTIDAD_PERSONAJES;
            texturasClientes = CargadorDeSkins.CargarSkinsClientes(content, cantidadPersonajes);
            generadorClientes = new GeneradorDeClientes(gestor, texturasClientes);
        }
        public override void Update(GameTime gameTime, Vector2 posicionVirtual)
        {
            if(generadorClientes.Actualizar(gameTime) && clientesActivos.Count < cantidadClientesMaxima)
            {
                clientesActivos.Add(generadorClientes.GenerarCliente(posicionesCola[clientesActivos.Count]));
            }
            foreach (Cliente cliente in clientesActivos)
            {
                cliente.Actualizar(gameTime);
            }
            if (clientesActivos.Count >0 ) 
            {
            }

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            foreach (Cliente cliente in clientesActivos)
            {
                cliente.Dibujar(spriteBatch);
            }
        }

    }
}
