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

        public abstract void Draw(SpriteBatch spriteBatch, Camera camera);

    }
}