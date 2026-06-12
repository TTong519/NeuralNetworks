using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlappyBirdGeneticLearning
{
    public class Pipe
    {
        public Vector2 Location;
        public float GapWidth;
        public float GapHeight;
        public Pipe(Vector2 location, float gapWidth, float gapHeight)
        {
            Location = location;
            GapWidth = gapWidth;
            GapHeight = gapHeight;
        }
        public (Rectangle Upper, Rectangle Lower) GetBounds(GraphicsDevice graphicsDevice)
        {
            return (new Rectangle((int)Location.X, (int)(Location.Y - GapHeight) - 500, (int)GapWidth, 500), new Rectangle((int)Location.X, (int)(Location.Y + GapHeight), (int)GapWidth, 500));
        }
        public void Update(float speed)
        {
            Location = new Vector2(Location.X - speed, Location.Y);
        }
        public void Draw(SpriteBatch spriteBatch, Texture2D pipeTexture)
        {
            var bounds = GetBounds(spriteBatch.GraphicsDevice);
            spriteBatch.Draw(pipeTexture, new Vector2(bounds.Upper.X, bounds.Upper.Y), null, Color.White, 0, default, new Vector2(1, 1), SpriteEffects.None, 1 );
            spriteBatch.Draw(pipeTexture, new Vector2(bounds.Lower.X, bounds.Lower.Y), null, Color.White, 0, default, new Vector2(1, 1), SpriteEffects.FlipVertically, 1);
        }
    }
}       
