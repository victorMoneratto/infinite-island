using InfiniteIsland.Engine;
using InfiniteIsland.Entity;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using IDrawable = InfiniteIsland.Engine.Interface.IDrawable;
using IUpdateable = InfiniteIsland.Engine.Interface.IUpdateable;

namespace InfiniteIsland.Components
{
    internal class Entities : IUpdateable, IDrawable
    {
        public readonly Coins Coins;
        public readonly Player Player;
        public readonly Slimes Slimes;
        public readonly Singularity Singularity;

        public Entities()
        {
            Singularity = new Singularity();
            Player = new Player();
            Coins = new Coins();
            Slimes = new Slimes();
        }

        public void Draw(SpriteBatch spriteBatch, Camera camera)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null,
                              camera.CalculateTransformMatrix(Vector2.One));

            Singularity.Draw(spriteBatch);
            Player.Draw(spriteBatch);
            Coins.Draw(spriteBatch);
            Slimes.Draw(spriteBatch);

            spriteBatch.End();
        }

        public void Update(GameTime gameTime)
        {
            Singularity.Update(gameTime);
            Player.Update(gameTime);
            Coins.Update(gameTime);
            Slimes.Update(gameTime);
        }

        public static void LoadContent(ContentManager content)
        {
            Singularity.LoadContent(content);
            Coins.LoadContent(content);
            Player.LoadContent(content);
            Slimes.LoadContent(content);
        }
    }
}