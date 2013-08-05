using System;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using InfiniteIsland.Engine;
using InfiniteIsland.Engine.Math;
using InfiniteIsland.Entity;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using IDrawable = InfiniteIsland.Engine.IDrawable;
using IUpdateable = InfiniteIsland.Engine.IUpdateable;

namespace InfiniteIsland.Components
{
    public class Entities : IUpdateable, IDrawable
    {
        public readonly Player Player;
        private readonly Body[] _bodies = new Body[200];
        private readonly Random _random = new Random();
        private int _bodyIndex;

        //Factor is the % of 'completition'
        private float _factor = 1f;
        //There should be something (a class, maybe?) to update this Tweening.
        private float? _nextFactor;

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

        //FOR THE LULZ

        public void Update(GameTime gameTime)
        {
            if (Input.Keyboard.IsKeyTyped(Keys.Tab))
                _nextFactor = _factor - 1/2f;
            if (_nextFactor.HasValue)
            {
                if (Math.Abs(_factor - _nextFactor.Value) > .01f)
                    _factor += (_nextFactor.Value - _factor)*.05f;
                else
                {
                    _factor = _nextFactor.Value;
                    _nextFactor = null;
                }
            }

            Player.Update(gameTime);
            InfiniteIsland.Camera.Viewport.Center =
                Player.Position.ToPixels() -
                Vector2.UnitX*.4f*(2f*_factor - 1)*InfiniteIsland.Camera.Viewport.Dimensions;

            if (Input.Mouse.IsButtonDown(Input.Mouse.MouseButton.Left))
            {
                Vector2 mousePos = Vector2.Transform(
                    position:
                        Input.Mouse.Position,
                    matrix:
                        Matrix.Invert(InfiniteIsland.Camera.CalculateTransformMatrix(Vector2.One))).ToMeters();
                if (_bodies[_bodyIndex] != null) InfiniteIsland.World.RemoveBody(_bodies[_bodyIndex]);
                _bodies[_bodyIndex] = BodyFactory.CreateCircle(InfiniteIsland.World, .2f, 0f, mousePos);
                _bodies[_bodyIndex].BodyType = BodyType.Dynamic;
                _bodyIndex = (_bodyIndex + 1)%_bodies.Length;
            }
        }
    }
}