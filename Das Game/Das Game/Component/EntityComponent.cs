using DasGame.Entity;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DasGame.Component
{
    public class EntityComponent
    {
        private Player _player;
        private SpriteBatch _spriteBatch;

        public void LoadContent(Game game)
        {
            _spriteBatch = new SpriteBatch(game.GraphicsDevice);

            _player = new Player(game.Content);
        }

        public void Update(GameTime gameTime)
        {
            _player.Update(gameTime);
        }

        public void Draw()
        {
            _spriteBatch.Begin();
            _player.Draw(_spriteBatch);
            _spriteBatch.End();
        }
    }
}