using InfiniteIsland.Game.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace InfiniteIsland.Game.Entity
{
    public class EntitiesManager
    {
        private Player _player;
        private SpriteBatch _spriteBatch;

        public void LoadContent(Microsoft.Xna.Framework.Game game)
        {
            _spriteBatch = new SpriteBatch(game.GraphicsDevice);

            _player = new Player(game.Content);
        }

        public void Update(GameTime gameTime)
        {
            _player.Update(gameTime);
            Camera.LookAt(_player.Position.ToPixels());
        }

        public void Draw()
        {
            _spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null,
                               Camera.CalculateTransformMatrix(Vector2.One));

            _player.Draw(_spriteBatch);

            _spriteBatch.End();
        }
    }
}