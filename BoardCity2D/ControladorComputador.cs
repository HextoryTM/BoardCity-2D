using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BoardCity2D
{
    class ControladorComputador : Controlador
    {
        public override Direcao Sentido { get; set; }

        public override int Jogar(Peao peao, Dado dado, Tabuleiro tab)
        {
            int resultadoDado = dado.Rodar();
            int contador = resultadoDado;

            Mover(peao, tab);
            while (resultadoDado != 0)
            {
                Mover(peao, tab);
                resultadoDado--;
            }

            return contador;
        }

        public override int JogarPrisao(Peao peao, Dado dado, Tabuleiro tab)
        {
            throw new NotImplementedException();
        }

        public override void Mover(Peao peao, Tabuleiro tab)
        {
            if (Sentido == Direcao.Norte)
            {
                if (peao.Y == 1 && peao.X == 0) { Sentido = Direcao.Leste; }
                peao.Y -= 1;
            }

            else if (Sentido == Direcao.Leste)
            {
                if (peao.Y == 0 && peao.X == tab.Colunas - 2)
                { Sentido = Direcao.Sul; }
                peao.X += 1;
            }

            else if (Sentido == Direcao.Sul)
            {
                if (peao.Y == tab.Linhas - 2 && peao.X == tab.Colunas - 1) { Sentido = Direcao.Oeste; }
                peao.Y += 1;
            }

            else if (Sentido == Direcao.Oeste)
            {
                if (peao.Y == tab.Linhas - 1 && peao.X == 1) { Sentido = Direcao.Norte; }
                peao.X -= 1;
            }
        }
    }
}
