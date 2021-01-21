using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardCity2D
{
    class DrawObjetos
    {
        public static void DrawBotao(Botao btn, SpriteBatch sp, Texture2D textura, SpriteFont fonte)
        {
            Rectangle rect = new Rectangle(btn.X, btn.Y, btn.Largura, btn.Altura);

            Vector2 size = fonte.MeasureString(btn.Texto); //Definir o tamanho do texto
            Vector2 posTexto = new Vector2(btn.X + ((btn.Largura / 2) - (size.X / 2)), btn.Y + ((btn.Altura / 2) - (size.Y / 2)));
            Color cor = btn.Cor;

            if (btn.Clicado == true) { cor = Color.Gray; }

            sp.Draw(textura, rect, cor);
            sp.DrawString(fonte, btn.Texto, posTexto, Color.White);
        }

        public static void DrawCasa(Casa c, SpriteBatch sp, Texture2D img)
        {
            int Alt = 86;
            int Larg = 86;
            int deslocX = 10 + 50;
            int deslocY = 10 + 50;

            int posX = deslocX + c.X * Larg;
            int posY = deslocY + c.Y * Alt;

            Rectangle rect = new Rectangle(posX, posY, Larg, Alt);
            Color corCasa = Color.White;

            if (c.Cor == Casa.CorCasa.Vermelha)
            {
                corCasa = Color.Red;
            }
            else if (c.Cor == Casa.CorCasa.Azul)
            {
                corCasa = Color.Blue;
            }
            else if (c.Cor == Casa.CorCasa.Verde)
            {
                corCasa = Color.Green;
            }
            else if (c.Cor == Casa.CorCasa.Amarela)
            {
                corCasa = Color.Yellow;
            }
            else if (c.Cor == Casa.CorCasa.Branca)
            {
                corCasa = Color.White;
            }

            sp.Draw(img, rect, corCasa);
        }

        public static void DrawPeao(Peao p, SpriteBatch sp, Texture2D img)
        {
            int Alt = 50;
            int Larg = 50;
            int deslocX = 50 + 50;
            int deslocY = 50 + 50;

            int posX = deslocX + p.X * (Larg + 31);
            int posY = deslocY + p.Y * (Alt + 30);

            DrawPeaoConfig(p, posX, posY, Larg, Alt, sp, img);
        }

        public static void DrawTabuleiro(Tabuleiro tab, SpriteBatch sp, Texture2D img)
        {
            #region Casas Especiais
            Casa e1 = tab.PainelCasas[tab.Linhas - 1, 0]; //Inferior Esquerdo
            e1 = new Casa(0, tab.Linhas - 1, Casa.CorCasa.Branca, new Imovel(0, tab.Linhas - 1, 0, 0));
            e1.Propriedade.Especial = 1;
            tab.PainelCasas[tab.Linhas - 1, 0] = e1;
            DrawObjetos.DrawCasa(e1, sp, img);
            
            Casa e2 = tab.PainelCasas[0, 0]; //Superior Esquerdo
            e2 = new Casa(0, 0, Casa.CorCasa.Branca, new Imovel(0, 0, 0, 0));
            e2.Propriedade.Especial = 2;
            tab.PainelCasas[0, 0] = e2;
            DrawObjetos.DrawCasa(e2, sp, img);
            
            Casa e3 = tab.PainelCasas[0, tab.Colunas - 1]; //Superior Direito
            e3 = new Casa(tab.Colunas - 1, 0, Casa.CorCasa.Branca, new Imovel(tab.Colunas - 1, 0, 0, 0));
            e3.Propriedade.Especial = 3;
            tab.PainelCasas[0, tab.Colunas - 1] = e3;
            DrawObjetos.DrawCasa(e3, sp, img);
            
            Casa e4 = tab.PainelCasas[tab.Linhas - 1, tab.Colunas - 1]; //Inferior Direito
            e4 = new Casa(tab.Colunas - 1, tab.Linhas - 1, Casa.CorCasa.Branca, new Imovel(tab.Colunas - 1, tab.Linhas - 1, 0, 0));
            e4.Propriedade.Especial = 4;
            tab.PainelCasas[tab.Linhas - 1, tab.Colunas - 1] = e4;
            DrawObjetos.DrawCasa(e4, sp, img);
            #endregion

            for (int lin = tab.Linhas - 2, adicional = 1; lin > 0; lin--, adicional++) //Casas Rua 1 (VERDE)
            {
                int col = 0;
                Casa c = tab.PainelCasas[lin, col];
                if (c != null)
                {
                    DrawObjetos.DrawCasa(c, sp, img);
                }
                else
                {
                    c = new Casa(col, lin, Casa.CorCasa.Verde, new Imovel(col, lin, 200 + adicional * 10, 100 + adicional * 10));
                    tab.PainelCasas[lin, col] = c;
                    DrawObjetos.DrawCasa(c, sp, img);
                }
            }

            for (int col = 1, adicional = 1; col < tab.Colunas - 1; col++, adicional++) //Casas Rua 2 (AMARELA)
            {
                int lin = 0;
                Casa c = tab.PainelCasas[lin, col];
                if (c != null)
                {
                    DrawObjetos.DrawCasa(c, sp, img);
                }
                else
                {
                    c = new Casa(col, lin, Casa.CorCasa.Amarela, new Imovel(col, lin, 300 + adicional * 10, 150 + adicional * 10));
                    tab.PainelCasas[lin, col] = c;
                    DrawObjetos.DrawCasa(c, sp, img);
                }
            }

            for (int lin = 1, adicional = 1; lin < tab.Linhas - 1; lin++, adicional++) //Casas Rua 3 (AZUL)
            {
                int col = tab.Colunas - 1;
                Casa c = tab.PainelCasas[lin, col];
                if (c != null)
                {
                    DrawObjetos.DrawCasa(c, sp, img);
                }
                else
                {
                    c = new Casa(col, lin, Casa.CorCasa.Azul, new Imovel(col, lin, 400 + adicional * 10, 200 + adicional * 10));
                    tab.PainelCasas[lin, col] = c;
                    DrawObjetos.DrawCasa(c, sp, img);
                }
            }

            for (int col = tab.Colunas - 2, adicional = 1; col > 0; col--, adicional++) //Casas Rua 4 (VERMELHA)
            {
                int lin = tab.Linhas - 1;
                Casa c = tab.PainelCasas[lin, col];
                if (c != null)
                {
                    DrawObjetos.DrawCasa(c, sp, img);
                }
                else
                {
                    c = new Casa(col, lin, Casa.CorCasa.Vermelha, new Imovel(col, lin, 500 + adicional * 10, 250 + adicional * 10));
                    tab.PainelCasas[lin, col] = c;
                    DrawObjetos.DrawCasa(c, sp, img);
                }
            }
        }

        public static void DrawDado(SpriteBatch sp, Texture2D[] imgDados, int nmro)
        {
            int Alt = 60;
            int Larg = 60;


            int posX = 395 - Larg;
            int posY = 400 - Alt;

            Rectangle rectDado = new Rectangle(posX, posY, Larg, Alt);

            sp.Draw(imgDados[nmro], rectDado, Color.White);
        }

        public static void DrawPeaoPreso(Peao p, SpriteBatch sp, Texture2D img, Texture2D imgCela)
        {
            int Alt = 50;
            int Larg = 50;
            int deslocX = 23 + 50;
            int deslocY = 22 + 50;

            int posX = deslocX + p.X * (Larg + 31);
            int posY = deslocY + p.Y * (Alt + 30);

            DrawPeaoConfig(p, posX, posY, Larg, Alt, sp, img);

            Larg = 55;
            deslocX = 15 + 50;
            deslocY = 15 + 50;
            posX = deslocX + p.X * (Larg + 31);
            posY = deslocY + p.Y * (Alt + 30);
            Rectangle rectCela = new Rectangle(posX, posY, Larg + 10, Alt + 10);

            sp.Draw(imgCela, rectCela, Color.White);
        }

        public static void DrawCela(SpriteBatch sp, Texture2D img)
        {
            int Alt = 50;
            int Larg = 55;
            int deslocX = 15 + 50;
            int deslocY = 15 + 50;

            int posX = deslocX * (Larg + 31);
            int posY = deslocY * (Alt + 30);

            posX = deslocX + 0 * (Larg + 31);
            posY = deslocY + 0 * (Alt + 30);
            Rectangle rectCela = new Rectangle(posX, posY, Larg + 10, Alt + 10);

            sp.Draw(img, rectCela, Color.White);
        }

        private static void DrawPeaoConfig(Peao p, int posX, int posY, int Larg, int Alt, SpriteBatch sp, Texture2D img)
        {
            Rectangle rect = new Rectangle(posX, posY, Larg, Alt);
            Color corCasa = Color.White;

            if (p.Cor == Peao.CorPeao.Vermelha)
            {
                corCasa = Color.Red;
            }
            else if (p.Cor == Peao.CorPeao.Azul)
            {
                corCasa = Color.Blue;
            }
            else if (p.Cor == Peao.CorPeao.Verde)
            {
                corCasa = Color.Green;
            }
            else if (p.Cor == Peao.CorPeao.Amarela)
            {
                corCasa = Color.Yellow;
            }
            else if (p.Cor == Peao.CorPeao.Branca)
            {
                corCasa = Color.White;
            }

            sp.Draw(img, rect, corCasa);
        }

        public static void DrawDinheiro(Botao btn, SpriteBatch sp, Texture2D textura, SpriteFont fonte)
        {
            Rectangle rect = new Rectangle(btn.X, btn.Y, btn.Largura, btn.Altura);

            Vector2 size = fonte.MeasureString(btn.Texto); //Definir o tamanho do texto
            Vector2 posTexto = new Vector2(btn.X + ((btn.Largura / 2) - (size.X / 2)), btn.Y + ((btn.Altura / 2) - (size.Y / 2)));
            Color cor = btn.Cor;

            if (btn.Clicado == true) { cor = Color.Gray; }

            sp.Draw(textura, rect, cor);
            sp.DrawString(fonte, btn.Texto, posTexto, Color.Black);
        }

        public static void DrawCredito(Credito credito, SpriteBatch sp, SpriteFont fonte, GraphicsDeviceManager graphics, int animacaoInt)
        {
            Vector2 size;
            int Largura = graphics.GraphicsDevice.Viewport.Width;

            #region Valor animacaoInt
            //Regiao usada pra descobrir o valor atual da variavel "animacaoInt"

            /*
            Vector2 log = new Vector2(0, 0);
            string logAnim = Convert.ToString(animacaoInt);
            sp.DrawString(fonte, logAnim, log, Color.White);
            */
            #endregion

            //posCD
            size = fonte.MeasureString(credito.CD);
            Vector2 posCD = new Vector2(0, 0);
            posCD.X = (Largura / 2) - (size.X / 2);
            posCD.Y = animacaoInt;

            //posNome
            size = fonte.MeasureString(credito.Nome);
            Vector2 posNome = new Vector2(0, 0);
            posNome.X = (Largura / 2) - (size.X / 2);
            posNome.Y = posCD.Y + 20;

            //posRA
            size = fonte.MeasureString(credito.RA);
            Vector2 posRA = new Vector2(0, 0);
            posRA.X = (Largura / 2) - (size.X / 2);
            posRA.Y = posNome.Y + 20;

            //posEmail
            size = fonte.MeasureString(credito.Email);
            Vector2 posEmail = new Vector2(0, 0);
            posEmail.X = (Largura / 2) - (size.X / 2);
            posEmail.Y = posRA.Y + 20;

            //Foto
            Rectangle rectFoto = new Rectangle((Largura / 2) - (130 / 2), 0, 130, 158);
            rectFoto.Y = (int)posEmail.Y + 60;

            sp.DrawString(fonte, credito.CD, posCD, Color.Black);
            sp.DrawString(fonte, credito.Nome, posNome, Color.Black);
            sp.DrawString(fonte, credito.RA, posRA, Color.Black);
            sp.DrawString(fonte, credito.Email, posEmail, Color.Black);
            sp.Draw(credito.Foto, rectFoto, Color.White);
        }

        public static void DrawAjuda(Ajuda ajuda, SpriteBatch sp, SpriteFont fonte, GraphicsDeviceManager graphics)
        {
            int Largura = graphics.GraphicsDevice.Viewport.Width;
            int Altura = graphics.GraphicsDevice.Viewport.Height;

            //Fotos
            Rectangle[] rectPaginas;  //110 é o valor da largura da carta + 10
            rectPaginas = new Rectangle[ajuda.NumPaginas];
            rectPaginas[ajuda.PaginaAtual] = new Rectangle((Largura / 2) - 250, (Altura / 2) - 350, 500, 700);

            sp.Draw(ajuda.PaginasAjuda[ajuda.PaginaAtual], rectPaginas[ajuda.PaginaAtual], Color.White);

            //for (int i = 0; i < ajuda.NumPaginas; i++)
            //{
            //    rectPaginas[i] = new Rectangle((Largura / 2) - 250, (Altura / 2) - 350, 500, 700);
            //    sp.Draw(ajuda.PaginasAjuda[i], rectPaginas[i], Color.White);
            //}

            DrawObjetos.DrawBotao(ajuda.BotaoPagAnt, sp, ajuda.ImgBotao1, fonte);
            DrawObjetos.DrawBotao(ajuda.BotaoPagProx, sp, ajuda.ImgBotao, fonte);
        }

        public static void DrawPeaoMenu(Peao p, SpriteBatch sp, Texture2D img, int posX, int posY)
        {
            int Alt = 30;
            int Larg = 30;

            DrawPeaoConfig(p, posX, posY, Larg, Alt, sp, img);
        }

        public static void DrawPropaganda(Propaganda propaganda, SpriteBatch sp, GraphicsDeviceManager graphics, SpriteFont fonte)
        {
            int Largura = graphics.GraphicsDevice.Viewport.Width;
            int Altura = graphics.GraphicsDevice.Viewport.Height;

            string temp = propaganda.Temporizador.ToString(@"ss");
            Rectangle rectPropaganda = new Rectangle((Largura / 2) - 250, (Altura / 2) - 250, 500, 500);
            Vector2 posTemp = new Vector2(rectPropaganda.X + rectPropaganda.Width - 50, rectPropaganda.Y + 30);

            sp.Draw(propaganda.ImgPropaganda, rectPropaganda, Color.White);
            sp.DrawString(fonte, "!!! PROPAGANDA !!!", new Vector2(rectPropaganda.X + rectPropaganda.Width / 2 - 100, rectPropaganda.Y + rectPropaganda.Height / 2 - 25), Color.Black);
            sp.DrawString(fonte, temp, posTemp, Color.Black);

            propaganda.BotaoX.X = (int)posTemp.X - 25;
            propaganda.BotaoX.Y = (int)posTemp.Y - 10;

            if (propaganda.Temporizador < propaganda.FimTempo)
            {
                DrawObjetos.DrawBotao(propaganda.BotaoX, sp, propaganda.ImgBotao, fonte);
            }
        }
    }
}


