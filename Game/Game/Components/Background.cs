using InfiniteIsland.Engine;
using InfiniteIsland.Visual;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using IDrawable = InfiniteIsland.Engine.Interface.IDrawable;
using IUpdateable = InfiniteIsland.Engine.Interface.IUpdateable;

namespace InfiniteIsland.Components
{
    public class Background : IUpdateable, IDrawable
    {
        public static Background Instance;
        public Color SkyColor = Color.DarkCyan;

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