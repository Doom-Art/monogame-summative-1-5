using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace monogame_5
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Texture2D _mapTex;
        Texture2D _bikeTex;
        Rectangle bikeRect;
        MouseState _mouseState;
        bool part1;
        bool part2;
        float seconds;
        float startTime;
        enum Screen
        {
            Intro, 
            Animation,
            EndScreen
        }
        Screen screen;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            part1 = true;
            part2 = true;
            screen = Screen.Intro;
            _graphics.PreferredBackBufferWidth = 900;
            _graphics.PreferredBackBufferHeight = 400;
            _graphics.ApplyChanges();
            bikeRect = new Rectangle(833, 380, 13, 13);
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _mapTex = Content.Load<Texture2D>("MapFix");
            _bikeTex = Content.Load<Texture2D>("bike2");
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            _mouseState = Mouse.GetState();
            seconds = (float)gameTime.TotalGameTime.TotalSeconds - startTime;
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (screen == Screen.Intro)
            {
                if (_mouseState.LeftButton == ButtonState.Pressed)
                    screen = Screen.Animation;
                startTime = (float)gameTime.TotalGameTime.TotalSeconds;
            }

            else if (screen == Screen.Animation && seconds >= 1){
                if (bikeRect.Y > 351 && part1){
                    bikeRect.X += 1;
                    bikeRect.Y -= 1;
                    part1 = false;
                }
                else if(bikeRect.Y > 351 && !part1){
                    bikeRect.Y -= 1;
                    part1 = true;
                }
                else if (bikeRect.Y > 335 && part2){
                    _bikeTex = Content.Load<Texture2D>("bike3");
                    bikeRect.X -= 1;
                    bikeRect.Y -= 1;
                }
                else if (bikeRect.Y > 304 && part2){
                    _bikeTex = Content.Load<Texture2D>("bike2");
                    bikeRect.X += 1;
                    bikeRect.Y -= 1;
                    part2 = false;
                }
                else if (bikeRect.Y > 304 && !part2)
                {
                    bikeRect.Y -= 1;
                    part2 = true;
                }

            }
            this.Window.Title = $"Bike Ride to School | Mouse X{_mouseState.X}, Mouse Y{_mouseState.Y}";
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Aqua);
            _spriteBatch.Begin();
            if (screen == Screen.Animation)
            {
                _spriteBatch.Draw(_mapTex, new Rectangle(0, 0, 900, 402), Color.White);
                _spriteBatch.Draw(_bikeTex, bikeRect, Color.White);
            }
            _spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}