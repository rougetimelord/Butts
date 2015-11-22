#region Using Statements
using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
#endregion

namespace Butts
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        #region init
        /// <summary>
        /// Garbage tier init stuff
        /// </summary>
        SpriteBatch spriteBatch;
        Texture2D _hi, _hiAt;
        static public Vector2 _fullscreen, _attacker;
        public static float _speed = 5;
        public static bool _attack = false;
        static List<Enemy> _enemies = new List<Enemy>();
        static List<Weed> _weeds = new List<Weed>();
        static float _t = 0;
        public static KeyboardState oldKey = Keyboard.GetState();
        public static bool pause = false;
        List<int> _k = new List<int>();
        string _sc;
        int _s = 0, _i=0;
        FontRenderer _fontRenderer;
        int wr = 0;
        List<int> wk = new List<int>();
        #endregion
        public Game1()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            #region Make Fullscreen
            //Windows forms bs
            var form = System.Windows.Forms.Control.FromHandle(Window.Handle).FindForm();
            form.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            form.Location = new System.Drawing.Point(0, 0);
            int[] fuck = { GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height };
            //Set bounds
            _fullscreen = new Vector2(fuck[0], fuck[1]);
            graphics.PreferredBackBufferHeight = fuck[1];
            graphics.PreferredBackBufferWidth = fuck[0];
            graphics.ApplyChanges();
            #endregion
            //Spawn player
            new Player();
            //Here would be tilt init call but tilt isn't supported yet
            //Position_Changers.Tilt.TiltTest();
            base.Initialize();
        }
        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            #region Sprite Loading
            //_hiAt is attack square
            _hiAt = this.Content.Load<Texture2D>("HIatt");
            //Player & enemy square
            _hi = this.Content.Load<Texture2D>("HI");
            //Font renderer stuff, (fontFile, fontTexture)
            _fontRenderer = new FontRenderer(FontHandler.FontLoader.Load(Path.Combine(this.Content.RootDirectory, "text.xml")), this.Content.Load<Texture2D>("text_0"));
            //Temporarily loaded weed/MLG sprites
            var weed = this.Content.Load<Texture2D>("Weed");
            var gun = this.Content.Load<Texture2D>("gun");
            var dor = this.Content.Load<Texture2D>("doritos");
            var mtn = this.Content.Load<Texture2D>("mtn");
            var hit = this.Content.Load<Texture2D>("hit");
            //Pass weed joke sprites to Weed class's sprite loader
            Weed.Sprites(weed, gun, dor, mtn, hit);
            #endregion
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }
        /// <summary>
        /// Update method if not paused
        /// </summary>
        public void Simulate(GameTime gameTime)
        {
            //Handle keyboard presses
            KeyHandler.Handler(Keyboard.GetState(), gameTime);
            //If tilt is supported by device update the reading
            //if (Position_Changers.Tilt._r)
                //Position_Changers.Tilt.Update();
            //If no enemies add them or t = 15
            if (_enemies.Count == 0 || _t == 15)
                _enemies.Add(new Enemy());
            int i = 0;
            foreach (Enemy en in _enemies)
            {
                //Update all enemies
                en.Update(Player.hiLocation);
                if (!en.alive)
                {
                    //if dead add this instance to the "kill" list
                    _k.Add(i);
                }
                //increase index
                i++;
            }
            int i2 = 0;
            for (i = (_k.Count != 0) ? 0 : 1; i < _k.Count; i++)
            {
                //Kill them
                if (_k[i] >= 0)
                {
                    //If player is alive add score
                    //Unless you play in spook mode
                    if (!PositionChecker.dead)
                        _s++;
                    _enemies.RemoveAt(_k[i] - i2);
                    i2++;
                }
            }
            //Clear "kill" list
            _k.Clear();
            //Set draw position of the attack shape
            _attacker = new Vector2(Player.hiLocation.X - 75, Player.hiLocation.Y - 75);
            //Increment or reset spawn timer
            _t = (_t > 30) ? 0 : _t + 1;
            //Turn int score into a string
            _sc = _s.ToString();
        }
        /// <summary>
        /// Update method for weed joke
        /// </summary>
        public void WeedUpdate()
        {
            if (_s >= 419)
            {
                //Start weed effects at 419 to give one frame for sprites to fall
                if (_s == 419)
                    _i = 1;
                //Reset or add to weed timer
                _i = (_i == 15) ? 0 : _i + 1;
            }
            if (_i == 1)
            {
                //Every 15 updates add new weed
                _weeds.Add(new Weed());
            }
            //wr is wk indexer
            wr = 0;
            //Clear weed kill list
            wk.Clear();
            foreach (Weed we in _weeds)
            {
                //Update position
                we.Update();
                //If weed off screen put it on the kill list
                if (we.position.Y > _fullscreen.Y)
                    wk.Add(wr);
                wr++;
            }
            int w2 = 0;
            for (int wi = (wk.Count != 0) ? 0 : 1; wi < wk.Count; wi++)
            {
                _weeds.RemoveAt(wk[wi] - w2);
                w2++;
            }
        }
        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            //Pause key check
            KeyboardState newKey = Keyboard.GetState();
            if (newKey.IsKeyDown(Keys.LeftControl) && oldKey.IsKeyUp(Keys.LeftControl))
            {
                pause = !pause;
            }

            oldKey = newKey;
            //If not paused gogogogo
            if (!pause && !PositionChecker.dead)
                Simulate(gameTime);
            //Just do weed anyways
            WeedUpdate();
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            // TODO: Add your drawing code here
            spriteBatch.Begin();
            foreach (Weed we in _weeds)
            {
                //Draw all the weed stuff
                //Set origin of draw
                Vector2 origin = new Vector2(we.sprite.Width / 2, we.sprite.Height / 2);
                //Draw main sprite
                spriteBatch.Draw(we.sprite, we.position, null, we.c, we.rot, origin, we.scale, SpriteEffects.None, 0);
                //Draw hitmarker for that sprite
                spriteBatch.Draw(Weed._hit, we.hPos, null, Color.White, 0F, Weed._ho, 1, SpriteEffects.None, 1);
            }
            if (!PositionChecker.dead)
            {
                //If alive draw player, attack area, and enemies
                spriteBatch.Draw(_hiAt, (!_attack) ? _fullscreen : _attacker, Color.Red);
                spriteBatch.Draw(_hi, Player.hiLocation, Color.White);
                foreach (Enemy en in _enemies)
                    spriteBatch.Draw(_hi, en.eLoc, Color.Purple);
            }
            if (PositionChecker.dead)
            {
                //If dead draw "game over" in the center of the screen
                _fontRenderer.DrawText(spriteBatch, (int)_fullscreen.X / 2 -  54, (int)_fullscreen.Y / 2, "Game Over");
            }
            //If alive and paused draw pause text
            if(pause && !PositionChecker.dead)
                _fontRenderer.DrawText(spriteBatch, (int)_fullscreen.X / 2 - 32, (int)_fullscreen.Y / 2, "Paused");
            //If alive draw score in top left if dead draw it in the middle
            _fontRenderer.DrawText(spriteBatch, (!PositionChecker.dead) ? 50 : (int)_fullscreen.X / 2, (!PositionChecker.dead) ? 50 : (int)_fullscreen.Y / 2 - 32, _sc);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
