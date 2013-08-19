using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Joints;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;

namespace InfiniteIsland.Engine.Physics
{
    public class WalkerBody
    {
        public readonly RevoluteJoint Motor;
        public readonly Body Torso;
        public readonly Body Wheel;

        private readonly float _wheelRadius;

        public WalkerBody(World world, Vector2 dimensions, object userData)
        {
            _wheelRadius = dimensions.X/1.9f;
            float torsoHeight = dimensions.Y - dimensions.X/2f;

            Torso = BodyFactory.CreateRectangle(
                world: world,
                width: dimensions.X,
                height: torsoHeight,
                density: 1f,
                position: Vector2.UnitX*20,
                userData: userData);

            Torso.BodyType = BodyType.Dynamic;

            Wheel = BodyFactory.CreateCircle(
                world: world,
                radius: _wheelRadius,
                density: 1f,
                position: Torso.Position + new Vector2(0, torsoHeight/2),
                userData: userData);

            Wheel.BodyType = BodyType.Dynamic;
            Wheel.Friction = float.MaxValue;
            Wheel.Restitution = float.MinValue;

            JointFactory.CreateFixedAngleJoint(world, Torso);

            Motor = JointFactory.CreateRevoluteJoint(world, Torso, Wheel, Vector2.Zero);
            Motor.MotorEnabled = true;
            Motor.MaxMotorTorque = 10;
        }

        public Vector2 Position
        {
            get { return Wheel.Position + new Vector2(_wheelRadius, _wheelRadius); }
            }
    }
}