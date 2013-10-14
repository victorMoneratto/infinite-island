using System.Collections.Generic;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using InfiniteIsland.Component;
using InfiniteIsland.Engine;
using InfiniteIsland.Engine.Math;
using InfiniteIsland.Engine.Visual;
using InfiniteIsland.State;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace InfiniteIsland.Entity
{
    public class Slimes
    {
        public readonly List<Slime> SlimeList;

        public Slimes(World world, Play play)
        {
            SlimeList = new List<Slime>();
            Wait.Until(time =>
            {
                if (time.Alive >= 2f)
                    if (InfiniteIsland.Random.Next(4) == 0)
                    {
                        SlimeList.Add(new Slime(world, play));
                        return true;
                    }
                return false;
            }, null, -1);
        }

        public void Update(GameTime gameTime)
        {
            foreach (Slime slime in SlimeList)
            {
                slime.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch, CameraOperator cameraOperator)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null,
                cameraOperator.Camera.CalculateTransformMatrix(Vector2.One));
            foreach (Slime slime in SlimeList)
            {
                slime.Draw(spriteBatch);
            }
            spriteBatch.End();
        }

        public static void LoadContent(ContentManager content)
        {
            Slime.LoadContent(content);
        }
    }

    public class Slime
    {
        private const float Scale = 2f;
        private static Animation _animation;

        private readonly Body _body;
        private readonly Sprite _sprite;

        private readonly Play _play;
        private static SoundEffect _hitSound;
        private static SoundEffect _hurtSound;

        public Slime(World world, Play play)
        {
            _play = play;
            _sprite = new Sprite(_animation)
            {
                FramesPerSecond = 10,
                Flip = SpriteEffects.FlipHorizontally,
                Body = {Scale = new Vector2(Scale)},
                Key = "walk"
            };

            _body = BodyFactory.CreateCircle(world: world,
                radius: .5f, density: .5f,
                position: play.Entities.Player.Body.TopMiddle - new Vector2(25, 0));

            _body.BodyType = BodyType.Dynamic;
            _body.LinearVelocity = new Vector2(play.Entities.Player.Body.Motor.MotorSpeed * .5f, 0);
            _body.OnCollision += _body_OnCollision;
        }

        bool _body_OnCollision(Fixture fixtureA, Fixture fixtureB, FarseerPhysics.Dynamics.Contacts.Contact contact)
        {
            Player player = fixtureB.UserData as Player;
            if (player == null) return true;
            _hurtSound.Play(1, 1f, 0);
            _body.LinearVelocity = new Vector2(0, -5);
            _body.IgnoreGravity = true;
            _body.CollidesWith = Category.None;
            Wait.Until(
                time =>
                {
                    _sprite.Body.Rotation = 2*time.Alive*MathHelper.TwoPi;
                    _sprite.Body.Scale = new Vector2(1 - 2*time.Alive);
                    return time.Alive >= .5f;
                },
                () =>
                {
                    _hitSound.Play();
                    _play.Entities.Slimes.SlimeList.Remove(this);
                    _play.World.RemoveBody(_body);
                });
            float factor = _play.Factor;
            Wait.Until(gameTime =>
                Tweening.Tween(
                    start: factor,
                    end: factor - 1 / 5f,
                    progress: gameTime.Alive / .6f,
                    step: value => _play.Factor = value,
                    scale: TweenScales.Quadratic));
            return false;
        }

        public void Update(GameTime gameTime)
        {
            _body.ApplyLinearImpulse(.1f*Vector2.UnitX);
            _sprite.Body.Center = _body.Position.ToPixels() + Vector2.UnitY*10;
            _sprite.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _sprite.Draw(spriteBatch);
        }

        public static void LoadContent(ContentManager content)
        {
            _hitSound = content.Load<SoundEffect>("sfx/hit");
            _animation = content.Load<Animation>("sprite/slime");
            _hurtSound = content.Load<SoundEffect>("sfx/elements08");
        }
    }
}