using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Joints;
using InfiniteIsland.Engine;
using InfiniteIsland.Engine.Math;
using InfiniteIsland.Engine.Visual;
using InfiniteIsland.Entity;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace InfiniteIsland.Component
{
    public class Cursor
    {
        private static Sprite _sprite;
        private FixedMouseJoint _mouseJoint;

        public void Draw(SpriteBatch spriteBatch, Camera camera)
        {
            spriteBatch.Begin();
            _sprite.Draw(spriteBatch);
            spriteBatch.End();
        }

        public void Update(GameTime gameTime, CameraOperator cameraOperator, World world)
        {
            _sprite.Body.Center = Input.Mouse.Position;
            if (_mouseJoint != null)
            {
                if (Input.Mouse.IsButtonDown(Input.Mouse.MouseButton.Left))
                {
                    _mouseJoint.WorldAnchorB = cameraOperator.Camera.PositionOnWorld(Input.Mouse.Position);
                }
                else
                {
                    world.RemoveJoint(_mouseJoint);
                    _mouseJoint = null;
                }
            }
            else
            {
                Vector2 cursorWorldPosition = cameraOperator.Camera.PositionOnWorld(Input.Mouse.Position);
                if (Input.Mouse.IsButtonClicked(Input.Mouse.MouseButton.Left))
                {
                    Fixture fixture;
                    float radiusMeters = _sprite.Body.Projection.BoundingBox.Dimensions.X.ToMeters();
                    if ((fixture =
                        world.TestPoint(cursorWorldPosition - new Vector2(radiusMeters))) != null ||
                        (fixture =
                            world.TestPoint(cursorWorldPosition + new Vector2(radiusMeters))) != null ||
                        (fixture =
                            world.TestPoint(cursorWorldPosition + new Vector2(radiusMeters, -radiusMeters))) != null ||
                        (fixture =
                            world.TestPoint(cursorWorldPosition - new Vector2(radiusMeters, -radiusMeters))) != null)
                    {
                        if (fixture.UserData is Coin)
                        {
                            _mouseJoint = new FixedMouseJoint(fixture.Body, cursorWorldPosition)
                            {
                                MaxForce = 200*fixture.Body.Mass
                            };
                            world.AddJoint(_mouseJoint);
                        }
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