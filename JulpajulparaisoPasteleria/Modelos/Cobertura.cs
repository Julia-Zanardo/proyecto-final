using JulpajulparaisoPasteleria.Enumeradores;

namespace JulpajulparaisoPasteleria.Modelos
{
    public class Cobertura
    {
        public float Precio {  get; private set; } = 5000f;
        public TipoCobertura Tipo {  get; private set; }
    }
}