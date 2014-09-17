using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Starbound.RealmFactoryCore;
using System.Timers;
using System.IO;
using System.Xml.Serialization;
using Microsoft.Xna.Framework.Storage;

namespace TextureAtlas
{

    public class Game1 : Microsoft.Xna.Framework.Game
    {

        public static bool In(MouseState ms, Rectangle rectangle)
        {

            if (ms.X >= rectangle.X && ms.X <= (rectangle.X + rectangle.Width) && ms.Y >= rectangle.Y && ms.Y <= (rectangle.Y + rectangle.Height))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #region variables

        private string name = "Turd Burglar";

        private KeyboardState kState;
        private KeyboardState NState;
        private MouseState MoldState;
        private MouseState mouseState;

        public Vector2 OffSet = new Vector2(0, 0);
        public Vector2 position = new Vector2(150, 150);
        public Vector2 position2 = new Vector2();
        public Vector2 velocity = new Vector2(300, 0);
        public Vector2 velocityup = new Vector2(0, 300);
        private Vector2 InvPos;

        public bool Toggle = false;
        public bool Valid = true;
        public bool blnRevert = false;
        private bool IsScrolling = false;
        private bool blnLogTime = false;
        private bool blnDrawDebuggerClicked = false;
        private bool isDrag = false;
        private bool DoesDrop = false;
        private bool BlnAttack = true;
        private bool blnEquip = false;
        private bool blnDSP = false;
        private bool blnOpen = false;
        private bool BlnDelay = false;
        private bool AniTick = false;
        private bool Attack = false;
        private bool blnIsConfirming = false;
        private bool debugScreenDrawBgClickedBound = false;
        private bool pauseMenuExitBound = false;
        private bool pauseMenuResolutionBound = false;
        private bool pauseMenuBound = false;
        private bool ResolutionConfirmBound = false;
        private bool ResolutionConfirmChangeBound = false;
        private bool ResolutionConfirmRevertBound = false;
        private bool blnIsDirty = false;
        private bool blnWindowSizeBound = false;

        public int dir = 1;
        public int DamageCounter = 0;
        public int Columns = 16;
        public int Rows = 16;
        public int TileWidth = 160;
        public int TileHeight = 160;
        private int DebugCycles = 40;
        private int IntDrop = 0;
        private int HP;

        public Random RNG = new Random();
        private DebugScreen debugScreenUpdate = new DebugScreen();
        private DebugScreen debugScreenDraw = new DebugScreen();
        private DebugScreen debugScreenDraw_Inventory = new DebugScreen();
        private DebugScreen debugScreenDraw_Equipment = new DebugScreen();
        private DebugScreen debugScreenDraw_Enemies = new DebugScreen();
        private DebugScreen debugScreenDraw_Character = new DebugScreen();
        private DebugScreen debugScreenDraw_Items = new DebugScreen();
        private DebugScreen debugScreenDraw_PauseMenu = new DebugScreen();
        private DebugScreen debugScreenDraw_BackGround = new DebugScreen();
        private DebugScreen debugScreenDraw_Other = new DebugScreen();
        private PauseMenu pauseMenu = new PauseMenu();
        private PauseMenu ResolutionConfirm = new PauseMenu();
        private MessageDisplay MSG = new MessageDisplay();
        private Equipment equipment;
        private Inventory inventory;
        private AnimatedSprite animatedSprite;

        private Stopwatch DebugTimer;
        private Stopwatch DebugTimer1;

        private BackGround backGround = new BackGround();

        private List<long> UpdateTimes = new List<long>();
        private List<long> DrawTimes = new List<long>();
        private List<long> DrawTimes_Inventory = new List<long>();
        private List<long> DrawTimes_Equipment = new List<long>();
        private List<long> DrawTimes_Enemies = new List<long>();
        private List<long> DrawTimes_Character = new List<long>();
        private List<long> DrawTimes_Items = new List<long>();
        private List<long> DrawTimes_PauseMenu = new List<long>();
        private List<long> DrawTimes_BackGround = new List<long>();
        private List<long> DrawTimes_Other = new List<long>();
        private List<Item> InvItems = new List<Item>();
        private List<Item> EquipItem = new List<Item>();
        private List<Item> DroppedItems = new List<Item>();
        public List<Enemy> Enemies = new List<Enemy>();

        private LevelSet levelSet;
        private Level currentlevel;

        private Rectangle TileRect = new Rectangle(0, 0, 80, 80);

        private float MouseOffSetx = 0f;
        private float MouseOffSety = 0f;
        private float AnimationTimer = 0f;
        private float AnimationDuration = .5f;
        private float AtkTmr = .2f;
        private float TimeBetweenAttacks = .2f;

        private Texture2D LegBG;
        private Texture2D texture;
        private Texture2D MenuBtn;
        private Texture2D Textbg;
        private Texture2D CharScrn;
        private Texture2D Grass1;
        private Texture2D legbeam;
        private Texture2D hpbarFull;
        private Texture2D InvText;
        private Texture2D imgPause;
        private Texture2D hpbar75;
        private Texture2D hpbarHalf;
        private Texture2D hpbarQuarter;
        private Texture2D hpbar40;
        private Texture2D buttonbg;
        public Texture2D sword;
        public Texture2D sword2;

        private SpriteFont fontMSG;
        private SpriteFont Font1;
        private SpriteFont Font2;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        enum GameState
        {
            Active,
            Inactive
        }

        GameState CurrentGameState = GameState.Active;

        #endregion

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            GlobalVariables.gfx = graphics;

            GlobalVariables.ScreenWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width; ;
            GlobalVariables.ScreenHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;

            //Index fullScreen and Graphics width/height until storage can be put in place to 
            //read values from storage

            GlobalVariables.LoadUserSettings();

            if (GlobalVariables.UserSetWidth > 0 && GlobalVariables.UserSetHeight > 0)
            {
                graphics.PreferredBackBufferWidth = GlobalVariables.UserSetWidth;
                graphics.PreferredBackBufferHeight = GlobalVariables.UserSetHeight;
                graphics.IsFullScreen = GlobalVariables.UserSetFullScreen;
                GlobalVariables.FullScreen = GlobalVariables.UserSetFullScreen;
            }
            else
            {
                graphics.PreferredBackBufferWidth = GlobalVariables.ScreenWidth;
                graphics.PreferredBackBufferHeight = GlobalVariables.ScreenHeight - 40;
                graphics.IsFullScreen = false;
                GlobalVariables.FullScreen = false;
            }

            this.Window.AllowUserResizing = true;

            Content.RootDirectory = "Content";
            IsFixedTimeStep = true;
            TargetElapsedTime = TimeSpan.FromMilliseconds(20);
        }

        void Window_ClientSizeChanged(object sender, EventArgs e)
        {
            if (sender == this.Window)
            {
                //ClientWindow Resized
                graphics.PreferredBackBufferWidth = Window.ClientBounds.Width;
                graphics.PreferredBackBufferHeight = Window.ClientBounds.Height;
                blnIsDirty = true;
            }
            else
            {
                this.Window.ClientSizeChanged -= new EventHandler<EventArgs>(Window_ClientSizeChanged);
                blnWindowSizeBound = false;
                if (blnRevert)
                {
                    if (GlobalVariables.OldFullScreen != graphics.IsFullScreen)
                    {
                        Toggle = true;
                    }
                    else
                    {
                        //Reset Graphics State to Old Graphics State
                        graphics.PreferredBackBufferWidth = GlobalVariables.OldWidth;
                        graphics.PreferredBackBufferHeight = GlobalVariables.OldHeight;
                        blnIsDirty = true;

                        //Flags are set to show Pause Menu at the Options Level
                        blnRevert = false;
                        blnIsConfirming = false;
                    }
                }
                else
                {
                    if (graphics.IsFullScreen != GlobalVariables.FullScreen)
                    {
                        Toggle = true;
                        GlobalVariables.OldFullScreen = graphics.IsFullScreen;
                    }
                    //Code for Video Option Changes
                    if (GlobalVariables.NewHeight > 0 && GlobalVariables.NewWidth > 0)
                    {
                        //Track Old Values
                        GlobalVariables.OldWidth = graphics.PreferredBackBufferWidth;
                        GlobalVariables.OldHeight = graphics.PreferredBackBufferHeight;

                        //adjust screen size to fit screen
                        if (!GlobalVariables.FullScreen)
                        {
                            if (GlobalVariables.NewWidth >= GlobalVariables.ScreenWidth)
                            {
                                GlobalVariables.NewWidth = GlobalVariables.ScreenWidth;
                            }
                            if (GlobalVariables.NewHeight >= (GlobalVariables.ScreenHeight - 40))
                            {
                                GlobalVariables.NewHeight = (GlobalVariables.ScreenHeight - 40);
                            }
                        }
                        else
                        {
                            if (GlobalVariables.NewWidth >= GlobalVariables.ScreenWidth)
                            {
                                GlobalVariables.NewWidth = GlobalVariables.ScreenWidth;
                            }
                            if (GlobalVariables.NewHeight >= GlobalVariables.ScreenHeight)
                            {
                                GlobalVariables.NewHeight = GlobalVariables.ScreenHeight;
                            }
                        }

                        graphics.PreferredBackBufferWidth = GlobalVariables.NewWidth;
                        graphics.PreferredBackBufferHeight = GlobalVariables.NewHeight;
                        blnIsDirty = true;

                        //Reset New Values To 0 to pass above validation
                        GlobalVariables.NewWidth = 0;
                        GlobalVariables.NewHeight = 0;

                        //Show Dialogue to Keep or Revert
                        blnIsConfirming = true;
                    }
                }
            }
        }

        protected override void Initialize()
        {
            Components.Add(new GamerServicesComponent(this));
            base.Initialize();
            GlobalVariables.TheGame = this;
        }

        protected override void LoadContent()
        {
            this.IsMouseVisible = true;
            spriteBatch = new SpriteBatch(GraphicsDevice);
            levelSet = Content.Load<LevelSet>("realm1");
            currentlevel = levelSet.GetLevel("Level 1").CreateInstance();
            LegBG = Content.Load<Texture2D>("LegendaryBG");
            MenuBtn = Content.Load<Texture2D>("MenuBtn");
            Font1 = Content.Load<SpriteFont>("HpFont");
            imgPause = Content.Load<Texture2D>("PauseBG");
            fontMSG = Content.Load<SpriteFont>("MessageDisplay");
            texture = Content.Load<Texture2D>("Char");
            hpbarFull = Content.Load<Texture2D>("hpbarFull");
            hpbar75 = Content.Load<Texture2D>("hpbar75");
            hpbarHalf = Content.Load<Texture2D>("hpbarHalf");
            hpbarQuarter = Content.Load<Texture2D>("hpbarQuarter");
            hpbar40 = Content.Load<Texture2D>("hpbar40");
            sword = Content.Load<Texture2D>("sword1");
            Font2 = Content.Load<SpriteFont>("EnemyName");
            GlobalVariables.Font10 = Content.Load<SpriteFont>("Font10");
            GlobalVariables.Font16 = Content.Load<SpriteFont>("Font16");
            GlobalVariables.Font24 = Content.Load<SpriteFont>("Font24");
            GlobalVariables.Font20 = Content.Load<SpriteFont>("Font20");
            GlobalVariables.Font28 = Content.Load<SpriteFont>("Font28");
            GlobalVariables.Font32 = Content.Load<SpriteFont>("Font32");
            Font2 = Content.Load<SpriteFont>("EnemyName");
            sword2 = Content.Load<Texture2D>("sword2");
            InvText = Content.Load<Texture2D>("Inventory");
            buttonbg = Content.Load<Texture2D>("ButtonBG");
            legbeam = Content.Load<Texture2D>("LegBeam");
            Textbg = Content.Load<Texture2D>("ItemDscBG");
            CharScrn = Content.Load<Texture2D>("HeroSelection");
            Grass1 = Content.Load<Texture2D>("grass_tile_001");
            GlobalVariables.CheckMark = Content.Load<Texture2D>("check");

            //Set Invstart Position
            InvPos = new Vector2(50, 50);

            animatedSprite = new AnimatedSprite(texture, sword, sword2, Attack, dir, 4, 4, position);

            //Setup for Old/New Key States
            NState = Keyboard.GetState();
            kState = NState;

            //Setup for Old/New Mouse States
            mouseState = Mouse.GetState();
            MoldState = mouseState;

        }

        protected override void Update(GameTime gameTime)
        {
            if (!blnWindowSizeBound)
            {
                this.Window.ClientSizeChanged += new EventHandler<EventArgs>(Window_ClientSizeChanged);
                blnWindowSizeBound = true;
            }

            #region StartTimer

            if (blnLogTime && UpdateTimes.Count < DebugCycles)
            {
                DebugTimer = new Stopwatch();
                DebugTimer.Start();
            }

            #endregion

            #region States

            if (!base.IsActive)
            {
                CurrentGameState = GameState.Inactive;
                return;
            }

            //Variable for "New" Keyboard State
            kState = NState;
            NState = Keyboard.GetState();
            MoldState = mouseState;
            mouseState = Mouse.GetState();

            #endregion

            #region Debug

            if (kState.IsKeyUp(Keys.F3) && NState.IsKeyDown(Keys.F3))
            {
                if (!blnLogTime)
                {
                    blnLogTime = true;
                }
            }

            #endregion

            #region EscapeKey

            //Is exiting Custom Debugger, or From being stepping inside it

            if (NState.IsKeyDown(Keys.Escape) && kState.IsKeyUp(Keys.Escape))
            {
                if (blnLogTime)
                {
                    blnLogTime = false;
                    debugScreenDraw = new DebugScreen();
                    debugScreenUpdate = new DebugScreen();
                    UpdateTimes.Clear();
                    DrawTimes.Clear();
                }
                else if (blnDrawDebuggerClicked)
                {
                    blnDrawDebuggerClicked = false;
                    debugScreenDraw = new DebugScreen();
                    debugScreenUpdate = new DebugScreen();
                    debugScreenDraw_BackGround = new DebugScreen();
                    debugScreenDraw_Character = new DebugScreen();
                    debugScreenDraw_Enemies = new DebugScreen();
                    debugScreenDraw_Equipment = new DebugScreen();
                    debugScreenDraw_Inventory = new DebugScreen();
                    debugScreenDraw_Items = new DebugScreen();
                    debugScreenDraw_Other = new DebugScreen();
                    debugScreenDraw_PauseMenu = new DebugScreen();
                    UpdateTimes.Clear();
                    DrawTimes.Clear();
                    DrawTimes_BackGround.Clear();
                    DrawTimes_Character.Clear();
                    DrawTimes_Enemies.Clear();
                    DrawTimes_Equipment.Clear();
                    DrawTimes_Inventory.Clear();
                    DrawTimes_Items.Clear();
                    DrawTimes_Other.Clear();
                    DrawTimes_PauseMenu.Clear();
                    blnLogTime = true;
                }
                else
                {
                    if (CurrentGameState == GameState.Active)
                    {
                        CurrentGameState = GameState.Inactive;
                    }
                    else
                    {
                        CurrentGameState = GameState.Active;
                    }
                }

            }

            if (CurrentGameState == GameState.Inactive)
            {

                return;

            }

            #endregion

            #region Nuke
            if (Enemies.Count > 0 && NState.IsKeyDown(Keys.F2))
            {
                foreach (Enemy enemy in Enemies)
                {
                    enemy.blnDead = true;
                }
            }
            #endregion

            #region Spawner
            //Spawner
            //Spawn 15 Enemies, or Spawns more if f1 is held down
            if (Enemies.Count < 15 || NState.IsKeyDown(Keys.F1))
            {
                Valid = true;
                //Create Random Location
                position2.X = RNG.Next(1, (Rows * TileHeight));
                position2.Y = RNG.Next(1, (Columns * TileWidth));
                //Check to prevent enemies from spawn on top of each other or on Character
                if (Enemies.Count >= 0)
                {
                    if (Enemies.Count == 0)
                    {
                        //logic to prevent enemies from spawning on character, when no enemies are in list
                        if (Math.Abs((position2.X - position.X)) < 50 && Math.Abs((position2.Y - position.Y)) < 90)
                        {
                            Valid = false;
                        }
                    }
                    for (int l = 0; l < Enemies.Count; l++)
                    {
                        //Check to prevent enemies from spawning on top of eachother
                        if (Math.Abs((position2.X - Enemies[l].Location.X)) < 50 && Math.Abs((position2.Y - Enemies[l].Location.Y)) < 90)
                        {
                            Valid = false;
                            break;
                        }
                        //check to prevent enemies from spawning on top of character
                        if (Math.Abs((position2.X - position.X)) < 50 && Math.Abs((position2.Y - position.Y)) < 90)
                        {
                            Valid = false;
                            break;
                        }
                    }
                    //If Valid Spawn Point
                    if (Valid)
                    {
                        //Health Set Here
                        HP = RNG.Next(500, 1500);
                        Enemies.Add(new Enemy(texture, hpbarFull, hpbar75, hpbarHalf, hpbarQuarter, hpbar40, Font1, Font2, 4, 4, 1500, position2, name, HP));
                    }
                }
            }
            #endregion

            #region keyevent

            //Character Movement Left

            if (kState.IsKeyDown(Keys.A) || kState.IsKeyDown(Keys.Left))
            {
                //Enemy Position is set to Valid

                Valid = true;

                //Check for If Character Tried to Run offScreen

                if (position.X > 10)
                {
                    //Check for Enemies
                    if (Enemies.Count < 1)
                    {
                        //Set Character Direction of Travel

                        animatedSprite.direction = 2;

                        //Update The Look of Character

                        animatedSprite.UpdateLeft();

                        //Move Character on Screen

                        if (position.X <= graphics.PreferredBackBufferWidth / 2)
                        {
                            if (animatedSprite.WorldPos.X >= (graphics.PreferredBackBufferWidth / 2) + 10)
                            {
                                OffSet += (velocity * (float)gameTime.ElapsedGameTime.TotalSeconds);
                                IsScrolling = true;
                            }
                            else
                            {
                                position -= (velocity * (float)gameTime.ElapsedGameTime.TotalSeconds);
                                IsScrolling = false;
                            }
                        }
                        else
                        {
                            position -= (velocity * (float)gameTime.ElapsedGameTime.TotalSeconds);
                            IsScrolling = false;
                        }

                        animatedSprite.WorldPos -= (velocity * (float)gameTime.ElapsedGameTime.TotalSeconds);

                    }
                    else
                    {
                        //Check where enemies are

                        for (int l = 0; l < Enemies.Count; l++)
                        {
                            //Check to See if Char is trying to run into Enemies

                            if (Math.Abs(((position.X - 10) - Enemies[l].Location.X)) < 35 && (Math.Abs(position.Y - Enemies[l].Location.Y) < 75))
                            {
                                //Dis-Allow Char to move into new Position because they are running into an enemy

                                Valid = false;
                                break;
                            }
                        }

                        if (Valid)
                        {

                            //Allow Character to move
                            //Update Direction of Char

                            animatedSprite.direction = 2;

                            //Update Look of Char

                            animatedSprite.UpdateLeft();

                            //Update Char Position on Screen

                            if (position.X <= graphics.PreferredBackBufferWidth / 2)
                            {
                                if (animatedSprite.WorldPos.X >= (graphics.PreferredBackBufferWidth / 2) + 10)
                                {
                                    OffSet += (velocity * (float)gameTime.ElapsedGameTime.TotalSeconds);
                                    IsScrolling = true;
                                }
                                else
                                {
                                    position -= (velocity * (float)gameTime.ElapsedGameTime.TotalSeconds);
                                    IsScrolling = false;
                                }
                            }
                            else
                            {
                                position -= (velocity * (float)gameTime.ElapsedGameTime.TotalSeconds);
                                IsScrolling = false;
                            }

                            animatedSprite.WorldPos -= (velocity * (float)gameTime.ElapsedGameTime.TotalSeconds);

                            //Check for Items

                            if (DroppedItems.Count > 0)
                            {
                                //If Scrolling, Move Items With Scrolling Screen

                                if (IsScrolling)
                                {
                                    for (int lc = 0; lc < DroppedItems.Count; lc++)
                                    {
                                        DroppedItems[lc].CharMovedLeft(gameTime, velocity);
                                    }
                                }
                            }

                            //Move Enemies on Screen in relation to the Characters Position on Screen(Only used in Scrolling)

                            if (IsScrolling)
                            {
                                foreach (Enemy enemy in Enemies)
                                {
                                    enemy.CharMovedLeft(gameTime, velocity);
                                }
                            }
                        }
                        else
                        {
                            //allow char to change direction, but don't update his position on the map
                            animatedSprite.direction = 2;
                            animatedSprite.UpdateLeft();
                        }
                    }
                }
            }

            //Character Movement Right

            if (kState.IsKeyDown(Keys.D) || kState.IsKeyDown(Keys.Right))
            {
                if (position.X < graphics.PreferredBackBufferWidth - 75)
                {
                    Valid = true;
                    if (Enemies.Count < 1)
                    {
                        animatedSprite.direction = 3;
                        animatedSprite.UpdateRight();

                        if (position.X >= graphics.PreferredBackBufferWidth / 2)
                        {
                            if (animatedSprite.WorldPos.X + (graphics.PreferredBackBufferWidth / 2) <= (Columns * TileWidth) - 10)
                            {
                                OffSet -= (velocity * (float)gameTime.ElapsedGameTime.TotalSeconds);
                                IsScrolling = true;
                            }
                            else
                            {
                                position += (velocity * (float)gameTime.ElapsedGameTime.TotalSeconds);
                                IsScrolling = false;
                            }
                        }
                        else
                        {
                            position += (velocity * (float)gameTime.ElapsedGameTime.TotalSeconds);
                            IsScrolling = false;
                        }

                        animatedSprite.WorldPos += (velocity * (float)gameTime.ElapsedGameTime.TotalSeconds);
                    }
                    else
                    {
                        for (int l = 0; l < Enemies.Count; l++)
                        {
                            if (Math.Abs(((position.X + 10) - Enemies[l].Location.X)) < 35 && (Math.Abs(position.Y - Enemies[l].Location.Y) < 75))
                            {
                                Valid = false;
                                break;
                            }
                        }
                        if (Valid)
                        {

                            animatedSprite.direction = 3;
                            animatedSprite.UpdateRight();

                            if (position.X >= graphics.PreferredBackBufferWidth / 2)
                            {
                                if (animatedSprite.WorldPos.X + (graphics.PreferredBackBufferWidth / 2) <= (Columns * TileWidth) - 10)
                                {
                                    OffSet -= (velocity * (float)gameTime.ElapsedGameTime.TotalSeconds);
                                    IsScrolling = true;
                                }
                                else
                                {
                                    position += (velocity * (float)gameTime.ElapsedGameTime.TotalSeconds);
                                    IsScrolling = false;
                                }
                            }
                            else
                            {
                                position += (velocity * (float)gameTime.ElapsedGameTime.TotalSeconds);
                                IsScrolling = false;
                            }

                            animatedSprite.WorldPos += (velocity * (float)gameTime.ElapsedGameTime.TotalSeconds);

                            //Check for Items
                            if (IsScrolling)
                            {
                                if (DroppedItems.Count > 0)
                                {
                                    for (int lc = 0; lc < DroppedItems.Count; lc++)
                                    {
                                        DroppedItems[lc].CharMovedRight(gameTime, velocity);
                                    }
                                }
                            }

                            if (IsScrolling)
                            {
                                foreach (Enemy enemy in Enemies)
                                {
                                    enemy.CharMovedRight(gameTime, velocity);
                                }
                            }
                        }
                        else
                        {
                            animatedSprite.direction = 3;
                            animatedSprite.UpdateRight();
                        }
                    }
                }
            }

            //Character Movement Up
            if (kState.IsKeyDown(Keys.Up) || kState.IsKeyDown(Keys.W))
            {
                Valid = true;
                if (position.Y > -10)
                {
                    if (Enemies.Count < 1)
                    {
                        animatedSprite.direction = 4;
                        animatedSprite.UpdateUp();

                        if (position.Y <= graphics.PreferredBackBufferHeight / 2)
                        {
                            if (animatedSprite.WorldPos.Y >= (graphics.PreferredBackBufferHeight / 2) + 10)
                            {
                                OffSet += (velocityup * (float)gameTime.ElapsedGameTime.TotalSeconds);
                                IsScrolling = true;
                            }
                            else
                            {
                                position -= (velocityup * (float)gameTime.ElapsedGameTime.TotalSeconds);
                                IsScrolling = false;
                            }
                        }
                        else
                        {
                            position -= (velocityup * (float)gameTime.ElapsedGameTime.TotalSeconds);
                            IsScrolling = false;
                        }

                        animatedSprite.WorldPos -= (velocityup * (float)gameTime.ElapsedGameTime.TotalSeconds);
                    }
                    else
                    {
                        for (int l = 0; l < Enemies.Count; l++)
                        {
                            if (Math.Abs(((position.X) - Enemies[l].Location.X)) < 35 && (Math.Abs((position.Y - 10) - Enemies[l].Location.Y) < 75))
                            {
                                Valid = false;
                                break;
                            }
                        }
                        if (Valid)
                        {

                            animatedSprite.direction = 4;
                            animatedSprite.UpdateUp();

                            if (position.Y <= graphics.PreferredBackBufferHeight / 2)
                            {
                                if (animatedSprite.WorldPos.Y >= (graphics.PreferredBackBufferHeight / 2) + 10)
                                {
                                    OffSet += (velocityup * (float)gameTime.ElapsedGameTime.TotalSeconds);
                                    IsScrolling = true;
                                }
                                else
                                {
                                    position -= (velocityup * (float)gameTime.ElapsedGameTime.TotalSeconds);
                                    IsScrolling = false;
                                }
                            }
                            else
                            {
                                position -= (velocityup * (float)gameTime.ElapsedGameTime.TotalSeconds);
                                IsScrolling = false;
                            }

                            animatedSprite.WorldPos -= (velocityup * (float)gameTime.ElapsedGameTime.TotalSeconds);

                            //Check for Items
                            if (DroppedItems.Count > 0)
                            {
                                if (IsScrolling)
                                {
                                    for (int lc = 0; lc < DroppedItems.Count; lc++)
                                    {
                                        DroppedItems[lc].CharMovedUp(gameTime, velocityup);
                                    }
                                }
                            }

                            if (IsScrolling)
                            {
                                foreach (Enemy enemy in Enemies)
                                {
                                    enemy.CharMovedUp(gameTime, velocityup);
                                }
                            }
                        }
                        else
                        {
                            animatedSprite.direction = 4;
                            animatedSprite.UpdateUp();
                        }
                    }
                }
            }

            //Character Movement Down
            if (kState.IsKeyDown(Keys.Down) || kState.IsKeyDown(Keys.S))
            {
                Valid = true;
                if (position.Y < graphics.PreferredBackBufferHeight - 125)
                {
                    if (Enemies.Count < 1)
                    {
                        animatedSprite.direction = 1;
                        animatedSprite.UpdateDown();

                        if (position.Y >= graphics.PreferredBackBufferHeight / 2)
                        {
                            if (animatedSprite.WorldPos.Y + (graphics.PreferredBackBufferHeight / 2) <= (Rows * TileHeight) - 10)
                            {
                                OffSet -= (velocityup * (float)gameTime.ElapsedGameTime.TotalSeconds);
                                IsScrolling = true;
                            }
                            else
                            {
                                position += (velocityup * (float)gameTime.ElapsedGameTime.TotalSeconds);
                                IsScrolling = false;
                            }
                        }
                        else
                        {
                            position += (velocityup * (float)gameTime.ElapsedGameTime.TotalSeconds);
                            IsScrolling = false;
                        }

                        animatedSprite.WorldPos += (velocityup * (float)gameTime.ElapsedGameTime.TotalSeconds);
                    }

                    else
                    {
                        for (int l = 0; l < Enemies.Count; l++)
                        {
                            if (Math.Abs(((position.X) - Enemies[l].Location.X)) < 35 && (Math.Abs((position.Y + 10) - Enemies[l].Location.Y) < 75))
                            {
                                Valid = false;
                                break;
                            }
                        }
                        if (Valid)
                        {

                            animatedSprite.direction = 1;
                            animatedSprite.UpdateDown();

                            if (position.Y >= graphics.PreferredBackBufferHeight / 2)
                            {
                                if (animatedSprite.WorldPos.Y + (graphics.PreferredBackBufferHeight / 2) <= (Rows * TileHeight) - 10)
                                {
                                    OffSet -= (velocityup * (float)gameTime.ElapsedGameTime.TotalSeconds);
                                    IsScrolling = true;
                                }
                                else
                                {
                                    position += (velocityup * (float)gameTime.ElapsedGameTime.TotalSeconds);
                                    IsScrolling = false;
                                }
                            }
                            else
                            {
                                position += (velocityup * (float)gameTime.ElapsedGameTime.TotalSeconds);
                                IsScrolling = false;
                            }

                            animatedSprite.WorldPos += (velocityup * (float)gameTime.ElapsedGameTime.TotalSeconds);

                            //Check for Items
                            if (DroppedItems.Count > 0)
                            {
                                if (IsScrolling)
                                {
                                    for (int lc = 0; lc < DroppedItems.Count; lc++)
                                    {
                                        DroppedItems[lc].CharMovedDown(gameTime, velocityup);
                                    }
                                }

                            }

                            if (IsScrolling)
                            {
                                foreach (Enemy enemy in Enemies)
                                {
                                    enemy.CharMovedDown(gameTime, velocityup);
                                }
                            }
                        }
                        else
                        {
                            animatedSprite.direction = 1;
                            animatedSprite.UpdateDown();
                        }
                    }
                }
            }
            #endregion

            #region monster
            //Call Update Which Moves My Monsters
            if (Enemies.Count > 0)
            {
                for (int l = 0; l < Enemies.Count; l++)
                {
                    if (Enemies[l].hp <= 0)
                    {
                        //TODO : Add Death Animation
                        Enemies[l].blnDie = true;
                    }
                    if (Enemies[l].blnDead)
                    {
                        IntDrop = RNG.Next(1, 100);
                        if (IntDrop > 90)
                        {
                            DoesDrop = true;
                        }
                        if (DoesDrop)
                        {
                            DroppedItems.Add(new Item(Enemies[l].Location, sword, "Ground", Font2, LegBG, legbeam, Textbg));
                        }
                        DoesDrop = false;
                        Enemies.Remove(Enemies[l]);
                    }
                }
            }
            foreach (Enemy enemy in Enemies)
            {
                enemy.Update(gameTime, position);
            }
            #endregion

            #region UpdVariables
            int x = mouseState.X;
            int y = mouseState.Y;
            //Set Mouse Default Start Position
            Mouse.SetPosition(x, y);
            //Animation Timer
            #endregion

            #region ClickEvents

            #region Inventory
            //Clicked Inventory

            //Is Inventory Being Closed
            if (mouseState.X < 65 && mouseState.Y < 15 && mouseState.LeftButton == ButtonState.Pressed && MoldState.LeftButton == ButtonState.Released && blnOpen == true || NState.IsKeyDown(Keys.I) && !kState.IsKeyDown(Keys.I) && blnOpen == true)
            {
                blnOpen = false;
                //Disable attacks while clicked to close inventory
                BlnAttack = false;
                BlnDelay = true;
            }

            //Is Inventory Being Opened
            else if (mouseState.X < 65 && mouseState.Y < 15 && mouseState.LeftButton == ButtonState.Pressed && MoldState.LeftButton == ButtonState.Released && blnOpen == false || NState.IsKeyDown(Keys.I) && !kState.IsKeyDown(Keys.I) && blnOpen == false)
            {
                //Dis-Allow Attacking while inventory is open
                BlnAttack = false;
                //Set Boolean for Inventory being open to true
                blnOpen = true;

                //Create new inventory with no items
                inventory = new Inventory(InvItems, InvPos);
            }
            if (isDrag == true && mouseState.LeftButton == ButtonState.Pressed && MoldState.LeftButton == ButtonState.Pressed)
            {
            }
            //If Inventory Open, Check for Mouse being Clicked To Drag
            else if (blnOpen && (mouseState.X - InvPos.X) > 17 && (mouseState.X - InvPos.X) < 388 && (mouseState.Y - InvPos.Y) > 8 && (mouseState.Y - InvPos.Y) < 70 && mouseState.LeftButton == ButtonState.Pressed && MoldState.LeftButton == ButtonState.Pressed)
            {
                //Start Inventory Drag
                isDrag = true;
            }
            else
            {
                isDrag = false;
                MouseOffSetx = 0f;
                MouseOffSety = 0f;
            }

            if (isDrag)
            {
                BlnAttack = false;
                //Set MouseOffset for beginning of drag
                if (MouseOffSetx == 0 && MouseOffSety == 0)
                {
                    MouseOffSetx = (mouseState.X - InvPos.X);
                    MouseOffSety = (mouseState.Y - InvPos.Y);
                }

                //Pin the Inventory Texture's position to the mouse while the button is held
                InvPos = new Vector2((mouseState.X - MouseOffSetx), (mouseState.Y - MouseOffSety));
                if (InvPos.X < 0)
                {
                    InvPos.X = 1;
                }
                if ((InvPos.X + 250) > graphics.PreferredBackBufferWidth)
                {
                    InvPos.X = (graphics.PreferredBackBufferWidth - 250);
                }
                if ((InvPos.Y + 300) > graphics.PreferredBackBufferHeight)
                {
                    InvPos.Y = (graphics.PreferredBackBufferHeight - 300);
                }
                if (InvPos.Y < 5)
                {
                    InvPos.Y = 6;
                }

                //Update Item Boxes in relation to the Inventory Background
                inventory.Update(InvPos);

            }

            //Is Equip being closed
            if (mouseState.X > 75 && mouseState.X < 145 && mouseState.Y < 15 && mouseState.LeftButton == ButtonState.Pressed && MoldState.LeftButton == ButtonState.Released && blnEquip == true || NState.IsKeyDown(Keys.U) && !kState.IsKeyDown(Keys.U) && blnEquip == true)
            {
                blnEquip = false;
                //Disable attacks while clicked to close equip
                BlnAttack = false;
                BlnDelay = true;
            }

            //Is Equipment being Opened
            else if (mouseState.X > 75 && mouseState.X < 145 && mouseState.Y < 15 && mouseState.LeftButton == ButtonState.Pressed && MoldState.LeftButton == ButtonState.Released && blnEquip == false || NState.IsKeyDown(Keys.I) && !kState.IsKeyDown(Keys.I) && blnEquip == false)
            {
                //Dis-Allow Attacking while equip is open
                BlnAttack = false;
                //Set Boolean for equip being open to true
                blnEquip = true;

                //Create new equip
                equipment = new Equipment(EquipItem);
            }

            //Inventory Open
            if (blnOpen)
            {
                for (int intlc = 0; intlc < InvItems.Count; intlc++)
                {
                    if ((mouseState.X - InvItems[intlc].location.X) < 50 && (mouseState.X - InvItems[intlc].location.X) > 0 && (mouseState.Y - InvItems[intlc].location.Y) < 30 && (mouseState.Y - InvItems[intlc].location.Y) > 0)
                    {
                        InvItems[intlc].invhover = true;
                    }
                    else
                    {
                        InvItems[intlc].invhover = false;
                    }
                }
            }

            for (int lc = 0; lc < DroppedItems.Count; lc++)
            {
                if ((mouseState.X - DroppedItems[lc].location.X) > 0 && (mouseState.X - DroppedItems[lc].location.X) < 75 && (mouseState.Y - DroppedItems[lc].location.Y) > 0 && (mouseState.Y - DroppedItems[lc].location.Y) < 40)
                {
                    DroppedItems[lc].hover = true;
                }
                else
                {
                    DroppedItems[lc].hover = false;
                }
                if (DroppedItems[lc].hover && mouseState.LeftButton == ButtonState.Pressed && MoldState.LeftButton == ButtonState.Released)
                {
                    if (InvItems.Count == 20)
                    {
                        MSG.tmrDSP = 0f;
                        blnDSP = true;
                        BlnAttack = false;
                        BlnDelay = true;
                        break;
                    }
                    else
                    {
                        InvItems.Add(DroppedItems[lc]);
                        DroppedItems.Remove(DroppedItems[lc]);
                        BlnAttack = false;
                        BlnDelay = true;
                        break;
                    }
                }
            }

            #endregion

            #region Attack
            //Timer while Animation is in effect
            if (AniTick)
                AnimationTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            //Delay After Attack(Attack Speed)
            if (BlnDelay)
                AtkTmr += (float)gameTime.ElapsedGameTime.TotalSeconds;

            //Conditions for Attack to be possible
            if (mouseState.LeftButton == ButtonState.Pressed && AnimationTimer < AnimationDuration && (AtkTmr <= TimeBetweenAttacks) && BlnAttack)
            {
                //Set Initial Sword Position for each given Direction
                if (animatedSprite.direction == 1)
                    animatedSprite.i = 0f;
                if (animatedSprite.direction == 2)
                    animatedSprite.i = 2f;
                if (animatedSprite.direction == 3)
                    animatedSprite.i = -2f;
                if (animatedSprite.direction == 4)
                    animatedSprite.i = 4f;

                //Activate Attack Animation
                animatedSprite.attack = true;
                //Start Animation Timer
                AniTick = true;

                //Boolean for Can Attack is set to False
                BlnAttack = false;
            }

            //Check to See if Animation is Finished, If so Resets Timers and Starts Attack Delay timer
            if (AnimationTimer >= AnimationDuration)
            {
                //Reset Timer
                AnimationTimer = 0;
                //Stop Timer
                AniTick = false;
                //Start Delay
                BlnDelay = true;
                //Check Character's Direction and Attack Boolean and Set Rotation of Image(Sword)
                if (animatedSprite.direction == 1 && animatedSprite.attack == true)
                    animatedSprite.i = 0f;
                if (animatedSprite.direction == 2 && animatedSprite.attack == true)
                    animatedSprite.i = 2f;
                if (animatedSprite.direction == 3 && animatedSprite.attack == true)
                    animatedSprite.i = -2f;
                if (animatedSprite.direction == 4 && animatedSprite.attack == true)
                    animatedSprite.i = 4f;
            }

            //Check For Completion of Delay after Attacks
            if (AtkTmr >= TimeBetweenAttacks)
            {
                //Reset Timer
                AtkTmr = 0;
                //Stop Timer
                BlnDelay = false;
                BlnAttack = true;
            }
            #endregion

            #endregion

            #region HitMonster
            for (int l = 0; l < Enemies.Count; l++)
            {
                if (Math.Abs((animatedSprite.SwordTip.X - (Enemies[l].Location.X + (Enemies[l].graphicwidth / 2)))) < 30 && Math.Abs((animatedSprite.SwordTip.Y - (Enemies[l].Location.Y + (Enemies[l].graphicheight / 2)))) < 40 && animatedSprite.attack)
                {
                    //TODO : Need to Make Damage Based Stat
                    //Remove HP Per Hit From Enemy

                    DamageCounter = RNG.Next(15, 50);
                    Enemies[l].hp -= DamageCounter;
                    Enemies[l].DamageCounter += DamageCounter;
                    if (animatedSprite.blnDisplaydamage)
                        Enemies[l].blnShowDamage = true;
                    else
                        Enemies[l].blnShowDamage = false;
                    //Enemies.Remove(Enemies[l]);
                    //break;
                }
            }
            #endregion

            #region Global Updates

            //Update The Game
            base.Update(gameTime);

            if (blnLogTime && UpdateTimes.Count < DebugCycles && DebugTimer != null)
            {
                DebugTimer.Stop();
                UpdateTimes.Add(DebugTimer.ElapsedTicks);
                DebugTimer.Reset();
            }

            #endregion

        }

        protected override void Draw(GameTime gameTime)
        {
            if (blnLogTime && DrawTimes.Count < DebugCycles)
            {
                DebugTimer = new Stopwatch();
                DebugTimer.Start();
            }

            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            if (blnLogTime && DrawTimes_BackGround.Count < DebugCycles)
            {
                DebugTimer1 = new Stopwatch();
                DebugTimer1.Start();
            }

            //Draw BackGround

            backGround.Draw(spriteBatch, graphics, Grass1, Rows, Columns, TileHeight, TileWidth, OffSet);

            if (blnLogTime && DrawTimes_BackGround.Count < DebugCycles && DebugTimer1 != null)
            {
                DebugTimer1.Stop();
                DrawTimes_BackGround.Add(DebugTimer1.ElapsedTicks);
                DebugTimer1.Reset();
            }

            spriteBatch.End();

            if (blnLogTime && DrawTimes_Items.Count < DebugCycles)
            {
                DebugTimer1 = new Stopwatch();
                DebugTimer1.Start();
            }

            foreach (Item item in DroppedItems)
            {
                item.Draw(spriteBatch, item.location);
            }

            if (blnLogTime && DrawTimes_Items.Count < DebugCycles && DebugTimer1 != null)
            {
                DebugTimer1.Stop();
                DrawTimes_Items.Add(DebugTimer1.ElapsedTicks);
                DebugTimer1.Reset();
            }

            if (blnLogTime && DrawTimes_Character.Count < DebugCycles)
            {
                DebugTimer1 = new Stopwatch();
                DebugTimer1.Start();
            }

            //Draw character
            animatedSprite.Draw(spriteBatch, position);

            if (blnLogTime && DrawTimes_Character.Count < DebugCycles && DebugTimer1 != null)
            {
                DebugTimer1.Stop();
                DrawTimes_Character.Add(DebugTimer1.ElapsedTicks);
                DebugTimer1.Reset();
            }

            if (blnLogTime && DrawTimes_Enemies.Count < DebugCycles)
            {
                DebugTimer1 = new Stopwatch();
                DebugTimer1.Start();
            }

            //Draw Each Enemy in List
            foreach (Enemy enemy in Enemies)
            {
                enemy.Draw(spriteBatch, enemy.Location);
            }

            if (blnLogTime && DrawTimes_Enemies.Count < DebugCycles && DebugTimer1 != null)
            {
                DebugTimer1.Stop();
                DrawTimes_Enemies.Add(DebugTimer1.ElapsedTicks);
                DebugTimer1.Reset();
            }

            if (blnLogTime && DrawTimes_Inventory.Count < DebugCycles)
            {
                DebugTimer1 = new Stopwatch();
                DebugTimer1.Start();
            }

            //Inventory Selector

            spriteBatch.Begin();
            if (blnOpen)
            {
                spriteBatch.Draw(InvText, new Rectangle((int)InvPos.X, (int)InvPos.Y, 250, 300), Color.White);
                if (InvItems.Count > 0)
                {
                    inventory.Draw(spriteBatch);
                }
            }

            if (blnLogTime && DrawTimes_Inventory.Count < DebugCycles && DebugTimer1 != null)
            {
                DebugTimer1.Stop();
                DrawTimes_Inventory.Add(DebugTimer1.ElapsedTicks);
                DebugTimer1.Reset();
            }

            if (blnLogTime && DrawTimes_Equipment.Count < DebugCycles)
            {
                DebugTimer1 = new Stopwatch();
                DebugTimer1.Start();
            }

            //Display Equipment

            if (blnEquip)
            {
                //Draw Empty Background for Character Equipment

                equipment.draw(spriteBatch, CharScrn, Font2);
            }

            if (blnLogTime && DrawTimes_Equipment.Count < DebugCycles && DebugTimer1 != null)
            {
                DebugTimer1.Stop();
                DrawTimes_Equipment.Add(DebugTimer1.ElapsedTicks);
                DebugTimer1.Reset();
            }

            if (blnLogTime && DrawTimes_Other.Count < DebugCycles)
            {
                DebugTimer1 = new Stopwatch();
                DebugTimer1.Start();
            }

            //Display Message

            if (blnDSP)
            {
                MSG.draw(spriteBatch, gameTime, "Your Inventory Is Full!", fontMSG);
            }

            spriteBatch.Draw(buttonbg, new Rectangle(7, 1, 58, 12), Color.Black);
            spriteBatch.Draw(buttonbg, new Rectangle(75, 1, 70, 12), Color.Black);
            spriteBatch.DrawString(Font2, "Inventory", new Vector2(10, -2), Color.Beige);
            spriteBatch.DrawString(Font2, "Equipment", new Vector2(80, -2), Color.Beige);
            spriteBatch.End();

            if (blnLogTime && DrawTimes_Other.Count < DebugCycles && DebugTimer1 != null)
            {
                DebugTimer1.Stop();
                DrawTimes_Other.Add(DebugTimer1.ElapsedTicks);
                DebugTimer1.Reset();
            }

            if (blnLogTime && DrawTimes_PauseMenu.Count < DebugCycles)
            {
                DebugTimer1 = new Stopwatch();
                DebugTimer1.Start();
            }

            //Pause Menu

            if (CurrentGameState == GameState.Inactive)
            {

                SpriteFont TheFont = GlobalVariables.AutoFont(graphics, 1);

                if (blnIsConfirming)
                {
                    if (ResolutionConfirm == null)
                    {
                        ResolutionConfirm = new PauseMenu();
                        ResolutionConfirmChangeBound = false;
                        ResolutionConfirmRevertBound = false;
                    }

                    if (!ResolutionConfirmChangeBound)
                    {
                        ResolutionConfirm.ConfirmChange += ConfirmChange;
                        ResolutionConfirmChangeBound = true;
                    }
                    if (!ResolutionConfirmRevertBound)
                    {
                        ResolutionConfirm.RevertChange += RevertChange;
                        ResolutionConfirmRevertBound = true;
                    }

                    ResolutionConfirm.Draw(spriteBatch, imgPause, MenuBtn, TheFont, graphics, Font2, 2);

                }
                else
                {

                    if (pauseMenu == null)
                    {
                        pauseMenu = new PauseMenu();
                        pauseMenuBound = false;
                        pauseMenuExitBound = false;
                        pauseMenuResolutionBound = false;
                    }
                    if (!pauseMenuBound)
                    {
                        pauseMenu.Resume += ResumeGame;
                        pauseMenuBound = true;
                    }
                    if (!pauseMenuExitBound)
                    {
                        pauseMenu.Exit += ExitGame;
                        pauseMenuExitBound = true;
                    }
                    if (!pauseMenuResolutionBound)
                    {
                        pauseMenu.ResolutionChange += ResolutionChange;
                        pauseMenuResolutionBound = true;
                    }

                    pauseMenu.Draw(spriteBatch, imgPause, MenuBtn, TheFont, graphics, Font2, 1);
                }
            }

            if (blnLogTime && DrawTimes_PauseMenu.Count < DebugCycles && DebugTimer1 != null)
            {
                DebugTimer1.Stop();
                DrawTimes_PauseMenu.Add(DebugTimer1.ElapsedTicks);
                DebugTimer1.Reset();
            }

            //Custom Debugger
            if (!debugScreenDrawBgClickedBound)
            {
                debugScreenDraw.DrawBgClicked += DrawLogClicked;
                debugScreenDrawBgClickedBound = true;
            }

            if (blnLogTime)
            {
                if (UpdateTimes.Count == DebugCycles)
                {
                    debugScreenUpdate.Draw(spriteBatch, Font2, GlobalVariables.Font24, false, graphics, UpdateTimes, "Update()", base.ToString(), DebugCycles.ToString(), MenuBtn, 1);
                }
                else
                {
                    debugScreenUpdate.Draw(spriteBatch, Font2, GlobalVariables.Font24, true, graphics, UpdateTimes, "Update()", base.ToString(), DebugCycles.ToString(), MenuBtn, 1);
                }
                if (DrawTimes.Count == DebugCycles)
                {
                    debugScreenDraw.Draw(spriteBatch, Font2, GlobalVariables.Font24, false, graphics, DrawTimes, "Draw()", base.ToString(), DebugCycles.ToString(), MenuBtn, 2);
                }
                else
                {
                    debugScreenDraw.Draw(spriteBatch, Font2, GlobalVariables.Font24, true, graphics, DrawTimes, "Draw()", base.ToString(), DebugCycles.ToString(), MenuBtn, 2);
                }
            }
            else if (blnDrawDebuggerClicked)
            {
                if (DrawTimes_BackGround.Count < DebugCycles || DrawTimes_Character.Count < DebugCycles || DrawTimes_Enemies.Count < DebugCycles || DrawTimes_Equipment.Count < DebugCycles
                     || DrawTimes_Inventory.Count < DebugCycles || DrawTimes_Items.Count < DebugCycles || DrawTimes_Other.Count < DebugCycles || DrawTimes_PauseMenu.Count < DebugCycles)
                {
                    blnLogTime = true;
                    debugScreenDraw.Draw(spriteBatch, Font2, GlobalVariables.Font24, true, graphics, DrawTimes, "BackGround.Draw()", base.ToString(), DebugCycles.ToString(), MenuBtn, 1);
                }
                else
                {
                    if (DrawTimes_BackGround.Count == DebugCycles)
                    {
                        debugScreenDraw_BackGround.Draw(spriteBatch, Font2, GlobalVariables.Font24, false, graphics, DrawTimes_BackGround, "BackGround.Draw()", base.ToString(), DebugCycles.ToString(), MenuBtn, 1);
                    }
                    if (DrawTimes_Character.Count == DebugCycles)
                    {
                        debugScreenDraw_Character.Draw(spriteBatch, Font2, GlobalVariables.Font24, false, graphics, DrawTimes_Character, "Character.Draw()", base.ToString(), DebugCycles.ToString(), MenuBtn, 2);
                    }
                    if (DrawTimes_Enemies.Count == DebugCycles)
                    {
                        debugScreenDraw_Enemies.Draw(spriteBatch, Font2, GlobalVariables.Font24, false, graphics, DrawTimes_Enemies, "Enemy.Draw()", base.ToString(), DebugCycles.ToString(), MenuBtn, 3);
                    }
                    if (DrawTimes_Equipment.Count == DebugCycles)
                    {
                        debugScreenDraw_Equipment.Draw(spriteBatch, Font2, GlobalVariables.Font24, false, graphics, DrawTimes_Equipment, "Equipment.Draw()", base.ToString(), DebugCycles.ToString(), MenuBtn, 4);
                    }
                    if (DrawTimes_Inventory.Count == DebugCycles)
                    {
                        debugScreenDraw_Inventory.Draw(spriteBatch, Font2, GlobalVariables.Font24, false, graphics, DrawTimes_Inventory, "Inventory.Draw()", base.ToString(), DebugCycles.ToString(), MenuBtn, 5);
                    }
                    if (DrawTimes_Items.Count == DebugCycles)
                    {
                        debugScreenDraw_Items.Draw(spriteBatch, Font2, GlobalVariables.Font24, false, graphics, DrawTimes_Items, "Item.Draw()", base.ToString(), DebugCycles.ToString(), MenuBtn, 6);
                    }
                    if (DrawTimes_Other.Count == DebugCycles)
                    {
                        debugScreenDraw_Other.Draw(spriteBatch, Font2, GlobalVariables.Font24, false, graphics, DrawTimes_Other, "Other.Draw()", base.ToString(), DebugCycles.ToString(), MenuBtn, 7);
                    }
                    if (DrawTimes_PauseMenu.Count == DebugCycles)
                    {
                        debugScreenDraw_PauseMenu.Draw(spriteBatch, Font2, GlobalVariables.Font24, false, graphics, DrawTimes_PauseMenu, "Pause.Draw()", base.ToString(), DebugCycles.ToString(), MenuBtn, 8);
                    }
                }
            }

            if (blnIsDirty)
            {
                graphics.ApplyChanges();
                blnIsDirty = false;
            }

            if (Toggle)
            {
                graphics.ToggleFullScreen();
                Toggle = false;
            }

            base.Draw(gameTime);

            if (blnLogTime && DrawTimes.Count < DebugCycles && DebugTimer != null)
            {
                DebugTimer.Stop();
                DrawTimes.Add(DebugTimer.ElapsedTicks);
                DebugTimer.Reset();
            }
        }

        private void ConfirmChange(object sender, EventArgs e)
        {
            blnIsConfirming = false;
            pauseMenu.blnVideoOpen = true;
            pauseMenu.DefaultPauseOpen = false;
            GlobalVariables.SaveUserSettings();
        }

        private void RevertChange(object sender, EventArgs e)
        {
            pauseMenu.blnIndexValues = true;
            blnRevert = true;
            blnIsConfirming = false;
            Window_ClientSizeChanged(this, EventArgs.Empty);
            pauseMenu.blnVideoOpen = true;
            pauseMenu.DefaultPauseOpen = false;
            GlobalVariables.SaveUserSettings();
        }

        private void ResolutionChange(object sender, EventArgs e)
        {
            Window_ClientSizeChanged(this, EventArgs.Empty);
        }

        public void ResumeGame(object sender, EventArgs eventArgs)
        {
            CurrentGameState = GameState.Active;
        }

        public void ExitGame(object sender, EventArgs eventArgs)
        {
            this.Exit();
        }

        public void DrawLogClicked(object sender, EventArgs eventArgs)
        {
            blnDrawDebuggerClicked = true;
            blnLogTime = false;
        }
    }
}
