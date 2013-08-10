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

        public WalkerBody(World world, float width, float height, object userData)
        {
            float torsoHeight = height - width/2f;

            Torso = BodyFactory.CreateRectangle(
                world: world,
                width: width,
                height: torsoHeight,
                density: 1f,
                position: Vector2.UnitX*20,
                userData: userData);

            Torso.BodyType = BodyType.Dynamic;

            Wheel = BodyFactory.CreateCircle(
                world: world,
                radius: width/1.9f,
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
    }
}