using InfiniteIsland.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace InfiniteIsland.Entity
{
    public class EntityComponent : IUpdateable, IRenderable
    {
        private readonly Player _player;

        public EntityComponent(Player player, Game game)
        {
            _player = player;
        }

        public void Update(GameTime gameTime)
        {
            _player.Update(gameTime);
            Camera.LookAt(_player.Position.ToPixels() + (Vector2.UnitX * .3f*Camera.Dimensions));
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null,
                               Camera.CalculateTransformMatrix(Vector2.One));

            _player.Draw(spriteBatch);

            spriteBatch.End();
        }
    }
}