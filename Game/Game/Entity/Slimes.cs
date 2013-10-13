using System.Collections.Generic;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using InfiniteIsland.Component;
using InfiniteIsland.Engine.Math;
using InfiniteIsland.Engine.Visual;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace InfiniteIsland.Entity
{
    public class Slimes
    {
        private readonly List<Slime> _slimes;

        public Slimes(World world)
        {
            _slimes = new List<Slime> {new Slime(world)};
        }

        public void Update(GameTime gameTime)
        {
            foreach (Slime slime in _slimes)
            {
                slime.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch, CameraOperator cameraOperator)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null,
                cameraOperator.Camera.CalculateTransformMatrix(Vector2.One));
            foreach (Slime slime in _slimes)
            {
                slime.Draw(spriteBatch);
            }
            spriteBatch.End();
        }

        public static void LoadContent(ContentManager content)
        {
            Slime.LoadContent(content);
        }
    }

    internal class Slime
    {
        private const float Scale = 2f;
        private static Animation _animation;

        private readonly Body _body;
        private readonly Sprite _sprite;

        public Slime(World world)
        {
            _sprite = new Sprite(_animation)
            {
                FramesPerSecond = 10,
                Flip = SpriteEffects.FlipHorizontally,
                Body = {Scale = new Vector2(Scale)},
                Key = "walk"
            };

            _body = BodyFactory.CreateCircle(world: world,
                radius: _sprite.Body.Dimensions.X.ToMeters()*.75f, density: .5f,
                position: new Vector2(10, 0));

            _body.BodyType = BodyType.Dynamic;
        }

        public void Update(GameTime gameTime)
        {
            _body.ApplyLinearImpulse(.1f*Vector2.UnitX);
            _sprite.Body.Center = _body.Position.ToPixels();
            _sprite.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _sprite.Draw(spriteBatch);
        }

        public static void LoadContent(ContentManager content)
        {
            _animation = content.Load<Animation>("sprite/slime");
        }
    }
}