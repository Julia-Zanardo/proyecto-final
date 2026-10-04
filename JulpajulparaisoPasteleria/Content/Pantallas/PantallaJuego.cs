using JulpajulparaisoPasteleria.Content.Botones;
using JulpajulparaisoPasteleria.Content.Controladores;
using JulpajulparaisoPasteleria.Content.Gestores;
using JulpajulparaisoPasteleria.Content.Interfaces;
using JulpajulparaisoPasteleria.Content.Pantallas.Estaciones;
using JulpajulparaisoPasteleria.Content.Utilidades;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JulpajulparaisoPasteleria.Content.Pantallas
{
    public class PantallaJuego : IPantalla
    {
        private Estacion[] estaciones;
        private GestorDePestañas gestorDePestañas;
        private RepositorioImagenes repositorioImagenes = new RepositorioImagenes();
        private GestorDeTickets gestorTickets = new GestorDeTickets();
        private GestorDePedidos gestorDePedidos = new GestorDePedidos();
        private Estacion estacionActual;
        private BotonBase botonPausa;
        private GestorDePantalla gestorDePantalla;
        public PantallaJuego(GestorDePantalla gestorDePantalla)
        {
            gestorDePestañas = new GestorDePestañas();
            this.gestorDePantalla = gestorDePantalla;
        }
        public void LoadContent(ContentManager content)
        {
            if (estaciones == null)
            {
                gestorDePestañas.LoadContent(content);
                botonPausa = new BotonBase(content.Load<Texture2D>("imagenes/Botones/botonPausa"), new Rectangle(1800, 10, 100, 100));
                this.repositorioImagenes.LoadContent(content);
                estaciones = new Estacion[] {
                     new EstacionDeOrdenes(gestorTickets, gestorDePedidos, repositorioImagenes),
                     new EstacionDeMezcla(gestorTickets, gestorDePedidos, repositorioImagenes),
                     new EstacionDeHorneado(gestorTickets, gestorDePedidos, repositorioImagenes),
                     new EstacionDeDecoracion(gestorTickets, gestorDePedidos, repositorioImagenes),
                     new EstacionDeEntrega(gestorTickets, gestorDePedidos, repositorioImagenes)
                 };
                foreach (Estacion e in estaciones)
                {
                    e.LoadContent(content);
                }
                estacionActual = estaciones[0];
            }
            if (GestorDeAudio.musicaSonando)
            {
                GestorDeAudio.ReproducirMusicaFondo();
            }
        }
        public void Actualizar(GameTime gameTime, Vector2 posVirtual)
        {
            estacionActual = gestorDePestañas.Actualizar(posVirtual, estaciones, estacionActual);
            estacionActual.Actualizar(gameTime, posVirtual);
            foreach (Estacion e in estaciones)
            {
                if (e != estacionActual)
                {
                 e.ActualizarEnSegundoPlano(gameTime);
                }
            }
            if (botonPausa.FueClickeado(posVirtual, ManejoEntrada.ElementoClickeado()))
            {
                gestorDePantalla.CambiarPantalla(new PantallaPausa(gestorDePantalla, this));
            }
            botonPausa.Actualizar(posVirtual);
        }
        public void Dibujar(SpriteBatch dibujo)
        {
            estacionActual.Dibujar(dibujo);
            gestorDePestañas.Dibujar(dibujo);
            botonPausa.Dibujar(dibujo);
        }
    }
}
