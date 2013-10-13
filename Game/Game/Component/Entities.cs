using FarseerPhysics.Dynamics;
using InfiniteIsland.Engine;
using InfiniteIsland.Entity;
using InfiniteIsland.State;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace InfiniteIsland.Component
{
    public class Entities
    {
        public readonly Coins Coins;
        public readonly Player Player;
        public readonly Slimes Slimes;

        public Entities(World world, Play play)
        {
            Player = new Player(world, play);
            Coins = new Coins();
            Slimes = new Slimes(world);
        }

        public void Draw(SpriteBatch spriteBatch, CameraOperator cameraOperator)
        {
            Coins.Draw(spriteBatch, cameraOperator, false);
            Slimes.Draw(spriteBatch, cameraOperator);
            Player.Draw(spriteBatch, cameraOperator);
        }

        public void Update(GameTime gameTime, CameraOperator cameraOperator, World world)
        {
            Player.Update(gameTime);
            Coins.Update(gameTime, cameraOperator, this, world);
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