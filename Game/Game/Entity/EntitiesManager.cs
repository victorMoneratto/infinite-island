using InfiniteIsland.Game.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace InfiniteIsland.Game.Entity
{
    public class EntitiesManager
    {
        public Player Player;
        private SpriteBatch _spriteBatch;

        public void LoadContent(Microsoft.Xna.Framework.Game game)
        {
            _spriteBatch = new SpriteBatch(game.GraphicsDevice);

            Player = new Player(game.Content);
        }

        public void Update(GameTime gameTime)
        {
            Player.Update(gameTime);
            Camera.LookAt(Player.Position.ToPixels() + (Vector2.UnitX * Camera.Dimensions.X * .3f));
        }

        public void Draw()
        {
            _spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null,
                               Camera.CalculateTransformMatrix(Vector2.One));

            Player.Draw(_spriteBatch);

            _spriteBatch.End();
        }
    }
}