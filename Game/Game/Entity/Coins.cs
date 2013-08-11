using System.Collections.Generic;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using InfiniteIsland.Engine.Math;
using InfiniteIsland.Engine.Visual;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using IDrawable = InfiniteIsland.Engine.Interface.IDrawable;
using IUpdateable = InfiniteIsland.Engine.Interface.IUpdateable;

namespace InfiniteIsland.Entity
{
    internal class Coins : IUpdateable, IDrawable
    {
        private static Texture2D _coinTexture;
        private readonly List<Coin> _coins = new List<Coin>();
        private float _milis;

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Coin coin in _coins)
            {
                coin.Draw(spriteBatch);
            }
        }

        public void Update(GameTime gameTime)
        {
            foreach (Coin coin in _coins)
            {
                coin.Update(gameTime);
            }

            _milis += gameTime.ElapsedGameTime.Milliseconds*1e-3f;
            if (_milis > 1f)
            {
                --_milis;
                _coins.Add(
                    new Coin(
                        texture: _coinTexture,
                        position: InfiniteIsland.Camera.Viewport.Center.ToMeters(),
                        velocity: InfiniteIsland.Entities.Player.Body.Wheel.LinearVelocity));
            }
        }

        public static void LoadContent(ContentManager content)
        {
            _coinTexture = content.Load<Texture2D>("img/coin");
        }
    }

    internal class Coin : Engine.Entity.Entity
    {
        public readonly Body Body;
        private readonly Sprite<CoinEnum> _sprite;

        public Coin(Texture2D texture, Vector2 position, Vector2 velocity)
        {
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
            Body.LinearVelocity = velocity;
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