using System;
using System.Collections.Generic;
using InfiniteIsland.Engine.Visual;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace InfiniteIsland.Visual
{
    public class RocksLayer : ParallaxLayer
    {
        private static Texture2D _rock;

        public RocksLayer(Vector2 parallax)
        {
            Parallax = parallax;
            var random = new Random();
            Sprites = new List<Sprite>();

            const int mountCounts = 20;
            for (int i = 0; i < mountCounts; i++)
            {
                //Sprite<Mount> sprite = new Sprite<Mount>(_rock, new Vector2(_rock.Width, _rock.Height))
                //    {
                //        Body =
                //            {
                //                Position = new Vector2(
                //                    x: ((10f + random.Next(1001))*i),
                //                    y: (TerrainChunk.VerticalPosition + 1f + (float) random.NextDouble()).ToPixels()),
                //                Pivot = new Vector2(_rock.Width/2f, _rock.Height),
                //                Rotation = MathHelper.PiOver4/2*(float) (random.NextDouble() - .5f),
                //                Scale =
                //                    new Vector2(1 + 5*(float) random.NextDouble(), 1 + 5*(float) random.NextDouble())
                //            }
                //    };
                //sprite.RegisterAnimation(Mount.Lonely, 0, 1);
                //sprite.AnimationKey = Mount.Lonely;
                //Sprites.Add(sprite);
            }
        }

        public static void LoadContent(ContentManager content)
        {
            _rock = content.Load<Texture2D>("img/rock");
        }

        public override void Update(GameTime gameTime)
        {
        }
    }
}