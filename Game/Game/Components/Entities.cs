using InfiniteIsland.Engine;
using InfiniteIsland.Entity;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using IDrawable = InfiniteIsland.Engine.Interface.IDrawable;
using IUpdateable = InfiniteIsland.Engine.Interface.IUpdateable;

namespace InfiniteIsland.Components
{
    internal class Entities : IUpdateable
    {
        public readonly Coins Coins;
        public readonly Player Player;
        public readonly Slimes Slimes;

        public Entities()
        {
            Player = new Player();
            Coins = new Coins();
            Slimes = new Slimes();
        }

        public void Draw(SpriteBatch spriteBatch, Camera camera)
        {
            Coins.Draw(spriteBatch, false);
            Slimes.Draw(spriteBatch);
            Player.Draw(spriteBatch);
        }

        public void Update(GameTime gameTime)
        {
            Player.Update(gameTime);
            Coins.Update(gameTime);
            Slimes.Update(gameTime);
        }

        public static void LoadContent(ContentManager content)
        {
            Coins.LoadContent(content);
            Player.LoadContent(content);
            Slimes.LoadContent(content);
        }
    }
}