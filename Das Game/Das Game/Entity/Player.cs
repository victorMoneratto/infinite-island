using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Joints;
using FarseerPhysics.Factories;
using InfiniteIsland.Game.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace InfiniteIsland.Game.Entity
{
    internal class Player : Entity
    {
        private const float MaxSpeed = 20f;
        private readonly RevoluteJoint _motor;
        private readonly Sprite<State> _sprite;
        private readonly Body _torso;

        private State _currentState = State.Idle;

        public Player(ContentManager content)
        {
            float heightMeters = MeasureUtil.ToMeters(102);
            float widthMeters = MeasureUtil.ToMeters(24);
            float torsoHeight = heightMeters - widthMeters/2f;

            _torso = BodyFactory.CreateRectangle(
                world: InfiniteIsland.World,
                width: widthMeters,
                height: torsoHeight,
                density: 1,
                position: Vector2.One);

            _torso.BodyType = BodyType.Dynamic;

            Body wheel = BodyFactory.CreateCircle(
                world: InfiniteIsland.World,
                radius: widthMeters/1.9f,
                density: 1,
                position: _torso.Position + new Vector2(0, torsoHeight/2));

            wheel.BodyType = BodyType.Dynamic;
            wheel.Friction = float.MaxValue;
            wheel.Restitution = 0;

            JointFactory.CreateFixedAngleJoint(InfiniteIsland.World, _torso);

            _motor = JointFactory.CreateRevoluteJoint(InfiniteIsland.World, _torso, wheel, Vector2.Zero);
            _motor.MotorEnabled = true;
            _motor.MaxMotorTorque = 10;

            _sprite = new Sprite<State>(content.Load<Texture2D>("cowboy"), 9, 1)
                {
                    OffsetFromCenter = MeasureUtil.ToPixels(new Vector2(widthMeters/3, widthMeters/4))
                };

            _sprite.SetAnimation(State.Idle, new Point(0, 0));
            _sprite.SetAnimation(State.Moving, 0);
        }

        //Not precise position, but pretty close
        public Vector2 Position { get { return _torso.Position; } }

        public override void Update(GameTime gameTime)
        {
            //Tweening for velocity?

            _motor.MotorSpeed = 0;

            if (Input.IsKeyDown(Keys.D))
            {
                _motor.MotorSpeed += MaxSpeed;
                _sprite.AnimationKey = State.Moving;
            }

            if (Input.IsKeyDown(Keys.A))
            {
                _motor.MotorSpeed -= MaxSpeed;
                _sprite.AnimationKey = State.Moving;
            }

            if (Input.IsKeyPressed(Keys.Space))
            {
                _torso.ApplyLinearImpulse(new Vector2(0, -6));
            }

            if(_motor.MotorSpeed == 0)
                _sprite.AnimationKey = State.Idle;

            _sprite.Update(gameTime, MeasureUtil.ToPixels(_torso.Position));

            //Completely temporary
            if ((_motor.MotorSpeed < 0 && _sprite.OffsetFromCenter.X > 0)
               || _motor.MotorSpeed > 0 && _sprite.OffsetFromCenter.X < 0)
                _sprite.OffsetFromCenter = new Vector2(-1, 1) * _sprite.OffsetFromCenter;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //flipHorizontally should be replaced
            _sprite.Draw(spriteBatch, _motor.MotorSpeed < 0);
        }
    }
}