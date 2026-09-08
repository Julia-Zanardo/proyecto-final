using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JulpajulparaisoPasteleria.Content
{
    public class AdaptadorDeResolucion
    {
        private int anchoVirtual;
        private int altoVirtual;
        private int anchoReal;
        private int altoReal;
        private int margenIzquierdo;
        private int margenSuperior;
        public Matrix MatrizDeTransformacion { get; private set; } //convertir coordenadas virtuales a coordenadas de pantalla
        private Matrix matrizInversa; // convertir coordenadas de pantalla a coordenadas virtuales
        public Viewport AreaDeDibujo { get; private set; }// Viewport representa un área rectangular en la pantalla

        public AdaptadorDeResolucion(int anchoVirtual, int altoVirtual)
        {
            this.anchoVirtual = anchoVirtual;
            this.altoVirtual = altoVirtual;
        }
        public void Actualizar(int anchoReal, int altoReal)
        {
            this.anchoReal = anchoReal;
            this.altoReal = altoReal;
            float escalaX = (float)anchoReal / anchoVirtual;
            float escalaY = (float)altoReal / altoVirtual;
            float escala = Math.Min(escalaX, escalaY);
            int anchoUtil = (int)(anchoVirtual * escala);
            int altoUtil = (int)(altoVirtual * escala);
            margenIzquierdo = (anchoReal - anchoUtil) / 2;
            margenSuperior = (altoReal - altoUtil) / 2;
            MatrizDeTransformacion = Matrix.CreateScale(escala, escala, 1f) * Matrix.CreateTranslation(margenIzquierdo, margenSuperior, 0f);
            matrizInversa = Matrix.Invert(MatrizDeTransformacion);
            AreaDeDibujo = new Viewport(margenIzquierdo, margenSuperior, anchoUtil, altoUtil);
        }
        public Vector2 AjustarCoordenada(Vector2 posicionPantalla)
        {
            return Vector2.Transform(posicionPantalla, matrizInversa);
        }
    }
}
