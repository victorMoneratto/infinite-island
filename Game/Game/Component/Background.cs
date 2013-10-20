using InfiniteIsland.Engine;
using InfiniteIsland.Visual;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace InfiniteIsland.Component
{
    public class Background 
    {
        public Color SkyColor = new Color(87, 168, 168);

        public Background(Vector2 screenDimensions)
        {
        }

        public void Draw(SpriteBatch spriteBatch, Camera camera)
        {
        }

        public void Update(GameTime gameTime)
        {
        }

        public static void LoadContent(ContentManager content)
        {
            MoonLayer.LoadContent(content);
        }
    }
}