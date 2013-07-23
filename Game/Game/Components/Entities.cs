using InfiniteIsland.Engine;
using InfiniteIsland.Engine.Math;
using InfiniteIsland.Entity;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using IDrawable = InfiniteIsland.Engine.Interface.IDrawable;
using IUpdateable = InfiniteIsland.Engine.Interface.IUpdateable;

namespace InfiniteIsland.Components
{
    public class Entities : IUpdateable, IDrawable
    {
        public readonly Player Player;

        public Entities(Player player)
        {
            Player = player;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null,
                              InfiniteIsland.Camera.CalculateTransformMatrix(Vector2.One));

            Player.Draw(spriteBatch);

            spriteBatch.End();
        }

        public void Update(GameTime gameTime)
        {
            Player.Update(gameTime);
            InfiniteIsland.Camera.Viewport.Center =
                Player.Position.ToPixels() + Vector2.UnitX*.3f*InfiniteIsland.Camera.Viewport.Dimensions;
        }
    }
}