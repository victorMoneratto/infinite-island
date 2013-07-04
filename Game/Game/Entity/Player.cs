using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Joints;
using FarseerPhysics.Factories;
using InfiniteIsland.Game.Util;
using InfiniteIsland.Game.Visual;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace InfiniteIsland.Game.Entity
{
    internal class Player : Entity
    {
        private const float MaxSpeed = 80f;
        private readonly RevoluteJoint _motor;
        private readonly Sprite<State> _sprite;
        private readonly Body _torso;

        public Player(ContentManager content)
        {
            float heightMeters = 150f.ToMeters();
            float widthMeters = 45f.ToMeters();
            float torsoHeight = heightMeters - widthMeters / 2f;

            _torso = BodyFactory.CreateRectangle(
                world: InfiniteIsland.World,
                width: widthMeters,
                height: torsoHeight,
                density: 2f,
                position: Vector2.One);

            _torso.BodyType = BodyType.Dynamic;

            Body wheel = BodyFactory.CreateCircle(
                world: InfiniteIsland.World,
                radius: widthMeters / 1.9f,
                density: 2f,
                position: _torso.Position + new Vector2(0, torsoHeight / 2));

            wheel.BodyType = BodyType.Dynamic;
            wheel.Friction = float.MaxValue;
            wheel.Restitution = float.MinValue;

            JointFactory.CreateFixedAngleJoint(InfiniteIsland.World, _torso);

            _motor = JointFactory.CreateRevoluteJoint(InfiniteIsland.World, _torso, wheel, Vector2.Zero);
            _motor.MotorEnabled = true;
            _motor.MaxMotorTorque = 10;

            _sprite = new Sprite<State>(content.Load<Texture2D>(@"img\alabama"), new Point(100, 176));
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

            _sprite.Update(gameTime, _torso.Position.ToPixels());
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