using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace InfiniteIsland.Engine.Visual
{
    public abstract class ParallaxLayer
    {
        public List<Sprite> Sprites;
        public Vector2 Parallax { get; set; }

        public abstract void Update(GameTime gameTime);

        public void Draw(SpriteBatch spriteBatch, Camera camera, Effect effect=null)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, effect,
                              camera.CalculateTransformMatrix(Parallax));
            foreach (Sprite sprite in Sprites)
            {
                sprite.Draw(spriteBatch);
            }
            spriteBatch.End();
        }

    }
}