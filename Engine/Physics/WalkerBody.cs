using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Joints;
using FarseerPhysics.Factories;
using InfiniteIsland.Engine.Math;
using Microsoft.Xna.Framework;

namespace InfiniteIsland.Engine.Physics
{
    public class WalkerBody
    {
        public readonly RevoluteJoint Motor;
        public readonly Body Torso;
        public readonly Body Wheel;

        public WalkerBody(World world)
        {
            float heightMeters = 140f.ToMeters();
            float widthMeters = 45f.ToMeters();
            float torsoHeight = heightMeters - widthMeters/2f;

            Torso = BodyFactory.CreateRectangle(
                world: world,
                width: widthMeters,
                height: torsoHeight,
                density: 1f,
                position: Vector2.UnitX*20,
                userData: this);

            Torso.BodyType = BodyType.Dynamic;

            Wheel = BodyFactory.CreateCircle(
                world: world,
                radius: widthMeters/1.9f,
                density: 1f,
                position: Torso.Position + new Vector2(0, torsoHeight/2));

            Wheel.BodyType = BodyType.Dynamic;
            Wheel.Friction = float.MaxValue;
            Wheel.Restitution = float.MinValue;

            JointFactory.CreateFixedAngleJoint(world, Torso);

            Motor = JointFactory.CreateRevoluteJoint(world, Torso, Wheel, Vector2.Zero);
            Motor.MotorEnabled = true;
            Motor.MaxMotorTorque = 10;
        }
    }
}