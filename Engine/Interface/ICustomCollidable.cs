using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Contacts;

namespace InfiniteIsland.Engine.Interface
{
    public interface ICustomCollidable
    {
        bool OnCollision(Fixture f1, Fixture f2, Contact contact);
    }
}