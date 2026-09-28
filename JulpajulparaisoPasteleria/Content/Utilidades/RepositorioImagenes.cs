using JulpajulparaisoPasteleria.Content.Botones;
using JulpajulparaisoPasteleria.Content.Enumeradores;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
namespace JulpajulparaisoPasteleria.Content.Utilidades
{
    public class RepositorioImagenes
    {

        public Dictionary<SaborBizcochuelo, Texture2D> IconosBizcochuelo { get; private set; }
        public Dictionary<SaborRelleno, Texture2D> IconosRelleno { get; private set; }
        public Dictionary<FormaBizcochuelo, Texture2D> IconosMoldes { get; private set; }
        public Dictionary<TipoCobertura, Texture2D> IconosCoberturas { get; private set; }
        public Dictionary<Topping, Texture2D> IconosToppings { get; private set; } 
        public BotonSabor[] IconosBotonesSabor { get; private set; }
        public Dictionary<SaborBizcochuelo, Texture2D> TexturasBowl { get; private set; }
        public void LoadContent(ContentManager content)
        {
            IconosBizcochuelo = new Dictionary<SaborBizcochuelo, Texture2D>();
            IconosBizcochuelo.Add(SaborBizcochuelo.CarameloVainilla, content.Load<Texture2D>("imagenes/SaboresBizcochuelo/botonCarameloVainilla"));
            IconosBizcochuelo.Add(SaborBizcochuelo.Chocolate, content.Load<Texture2D>("imagenes/SaboresBizcochuelo/botonChocolate"));
            IconosBizcochuelo.Add(SaborBizcochuelo.Arandano, content.Load<Texture2D>("imagenes/SaboresBizcochuelo/botonArandano"));
            IconosBizcochuelo.Add(SaborBizcochuelo.Frutilla, content.Load<Texture2D>("imagenes/SaboresBizcochuelo/botonFrutilla"));
            IconosRelleno = new Dictionary<SaborRelleno, Texture2D>();
            IconosRelleno.Add(SaborRelleno.Chantilly, content.Load<Texture2D>("imagenes/Rellenos/rellenoChantilly"));
            IconosRelleno.Add(SaborRelleno.Chocolate, content.Load<Texture2D>("imagenes/Rellenos/rellenoChocolate"));
            IconosRelleno.Add(SaborRelleno.Frutilla, content.Load<Texture2D>("imagenes/Rellenos/rellenoFrutilla"));
            IconosRelleno.Add(SaborRelleno.Limon, content.Load<Texture2D>("imagenes/Rellenos/rellenoLimon"));
            IconosMoldes = new Dictionary<FormaBizcochuelo, Texture2D>();
            IconosMoldes.Add(FormaBizcochuelo.Redondo, content.Load<Texture2D>("imagenes/Moldes/moldeRedondo"));
            IconosMoldes.Add(FormaBizcochuelo.Cuadrado, content.Load<Texture2D>("imagenes/Moldes/moldeCuadrado"));
            IconosMoldes.Add(FormaBizcochuelo.Corazon, content.Load<Texture2D>("imagenes/Moldes/moldeCorazon"));
            IconosCoberturas = new Dictionary<TipoCobertura, Texture2D>();
            IconosCoberturas.Add(TipoCobertura.GlaseadoDeBanana, content.Load<Texture2D>("imagenes/Coberturas/glaseadoBanana"));
            IconosCoberturas.Add(TipoCobertura.GlaseadoDeFrutilla, content.Load<Texture2D>("imagenes/Coberturas/glaseadoFrutilla"));
            IconosCoberturas.Add(TipoCobertura.GlaseadoDePistacho, content.Load<Texture2D>("imagenes/Coberturas/glaseadoPistacho"));
            IconosCoberturas.Add(TipoCobertura.GlaseadoDeVainilla, content.Load<Texture2D>("imagenes/Coberturas/glaseadoVainilla"));
            IconosCoberturas.Add(TipoCobertura.GanacheChocolate, content.Load<Texture2D>("imagenes/Coberturas/ganacheChocolate"));
            IconosCoberturas.Add(TipoCobertura.AlgodonDeAzucar, content.Load<Texture2D>("imagenes/Coberturas/algodonAzucar"));
            IconosCoberturas.Add(TipoCobertura.Napolitano, content.Load<Texture2D>("imagenes/Coberturas/napolitana"));
            IconosToppings = new Dictionary<Topping, Texture2D>();
            IconosToppings.Add(Topping.Banana, content.Load<Texture2D>("imagenes/Topings/topingBanana"));
            IconosToppings.Add(Topping.Cereza, content.Load<Texture2D>("imagenes/Topings/topingCereza"));
            IconosToppings.Add(Topping.Oreo, content.Load<Texture2D>("imagenes/Topings/topingOreo"));
            IconosToppings.Add(Topping.Cubanito, content.Load<Texture2D>("imagenes/Topings/topingCubanito"));
            IconosToppings.Add(Topping.Waffle, content.Load<Texture2D>("imagenes/Topings/topingWaffle"));
            IconosBotonesSabor = new BotonSabor[4];
            IconosBotonesSabor[0]=(new BotonSabor(content.Load<Texture2D>("imagenes/SaboresBizcochuelo/botonCarameloVainilla"), new Microsoft.Xna.Framework.Rectangle(285, 240, 150, 100), SaborBizcochuelo.CarameloVainilla));
            IconosBotonesSabor[1] = (new BotonSabor(content.Load<Texture2D>("imagenes/SaboresBizcochuelo/botonArandano"), new Microsoft.Xna.Framework.Rectangle(465, 240, 150, 100), SaborBizcochuelo.Arandano));
            IconosBotonesSabor[2] = (new BotonSabor(content.Load<Texture2D>("imagenes/SaboresBizcochuelo/botonFrutilla"), new Microsoft.Xna.Framework.Rectangle(1165, 240, 150, 100), SaborBizcochuelo.Frutilla));
            IconosBotonesSabor[3] = (new BotonSabor(content.Load<Texture2D>("imagenes/SaboresBizcochuelo/botonChocolate"), new Microsoft.Xna.Framework.Rectangle(1345, 240, 150, 100), SaborBizcochuelo.Chocolate));
            TexturasBowl = new Dictionary<SaborBizcochuelo, Texture2D>();
            TexturasBowl.Add(SaborBizcochuelo.CarameloVainilla, content.Load<Texture2D>("imagenes/Bowls/bowlCaramelo"));
            TexturasBowl.Add(SaborBizcochuelo.Chocolate, content.Load<Texture2D>("imagenes/Bowls/bowlChocolate"));
            TexturasBowl.Add(SaborBizcochuelo.Arandano, content.Load<Texture2D>("imagenes/Bowls/bowlArandano"));
            TexturasBowl.Add(SaborBizcochuelo.Frutilla, content.Load<Texture2D>("imagenes/Bowls/bowlFrutilla"));
        }
    }
}
