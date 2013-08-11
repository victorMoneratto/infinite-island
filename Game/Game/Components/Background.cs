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
        private readonly RocksLayer[] _parallaxLayers = new RocksLayer[3];

        public Background()
        {
            for (int i = 0; i < _parallaxLayers.Length; i++)
            {
                _parallaxLayers[i] = new RocksLayer(new Vector2((float) i/_parallaxLayers.Length, 1f));
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (RocksLayer layer in _parallaxLayers)
            {
                layer.Draw(spriteBatch, InfiniteIsland.Camera);
            }
        }

        public void Update(GameTime gameTime)
        {
            foreach (RocksLayer layer in _parallaxLayers)
            {
                layer.Update(gameTime);
            }
        }

        public static void LoadContent(ContentManager content)
        {
            RocksLayer.LoadContent(content);
        }
    }
}