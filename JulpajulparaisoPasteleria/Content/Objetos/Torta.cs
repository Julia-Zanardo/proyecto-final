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
    public class Torta : ObjetoArrastrable
    {
        public SaborBizcochuelo SaborBizcochuelo { get; set; }
        public EstadoCoccion EstadoCoccion { get; set; } = EstadoCoccion.Crudo;
        public SaborRelleno Relleno { get; set; }
        public TipoCobertura Cobertura { get; set; }
        public List<TipoTopping> Decoracion { get; set; } = new List<TipoTopping>();
        public FormaBizcochuelo FormaBizcochuelo { get; set; }
        public List<Topping> toppings { get;set; }
        public float tiempoDeHorneadoNecesario { get; private set; } = 10f;
        public float tiempoDeHorneadoQuemado { get; private set; } = 20f;
        public float tiempoDeHorneadoActual { get; set; }
        public EstacionActual EstacionActual { get; set; } = EstacionActual.EstacionDeOrdenes;
        public Rectangle[] PosicionesToppings { get; private set; }
        public Torta(Rectangle area) : base(new Vector2(area.X, area.Y), area)
        {
            toppings = new List<Topping>();
        }
        public void crearPosicionesToppings() 
        {
            switch (FormaBizcochuelo) 
            {
                case FormaBizcochuelo.Corazon:
                    PosicionesToppings = new Rectangle[Constante.CANTIDAD_MAXIMA_TOPPINGS] {
                     new Rectangle(Area.X+250,Area.Y+150,100,100),
                     new Rectangle(Area.X+70, Area.Y+70, 100,100),
                     new Rectangle(Area.X+430, Area.Y+70,100,100)
                    };
                    break;
                case FormaBizcochuelo.Redondo:
                    PosicionesToppings = new Rectangle[Constante.CANTIDAD_MAXIMA_TOPPINGS] {
                     new Rectangle(Area.X+250,Area.Y+100,100,100),
                     new Rectangle(Area.X+50, Area.Y+100, 100,100),
                     new Rectangle(Area.X+450, Area.Y+100,100,100)
                    };
                    break;
                case FormaBizcochuelo.Cuadrado:
                    PosicionesToppings = new Rectangle[Constante.CANTIDAD_MAXIMA_TOPPINGS] {
                     new Rectangle(Area.X+250,Area.Y+70,100,100),
                     new Rectangle(Area.X+50, Area.Y+110, 100,100),
                     new Rectangle(Area.X+450, Area.Y+110,100,100)
                    };
                    break;
            }
        }
        public void LimpiarTorta()
        {

        }
        public void Actualizar(Vector2 posicionVirtual)
        {
            base.ActualizarArrastre(posicionVirtual);
        }
        public void Dibujar(SpriteBatch spriteBatch, Rectangle area, Texture2D textura)
        {
            spriteBatch.Draw(textura, area, Color.White);
            if (toppings.Count > 0) 
            {
                foreach (Topping topping in toppings)
                {
                    topping.Dibujar(spriteBatch);
                }
            }
        }
        public void cambiarArea(Rectangle nuevaArea)
        {
            Area = nuevaArea;
            base.Posicion = new Vector2(nuevaArea.X, nuevaArea.Y);
            crearPosicionesToppings();
        }
    }
}

