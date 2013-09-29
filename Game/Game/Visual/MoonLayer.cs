using System.Collections.Generic;
using InfiniteIsland.Engine;
using InfiniteIsland.Engine.Visual;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace InfiniteIsland.Visual
{
    public class MoonLayer : ParallaxLayer
    {
        private static Sprite _moon;

        public MoonLayer(Vector2 position)
        {
            Parallax = Vector2.Zero;
            Sprites = new List<Sprite> {_moon};
            _moon.Body.Center = position + 200*Vector2.UnitY;
        }

        public static void LoadContent(ContentManager content)
        {
            _moon = new Sprite(content.Load<Animation>("sprite/planets"))
                {
                    Body = {Scale = new Vector2(1.5f)},
                    Key = "dusty",
                };
        }

        public override void Update(GameTime gameTime)
        {
            //_moon.Body.TopLeft =
            //    Vector2.Zero - _moon.Body.Scale * _moon.Body.Dimensions/3f;
            _moon.Body.Rotation += (gameTime.ElapsedGameTime.Milliseconds*1e-3f*MathHelper.PiOver4)%MathHelper.TwoPi;
        }

        public override void Draw(SpriteBatch spriteBatch, Camera camera)
        {
            //spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, effect,
            //                  camera.CalculateTransformMatrix(Parallax));
            //foreach (Sprite sprite in Sprites)
            //{
            //    sprite.Draw(spriteBatch);
            //}
            //spriteBatch.End();
        }
    }
}