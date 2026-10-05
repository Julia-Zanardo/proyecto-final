using JulpajulparaisoPasteleria.Content.Controladores;
using JulpajulparaisoPasteleria.Content.Enumeradores;
using JulpajulparaisoPasteleria.Content.Gestores;
using JulpajulparaisoPasteleria.Content.Logica;
using JulpajulparaisoPasteleria.Content.Objetos;
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
        private Rectangle areaTicket = new Rectangle(400, 400, 30, 50);
        private Rectangle areaBasura = new Rectangle(200, 600, 100, 100);
        private Rectangle areaTortaEntregar = new Rectangle(800, 750, 100, 100);
        public EstacionDeEntrega(GestorDeTickets gestorDeTickets, GestorDePedidos gestorDePedidos, RepositorioImagenes repositorioImagenes, GestorDeClientes gestorDeClientes) : base(gestorDeTickets, gestorDePedidos, repositorioImagenes)
        {
            this.gestorDeClientes = gestorDeClientes;
            posicionesTortas = new Rectangle[] { 
                new Rectangle(100,420,Constante.MEDIDA_TORTA_X,Constante.MEDIDA_TORTA_Y/2),
                new Rectangle(400,420,Constante.MEDIDA_TORTA_X,Constante.MEDIDA_TORTA_Y/2) };
        }
        public override void LoadContent(ContentManager content)
        {
            Fondo = content.Load<Texture2D>("imagenes/Fondos/estacionDeEntrega");
        }
        public override void Actualizar(GameTime gameTime, Vector2 posicionVirtual)     
        {
            base.tortasEstacion = gestorDePedidos.getTortasEstacion(EstacionActual.EstacionDeEntrega);
            for (int i = 0; i < tortasEstacion.Count; i++)
            {
                Torta torta = tortasEstacion[i];
                if (!torta.EstaSiendoArrastrado && i < posicionesTortas.Length)
                {
                    Rectangle area = new Rectangle((int)posicionesTortas[i].X, (int)posicionesTortas[i].Y, 300, 200);
                    torta.cambiarArea(area);
                }
                torta.Actualizar(posicionVirtual);
                if (torta.EstaSiendoArrastrado)
                {
                    if (DetectorDeColisiones.DetectarColision(torta.Area, areaTortaEntregar))
                    {
                        //aca la torta lista se elimina de tortas estacion y pasa a una clase que calcula la puntuacion y la muestra en pantalla
                        //pero solo si el ticket tambien fue entregado
                        torta.cambiarArea(areaTortaEntregar);
                        tortasEstacion.Remove(torta);
                    }
                }
                
            }
            DibujadorDeTicket ticketPuesto = gestorDeTickets.verificarColision(areaTicket);
            if (ticketPuesto != null)
            {
                ticketPuesto.Area = areaTicket;
            }
            base.gestorDeTickets.ActualizarTickets(posicionVirtual);
        }
        public override void ActualizarEnSegundoPlano(GameTime gameTime)
        {
        }
        public override void Dibujar(SpriteBatch spriteBatch)
        {
            base.Dibujar(spriteBatch);
            if (tortasEstacion.Count > 0)
            {
                for (int i = 0; i < tortasEstacion.Count; i++)
                {
                    if (i < posicionesTortas.Length) 
                    {
                        tortasEstacion[i].Dibujar(spriteBatch, tortasEstacion[i].Area, r.TexturasBizcochueloConCobertura[(tortasEstacion[i].FormaBizcochuelo, tortasEstacion[i].Cobertura)]);
                    }
                }
            }
            if (clienteActual != null)
            {
                clienteActual.Dibujar(spriteBatch, new Vector2(800, -300), 10f);
            }
            spriteBatch.Draw(r.TexturaPosicionarTicket, areaTicket, Color.White);
            spriteBatch.Draw(r.TexturaBasura, areaBasura, Color.White);
            DibujarTickets(spriteBatch);
        }

    }
}
