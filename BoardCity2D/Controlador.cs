using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardCity2D
{
    abstract class Controlador
    {
        public enum Direcao { Norte, Sul, Leste, Oeste }
        public abstract Direcao Sentido { get; set; }

        public abstract int Jogar(Peao peao, Dado dado, Tabuleiro tab);
        public abstract void Mover(Peao peao, Tabuleiro tab);
        public abstract int JogarPrisao(Peao peao, Dado dado, Tabuleiro tab);
    }
}
