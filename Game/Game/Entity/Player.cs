using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Contacts;
using InfiniteIsland.Component;
using InfiniteIsland.Engine;
using InfiniteIsland.Engine.Math;
using InfiniteIsland.Engine.Physics;
using InfiniteIsland.Engine.Terrain;
using InfiniteIsland.Engine.Visual;
using InfiniteIsland.State;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace InfiniteIsland.Entity
{
    public class Player
    {
        private const float MaxSpeed = 40f;
        private static Sprite _sprite;

        private static SoundEffect _coinConsumeSound, _jumpSound, _hurtSound;

        public readonly HumanoidBody Body;
        private readonly Play _play;
        public Player(World world, Play play)
        {
            _play = play;
            Body = new HumanoidBody(
                world: world,
                dimensions: _sprite.Animation.MaxDimensions.ToMeters()*.9f,
                userData: this);

            Body.Torso.OnCollision += OnCollision;
            Body.Wheel.OnCollision += Wheel_OnCollision;

            Body.Motor.MotorSpeed = MaxSpeed;
            _sprite.Key = AnimationKeys.Walk;
        }

        bool Wheel_OnCollision(Fixture fixtureA, Fixture fixtureB, Contact contact)
        {
            return (fixtureB.Body.UserData is TerrainChunk);
        }

        private bool OnCollision(Fixture f1, Fixture f2, Contact contact)
        {
            Coin coin = f2.UserData as Coin;
            if (coin == null)
                return false;

            if (coin.Type == Coin.AnimationKeys.Good)
            {
                ++_play.Coins;
                _coinConsumeSound.Play(1f, 1f, 0f);
            }
            else
            {
                float factor = _play.Factor;
                Wait.Until(time =>
                    Tweening.Tween(
                        start: factor,
                        end: factor - 1/5f,
                        progress: time.Alive/.6f,
                        step: value => _play.Factor = value,
                        scale: TweenScales.Quadratic));

                _hurtSound.Play(1, 1f, 0);
            }
            _play.Entities.Coins.Remove(coin, _play.World);
            return false;
        }

        public void Update(GameTime gameTime)
        {
            if (Input.Keyboard.IsKeyTyped(Keys.Space))
            {
                _jumpSound.Play(1f, 1f, 0f);
                Body.Torso.ApplyLinearImpulse(new Vector2(0, -20));
            }

            _sprite.Update(gameTime);
            _sprite.Body.TopRight = Body.TopMiddle.ToPixels() + new Vector2(_sprite.Body.Dimensions.X/2f, 0);
        }

        public void Draw(SpriteBatch spriteBatch, CameraOperator cameraOperator)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null,
                cameraOperator.Camera.CalculateTransformMatrix(Vector2.One));
            _sprite.Draw(spriteBatch);
            spriteBatch.End();
        }

        public static void LoadContent(ContentManager content)
        {
            _coinConsumeSound = content.Load<SoundEffect>("sfx/coin");
            _jumpSound = content.Load<SoundEffect>("sfx/jump");
            _hurtSound = content.Load<SoundEffect>("sfx/elements08");

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