using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using static CommonLib.MinimaxTree;
using MonoGame.Extended;

namespace TicTacToe
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        TicTacToe currentState;
        Texture2D OTexture;
        Texture2D XTexture;
        bool isMax;
        int counter;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
            currentState = new TicTacToe(false, false, false, false, [[0, 0, 0], [0, 0, 0], [0, 0, 0]]);
            isMax = false;
            counter = 0;
            graphics.PreferredBackBufferWidth = 900;
            graphics.PreferredBackBufferHeight = 900;
            graphics.ApplyChanges();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            OTexture = Content.Load<Texture2D>("0");
            XTexture = Content.Load<Texture2D>("x");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            counter++;
            if(counter % 500 == 0)
            {
                var (value, nextState) = Minimax(currentState, isMax);
                isMax = !isMax;
                currentState = nextState;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            spriteBatch.DrawLine(new(0, 300), new(900, 300), Color.Black);
            spriteBatch.DrawLine(new(0, 600), new(900, 600), Color.Black);
            spriteBatch.DrawLine(new(300, 0), new(300, 900), Color.Black);
            spriteBatch.DrawLine(new(600, 0), new(600, 900), Color.Black);
            for(int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (currentState.Board != null && currentState.Board[i][j] == 1)
                    {
                        spriteBatch.Draw(XTexture, new Vector2(i * 300, j * 300), Color.White);
                    }
                    else if (currentState.Board != null && currentState.Board[i][j] == -1)
                    {
                        spriteBatch.Draw(OTexture, new Vector2(i * 300, j * 300), Color.White);
                    }
                }
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
