using InfiniteIsland.Visual;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using IDrawable = InfiniteIsland.Engine.IDrawable;
using IUpdateable = InfiniteIsland.Engine.IUpdateable;

namespace InfiniteIsland.Components
{
    public class Background : Engine.IUpdateable, Engine.IDrawable
    {
        private readonly RocksLayer[] _parallaxLayers = new RocksLayer[3];

        public Background(Game game)
        {
            for (int i = 0; i < _parallaxLayers.Length; i++)
            {
                _parallaxLayers[i] = new RocksLayer(new Vector2((float)i / _parallaxLayers.Length, 1f));
                _parallaxLayers[i].LoadContent(game);
            }

        }

        public void Update(GameTime gameTime)
        {
            foreach (RocksLayer layer in _parallaxLayers)
            {
                layer.Update(gameTime);
            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (RocksLayer layer in _parallaxLayers)
            {
                layer.Draw(spriteBatch, InfiniteIsland.Camera);
            }
        }
    }
}