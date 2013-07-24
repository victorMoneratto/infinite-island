using System;
using System.Collections.Generic;
using InfiniteIsland.Engine.Math;
using InfiniteIsland.Engine.Terrain;
using InfiniteIsland.Engine.Visual;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace InfiniteIsland.Visual
{
    public class RocksLayer : ParallaxLayer<RocksLayer.Mount>
    {
        public enum Mount
        {
            Lonely
        }

        public RocksLayer(Vector2 parallax)
        {
            Parallax = parallax;
        }

        public override void LoadContent(Game game)
        {
            var lonelyMount = game.Content.Load<Texture2D>("img/rock");
            var random = new Random();
            Sprites = new List<Sprite<Mount>>();

            const int mountCounts = 20;
            for (int i = 0; i < mountCounts; i++)
            {
                var sprite = new Sprite<Mount>(lonelyMount, new Vector2(lonelyMount.Width, lonelyMount.Height))
                    {
                        Body =
                            {
                                Center = new Vector2(
                                    x: ((10f + random.Next(1001))*i),
                                    y: (TerrainChunk.VerticalPosition + 1f + (float) random.NextDouble()).ToPixels()),
                                Pivot = new Vector2(lonelyMount.Width/2f, lonelyMount.Height),
                                Rotation = MathHelper.PiOver4/2 * (float)(random.NextDouble()-.5f),
                                Scale = new Vector2(1 +5*(float)random.NextDouble(), 1+5*(float)random.NextDouble())
                            }
                    };
                sprite.RegisterAnimation(Mount.Lonely, 0, 1);
                sprite.AnimationKey = Mount.Lonely;
                Sprites.Add(sprite);
            }
        }

        public override void Update(GameTime gameTime)
        {
        }
    }
}