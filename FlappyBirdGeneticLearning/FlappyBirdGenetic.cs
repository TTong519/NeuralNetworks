using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace FlappyBirdGeneticLearning
{
    public class FlappyBirdGenetic : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        Texture2D birdTexture;
        Texture2D pipeTexture;
        List<Pipe> Pipes;
        Bird bird;
        int counter = 0;
        public FlappyBirdGenetic()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            graphics.PreferredBackBufferWidth = 1200;
            graphics.PreferredBackBufferHeight = 600;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            Pipes = new List<Pipe>();
            bird = new Bird(new Rectangle(200, 100, 60, 50), 0.5);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            birdTexture = Content.Load<Texture2D>("bird");
            pipeTexture = Content.Load<Texture2D>("pipe");
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            counter++;
            foreach (Pipe pipe in Pipes)
            {
                pipe.Update(4);
            }
            if(counter % 120 == 0)
            {
                Pipes.Add(new Pipe(new Vector2(1300, 300 + (float)(Random.Shared.NextDouble() * 400) - 200), 112, 100));
            }
            if(bird.Update(Pipes, GraphicsDevice))
            {
                Exit();
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            bird.Draw(spriteBatch, birdTexture);
            foreach (Pipe pipe in Pipes)
            {
                pipe.Draw(spriteBatch, pipeTexture);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
