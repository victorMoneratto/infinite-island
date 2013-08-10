using InfiniteIsland.Engine;
using InfiniteIsland.Engine.Math;
using InfiniteIsland.Engine.Physics;
using InfiniteIsland.Engine.Visual;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace InfiniteIsland.Entity
{
    public class Player : Engine.Entity.Entity
    {
        private readonly Sprite<State> _sprite;

        public WalkerBody Body;

        public float MaxSpeed = 80f;

        public Player(ContentManager content)
        {
            Body = new WalkerBody(InfiniteIsland.World);
            _sprite = new Sprite<State>(content.Load<Texture2D>(@"img\alabama"), new Vector2(100, 176));
            _sprite.RegisterAnimation(State.Idle, new Point(0, 0));
            _sprite.RegisterAnimation(State.Moving, 28, 14);
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
                Body.Torso.ApplyLinearImpulse(new Vector2(0, -24));
            }

            if (Body.Motor.MotorSpeed == 0)
            {
                _sprite.AnimationKey = State.Idle;
            }
            else
                _sprite.FlipHorizontally = Body.Motor.MotorSpeed < 0;

            _sprite.Update(gameTime);
            _sprite.Body.Center = Body.Torso.Position.ToPixels();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            _sprite.Draw(spriteBatch);
        }

        private enum State
        {
            Idle,
            Moving
        };
    }
}