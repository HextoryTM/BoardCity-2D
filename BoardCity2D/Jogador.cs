using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardCity2D
{
    class Jogador : Controlador
    {
        public enum EstadosJogador { Jogando, Preso }

        public override Direcao Sentido { get; set; }
        public Peao.CorPeao CorPeao { get; set; }
        public EstadosJogador Estado { get; set; }
        public int Dinheiro { get; set; }
        public int RodadasPreso { get; set; }

        public Controlador playControl;

        public Jogador(Peao.CorPeao cor, Controlador control)
        {
            RodadasPreso = 0;
            Estado = EstadosJogador.Jogando;
            Dinheiro = 2500;
            CorPeao = cor;
            playControl = control;
        }

        public override int Jogar(Peao peao, Dado dado, Tabuleiro tab)
        {
            return playControl.Jogar(peao, dado, tab);
        }

        public override void Mover(Peao peao, Tabuleiro tab)
        {
            playControl.Mover(peao, tab);
        }

        public override int JogarPrisao(Peao peao, Dado dado, Tabuleiro tab)
        {
            return playControl.JogarPrisao(peao, dado, tab);
        }
    }
}
