using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardCity2D
{
    class Tabuleiro
    {
        public int Linhas { get; }
        public int Colunas { get; }
        public Casa[,] PainelCasas { get; }
        private int[] alturaDaLinha;

        public Tabuleiro(int lin, int col)
        {
            Linhas = lin;
            Colunas = col;
            PainelCasas = new Casa[Linhas, Colunas];

            alturaDaLinha = new int[col];
            for (int i = 0; i < col; i++)
            {
                alturaDaLinha[i] = 0;
            }
        }
    }
}
