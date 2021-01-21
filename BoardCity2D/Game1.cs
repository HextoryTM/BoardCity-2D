using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Threading;

namespace BoardCity2D
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        enum EstadoJogo { Menu, Jogar, Ajuda, Credito, Sair};
        EstadoJogo estado;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        int LarguraTela;
        int AlturaTela;
        bool novoJogo;

        //Ajuda
        Ajuda ajuda;

        //Creditos
        Credito credito;
        Texture2D Foto;

        #region TabuleiroBG e BG
        Rectangle rectOverlap;
        Texture2D imgOverlap;
        Rectangle rectTabBG;
        Texture2D imgTabBG;
        Rectangle rectBg;
        Texture2D imgBg;
        Texture2D imgBg1;
        #endregion

        #region Jogadores
        Jogador[] Jogador;
        int numJogadores;
        int maxJogadores;
        int jogadorAtual;
        int proxJogador;
        #endregion
        bool ComputadorOn;

        #region Dinheiro
        string[] dinheiroJog;
        Vector2[] posDinheiro;
        Botao[] botaoDinheiroJog;
        #endregion

        Peao[] peoes;
        Peao peao1;
        Casa casaAtual;
        Jogador jogadorAnimacao;
        bool fimAnimacao;

        #region fimDeJogo
        int fimDeJogo;
        string msgFimDeJogo;
        Vector2 posMsgFimDeJogo;
        #endregion

        Dado dado;
        int resultadoDado;
        bool comecarRemover;

        Tabuleiro tab;

        #region Texturas
        Texture2D imgCasa;
        Texture2D imgPeao;
        Texture2D[] imgDados;
        Texture2D imgBotao;
        Texture2D imgCela;
        Texture2D imgDinheiro;
        Texture2D[] paginasAjuda;
        Texture2D imgBotaoAjuda;
        Texture2D imgBotaoAjuda1;
        Texture2D imgBotaoMais;
        Texture2D imgBotaoMenos;
        Texture2D imgBotaoGirar;
        Texture2D imgBotaoX;
        Texture2D imgPropaganda;
        Texture2D configNovoJogo;
        #endregion
        int numPaginasAjuda;
        int paginaAtual;

        //Config NJ
        Rectangle rectConfig;
        string configNJ;
        Peao.CorPeao[] corConfig;
        int[] contaCor;
        bool verificarJogar;

        List<Casa> Compradas;

        #region Botoes
        //Estado Menu
        Botao botaoNovoJogo;
        Botao botaoIniciarNJ;
        Botao botaoCancelarNJ;
        Botao botaoMais;
        Botao botaoMenos;
        Botao[] botaoGirar;
        Botao botaoJogar;
        Botao botaoAjuda;
        Botao botaoCredito;
        Botao botaoSair;

        //Estado Jogar
        Botao botaoDado;
        Botao botaoCompraS;
        Botao botaoCompraN;
        Botao botaoOK;

        //BotaoEstadoSair
        Vector2 posConfirmarMsg;
        Botao botaoConfirmarSim;
        Botao botaoConfirmarNao;

        //BotaoGeral
        Botao botaoVoltar;
        Botao botaoX;

        //Ajuda
        Botao botaoPagAnt;
        Botao botaoPagProx;
        #endregion

        #region Compra / Aluguel
        Vector2 size;
        string msgAluguel;
        string msgCompra;
        Rectangle rectMsgCompra;
        Vector2 posMsgCompra;
        Vector2 posMsgAluguel;

        string precoCasa;
        string valorAluguel;
        #endregion

        SpriteFont fonte;
        SpriteFont fonte1;
        int tempoPausaInteracao;
        bool interagindoCompra;
        bool interagindoAluguel;
        int animacaoInt;

        TimeSpan temporizador;
        TimeSpan fimTempo;
        Propaganda propaganda;
        bool fechaPropaganda;

        bool mouseClicked;
        bool lMousePressed;
        Vector2 posMouse;
        MouseState meuMouse;

        Vector2 posMsg;
        Color cor;
        String msg;
        string msgCor;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = 900;
            graphics.PreferredBackBufferHeight = 900;
            graphics.ApplyChanges();
            LarguraTela = GraphicsDevice.Viewport.Width;
            AlturaTela = GraphicsDevice.Viewport.Height;

            rectOverlap = new Rectangle(31, 31, 668, 668);
            rectTabBG = new Rectangle(50, 50, 630, 630);
            rectBg = new Rectangle(0, 0, LarguraTela, AlturaTela);

            // TODO: Add your initialization logic here
            tab = new Tabuleiro(7, 7);
            tempoPausaInteracao = 0;
            estado = EstadoJogo.Menu;
            novoJogo = false;
            rectConfig = new Rectangle(LarguraTela / 2 - 200, AlturaTela / 2 - 200, 400, 400);
            verificarJogar = false;

            #region Temporizador
            fimTempo = TimeSpan.FromSeconds(0);
            temporizador = TimeSpan.FromSeconds(6);
            fechaPropaganda = false;
            #endregion

            #region Iniciar FimDeJogo
            fimDeJogo = 0;
            msgFimDeJogo = "";
            posMsgFimDeJogo = new Vector2(0, 0);
            #endregion

            #region Iniciar Aluguel/Compra
            msgAluguel = "";
            msgCompra = "";
            size = new Vector2(0,0);
            rectMsgCompra = new Rectangle(LarguraTela / 2 - 125, AlturaTela / 2 - 150, 400, 250);
            posMsgAluguel = new Vector2(0, 0);
            posMsgCompra = new Vector2(0, 0);
            #endregion

            #region Iniciar Botoes
            //Estado Menu
            botaoNovoJogo = new Botao((LarguraTela / 2) - 60, 350, "NOVO JOGO");
            botaoIniciarNJ = new Botao(rectConfig.X + rectConfig.Width / 2 + 30, rectConfig.Y + rectConfig.Height - 60, "INICIAR");
            botaoCancelarNJ = new Botao(rectConfig.X + rectConfig.Width / 2 - 150, rectConfig.Y + rectConfig.Height - 60, "CANCELAR");
            botaoMais = new Botao((rectConfig.X + rectConfig.Width) - 70, rectConfig.Y + 30, "", 50, 50, Color.Gray);
            botaoMenos = new Botao((rectConfig.X + rectConfig.Width) - 120, rectConfig.Y + 30, "", 50, 50, Color.Gray);
            botaoJogar = new Botao((LarguraTela / 2) - 60, botaoNovoJogo.Y + 50, "JOGAR", Color.Gray);
            botaoAjuda = new Botao((LarguraTela / 2) - 60, botaoJogar.Y + 50, "AJUDA");
            botaoCredito = new Botao((LarguraTela / 2) - 60, botaoAjuda.Y + 50, "SOBRE");
            botaoSair = new Botao((LarguraTela / 2) - 60, botaoCredito.Y + 50, "SAIR");

            //Estado Jogar
            botaoDado = new Botao(rectTabBG.X + (rectTabBG.Width / 2) - 60, rectTabBG.Y + (rectTabBG.Height / 2) + 80 - 15, "RODAR");
            botaoCompraS = new Botao(rectMsgCompra.X + (rectMsgCompra.Width / 2 - 120), rectMsgCompra.Y + (rectMsgCompra.Height - 50), "SIM");
            botaoCompraN = new Botao(rectMsgCompra.X + (rectMsgCompra.Width / 2), rectMsgCompra.Y + (rectMsgCompra.Height - 50), "NAO");
            botaoOK = new Botao(rectMsgCompra.X + (rectMsgCompra.Width / 2) - 60, rectMsgCompra.Y + (rectMsgCompra.Height - 50), "OK");

            //Botao Geral
            botaoVoltar = new Botao(750, 700, "VOLTAR");
            botaoX = new Botao(0, 0, "", 50, 50, Color.White);

            //Ajuda
            botaoPagAnt = new Botao(200 + 60, 720, "", Color.Gray);
            botaoPagProx = new Botao(200 + 500 - 120 - 60, 720, "", Color.Gray);
            #endregion

            #region Iniciar Jogadores
            numJogadores = 1;
            maxJogadores = 3;

            #region BotaoMenu
            botaoGirar = new Botao[maxJogadores];
            #endregion
            Jogador = new Jogador[maxJogadores];
            peoes = new Peao[maxJogadores];
            corConfig = new Peao.CorPeao[5];
            posDinheiro = new Vector2[maxJogadores];
            botaoDinheiroJog = new Botao[maxJogadores];
            dinheiroJog = new string[maxJogadores];
            contaCor = new int[maxJogadores];

            ComputadorOn = false;

            corConfig[0] = Peao.CorPeao.Branca;
            corConfig[1] = Peao.CorPeao.Vermelha;
            corConfig[2] = Peao.CorPeao.Verde;
            corConfig[3] = Peao.CorPeao.Azul;
            corConfig[4] = Peao.CorPeao.Amarela;

            if (ComputadorOn == false)
            {
                for(int i = 0; i < maxJogadores; i++)
                {
                    contaCor[i] = i;
                    posDinheiro[i] = new Vector2(50, AlturaTela - 180);
                    botaoDinheiroJog[i] = new Botao((int)posDinheiro[i].X, (int)posDinheiro[i].Y, dinheiroJog[i], 0, 0, Color.White);
                    Jogador[i] = new Jogador(corConfig[contaCor[i]], new ControladorMouse());
                    peoes[i] = new Peao(0, tab.Linhas - 1, Jogador[i].CorPeao);
                    dinheiroJog[i] = string.Format("DINHEIRO JOGADOR " + (i + 1) + ": R$ {0}", Jogador[i].Dinheiro);
                }
            }
            else
            {
                //ComputadorOn == true
            }
            jogadorAtual = 0;
            proxJogador = (jogadorAtual + 1) % numJogadores;

            #endregion

            #region Estado Jogar
            comecarRemover = false;
            fimAnimacao = false;
            interagindoCompra = false;
            interagindoAluguel = false;

            casaAtual = tab.PainelCasas[peoes[jogadorAtual].Y, peoes[jogadorAtual].X];
            Compradas = new List<Casa>();

            resultadoDado = 0;
            imgDados = new Texture2D[6];
            dado = new Dado();
            #endregion

            #region Estado Credito e Ajuda
            numPaginasAjuda = 9;
            paginaAtual = 0;
            animacaoInt = AlturaTela + 10;

            paginasAjuda = new Texture2D[numPaginasAjuda];
            #endregion

            mouseClicked = false;
            this.IsMouseVisible = true;
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            spriteBatch = new SpriteBatch(GraphicsDevice);

            #region Carregar Textura
            //imgCasa = Content.Load<Texture2D>("CasaP");
            imgCasa = Content.Load<Texture2D>("imgCasa");
            imgPeao = Content.Load<Texture2D>("Peao");
            imgBotao = Content.Load<Texture2D>("botao");
            fonte = Content.Load<SpriteFont>("fonte");
            fonte1 = Content.Load<SpriteFont>("fonte1");
            imgTabBG = Content.Load<Texture2D>("imgBG");
            imgOverlap = Content.Load<Texture2D>("overlapPreco");
            imgCela = Content.Load<Texture2D>("imgCela");
            imgBg = Content.Load<Texture2D>("bg");
            imgBg1 = Content.Load<Texture2D>("imgBG1");
            imgDinheiro = Content.Load<Texture2D>("imgDinheiro");
            Foto = Content.Load<Texture2D>("foto");
            imgBotaoAjuda = Content.Load<Texture2D>("imgBotaoAjuda");
            imgBotaoAjuda1 = Content.Load<Texture2D>("imgBotaoAjuda1");
            imgBotaoMais = Content.Load<Texture2D>("botaoMais");
            imgBotaoMenos = Content.Load<Texture2D>("botaoMenos");
            imgBotaoGirar = Content.Load<Texture2D>("botaoGirar");
            imgBotaoX = Content.Load<Texture2D>("botaoX");
            imgPropaganda = Content.Load<Texture2D>("imgPropaganda");
            configNovoJogo = Content.Load<Texture2D>("configNovoJogo");

            for (int i = 0; i < numPaginasAjuda; i++)
            {
                paginasAjuda[i] = Content.Load<Texture2D>("paginaAjuda" + (i + 1));
            }
            #endregion

            #region Carregar Dados
            for (int i = 0; i < 6; i++)
            {
                imgDados[i] = Content.Load<Texture2D>("dado" + (i + 1));
            }
            #endregion
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            
            if (estado == EstadoJogo.Menu)
            {
                #region Estado Menu
                VerificarCliqueMouse();

                #region CorBotao +-
                if (numJogadores == maxJogadores)
                {
                    botaoMenos.Cor = Color.White;
                    botaoMais.Cor = Color.Gray;

                }
                else if (numJogadores > 1 && numJogadores < maxJogadores)
                {
                    botaoMais.Cor = Color.White;
                    botaoMenos.Cor = Color.White;
                }
                else
                {
                    botaoMenos.Cor = Color.Gray;
                    botaoMais.Cor = Color.White;
                }
                #endregion

                if (mouseClicked == true)
                {
                    if (novoJogo)
                    {
                        botaoCancelarNJ.VerificarClique((int)posMouse.X, (int)posMouse.Y);
                        if (VerificarCores() == false)
                            botaoIniciarNJ.VerificarClique((int)posMouse.X, (int)posMouse.Y);

                        if (numJogadores < maxJogadores)
                            botaoMais.VerificarClique((int)posMouse.X, (int)posMouse.Y);
                        if (numJogadores > 1)
                            botaoMenos.VerificarClique((int)posMouse.X, (int)posMouse.Y);
                        for (int i = 0; i < numJogadores; i++)
                        {
                            botaoGirar[i].VerificarClique((int)posMouse.X, (int)posMouse.Y);

                            if (botaoGirar[i].Clicado)
                            {
                                contaCor[i] = (contaCor[i] + 1) % corConfig.Length;
                                botaoGirar[i].Clicado = false;
                            }
                        }
                    }
                    else
                    {
                        botaoNovoJogo.VerificarClique((int)posMouse.X, (int)posMouse.Y);
                        if (verificarJogar)
                            botaoJogar.VerificarClique((int)posMouse.X, (int)posMouse.Y);
                        botaoAjuda.VerificarClique((int)posMouse.X, (int)posMouse.Y);
                        botaoCredito.VerificarClique((int)posMouse.X, (int)posMouse.Y);
                        botaoSair.VerificarClique((int)posMouse.X, (int)posMouse.Y);
                    }

                    if (botaoNovoJogo.Clicado)
                    {
                        for (int i = 0; i < maxJogadores; i++)
                            contaCor[i] = i;
                        novoJogo = true;
                        botaoNovoJogo.Clicado = false;
                    }

                    #region NovoJogo Config
                    if (botaoMais.Clicado)
                    {
                        numJogadores += 1;
                        botaoMais.Clicado = false;
                    }

                    if (botaoMenos.Clicado)
                    {
                        numJogadores -= 1;
                        botaoMenos.Clicado = false;
                    }

                    if (botaoIniciarNJ.Clicado)
                    {
                        temporizador = TimeSpan.FromSeconds(6);
                        fechaPropaganda = false;

                        #region Iniciar Jogadores
                        botaoJogar.Cor = Color.White;
                        verificarJogar = true;
                        ComputadorOn = false;

                        if (ComputadorOn == false)
                        {
                            for (int i = 0; i < numJogadores; i++)
                            {
                                Jogador[i] = new Jogador(corConfig[contaCor[i]], new ControladorMouse());
                                peoes[i] = new Peao(0, tab.Linhas - 1, Jogador[i].CorPeao);
                                dinheiroJog[i] = string.Format("DINHEIRO JOGADOR " + (i + 1) + ": R$ {0}", Jogador[i].Dinheiro);
                                posDinheiro[i] = new Vector2(50, AlturaTela - 180 + (50 * i));
                            }
                        }
                        else
                        {
                            //ComputadorOn == true
                        }
                        jogadorAtual = 0;
                        proxJogador = (jogadorAtual + 1) % numJogadores;

                        #endregion

                        tab = new Tabuleiro(7, 7);
                        tempoPausaInteracao = 0;
                        fimDeJogo = 0;

                        #region Estado Jogar
                        comecarRemover = false;
                        fimAnimacao = false;
                        interagindoCompra = false;
                        interagindoAluguel = false;

                        casaAtual = tab.PainelCasas[peoes[jogadorAtual].Y, peoes[jogadorAtual].X];
                        if (Compradas.Count > 0)
                            Compradas.Clear();

                        resultadoDado = 0;
                        #endregion

                        estado = EstadoJogo.Jogar;
                        botaoJogar.Cor = Color.White;

                        novoJogo = false;
                        botaoIniciarNJ.Clicado = false;
                    }

                    if (botaoCancelarNJ.Clicado)
                    {
                        estado = EstadoJogo.Menu;
                        novoJogo = false;
                        botaoCancelarNJ.Clicado = false;
                    }
                    #endregion

                    if (botaoJogar.Clicado)
                    {
                        estado = EstadoJogo.Jogar;
                        botaoJogar.Clicado = false;
                    }

                    if (botaoAjuda.Clicado)
                    {
                        estado = EstadoJogo.Ajuda;
                        botaoAjuda.Clicado = false;
                    }

                    if (botaoCredito.Clicado)
                    {
                        estado = EstadoJogo.Credito;
                        botaoCredito.Clicado = false;
                    }

                    if (botaoSair.Clicado)
                    {
                        estado = EstadoJogo.Sair;
                        botaoSair.Clicado = false;
                    }

                    mouseClicked = false;
                }
                #endregion
            }

            else if (estado == EstadoJogo.Jogar)
            {
                #region Estado Jogar
                
                if (tempoPausaInteracao == 0)
                {
                    VerificarCliqueMouse();

                    if (fimDeJogo != 0)
                    {
                        if (temporizador < fimTempo) { }
                        else 
                            temporizador -= gameTime.ElapsedGameTime;
                    }

                    for (int i = 0; i < numJogadores; i++) //Verificar Fim de jogo por falência
                    {
                        if (Jogador[i].Dinheiro < 0)
                        {
                            fimDeJogo = 1;
                            jogadorAtual = i;
                            proxJogador = (jogadorAtual + 1) % numJogadores;
                        }
                    }

                    #region Computador (NÃO DISPONÍVEL)
                    if (ComputadorOn && jogadorAtual == 1)//Computador só estará disponível em versões posteriores
                    {
                        #region Animacao
                        peao1 = new Peao(peoes[jogadorAtual].X, peoes[jogadorAtual].Y, peoes[jogadorAtual].Cor);
                        jogadorAnimacao = new Jogador(peao1.Cor, new ControladorComputador());
                        #endregion
                        if (comecarRemover)
                            tab.PainelCasas[peoes[jogadorAtual].Y, peoes[jogadorAtual].X].ListaPeoes.Remove(peoes[jogadorAtual]);
                        resultadoDado = Jogador[jogadorAtual].Jogar(peoes[jogadorAtual], dado, tab); //Jogada

                        tempoPausaInteracao = 1;
                    }
                    #endregion

                    if (mouseClicked == true)
                    {
                        if (temporizador < fimTempo)
                        {
                            botaoX.VerificarClique((int)posMouse.X, (int)posMouse.Y);
                        }

                        if (!interagindoCompra && !interagindoAluguel && fimDeJogo == 0)
                        {
                            botaoDado.VerificarClique((int)posMouse.X, (int)posMouse.Y);
                            botaoVoltar.VerificarClique((int)posMouse.X, (int)posMouse.Y);
                        }
                        else if (interagindoCompra && fimDeJogo == 0)
                        {
                            botaoCompraS.VerificarClique((int)posMouse.X, (int)posMouse.Y);
                            botaoCompraN.VerificarClique((int)posMouse.X, (int)posMouse.Y);
                        }
                        else if (interagindoAluguel && fimDeJogo == 0)
                        {
                            botaoOK.VerificarClique((int)posMouse.X, (int)posMouse.Y);
                        }

                        if (fimDeJogo != 0 && fechaPropaganda) //OK do fim do jogo é o mesmo do aluguel
                        {
                            botaoOK.VerificarClique((int)posMouse.X, (int)posMouse.Y);
                        }

                        #region Comprando Propriedade
                        if (botaoCompraS.Clicado)
                        {
                            Jogador[jogadorAtual].Dinheiro -= Convert.ToInt32(precoCasa);
                            casaAtual.Propriedade.Comprador = peoes[jogadorAtual];
                            casaAtual.Propriedade.Estado = Imovel.EstadosImovel.Comprado;
                            Compradas.Add(casaAtual);
                            jogadorAtual = (jogadorAtual + 1) % numJogadores;
                            interagindoCompra = false;
                            botaoCompraS.Clicado = false;
                        }
                        if (botaoCompraN.Clicado)
                        {
                            jogadorAtual = (jogadorAtual + 1) % numJogadores;
                            interagindoCompra = false;
                            botaoCompraN.Clicado = false;
                        }
                        #endregion

                        #region Pagando Aluguel
                        if (botaoOK.Clicado)
                        {
                            if (interagindoAluguel)
                            {
                                jogadorAtual = (jogadorAtual + 1) % numJogadores;
                                interagindoAluguel = false;
                            }
                            else if (fimDeJogo != 0)
                            {
                                verificarJogar = false;
                                botaoJogar.Cor = Color.Gray;
                                estado = EstadoJogo.Menu;
                            }
                            botaoOK.Clicado = false;
                        }
                        #endregion

                        if (botaoDado.Clicado)
                        {
                            #region Jogada Preso
                            if (Jogador[jogadorAtual].Estado == BoardCity2D.Jogador.EstadosJogador.Preso)
                            {
                                #region Animacao
                                peao1 = new Peao(peoes[jogadorAtual].X, peoes[jogadorAtual].Y, peoes[jogadorAtual].Cor);
                                jogadorAnimacao = new Jogador(peao1.Cor, new ControladorMouse());
                                #endregion
                                resultadoDado = Jogador[jogadorAtual].JogarPrisao(peoes[jogadorAtual], dado, tab);
                                casaAtual = tab.PainelCasas[peoes[jogadorAtual].Y, peoes[jogadorAtual].X];

                                if (resultadoDado == 5)
                                {
                                    Jogador[jogadorAtual].RodadasPreso = 0;
                                    tempoPausaInteracao = 1;
                                    Jogador[jogadorAtual].Estado = BoardCity2D.Jogador.EstadosJogador.Jogando;
                                }
                                else
                                {
                                    Jogador[jogadorAtual].RodadasPreso += 1;
                                    if (Jogador[jogadorAtual].RodadasPreso >= 3)
                                    {
                                        Jogador[jogadorAtual].Estado = BoardCity2D.Jogador.EstadosJogador.Jogando;
                                        Jogador[jogadorAtual].RodadasPreso = 0;
                                    }
                                    tempoPausaInteracao = 0;
                                    jogadorAtual = (jogadorAtual + 1) % numJogadores;
                                }
                            }
                            #endregion

                            #region Jogada Jogando
                            else if (Jogador[jogadorAtual].Estado == BoardCity2D.Jogador.EstadosJogador.Jogando)
                            {
                                #region Animacao
                                peao1 = new Peao(peoes[jogadorAtual].X, peoes[jogadorAtual].Y, peoes[jogadorAtual].Cor);
                                jogadorAnimacao = new Jogador(peao1.Cor, new ControladorMouse());
                                #endregion
                                if (comecarRemover)
                                    tab.PainelCasas[peoes[jogadorAtual].Y, peoes[jogadorAtual].X].ListaPeoes.Remove(peoes[jogadorAtual]);
                                resultadoDado = Jogador[jogadorAtual].Jogar(peoes[jogadorAtual], dado, tab); //Jogada
                                casaAtual = tab.PainelCasas[peoes[jogadorAtual].Y, peoes[jogadorAtual].X];

                                tempoPausaInteracao = 1;
                            }
                            #endregion

                            botaoDado.Clicado = false;
                        }

                        if (botaoVoltar.Clicado)
                        {
                            estado = EstadoJogo.Menu;
                            botaoVoltar.Clicado = false;
                        }

                        if (botaoX.Clicado)
                        {
                            fechaPropaganda = true;
                            botaoX.Clicado = false;
                        }
                        mouseClicked = false;
                    }
                }
                #endregion
            }

            else if (estado == EstadoJogo.Ajuda)
            {
                #region Estado Ajuda
                VerificarCliqueMouse();

                if (mouseClicked)
                {
                    botaoVoltar.VerificarClique((int)posMouse.X, (int)posMouse.Y);
                    if (numPaginasAjuda > 1 && paginaAtual < numPaginasAjuda - 1)
                    {
                        botaoPagProx.VerificarClique((int)posMouse.X, (int)posMouse.Y);
                    }
                    if (paginaAtual > 0)
                    {
                        botaoPagAnt.VerificarClique((int)posMouse.X, (int)posMouse.Y);
                    }

                    if (botaoPagProx.Clicado)
                    {
                        paginaAtual = (paginaAtual + 1) % numPaginasAjuda;
                        botaoPagProx.Clicado = false;
                    }

                    if (botaoPagAnt.Clicado)
                    {
                        paginaAtual = (paginaAtual - 1) % numPaginasAjuda;
                        botaoPagAnt.Clicado = false;
                    }

                    if (botaoVoltar.Clicado)
                    {
                        estado = EstadoJogo.Menu;
                        botaoVoltar.Clicado = false;
                    }

                    mouseClicked = false;
                }
                #endregion
            }

            else if (estado == EstadoJogo.Credito)
            {
                #region Estado Credito
                VerificarCliqueMouse();

                if (mouseClicked)
                {
                    botaoVoltar.VerificarClique((int)posMouse.X, (int)posMouse.Y);

                    if (botaoVoltar.Clicado)
                    {
                        animacaoInt = AlturaTela + 50;
                        botaoVoltar.Clicado = false;
                        estado = EstadoJogo.Menu;
                    }

                    mouseClicked = false;
                }
                #endregion
            }

            else if (estado == EstadoJogo.Sair)
            {
                #region Estado Sair
                VerificarCliqueMouse();

                if (mouseClicked)
                {
                    botaoConfirmarSim.VerificarClique((int)posMouse.X, (int)posMouse.Y);
                    botaoConfirmarNao.VerificarClique((int)posMouse.X, (int)posMouse.Y);

                    if (botaoConfirmarSim.Clicado)
                    {
                        botaoConfirmarSim.Clicado = false;
                        Exit();
                    }
                    if (botaoConfirmarNao.Clicado)
                    {
                        estado = EstadoJogo.Menu;
                        botaoConfirmarNao.Clicado = false;
                    }
                    mouseClicked = false;
                }
                #endregion
            }

            base.Update(gameTime);
        }

        private void VerificarCliqueMouse()
        {
            meuMouse = Mouse.GetState();
            posMouse.X = meuMouse.X;
            posMouse.Y = meuMouse.Y;

            if (meuMouse.LeftButton == ButtonState.Pressed)
                lMousePressed = true;

            if (meuMouse.LeftButton == ButtonState.Released && lMousePressed == true)
            {
                mouseClicked = true;
                lMousePressed = false;
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            

            if (estado == EstadoJogo.Menu)
            {
                #region Estado Menu Draw
                spriteBatch.Draw(imgBg1, rectBg, Color.White);

                if (novoJogo)
                {
                    spriteBatch.Draw(configNovoJogo, rectConfig, Color.White);

                    for (int i = 1; i <= numJogadores; i++)
                    {
                        botaoGirar[i - 1] = new Botao(rectConfig.X + rectConfig.Width - 80, rectConfig.Y + 80 + (40 * i) - 5, "", 30, 30, Color.White);
                        Jogador[i - 1].CorPeao = corConfig[contaCor[i - 1]];
                        peoes[i - 1].Cor = Jogador[i - 1].CorPeao;

                        configNJ = string.Format("JOGADOR {0}: {1}", i, peoes[i - 1].Cor);
                        spriteBatch.DrawString(fonte, configNJ, new Vector2(rectConfig.X + 30, rectConfig.Y + 80 + (40 * i)), Color.Black);
                        DrawObjetos.DrawPeaoMenu(peoes[i - 1], spriteBatch, imgPeao, rectConfig.X + rectConfig.Width - 115, rectConfig.Y + 80 + (40 * i) - 5);
                        DrawObjetos.DrawBotao(botaoGirar[i - 1], spriteBatch, imgBotaoGirar, fonte);
                    }

                    if (VerificarCores() == true)
                    {
                        botaoIniciarNJ.Cor = Color.Gray;
                        size = fonte.MeasureString("!!!NAO PODE USAR COR REPETIDA !!!");
                        spriteBatch.DrawString(fonte, "!!! NAO PODE USAR COR REPETIDA !!!", new Vector2(rectConfig.X + (rectConfig.Width / 2 ) - (size.X / 2), rectConfig.Y + rectConfig.Height - 90), Color.Red);
                    }
                    else
                        botaoIniciarNJ.Cor = Color.White;
                    
                    DrawObjetos.DrawBotao(botaoMais, spriteBatch, imgBotaoMais, fonte);
                    DrawObjetos.DrawBotao(botaoMenos, spriteBatch, imgBotaoMenos, fonte);
                    DrawObjetos.DrawBotao(botaoIniciarNJ, spriteBatch, imgBotao, fonte);
                    DrawObjetos.DrawBotao(botaoCancelarNJ, spriteBatch, imgBotao, fonte);
                }
                else
                {
                    DrawObjetos.DrawBotao(botaoNovoJogo, spriteBatch, imgBotao, fonte);
                    DrawObjetos.DrawBotao(botaoJogar, spriteBatch, imgBotao, fonte);
                    DrawObjetos.DrawBotao(botaoAjuda, spriteBatch, imgBotao, fonte);
                    DrawObjetos.DrawBotao(botaoCredito, spriteBatch, imgBotao, fonte);
                    DrawObjetos.DrawBotao(botaoSair, spriteBatch, imgBotao, fonte);
                }
                #endregion
            }

            else if (estado == EstadoJogo.Jogar)
            {
                #region Draw Estado Jogar
                spriteBatch.Draw(imgBg, rectBg, Color.White);
                spriteBatch.Draw(imgTabBG, rectTabBG, Color.White);
                spriteBatch.Draw(imgOverlap, rectOverlap, Color.White);

                #region AtualizarComprador
                if (Compradas.Count > 0)
                {
                    int Alt = 80;
                    int Larg = 80;
                    int deslocX = 40 + 50;
                    int deslocY = 40 + 50;

                    for (int i = 0; i < Compradas.Count; i++)
                    {
                        int posX = deslocX + Compradas[i].X * Larg;
                        int posY = deslocY + Compradas[i].Y * Alt;

                        Vector2 posAfast = new Vector2(0, 0);

                        if (Compradas[i].Propriedade.Linha == 0)
                            posAfast = new Vector2(+22, -30);
                        else if (Compradas[i].Propriedade.Linha == tab.Linhas - 1)
                            posAfast = new Vector2(+22, +70);
                        else if (Compradas[i].Propriedade.Coluna == 0)
                            posAfast = new Vector2(-36, +22);
                        else if (Compradas[i].Propriedade.Coluna == tab.Colunas - 1)
                            posAfast = new Vector2(+76, +22);

                        Rectangle rectComp = new Rectangle(posX + (int)posAfast.X, posY + (int)posAfast.Y, 30, 30);
                        Color corComprador = Color.White;

                        if (Compradas[i].Propriedade.Comprador.Cor == Peao.CorPeao.Vermelha)
                        {
                            corComprador = Color.Red;
                        }
                        else if (Compradas[i].Propriedade.Comprador.Cor == Peao.CorPeao.Azul)
                        {
                            corComprador = Color.Blue;
                        }
                        else if (Compradas[i].Propriedade.Comprador.Cor == Peao.CorPeao.Verde)
                        {
                            corComprador = Color.Green;
                        }
                        else if (Compradas[i].Propriedade.Comprador.Cor == Peao.CorPeao.Amarela)
                        {
                            corComprador = Color.Yellow;
                        }
                        else if (Compradas[i].Propriedade.Comprador.Cor == Peao.CorPeao.Branca)
                        {
                            corComprador = Color.White;
                        }

                        spriteBatch.Draw(imgPeao, rectComp, corComprador);
                    }
                }
                #endregion

                DrawObjetos.DrawDado(spriteBatch, imgDados, resultadoDado);
                DrawObjetos.DrawTabuleiro(tab, spriteBatch, imgCasa);
                DrawObjetos.DrawCela(spriteBatch, imgCela);
                DrawObjetos.DrawBotao(botaoVoltar, spriteBatch, imgBotao, fonte);

                for (int i = 0; i < numJogadores; i++)
                {
                    dinheiroJog[i] = string.Format("DINHEIRO JOGADOR " + (i + 1) + ": R$ {0}", Jogador[i].Dinheiro);
                    botaoDinheiroJog[i] = new Botao((int)posDinheiro[i].X, (int)posDinheiro[i].Y, dinheiroJog[i], 350, 50, Color.White);
                    DrawObjetos.DrawDinheiro(botaoDinheiroJog[i], spriteBatch, imgDinheiro, fonte1);
                }

                if (tempoPausaInteracao == 0)
                {
                    posMsg = new Vector2(200, 520);
                    cor = Color.White;
                    switch (Jogador[jogadorAtual].CorPeao)
                    {
                        case Peao.CorPeao.Branca:
                            cor = Color.White;
                            break;
                        case Peao.CorPeao.Vermelha:
                            cor = Color.Red;
                            break;
                        case Peao.CorPeao.Verde:
                            cor = Color.Green;
                            break;
                        case Peao.CorPeao.Azul:
                            cor = Color.Blue;
                            break;
                        case Peao.CorPeao.Amarela:
                            cor = Color.Yellow;
                            break;
                    }
                    msg = "JOGADOR DA COR:  ";
                    msgCor = Jogador[jogadorAtual].CorPeao.ToString();

                    spriteBatch.DrawString(fonte1, msg, posMsg, Color.White);
                    size = fonte.MeasureString(msg);
                    posMsg.X += size.X;
                    spriteBatch.DrawString(fonte1, msgCor, posMsg, cor);

                    for (int i = 0; i < numJogadores; i++)
                    {
                        if (Jogador[i].Estado == BoardCity2D.Jogador.EstadosJogador.Preso)
                            DrawObjetos.DrawPeaoPreso(peoes[i], spriteBatch, imgPeao, imgCela);
                        else
                            DrawObjetos.DrawPeao(peoes[i], spriteBatch, imgPeao);
                    }

                    if (interagindoCompra && fimDeJogo == 0)
                    {
                        //ComprarPropriedade();
                        size = fonte.MeasureString(msgCompra);
                        posMsgCompra = new Vector2(rectMsgCompra.X + ((rectMsgCompra.Width / 2) - (size.X / 2)), rectMsgCompra.Y + ((rectMsgCompra.Height / 2) - (size.Y / 2)));

                        spriteBatch.Draw(imgBotao, rectMsgCompra, Color.White);
                        spriteBatch.DrawString(fonte, msgCompra, posMsgCompra, Color.White);
                        DrawObjetos.DrawBotao(botaoCompraS, spriteBatch, imgBotao, fonte);
                        DrawObjetos.DrawBotao(botaoCompraN, spriteBatch, imgBotao, fonte);
                    }
                    else if (interagindoAluguel && fimDeJogo == 0)
                    {
                        //PagarAluguel();
                        size = fonte.MeasureString(msgAluguel);
                        posMsgAluguel = new Vector2(rectMsgCompra.X + ((rectMsgCompra.Width / 2) - (size.X / 2)), rectMsgCompra.Y + ((rectMsgCompra.Height / 2) - (size.Y / 2)));

                        spriteBatch.Draw(imgBotao, rectMsgCompra, Color.White);
                        spriteBatch.DrawString(fonte, msgAluguel, posMsgAluguel, Color.White);
                        DrawObjetos.DrawBotao(botaoOK, spriteBatch, imgBotao, fonte);
                    }
                    else DrawObjetos.DrawBotao(botaoDado, spriteBatch, imgBotao, fonte);

                    #region Fim de Jogo
                    if (fimDeJogo != 0)
                    {
                        switch (numJogadores)
                        {
                            case 1:
                                switch (fimDeJogo)
                                {
                                    case 1: //Vitória por jogadore falidos
                                        msgFimDeJogo = string.Format("FIM DE JOGO!");
                                        size = fonte.MeasureString(msgFimDeJogo);
                                        posMsgFimDeJogo = new Vector2(rectMsgCompra.X + ((rectMsgCompra.Width / 2) - (size.X / 2)), rectMsgCompra.Y + (size.Y + 60));
                                        spriteBatch.Draw(imgBotao, rectMsgCompra, Color.White);
                                        spriteBatch.DrawString(fonte, msgFimDeJogo, posMsgFimDeJogo, Color.White);

                                        msgFimDeJogo = string.Format("Dinheiro Jogador {0}: R${1}", proxJogador + 1, Jogador[proxJogador].Dinheiro);
                                        size = fonte.MeasureString(msgFimDeJogo);
                                        posMsgFimDeJogo = new Vector2(rectMsgCompra.X + ((rectMsgCompra.Width / 2) - (size.X / 2)), rectMsgCompra.Y + (size.Y + 100));
                                        spriteBatch.DrawString(fonte, msgFimDeJogo, posMsgFimDeJogo, Color.White);

                                        DrawObjetos.DrawBotao(botaoOK, spriteBatch, imgBotao, fonte);
                                        break;
                                }
                                break;
                            case 2:
                                switch (fimDeJogo)
                                {
                                    case 1: //Vitória por jogadore falidos
                                        msgFimDeJogo = string.Format("O Jogador {0} Venceu a Partida!", proxJogador + 1);
                                        size = fonte.MeasureString(msgFimDeJogo);
                                        posMsgFimDeJogo = new Vector2(rectMsgCompra.X + ((rectMsgCompra.Width / 2) - (size.X / 2)), rectMsgCompra.Y + (size.Y + 60));
                                        spriteBatch.Draw(imgBotao, rectMsgCompra, Color.White);
                                        spriteBatch.DrawString(fonte, msgFimDeJogo, posMsgFimDeJogo, Color.White);

                                        msgFimDeJogo = string.Format("Dinheiro Jogador {0}: R${1}\nDinheiro Jogador {2}: R${3}", proxJogador + 1, Jogador[proxJogador].Dinheiro, jogadorAtual + 1, Jogador[jogadorAtual].Dinheiro);
                                        size = fonte.MeasureString(msgFimDeJogo);
                                        posMsgFimDeJogo = new Vector2(rectMsgCompra.X + ((rectMsgCompra.Width / 2) - (size.X / 2)), rectMsgCompra.Y + (size.Y + 80));
                                        spriteBatch.DrawString(fonte, msgFimDeJogo, posMsgFimDeJogo, Color.White);

                                        DrawObjetos.DrawBotao(botaoOK, spriteBatch, imgBotao, fonte);
                                        break;
                                }
                                break;
                            case 3:
                                switch (fimDeJogo)
                                {
                                    case 1: //Vitória por jogadore falidos
                                        spriteBatch.Draw(imgBotao, rectMsgCompra, Color.White);

                                        if (Jogador[proxJogador].Dinheiro > Jogador[(proxJogador + 1) % numJogadores].Dinheiro)
                                        {
                                            msgFimDeJogo = string.Format("O Jogador {0} Venceu a Partida!", proxJogador + 1);
                                            size = fonte.MeasureString(msgFimDeJogo);
                                            posMsgFimDeJogo = new Vector2(rectMsgCompra.X + ((rectMsgCompra.Width / 2) - (size.X / 2)), rectMsgCompra.Y + (size.Y + 60));
                                            spriteBatch.DrawString(fonte, msgFimDeJogo, posMsgFimDeJogo, Color.White);
                                        }
                                        else if (Jogador[proxJogador].Dinheiro == Jogador[(proxJogador + 1) % numJogadores].Dinheiro)
                                        {
                                            msgFimDeJogo = string.Format("EMPATE!");
                                            size = fonte.MeasureString(msgFimDeJogo);
                                            posMsgFimDeJogo = new Vector2(rectMsgCompra.X + ((rectMsgCompra.Width / 2) - (size.X / 2)), rectMsgCompra.Y + (size.Y + 60));
                                            spriteBatch.DrawString(fonte, msgFimDeJogo, posMsgFimDeJogo, Color.White);
                                        }
                                        else
                                        {
                                            proxJogador = (proxJogador + 1) % numJogadores;
                                            msgFimDeJogo = string.Format("O Jogador {0} Venceu a Partida!", proxJogador + 1);
                                            size = fonte.MeasureString(msgFimDeJogo);
                                            posMsgFimDeJogo = new Vector2(rectMsgCompra.X + ((rectMsgCompra.Width / 2) - (size.X / 2)), rectMsgCompra.Y + (size.Y + 60));
                                            spriteBatch.DrawString(fonte, msgFimDeJogo, posMsgFimDeJogo, Color.White);
                                        }
                                        msgFimDeJogo = string.Format("Dinheiro Jogador {0}: R${1}\nDinheiro Jogador {2}: R${3}\nDinheiro Jogador {4}: R${5}", 1, Jogador[0].Dinheiro, 2, Jogador[1].Dinheiro, 3, Jogador[2].Dinheiro);
                                        size = fonte.MeasureString(msgFimDeJogo);
                                        posMsgFimDeJogo = new Vector2(rectMsgCompra.X + ((rectMsgCompra.Width / 2) - (size.X / 2)), rectMsgCompra.Y + (size.Y + 80));
                                        spriteBatch.DrawString(fonte, msgFimDeJogo, posMsgFimDeJogo, Color.White);

                                        DrawObjetos.DrawBotao(botaoOK, spriteBatch, imgBotao, fonte);
                                        break;
                                }
                                break;
                        }

                        if (!fechaPropaganda)
                        {
                            propaganda = new Propaganda(imgPropaganda, imgBotaoX, botaoX, temporizador, fimTempo);
                            DrawObjetos.DrawPropaganda(propaganda, spriteBatch, graphics, fonte1);
                        }
                    }
                    #endregion
                }

                if (tempoPausaInteracao != 0)
                {
                    posMsg = new Vector2(200, 520);
                    cor = Color.White;
                    switch (Jogador[jogadorAtual].CorPeao)
                    {
                        case Peao.CorPeao.Branca:
                            cor = Color.White;
                            break;
                        case Peao.CorPeao.Vermelha:
                            cor = Color.Red;
                            break;
                        case Peao.CorPeao.Verde:
                            cor = Color.Green;
                            break;
                        case Peao.CorPeao.Azul:
                            cor = Color.Blue;
                            break;
                        case Peao.CorPeao.Amarela:
                            cor = Color.Yellow;
                            break;
                    }
                    msg = "JOGADOR DA COR:  ";
                    msgCor = Jogador[jogadorAtual].CorPeao.ToString();

                    spriteBatch.DrawString(fonte1, msg, posMsg, Color.White);
                    size = fonte.MeasureString(msg);
                    posMsg.X += size.X;
                    spriteBatch.DrawString(fonte1, msgCor, posMsg, cor);

                    for (int i = 0; i < numJogadores; i++)
                    {
                        if (i == jogadorAtual)
                        {
                            //Não desenha o jogador atual durante a animação, apenas usa o DrawAnimaçãoPeao();
                        }
                        else
                        {
                            if (Jogador[i].Estado == BoardCity2D.Jogador.EstadosJogador.Preso)
                                DrawObjetos.DrawPeaoPreso(peoes[i], spriteBatch, imgPeao, imgCela);
                            else
                                DrawObjetos.DrawPeao(peoes[i], spriteBatch, imgPeao);
                        }
                    }

                    #region Animação Antigo / Backup
                    /*
                    switch (numJogadores)
                    {
                        case 1:
                            break;
                        case 2:
                            if (jogadorAtual == 0)
                            {
                                if (Jogador[1].Estado == BoardCity2D.Jogador.EstadosJogador.Preso)
                                    DrawObjetos.DrawPeaoPreso(peoes[1], spriteBatch, imgPeao, imgCela);
                                else
                                    DrawObjetos.DrawPeao(peoes[1], spriteBatch, imgPeao);
                            }
                            else if (jogadorAtual == 1)
                            {
                                if (Jogador[0].Estado == BoardCity2D.Jogador.EstadosJogador.Preso)
                                    DrawObjetos.DrawPeaoPreso(peoes[0], spriteBatch, imgPeao, imgCela);
                                else
                                    DrawObjetos.DrawPeao(peoes[0], spriteBatch, imgPeao);
                            }
                            break;
                        case 3:
                            if (jogadorAtual == 0)
                            {
                                if (Jogador[1].Estado == BoardCity2D.Jogador.EstadosJogador.Preso && Jogador[2].Estado == BoardCity2D.Jogador.EstadosJogador.Preso)
                                {
                                    DrawObjetos.DrawPeaoPreso(peoes[1], spriteBatch, imgPeao, imgCela);
                                    DrawObjetos.DrawPeaoPreso(peoes[2], spriteBatch, imgPeao, imgCela);
                                }
                                else if (Jogador[1].Estado == BoardCity2D.Jogador.EstadosJogador.Preso && Jogador[2].Estado == BoardCity2D.Jogador.EstadosJogador.Jogando)
                                {
                                    DrawObjetos.DrawPeaoPreso(peoes[1], spriteBatch, imgPeao, imgCela);
                                    DrawObjetos.DrawPeao(peoes[2], spriteBatch, imgPeao);
                                }
                                else if (Jogador[2].Estado == BoardCity2D.Jogador.EstadosJogador.Preso && Jogador[1].Estado == BoardCity2D.Jogador.EstadosJogador.Jogando)
                                {
                                    DrawObjetos.DrawPeaoPreso(peoes[2], spriteBatch, imgPeao, imgCela);
                                    DrawObjetos.DrawPeao(peoes[1], spriteBatch, imgPeao);
                                }
                                else
                                {
                                    DrawObjetos.DrawPeao(peoes[1], spriteBatch, imgPeao);
                                    DrawObjetos.DrawPeao(peoes[2], spriteBatch, imgPeao);
                                }
                            }
                            else if (jogadorAtual == 1)
                            {
                                if (Jogador[0].Estado == BoardCity2D.Jogador.EstadosJogador.Preso && Jogador[2].Estado == BoardCity2D.Jogador.EstadosJogador.Preso)
                                {
                                    DrawObjetos.DrawPeaoPreso(peoes[0], spriteBatch, imgPeao, imgCela);
                                    DrawObjetos.DrawPeaoPreso(peoes[2], spriteBatch, imgPeao, imgCela);
                                }
                                else if (Jogador[0].Estado == BoardCity2D.Jogador.EstadosJogador.Preso && Jogador[2].Estado == BoardCity2D.Jogador.EstadosJogador.Jogando)
                                {
                                    DrawObjetos.DrawPeaoPreso(peoes[0], spriteBatch, imgPeao, imgCela);
                                    DrawObjetos.DrawPeao(peoes[2], spriteBatch, imgPeao);
                                }
                                else if (Jogador[2].Estado == BoardCity2D.Jogador.EstadosJogador.Preso && Jogador[0].Estado == BoardCity2D.Jogador.EstadosJogador.Jogando)
                                {
                                    DrawObjetos.DrawPeao(peoes[0], spriteBatch, imgPeao);
                                    DrawObjetos.DrawPeaoPreso(peoes[2], spriteBatch, imgPeao, imgCela);
                                }
                                else
                                {
                                    DrawObjetos.DrawPeao(peoes[0], spriteBatch, imgPeao);
                                    DrawObjetos.DrawPeao(peoes[2], spriteBatch, imgPeao);
                                }
                            }
                            else if (jogadorAtual == 2)
                            {
                                if(Jogador[0].Estado == BoardCity2D.Jogador.EstadosJogador.Preso && Jogador[1].Estado == BoardCity2D.Jogador.EstadosJogador.Preso)
                                {
                                    DrawObjetos.DrawPeaoPreso(peoes[0], spriteBatch, imgPeao, imgCela);
                                    DrawObjetos.DrawPeaoPreso(peoes[1], spriteBatch, imgPeao, imgCela);
                                }
                                else if (Jogador[0].Estado == BoardCity2D.Jogador.EstadosJogador.Preso && Jogador[1].Estado == BoardCity2D.Jogador.EstadosJogador.Jogando)
                                {
                                    DrawObjetos.DrawPeaoPreso(peoes[0], spriteBatch, imgPeao, imgCela);
                                    DrawObjetos.DrawPeao(peoes[1], spriteBatch, imgPeao);
                                }
                                else if (Jogador[1].Estado == BoardCity2D.Jogador.EstadosJogador.Preso && Jogador[0].Estado == BoardCity2D.Jogador.EstadosJogador.Jogando)
                                {
                                    DrawObjetos.DrawPeao(peoes[0], spriteBatch, imgPeao);
                                    DrawObjetos.DrawPeaoPreso(peoes[1], spriteBatch, imgPeao, imgCela);
                                }
                                else
                                {
                                    DrawObjetos.DrawPeao(peoes[0], spriteBatch, imgPeao);
                                    DrawObjetos.DrawPeao(peoes[1], spriteBatch, imgPeao);
                                }
                            }
                            break;
                    }
                    */
                    #endregion

                    DrawAnimacaoPeao();
                    if (fimAnimacao)
                        VerificarPropriedade();

                }
                #endregion
            }

            else if (estado == EstadoJogo.Ajuda)
            {
                #region Estado Ajuda Draw
                spriteBatch.Draw(imgBg, rectBg, Color.White);
                ajuda = new Ajuda(paginasAjuda, botaoPagAnt, botaoPagProx, imgBotaoAjuda, imgBotaoAjuda1, paginaAtual);

                if (numPaginasAjuda > 0 && paginaAtual < numPaginasAjuda - 1)
                {
                    botaoPagProx.Cor = Color.White;
                } else
                    botaoPagProx.Cor = Color.Gray;

                if (paginaAtual > 0)
                {
                    botaoPagAnt.Cor = Color.White;
                } else
                    botaoPagAnt.Cor = Color.Gray;

                DrawObjetos.DrawBotao(botaoVoltar, spriteBatch, imgBotao, fonte);
                DrawObjetos.DrawAjuda(ajuda, spriteBatch, fonte, graphics);
                #endregion
            }

            else if (estado == EstadoJogo.Credito)
            {
                #region Estado Credito Draw
                spriteBatch.Draw(imgBg, rectBg, Color.White);
                credito = new Credito("CRIADO E DESENVOLVIDO POR:", "Rafael de Aquino Ferreira", "rafael0520@gmail.com", "RA00212876", Foto);

                DrawObjetos.DrawBotao(botaoVoltar, spriteBatch, imgBotao, fonte);
                DrawObjetos.DrawCredito(credito, spriteBatch, fonte1, graphics, animacaoInt);
                animacaoInt--;

                if (animacaoInt < -280)
                    animacaoInt = AlturaTela + 10;
                #endregion
            }

            else if (estado == EstadoJogo.Sair)
            {
                #region Estado Sair Draw
                spriteBatch.Draw(imgBg, rectBg, Color.White);


                //Deseja realmente Sair
                size = fonte.MeasureString("DESEJA REALMENTE SAIR?");
                posConfirmarMsg = new Vector2((LarguraTela / 2) - (size.X / 2), (AlturaTela / 2) - 100);

                #region Iniciar Botoes Sair
                //Confirmar Sim ("50" é o valor da largura do botao default dividido por 2)
                botaoConfirmarSim = new Botao((LarguraTela / 2) - 60, ((int)posConfirmarMsg.Y + 50), "SIM");

                //Confirmar Nao ("50" é o valor da largura do botao default dividido por 2)
                botaoConfirmarNao = new Botao((LarguraTela / 2) - 60, ((int)posConfirmarMsg.Y + 100), "NAO");
                #endregion

                spriteBatch.DrawString(fonte1, "DESEJA REALMENTE SAIR?", posConfirmarMsg, Color.Black);
                DrawObjetos.DrawBotao(botaoConfirmarSim, spriteBatch, imgBotao, fonte);
                DrawObjetos.DrawBotao(botaoConfirmarNao, spriteBatch, imgBotao, fonte);
                #endregion
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }

        private void VerificarPropriedade()
        {
            casaAtual = tab.PainelCasas[peoes[jogadorAtual].Y, peoes[jogadorAtual].X];
            int peaoAtualX = peoes[jogadorAtual].X;
            int peaoAtualY = peoes[jogadorAtual].Y;
            casaAtual.ListaPeoes.Add(peoes[jogadorAtual]);
            comecarRemover = true;

            if (casaAtual == tab.PainelCasas[tab.Linhas - 1, tab.Colunas - 1])
            {
                Jogador[jogadorAtual].Estado = BoardCity2D.Jogador.EstadosJogador.Preso;
                peoes[jogadorAtual].X = 0;
                peoes[jogadorAtual].Y = 0;
                Jogador[jogadorAtual].Sentido = Controlador.Direcao.Leste;
            }

            if (casaAtual.Propriedade.Especial == 0)
            {
                if (casaAtual.Propriedade.Estado == Imovel.EstadosImovel.Venda)
                {

                    casaAtual = tab.PainelCasas[peoes[jogadorAtual].Y, peoes[jogadorAtual].X];
                    precoCasa = Convert.ToString(casaAtual.Propriedade.Preco);
                    valorAluguel = Convert.ToString(casaAtual.Propriedade.Aluguel);

                    msgCompra = string.Format("DESEJA COMPRAR ESSA PROPRIEDADE? \nVALOR: R${0}\nALUGUEL: R${1}", precoCasa, valorAluguel);

                    interagindoCompra = true;

                }
                else if (tab.PainelCasas[peaoAtualY, peaoAtualX].Propriedade.Estado == Imovel.EstadosImovel.Comprado)
                {
                    casaAtual = tab.PainelCasas[peoes[jogadorAtual].Y, peoes[jogadorAtual].X];

                    if (casaAtual.Propriedade.Comprador != peoes[jogadorAtual])
                    {
                        valorAluguel = Convert.ToString(casaAtual.Propriedade.Aluguel);
                        Jogador[jogadorAtual].Dinheiro -= Convert.ToInt32(valorAluguel);
                        
                        for (int i = 0; i < numJogadores; i++) //Usado para que o aluguel chegue ao jogador certo com inúmeros jogadores na partida
                        {
                            if (Jogador[i].CorPeao == tab.PainelCasas[peoes[jogadorAtual].Y, peoes[jogadorAtual].X].Propriedade.Comprador.Cor)
                                Jogador[i].Dinheiro += Convert.ToInt32(valorAluguel);
                        }
                        

                        msgAluguel = string.Format("Voce pagou R${0} de aluguel. ", valorAluguel);

                        interagindoAluguel = true;
                    }
                    else
                        jogadorAtual = (jogadorAtual + 1) % numJogadores;
                }
            }
            else
                jogadorAtual = (jogadorAtual + 1) % numJogadores;
        }

        private void DrawAnimacaoPeao()
        {
            DrawObjetos.DrawPeao(peao1, spriteBatch, imgPeao);

            if (peao1.X == peoes[jogadorAtual].X && peao1.Y == peoes[jogadorAtual].Y)
            {
                tempoPausaInteracao = 0;
                fimAnimacao = true;
            }
            else
                fimAnimacao = false;

            if (peao1.Y <= tab.Linhas - 1 && peao1.X == 0 && peao1.Y >= 0) { jogadorAnimacao.Sentido = Controlador.Direcao.Norte; }
            else if (peao1.Y == 0 && peao1.X >= 0 && peao1.X <= tab.Colunas - 1) { jogadorAnimacao.Sentido = Controlador.Direcao.Leste; }
            else if (peao1.Y >= 0 && peao1.X == tab.Colunas - 1 && peao1.Y <= tab.Linhas - 1) { jogadorAnimacao.Sentido = Controlador.Direcao.Sul; }
            else if (peao1.Y == tab.Linhas - 1 && peao1.X <= tab.Colunas - 1 && peao1.X >= 0) { jogadorAnimacao.Sentido = Controlador.Direcao.Oeste; }

            if (jogadorAnimacao.Sentido == Controlador.Direcao.Norte)
            {
                peao1.Y -= 1;
                if (peao1.Y < 0) { peao1.Y += 1; peao1.X += 1; }               //Esse if impossibilita a peça de seguir infinitamente em uma direção,
                Thread.Sleep(500);                                             //assim alterando sua rota para onde deveria ir antes do Sleep
            }

            else if (jogadorAnimacao.Sentido == Controlador.Direcao.Leste)
            {
                
                peao1.X += 1;
                if (peao1.X > tab.Colunas - 1) { peao1.X -= 1; peao1.Y += 1; } //Esse if impossibilita a peça de seguir infinitamente em uma direção,
                Thread.Sleep(500);                                             //assim alterando sua rota para onde deveria ir antes do Sleep
            }

            else if (jogadorAnimacao.Sentido == Controlador.Direcao.Sul)
            {
                peao1.Y += 1;
                if (peao1.Y > tab.Linhas - 1) { peao1.Y -= 1; peao1.X -= 1; }  //Esse if impossibilita a peça de seguir infinitamente em uma direção,
                Thread.Sleep(500);                                             //assim alterando sua rota para onde deveria ir antes do Sleep
            }

            else if (jogadorAnimacao.Sentido == Controlador.Direcao.Oeste)
            {
                
                peao1.X -= 1;
                if (peao1.X < 0) { peao1.X += 1; peao1.Y -= 1; }               //Esse if impossibilita a peça de seguir infinitamente em uma direção,
                Thread.Sleep(500);                                             //assim alterando sua rota para onde deveria ir antes do Sleep
            }

            if (tab.PainelCasas[peao1.Y, peao1.X].Propriedade.Especial == 1)
            {
                Jogador[jogadorAtual].Dinheiro += 200;
            }
                
        }

        private bool VerificarCores()
        {
            bool ok = false;

            switch (numJogadores)
            {
                case 1:
                    ok = false;
                    break;
                case 2:
                    if (Jogador[0].CorPeao == Jogador[1].CorPeao)
                        ok = true;
                    else
                        ok = false;
                    break;
                case 3:
                    if (Jogador[0].CorPeao == Jogador[1].CorPeao || Jogador[0].CorPeao == Jogador[2].CorPeao || Jogador[1].CorPeao == Jogador[2].CorPeao)
                        ok = true;
                    else
                        ok = false;
                    break;
            }
            
            return ok;
        }
    }
}
