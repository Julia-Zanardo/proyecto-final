using JulpajulparaisoPasteleria.Enumeradores;
namespace JulpajulparaisoPasteleria.Content.Objetos
{
    public class Bizcochuelo
    {
        public SaborBizcochuelo Sabor {  get; private set; }
        public TiempoCoccion Coccion { get; private set; }
        Bizcochuelo(SaborBizcochuelo saborElegido) { 
            Sabor = saborElegido;
            Coccion = TiempoCoccion.Crudo;

        }
        public void cambiarEstadoDeCoccion(TiempoCoccion coccion) {
            this.Coccion = coccion;
        }
    }
    }