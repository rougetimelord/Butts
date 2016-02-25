﻿#region Using Statements
using System;
using System.IO;
using System.Linq;
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
        static Dictionary<int, Enemy> _enemiesDict = new Dictionary<int, Enemy>();
        int enemyID = 0;
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
            //Set bounds
            _fullscreen = new Vector2(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height);
            graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
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
            //If no enemies or t = 15 add them
            if (_enemiesDict.Count == 0 || _t == 15)
            {
                _enemiesDict.Add(enemyID, new Enemy(_s));
                enemyID++;
            }
            int i = 0;
            foreach (KeyValuePair<int,Enemy> en in _enemiesDict)
            {
                //Update all enemies
                en.Value.Update(Player.hiLocation);
                if (!en.Value.alive)
                {
                    //if dead add this ID to the "kill" list
                    _k.Add(en.Key);
                }
            }
            for (i = (_k.Count != 0) ? 0 : 1; i < _k.Count; i++)
            {
                //If player is alive add score
                //Unless you play in spook mode
                if (!PositionChecker.dead)
                    _s++;
                _enemiesDict.Remove(_k[i]);
            }
            //Clear "kill" list
            _k.Clear();
            //Increment or reset spawn timer
            _t = (_t > 15) ? 0 : _t + 1;
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
            if ((newKey.IsKeyDown(Keys.Space) && oldKey.IsKeyUp(Keys.Space)) && PositionChecker.dead)
            {
                Player.hiLocation = new Vector2(Game1._fullscreen.X / 2, Game1._fullscreen.Y / 2);
                PositionChecker.dead = false;
                _enemiesDict.Clear();
                _s = 0;
                _weeds.Clear();
                enemyID = 0;
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
                if(_attack)
                    spriteBatch.Draw(_hiAt, Player.hiLocation, null, new Color(255,0,0,5),0F,new Vector2(_hiAt.Width/2,_hiAt.Height/2),1F,SpriteEffects.None,1F);
                spriteBatch.Draw(_hi, Player.hiLocation, null, Color.Pink, 0F, new Vector2(_hi.Width / 2, _hi.Height / 2), 1F, SpriteEffects.None, 1F);
                foreach (KeyValuePair<int,Enemy> en in _enemiesDict.OrderBy(o => o.Value.type))
                    spriteBatch.Draw(_hi, en.Value.eLoc, null, en.Value.color, 0F, new Vector2(_hi.Width / 2, _hi.Height / 2), 1F, SpriteEffects.None, 0F);
                _fontRenderer.DrawText(spriteBatch, (int)_fullscreen.X - 100, 50, KeyHandler.timer);
            }
            if (PositionChecker.dead)
            {
                //If dead draw "game over" in the center of the screen
                _fontRenderer.DrawText(spriteBatch, (int)_fullscreen.X / 2 -  54, (int)_fullscreen.Y / 2, "Game Over");
                _fontRenderer.DrawText(spriteBatch, (int)_fullscreen.X / 2 - 128, (int)_fullscreen.Y / 2 + 32, "Press space to try again");
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
