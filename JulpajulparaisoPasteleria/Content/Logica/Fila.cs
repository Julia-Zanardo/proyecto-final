
using Microsoft.Xna.Framework;


namespace JulpajulparaisoPasteleria.Content.Logica
{
    public class Fila
    {
        public Rectangle[] posicionesOrdenarPedido { get; private set; }
        public Rectangle[] posicionesEntregaPedido { get; private set; }

        public Fila()
        {
            posicionesOrdenarPedido = new Rectangle[]
            {
                new Rectangle(100, 200, 50, 50),
                new Rectangle(300, 200, 50, 50),
                new Rectangle(500, 200, 50, 50),
                new Rectangle(700, 200, 50, 50),
                new Rectangle(900, 200, 50, 50),
                new Rectangle(1100, 200, 50, 50),
                new Rectangle(1300, 200, 50, 50),
                new Rectangle(1500, 200, 50, 50),
                new Rectangle(1700, 200, 50, 50)
            };
            posicionesEntregaPedido = new Rectangle[]
            {
                new Rectangle(100, 100, 50, 50),
                new Rectangle(300, 100, 50, 50),
                new Rectangle(500, 100, 50, 50),
                new Rectangle(700, 100, 50, 50),
                new Rectangle(900, 100, 50, 50),
                new Rectangle(1100, 100, 50, 50),
                new Rectangle(1300, 100, 50, 50),
                new Rectangle(1500, 100, 50, 50),
                new Rectangle(1700, 100, 50, 50)
            };
        }
        public Rectangle obtenerPosicionFilaPedido(int indice)
        {
            return posicionesOrdenarPedido[indice];
        }
        public Rectangle obtenerPosicionFilaEntrega(int indice)
        {
            return posicionesEntregaPedido[indice];
        }
    }
}
