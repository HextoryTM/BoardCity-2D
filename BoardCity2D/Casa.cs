using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardCity2D
{
    class Casa
    {
        public enum CorCasa { Vermelha, Azul, Verde, Amarela, Branca}

        public int X { get; set; }
        public int Y { get; set; }
        public CorCasa Cor { get; set; }
        public Imovel Propriedade { get; set; }
        public List<Peao> ListaPeoes;

        public Casa(int posX, int posY, CorCasa corCasa, Imovel prop)
        {
            ListaPeoes = new List<Peao>();
            X = posX;
            Y = posY;
            Cor = corCasa;
            Propriedade = prop;
        }
    }
}
