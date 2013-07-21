using System;
using System.Collections.Generic;
using InfiniteIsland.SpriteComponent;
using InfiniteIsland.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace InfiniteIsland.Background
{
    public abstract class Layer<T> where T:struct 
    {
        public Vector2 Parallax { get; set; }

        public List<Sprite<T>> Sprites;

        public abstract void LoadContent(Game game);

        public abstract void Update(GameTime gameTime);

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, Camera.CalculateTransformMatrix(Parallax));
            foreach (Sprite<T> sprite in Sprites)
            {
                sprite.Draw(spriteBatch);
            }
            spriteBatch.End();
        }
    }

    public class MountLayer : Layer<MountLayer.Mount>
    {
        public MountLayer(Vector2 parallax)
        {
            Parallax = parallax;
        }

        public override void LoadContent(Game game)
        {
            Texture2D lonelyMount = game.Content.Load<Texture2D>("img/rock");
            Random random = new Random();
            Sprites = new List<Sprite<Mount>>();

            const int mountCounts = 20;
            for (int i = 0; i < mountCounts; i++)
            {
                Sprite<Mount> sprite = new Sprite<Mount>(lonelyMount, new Point(lonelyMount.Width, lonelyMount.Height))
                    {
                        Position = new Vector2(
                            x: ((10f + random.Next(11)) * i).ToPixels(),
                            y: (3f + (float)random.NextDouble()).ToPixels()),
                        Rotation = MathHelper.PiOver4 * (float)random.NextDouble() - MathHelper.PiOver4/2
                    };
                sprite.RegisterAnimation(Mount.Lonely, 0, 1);
                sprite.AnimationKey = Mount.Lonely;
                Sprites.Add(sprite);
            }
        }

        public override void Update(GameTime gameTime)
        {
        }

        public enum Mount
        {
            Lonely
        }
    }
}
