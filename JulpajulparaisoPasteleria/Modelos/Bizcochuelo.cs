using JulpajulparaisoPasteleria.Enumeradores;
namespace JulpajulparaisoPasteleria.Modelos
{
    public class Bizcochuelo
    {
        private SaborBizcochuelo Sabor {  get; set; }
        private float Precio { get; set; } = 10000f;
        private TiempoCoccion Coccion { get; set; }
    }
}