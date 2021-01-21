using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardCity2D
{
    class Imovel
    {
        public enum EstadosImovel { Venda, Comprado}

        public int Coluna { get; set; }
        public int Linha { get; set; }
        public int Preco { get; set; }
        public int Aluguel { get; set; }
        public EstadosImovel Estado { get; set; }
        public Peao Comprador { get; set; }
        public int Especial { get; set; }

        public Imovel (int posCol, int posLin, int preco, int aluguel)
        {
            Especial = 0;
            Comprador = null;
            Estado = EstadosImovel.Venda;
            Coluna = posCol;
            Linha = posLin;
            Preco = preco;
            Aluguel = aluguel;
        }
    }
}
