using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JulpajulparaisoPasteleria.Content.Logica
{
    public static class CargadorDeSkins
    {
        private static Dictionary<int, ConfiguracionSkin> datosSkins = new Dictionary<int, ConfiguracionSkin>()
        {
            { 1, new ConfiguracionSkin(12, 9, 12) },
            { 2, new ConfiguracionSkin(12, 7, 12) },
            { 3, new ConfiguracionSkin(12, 6, 12) },

        };
        public static List<SkinCliente> CargarSkinsClientes(ContentManager content, int cantidadPersonajes)
        {
            List<SkinCliente> skinsClientes = new List<SkinCliente>();
            for (int i = 1; i <= cantidadPersonajes; i++)
            {
                ConfiguracionSkin configuracionColumnas = datosSkins[i];
                Texture2D caminando = content.Load<Texture2D>($"imagenes/Sprites/clienteCaminando{i}");
                Texture2D esperando = content.Load<Texture2D>($"imagenes/Sprites/clienteEsperando{i}");
                skinsClientes.Add(new SkinCliente(caminando, esperando, configuracionColumnas));
            }
            return skinsClientes;
        }
    }
}
