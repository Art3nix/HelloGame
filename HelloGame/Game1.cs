using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace HelloGame
{
    public class HelloGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;


        private Vector2 ballPosition;
        private Vector2 ballVelocity;
        private Texture2D ballTexture;
        private Color ballColor = Color.Black;
        private Color bgColor = Color.White;
        private const int ballSize = 64;

        public HelloGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Window.Title = "Hello Game";
        }

        protected override void Initialize()
        {
            ballPosition = new Vector2 (
                GraphicsDevice.Viewport.Width / 2,
                GraphicsDevice.Viewport.Height / 2
                );

            System.Random random = new System.Random();
            ballVelocity = new Vector2(
                (float)random.NextDouble()*3,
                (float)random.NextDouble()
                );
            ballVelocity.Normalize();
            ballVelocity *= 200;


            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            ballTexture = Content.Load<Texture2D>("ball");


        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            ballPosition += ballVelocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            System.Random random = new System.Random();

            if (ballPosition.X < GraphicsDevice.Viewport.X
                || ballPosition.X > GraphicsDevice.Viewport.Width - ballSize) {
                ballVelocity.X *= -1;
                bgColor = new Color(random.Next(0, 256), random.Next(0, 256), random.Next(0, 256));
            }

            if (ballPosition.Y < GraphicsDevice.Viewport.Y
                || ballPosition.Y > GraphicsDevice.Viewport.Height - ballSize) { 
                ballVelocity.Y *= -1;
                bgColor = new Color(random.Next(0, 256), random.Next(0, 256), random.Next(0, 256));
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(bgColor);

            _spriteBatch.Begin();

            _spriteBatch.Draw(ballTexture, ballPosition, ballColor);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}