using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlappyBirdGeneticLearning
{
    public class Bird
    {
        public Rectangle Bounds;
        public double yVelocity;
        public double Gravity;
        private bool lastState = false;
        public Bird(Rectangle bounds, double gravity)
        {
            Bounds = bounds;
            yVelocity = 0;
            Gravity = gravity;
        }
        public bool Update(List<Pipe> pipes, GraphicsDevice graphicsDevice)
        {
            yVelocity += Gravity;
            bool currentState = Keyboard.GetState().IsKeyDown(Keys.Space);
            if (currentState && !lastState)
            {
                yVelocity -= 12;
            }
            lastState = currentState;
            Bounds = new Rectangle(Bounds.X, (int)(Bounds.Y + yVelocity), Bounds.Width, Bounds.Height);
            if(Bounds.Y < 0 || Bounds.Y + Bounds.Height > graphicsDevice.Viewport.Height)
            {
                return true;
            }
            return CheckCollisions(pipes, graphicsDevice);
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
            spriteBatch.Draw(birdTexture, Bounds, Color.White);
        }
    }
}
