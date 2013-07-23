using InfiniteIsland.Visual;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using IDrawable = InfiniteIsland.Engine.Interface.IDrawable;
using IUpdateable = InfiniteIsland.Engine.Interface.IUpdateable;

namespace InfiniteIsland.Components
{
    public class Background : Engine.Interface.IUpdateable, Engine.Interface.IDrawable
    {
        private readonly MountsLayer[] _parallaxLayers = new MountsLayer[3];

        public Background(Game game)
        {
            for (int i = 0; i < _parallaxLayers.Length; i++)
            {
                _parallaxLayers[i] = new MountsLayer(new Vector2((float)i / _parallaxLayers.Length, 1f));
                _parallaxLayers[i].LoadContent(game);
            }

        }

        public void Update(GameTime gameTime)
        {
            foreach (MountsLayer layer in _parallaxLayers)
            {
                layer.Update(gameTime);
            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (MountsLayer layer in _parallaxLayers)
            {
                layer.Draw(spriteBatch, InfiniteIsland.Camera);
            }
        }
    }
}