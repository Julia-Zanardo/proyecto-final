using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace JulpajulparaisoPasteleria.Content
{
    internal class ManejoEntrada
    {
        private static MouseState estadoActualMouse;
        private static MouseState estadoAnteriorMouse;

        public static Vector2 PosicionMouse
        {
            get { return  new Vector2(estadoActualMouse.X, estadoActualMouse.Y);}
        }
        public static Rectangle LimitesMouse
        {
        get { return new Rectangle(estadoActualMouse.X, estadoActualMouse.Y, 1, 1); }
        }
        public static void actualizar()
        {
            estadoAnteriorMouse = estadoActualMouse;
            estadoActualMouse  = Mouse.GetState();
        }
        public static bool elementoClickeado()
        {
            return estadoActualMouse.LeftButton == ButtonState.Pressed && estadoAnteriorMouse.LeftButton == ButtonState.Released;
        }
        public static bool elementoPresionado()
        {
            return estadoActualMouse.LeftButton == ButtonState.Pressed;
        }
        public static bool elementoSoltado()
        {
            return estadoActualMouse.LeftButton == ButtonState.Released && estadoAnteriorMouse.LeftButton == ButtonState.Pressed;
        }
    }
}
