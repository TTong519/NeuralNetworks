using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using System.Collections.Generic;

namespace MonteCarloCheckers
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        public Texture2D redPiece;
        public Texture2D blackPiece;
        public Texture2D redKing;
        public Texture2D blackKing;
        public Texture2D board;
        public CheckersState CurrentState;
        public ButtonState prevLeftMouseState;
        public MouseState mouseState;
        public bool isMax;
        public List<CheckersState> toHighlight;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            graphics.PreferredBackBufferWidth = 1000;
            graphics.PreferredBackBufferHeight = 1000;
            graphics.ApplyChanges();
            prevLeftMouseState = ButtonState.Released;
            mouseState = Mouse.GetState();
            isMax = false;
            toHighlight = new List<CheckersState>();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            redPiece = Content.Load<Texture2D>("red");
            blackPiece = Content.Load<Texture2D>("black");
            redKing = Content.Load<Texture2D>("redk");
            blackKing = Content.Load<Texture2D>("blackk");
            board = Content.Load<Texture2D>("board");
            PieceState[,] initialBoard = new PieceState[8, 8]
            {
                { PieceState.Empty, PieceState.Red,   PieceState.Empty, PieceState.Red,   PieceState.Empty, PieceState.Red,   PieceState.Empty, PieceState.Red   },
                { PieceState.Red,   PieceState.Empty, PieceState.Red,   PieceState.Empty, PieceState.Red,   PieceState.Empty, PieceState.Red,   PieceState.Empty },
                { PieceState.Empty, PieceState.Red,   PieceState.Empty, PieceState.Red,   PieceState.Empty, PieceState.Red,   PieceState.Empty, PieceState.Red   },
                { PieceState.Empty, PieceState.Empty, PieceState.Empty, PieceState.Empty, PieceState.Empty, PieceState.Empty, PieceState.Empty, PieceState.Empty },
                { PieceState.Empty, PieceState.Empty, PieceState.Empty, PieceState.Empty, PieceState.Empty, PieceState.Empty, PieceState.Empty, PieceState.Empty },
                { PieceState.Black, PieceState.Empty, PieceState.Black, PieceState.Empty, PieceState.Black, PieceState.Empty, PieceState.Black, PieceState.Empty },
                { PieceState.Empty, PieceState.Black, PieceState.Empty, PieceState.Black, PieceState.Empty, PieceState.Black, PieceState.Empty, PieceState.Black },
                { PieceState.Black, PieceState.Empty, PieceState.Black, PieceState.Empty, PieceState.Black, PieceState.Empty, PieceState.Black, PieceState.Empty }
            };
            CurrentState = new CheckersState(initialBoard);
            CurrentState.GenerateChildren(isMax);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            mouseState = Mouse.GetState();
            if (mouseState.LeftButton == ButtonState.Pressed && prevLeftMouseState == ButtonState.Released)
            {
                Point square = new Point(mouseState.Y / 125, mouseState.X / 125);
                for(int i = 0; i < toHighlight.Count; i++)
                {
                    if (toHighlight[i].Board[square.X, square.Y] != CurrentState.Board[square.X, square.Y] && toHighlight[i].Board[square.X, square.Y] != PieceState.Empty)
                    {
                        CurrentState = toHighlight[i];
                        isMax = !isMax;
                        toHighlight.Clear();
                        CurrentState.GenerateChildren(isMax);
                        break;
                    }
                }
                toHighlight = CurrentState.MovesByPiece(square);
            }
            prevLeftMouseState = mouseState.LeftButton;
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            spriteBatch.Draw(board, new Vector2(0, 0), Color.White);
            for(int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Vector2 position = new Vector2(j * 125, i * 125);
                    switch (CurrentState.Board[i, j])
                    {
                        case PieceState.Red:
                            spriteBatch.Draw(redPiece, position, Color.White);
                            break;
                        case PieceState.Black:
                            spriteBatch.Draw(blackPiece, position, Color.White);
                            break;
                        case PieceState.RedKing:
                            spriteBatch.Draw(redKing, position, Color.White);
                            break;
                        case PieceState.BlackKing:
                            spriteBatch.Draw(blackKing, position, Color.White);
                            break;
                    }
                }
            }
            for(int i = 0; i < toHighlight.Count; i++)
            {
                for(int j = 0; j < 8; j++)
                {
                    for (int k = 0; k < 8; k++)
                    {
                        if (toHighlight[i].Board[j, k] != CurrentState.Board[j, k])
                        {
                            Vector2 position = new Vector2(k * 125, j * 125);
                            spriteBatch.DrawRectangle(new Rectangle((int)position.X, (int)position.Y, 125, 125), Color.Yellow, 5);
                        }
                    }
                }
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
