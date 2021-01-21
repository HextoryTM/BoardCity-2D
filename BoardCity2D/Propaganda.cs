using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardCity2D
{
    class Propaganda
    {
        public TimeSpan Temporizador { get; set; }
        public TimeSpan FimTempo { get; set; }
        public Texture2D ImgPropaganda { get; }
        public Texture2D ImgBotao { get; }
        public Botao BotaoX { get; set; }

        public Propaganda(Texture2D imgPropaganda, Texture2D imgBotao, Botao botaoX, TimeSpan temporizador, TimeSpan fimTempo)
        {
            FimTempo = fimTempo;
            Temporizador = temporizador;
            BotaoX = botaoX;
            ImgPropaganda = imgPropaganda;
            ImgBotao = imgBotao;
        }
    }
}
