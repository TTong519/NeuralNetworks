using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLib;

namespace FlappyBirdGeneticLearning
{
    public class Bird
    {
        public Rectangle Bounds;
        public double yVelocity;
        public double Gravity;
        public NeuralNetwork NeuralNetwork;
        public bool isDead = false;
        public int Score = 0;
        public Bird(Rectangle bounds, double gravity)
        {
            Bounds = bounds;
            yVelocity = 0;
            Gravity = gravity;
        }
        public bool Update(List<Pipe> pipes, GraphicsDevice graphicsDevice, bool isJump)
        {
            if(isDead)
            {
                return false;
            }
            yVelocity += Gravity;
            if (isJump)
            {
                yVelocity = -10;
            }
            Score++;
            Bounds = new Rectangle(Bounds.X, (int)(Bounds.Y + yVelocity), Bounds.Width, Bounds.Height);
            if(Bounds.Y < 0 || Bounds.Y + Bounds.Height > graphicsDevice.Viewport.Height)
            {
                isDead = true;
                return true;
            }
            isDead = CheckCollisions(pipes, graphicsDevice);
            return isDead;
        }
        private bool CheckCollisions(List<Pipe> pipes, GraphicsDevice graphicsDevice)
        {
            foreach (Pipe pipe in pipes)
            {
                var bounds = pipe.GetBounds(graphicsDevice);
                if (Bounds.Intersects(bounds.Upper) || Bounds.Intersects(bounds.Lower))
                {
                    return true;
                }
            }
            return false;
        }
        public void Draw(SpriteBatch spriteBatch, Texture2D birdTexture)
        {
            if (isDead)
            {
                return;
            }   
            spriteBatch.Draw(birdTexture, Bounds, Color.White);
        }
    }
}
