using InfiniteIsland.Engine;
using InfiniteIsland.Entity;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using IDrawable = InfiniteIsland.Engine.Interface.IDrawable;
using IUpdateable = InfiniteIsland.Engine.Interface.IUpdateable;

namespace InfiniteIsland.Components
{
    internal class Entities : IUpdateable, IDrawable
    {
        public readonly Coins Coins;
        public readonly Player Player;

        public Entities()
        {
            Player = new Player();
            Coins = new Coins();
        }

        public void Draw(SpriteBatch spriteBatch, Camera camera)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null,
                              camera.CalculateTransformMatrix(Vector2.One));

            Coins.Draw(spriteBatch, camera);
            Player.Draw(spriteBatch);

            spriteBatch.End();
        }

        public void Update(GameTime gameTime)
        {
            //if (Input.Keyboard.IsKeyTyped(Keys.Tab))
            //    _nextFactor = _factor - 1/2f;
            //if (_nextFactor.HasValue)
            //{
            //    if (Math.Abs(_factor - _nextFactor.Value) > .01f)
            //        _factor += (_nextFactor.Value - _factor)*.05f;
            //    else
            //    {
            //        _factor = _nextFactor.Value;
            //        _nextFactor = null;
            //    }
            //}

            Coins.Update(gameTime);
            Player.Update(gameTime);
        }

        public static void LoadContent(ContentManager content)
        {
            Coins.LoadContent(content);
            Player.LoadContent(content);
        }
    }
}