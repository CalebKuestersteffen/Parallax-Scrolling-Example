using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ParallaxScrollingExample
{
    /// <summary>
    /// A game demonstrating parallax scrolling
    /// </summary>
    public class ParallaxScrollingGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Player _player;

        // Layer textures
        private Texture2D _foreground;
        private Texture2D _midground;
        private Texture2D _background;

        public ParallaxScrollingGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            // Need to use HiDef due to the size of our textures!
            _graphics.GraphicsProfile = GraphicsProfile.HiDef;
        }

        /// <summary>
        /// Initializes the game
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _player = new Player();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _player.LoadContent(Content);

            // load the layer textures
            _foreground = Content.Load<Texture2D>("foreground");
            _midground = Content.Load<Texture2D>("midground");
            _background = Content.Load<Texture2D>("background");
        }

        /// <summary>
        /// Updates the game
        /// </summary>
        /// <param name="gameTime">An object representing time in-game</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _player.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// Draws the game
        /// </summary>
        /// <param name="gameTime">An object representing time in-game</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);


            float playerX = MathHelper.Clamp(_player.Position.X, 300, 13600);
            float offsetX = (300 - playerX);

            Matrix transform;

            // TODO: Add your drawing code here

            // Background
            transform = Matrix.CreateTranslation(offsetX * 0.333f, 0, 0);
            _spriteBatch.Begin(transformMatrix: transform);
            _spriteBatch.Draw(_background, Vector2.Zero, Color.White);
            _spriteBatch.End();

            // Midground
            transform = Matrix.CreateTranslation(offsetX * 0.666f, 0, 0);
            _spriteBatch.Begin(transformMatrix: transform);
            _spriteBatch.Draw(_midground, Vector2.Zero, Color.White);
            _spriteBatch.End();

            // Foreground
            transform = Matrix.CreateTranslation(offsetX, 0, 0);
            _spriteBatch.Begin(transformMatrix: transform);
            _spriteBatch.Draw(_foreground, Vector2.Zero, Color.White);
            _player.Draw(gameTime, _spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
