using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JulpajulparaisoPasteleria.Content.Objetos;
using JulpajulparaisoPasteleria.Content.Modelos;
using JulpajulparaisoPasteleria.Content.Personajes;
using JulpajulparaisoPasteleria.Content.Utilidades;
using JulpajulparaisoPasteleria.Content.Enumeradores;

namespace JulpajulparaisoPasteleria.Content.Logica
{
    public class CalculadorDePuntuacion
    {
        private Torta torta;
        private Pedido pedido;
        private int puntuacionAcumulada;

        public void RecibirElemetos(Torta torta, Pedido pedido) 
        {
            this.torta = torta;
            this.pedido = pedido;
            this.puntuacionAcumulada = 0;
        }
        public int CalcularPuntuacionTotal()
        {
            return puntuacionAcumulada;
        }
        public int CalcularPuntuacionEstacionDeOrdenes(Cliente cliente)
        {
            int puntaje = 0;
            if (cliente.cronometroEspera < Constante.TIEMPO_MAXIMO_DE_ESPERA / 2)
            {
                puntaje = 25;
            }
            else
            {
                puntaje = 15;
            }
            puntuacionAcumulada += puntaje;
            return puntaje;
        }
        public int CalcularPuntuacionEstacionDeMezcla()
        {
            int puntaje = 0;
            if (torta.FormaBizcochuelo == pedido.FormaBizcochuelo && torta.SaborBizcochuelo == pedido.SaborBizcochuelo)
            {
                puntaje = 25;
            }
            else if (torta.SaborBizcochuelo == pedido.SaborBizcochuelo || torta.FormaBizcochuelo == pedido.FormaBizcochuelo)
            {
                puntaje = 10;
            }
            puntuacionAcumulada += puntaje;
            return puntaje;
        }
        public int CalcularPuntuacionEstacionDeHorneado()
        {
            int puntaje = 0;
            if (torta.EstadoCoccion == EstadoCoccion.Quemado || torta.EstadoCoccion == EstadoCoccion.Crudo)
            {
                puntaje = 0;
            }
            else
            {
                puntaje = 20;
            }
            puntuacionAcumulada += puntaje;
            return puntaje;
        }
        public int CalcularPuntucacionEstacionDeDecoracion()
        {
            int puntaje = 0;
            if (torta.Relleno == pedido.SaborRelleno)
            {
                puntaje += 5;
            }
            if(torta.Cobertura == pedido.Cobertura)
            {
                puntaje += 10;
            }
            if (verificarTipoTopping())
            {
                puntaje += 15;
            }
            puntuacionAcumulada += puntaje;
            return puntaje;
        }
        private bool verificarTipoTopping()
        {
            if (torta.toppings.Count != pedido.ToppingsDeseados.Count)
            {
                return false;
            }
            List<TipoTopping> toppingsPuestosTipos = new List<TipoTopping>();

            foreach (Topping t in torta.toppings)
            {
                toppingsPuestosTipos.Add(t.tipoTopping);
            }
            foreach (TipoTopping topping in pedido.ToppingsDeseados)
            {
                if (toppingsPuestosTipos.Contains(topping))
                {
                    toppingsPuestosTipos.Remove(topping);
                }
                else 
                {
                    return false;
                }
            }
            return true;
        }
    }
}
