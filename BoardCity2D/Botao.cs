using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardCity2D
{
    class Botao
    {
        #region PROPRIEDADES
        public Int32 X { get; set; }
        public Int32 Y { get; set; }
        public Int32 Largura { get; set; }
        public Int32 Altura { get; set; }
        public Color Cor { get; set; }
        public String Texto { get; set; }
        public bool Clicado { get; set; }
        public Texture2D Img { get; set; }
        #endregion

        #region CONSTRUTORES
        public Botao(Int32 x1, Int32 y1, String txt)
        {
            this.X = x1;
            this.Y = y1;
            this.Texto = txt;
            this.Largura = 120;
            this.Altura = 50;
            this.Cor = Color.White;
            this.Clicado = false;
        }

        public Botao(Int32 x1, Int32 y1, String txt, Int32 Larg, Int32 Alt, Color cor)
        {
            this.X = x1;
            this.Y = y1;
            this.Texto = txt;
            this.Largura = Larg;
            this.Altura = Alt;
            this.Cor = cor;
            this.Clicado = false;
        }

        public Botao(Int32 x1, Int32 y1, String txt, Color cor)
        {
            this.X = x1;
            this.Y = y1;
            this.Texto = txt;
            this.Largura = 120;
            this.Altura = 50;
            this.Cor = cor;
            this.Clicado = false;
        }

        #endregion

        #region MÉTODOS
        public void VerificarClique(Int32 x1, Int32 y1)
        {
            if (x1 > X && x1 < (X + Largura) &&
                y1 > Y && y1 < (Y + Altura))
            {
                Clicado = !Clicado;
            }
        }
        #endregion
    }
}