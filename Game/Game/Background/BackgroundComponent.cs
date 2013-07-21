using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace InfiniteIsland.Background
{
    public class BackgroundComponent : IUpdateable, IRenderable
    {
        private readonly MountLayer[] _layers = new MountLayer[3];

        public BackgroundComponent(Game game)
        {
            for (int i = 0; i < _layers.Length; i++)
            {
                _layers[i] = new MountLayer(new Vector2((float)i / _layers.Length, 1f));
                _layers[i].LoadContent(game);
            }

        }

        public void Update(GameTime gameTime)
        {
            foreach (MountLayer layer in _layers)
            {
                layer.Update(gameTime);
            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (MountLayer layer in _layers)
            {
                layer.Draw(spriteBatch);
            }
        }
    }
}