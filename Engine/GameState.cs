using Microsoft.Xna.Framework;

namespace InfiniteIsland.Engine
{
    public class GameState : DrawableGameComponent
    {
        protected GameState(Game game) : base(game)
        {
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }

        protected override void LoadContent()
        {
            base.LoadContent();
        }
    }
}