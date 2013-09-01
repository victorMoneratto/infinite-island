﻿using System.Collections.Generic;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using InfiniteIsland.Engine.Math;
using InfiniteIsland.Engine.Visual;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using IUpdateable = InfiniteIsland.Engine.Interface.IUpdateable;

namespace InfiniteIsland.Entity
{
    internal class Slimes : IUpdateable
    {
        private readonly List<Slime> _slimes;

        public Slimes()
        {
            _slimes = new List<Slime> {new Slime()};
        }

        public void Update(GameTime gameTime)
        {
            foreach (Slime slime in _slimes)
            {
                slime.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Slime slime in _slimes)
            {
                slime.Draw(spriteBatch);
            }
        }

        public static void LoadContent(ContentManager content)
        {
            Slime.LoadContent(content);
        }
    }

    internal class Slime : Engine.Entity.Entity
    {
        private const float Scale = 2f;
        private static Animation _animation;

        private readonly Body _body;
        private readonly Sprite _sprite;

        public Slime()
        {
            _sprite = new Sprite(_animation)
                {
                    FramesPerSecond = 10,
                    Flip = SpriteEffects.FlipHorizontally,
                    Body = {Scale = new Vector2(Scale)},
                    Key = "walk"
                };

            //_body = BodyFactory.CreateRectangle(
            //    world: InfiniteIsland.World,
            //    width: _sprite.Body.Dimensions.X.ToMeters() * _sprite.Body.Scale.X,
            //    height: _sprite.Body.Dimensions.Y.ToMeters() * _sprite.Body.Scale.Y,
            //    density: 1f,
            //    position: new Vector2(10, 0));

            _body = BodyFactory.CreateCircle(world: InfiniteIsland.World, radius: _sprite.Body.Dimensions.X.ToMeters()*.75f, density:.5f,
                                             position:new Vector2(10, 0));

            _body.BodyType = BodyType.Dynamic;

            _body.OnCollision += (a, b, contact) =>
                {
                    //Engine.Entity.Entity player = b.UserData as Player;
                    //if (player != null)
                    //    _body.ApplyAngularImpulse(-1);
                    return true;
                };
        }

        public override void Update(GameTime gameTime)
        {
            _body.ApplyLinearImpulse(.1f*Vector2.UnitX);
            _sprite.Body.Center = _body.Position.ToPixels();
            //_sprite.Body.Rotation = _body.Rotation;
            _sprite.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            _sprite.Draw(spriteBatch);
        }

        public static void LoadContent(ContentManager content)
        {
            _animation = content.Load<Animation>("sprite/slime");
        }
    }
}