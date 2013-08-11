using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Contacts;
using InfiniteIsland.Engine;
using InfiniteIsland.Engine.Interface;
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
    public class Player : Engine.Entity.Entity, ICustomCollidable
    {
        private static Sprite<State> _sprite;

        private static SoundEffect _coinSound, _jumpSound;
        private static Texture2D _alabama;

        public WalkerBody Body;

        private const float MaxSpeed = 80f;

        public Player()
        {
            Body = new WalkerBody(InfiniteIsland.World, 45f.ToMeters(), 140f.ToMeters(), this);
            Body.Torso.OnCollision += OnCollision;
        }

        public override void Update(GameTime gameTime)
        {
            Body.Motor.MotorSpeed = 0;

            if (Input.Keyboard.IsKeyDown(Keys.D))
            {
                Body.Motor.MotorSpeed += MaxSpeed;
                _sprite.AnimationKey = State.Moving;
            }

            if (Input.Keyboard.IsKeyDown(Keys.A))
            {
                Body.Motor.MotorSpeed -= MaxSpeed;
                _sprite.AnimationKey = State.Moving;
            }

            if (Input.Keyboard.IsKeyTyped(Keys.Space))
            {
                _jumpSound.Play();
                Body.Torso.ApplyLinearImpulse(new Vector2(0, -24));
            }

            if (Body.Motor.MotorSpeed == 0)
            {
                _sprite.AnimationKey = State.Idle;
            }
            else
            {
                _sprite.FlipHorizontally = Body.Motor.MotorSpeed < 0;
            }

            _sprite.Update(gameTime);
            _sprite.Body.Center = Body.Torso.Position.ToPixels();
        }

        public bool OnCollision(Fixture f1, Fixture f2, Contact contact)
        {
            var entity = f2.UserData as Engine.Entity.Entity;
            if (entity == null)
                return false;

            if (entity is Coin)
            {
                _coinSound.Play();
            }

            return true;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            _sprite.Draw(spriteBatch);
        }

        public static void LoadContent(ContentManager content)
        {
            _coinSound = content.Load<SoundEffect>("sfx/coin");
            _jumpSound = content.Load<SoundEffect>("sfx/jump");

            _alabama = content.Load<Texture2D>("img/alabama");
            _sprite = new Sprite<State>(_alabama, new Vector2(100, 176));
            _sprite.RegisterAnimation(State.Idle, new Point(0, 0));
            _sprite.RegisterAnimation(State.Moving, 28, 14);
        }

        private enum State
        {
            Idle,
            Moving
        };
    }
}