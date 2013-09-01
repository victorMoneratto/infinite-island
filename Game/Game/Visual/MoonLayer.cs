using System.Collections.Generic;
using InfiniteIsland.Engine.Visual;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace InfiniteIsland.Visual
{
    public class MoonLayer : ParallaxLayer
    {
        private static Sprite _moon;

        public MoonLayer()
        {
            Parallax = Vector2.Zero;
            Sprites = new List<Sprite>(){_moon};
        }

        public static void LoadContent(ContentManager content)
        {
            _moon = new Sprite(content.Load<Animation>("sprite/moon"))
                {
                    Body ={Scale = new Vector2(.5f)},
                    Tint = Color.White
                };
        }

        public override void Update(GameTime gameTime)
        {
            _moon.Body.TopLeft = Vector2.Zero - _moon.Body.Scale * _moon.Body.Dimensions/3f;
            _moon.Body.Rotation += (gameTime.ElapsedGameTime.Milliseconds*1e-3f * MathHelper.PiOver4) % MathHelper.TwoPi;
        }
    }
}