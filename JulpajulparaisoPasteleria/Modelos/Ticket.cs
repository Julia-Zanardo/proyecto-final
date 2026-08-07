using JulpajulparaisoPasteleria.Enumeradores;
using System.Collections.Generic;

namespace JulpajulparaisoPasteleria.Modelos
{
    public class Ticket
    {
        public int Id { get; private set; }
        public SaborBizcochuelo SaborBizcochuelo { get; private set; }
        public FormaBizcochuelo FormaBizcochuelo { get; private set; }
        public SaborRelleno SaborRelleno { get; private set; }
        public Cobertura Cobertura { get; private set; }
        public List<Topping> ToppingsDeseados { get; private set; }
        public Ticket(int id, SaborBizcochuelo saborBizcochuelo, FormaBizcochuelo formaBizcochuelo, SaborRelleno saborRelleno, Cobertura cobertura)
        {
            Id = id;
            SaborBizcochuelo = saborBizcochuelo;
            FormaBizcochuelo = formaBizcochuelo;
            SaborRelleno = saborRelleno;
            Cobertura = cobertura;
        }

    }
}