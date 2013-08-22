using System.Collections.Generic;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using InfiniteIsland.Engine;
using InfiniteIsland.Engine.Math;
using InfiniteIsland.Engine.Visual;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using IDrawable = InfiniteIsland.Engine.Interface.IDrawable;
using IUpdateable = InfiniteIsland.Engine.Interface.IUpdateable;

namespace InfiniteIsland.Entity
{
    internal class Slimes : IUpdateable, IDrawable
    {
        private readonly List<Slime> _slimes;
        private float _milis;

        public Slimes()
        {
            _slimes = new List<Slime>();
            _slimes.Add(new Slime());
        }

        public void Draw(SpriteBatch spriteBatch, Camera camera)
        {
            foreach (Slime slime in _slimes)
            {
                slime.Draw(spriteBatch);
            }
        }

        public void Update(GameTime gameTime)
        {
            foreach (Slime slime in _slimes)
            {
                slime.Update(gameTime);
            }
        }

        public static void LoadContent(ContentManager content)
        {
            Slime.LoadContent(content);
        }
    }

    internal class Slime : Engine.Entity.Entity
    {
        private static Animation _animation;

        private readonly Body _body;
        private readonly Sprite _sprite;

        private float _scale = 2f;

        public Slime()
        {
            _sprite = new Sprite(_animation)
                {
                    FramesPerSecond = 10,
                    Flip = SpriteEffects.FlipHorizontally,
                    Body = {Scale = new Vector2(_scale)}
                };
            _sprite.Key = "walk";

            _body = BodyFactory.CreateRectangle(
                world: InfiniteIsland.World,
                width: _sprite.Animation.MaxDimensions.X.ToMeters() * _scale * .7f,
                height: _sprite.Animation.MaxDimensions.Y.ToMeters() * _scale * .7f,
                density: 1f,
                position: new Vector2(19, 0));
            _body.BodyType = BodyType.Dynamic;
        }

        public override void Update(GameTime gameTime)
        {
            _sprite.Body.Center = _body.Position.ToPixels();
            _sprite.Body.Rotation = _body.Rotation;
            _sprite.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            _sprite.Draw(spriteBatch);
        }

        public static void LoadContent(ContentManager content)
        {
            _animation = content.Load<Animation>("sprite/slime");
        }
    }
}