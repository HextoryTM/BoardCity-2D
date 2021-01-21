using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardCity2D
{
    class Peao
    {
        public enum CorPeao { Branca, Vermelha, Verde, Azul, Amarela}

        public int X { get; set; }
        public int Y { get; set; }
        public CorPeao Cor { get; set; }

        public Peao(int posX, int posY, CorPeao corPeao)
        {
            X = posX;
            Y = posY;
            Cor = corPeao;
        }
    }
}
