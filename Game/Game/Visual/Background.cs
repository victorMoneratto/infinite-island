using InfiniteIsland.Game.Util;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace InfiniteIsland.Game.Visual
{
    class Background
    {
        private SpriteBatch _spriteBatch;
        private List<Layer> _layers;

        private NewRectangle _rect = new NewRectangle()
        {
            Dimension = Vector2.One,
        };

        public Background()
        {
            new Layer();
        }

        public void LoadContent(Microsoft.Xna.Framework.Game game)
        {
            _spriteBatch = new SpriteBatch(game.GraphicsDevice);
        }

        public void Update(GameTime gameTime)
        {
            _rect.Center = InfiniteIsland.EntitiesManager.Player.Position;

        }

        public void Draw()
        {
        }
    }
}
