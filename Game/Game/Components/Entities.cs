using InfiniteIsland.Engine;
using InfiniteIsland.Engine.Math;
using InfiniteIsland.Entity;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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

        //Factor is the % of 'completition'
        private float _factor = 1f;
        //There should be something (a class, maybe?) to update this Tweening.
        private float? _nextFactor;

        public void Update(GameTime gameTime)
        {
            if (Input.Keyboard.IsKeyTyped(Keys.Tab))
                _nextFactor = _factor - 1/2f;
            if (_nextFactor.HasValue)
            {
                if (System.Math.Abs(_factor - _nextFactor.Value) > .01f)
                    _factor += (_nextFactor.Value - _factor) * .05f;
                else
                {
                    _factor = _nextFactor.Value;
                    _nextFactor = null;
                }
            }

            Player.Update(gameTime);
            InfiniteIsland.Camera.Viewport.Center =
                Player.Position.ToPixels() -
                Vector2.UnitX * .4f *(2f*_factor - 1) * InfiniteIsland.Camera.Viewport.Dimensions;
        }
    }
}