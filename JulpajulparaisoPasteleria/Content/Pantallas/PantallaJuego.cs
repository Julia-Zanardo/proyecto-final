using JulpajulparaisoPasteleria.Content.Botones;
using JulpajulparaisoPasteleria.Content.Controladores;
using JulpajulparaisoPasteleria.Content.Interfaces;
using JulpajulparaisoPasteleria.Content.Pantallas.Estaciones;
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
        private AdministradorDePestañas administradorDePestañas;
        private Estacion estacionActual;
        private BotonBase botonPausa;
        private GestorDePantalla gestorDePantalla;
        public PantallaJuego(GestorDePantalla gestorDePantalla)
        {
            administradorDePestañas = new AdministradorDePestañas();
            this.gestorDePantalla = gestorDePantalla;
        }
        public void LoadContent(ContentManager content)
        { 
            if (estaciones == null)
            {
                administradorDePestañas.LoadContent(content);
                botonPausa = new BotonBase(content.Load<Texture2D>("imagenes/Botones/botonPausa"), new Rectangle(1800, 10, 100, 100));
                estaciones = new Estacion[] {
                     new EstacionDeOrdenes(),
                     new EstacionDeMezcla(),
                     new EstacionDeHorneado(),
                     new EstacionDeDecoracion(),
                     new EstacionDeEntrega()
                 };
                foreach (Estacion e in estaciones)
                {
                    e.LoadContent(content);
                }
                estacionActual = estaciones[0];
            }
            GestorDeAudio.ReproducirMusicaFondo();
        }
        public void Actualizar(GameTime gameTime, Vector2 posVirtual)
        {
            estacionActual = administradorDePestañas.Actualizar(posVirtual, estaciones, estacionActual);
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
            administradorDePestañas.Dibujar(dibujo);
            botonPausa.Dibujar(dibujo);
        }
    }
}
