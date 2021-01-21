using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardCity2D
{
    class Ajuda
    {
        public int NumPaginas { get;}
        public Texture2D[] PaginasAjuda { get; }
        public Botao BotaoPagAnt { get; set; }
        public Botao BotaoPagProx { get; set; }
        public Texture2D ImgBotao { get; }
        public Texture2D ImgBotao1 { get; }
        public int PaginaAtual { get; }

        public Ajuda(Texture2D[] paginas, Botao botaoPagAnt, Botao botaoPagProx, Texture2D imgBotao, Texture2D imgBotao1, int paginaAtual)
        {
            PaginaAtual = paginaAtual;
            PaginasAjuda = paginas;
            BotaoPagAnt = botaoPagAnt;
            BotaoPagProx = botaoPagProx;
            NumPaginas = paginas.Length;
            ImgBotao = imgBotao;
            ImgBotao1 = imgBotao1;
        }
    }
}
