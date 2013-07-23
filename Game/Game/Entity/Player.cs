using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Joints;
using FarseerPhysics.Factories;
using InfiniteIsland.Sprite;
using InfiniteIsland.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace InfiniteIsland.Entity
{
    public class Player : Entity
    {
        private const float MaxSpeed = 80f;
        private readonly RevoluteJoint _motor;
        private readonly Sprite<State> _sprite;
        private readonly Body _torso;

        public Player(World world, ContentManager content)
        {
            float heightMeters = 140f.ToMeters();
            float widthMeters = 45f.ToMeters();
            float torsoHeight = heightMeters - widthMeters / 2f;

            _torso = BodyFactory.CreateRectangle(
                world: world,
                width: widthMeters,
                height: torsoHeight,
                density: 1f,
                position: Vector2.UnitX * 10);

            _torso.BodyType = BodyType.Dynamic;

            Body wheel = BodyFactory.CreateCircle(
                world: world,
                radius: widthMeters / 1.9f,
                density: 1f,
                position: _torso.Position + new Vector2(0, torsoHeight / 2));

            wheel.BodyType = BodyType.Dynamic;
            wheel.Friction = float.MaxValue;
            wheel.Restitution = float.MinValue;

            JointFactory.CreateFixedAngleJoint(world, _torso);

            _motor = JointFactory.CreateRevoluteJoint(world, _torso, wheel, Vector2.Zero);
            _motor.MotorEnabled = true;
            _motor.MaxMotorTorque = 10;

            _sprite = new Sprite<State>(content.Load<Texture2D>(@"img\alabama"), new Vector2(100, 176));
            _sprite.RegisterAnimation(State.Idle, new Point(0, 0));
            _sprite.RegisterAnimation(State.Moving, 28, 14);
        }


        public Vector2 Position
        {
            //Not precise position, but pretty close
            get { return _torso.Position; }
        }

        public override void Update(GameTime gameTime)
        {
            //Tweening for velocity?

            _motor.MotorSpeed = 0;

            if (Input.Keyboard.IsKeyDown(Keys.D))
            {
                _motor.MotorSpeed += MaxSpeed;
                _sprite.AnimationKey = State.Moving;
            }

            if (Input.Keyboard.IsKeyDown(Keys.A))
            {
                _motor.MotorSpeed -= MaxSpeed;
                _sprite.AnimationKey = State.Moving;
            }

            if (Input.Keyboard.IsKeyTyped(Keys.Space))
            {
                _torso.ApplyLinearImpulse(new Vector2(0, -6));
            }

            if (_motor.MotorSpeed == 0)
                _sprite.AnimationKey = State.Idle;

            if (Input.Keyboard.IsKeyDown(Keys.Q))
            {
                _sprite.Body.Rotation += MathHelper.Pi*gameTime.ElapsedGameTime.Milliseconds * 1e-3f;
            }

            if (Input.Keyboard.IsKeyDown(Keys.E))
            {
                _sprite.Body.Rotation -= MathHelper.Pi * gameTime.ElapsedGameTime.Milliseconds * 1e-3f;
            }

            _sprite.Update(gameTime);
            _sprite.Body.Center= _torso.Position.ToPixels();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            _sprite.Draw(spriteBatch, _motor.MotorSpeed < 0);
        }

        private enum State
        {
            Idle,
            Moving
        };
    }
}