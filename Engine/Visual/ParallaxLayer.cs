using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace InfiniteIsland.Engine.Visual
{
    public abstract class ParallaxLayer<T> where T : struct
    {
        public List<Sprite<T>> Sprites;
        public Vector2 Parallax { get; set; }

        public abstract void Update(GameTime gameTime);

        public void Draw(SpriteBatch spriteBatch, Camera camera)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null,
                              camera.CalculateTransformMatrix(Parallax));
            foreach (var sprite in Sprites)
            {
                sprite.Draw(spriteBatch);
            }
            spriteBatch.End();
        }
    }
}