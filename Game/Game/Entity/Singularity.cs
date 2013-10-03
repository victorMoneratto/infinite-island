using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using InfiniteIsland.Component;
using InfiniteIsland.Engine;
using InfiniteIsland.Engine.Math;
using InfiniteIsland.Engine.Visual;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace InfiniteIsland.Entity
{
    internal class Singularity : Engine.Entity.Entity
    {
        private static Sprite _sprite;
        private readonly Body _body;

        public Singularity()
        {
            _body = BodyFactory.CreateCircle(
                world: InfiniteIsland.World,
                radius: (_sprite.Body.Projection.BoundingBox.Dimensions.X*.4f).ToMeters(),
                density: 0f);
            _body.Enabled = false;
        }

        public override void Update(GameTime gameTime)
        {
            if (Input.Mouse.IsButtonDown(Input.Mouse.MouseButton.Right))
            {
                if (!_body.Enabled)
                {
                    _body.Enabled = true;
                    //some hardcoding T-T
                    var randomPos = new Vector2(1280*(float) InfiniteIsland.Random.NextDouble(),
                                                _sprite.Body.Dimensions.Y/2f);
                    _body.Position = CameraOperator.Instance.Camera.PositionOnWorld(randomPos);
                    _sprite.Body.Center = _body.Position.ToPixels();
                }
            }
            else
            {
                _body.Enabled = false;
            }

            if (_body.Enabled)
            {
                _sprite.Body.Rotation += (gameTime.ElapsedGameTime.Milliseconds*5e-3f)%MathHelper.TwoPi;
                _sprite.Body.Scale = new Vector2(1f + (float) InfiniteIsland.Random.NextDouble()/10f);
                _sprite.Body.Center += new Vector2(
                    x: (Entities.Instance.Player.Body.Motor.MotorSpeed*
                       (float) InfiniteIsland.Random.NextDouble()) +
                       ((float) InfiniteIsland.Random.NextDouble() - .5f)*20,
                    y: ((float) InfiniteIsland.Random.NextDouble() - .5f)*20);
                _body.Position = _sprite.Body.Center.ToMeters();
                _sprite.Update(gameTime);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Input.Mouse.IsButtonDown(Input.Mouse.MouseButton.Right))
                _sprite.Draw(spriteBatch);
        }

        public static void LoadContent(ContentManager content)
        {
            _sprite = new Sprite(content.Load<Animation>("sprite/singularity"));
        }
    }
}