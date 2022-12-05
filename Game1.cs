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
        Rectangle bikeRect;
        MouseState _mouseState;
        int part;
        bool change;
        int change2;
        float seconds;
        float startTime;
        Texture2D canadaFlagTex;
        SoundEffect bell;
        SoundEffect ohCanada;
        SoundEffectInstance song;
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
            part = 1;
            screen = Screen.Intro;
            _graphics.PreferredBackBufferWidth = 900;
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
            song = ohCanada.CreateInstance();
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
                if (_mouseState.LeftButton == ButtonState.Pressed){
                    song.Pause();
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