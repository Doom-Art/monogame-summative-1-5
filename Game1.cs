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
        bool part3;
        bool part4;
        bool part5;
        bool part6;
        bool part7;
        bool part8;
        bool change;
        int change2;
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
            change = true;
            change2 = 0;
            part1 = true;
            part2 = true;
            part3 = true;
            part4 = true;
            part5 = true;
            part6 = true;
            part7 = true;
            part8 = true;
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
            if (screen == Screen.Intro){
                if (_mouseState.LeftButton == ButtonState.Pressed)
                    screen = Screen.Animation;
                startTime = (float)gameTime.TotalGameTime.TotalSeconds;
            }

            else if (screen == Screen.Animation && seconds >= 1){
                if (part1){
                    if (bikeRect.Y > 351 && change){
                        bikeRect.X += 1;
                        bikeRect.Y -= 1;
                        change = false;
                    }
                    else if (bikeRect.Y > 351 && !change){
                        bikeRect.Y -= 1;
                        change = true;
                    }
                    else{
                        part1 = false;
                        change = true;
                    }
                }
                else if (part2){
                    if (bikeRect.Y > 335 && change){
                        _bikeTex = Content.Load<Texture2D>("bike3");
                        bikeRect.X -= 1;
                        bikeRect.Y -= 1;
                    }
                    else if (bikeRect.Y > 294 && change){
                        _bikeTex = Content.Load<Texture2D>("bike2");
                        bikeRect.X += 1;
                        bikeRect.Y -= 1;
                        change = false;
                    }
                    else if (bikeRect.Y > 294 && !change){
                        bikeRect.Y -= 1;
                        change = true;
                    }
                    else{
                        part2 = false;
                        change = true;
                    }
                }
                else if (part3){
                    _bikeTex = Content.Load<Texture2D>("bike3");
                    if (bikeRect.X > 688)
                        bikeRect.X -= 1;
                    else
                        part3 = false;
                }
                else if (part4){
                    if (bikeRect.Y < 320 && change2 <= 4){
                        bikeRect.X -= 1;
                        change2++;

                    }
                    else if (bikeRect.Y <313){
                        _bikeTex = Content.Load<Texture2D>("bike4");
                        bikeRect.Y += 1;
                        bikeRect.X -= 1;
                    }
                    else{
                        change2 = 0;
                        part4 = false;
                    }
                }
                else if (part5){
                    if (bikeRect.X > 633){
                        _bikeTex = Content.Load<Texture2D>("bike3");
                        bikeRect.X -= 1;
                        change2++;
                    }
                    else if (bikeRect.Y > 297 && change){
                        bikeRect.Y -= 1;
                        bikeRect.X -= 1;
                        change = false;
                    }
                    else if (bikeRect.Y > 297 && !change) {
                        bikeRect.X -= 1;
                        change = true;
                    }
                    else{
                        change2 = 0;
                        change = true;
                        part5 = false;
                    }
                }               
                else if (part6){
                    if (bikeRect.X > 527 && change2 <= 2){
                        bikeRect.X -= 1;
                        change2++;
                    }
                    else if (bikeRect.X > 527){
                        bikeRect.Y -= 1;
                        bikeRect.X -= 1;
                        change2 = 0;
                    }
                    else if (bikeRect.X > 474){
                        bikeRect.X -= 1;
                    }
                    else{
                        bikeRect.Y -= 3;
                        change2 = 0;
                        part6 = false;
                        startTime = (float)gameTime.TotalGameTime.TotalSeconds;
                    }
                }
                else if (part7){
                    
                    if (bikeRect.X > 31){
                        bikeRect.X -= 1;
                    }
                    else if (bikeRect.Y > 125){
                        _bikeTex = Content.Load<Texture2D>("bike2");
                        bikeRect.Y -= 1;
                    }
                    else{
                        _bikeTex = Content.Load<Texture2D>("bike1");
                        part7 = false;
                    }
                }
                else if (part8){
                    if (bikeRect.X < 54){
                        bikeRect.X += 1;
                    }
                    else if(bikeRect.Y < 172){
                        _bikeTex = Content.Load<Texture2D>("bike4");
                        bikeRect.Y += 1;
                    }
                    else{
                        _bikeTex = Content.Load<Texture2D>("bike1");
                        bikeRect.X += 3;
                        part8 = false;
                        startTime = (float)(gameTime.TotalGameTime.TotalSeconds);
                    }
                }
                else if (seconds > 5){
                    screen = Screen.EndScreen;
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