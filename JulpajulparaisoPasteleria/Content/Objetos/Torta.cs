using JulpajulparaisoPasteleria.Content.Controladores;
using JulpajulparaisoPasteleria.Content.Enumeradores;
using JulpajulparaisoPasteleria.Content.Interfaces;
using JulpajulparaisoPasteleria.Content.Utilidades;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace JulpajulparaisoPasteleria.Content.Objetos
{
    public class Torta
    {
        public Rectangle Area { get; private set; }
        private Vector2 posicionTorta;
        private Vector2 desplazamiento;
        public SaborBizcochuelo SaborBizcochuelo { get; set; }
        public EstadoCoccion EstadoCoccion { get; set; } = EstadoCoccion.Cruda;
        public SaborRelleno Relleno { get; set; }
        public TipoCobertura Cobertura { get; set; }
        public List<Topping> Decoracion { get; set; } = new List<Topping>();
        public FormaBizcochuelo FormaBizcochuelo { get; set; }
        public float tiempoDeHorneadoNecesario { get; private set; } = 10f;
        public float tiempoDeHorneadoQuemado { get; private set; } = 20f;
        public float tiempoDeHorneadoActual { get; set; }
        public EstacionActual EstacionActual { get; set; } = EstacionActual.EstacionDeOrdenes;
        public bool estaSiendoArrastrada { get; private set; } = false;
        public void AgregarTopping(Topping topping)
        {
            Decoracion.Add(topping);
        }

        public void LimpiarTorta()
        {

        }
        public void AgregarSaborBizcochuelo(SaborBizcochuelo sabor)
        {
            if (SaborBizcochuelo == default)
            {
                SaborBizcochuelo = sabor;
            }
        }
        public void Actualizar(Vector2 posicionVirtual)
        {
            Area = new Rectangle((int)posicionTorta.X, (int)posicionTorta.Y, Constante.MEDIDA_TORTA_X, Constante.MEDIDA_TORTA_Y);

           
            if (ManejoEntrada.ElementoPresionado() && Area.Contains(posicionVirtual))
            {
                estaSiendoArrastrada = true;
                desplazamiento = posicionTorta - posicionVirtual; 
            }

            if (estaSiendoArrastrada)
            {
                posicionTorta = posicionVirtual + desplazamiento; 

                if (ManejoEntrada.ElementoSoltado())
                {
                    estaSiendoArrastrada = false;
                }
            }
        }
        public void Dibujar(SpriteBatch spriteBatch, Rectangle area, Texture2D textura)
        {
            spriteBatch.Draw(textura, area, Color.White);
        }
        public void cambiarArea(Rectangle nuevaArea)
        {
            Area = nuevaArea;
            posicionTorta = new Vector2(nuevaArea.X, nuevaArea.Y);
        }
    }
}

