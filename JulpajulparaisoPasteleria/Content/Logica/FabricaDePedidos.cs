using JulpajulparaisoPasteleria.Content.Enumeradores;
using JulpajulparaisoPasteleria.Content.Modelos;
using JulpajulparaisoPasteleria.Content.Utilidades;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
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
        private TipoTopping[] toppings = (TipoTopping[])Enum.GetValues(typeof(TipoTopping));

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
            List<TipoTopping> toppingsDeseados = new List<TipoTopping>();
            int cantidadToppings = r.Next(1, Constante.CANTIDAD_MAXIMA_TOPPINGS);

            for (int i = 0; i < cantidadToppings; i++)
            {
                TipoTopping toppingAleatorio = toppings[r.Next(toppings.Length)];
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
