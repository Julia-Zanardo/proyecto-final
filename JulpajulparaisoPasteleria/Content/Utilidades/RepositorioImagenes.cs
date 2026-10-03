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
        public Dictionary<TipoTopping, Texture2D> IconosToppings { get; private set; } 
        public BotonSabor[] IconosBotonesSabor { get; private set; }
        public Dictionary<SaborBizcochuelo, Texture2D> TexturasBowl { get; private set; }
        public BotonForma[] IconosBotonesFormas { get; private set; }
        public Dictionary<(SaborBizcochuelo, FormaBizcochuelo, EstadoCoccion), Texture2D> TexturasMoldeSaborCoccion { get; private set; } 
        public Dictionary<FormaBizcochuelo, Texture2D> TexturasFormaQuemada { get; private set; }
        public Dictionary<(SaborBizcochuelo, FormaBizcochuelo), Texture2D> TexturasBizcochueloSinRelleno { get; private set; }
        public BotonCobertura[] IconosBotonesCobertura { get; private set; }
        public BotonRelleno[] IconosBotonesRelleno { get; private set; }
        public Texture2D TexturaBasura { get; private set; }
        public Dictionary<(SaborBizcochuelo,FormaBizcochuelo,SaborRelleno), Texture2D> TexturasBizcochueloConRelleno { get; private set; }
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
            IconosToppings = new Dictionary<TipoTopping, Texture2D>();
            IconosToppings.Add(TipoTopping.Banana, content.Load<Texture2D>("imagenes/Topings/topingBanana"));
            IconosToppings.Add(TipoTopping.Cereza, content.Load<Texture2D>("imagenes/Topings/topingCereza"));
            IconosToppings.Add(TipoTopping.Oreo, content.Load<Texture2D>("imagenes/Topings/topingOreo"));
            IconosToppings.Add(TipoTopping.Cubanito, content.Load<Texture2D>("imagenes/Topings/topingCubanito"));
            IconosToppings.Add(TipoTopping.Waffle, content.Load<Texture2D>("imagenes/Topings/topingWaffle"));
            IconosBotonesSabor = new BotonSabor[4];
            IconosBotonesSabor[0] = (new BotonSabor(content.Load<Texture2D>("imagenes/SaboresBizcochuelo/botonCarameloVainilla"), new Microsoft.Xna.Framework.Rectangle(285, 240, 150, 100), SaborBizcochuelo.CarameloVainilla));
            IconosBotonesSabor[1] = (new BotonSabor(content.Load<Texture2D>("imagenes/SaboresBizcochuelo/botonArandano"), new Microsoft.Xna.Framework.Rectangle(465, 240, 150, 100), SaborBizcochuelo.Arandano));
            IconosBotonesSabor[2] = (new BotonSabor(content.Load<Texture2D>("imagenes/SaboresBizcochuelo/botonFrutilla"), new Microsoft.Xna.Framework.Rectangle(1165, 240, 150, 100), SaborBizcochuelo.Frutilla));
            IconosBotonesSabor[3] = (new BotonSabor(content.Load<Texture2D>("imagenes/SaboresBizcochuelo/botonChocolate"), new Microsoft.Xna.Framework.Rectangle(1345, 240, 150, 100), SaborBizcochuelo.Chocolate));
            TexturasBowl = new Dictionary<SaborBizcochuelo, Texture2D>();
            TexturasBowl.Add(SaborBizcochuelo.CarameloVainilla, content.Load<Texture2D>("imagenes/Bowls/bowlCaramelo"));
            TexturasBowl.Add(SaborBizcochuelo.Chocolate, content.Load<Texture2D>("imagenes/Bowls/bowlChocolate"));
            TexturasBowl.Add(SaborBizcochuelo.Arandano, content.Load<Texture2D>("imagenes/Bowls/bowlArandano"));
            TexturasBowl.Add(SaborBizcochuelo.Frutilla, content.Load<Texture2D>("imagenes/Bowls/bowlFrutilla"));
            IconosBotonesFormas = new BotonForma[3];
            IconosBotonesFormas[0] = (new BotonForma(content.Load<Texture2D>("imagenes/Moldes/moldeRedondo"), new Microsoft.Xna.Framework.Rectangle(815, 75, 150, 100), FormaBizcochuelo.Redondo));
            IconosBotonesFormas[1] = (new BotonForma(content.Load<Texture2D>("imagenes/Moldes/moldeCuadrado"), new Microsoft.Xna.Framework.Rectangle(815, 200, 150, 100), FormaBizcochuelo.Cuadrado));
            IconosBotonesFormas[2] = (new BotonForma(content.Load<Texture2D>("imagenes/Moldes/moldeCorazon"), new Microsoft.Xna.Framework.Rectangle(815, 325, 150, 100), FormaBizcochuelo.Corazon));
            TexturasMoldeSaborCoccion = new Dictionary<(SaborBizcochuelo, FormaBizcochuelo, EstadoCoccion), Texture2D>();
            TexturasMoldeSaborCoccion.Add((SaborBizcochuelo.CarameloVainilla, FormaBizcochuelo.Redondo, EstadoCoccion.Crudo), content.Load<Texture2D>("imagenes/Moldes/moldeVainillaRedondoCrudo"));
            TexturasMoldeSaborCoccion.Add((SaborBizcochuelo.CarameloVainilla, FormaBizcochuelo.Redondo, EstadoCoccion.Perfecto), content.Load<Texture2D>("imagenes/Moldes/moldeVainillaRedondoPerfecto"));
            TexturasMoldeSaborCoccion.Add((SaborBizcochuelo.Chocolate, FormaBizcochuelo.Redondo, EstadoCoccion.Crudo), content.Load<Texture2D>("imagenes/Moldes/moldeChocolateRedondoCrudo"));
            TexturasMoldeSaborCoccion.Add((SaborBizcochuelo.Chocolate, FormaBizcochuelo.Redondo, EstadoCoccion.Perfecto), content.Load<Texture2D>("imagenes/Moldes/moldeChocolateRedondoPerfecto"));
            TexturasMoldeSaborCoccion.Add((SaborBizcochuelo.Frutilla, FormaBizcochuelo.Redondo, EstadoCoccion.Crudo), content.Load<Texture2D>("imagenes/Moldes/moldeFrutillaRedondoCrudo"));
            TexturasMoldeSaborCoccion.Add((SaborBizcochuelo.Frutilla, FormaBizcochuelo.Redondo, EstadoCoccion.Perfecto), content.Load<Texture2D>("imagenes/Moldes/moldeFrutillaRedondoPerfecto"));
            TexturasMoldeSaborCoccion.Add((SaborBizcochuelo.Arandano, FormaBizcochuelo.Redondo, EstadoCoccion.Crudo), content.Load<Texture2D>("imagenes/Moldes/moldeArandanoRedondoCrudo"));
            TexturasMoldeSaborCoccion.Add((SaborBizcochuelo.Arandano, FormaBizcochuelo.Redondo, EstadoCoccion.Perfecto), content.Load<Texture2D>("imagenes/Moldes/moldeArandanoRedondoPerfecto"));

            TexturasMoldeSaborCoccion.Add((SaborBizcochuelo.CarameloVainilla, FormaBizcochuelo.Cuadrado, EstadoCoccion.Crudo), content.Load<Texture2D>("imagenes/Moldes/moldeVainillaCuadradoCrudo"));
            TexturasMoldeSaborCoccion.Add((SaborBizcochuelo.CarameloVainilla, FormaBizcochuelo.Cuadrado, EstadoCoccion.Perfecto), content.Load<Texture2D>("imagenes/Moldes/moldeVainillaCuadradoPerfecto"));
            TexturasMoldeSaborCoccion.Add((SaborBizcochuelo.Chocolate, FormaBizcochuelo.Cuadrado, EstadoCoccion.Crudo), content.Load<Texture2D>("imagenes/Moldes/moldeChocolateCuadradoCrudo"));
            TexturasMoldeSaborCoccion.Add((SaborBizcochuelo.Chocolate, FormaBizcochuelo.Cuadrado, EstadoCoccion.Perfecto), content.Load<Texture2D>("imagenes/Moldes/moldeChocolateCuadradoPerfecto"));
            TexturasMoldeSaborCoccion.Add((SaborBizcochuelo.Frutilla, FormaBizcochuelo.Cuadrado, EstadoCoccion.Crudo), content.Load<Texture2D>("imagenes/Moldes/moldeFrutillaCuadradoCrudo"));
            TexturasMoldeSaborCoccion.Add((SaborBizcochuelo.Frutilla, FormaBizcochuelo.Cuadrado, EstadoCoccion.Perfecto), content.Load<Texture2D>("imagenes/Moldes/moldeFrutillaCuadradoPerfecto"));
            TexturasMoldeSaborCoccion.Add((SaborBizcochuelo.Arandano, FormaBizcochuelo.Cuadrado, EstadoCoccion.Crudo), content.Load<Texture2D>("imagenes/Moldes/moldeArandanoCuadradoCrudo"));
            TexturasMoldeSaborCoccion.Add((SaborBizcochuelo.Arandano, FormaBizcochuelo.Cuadrado, EstadoCoccion.Perfecto), content.Load<Texture2D>("imagenes/Moldes/moldeArandanoCuadradoPerfecto"));

            TexturasMoldeSaborCoccion.Add((SaborBizcochuelo.CarameloVainilla, FormaBizcochuelo.Corazon, EstadoCoccion.Crudo), content.Load<Texture2D>("imagenes/Moldes/moldeVainillaCorazonCrudo"));
            TexturasMoldeSaborCoccion.Add((SaborBizcochuelo.CarameloVainilla, FormaBizcochuelo.Corazon, EstadoCoccion.Perfecto), content.Load<Texture2D>("imagenes/Moldes/moldeVainillaCorazonPerfecto"));
            TexturasMoldeSaborCoccion.Add((SaborBizcochuelo.Chocolate, FormaBizcochuelo.Corazon, EstadoCoccion.Crudo), content.Load<Texture2D>("imagenes/Moldes/moldeChocolateCorazonCrudo"));
            TexturasMoldeSaborCoccion.Add((SaborBizcochuelo.Chocolate, FormaBizcochuelo.Corazon, EstadoCoccion.Perfecto), content.Load<Texture2D>("imagenes/Moldes/moldeChocolateCorazonPerfecto"));
            TexturasMoldeSaborCoccion.Add((SaborBizcochuelo.Frutilla, FormaBizcochuelo.Corazon, EstadoCoccion.Crudo), content.Load<Texture2D>("imagenes/Moldes/moldeFrutillaCorazonCrudo"));
            TexturasMoldeSaborCoccion.Add((SaborBizcochuelo.Frutilla, FormaBizcochuelo.Corazon, EstadoCoccion.Perfecto), content.Load<Texture2D>("imagenes/Moldes/moldeFrutillaCorazonPerfecto"));
            TexturasMoldeSaborCoccion.Add((SaborBizcochuelo.Arandano, FormaBizcochuelo.Corazon, EstadoCoccion.Crudo), content.Load<Texture2D>("imagenes/Moldes/moldeArandanoCorazonCrudo"));
            TexturasMoldeSaborCoccion.Add((SaborBizcochuelo.Arandano, FormaBizcochuelo.Corazon, EstadoCoccion.Perfecto), content.Load<Texture2D>("imagenes/Moldes/moldeArandanoCorazonPerfecto"));

            TexturasFormaQuemada = new Dictionary<FormaBizcochuelo, Texture2D>();
            TexturasFormaQuemada.Add(FormaBizcochuelo.Redondo, content.Load<Texture2D>("imagenes/Moldes/moldeRedondoQuemado"));
            TexturasFormaQuemada.Add(FormaBizcochuelo.Cuadrado, content.Load<Texture2D>("imagenes/Moldes/moldeCuadradoQuemado"));
            TexturasFormaQuemada.Add(FormaBizcochuelo.Corazon, content.Load<Texture2D>("imagenes/Moldes/moldeCorazonQuemado"));

            TexturasBizcochueloSinRelleno = new Dictionary<(SaborBizcochuelo, FormaBizcochuelo), Texture2D>();
            TexturasBizcochueloSinRelleno.Add((SaborBizcochuelo.CarameloVainilla, FormaBizcochuelo.Redondo), content.Load<Texture2D>("imagenes/Bizcochuelos/vainillaRedondoSinCorte"));
            TexturasBizcochueloSinRelleno.Add((SaborBizcochuelo.CarameloVainilla, FormaBizcochuelo.Cuadrado), content.Load<Texture2D>("imagenes/Bizcochuelos/vainillaCuadradoSinCorte"));
            TexturasBizcochueloSinRelleno.Add((SaborBizcochuelo.CarameloVainilla, FormaBizcochuelo.Corazon), content.Load<Texture2D>("imagenes/Bizcochuelos/vainillaCorazonSinCorte"));
            TexturasBizcochueloSinRelleno.Add((SaborBizcochuelo.Frutilla, FormaBizcochuelo.Redondo), content.Load<Texture2D>("imagenes/Bizcochuelos/frutillaRedondoSinCorte"));
            TexturasBizcochueloSinRelleno.Add((SaborBizcochuelo.Frutilla, FormaBizcochuelo.Cuadrado), content.Load<Texture2D>("imagenes/Bizcochuelos/frutillaCuadradoSinCorte"));
            TexturasBizcochueloSinRelleno.Add((SaborBizcochuelo.Frutilla, FormaBizcochuelo.Corazon), content.Load<Texture2D>("imagenes/Bizcochuelos/frutillaCorazonSinCorte"));
            TexturasBizcochueloSinRelleno.Add((SaborBizcochuelo.Chocolate, FormaBizcochuelo.Redondo), content.Load<Texture2D>("imagenes/Bizcochuelos/chocolateRedondoSinCorte"));
            TexturasBizcochueloSinRelleno.Add((SaborBizcochuelo.Chocolate, FormaBizcochuelo.Cuadrado), content.Load<Texture2D>("imagenes/Bizcochuelos/chocolateCuadradoSinCorte"));
            TexturasBizcochueloSinRelleno.Add((SaborBizcochuelo.Chocolate, FormaBizcochuelo.Corazon), content.Load<Texture2D>("imagenes/Bizcochuelos/chocolateCorazonSinCorte"));
            TexturasBizcochueloSinRelleno.Add((SaborBizcochuelo.Arandano, FormaBizcochuelo.Redondo), content.Load<Texture2D>("imagenes/Bizcochuelos/arandanoRedondoSinCorte"));
            TexturasBizcochueloSinRelleno.Add((SaborBizcochuelo.Arandano, FormaBizcochuelo.Cuadrado), content.Load<Texture2D>("imagenes/Bizcochuelos/arandanoCuadradoSinCorte"));
            TexturasBizcochueloSinRelleno.Add((SaborBizcochuelo.Arandano, FormaBizcochuelo.Corazon), content.Load<Texture2D>("imagenes/Bizcochuelos/arandanoCorazonSinCorte"));

            IconosBotonesCobertura = new BotonCobertura[7];
            IconosBotonesCobertura[0] = (new BotonCobertura(content.Load<Texture2D>("imagenes/Coberturas/glaseadoBanana"), new Rectangle(600, 100, 60, 160), TipoCobertura.GlaseadoDeBanana));
            IconosBotonesCobertura[1] = (new BotonCobertura(content.Load<Texture2D>("imagenes/Coberturas/algodonAzucar"), new Rectangle(700, 100, 60, 160), TipoCobertura.AlgodonDeAzucar));
            IconosBotonesCobertura[2] = (new BotonCobertura(content.Load<Texture2D>("imagenes/Coberturas/glaseadoFrutilla"), new Rectangle(800, 100, 60, 160), TipoCobertura.GlaseadoDeFrutilla));
            IconosBotonesCobertura[3] = (new BotonCobertura(content.Load<Texture2D>("imagenes/Coberturas/ganacheChocolate"), new Rectangle(900, 100, 60, 160), TipoCobertura.GanacheChocolate));
            IconosBotonesCobertura[4] = (new BotonCobertura(content.Load<Texture2D>("imagenes/Coberturas/glaseadoPistacho"), new Rectangle(1000, 100, 60, 160), TipoCobertura.GlaseadoDePistacho));
            IconosBotonesCobertura[5] = (new BotonCobertura(content.Load<Texture2D>("imagenes/Coberturas/glaseadoVainilla"), new Rectangle(1100, 100, 60, 160), TipoCobertura.GlaseadoDeVainilla));
            IconosBotonesCobertura[6] = (new BotonCobertura(content.Load<Texture2D>("imagenes/Coberturas/napolitana"), new Rectangle(1200, 100, 60, 160), TipoCobertura.Napolitano));

            IconosBotonesRelleno = new BotonRelleno[4];
            IconosBotonesRelleno[0] = (new BotonRelleno(content.Load<Texture2D>("imagenes/Rellenos/botonRellenoChantilly"), new Rectangle(750, 300, 60, 150), SaborRelleno.Chantilly));
            IconosBotonesRelleno[1] = (new BotonRelleno(content.Load<Texture2D>("imagenes/Rellenos/botonRellenoChocolate"), new Rectangle(850, 300, 60, 150), SaborRelleno.Chocolate));
            IconosBotonesRelleno[2] = (new BotonRelleno(content.Load<Texture2D>("imagenes/Rellenos/botonRellenoFrutilla"), new Rectangle(950, 300, 60, 150), SaborRelleno.Frutilla));
            IconosBotonesRelleno[3] = (new BotonRelleno(content.Load<Texture2D>("imagenes/Rellenos/botonRellenoLimon"), new Rectangle(1050, 300, 60, 150), SaborRelleno.Limon));

            TexturaBasura = content.Load<Texture2D>("imagenes/EstacionDecoracion/areaDeBasura");

            TexturasBizcochueloConRelleno = new Dictionary<(SaborBizcochuelo, FormaBizcochuelo, SaborRelleno), Texture2D>();
            TexturasBizcochueloConRelleno.Add((SaborBizcochuelo.CarameloVainilla, FormaBizcochuelo.Redondo, SaborRelleno.Chantilly), content.Load<Texture2D>("imagenes/BizcochuelosRellenos/vainillaRedondoCorteChantilly"));
            TexturasBizcochueloConRelleno.Add((SaborBizcochuelo.CarameloVainilla, FormaBizcochuelo.Redondo, SaborRelleno.Chocolate), content.Load<Texture2D>("imagenes/BizcochuelosRellenos/vainillaRedondoCorteChocolate"));
            TexturasBizcochueloConRelleno.Add((SaborBizcochuelo.CarameloVainilla, FormaBizcochuelo.Redondo, SaborRelleno.Frutilla), content.Load<Texture2D>("imagenes/BizcochuelosRellenos/vainillaRedondoCorteFrutilla"));
            TexturasBizcochueloConRelleno.Add((SaborBizcochuelo.CarameloVainilla, FormaBizcochuelo.Redondo, SaborRelleno.Limon), content.Load<Texture2D>("imagenes/BizcochuelosRellenos/vainillaRedondoCorteLimon"));

            TexturasBizcochueloConRelleno.Add((SaborBizcochuelo.CarameloVainilla, FormaBizcochuelo.Cuadrado, SaborRelleno.Chantilly), content.Load<Texture2D>("imagenes/BizcochuelosRellenos/vainillaCuadradoCorteChantilly"));
            TexturasBizcochueloConRelleno.Add((SaborBizcochuelo.CarameloVainilla, FormaBizcochuelo.Cuadrado, SaborRelleno.Chocolate), content.Load<Texture2D>("imagenes/BizcochuelosRellenos/vainillaCuadradoCorteChocolate"));
            TexturasBizcochueloConRelleno.Add((SaborBizcochuelo.CarameloVainilla, FormaBizcochuelo.Cuadrado, SaborRelleno.Frutilla), content.Load<Texture2D>("imagenes/BizcochuelosRellenos/vainillaCuadradoCorteFrutilla"));
            TexturasBizcochueloConRelleno.Add((SaborBizcochuelo.CarameloVainilla, FormaBizcochuelo.Cuadrado, SaborRelleno.Limon), content.Load<Texture2D>("imagenes/BizcochuelosRellenos/vainillaCuadradoCorteLimon"));

            TexturasBizcochueloConRelleno.Add((SaborBizcochuelo.CarameloVainilla, FormaBizcochuelo.Corazon, SaborRelleno.Chantilly), content.Load<Texture2D>("imagenes/BizcochuelosRellenos/vainillaCorazonCorteChantilly"));
            TexturasBizcochueloConRelleno.Add((SaborBizcochuelo.CarameloVainilla, FormaBizcochuelo.Corazon, SaborRelleno.Chocolate), content.Load<Texture2D>("imagenes/BizcochuelosRellenos/vainillaCorazonCorteChocolate"));
            TexturasBizcochueloConRelleno.Add((SaborBizcochuelo.CarameloVainilla, FormaBizcochuelo.Corazon, SaborRelleno.Frutilla), content.Load<Texture2D>("imagenes/BizcochuelosRellenos/vainillaCorazonCorteFrutilla"));
            TexturasBizcochueloConRelleno.Add((SaborBizcochuelo.CarameloVainilla, FormaBizcochuelo.Corazon, SaborRelleno.Limon), content.Load<Texture2D>("imagenes/BizcochuelosRellenos/vainillaCorazonCorteLimon"));

            TexturasBizcochueloConRelleno.Add((SaborBizcochuelo.Chocolate, FormaBizcochuelo.Redondo, SaborRelleno.Chantilly), content.Load<Texture2D>("imagenes/BizcochuelosRellenos/chocolateRedondoCorteChantilly"));
            TexturasBizcochueloConRelleno.Add((SaborBizcochuelo.Chocolate, FormaBizcochuelo.Redondo, SaborRelleno.Chocolate), content.Load<Texture2D>("imagenes/BizcochuelosRellenos/chocolateRedondoCorteChocolate"));
            TexturasBizcochueloConRelleno.Add((SaborBizcochuelo.Chocolate, FormaBizcochuelo.Redondo, SaborRelleno.Frutilla), content.Load<Texture2D>("imagenes/BizcochuelosRellenos/chocolateRedondoCorteFrutilla"));
            TexturasBizcochueloConRelleno.Add((SaborBizcochuelo.Chocolate, FormaBizcochuelo.Redondo, SaborRelleno.Limon), content.Load<Texture2D>("imagenes/BizcochuelosRellenos/chocolateRedondoCorteLimon"));

            TexturasBizcochueloConRelleno.Add((SaborBizcochuelo.Chocolate, FormaBizcochuelo.Cuadrado, SaborRelleno.Chantilly), content.Load<Texture2D>("imagenes/BizcochuelosRellenos/chocolateCuadradoCorteChantilly"));
            TexturasBizcochueloConRelleno.Add((SaborBizcochuelo.Chocolate, FormaBizcochuelo.Cuadrado, SaborRelleno.Chocolate), content.Load<Texture2D>("imagenes/BizcochuelosRellenos/chocolateCuadradoCorteChocolate"));
            TexturasBizcochueloConRelleno.Add((SaborBizcochuelo.Chocolate, FormaBizcochuelo.Cuadrado, SaborRelleno.Frutilla), content.Load<Texture2D>("imagenes/BizcochuelosRellenos/chocolateCuadradoCorteFrutilla"));
            TexturasBizcochueloConRelleno.Add((SaborBizcochuelo.Chocolate, FormaBizcochuelo.Cuadrado, SaborRelleno.Limon), content.Load<Texture2D>("imagenes/BizcochuelosRellenos/chocolateCuadradoCorteLimon"));

            TexturasBizcochueloConRelleno.Add((SaborBizcochuelo.Chocolate, FormaBizcochuelo.Corazon, SaborRelleno.Chantilly), content.Load<Texture2D>("imagenes/BizcochuelosRellenos/chocolateCorazonCorteChantilly"));
            TexturasBizcochueloConRelleno.Add((SaborBizcochuelo.Chocolate, FormaBizcochuelo.Corazon, SaborRelleno.Chocolate), content.Load<Texture2D>("imagenes/BizcochuelosRellenos/chocolateCorazonCorteChocolate"));
            TexturasBizcochueloConRelleno.Add((SaborBizcochuelo.Chocolate, FormaBizcochuelo.Corazon, SaborRelleno.Frutilla), content.Load<Texture2D>("imagenes/BizcochuelosRellenos/chocolateCorazonCorteFrutilla"));
            TexturasBizcochueloConRelleno.Add((SaborBizcochuelo.Chocolate, FormaBizcochuelo.Corazon, SaborRelleno.Limon), content.Load<Texture2D>("imagenes/BizcochuelosRellenos/chocolateCorazonCorteLimon"));

            TexturasBizcochueloConRelleno.Add((SaborBizcochuelo.Frutilla, FormaBizcochuelo.Redondo, SaborRelleno.Chantilly), content.Load<Texture2D>("imagenes/BizcochuelosRellenos/frutillaRedondoCorteChantilly"));
            TexturasBizcochueloConRelleno.Add((SaborBizcochuelo.Frutilla, FormaBizcochuelo.Redondo, SaborRelleno.Chocolate), content.Load<Texture2D>("imagenes/BizcochuelosRellenos/frutillaRedondoCorteChocolate"));
            TexturasBizcochueloConRelleno.Add((SaborBizcochuelo.Frutilla, FormaBizcochuelo.Redondo, SaborRelleno.Frutilla), content.Load<Texture2D>("imagenes/BizcochuelosRellenos/frutillaRedondoCorteFrutilla"));
            TexturasBizcochueloConRelleno.Add((SaborBizcochuelo.Frutilla, FormaBizcochuelo.Redondo, SaborRelleno.Limon), content.Load<Texture2D>("imagenes/BizcochuelosRellenos/frutillaRedondoCorteLimon"));

            TexturasBizcochueloConRelleno.Add((SaborBizcochuelo.Frutilla, FormaBizcochuelo.Cuadrado, SaborRelleno.Chantilly), content.Load<Texture2D>("imagenes/BizcochuelosRellenos/frutillaCuadradoCorteChantilly"));
            TexturasBizcochueloConRelleno.Add((SaborBizcochuelo.Frutilla, FormaBizcochuelo.Cuadrado, SaborRelleno.Chocolate), content.Load<Texture2D>("imagenes/BizcochuelosRellenos/frutillaCuadradoCorteChocolate"));
            TexturasBizcochueloConRelleno.Add((SaborBizcochuelo.Frutilla, FormaBizcochuelo.Cuadrado, SaborRelleno.Frutilla), content.Load<Texture2D>("imagenes/BizcochuelosRellenos/frutillaCuadradoCorteFrutilla"));
            TexturasBizcochueloConRelleno.Add((SaborBizcochuelo.Frutilla, FormaBizcochuelo.Cuadrado, SaborRelleno.Limon), content.Load<Texture2D>("imagenes/BizcochuelosRellenos/frutillaCuadradoCorteLimon"));

            TexturasBizcochueloConRelleno.Add((SaborBizcochuelo.Frutilla, FormaBizcochuelo.Corazon, SaborRelleno.Chantilly), content.Load<Texture2D>("imagenes/BizcochuelosRellenos/frutillaCorazonCorteChantilly"));
            TexturasBizcochueloConRelleno.Add((SaborBizcochuelo.Frutilla, FormaBizcochuelo.Corazon, SaborRelleno.Chocolate), content.Load<Texture2D>("imagenes/BizcochuelosRellenos/frutillaCorazonCorteChocolate"));
            TexturasBizcochueloConRelleno.Add((SaborBizcochuelo.Frutilla, FormaBizcochuelo.Corazon, SaborRelleno.Frutilla), content.Load<Texture2D>("imagenes/BizcochuelosRellenos/frutillaCorazonCorteFrutilla"));
            TexturasBizcochueloConRelleno.Add((SaborBizcochuelo.Frutilla, FormaBizcochuelo.Corazon, SaborRelleno.Limon), content.Load<Texture2D>("imagenes/BizcochuelosRellenos/frutillaCorazonCorteLimon"));

            TexturasBizcochueloConRelleno.Add((SaborBizcochuelo.Arandano, FormaBizcochuelo.Redondo, SaborRelleno.Chantilly), content.Load<Texture2D>("imagenes/BizcochuelosRellenos/arandanoRedondoCorteChantilly"));
            TexturasBizcochueloConRelleno.Add((SaborBizcochuelo.Arandano, FormaBizcochuelo.Redondo, SaborRelleno.Chocolate), content.Load<Texture2D>("imagenes/BizcochuelosRellenos/arandanoRedondoCorteChocolate"));
            TexturasBizcochueloConRelleno.Add((SaborBizcochuelo.Arandano, FormaBizcochuelo.Redondo, SaborRelleno.Frutilla), content.Load<Texture2D>("imagenes/BizcochuelosRellenos/arandanoRedondoCorteFrutilla"));
            TexturasBizcochueloConRelleno.Add((SaborBizcochuelo.Arandano, FormaBizcochuelo.Redondo, SaborRelleno.Limon), content.Load<Texture2D>("imagenes/BizcochuelosRellenos/arandanoRedondoCorteLimon"));

            TexturasBizcochueloConRelleno.Add((SaborBizcochuelo.Arandano, FormaBizcochuelo.Cuadrado, SaborRelleno.Chantilly), content.Load<Texture2D>("imagenes/BizcochuelosRellenos/arandanoCuadradoCorteChantilly"));
            TexturasBizcochueloConRelleno.Add((SaborBizcochuelo.Arandano, FormaBizcochuelo.Cuadrado, SaborRelleno.Chocolate), content.Load<Texture2D>("imagenes/BizcochuelosRellenos/arandanoCuadradoCorteChocolate"));
            TexturasBizcochueloConRelleno.Add((SaborBizcochuelo.Arandano, FormaBizcochuelo.Cuadrado, SaborRelleno.Frutilla), content.Load<Texture2D>("imagenes/BizcochuelosRellenos/arandanoCuadradoCorteFrutilla"));
            TexturasBizcochueloConRelleno.Add((SaborBizcochuelo.Arandano, FormaBizcochuelo.Cuadrado, SaborRelleno.Limon), content.Load<Texture2D>("imagenes/BizcochuelosRellenos/arandanoCuadradoCorteLimon"));

            TexturasBizcochueloConRelleno.Add((SaborBizcochuelo.Arandano, FormaBizcochuelo.Corazon, SaborRelleno.Chantilly), content.Load<Texture2D>("imagenes/BizcochuelosRellenos/arandanoCorazonCorteChantilly"));
            TexturasBizcochueloConRelleno.Add((SaborBizcochuelo.Arandano, FormaBizcochuelo.Corazon, SaborRelleno.Chocolate), content.Load<Texture2D>("imagenes/BizcochuelosRellenos/arandanoCorazonCorteChocolate"));
            TexturasBizcochueloConRelleno.Add((SaborBizcochuelo.Arandano, FormaBizcochuelo.Corazon, SaborRelleno.Frutilla), content.Load<Texture2D>("imagenes/BizcochuelosRellenos/arandanoCorazonCorteFrutilla"));
            TexturasBizcochueloConRelleno.Add((SaborBizcochuelo.Arandano, FormaBizcochuelo.Corazon, SaborRelleno.Limon), content.Load<Texture2D>("imagenes/BizcochuelosRellenos/arandanoCorazonCorteLimon"));

        }
    }
}
