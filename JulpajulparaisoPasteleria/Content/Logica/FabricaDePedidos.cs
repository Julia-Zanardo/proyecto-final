using JulpajulparaisoPasteleria.Enumeradores;
using JulpajulparaisoPasteleria.Modelos;
using System;
using System.Collections.Generic;
using static Microsoft.Xna.Framework.MathHelper;

namespace JulpajulparaisoPasteleria.Content.Logica
{
    public class FabricaDePedidos
    {
        private System.Random r;
        private SaborBizcochuelo[] saboresBizcochuelo = (SaborBizcochuelo[])Enum.GetValues(typeof(SaborBizcochuelo));
        private FormaBizcochuelo[] formasBizcochuelo = (FormaBizcochuelo[])Enum.GetValues(typeof(FormaBizcochuelo));
        private SaborRelleno[] saboresRelleno = (SaborRelleno[])Enum.GetValues(typeof(SaborRelleno));
        private TipoCobertura[] tiposCobertura = (TipoCobertura[])Enum.GetValues(typeof(TipoCobertura));
        private Topping[] toppings = (Topping[])Enum.GetValues(typeof(Topping));

        public FabricaDePedidos() 
        {
            r = new System.Random();
        }
        public Pedido CrearPedido(int id)
        {
            SaborBizcochuelo saborElegido = saboresBizcochuelo[r.Next(saboresBizcochuelo.Length)];
            FormaBizcochuelo formaElegida = formasBizcochuelo[r.Next(formasBizcochuelo.Length)];
            SaborRelleno rellenoElegido = saboresRelleno[r.Next(saboresRelleno.Length)];
            TipoCobertura coberturaElegida = tiposCobertura[r.Next(tiposCobertura.Length)];
            List<Topping> toppingsDeseados = new List<Topping>();
            int cantidadToppings = r.Next(1, 3);

            for (int i = 0; i < cantidadToppings; i++)
            {
                Topping toppingAleatorio = toppings[r.Next(toppings.Length)];
                toppingsDeseados.Add(toppingAleatorio);
            }
            return new Pedido(
                id,
                saborElegido,
                formaElegida,
                rellenoElegido,
                coberturaElegida,
                toppingsDeseados
            );
        }
    }
}
