using InfiniteIsland.Game.Entity;
using InfiniteIsland.Game.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace InfiniteIsland.Game.Component
{
    public class EntityComponent
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
            InfiniteIsland.Camera.LookAt(MeasureUtil.ToPixels(_player.Position));
        }

        public void Draw()
        {
            _spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, InfiniteIsland.Camera.CalculateViewMatrix(Vector2.One));
            //_spriteBatch.Begin();
            _player.Draw(_spriteBatch);
            _spriteBatch.End();
        }
    }
}