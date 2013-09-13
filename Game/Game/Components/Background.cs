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
        private static Effect _blur;
        private readonly MoonLayer _moon;
        public Color SkyColor = Color.DarkCyan;

        public Background(Vector2 screenDimensions)
        {
            _moon = new MoonLayer(screenDimensions/2f);
        }

        public void Draw(SpriteBatch spriteBatch, Camera camera)
        {
            _moon.Draw(spriteBatch, camera, _blur);
        }

        public void Update(GameTime gameTime)
        {
            _moon.Update(gameTime);
        }

        public static void LoadContent(ContentManager content)
        {
            MoonLayer.LoadContent(content);
            _blur = content.Load<Effect>("Blur");
        }
    }
}