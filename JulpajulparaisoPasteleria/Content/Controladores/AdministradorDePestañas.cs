using JulpajulparaisoPasteleria.Content.Botones;
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

namespace JulpajulparaisoPasteleria.Content.Controladores
{
    public class AdministradorDePestañas
    {
        private BotonBase[] indicePestanias = new BotonBase[5];
        private Texture2D pestanias;

        public void LoadContent(ContentManager content)
        {
            int yBarra = Constante.ALTO_VIRTUAL - Constante.ALTO_BARRA;
            int anchoPestania = Constante.ANCHO_VIRTUAL / 5;

            for (int i = 0; i < indicePestanias.Length; i++)
            {
                Rectangle areaVirtual = new Rectangle(i * anchoPestania, yBarra, anchoPestania, Constante.ALTO_BARRA);
                indicePestanias[i] = new BotonBase(areaVirtual);
            }
            pestanias = content.Load<Texture2D>("imagenes/Fondos/Pestañas");
        }
        public Estacion Actualizar(Vector2 posVirtual, Estacion[] estaciones, Estacion estacionActual)
        {
            if (ManejoEntrada.ElementoClickeado())
            {
                int i = 0;
                bool encontrado = false;
                while (!encontrado && i < indicePestanias.Length)
                {
                    if (indicePestanias[i].FueClickeado(posVirtual, ManejoEntrada.ElementoClickeado()))
                    {
                        estacionActual = estaciones[i];
                        encontrado = true;
                    }
                    i++;
                }
            }
            return estacionActual;
        }
        public void Dibujar(SpriteBatch spriteBatch)
        {
            int yBarra = Constante.ALTO_VIRTUAL - Constante.ALTO_BARRA;
            spriteBatch.Draw(pestanias, new Rectangle(0, yBarra, Constante.ANCHO_VIRTUAL, Constante.ALTO_BARRA), Color.White);
        }
    }
}
