﻿using System;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Contacts;
using InfiniteIsland.Component;
using InfiniteIsland.Engine;
using InfiniteIsland.Engine.Math;
using InfiniteIsland.Engine.Physics;
using InfiniteIsland.Engine.Visual;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace InfiniteIsland.Entity
{
    public class Player : Engine.Entity.Entity
    {
        private const float MaxSpeed = 40f;
        private static Sprite _sprite;

        private static SoundEffect _coinConsumeSound, _jumpSound, _hurtSound;

        public readonly HumanoidBody Body;

        public Player()
        {
            Body = new HumanoidBody(
                world: InfiniteIsland.World,
                dimensions: _sprite.Animation.MaxDimensions.ToMeters()*.9f,
                userData: this);

            Body.Torso.OnCollision += OnCollision;

            Body.Motor.MotorSpeed = MaxSpeed;
            _sprite.Key = AnimationKeys.Walk;
        }

        private bool OnCollision(Fixture f1, Fixture f2, Contact contact)
        {
            var entity = f2.UserData as Engine.Entity.Entity;
            if (entity == null)
                return false;

            if (entity is Coin)
            {
                if (((Coin) entity).Type == Coin.AnimationKeys.Good)
                {
                    ++InfiniteIsland.Coins;
                    _coinConsumeSound.Play(1f, 1f, 0f);
                }
                else
                {
                    float factor = InfiniteIsland.Factor;
                    Wait.Until(time =>
                               Tweening.Tween(
                                   start: factor,
                                   end: factor - 1/3f,
                                   progress: time/.5f,
                                   step: value => InfiniteIsland.Factor = value,
                                   scale: TweenScales.Quadratic));

                    _hurtSound.Play();
                }
                Entities.Instance.Coins.Remove(entity as Coin);
                return false;
            }

            return true;
        }

        public override void Update(GameTime gameTime)
        {
            if (Input.Keyboard.IsKeyTyped(Keys.Space))
            {
                _jumpSound.Play(1f, 1f, 0f);
                Body.Torso.ApplyLinearImpulse(new Vector2(0, -15));
            }

            _sprite.Update(gameTime);
            _sprite.Body.TopRight = Body.TopMiddle.ToPixels() + new Vector2(_sprite.Body.Dimensions.X/2f, 0);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null,
                              CameraOperator.Instance.Camera.CalculateTransformMatrix(Vector2.One));
            _sprite.Draw(spriteBatch);
            spriteBatch.End();
        }

        public static void LoadContent(ContentManager content)
        {
            _coinConsumeSound = content.Load<SoundEffect>("sfx/coin");
            _jumpSound = content.Load<SoundEffect>("sfx/jump");
            _hurtSound = content.Load<SoundEffect>("sfx/hurt");

            _sprite = new Sprite(content.Load<Animation>("sprite/p3"));
        }

        private struct AnimationKeys
        {
            public const string Walk = "walk";
            public const string Stand = "stand";
            public const string Jump = "jump";
        }
    }
}