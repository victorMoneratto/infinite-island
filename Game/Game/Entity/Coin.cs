using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using InfiniteIsland.Engine.Math;
using InfiniteIsland.Engine.Visual;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace InfiniteIsland.Entity
{
    internal class Coin : Engine.Entity.Entity
    {
        public readonly Body Body;
        private readonly Sprite<CoinEnum> _sprite;

        public Coin(Game game, Vector2 position)
        {
            var texture = game.Content.Load<Texture2D>("img/coin");
            _sprite = new Sprite<CoinEnum>(texture, new Vector2(texture.Width));
            _sprite.RegisterAnimation(CoinEnum.Coin, 0, 1);
            _sprite.AnimationKey = CoinEnum.Coin;
            Body = BodyFactory.CreateCircle(
                world: InfiniteIsland.World,
                radius: (texture.Width/2f).ToMeters(),
                density: 1f,
                position: position,
                userData: this);
            Body.BodyType = BodyType.Dynamic;
            Body.Restitution = 1.1f;
        }

        public override void Update(GameTime gameTime)
        {
            _sprite.Body.Center = Body.Position.ToPixels();
            _sprite.Body.Rotation = Body.Rotation;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            _sprite.Draw(spriteBatch);
        }

        private enum CoinEnum
        {
            Coin
        }
    }
}