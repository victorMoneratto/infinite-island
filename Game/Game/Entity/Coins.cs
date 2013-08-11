using System.Collections.Generic;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using InfiniteIsland.Engine;
using InfiniteIsland.Engine.Math;
using InfiniteIsland.Engine.Math.Geometry;
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
        private readonly List<Coin> _coins = new List<Coin>(10);
        private float _milis;

        public void Draw(SpriteBatch spriteBatch, Camera camera)
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

                RotatableRectangleF cameraViewport = InfiniteIsland.CameraOperator.Camera.Viewport;
                //too tired to properly name it for now
                var factor = (float) InfiniteIsland.Random.NextDouble();
                if (_coins.Count == _coins.Capacity)
                    Remove(_coins[0]);
                _coins.Add(
                    new Coin(
                        position:
                            (cameraViewport.TopLeft + factor*new Vector2(cameraViewport.Dimensions.X, 0)).ToMeters(),
                        linearVelocity:
                            (2 - 2.5f*factor)*InfiniteIsland.Entities.Player.Body.Wheel.LinearVelocity));
            }
        }

        public void Remove(Coin coin)
        {
            InfiniteIsland.World.RemoveBody(coin.Body);
            _coins.Remove(coin);
        }

        public static void LoadContent(ContentManager content)
        {
            Coin.LoadContent(content);
        }
    }

    internal class Coin : Engine.Entity.Entity
    {
        private const float Scale = 2f;

        private static Texture2D _coinTexture;
        public readonly Body Body;
        private readonly Sprite<CoinEnum> _sprite;

        public Coin(Vector2 position, Vector2 linearVelocity)
        {
            _sprite = new Sprite<CoinEnum>(_coinTexture, new Vector2(_coinTexture.Width));
            _sprite.RegisterAnimation(CoinEnum.Coin, 0, 1);
            _sprite.AnimationKey = CoinEnum.Coin;
            _sprite.Body.Scale = new Vector2(Scale);
            Body = BodyFactory.CreateCircle(
                world: InfiniteIsland.World,
                radius: (_coinTexture.Width/2f).ToMeters()*Scale,
                density: 1f,
                position: position,
                userData: this);
            Body.BodyType = BodyType.Dynamic;
            Body.Restitution = 1f;
            Body.LinearVelocity = linearVelocity;
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

        public static void LoadContent(ContentManager content)
        {
            _coinTexture = content.Load<Texture2D>("img/coin");
        }

        private enum CoinEnum
        {
            Coin
        }
    }
}