#region Using Statements
using System;
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
        SpriteBatch spriteBatch;
        Texture2D _hi, _hiAt;
        //SpriteFont arial;
        static public Vector2 _fullscreen, _attacker;
        public static float _speed = 5;
        public static bool _attack = false;
        static List<Enemy> _enemies = new List<Enemy>();
        static float _t = 0;
        List<int> _k = new List<int>();
        string _sc;
        int _s = 0;
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
            IntPtr hWnd = this.Window.Handle;
            var control = System.Windows.Forms.Control.FromHandle(hWnd);
            var form = control.FindForm();
            form.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            form.Location = new System.Drawing.Point(0, 0);
            //TODO: Make fake fullscreen and pass fullscreen to enemy.Spawn()
            int[] fuck = { GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height };
            _fullscreen = new Vector2(fuck[0], fuck[1]);
            graphics.PreferredBackBufferHeight = fuck[1];
            graphics.PreferredBackBufferWidth = fuck[0];
            graphics.ApplyChanges();
            #endregion
            //Spawn player
            new Player();
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
            //arial = Content.Load<SpriteFont>("Arial.xnb");
            _hiAt = this.Content.Load<Texture2D>("HIatt");
            _hi = this.Content.Load<Texture2D>("HI");
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
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            KeyHandler.Handler(Keyboard.GetState(), gameTime);
            //If no enemies add them or t = 15
            if (_enemies.Count == 0 || _t == 15)
                _enemies.Add(new Enemy());
            int i = 0;
            foreach (Enemy en in _enemies)
            {
                //Update the enemies
                en.Update(Player.hiLocation);
                if (!en.alive)
                {
                    //if dead add the to the "kill" list
                    _k.Add(i);
                }
                //increase index
                i++;
            }
           int i2 = 0;
           for (i = (_k.Count != 0) ? 0 : 1; i < _k.Count; i++)
            {
               //kill them
               //Some glitches when multiple enemies are killed on the same update
                if (_k[i] >= 0)
                {
                    _s++;
                    _enemies.RemoveAt(_k[i]-i2);
                    i2++;
                }
            }
            _k.Clear();
            //Set draw position of the attack shape
            _attacker = new Vector2(Player.hiLocation.X - 75, Player.hiLocation.Y - 75);
            //Increment or reset spawn timer
            _t = (_t > 60) ? 0 : _t + 1;
            _sc = _s.ToString();
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
            if (PositionChecker.dd)
            {
                //spriteBatch.DrawString(arial, "Dead", new Vector2(_fullscreen.X / 2, _fullscreen.Y / 2), Color.White)
                Exit();
            }
            if (!PositionChecker.dd)
            {
                spriteBatch.Draw(_hiAt, (!_attack) ? _fullscreen : _attacker, Color.Red);
                spriteBatch.Draw(_hi, Player.hiLocation, Color.White);
                foreach (Enemy en in _enemies)
                    spriteBatch.Draw(_hi, en.eLoc, Color.Purple);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
