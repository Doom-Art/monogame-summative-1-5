using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
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
        Texture2D kianBikepic;
        Texture2D rectangleTex;
        Rectangle bikeRect;
        MouseState _mouseState;
        int part;
        bool change;
        int change2;
        float seconds;
        float startTime;
        bool pedalling;
        Texture2D canadaFlagTex;
        SoundEffect bell;
        SoundEffect ohCanada;
        SoundEffectInstance song;
        SoundEffect pedal;
        SoundEffectInstance pedalInstance;
        SoundEffect bikeBell;
        SoundEffectInstance bikeBellInstance;
        SpriteFont font;
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
            pedalling = true;
            part = 1;
            screen = Screen.Intro;
            _graphics.PreferredBackBufferWidth = 600;
            _graphics.PreferredBackBufferHeight = 400;
            _graphics.ApplyChanges();
            bikeRect = new Rectangle(833, 380, 13, 13);
            this.Window.Title = "Bike Ride to School";
            // TODO: Add your initialization logic here
            base.Initialize();
        }
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _mapTex = Content.Load<Texture2D>("MapFix");
            canadaFlagTex = Content.Load<Texture2D>("flag");
            _bikeTex = Content.Load<Texture2D>("bike2");
            bell = Content.Load<SoundEffect>("school_bell");
            ohCanada = Content.Load<SoundEffect>("Oh Canada");
            kianBikepic = Content.Load<Texture2D>("Kian_bike_fix");
            rectangleTex = Content.Load<Texture2D>("rectangle");
            song = ohCanada.CreateInstance();
            font = Content.Load<SpriteFont>("Font");
            pedal = Content.Load<SoundEffect>("Pedalling");
            pedalInstance = pedal.CreateInstance();
            bikeBell = Content.Load<SoundEffect>("Bicycle-bell");
            bikeBellInstance = bikeBell.CreateInstance();
            // TODO: use this.Content to load your game content here
        }
        protected override void Update(GameTime gameTime)
        {
            _mouseState = Mouse.GetState();
            seconds = (float)gameTime.TotalGameTime.TotalSeconds - startTime;
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (screen == Screen.Intro){
                if (_mouseState.LeftButton == ButtonState.Pressed){
                    _graphics.PreferredBackBufferWidth = 900;
                    _graphics.PreferredBackBufferHeight = 400;
                    _graphics.ApplyChanges();
                    screen = Screen.Animation;
                }
                startTime = (float)gameTime.TotalGameTime.TotalSeconds;
            }
            else if (screen == Screen.Animation && seconds >= 1.3){
                if (pedalling)
                    pedalInstance.Play();
                if (part ==1){
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
                        part++;
                        change = true;
                    }
                }
                else if (part==2){
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
                        part++;
                        change = true;
                    }
                }
                else if (part==3){
                    _bikeTex = Content.Load<Texture2D>("bike3");
                    if (bikeRect.X > 688)
                        bikeRect.X -= 1;
                    else
                        part++;
                }
                else if (part==4){
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
                        part++;
                    }
                }
                else if (part==5){
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
                        part++;
                    }
                }               
                else if (part==6){
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
                        part++;
                        pedalInstance.Pause();
                        bikeBellInstance.Play();
                        startTime = (float)gameTime.TotalGameTime.TotalSeconds;
                    }
                }
                else if (part==7){
                    
                    if (bikeRect.X > 31){
                        bikeRect.X -= 1;
                    }
                    else if (bikeRect.Y > 125){
                        _bikeTex = Content.Load<Texture2D>("bike2");
                        bikeRect.Y -= 1;
                    }
                    else{
                        _bikeTex = Content.Load<Texture2D>("bike1");
                        part++;
                    }
                }
                else if (part==8){
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
                        part++;
                        pedalInstance.Pause();
                        pedalling = false;
                        startTime = (float)(gameTime.TotalGameTime.TotalSeconds);
                        bell.Play();
                        change = true;
                    }
                }
                else if (seconds > 5.5 && change){
                    song.Play();
                    change=false;
                }
                else if (seconds > 6){
                    screen = Screen.EndScreen;
                }
            }
            else if(screen == Screen.EndScreen){
                this.Window.Title = "Oh Canada | Left click to replay";
                if (_mouseState.RightButton == ButtonState.Pressed){
                    song.Pause();
                }
                else if (_mouseState.LeftButton == ButtonState.Pressed){
                    song.Play();
                }
            }
            // TODO: Add your update logic here
            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Aqua);
            _spriteBatch.Begin();
            if (screen == Screen.Intro){
                _spriteBatch.Draw(kianBikepic, new Rectangle(0, 0, 600, 400), Color.White);
                _spriteBatch.Draw(rectangleTex, new Rectangle(60, 4, 470, 22), Color.White);
                _spriteBatch.DrawString(font, "Welcome to Kian's bike ride to school!", new Vector2(60, 0), Color.Black);
                _spriteBatch.DrawString(font, "Left click to start", new Vector2(375, 360), Color.Black);
            }
            else if (screen == Screen.Animation){
                _spriteBatch.Draw(_mapTex, new Rectangle(0, 0, 900, 402), Color.White);
                _spriteBatch.Draw(_bikeTex, bikeRect, Color.White);
            }
            else if(screen == Screen.EndScreen){
                _spriteBatch.Draw(canadaFlagTex, new Rectangle(0, 0, 900, 400), Color.White);
            }
            _spriteBatch.End();
            // TODO: Add your drawing code here
            base.Draw(gameTime);
        }
    }
}