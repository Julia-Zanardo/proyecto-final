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
            { 1, new ConfiguracionSkin(12, 9, 12, 8, 11) },
            { 2, new ConfiguracionSkin(12, 7, 12, 9, 6) },
            { 3, new ConfiguracionSkin(12, 6, 12, 6, 5) },
            { 4, new ConfiguracionSkin(8, 11, 8, 5,13) },
            { 5, new ConfiguracionSkin(8, 7, 8, 10,9) },
            { 6, new ConfiguracionSkin(10, 6, 10, 5,3) },
            { 7, new ConfiguracionSkin(10, 6, 10, 4,3) },
            { 8, new ConfiguracionSkin(10, 6, 10, 4,3) }
        };
        public static List<SkinCliente> CargarSkinsClientes(ContentManager content, int cantidadPersonajes)
        {
            List<SkinCliente> skinsClientes = new List<SkinCliente>();
            for (int i = 1; i <= cantidadPersonajes; i++)
            {
                ConfiguracionSkin configuracionColumnas = datosSkins[i];
                Texture2D caminando = content.Load<Texture2D>($"imagenes/Sprites/clienteCaminando{i}");
                Texture2D esperando = content.Load<Texture2D>($"imagenes/Sprites/clienteEsperando{i}");
                Texture2D enojado = content.Load<Texture2D>($"imagenes/Sprites/clienteEnojado{i}");
                Texture2D hablando = content.Load<Texture2D>($"imagenes/Sprites/clienteHablando{i}");
                skinsClientes.Add(new SkinCliente(caminando,esperando,enojado,hablando, configuracionColumnas));
            }
            return skinsClientes;
        }
    }
}
