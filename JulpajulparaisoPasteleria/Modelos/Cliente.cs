using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JulpajulparaisoPasteleria.Modelos
{
    public class Cliente
    {
        public int Id { get; private set; }
        public string Nombre { get; private set; }
        public Ticket Ticket { get; private set; }
        public float Paciencia { get; private set; }
        public float PacienciaMax { get; private set; }
        public Cliente(int id, String nombre, Ticket ticket, float paciencia)
        {
            Id = id;
            Nombre = nombre;
            Ticket = ticket;
            Paciencia = paciencia;
        }
        public void ReducirPaciencia(float tiempoTranscurrido)
        {
            Paciencia -= tiempoTranscurrido;
            if (Paciencia < 0)
            {
                Paciencia = 0;
            }
        }
        public bool EstaEnojado()
        {
            return Paciencia <= 0;
        }
    }
}
