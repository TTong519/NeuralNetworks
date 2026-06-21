using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using CommonLib;
using static CommonLib.Math;
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
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 800;
            graphics.ApplyChanges();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            // TODO: Add your initialization logic here
           
            nn = new(ActivationFuncs.TanH, ErrorFuncs.MSE, CommonLib.Math.Identity, 1, 3, 5, 2, 1);
            for (double i = 0; i <= System.Math.PI * 2; i += System.Math.PI/18)
            {
                data.Add(i, System.Math.Sin(i));
            }
            foreach (var pair in data)
            {
                double normalizedX = pair.Key / (System.Math.PI * 2);
                double normalizedY = (pair.Value + 1) / 2;
                inputs.Add(normalizedX);
                targets.Add(normalizedY);
            }
            nn.Randomize(Random.Shared, -1, 1);
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            nn.BatchTrain([inputs.ToArray()], [targets.ToArray()], 0.01, 0.9, 1);
            // TODO: Add your update logic here
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            for (int i = 0; i < inputs.Count; i++)
            {
                Vector2 targetPoint = new((float)inputs[i] * 800, (1f - (float)targets[i]) * 800);
                spriteBatch.DrawCircle(targetPoint, 3, 16, Color.Red, 3);
                double networkOutput = nn.Compute(new double[] { inputs[i] })[0];
                Vector2 predictionPoint = new((float)inputs[i] * 800, (1f - (float)networkOutput) * 800);
                spriteBatch.DrawCircle(predictionPoint, 3, 16, Color.Green, 3);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
