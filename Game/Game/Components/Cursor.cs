using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Joints;
using InfiniteIsland.Engine;
using InfiniteIsland.Engine.Math;
using InfiniteIsland.Engine.Visual;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using IDrawable = InfiniteIsland.Engine.Interface.IDrawable;
using IUpdateable = InfiniteIsland.Engine.Interface.IUpdateable;

namespace InfiniteIsland.Components
{
    internal class Cursor : IUpdateable, IDrawable
    {
        private static Sprite _sprite;
        private FixedMouseJoint _mouseJoint;

        public void Draw(SpriteBatch spriteBatch, Camera camera)
        {
            spriteBatch.Begin();

            _sprite.Draw(spriteBatch);
            spriteBatch.End();
        }

        public void Update(GameTime gameTime)
        {
            _sprite.Body.Center = Input.Mouse.Position;
            if (_mouseJoint != null)
            {
                if (Input.Mouse.IsButtonDown(Input.Mouse.MouseButton.Left))
                {
                    _mouseJoint.WorldAnchorB = InfiniteIsland.CameraOperator.Camera.PositionOnWorld(Input.Mouse.Position);
                }
                else
                {
                    InfiniteIsland.World.RemoveJoint(_mouseJoint);
                    _mouseJoint = null;
                }
            }
            else
            {
                Vector2 cursorWorldPosition = InfiniteIsland.CameraOperator.Camera.PositionOnWorld(Input.Mouse.Position);
                if (Input.Mouse.IsButtonClicked(Input.Mouse.MouseButton.Left))
                {
                    Fixture fixture;
                    float radiusMeters = _sprite.Body.Projection.BoundingBox.Dimensions.X.ToMeters();
                    if ((fixture =
                         InfiniteIsland.World.TestPoint(cursorWorldPosition - new Vector2(radiusMeters))) != null ||
                        (fixture =
                         InfiniteIsland.World.TestPoint(cursorWorldPosition + new Vector2(radiusMeters))) != null ||
                        (fixture =
                         InfiniteIsland.World.TestPoint(cursorWorldPosition + new Vector2(radiusMeters, -radiusMeters))) != null ||
                        (fixture =
                         InfiniteIsland.World.TestPoint(cursorWorldPosition - new Vector2(radiusMeters, -radiusMeters))) != null)
                    {
                        _mouseJoint = new FixedMouseJoint(fixture.Body, cursorWorldPosition)
                            {
                                MaxForce = 200*fixture.Body.Mass
                            };
                        InfiniteIsland.World.AddJoint(_mouseJoint);
                    }
                }
            }
        }

        public static void LoadContent(ContentManager content)
        {
            _sprite = new Sprite(content.Load<Animation>("sprite/cursor"));
        }
    }
}