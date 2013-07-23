using System;
using System.Collections.Generic;
using InfiniteIsland.Sprite;
using InfiniteIsland.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace InfiniteIsland.Background
{
    public abstract class Layer<T> where T : struct
    {
        public List<Sprite<T>> Sprites;
        public Vector2 Parallax { get; set; }

        public abstract void LoadContent(Game game);

        public abstract void Update(GameTime gameTime);

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null,
                              Camera.CalculateTransformMatrix(Parallax));
            foreach (var sprite in Sprites)
            {
                sprite.Draw(spriteBatch);
            }
            spriteBatch.End();
        }
    }

    public class MountLayer : Layer<MountLayer.Mount>
    {
        public enum Mount
        {
            Lonely
        }

        public MountLayer(Vector2 parallax)
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
                                    x: ((10f + random.Next(11))*i).ToPixels(),
                                    y: (3f + (float) random.NextDouble()).ToPixels()),
                                Rotation = MathHelper.PiOver4*(float) random.NextDouble() - MathHelper.PiOver4/2
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