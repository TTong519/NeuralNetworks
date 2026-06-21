using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using CommonLib;
using System.Collections.Generic;
using System;
using MonoGame.Extended;
using System.Linq;
namespace NNAsSin
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        NeuralNetwork nn;
        Dictionary<double, double> data = new Dictionary<double, double>();
        List<double> inputs = new List<double>();
        List<double> targets = new List<double>();
        double xscale;
        double inputMin;
        double inputMax;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 800;
            graphics.ApplyChanges();
            nn = new(ActivationFuncs.TanH, ErrorFuncs.MSE, CommonLib.Math.Identity, 1, 3, 5, 3, 1);
            for (double i = 0; i < System.Math.PI * 2; i += 0.01)
            {
                data.Add(i, System.Math.Sin(i));
            }
            inputMin = 0;
            inputMax = System.Math.PI * 2;
            foreach (var pair in data)
            {
                inputs.Add(pair.Key);
                targets.Add(pair.Value);
            }
            nn.Randomize(Random.Shared, -1, 1);
            xscale = 800 / (2 * System.Math.PI);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            nn.Train(new double[][] { inputs.ToArray() }, new double [][] { targets.ToArray() }, 0.1, 0.9);
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            foreach (var pair in data)
            {
                spriteBatch.DrawCircle(x: (float)(pair.Key * xscale), y: (float)(pair.Value * 100 + 400), radius: 5f, sides: 100, color: Color.Black, 5);
                spriteBatch.DrawCircle(x: (float)(pair.Key * xscale), y: (float)(nn.Compute(new[] { pair.Key })[0] * 100 + 400), radius: 5f, sides: 100, color: Color.Black, 5);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
