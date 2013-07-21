using InfiniteIsland.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace InfiniteIsland.Entity
{
    public class EntityComponent : IUpdateable, IRenderable
    {
        public readonly Player Player;

        public EntityComponent(Player player)
        {
            Player = player;
        }

        public void Update(GameTime gameTime)
        {
            Player.Update(gameTime);
            Camera.Center = Player.Position.ToPixels();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null,
                               Camera.CalculateTransformMatrix(Vector2.One));

            Player.Draw(spriteBatch);

            spriteBatch.End();
        }
    }
}