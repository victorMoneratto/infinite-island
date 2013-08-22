using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Contacts;
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
        private const float MaxSpeed = 80f;
        private static Sprite _sprite;

        private static SoundEffect _coinConsumeSound, _jumpSound;

        public HumanoidBody Body;

        public Player()
        {
            Body = new HumanoidBody(
                world: InfiniteIsland.World,
                dimensions: _sprite.Animation.MaxDimensions.ToMeters() * .9f,
                userData: this);
            Body.Torso.OnCollision += OnCollision;
        }

        public bool OnCollision(Fixture f1, Fixture f2, Contact contact)
        {
            var entity = f2.UserData as Engine.Entity.Entity;
            if (entity == null)
                return false;

            if (entity is Coin)
            {
                ++InfiniteIsland.Coins;
                _coinConsumeSound.Play(1f, 1f, 0f);
                InfiniteIsland.Entities.Coins.Remove(entity as Coin);
                return false;
            }

            return true;
        }

        public override void Update(GameTime gameTime)
        {
            Body.Motor.MotorSpeed = 0;

            if (Input.Keyboard.IsKeyDown(Keys.D))
            {
                Body.Motor.MotorSpeed += MaxSpeed;
                _sprite.Key = AnimationKey.Walk;
            }

            if (Input.Keyboard.IsKeyDown(Keys.A))
            {
                Body.Motor.MotorSpeed -= MaxSpeed;
                _sprite.Key = AnimationKey.Walk;
            }

            if (Input.Keyboard.IsKeyTyped(Keys.Space))
            {
                _jumpSound.Play(1f, 1f, 0f);
                Body.Torso.ApplyLinearImpulse(new Vector2(0, -15));
            }

            if (Body.Motor.MotorSpeed == 0)
            {
                _sprite.Key = AnimationKey.Stand;
            }
            else
            {
                _sprite.Flip = (Body.Motor.MotorSpeed < 0) ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            }

            _sprite.Update(gameTime);
            _sprite.Body.TopRight = Body.TopMiddle.ToPixels() + Vector2.UnitX * _sprite.Body.Dimensions.X/2f;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            _sprite.Draw(spriteBatch);
        }

        public static void LoadContent(ContentManager content)
        {
            _coinConsumeSound = content.Load<SoundEffect>("sfx/coin");
            _jumpSound = content.Load<SoundEffect>("sfx/jump");

            _sprite = new Sprite(content.Load<Animation>("sprite/p2"));
        }

        private struct AnimationKey
        {
            public const string Walk = "walk";
            public const string Stand = "stand";
            public const string Jump = "jump";
        }
    }
}