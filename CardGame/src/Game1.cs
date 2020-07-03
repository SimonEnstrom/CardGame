using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Diagnostics;

namespace CardGame
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class cg : Game
    {
        GraphicsDeviceManager g_graphics;
        SpriteBatch g_spriteBatch;
        Input.InputModule g_Input;

        int screenWidth;
        int screenHeight;

        Scoreboard g_Scoreboard;

        Deck g_Deck; 

        Card g_cCard;
        Vector2 g_cCardPos;

        Button gB_High;
        Button gB_Low;

        Button gB_RQ;

        SpriteFont font_Arial;

        System.Random g_Random;

        public cg()
        {
            g_graphics = new GraphicsDeviceManager(this);
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
            // TODO: Add your initialization logic here
            g_Random = new System.Random();
            g_Input = new Input.InputModule();
            g_Deck = new Deck(Content, g_Random);
            g_cCardPos = Vector2.Zero;

            g_Scoreboard = new Scoreboard();

            screenWidth = g_graphics.GraphicsDevice.Viewport.Bounds.Width;
            screenHeight = g_graphics.GraphicsDevice.Viewport.Bounds.Height;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            g_spriteBatch = new SpriteBatch(GraphicsDevice);
            g_Deck.GenerateNewDeck();

            Texture2D tx = Content.Load<Texture2D>("tinycards");

            gB_High = new Button("knapptbra", new Vector2(screenWidth - 100, 30), g_Input, Content);
            gB_Low = new Button("knapptbra", new Vector2(screenWidth - 100, 100), g_Input, Content);
            gB_RQ = new Button("knapptbra", new Vector2(screenWidth - 100, 170), g_Input, Content);

            font_Arial = Content.Load<SpriteFont>("Score");

            g_cCard = g_Deck.NextCard();
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

            g_Input.Pull_Events();

            if (g_Input.KeyPressed(Keys.Enter))
            {
                g_cCard = g_Deck.NextCard();
                Debug.Write("Card[" + g_cCard.m_Value + "," + (int)g_cCard.m_Type + "]\n");
            }

            if (gB_High.Click())
            {
                Card newCard = g_Deck.NextCard();
                if (newCard.m_Value == 13)
                {
                    g_cCard = newCard;
                    return;
                }
                    

                if (g_cCard.m_Value < newCard.m_Value)
                    g_Scoreboard.win();
                else if (g_cCard.m_Value > newCard.m_Value)
                    g_Scoreboard.Lose();

                g_cCard = newCard;
            }

            if(gB_Low.Click())
            {
                Card newCard = g_Deck.NextCard();
                if (newCard.m_Value == 13)
                {
                    g_cCard = newCard;
                    return;
                }

                if (g_cCard.m_Value > newCard.m_Value)
                    g_Scoreboard.win();
                else if (g_cCard.m_Value < newCard.m_Value)
                    g_Scoreboard.Lose();

                g_cCard = newCard;
            }

            if (gB_RQ.Click()) {

                Exit();
            }
            g_cCardPos = g_Input.MousePos().ToVector2();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            g_spriteBatch.Begin();

            if (!g_Deck.EmptyDeck())
                g_Deck.DrawCard(g_spriteBatch, new Card(13, 0), new Vector2(10, 10));

            gB_High.Draw(g_spriteBatch);
            gB_Low.Draw(g_spriteBatch);
            gB_RQ.Draw(g_spriteBatch);

            g_Deck.DrawDrawnCards(g_spriteBatch);

            g_Scoreboard.Draw(g_spriteBatch, font_Arial);

            g_spriteBatch.DrawString(font_Arial, "High:", new Vector2(screenWidth - 160, 40), Color.BlanchedAlmond);
            g_spriteBatch.DrawString(font_Arial, "Low:", new Vector2(screenWidth - 160, 110), Color.BlanchedAlmond);
            g_spriteBatch.DrawString(font_Arial, "RQ:", new Vector2(screenWidth - 160, 180), Color.BlanchedAlmond);

            g_Deck.DrawCard(g_spriteBatch, g_cCard, g_cCardPos);

            g_spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
