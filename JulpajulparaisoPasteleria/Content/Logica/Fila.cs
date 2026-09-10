
using Microsoft.Xna.Framework;


namespace JulpajulparaisoPasteleria.Content.Logica
{
    public class Fila
    {
        public Rectangle[] posiciones { get; private set; }

        public Fila()
        {
            posiciones = new Rectangle[]
            {
                new Rectangle(100, 200, 50, 50),
                new Rectangle(300, 200, 50, 50),
                new Rectangle(500, 200, 50, 50)
            };
        }
        public Rectangle obtenerPosicionFila(int indice)
        {
            return posiciones[indice];
        }
    }
}
