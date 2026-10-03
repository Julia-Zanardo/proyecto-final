using JulpajulparaisoPasteleria.Content.Enumeradores;
using System.Collections.Generic;

namespace JulpajulparaisoPasteleria.Content.Modelos
{
    public class Pedido
    {
        public int Id { get; private set; }
        public SaborBizcochuelo SaborBizcochuelo { get; private set; }
        public FormaBizcochuelo FormaBizcochuelo { get; private set; }
        public SaborRelleno SaborRelleno { get; private set; }
        public TipoCobertura Cobertura { get; private set; }
        public List<TipoTopping> ToppingsDeseados { get; private set; }
        public Pedido(int id, SaborBizcochuelo saborBizcochuelo, FormaBizcochuelo formaBizcochuelo, SaborRelleno saborRelleno, TipoCobertura cobertura, List<TipoTopping> toppingsDeseados)
        {
            Id = id;
            SaborBizcochuelo = saborBizcochuelo;
            FormaBizcochuelo = formaBizcochuelo;
            SaborRelleno = saborRelleno;
            Cobertura = cobertura;
            ToppingsDeseados = toppingsDeseados;
        }

    }
}