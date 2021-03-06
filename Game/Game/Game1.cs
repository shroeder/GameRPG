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
        public MouseState MoldState;
        public MouseState mouseState;

        public Vector2 OffSet = new Vector2(0, 0);
        public Vector2 position = new Vector2(150, 150);
        public Vector2 velocity = new Vector2(300, 0);
        public Vector2 velocityup = new Vector2(0, 300);
        private Vector2 InvPos = new Vector2(150, 150);
        private Vector2 OldPos = new Vector2(0, 0);

        public bool AttackTimerOn = false;
        public bool canAttack = false;
        public bool ItemHovered = false;
        public bool blnClamp = false;
        public bool LeftMouseHeld = false;
        public bool PathFinding = true;
        public bool Toggle = false;
        public bool Valid = true;
        public bool blnRevert = false;
        public bool blnEquip = false;
        public bool blnOpen = false;
        private bool blnIgnore = false;
        private bool IsScrolling = false;
        private bool blnLogTime = false;
        private bool blnDrawDebuggerClicked = false;
        private bool isDrag = false;
        private bool DoesDrop = false;
        private bool blnDSP = false;
        private bool blnPaused = false;
        private bool blnIsConfirming = false;
        private bool debugScreenDrawBgClickedBound = false;
        private bool pauseMenuExitBound = false;
        private bool pauseMenuResolutionBound = false;
        private bool pauseMenuBound = false;
        private bool ResolutionConfirmChangeBound = false;
        private bool ResolutionConfirmRevertBound = false;
        private bool blnIsDirty = false;

        public int EnemyTextureColumns;
        public int EnemyTextureRows;
        public int CountMouseHeld;
        public int dir = 1;
        public int Columns = 26;
        public int Rows = 26;
        public int TileWidth = 160;
        public int TileHeight = 160;
        private int DebugCycles = 40;
        private int IntDrop = 0;
        private int HP;
        private int MaxHP;
        private int EnemyEvasion;
        private int EnemyArmour;
        private int Intlc;

        public GameTime theGameTime { get; set; }

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
        public Equipment equipment;
        public Inventory inventory;
        public AnimatedSprite animatedSprite;
        public Item theItem;

        public Stopwatch AttackTimer;
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
        private List<Item> EquipItem = new List<Item>();
        public List<Item> DroppedItems = new List<Item>();
        public List<Enemy> Enemies = new List<Enemy>();

        private Rectangle TileRect = new Rectangle(0, 0, 80, 80);

        private float MouseOffSetx = 0f;
        private float MouseOffSety = 0f;

        public Texture2D CharWeapon;
        public Texture2D CharBoots;
        public Texture2D CharChest;
        public Texture2D CharPants;
        public Texture2D CharBelt;
        public Texture2D CharHelmet;
        public Texture2D CharGloves;
        public Texture2D CharShoulders;
        private Texture2D EnemyTexture;
        private Texture2D texture;
        public Texture2D HeroTxt;
        private Texture2D MenuBtn;
        private Texture2D CharScrn;
        private Texture2D Grass1;
        private Texture2D hpbarFull;
        public Texture2D InvText;
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

        public GraphicsDeviceManager graphics;
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
            GlobalVariables.Rows = 26;
            GlobalVariables.Columns = 26;
            GlobalVariables.TileHeight = 160;
            GlobalVariables.TileWidth = 160;

            GlobalVariables.LoadGameData();

            graphics = new GraphicsDeviceManager(this);
            GlobalVariables.gfx = graphics;

            GlobalVariables.ScreenWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width; ;
            GlobalVariables.ScreenHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;

            GlobalVariables.LoadUserSettings();

            if (GlobalVariables.UserSetWidth > 0 && GlobalVariables.UserSetHeight > 0)
            {
                if (!GlobalVariables.FullScreen)
                {
                    if (GlobalVariables.UserSetHeight > GlobalVariables.ScreenHeight)
                    {
                        graphics.PreferredBackBufferHeight = GlobalVariables.ScreenHeight;
                    }
                    else
                    {
                        graphics.PreferredBackBufferHeight = GlobalVariables.UserSetHeight;
                    }
                    if (GlobalVariables.UserSetWidth > GlobalVariables.ScreenWidth)
                    {
                        graphics.PreferredBackBufferWidth = GlobalVariables.ScreenWidth;
                    }
                    else
                    {
                        graphics.PreferredBackBufferWidth = GlobalVariables.UserSetWidth;
                    }
                }

                graphics.IsFullScreen = GlobalVariables.UserSetFullScreen;
                GlobalVariables.FullScreen = GlobalVariables.UserSetFullScreen;
            }
            else
            {
                graphics.PreferredBackBufferWidth = GlobalVariables.ScreenWidth;
                graphics.PreferredBackBufferHeight = GlobalVariables.ScreenHeight;
                GlobalVariables.UserSetWidth = GlobalVariables.ScreenWidth;
                GlobalVariables.UserSetHeight = GlobalVariables.ScreenHeight;
                graphics.IsFullScreen = false;
                GlobalVariables.FullScreen = false;
            }

            Content.RootDirectory = "Content";
            IsFixedTimeStep = true;
            TargetElapsedTime = TimeSpan.FromMilliseconds(20);
        }

        void Window_ClientSizeChanged(object sender, EventArgs e)
        {
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
                    GlobalVariables.UserSetHeight = GlobalVariables.OldWidth;
                    GlobalVariables.UserSetHeight = GlobalVariables.OldHeight;
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

                    GlobalVariables.UserSetHeight = GlobalVariables.NewHeight;
                    GlobalVariables.UserSetWidth = GlobalVariables.NewWidth;

                    if (GlobalVariables.NewWidth >= GlobalVariables.ScreenWidth)
                    {
                        GlobalVariables.NewWidth = GlobalVariables.ScreenWidth;
                    }
                    if (GlobalVariables.NewHeight >= GlobalVariables.ScreenHeight)
                    {
                        GlobalVariables.NewHeight = GlobalVariables.ScreenHeight;
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

        protected override void Initialize()
        {
            IntPtr hWnd = this.Window.Handle;
            var control = System.Windows.Forms.Control.FromHandle(hWnd);
            var form = control.FindForm();
            form.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            if (GraphicsDevice.DisplayMode.Width == graphics.PreferredBackBufferWidth && GraphicsDevice.DisplayMode.Height == graphics.PreferredBackBufferHeight)
            {
                form.Location = new System.Drawing.Point(0, 0);
            }
            CountMouseHeld = 0;
            GlobalVariables.CurrentDir = GlobalVariables.Dir.Nothing;
            Components.Add(new GamerServicesComponent(this));
            base.Initialize();
            GlobalVariables.TheGame = this;

            if (GlobalVariables.Equipment == null)
            {
                GlobalVariables.TheHero = new HeroDisplay();
            }
            else if (GlobalVariables.Equipment.Hero == null)
            {
                GlobalVariables.TheHero = new HeroDisplay();
            }
            else
            {
                GlobalVariables.TheHero = GlobalVariables.Equipment.Hero;
            }

            if (GlobalVariables.Inventory != null)
            {
                inventory = GlobalVariables.Inventory;
            }
            else
            {
                inventory = new Inventory(new List<Item>(), new Vector2(150, 150));
                GlobalVariables.Inventory = inventory;
            }

            if (GlobalVariables.Equipment != null)
            {
                equipment = new Equipment(GlobalVariables.Equipment.Helmet, GlobalVariables.Equipment.Shoulders, GlobalVariables.Equipment.Chest, GlobalVariables.Equipment.Pants, GlobalVariables.Equipment.RightWeapon, GlobalVariables.Equipment.LeftWeapon, GlobalVariables.Equipment.Gloves, GlobalVariables.Equipment.Boots, GlobalVariables.Equipment.Belt, GlobalVariables.Equipment.LeftRing, GlobalVariables.Equipment.RightRing, GlobalVariables.TheHero);

            }
            else
            {
                equipment = new Equipment(null, null, null, null, null, null, null, null, null, null, null, GlobalVariables.TheHero, false);
                GlobalVariables.Equipment = equipment;
            }

            if (equipment.Helmet != null)
            {
                Item tempItem = equipment.Helmet;
                tempItem.ItemTexture = Content.Load<Texture2D>(equipment.Helmet.DroppedTextureName);
                equipment.Helmet = new Item(tempItem.location, tempItem.ItemTexture, tempItem.ItemType, tempItem.ItemLevel, tempItem.ItemSlot, tempItem.DroppedTextureName, tempItem.BaseStat, tempItem.BaseStatName, tempItem.SubType, true);
                equipment.Hero.txtHelm = Content.Load<Texture2D>(tempItem.ItemTextureName);
                equipment.Hero.txtHero = Content.Load<Texture2D>("SpriteSheetHelm");
                animatedSprite.CharHelm = Content.Load<Texture2D>(tempItem.ItemTextureName);
                animatedSprite.Texture = Content.Load<Texture2D>("SpriteSheetHelm");
                equipment.Helmet.affixes = tempItem.affixes;
                equipment.Helmet.ItemDescription = tempItem.ItemDescription;
                equipment.Helmet.AffixList = tempItem.AffixList;
                equipment.Helmet.ItemName = tempItem.ItemName;
                equipment.Helmet.quality = tempItem.quality;
                equipment.Helmet.ItemTextureName = tempItem.ItemTextureName;
                if (equipment.Helmet.quality == 1)
                {
                    equipment.Helmet.RarityColor = Color.White;
                    equipment.Helmet.ItemColor = Color.White;
                }
                if (equipment.Helmet.quality == 2)
                {
                    equipment.Helmet.RarityColor = Color.AliceBlue;
                    equipment.Helmet.ItemColor = Color.AliceBlue;
                }
                if (equipment.Helmet.quality == 3)
                {
                    equipment.Helmet.RarityColor = Color.DeepSkyBlue;
                    equipment.Helmet.ItemColor = Color.DeepSkyBlue;
                }
                if (equipment.Helmet.quality == 4)
                {
                    equipment.Helmet.RarityColor = Color.Orange;
                    equipment.Helmet.ItemColor = Color.Orange;
                }
                if (equipment.Helmet.quality == 5)
                {
                    equipment.Helmet.RarityColor = Color.Purple;
                    equipment.Helmet.ItemColor = Color.NavajoWhite;
                }
                if (equipment.Helmet.quality == 6)
                {
                    equipment.Helmet.RarityColor = Color.Brown;
                    equipment.Helmet.ItemColor = Color.White;
                }
            }

            if (equipment.Shoulders != null)
            {
                Item tempItem = equipment.Shoulders;
                tempItem.ItemTexture = Content.Load<Texture2D>(equipment.Shoulders.DroppedTextureName);
                equipment.Shoulders = new Item(tempItem.location, tempItem.ItemTexture, tempItem.ItemType, tempItem.ItemLevel, tempItem.ItemSlot, tempItem.DroppedTextureName, tempItem.BaseStat, tempItem.BaseStatName, tempItem.SubType, true);
                equipment.Hero.txtShoulders = Content.Load<Texture2D>(tempItem.ItemTextureName);
                animatedSprite.CharShoulders = Content.Load<Texture2D>(tempItem.ItemTextureName);
                equipment.Shoulders.affixes = tempItem.affixes;
                equipment.Shoulders.AffixList = tempItem.AffixList;
                equipment.Shoulders.ItemDescription = tempItem.ItemDescription;
                equipment.Shoulders.ItemName = tempItem.ItemName;
                equipment.Shoulders.quality = tempItem.quality;
                equipment.Shoulders.ItemTextureName = tempItem.ItemTextureName;
                if (equipment.Shoulders.quality == 1)
                {
                    equipment.Shoulders.RarityColor = Color.White;
                    equipment.Shoulders.ItemColor = Color.White;
                }
                if (equipment.Shoulders.quality == 2)
                {
                    equipment.Shoulders.RarityColor = Color.AliceBlue;
                    equipment.Shoulders.ItemColor = Color.AliceBlue;
                }
                if (equipment.Shoulders.quality == 3)
                {
                    equipment.Shoulders.RarityColor = Color.DeepSkyBlue;
                    equipment.Shoulders.ItemColor = Color.DeepSkyBlue;
                }
                if (equipment.Shoulders.quality == 4)
                {
                    equipment.Shoulders.RarityColor = Color.Orange;
                    equipment.Shoulders.ItemColor = Color.Orange;
                }
                if (equipment.Shoulders.quality == 5)
                {
                    equipment.Shoulders.RarityColor = Color.Purple;
                    equipment.Shoulders.ItemColor = Color.NavajoWhite;
                }
                if (equipment.Shoulders.quality == 6)
                {
                    equipment.Shoulders.RarityColor = Color.Brown;
                    equipment.Shoulders.ItemColor = Color.White;
                }
            }

            if (equipment.Chest != null)
            {
                Item tempItem = equipment.Chest;
                tempItem.ItemTexture = Content.Load<Texture2D>(equipment.Chest.DroppedTextureName);
                equipment.Chest = new Item(tempItem.location, tempItem.ItemTexture, tempItem.ItemType, tempItem.ItemLevel, tempItem.ItemSlot, tempItem.DroppedTextureName, tempItem.BaseStat, tempItem.BaseStatName, tempItem.SubType, true);
                equipment.Hero.txtChest = Content.Load<Texture2D>(tempItem.ItemTextureName);
                animatedSprite.CharChest = Content.Load<Texture2D>(tempItem.ItemTextureName);
                equipment.Chest.affixes = tempItem.affixes;
                equipment.Chest.ItemDescription = tempItem.ItemDescription;
                equipment.Chest.AffixList = tempItem.AffixList;
                equipment.Chest.ItemName = tempItem.ItemName;
                equipment.Chest.quality = tempItem.quality;
                equipment.Chest.ItemTextureName = tempItem.ItemTextureName;
                if (equipment.Chest.quality == 1)
                {
                    equipment.Chest.RarityColor = Color.White;
                    equipment.Chest.ItemColor = Color.White;
                }
                if (equipment.Chest.quality == 2)
                {
                    equipment.Chest.RarityColor = Color.AliceBlue;
                    equipment.Chest.ItemColor = Color.AliceBlue;
                }
                if (equipment.Chest.quality == 3)
                {
                    equipment.Chest.RarityColor = Color.DeepSkyBlue;
                    equipment.Chest.ItemColor = Color.DeepSkyBlue;
                }
                if (equipment.Chest.quality == 4)
                {
                    equipment.Chest.RarityColor = Color.Orange;
                    equipment.Chest.ItemColor = Color.Orange;
                }
                if (equipment.Chest.quality == 5)
                {
                    equipment.Chest.RarityColor = Color.Purple;
                    equipment.Chest.ItemColor = Color.NavajoWhite;
                }
                if (equipment.Chest.quality == 6)
                {
                    equipment.Chest.RarityColor = Color.Brown;
                    equipment.Chest.ItemColor = Color.White;
                }
            }

            if (equipment.Pants != null)
            {
                Item tempItem = equipment.Pants;
                tempItem.ItemTexture = Content.Load<Texture2D>(equipment.Pants.DroppedTextureName);
                equipment.Pants = new Item(tempItem.location, tempItem.ItemTexture, tempItem.ItemType, tempItem.ItemLevel, tempItem.ItemSlot, tempItem.DroppedTextureName, tempItem.BaseStat, tempItem.BaseStatName, tempItem.SubType, true);
                equipment.Hero.txtPants = Content.Load<Texture2D>(tempItem.ItemTextureName);
                animatedSprite.CharPants = Content.Load<Texture2D>(tempItem.ItemTextureName);
                equipment.Pants.affixes = tempItem.affixes;
                equipment.Pants.ItemDescription = tempItem.ItemDescription;
                equipment.Pants.AffixList = tempItem.AffixList;
                equipment.Pants.ItemName = tempItem.ItemName;
                equipment.Pants.quality = tempItem.quality;
                equipment.Pants.ItemTextureName = tempItem.ItemTextureName;
                if (equipment.Pants.quality == 1)
                {
                    equipment.Pants.RarityColor = Color.White;
                    equipment.Pants.ItemColor = Color.White;
                }
                if (equipment.Pants.quality == 2)
                {
                    equipment.Pants.RarityColor = Color.AliceBlue;
                    equipment.Pants.ItemColor = Color.AliceBlue;
                }
                if (equipment.Pants.quality == 3)
                {
                    equipment.Pants.RarityColor = Color.DeepSkyBlue;
                    equipment.Pants.ItemColor = Color.DeepSkyBlue;
                }
                if (equipment.Pants.quality == 4)
                {
                    equipment.Pants.RarityColor = Color.Orange;
                    equipment.Pants.ItemColor = Color.Orange;
                }
                if (equipment.Pants.quality == 5)
                {
                    equipment.Pants.RarityColor = Color.Purple;
                    equipment.Pants.ItemColor = Color.NavajoWhite;
                }
                if (equipment.Pants.quality == 6)
                {
                    equipment.Pants.RarityColor = Color.Brown;
                    equipment.Pants.ItemColor = Color.White;
                }
            }

            if (equipment.RightWeapon != null)
            {
                Item tempItem = equipment.RightWeapon;
                tempItem.ItemTexture = Content.Load<Texture2D>(equipment.RightWeapon.DroppedTextureName);
                equipment.RightWeapon = new Item(tempItem.location, tempItem.ItemTexture, tempItem.ItemType, tempItem.ItemLevel, tempItem.ItemSlot, tempItem.DroppedTextureName, tempItem.BaseStat, tempItem.BaseStatName, tempItem.SubType, true, tempItem.BaseAttackSpeed);
                equipment.Hero.txtRightWeapon = Content.Load<Texture2D>(tempItem.ItemTextureName);
                animatedSprite.CharWeapon = Content.Load<Texture2D>(tempItem.ItemTextureName);
                equipment.RightWeapon.affixes = tempItem.affixes;
                equipment.RightWeapon.ItemDescription = tempItem.ItemDescription;
                equipment.RightWeapon.AffixList = tempItem.AffixList;
                equipment.RightWeapon.ItemName = tempItem.ItemName;
                equipment.RightWeapon.quality = tempItem.quality;
                equipment.RightWeapon.ItemTextureName = tempItem.ItemTextureName;
                if (equipment.RightWeapon.quality == 1)
                {
                    equipment.RightWeapon.RarityColor = Color.White;
                    equipment.RightWeapon.ItemColor = Color.White;
                }
                if (equipment.RightWeapon.quality == 2)
                {
                    equipment.RightWeapon.RarityColor = Color.AliceBlue;
                    equipment.RightWeapon.ItemColor = Color.AliceBlue;
                }
                if (equipment.RightWeapon.quality == 3)
                {
                    equipment.RightWeapon.RarityColor = Color.DeepSkyBlue;
                    equipment.RightWeapon.ItemColor = Color.DeepSkyBlue;
                }
                if (equipment.RightWeapon.quality == 4)
                {
                    equipment.RightWeapon.RarityColor = Color.Orange;
                    equipment.RightWeapon.ItemColor = Color.Orange;
                }
                if (equipment.RightWeapon.quality == 5)
                {
                    equipment.RightWeapon.RarityColor = Color.Purple;
                    equipment.RightWeapon.ItemColor = Color.NavajoWhite;
                }
                if (equipment.RightWeapon.quality == 6)
                {
                    equipment.RightWeapon.RarityColor = Color.Brown;
                    equipment.RightWeapon.ItemColor = Color.White;
                }
            }

            if (equipment.LeftWeapon != null)
            {
                Item tempItem = equipment.LeftWeapon;
                tempItem.ItemTexture = Content.Load<Texture2D>(equipment.LeftWeapon.DroppedTextureName);
                equipment.LeftWeapon = new Item(tempItem.location, tempItem.ItemTexture, tempItem.ItemType, tempItem.ItemLevel, tempItem.ItemSlot, tempItem.DroppedTextureName, tempItem.BaseStat, tempItem.BaseStatName, tempItem.SubType, true, tempItem.BaseAttackSpeed);
                equipment.Hero.txtLeftWeapon = Content.Load<Texture2D>(tempItem.ItemTextureName);
                animatedSprite.CharWeapon = Content.Load<Texture2D>(tempItem.ItemTextureName);
                equipment.LeftWeapon.affixes = tempItem.affixes;
                equipment.LeftWeapon.AffixList = tempItem.AffixList;
                equipment.LeftWeapon.ItemName = tempItem.ItemName;
                equipment.LeftWeapon.ItemDescription = tempItem.ItemDescription;
                equipment.LeftWeapon.quality = tempItem.quality;
                equipment.LeftWeapon.ItemTextureName = tempItem.ItemTextureName;
                if (equipment.LeftWeapon.quality == 1)
                {
                    equipment.LeftWeapon.RarityColor = Color.White;
                    equipment.LeftWeapon.ItemColor = Color.White;
                }
                if (equipment.LeftWeapon.quality == 2)
                {
                    equipment.LeftWeapon.RarityColor = Color.AliceBlue;
                    equipment.LeftWeapon.ItemColor = Color.AliceBlue;
                }
                if (equipment.LeftWeapon.quality == 3)
                {
                    equipment.LeftWeapon.RarityColor = Color.DeepSkyBlue;
                    equipment.LeftWeapon.ItemColor = Color.DeepSkyBlue;
                }
                if (equipment.LeftWeapon.quality == 4)
                {
                    equipment.LeftWeapon.RarityColor = Color.Orange;
                    equipment.LeftWeapon.ItemColor = Color.Orange;
                }
                if (equipment.LeftWeapon.quality == 5)
                {
                    equipment.LeftWeapon.RarityColor = Color.Purple;
                    equipment.LeftWeapon.ItemColor = Color.NavajoWhite;
                }
                if (equipment.LeftWeapon.quality == 6)
                {
                    equipment.LeftWeapon.RarityColor = Color.Brown;
                    equipment.LeftWeapon.ItemColor = Color.White;
                }
            }

            if (equipment.Gloves != null)
            {
                Item tempItem = equipment.Gloves;
                tempItem.ItemTexture = Content.Load<Texture2D>(equipment.Gloves.DroppedTextureName);
                equipment.Gloves = new Item(tempItem.location, tempItem.ItemTexture, tempItem.ItemType, tempItem.ItemLevel, tempItem.ItemSlot, tempItem.DroppedTextureName, tempItem.BaseStat, tempItem.BaseStatName, tempItem.SubType, true);
                equipment.Hero.txtGloves = Content.Load<Texture2D>(tempItem.ItemTextureName);
                animatedSprite.CharGloves = Content.Load<Texture2D>(tempItem.ItemTextureName);
                equipment.Gloves.affixes = tempItem.affixes;
                equipment.Gloves.ItemDescription = tempItem.ItemDescription;
                equipment.Gloves.AffixList = tempItem.AffixList;
                equipment.Gloves.ItemName = tempItem.ItemName;
                equipment.Gloves.quality = tempItem.quality;
                equipment.Gloves.ItemTextureName = tempItem.ItemTextureName;
                if (equipment.Gloves.quality == 1)
                {
                    equipment.Gloves.RarityColor = Color.White;
                    equipment.Gloves.ItemColor = Color.White;
                }
                if (equipment.Gloves.quality == 2)
                {
                    equipment.Gloves.RarityColor = Color.AliceBlue;
                    equipment.Gloves.ItemColor = Color.AliceBlue;
                }
                if (equipment.Gloves.quality == 3)
                {
                    equipment.Gloves.RarityColor = Color.DeepSkyBlue;
                    equipment.Gloves.ItemColor = Color.DeepSkyBlue;
                }
                if (equipment.Gloves.quality == 4)
                {
                    equipment.Gloves.RarityColor = Color.Orange;
                    equipment.Gloves.ItemColor = Color.Orange;
                }
                if (equipment.Gloves.quality == 5)
                {
                    equipment.Gloves.RarityColor = Color.Purple;
                    equipment.Gloves.ItemColor = Color.NavajoWhite;
                }
                if (equipment.Gloves.quality == 6)
                {
                    equipment.Gloves.RarityColor = Color.Brown;
                    equipment.Gloves.ItemColor = Color.White;
                }
            }

            if (equipment.Boots != null)
            {
                Item tempItem = equipment.Boots;
                tempItem.ItemTexture = Content.Load<Texture2D>(equipment.Boots.DroppedTextureName);
                equipment.Boots = new Item(tempItem.location, tempItem.ItemTexture, tempItem.ItemType, tempItem.ItemLevel, tempItem.ItemSlot, tempItem.DroppedTextureName, tempItem.BaseStat, tempItem.BaseStatName, tempItem.SubType, true);
                equipment.Hero.txtBoots = Content.Load<Texture2D>(tempItem.ItemTextureName);
                animatedSprite.CharBoots = Content.Load<Texture2D>(tempItem.ItemTextureName);
                equipment.Boots.affixes = tempItem.affixes;
                equipment.Boots.ItemDescription = tempItem.ItemDescription;
                equipment.Boots.AffixList = tempItem.AffixList;
                equipment.Boots.ItemName = tempItem.ItemName;
                equipment.Boots.quality = tempItem.quality;
                equipment.Boots.ItemTextureName = tempItem.ItemTextureName;
                if (equipment.Boots.quality == 1)
                {
                    equipment.Boots.RarityColor = Color.White;
                    equipment.Boots.ItemColor = Color.White;
                }
                if (equipment.Boots.quality == 2)
                {
                    equipment.Boots.RarityColor = Color.AliceBlue;
                    equipment.Boots.ItemColor = Color.AliceBlue;
                }
                if (equipment.Boots.quality == 3)
                {
                    equipment.Boots.RarityColor = Color.DeepSkyBlue;
                    equipment.Boots.ItemColor = Color.DeepSkyBlue;
                }
                if (equipment.Boots.quality == 4)
                {
                    equipment.Boots.RarityColor = Color.Orange;
                    equipment.Boots.ItemColor = Color.Orange;
                }
                if (equipment.Boots.quality == 5)
                {
                    equipment.Boots.RarityColor = Color.Purple;
                    equipment.Boots.ItemColor = Color.NavajoWhite;
                }
                if (equipment.Boots.quality == 6)
                {
                    equipment.Boots.RarityColor = Color.Brown;
                    equipment.Boots.ItemColor = Color.White;
                }
            }

            if (equipment.Belt != null)
            {
                Item tempItem = equipment.Belt;
                tempItem.ItemTexture = Content.Load<Texture2D>(equipment.Belt.DroppedTextureName);
                equipment.Belt = new Item(tempItem.location, tempItem.ItemTexture, tempItem.ItemType, tempItem.ItemLevel, tempItem.ItemSlot, tempItem.DroppedTextureName, tempItem.BaseStat, tempItem.BaseStatName, tempItem.SubType, true);
                //equipment.Hero.txtBelt = Content.Load<Texture2D>(tempItem.ItemTextureName);
                equipment.Belt.ItemDescription = tempItem.ItemDescription;
                equipment.Belt.affixes = tempItem.affixes;
                equipment.Belt.AffixList = tempItem.AffixList;
                equipment.Belt.ItemName = tempItem.ItemName;
                equipment.Belt.quality = tempItem.quality;
                equipment.Belt.ItemTextureName = tempItem.ItemTextureName;
                if (equipment.Belt.quality == 1)
                {
                    equipment.Belt.RarityColor = Color.White;
                    equipment.Belt.ItemColor = Color.White;
                }
                if (equipment.Belt.quality == 2)
                {
                    equipment.Belt.RarityColor = Color.AliceBlue;
                    equipment.Belt.ItemColor = Color.AliceBlue;
                }
                if (equipment.Belt.quality == 3)
                {
                    equipment.Belt.RarityColor = Color.DeepSkyBlue;
                    equipment.Belt.ItemColor = Color.DeepSkyBlue;
                }
                if (equipment.Belt.quality == 4)
                {
                    equipment.Belt.RarityColor = Color.Orange;
                    equipment.Belt.ItemColor = Color.Orange;
                }
                if (equipment.Belt.quality == 5)
                {
                    equipment.Belt.RarityColor = Color.Purple;
                    equipment.Belt.ItemColor = Color.NavajoWhite;
                }
                if (equipment.Belt.quality == 6)
                {
                    equipment.Belt.RarityColor = Color.Brown;
                    equipment.Belt.ItemColor = Color.White;
                }
            }

            if (equipment.LeftRing != null)
            {
                Item tempItem = equipment.LeftRing;
                tempItem.ItemTexture = Content.Load<Texture2D>(equipment.LeftRing.DroppedTextureName);
                equipment.LeftRing = new Item(tempItem.location, tempItem.ItemTexture, tempItem.ItemType, tempItem.ItemLevel, tempItem.ItemSlot, tempItem.DroppedTextureName, tempItem.BaseStat, tempItem.BaseStatName, tempItem.SubType, true);
                //equipment.Hero.txtLeftRing = Content.Load<Texture2D>(tempItem.ItemTextureName);
                equipment.LeftRing.affixes = tempItem.affixes;
                equipment.LeftRing.ItemDescription = tempItem.ItemDescription;
                equipment.LeftRing.AffixList = tempItem.AffixList;
                equipment.LeftRing.ItemName = tempItem.ItemName;
                equipment.LeftRing.quality = tempItem.quality;
                equipment.LeftRing.ItemTextureName = tempItem.ItemTextureName;
                if (equipment.LeftRing.quality == 1)
                {
                    equipment.LeftRing.RarityColor = Color.White;
                    equipment.LeftRing.ItemColor = Color.White;
                }
                if (equipment.LeftRing.quality == 2)
                {
                    equipment.LeftRing.RarityColor = Color.AliceBlue;
                    equipment.LeftRing.ItemColor = Color.AliceBlue;
                }
                if (equipment.LeftRing.quality == 3)
                {
                    equipment.LeftRing.RarityColor = Color.DeepSkyBlue;
                    equipment.LeftRing.ItemColor = Color.DeepSkyBlue;
                }
                if (equipment.LeftRing.quality == 4)
                {
                    equipment.LeftRing.RarityColor = Color.Orange;
                    equipment.LeftRing.ItemColor = Color.Orange;
                }
                if (equipment.LeftRing.quality == 5)
                {
                    equipment.LeftRing.RarityColor = Color.Purple;
                    equipment.LeftRing.ItemColor = Color.NavajoWhite;
                }
                if (equipment.LeftRing.quality == 6)
                {
                    equipment.LeftRing.RarityColor = Color.Brown;
                    equipment.LeftRing.ItemColor = Color.White;
                }
            }

            if (equipment.RightRing != null)
            {
                Item tempItem = equipment.RightRing;
                tempItem.ItemTexture = Content.Load<Texture2D>(equipment.RightRing.DroppedTextureName);
                equipment.RightRing = new Item(tempItem.location, tempItem.ItemTexture, tempItem.ItemType, tempItem.ItemLevel, tempItem.ItemSlot, tempItem.DroppedTextureName, tempItem.BaseStat, tempItem.BaseStatName, tempItem.SubType, true);
                //equipment.Hero.txtRightRing = Content.Load<Texture2D>(tempItem.ItemTextureName);
                equipment.RightRing.affixes = tempItem.affixes;
                equipment.RightRing.ItemDescription = tempItem.ItemDescription;
                equipment.RightRing.AffixList = tempItem.AffixList;
                equipment.RightRing.ItemName = tempItem.ItemName;
                equipment.RightRing.quality = tempItem.quality;
                equipment.RightRing.ItemTextureName = tempItem.ItemTextureName;
                if (equipment.RightRing.quality == 1)
                {
                    equipment.RightRing.RarityColor = Color.White;
                    equipment.RightRing.ItemColor = Color.White;
                }
                if (equipment.RightRing.quality == 2)
                {
                    equipment.RightRing.RarityColor = Color.AliceBlue;
                    equipment.RightRing.ItemColor = Color.AliceBlue;
                }
                if (equipment.RightRing.quality == 3)
                {
                    equipment.RightRing.RarityColor = Color.DeepSkyBlue;
                    equipment.RightRing.ItemColor = Color.DeepSkyBlue;
                }
                if (equipment.RightRing.quality == 4)
                {
                    equipment.RightRing.RarityColor = Color.Orange;
                    equipment.RightRing.ItemColor = Color.Orange;
                }
                if (equipment.RightRing.quality == 5)
                {
                    equipment.RightRing.RarityColor = Color.Purple;
                    equipment.RightRing.ItemColor = Color.NavajoWhite;
                }
                if (equipment.RightRing.quality == 6)
                {
                    equipment.RightRing.RarityColor = Color.Brown;
                    equipment.RightRing.ItemColor = Color.White;
                }
            }

            if (inventory != null)
            {
                if (inventory.Items.Count() > 0)
                {
                    for (int intlc = 0; intlc < inventory.Items.Count(); intlc++)
                    {
                        if (inventory.Items[intlc] != null)
                        {
                            Item tempItem = inventory.Items[intlc];
                            tempItem.ItemTexture = Content.Load<Texture2D>(inventory.Items[intlc].DroppedTextureName);
                            inventory.Items[intlc] = new Item(tempItem.location, tempItem.ItemTexture, tempItem.ItemType, tempItem.ItemLevel, tempItem.ItemSlot, tempItem.DroppedTextureName, tempItem.BaseStat, tempItem.BaseStatName, tempItem.SubType, true, tempItem.BaseAttackSpeed);
                            inventory.Items[intlc].affixes = tempItem.affixes;
                            inventory.Items[intlc].ItemDescription = tempItem.ItemDescription;
                            inventory.Items[intlc].AffixList = tempItem.AffixList;
                            inventory.Items[intlc].ItemName = tempItem.ItemName;
                            inventory.Items[intlc].quality = tempItem.quality;
                            inventory.Items[intlc].ItemTextureName = tempItem.ItemTextureName;
                            if (inventory.Items[intlc].quality == 1)
                            {
                                inventory.Items[intlc].RarityColor = Color.White;
                                inventory.Items[intlc].ItemColor = Color.White;
                            }
                            if (inventory.Items[intlc].quality == 2)
                            {
                                inventory.Items[intlc].RarityColor = Color.AliceBlue;
                                inventory.Items[intlc].ItemColor = Color.AliceBlue;
                            }
                            if (inventory.Items[intlc].quality == 3)
                            {
                                inventory.Items[intlc].RarityColor = Color.DeepSkyBlue;
                                inventory.Items[intlc].ItemColor = Color.DeepSkyBlue;
                            }
                            if (inventory.Items[intlc].quality == 4)
                            {
                                inventory.Items[intlc].RarityColor = Color.Orange;
                                inventory.Items[intlc].ItemColor = Color.Orange;
                            }
                            if (inventory.Items[intlc].quality == 5)
                            {
                                inventory.Items[intlc].RarityColor = Color.Purple;
                                inventory.Items[intlc].ItemColor = Color.NavajoWhite;
                            }
                            if (inventory.Items[intlc].quality == 6)
                            {
                                inventory.Items[intlc].RarityColor = Color.Brown;
                                inventory.Items[intlc].ItemColor = Color.White;
                            }
                        }
                    }
                }
            }
            GlobalVariables.UpdateStats();
            AttackTimer = new Stopwatch();
            AttackTimer.Start();
            AttackTimerOn = true;

        }

        protected override void LoadContent()
        {

            //Indexed to test sword, need to create equipment menu where we then update this value

            if (equipment == null)
            {
                HeroTxt = Content.Load<Texture2D>("HeroSS2H");
            }
            else
            {
                if (equipment.LeftWeapon.ItemSlot == Item.itemSlot.TwoHanded || equipment.RightWeapon.ItemSlot == Item.itemSlot.TwoHanded)
                {
                    HeroTxt = Content.Load<Texture2D>("HeroSS2H");
                    CharWeapon = Content.Load<Texture2D>(equipment.LeftWeapon.ItemTextureName);
                }
                else
                {
                    //no Unarmed attack set yet
                    HeroTxt = Content.Load<Texture2D>("HeroSS2H");
                }
            }

            this.IsMouseVisible = true;
            spriteBatch = new SpriteBatch(GraphicsDevice);
            GlobalVariables.RotateClock = Content.Load<Texture2D>("RotateClock");
            GlobalVariables.RotateCounter = Content.Load<Texture2D>("RotateCounter");
            GlobalVariables.LegendaryBG = Content.Load<Texture2D>("LegendaryBG");
            MenuBtn = Content.Load<Texture2D>("MenuBtn");
            GlobalVariables.txtButton = Content.Load<Texture2D>("MenuBtn");
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
            GlobalVariables.TestSquare = Content.Load<Texture2D>("testSquare");
            GlobalVariables.Font6 = Content.Load<SpriteFont>("Font6");
            GlobalVariables.Font8 = Content.Load<SpriteFont>("Font8");
            GlobalVariables.Font10 = Content.Load<SpriteFont>("Font10");
            GlobalVariables.Font12 = Content.Load<SpriteFont>("Font12");
            GlobalVariables.font14 = Content.Load<SpriteFont>("Font14");
            GlobalVariables.Font16 = Content.Load<SpriteFont>("Font16");
            GlobalVariables.Font24 = Content.Load<SpriteFont>("Font24");
            GlobalVariables.Font20 = Content.Load<SpriteFont>("Font20");
            GlobalVariables.Font28 = Content.Load<SpriteFont>("Font28");
            GlobalVariables.Font32 = Content.Load<SpriteFont>("Font32");
            Font2 = Content.Load<SpriteFont>("EnemyName");
            sword2 = Content.Load<Texture2D>("sword2");
            InvText = Content.Load<Texture2D>("Inventory");
            buttonbg = Content.Load<Texture2D>("ButtonBG");
            GlobalVariables.LegendaryBeam = Content.Load<Texture2D>("LegBeam");
            GlobalVariables.TextureBack = Content.Load<Texture2D>("ItemDscBG");
            CharScrn = Content.Load<Texture2D>("Equipment");
            Grass1 = Content.Load<Texture2D>("grass_tile_001");
            GlobalVariables.CheckMark = Content.Load<Texture2D>("check");

            //Set Invstart Position
            InvPos = new Vector2(50, 50);

            animatedSprite = new AnimatedSprite(HeroTxt, dir, 8, 6, position, CharWeapon);

            //Setup for Old/New Key States
            NState = Keyboard.GetState();
            kState = NState;

            //Setup for Old/New Mouse States
            mouseState = Mouse.GetState();
            MoldState = mouseState;

        }

        protected override void Update(GameTime gameTime)
        {

            if (AttackTimerOn)
            {
                if (AttackTimer.ElapsedMilliseconds > 1000 / GlobalVariables._CharacterAttackSpeed)
                {
                    AttackTimer.Stop();
                    AttackTimer.Reset();
                    AttackTimerOn = false;
                    canAttack = true;
                }
                else
                {
                    canAttack = false;
                }
            }
            else
            {
                //temp fix to bug at load that has it at false even tho i set it to true
                if (!canAttack)
                {
                    canAttack = true;
                }
            }


            ItemHovered = false;

            if (!PathFinding)
            {
                Intlc += 1;
                if (OldPos.X == 0 && OldPos.Y == 0)
                {
                    OldPos = animatedSprite.WorldPos;
                }
                else
                {
                    if (Intlc >= 20)
                    {
                        Vector2 newvalue = OldPos - animatedSprite.WorldPos;
                        if (newvalue.X < 50 && newvalue.X > -50 && newvalue.Y < 50 && newvalue.Y > -50)
                        {
                            PathFinding = true;
                            GlobalVariables.CurrentDir = GlobalVariables.Dir.Nothing;
                            GlobalVariables.MoveToLoc = new Vector2(0, 0);
                        }
                        else
                        {
                            OldPos = new Vector2(0, 0);
                            Intlc = 0;
                        }
                    }
                }
            }
            else
            {
                OldPos = new Vector2(0, 0);
                Intlc = 0;
            }

            theGameTime = gameTime;

            //Inventory

            if (mouseState.X < 65 && mouseState.Y < 15 && mouseState.LeftButton == ButtonState.Pressed && MoldState.LeftButton == ButtonState.Released && blnOpen == true || NState.IsKeyDown(Keys.I) && !kState.IsKeyDown(Keys.I) && blnOpen == true)
            {
                blnOpen = false;
            }

            else if (mouseState.X < 65 && mouseState.Y < 15 && mouseState.LeftButton == ButtonState.Pressed && MoldState.LeftButton == ButtonState.Released && blnOpen == false || NState.IsKeyDown(Keys.I) && !kState.IsKeyDown(Keys.I) && blnOpen == false)
            {
                blnOpen = true;
            }

            else if (blnOpen && (mouseState.X - InvPos.X) > 17 && (mouseState.X - InvPos.X) < 388 && (mouseState.Y - InvPos.Y) > 8 && (mouseState.Y - InvPos.Y) < 70 && mouseState.LeftButton == ButtonState.Pressed && MoldState.LeftButton == ButtonState.Pressed)
            {
                isDrag = true;
            }
            else if (isDrag && MoldState.LeftButton == ButtonState.Pressed && mouseState.LeftButton == ButtonState.Pressed)
            {
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
                if (MouseOffSetx == 0 && MouseOffSety == 0)
                {
                    MouseOffSetx = (mouseState.X - InvPos.X);
                    MouseOffSety = (mouseState.Y - InvPos.Y);
                }

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

            }

            if (mouseState.X > 75 && mouseState.X < 145 && mouseState.Y < 15 && mouseState.LeftButton == ButtonState.Pressed && MoldState.LeftButton == ButtonState.Released && blnEquip == false || NState.IsKeyDown(Keys.E) && !kState.IsKeyDown(Keys.E) && blnEquip == false)
            {
                blnEquip = true;
            }

            else if (mouseState.X > 75 && mouseState.X < 145 && mouseState.Y < 15 && mouseState.LeftButton == ButtonState.Pressed && MoldState.LeftButton == ButtonState.Released && blnEquip == true || NState.IsKeyDown(Keys.E) && !kState.IsKeyDown(Keys.E) && blnEquip == true)
            {
                blnEquip = false;
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
                blnPaused = true;
                return;
            }

            if (pauseMenu != null)
            {
                pauseMenu.Update();
            }
            //Variable for "New" Keyboard State
            kState = NState;
            NState = Keyboard.GetState();
            MoldState = mouseState;
            mouseState = Mouse.GetState();

            #endregion

            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                CountMouseHeld += 1;
            }
            else
            {
                CountMouseHeld = 0;
            }

            if (CountMouseHeld > 10)
            {
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    LeftMouseHeld = true;
                }
                else
                {
                    LeftMouseHeld = false;
                    CountMouseHeld = 0;
                }
            }
            else
            {
                LeftMouseHeld = false;
            }

            #region Debug

            if (kState.IsKeyUp(Keys.F3) && NState.IsKeyDown(Keys.F3))
            {
                if (!blnLogTime)
                {
                    blnLogTime = true;
                }
            }

            if (kState.IsKeyUp(Keys.F10) && NState.IsKeyDown(Keys.F10))
            {
                GlobalVariables.SaveGameData();
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
                    if (blnOpen || blnEquip)
                    {
                        blnOpen = false;
                        blnEquip = false;
                    }
                    else if (CurrentGameState == GameState.Active)
                    {
                        CurrentGameState = GameState.Inactive;
                        blnPaused = true;
                    }
                    else
                    {
                        if (!blnIsConfirming)
                        {
                            blnPaused = false;
                            pauseMenu = null;
                            CurrentGameState = GameState.Active;
                            if (!AttackTimerOn)
                            {
                                AttackTimerOn = true;
                            }
                            if (!AttackTimer.IsRunning)
                            {
                                AttackTimer.Start();
                            }
                        }
                    }
                }

            }

            if (CurrentGameState == GameState.Inactive)
            {
                if (AttackTimer.IsRunning)
                {
                    AttackTimer.Stop();
                }
                if (AttackTimerOn)
                {
                    AttackTimerOn = false;
                }
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
            if (Enemies.Count < 1 || NState.IsKeyDown(Keys.F1))
            {
                Valid = true;
                //Create Random Enemy
                int Enemy = RNG.Next(1, 1);
                switch (Enemy)
                {

                    case 1:

                        EnemyTexture = texture;
                        EnemyTextureColumns = 4;
                        EnemyTextureRows = 4;
                        name = "Turd Burglar";
                        EnemyEvasion = 40;
                        EnemyArmour = 40;
                        HP = RNG.Next(500, 1500);
                        MaxHP = 1500;
                        break;

                }
                //Create Random Location
                int EnemyHeight = EnemyTexture.Height / EnemyTextureRows;
                int EnemyWidth = EnemyTexture.Width / EnemyTextureColumns;
                int maxX = (Columns - 10) * TileWidth;
                int maxY = (Rows - 10) * TileHeight;
                Rectangle SpawnPos = new Rectangle(RNG.Next(100, maxX), RNG.Next(100, maxY), EnemyWidth, EnemyHeight);
                //Check to prevent enemies from spawn on top of each other or on Character
                if (Enemies.Count >= 0)
                {
                    if (Enemies.Count == 0)
                    {
                        //logic to prevent enemies from spawning on character, when no enemies are in list
                        if (SpawnPos.Intersects(animatedSprite.Bounds))
                        {
                            Valid = false;
                        }
                    }
                    for (int l = 0; l < Enemies.Count; l++)
                    {
                        //Check to prevent enemies from spawning on top of eachother
                        if (SpawnPos.Intersects(Enemies[l].Bounds))
                        {
                            Valid = false;
                            break;
                        }
                        //check to prevent enemies from spawning on top of character
                        if (SpawnPos.Intersects(animatedSprite.Bounds))
                        {
                            Valid = false;
                            break;
                        }
                    }
                    //If Valid Spawn Point
                    if (Valid)
                    {
                        Vector2 SpawnLocation = new Vector2(SpawnPos.X, SpawnPos.Y);
                        Enemies.Add(new Enemy(EnemyTexture, hpbarFull, hpbar75, hpbarHalf, hpbarQuarter, hpbar40, Font1, Font2, 4, 4, MaxHP, SpawnLocation, name, HP, GlobalVariables.CharacterLevel, EnemyEvasion, EnemyArmour));
                    }
                }
            }
            #endregion

            #region keyevent

            if (kState.IsKeyDown(Keys.A) || kState.IsKeyDown(Keys.W) || kState.IsKeyDown(Keys.S) || kState.IsKeyDown(Keys.D) || kState.IsKeyDown(Keys.Left) || kState.IsKeyDown(Keys.Up) || kState.IsKeyDown(Keys.Down) || kState.IsKeyDown(Keys.Right))
            {
                PathFinding = true;
                GlobalVariables.CurrentDir = GlobalVariables.Dir.Nothing;
            }

            //Character Movement Left

            if (kState.IsKeyDown(Keys.A) || kState.IsKeyDown(Keys.Left) || GlobalVariables.CurrentDir == GlobalVariables.Dir.Left || GlobalVariables.CurrentDir == GlobalVariables.Dir.UpLeft || GlobalVariables.CurrentDir == GlobalVariables.Dir.DownLeft)
            {
                Valid = true;

                if (position.X > 10)
                {
                    if (Enemies.Count < 1)
                    {
                        animatedSprite.direction = 2;
                        animatedSprite.UpdateLeft();
                        if (position.X <= graphics.PreferredBackBufferWidth / 2)
                        {
                            if (animatedSprite.WorldPos.X >= (graphics.PreferredBackBufferWidth / 2) + 10)
                            {
                                OffSet += (velocity * (float)gameTime.ElapsedGameTime.TotalSeconds);
                                if (GlobalVariables.MoveToLoc.X > 0 && GlobalVariables.MoveToLoc.Y > 0)
                                {
                                    GlobalVariables.MoveToLoc += (velocity * (float)gameTime.ElapsedGameTime.TotalSeconds);
                                }
                                IsScrolling = true;
                            }
                            else
                            {
                                position -= (velocity * (float)gameTime.ElapsedGameTime.TotalSeconds);
                                animatedSprite.position -= (velocity * (float)gameTime.ElapsedGameTime.TotalSeconds);
                                IsScrolling = false;
                            }
                        }
                        else
                        {
                            position -= (velocity * (float)gameTime.ElapsedGameTime.TotalSeconds);
                            animatedSprite.position -= (velocity * (float)gameTime.ElapsedGameTime.TotalSeconds);
                            IsScrolling = false;
                        }

                        animatedSprite.WorldPos -= (velocity * (float)gameTime.ElapsedGameTime.TotalSeconds);
                    }
                    else
                    {
                        Rectangle newRectangle = animatedSprite.Bounds;
                        newRectangle.X -= Convert.ToInt32((velocity * (float)gameTime.ElapsedGameTime.TotalSeconds).X);

                        for (int l = 0; l < Enemies.Count; l++)
                        {

                            if (newRectangle.Intersects(Enemies[l].Bounds) && !Enemies[l].blnDie)
                            {
                                Valid = false;
                                break;
                            }
                        }

                        if (Valid)
                        {
                            animatedSprite.direction = 2;
                            animatedSprite.UpdateLeft();
                            if (position.X <= graphics.PreferredBackBufferWidth / 2)
                            {
                                if (animatedSprite.WorldPos.X >= (graphics.PreferredBackBufferWidth / 2) + 10)
                                {
                                    OffSet += (velocity * (float)gameTime.ElapsedGameTime.TotalSeconds);
                                    if (GlobalVariables.MoveToLoc.X > 0 && GlobalVariables.MoveToLoc.Y > 0)
                                    {
                                        GlobalVariables.MoveToLoc += (velocity * (float)gameTime.ElapsedGameTime.TotalSeconds);
                                    }
                                    IsScrolling = true;
                                }
                                else
                                {
                                    position -= (velocity * (float)gameTime.ElapsedGameTime.TotalSeconds);
                                    animatedSprite.position -= (velocity * (float)gameTime.ElapsedGameTime.TotalSeconds);
                                    IsScrolling = false;
                                }
                            }
                            else
                            {
                                position -= (velocity * (float)gameTime.ElapsedGameTime.TotalSeconds);
                                animatedSprite.position -= (velocity * (float)gameTime.ElapsedGameTime.TotalSeconds);
                                IsScrolling = false;
                            }

                            animatedSprite.WorldPos -= (velocity * (float)gameTime.ElapsedGameTime.TotalSeconds);

                            if (DroppedItems.Count > 0)
                            {
                                if (IsScrolling)
                                {
                                    if (theItem != null)
                                    {
                                        theItem.Bounds.X += (int)(velocity * (float)gameTime.ElapsedGameTime.TotalSeconds).X;
                                    }
                                    for (int lc = 0; lc < DroppedItems.Count; lc++)
                                    {
                                        DroppedItems[lc].CharMovedLeft(gameTime, velocity);
                                    }
                                }
                            }

                            if (IsScrolling)
                            {
                                foreach (Enemy enemy in Enemies)
                                {
                                    enemy.Location += (velocity * (float)gameTime.ElapsedGameTime.TotalSeconds);
                                }
                            }
                        }
                        else
                        {
                            animatedSprite.direction = 2;
                            animatedSprite.UpdateLeft();
                        }
                    }
                }
            }

            //Character Movement Right

            if (kState.IsKeyDown(Keys.D) || kState.IsKeyDown(Keys.Right) || GlobalVariables.CurrentDir == GlobalVariables.Dir.Right || GlobalVariables.CurrentDir == GlobalVariables.Dir.UpRight || GlobalVariables.CurrentDir == GlobalVariables.Dir.DownRight)
            {
                if (position.X < graphics.PreferredBackBufferWidth - animatedSprite.Width)
                {
                    Valid = true;
                    if (Enemies.Count < 1)
                    {
                        animatedSprite.direction = 3;
                        animatedSprite.UpdateRight();

                        if (position.X >= graphics.PreferredBackBufferWidth / 2)
                        {
                            if (animatedSprite.WorldPos.X + (graphics.PreferredBackBufferWidth / 2) <= ((Columns - 10) * TileWidth) - 10)
                            {
                                OffSet -= (velocity * (float)gameTime.ElapsedGameTime.TotalSeconds);
                                if (GlobalVariables.MoveToLoc.X > 0 && GlobalVariables.MoveToLoc.Y > 0)
                                {
                                    GlobalVariables.MoveToLoc -= (velocity * (float)gameTime.ElapsedGameTime.TotalSeconds);
                                }
                                IsScrolling = true;
                            }
                            else
                            {
                                position += (velocity * (float)gameTime.ElapsedGameTime.TotalSeconds);
                                animatedSprite.position += (velocity * (float)gameTime.ElapsedGameTime.TotalSeconds);
                                IsScrolling = false;
                            }
                        }
                        else
                        {
                            position += (velocity * (float)gameTime.ElapsedGameTime.TotalSeconds);
                            animatedSprite.position += (velocity * (float)gameTime.ElapsedGameTime.TotalSeconds);
                            IsScrolling = false;
                        }

                        animatedSprite.WorldPos += (velocity * (float)gameTime.ElapsedGameTime.TotalSeconds);
                    }
                    else
                    {
                        Rectangle newRectangle = animatedSprite.Bounds;
                        newRectangle.X += Convert.ToInt32((velocity * (float)gameTime.ElapsedGameTime.TotalSeconds).X);

                        for (int l = 0; l < Enemies.Count; l++)
                        {

                            if (newRectangle.Intersects(Enemies[l].Bounds) && !Enemies[l].blnDie)
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
                                if (animatedSprite.WorldPos.X + (graphics.PreferredBackBufferWidth / 2) <= ((Columns - 10) * TileWidth) - 10)
                                {
                                    OffSet -= (velocity * (float)gameTime.ElapsedGameTime.TotalSeconds);
                                    if (GlobalVariables.MoveToLoc.X > 0 && GlobalVariables.MoveToLoc.Y > 0)
                                    {
                                        GlobalVariables.MoveToLoc -= (velocity * (float)gameTime.ElapsedGameTime.TotalSeconds);
                                    }
                                    IsScrolling = true;
                                }
                                else
                                {
                                    position += (velocity * (float)gameTime.ElapsedGameTime.TotalSeconds);
                                    animatedSprite.position += (velocity * (float)gameTime.ElapsedGameTime.TotalSeconds);
                                    IsScrolling = false;
                                }
                            }
                            else
                            {
                                position += (velocity * (float)gameTime.ElapsedGameTime.TotalSeconds);
                                animatedSprite.position += (velocity * (float)gameTime.ElapsedGameTime.TotalSeconds);
                                IsScrolling = false;
                            }

                            animatedSprite.WorldPos += (velocity * (float)gameTime.ElapsedGameTime.TotalSeconds);

                            //Check for Items
                            if (IsScrolling)
                            {
                                if (DroppedItems.Count > 0)
                                {
                                    if (theItem != null)
                                    {
                                        theItem.Bounds.X -= (int)(velocity * (float)gameTime.ElapsedGameTime.TotalSeconds).X;
                                    }
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
                                    enemy.Location -= (velocity * (float)gameTime.ElapsedGameTime.TotalSeconds);
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
            if (kState.IsKeyDown(Keys.Up) || kState.IsKeyDown(Keys.W) || GlobalVariables.CurrentDir == GlobalVariables.Dir.Up || GlobalVariables.CurrentDir == GlobalVariables.Dir.UpLeft || GlobalVariables.CurrentDir == GlobalVariables.Dir.UpRight)
            {
                Valid = true;
                if (position.Y > -10)
                {
                    if (Enemies.Count < 1)
                    {
                        if (kState.IsKeyDown(Keys.Right) || kState.IsKeyDown(Keys.D))
                        {
                            animatedSprite.direction = 3;
                            animatedSprite.UpdateRight();
                        }
                        else if (kState.IsKeyDown(Keys.Left) || kState.IsKeyDown(Keys.A))
                        {
                            animatedSprite.direction = 2;
                            animatedSprite.UpdateLeft();
                        }
                        else
                        {
                            animatedSprite.direction = 4;
                            animatedSprite.UpdateUp();
                        }

                        if (position.Y <= graphics.PreferredBackBufferHeight / 2)
                        {
                            if (animatedSprite.WorldPos.Y >= (graphics.PreferredBackBufferHeight / 2) + 10)
                            {
                                OffSet += (velocityup * (float)gameTime.ElapsedGameTime.TotalSeconds);
                                if (GlobalVariables.MoveToLoc.X > 0 && GlobalVariables.MoveToLoc.Y > 0)
                                {
                                    GlobalVariables.MoveToLoc += (velocityup * (float)gameTime.ElapsedGameTime.TotalSeconds);
                                }
                                IsScrolling = true;
                            }
                            else
                            {
                                position -= (velocityup * (float)gameTime.ElapsedGameTime.TotalSeconds);
                                animatedSprite.position -= (velocityup * (float)gameTime.ElapsedGameTime.TotalSeconds);
                                IsScrolling = false;
                            }
                        }
                        else
                        {
                            position -= (velocityup * (float)gameTime.ElapsedGameTime.TotalSeconds);
                            animatedSprite.position -= (velocityup * (float)gameTime.ElapsedGameTime.TotalSeconds);
                            IsScrolling = false;
                        }

                        animatedSprite.WorldPos -= (velocityup * (float)gameTime.ElapsedGameTime.TotalSeconds);
                    }
                    else
                    {
                        Rectangle newRectangle = animatedSprite.Bounds;
                        newRectangle.Y -= Convert.ToInt32((velocityup * (float)gameTime.ElapsedGameTime.TotalSeconds).Y);

                        for (int l = 0; l < Enemies.Count; l++)
                        {

                            if (newRectangle.Intersects(Enemies[l].Bounds) && !Enemies[l].blnDie)
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
                                    if (GlobalVariables.MoveToLoc.X > 0 && GlobalVariables.MoveToLoc.Y > 0)
                                    {
                                        GlobalVariables.MoveToLoc += (velocityup * (float)gameTime.ElapsedGameTime.TotalSeconds);
                                    }
                                    IsScrolling = true;
                                }
                                else
                                {
                                    position -= (velocityup * (float)gameTime.ElapsedGameTime.TotalSeconds);
                                    animatedSprite.position -= (velocityup * (float)gameTime.ElapsedGameTime.TotalSeconds);
                                    IsScrolling = false;
                                }
                            }
                            else
                            {
                                position -= (velocityup * (float)gameTime.ElapsedGameTime.TotalSeconds);
                                animatedSprite.position -= (velocityup * (float)gameTime.ElapsedGameTime.TotalSeconds);
                                IsScrolling = false;
                            }

                            animatedSprite.WorldPos -= (velocityup * (float)gameTime.ElapsedGameTime.TotalSeconds);

                            //Check for Items
                            if (DroppedItems.Count > 0)
                            {
                                if (IsScrolling)
                                {
                                    if (theItem != null)
                                    {
                                        theItem.Bounds.Y += (int)(velocityup * (float)gameTime.ElapsedGameTime.TotalSeconds).Y;
                                    }
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
                                    enemy.Location += (velocityup * (float)gameTime.ElapsedGameTime.TotalSeconds);
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
            if (kState.IsKeyDown(Keys.Down) || kState.IsKeyDown(Keys.S) || GlobalVariables.CurrentDir == GlobalVariables.Dir.Down || GlobalVariables.CurrentDir == GlobalVariables.Dir.DownLeft || GlobalVariables.CurrentDir == GlobalVariables.Dir.DownRight)
            {
                Valid = true;
                if (position.Y < graphics.PreferredBackBufferHeight - animatedSprite.Height)
                {
                    if (Enemies.Count < 1)
                    {
                        if (kState.IsKeyDown(Keys.Right) || kState.IsKeyDown(Keys.D))
                        {
                            animatedSprite.direction = 3;
                            animatedSprite.UpdateRight();
                        }
                        else if (kState.IsKeyDown(Keys.Left) || kState.IsKeyDown(Keys.A))
                        {
                            animatedSprite.direction = 2;
                            animatedSprite.UpdateLeft();
                        }
                        else
                        {
                            animatedSprite.direction = 1;
                            animatedSprite.UpdateDown();
                        }

                        if (position.Y >= graphics.PreferredBackBufferHeight / 2)
                        {
                            if (animatedSprite.WorldPos.Y + (graphics.PreferredBackBufferHeight / 2) <= ((Rows - 10) * TileHeight) - 10)
                            {
                                OffSet -= (velocityup * (float)gameTime.ElapsedGameTime.TotalSeconds);
                                if (GlobalVariables.MoveToLoc.X > 0 && GlobalVariables.MoveToLoc.Y > 0)
                                {
                                    GlobalVariables.MoveToLoc -= (velocityup * (float)gameTime.ElapsedGameTime.TotalSeconds);
                                }
                                IsScrolling = true;
                            }
                            else
                            {
                                position += (velocityup * (float)gameTime.ElapsedGameTime.TotalSeconds);
                                animatedSprite.position += (velocityup * (float)gameTime.ElapsedGameTime.TotalSeconds);
                                IsScrolling = false;
                            }
                        }
                        else
                        {
                            position += (velocityup * (float)gameTime.ElapsedGameTime.TotalSeconds);
                            animatedSprite.position += (velocityup * (float)gameTime.ElapsedGameTime.TotalSeconds);
                            IsScrolling = false;
                        }

                        animatedSprite.WorldPos += (velocityup * (float)gameTime.ElapsedGameTime.TotalSeconds);
                    }

                    else
                    {
                        Rectangle newRectangle = animatedSprite.Bounds;
                        newRectangle.Y += Convert.ToInt32((velocityup * (float)gameTime.ElapsedGameTime.TotalSeconds).Y);

                        for (int l = 0; l < Enemies.Count; l++)
                        {

                            if (newRectangle.Intersects(Enemies[l].Bounds) && !Enemies[l].blnDie)
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
                                if (animatedSprite.WorldPos.Y + (graphics.PreferredBackBufferHeight / 2) <= ((Rows - 10) * TileHeight) - 10)
                                {
                                    OffSet -= (velocityup * (float)gameTime.ElapsedGameTime.TotalSeconds);
                                    if (GlobalVariables.MoveToLoc.X > 0 && GlobalVariables.MoveToLoc.Y > 0)
                                    {
                                        GlobalVariables.MoveToLoc -= (velocityup * (float)gameTime.ElapsedGameTime.TotalSeconds);
                                    }
                                    IsScrolling = true;
                                }
                                else
                                {
                                    position += (velocityup * (float)gameTime.ElapsedGameTime.TotalSeconds);
                                    animatedSprite.position += (velocityup * (float)gameTime.ElapsedGameTime.TotalSeconds);
                                    IsScrolling = false;
                                }
                            }
                            else
                            {
                                position += (velocityup * (float)gameTime.ElapsedGameTime.TotalSeconds);
                                animatedSprite.position += (velocityup * (float)gameTime.ElapsedGameTime.TotalSeconds);
                                IsScrolling = false;
                            }

                            animatedSprite.WorldPos += (velocityup * (float)gameTime.ElapsedGameTime.TotalSeconds);

                            //Check for Items
                            if (DroppedItems.Count > 0)
                            {
                                if (IsScrolling)
                                {
                                    if (theItem != null)
                                    {
                                        theItem.Bounds.Y -= (int)(velocityup * (float)gameTime.ElapsedGameTime.TotalSeconds).Y;
                                    }
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
                                    enemy.Location -= (velocityup * (float)gameTime.ElapsedGameTime.TotalSeconds);
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
                int deadEnemyRarity = 0;
                for (int l = 0; l < Enemies.Count; l++)
                {
                    if (Enemies[l].hp <= 0)
                    {
                        //TODO : Add Death Animation
                        Enemies[l].blnDie = true;
                    }
                    if (Enemies[l].blnDead)
                    {
                        deadEnemyRarity = Enemies[l].GetRarity();
                        switch (deadEnemyRarity)
                        {
                            case 1:
                                IntDrop = RNG.Next(1 + (int)(GlobalVariables.CharacterMagicFindQuantity * .03), 1 + (int)(GlobalVariables.CharacterMagicFindQuantity * .03) + 100);
                                break;
                            case 2:
                                IntDrop = RNG.Next(1 + (int)(GlobalVariables.CharacterMagicFindQuantity * .04), 1 + (int)(GlobalVariables.CharacterMagicFindQuantity * .04) + 100);
                                break;
                            case 3:
                                IntDrop = RNG.Next(1 + (int)(GlobalVariables.CharacterMagicFindQuantity * .06), 1 + (int)(GlobalVariables.CharacterMagicFindQuantity * .06) + 100);
                                break;
                            case 4:
                                IntDrop = RNG.Next(1 + (int)(GlobalVariables.CharacterMagicFindQuantity * .15), 1 + (int)(GlobalVariables.CharacterMagicFindQuantity * .15) + 100);
                                break;
                            case 5:
                                IntDrop = RNG.Next(1 + (int)(GlobalVariables.CharacterMagicFindQuantity * 1), 1 + (int)(GlobalVariables.CharacterMagicFindQuantity * 1) + 100);
                                break;
                        }
                        if (IntDrop > 50)
                        {
                            DoesDrop = true;
                        }
                        else
                        {
                            DoesDrop = false;
                        }
                        if (DoesDrop)
                        {
                            int ItemType = GlobalVariables.RollVsItemType();
                            int SubType = 0;
                            int ItemLevel = Enemies[l].Level + RNG.Next(1, 3) - RNG.Next(1, 3);
                            string itemTextureName = "";
                            if (ItemLevel < 1)
                            {
                                ItemLevel = 1;
                            }
                            string basestatname = "";
                            int basestat = 0;
                            double baseatkspd = 0.0;
                            Texture2D DroppedItem = sword;
                            Item.itemSlot itmSlot = Item.itemSlot.Nothing;

                            ContentManager contentManager = new ContentManager(Content.ServiceProvider, Content.RootDirectory);
                            switch (ItemType)
                            {

                                //Weapon
                                case 1:

                                    SubType = GlobalVariables.RollVsWeaponType();

                                    switch (SubType)
                                    {
                                        //Two-Handed Sword
                                        case 1:

                                            itmSlot = Item.itemSlot.TwoHanded;
                                            int TypeOfTwoHander = GlobalVariables.RollVsTwoHanderType();

                                            switch (TypeOfTwoHander)
                                            {

                                                case 1:

                                                    DroppedItem = contentManager.Load<Texture2D>("Two-HandedSword");
                                                    baseatkspd = 1.10;
                                                    itemTextureName = "Two-HandedSword";
                                                    basestatname = "Physical Damage";
                                                    basestat = GlobalVariables.GetBaseStat(ItemType, SubType);
                                                    break;
                                            }

                                            break;
                                    }

                                    break;

                                //Boots
                                case 2:

                                    itmSlot = Item.itemSlot.Boots;
                                    SubType = GlobalVariables.RollVsBootType();

                                    switch (SubType)
                                    {

                                        case 1:

                                            DroppedItem = contentManager.Load<Texture2D>("Leatherboots");
                                            itemTextureName = "Leatherboots";
                                            basestatname = "Evasion";
                                            basestat = GlobalVariables.GetBaseStat(ItemType, SubType);
                                            break;

                                    }

                                    break;

                                //Pants
                                case 3:

                                    itmSlot = Item.itemSlot.Pants;
                                    SubType = GlobalVariables.RollVsPantType();

                                    switch (SubType)
                                    {

                                        case 1:

                                            DroppedItem = contentManager.Load<Texture2D>("LeatherPants");
                                            itemTextureName = "LeatherPants";
                                            basestatname = "Evasion";
                                            basestat = GlobalVariables.GetBaseStat(ItemType, SubType);
                                            break;
                                    }

                                    break;

                                //Chest
                                case 4:

                                    itmSlot = Item.itemSlot.Chest;
                                    SubType = GlobalVariables.RollVsChestType();

                                    switch (SubType)
                                    {

                                        case 1:

                                            DroppedItem = contentManager.Load<Texture2D>("LeatherChest");
                                            itemTextureName = "LeatherChest";
                                            basestatname = "Evasion";
                                            basestat = GlobalVariables.GetBaseStat(ItemType, SubType);
                                            break;
                                    }

                                    break;

                                //Gloves
                                case 5:

                                    itmSlot = Item.itemSlot.Gloves;
                                    SubType = GlobalVariables.RollVsGlovesType();

                                    switch (SubType)
                                    {

                                        case 1:

                                            DroppedItem = contentManager.Load<Texture2D>("Leathergloves");
                                            itemTextureName = "Leathergloves";
                                            basestatname = "Evasion";
                                            basestat = GlobalVariables.GetBaseStat(ItemType, SubType);
                                            break;
                                    }

                                    break;

                                //Ring
                                case 6:

                                    itmSlot = Item.itemSlot.Ring;
                                    SubType = GlobalVariables.RollVsRingType();

                                    switch (SubType)
                                    {

                                        case 1:

                                            DroppedItem = contentManager.Load<Texture2D>("Ring1");
                                            itemTextureName = "Ring1";
                                            basestatname = "";
                                            basestat = 0;
                                            break;
                                    }

                                    break;

                                //Belt
                                case 7:

                                    itmSlot = Item.itemSlot.Belt;
                                    SubType = GlobalVariables.RollVsBeltType();

                                    switch (SubType)
                                    {

                                        case 1:

                                            DroppedItem = contentManager.Load<Texture2D>("Leatherbelt");
                                            itemTextureName = "Leatherbelt";
                                            basestatname = "";
                                            basestat = 0;
                                            break;
                                    }

                                    break;

                                //Helmt
                                case 8:

                                    itmSlot = Item.itemSlot.Helmet;
                                    SubType = GlobalVariables.RollVsHelmType();

                                    switch (SubType)
                                    {

                                        case 1:

                                            DroppedItem = contentManager.Load<Texture2D>("LeatherHelmet");
                                            itemTextureName = "LeatherHelmet";
                                            basestatname = "Evasion";
                                            basestat = 25;
                                            break;
                                    }

                                    break;

                                //Shoudlers
                                case 9:

                                    itmSlot = Item.itemSlot.Shoulders;
                                    SubType = GlobalVariables.RollVsShoulderType();

                                    switch (SubType)
                                    {

                                        case 1:

                                            DroppedItem = contentManager.Load<Texture2D>("LeatherPaulders");
                                            itemTextureName = "LeatherPaulders";
                                            basestatname = "Evasion";
                                            basestat = 35;
                                            break;
                                    }

                                    break;

                            }
                            DroppedItems.Add(new Item(Enemies[l].Location, DroppedItem, ItemType, ItemLevel, itmSlot, itemTextureName, basestat, basestatname, SubType, false, baseatkspd, deadEnemyRarity));
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
            #endregion

            #region ClickEvents

            #region Attack

            //Logic to Check if a Character has clicked on an enemy
            if (mouseState.LeftButton == ButtonState.Pressed && !LeftMouseHeld)
            {
                foreach (Enemy en in Enemies)
                {
                    if (en.Bounds.Intersects(new Rectangle(mouseState.X, mouseState.Y, 5, 5)))
                    {
                        en.CharacterAttacked = true;
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
                    theItem = DroppedItems[lc];
                    break;
                }
            }

            if (theItem != null)
            {
                if (animatedSprite.Bounds.Intersects(theItem.Bounds))
                {
                    if (inventory.Count() == 20)
                    {
                        MSG.tmrDSP = 0f;
                        blnDSP = true;
                    }
                    else
                    {
                        inventory.Add(theItem);
                        DroppedItems.Remove(theItem);
                        theItem = null;
                    }
                }
            }

            //Logic to see if any enemies have Bln set to trigger character animation
            foreach (Enemy en in Enemies)
            {
                if (en.CharacterAttacked && !PathFinding && !en.blnDie)
                {
                    PathFinding = GlobalVariables.MoveCharacterToPosition(animatedSprite, this, en.Location, velocity, velocityup, Enemies);

                    if (PathFinding)
                    {
                        animatedSprite.AnimateAttack(en.Bounds);

                        double damage = 0;
                        bool IsHit = GlobalVariables.RollVsHit(en.Level, en.Evasion);
                        bool IsCrit = false;
                        if (IsHit)
                        {
                            IsCrit = GlobalVariables.RollVsCrit();
                        }
                        if (IsHit)
                        {
                            if (IsCrit)
                            {
                                damage = (GlobalVariables.RollPhysicalDamage() * (1 + (GlobalVariables.CharacterCritDamageModifier * .01)));
                            }
                            else
                            {
                                damage = GlobalVariables.RollPhysicalDamage();
                            }
                            //Enemy Damage reduction
                            double enReduction = en.Armour / (double)(en.Armour + 400);
                            double damageReduced = damage * enReduction;
                            damage -= damageReduced;
                        }
                        if (IsHit)
                        {
                            en.hp -= Convert.ToInt32(damage);
                            if (IsCrit)
                            {
                                en.DamageCounterList.Add(Convert.ToString(Convert.ToUInt32(damage)) + "Crit");
                            }
                            else
                            {
                                en.DamageCounterList.Add(Convert.ToString(Convert.ToUInt32(damage)));
                            }
                        }
                        else
                        {
                            en.DamageCounterList.Add("Miss");
                        }
                        en.m.Add(2);
                    }
                }
            }

            //Condition where the pathfinding should not pathfind:P
            Rectangle rect = new Rectangle(mouseState.X, mouseState.Y, 1, 1);

            if (blnOpen)
            {
                if (rect.Intersects(inventory.Bounds) && mouseState.LeftButton == ButtonState.Pressed || blnClamp && mouseState.LeftButton == ButtonState.Pressed || mouseState.X < 150 && mouseState.Y < 50 && mouseState.LeftButton == ButtonState.Pressed)
                {
                    blnIgnore = true;
                }
                else if (blnEquip)
                {
                    if (rect.Intersects(equipment.Bounds) && mouseState.LeftButton == ButtonState.Pressed)
                    {
                        blnIgnore = true;
                    }
                    else
                    {
                        blnIgnore = false;
                    }
                }
                else
                {
                    blnIgnore = false;
                }
            }
            else
            {
                if (blnClamp && mouseState.LeftButton == ButtonState.Pressed || mouseState.X < 150 && mouseState.Y < 50 && mouseState.LeftButton == ButtonState.Pressed)
                {
                    blnIgnore = true;
                }
                else if (blnEquip)
                {
                    if (rect.Intersects(equipment.Bounds) && mouseState.LeftButton == ButtonState.Pressed)
                    {
                        blnIgnore = true;
                    }
                    else
                    {
                        blnIgnore = false;
                    }
                }
                else
                {
                    blnIgnore = false;
                }
            }

            //Pick up Item Drop Item
            if (rect.Intersects(inventory.Bounds) && mouseState.LeftButton == ButtonState.Pressed && MoldState.LeftButton == ButtonState.Released)
            {
                if (blnClamp)
                {
                    inventory.ItemDropped(new Vector2(mouseState.X, mouseState.Y), inventory.ClampedItem);
                }
                else
                {
                    inventory.GrabItem();
                }
            }
            else if (rect.Intersects(equipment.HelmBounds) && mouseState.LeftButton == ButtonState.Pressed && MoldState.LeftButton == ButtonState.Released && blnEquip)
            {
                if (blnClamp)
                {
                    if (inventory.ClampedItem.ItemSlot == Item.itemSlot.Helmet)
                    {
                        inventory.ClampedItem.location.X = equipment.HelmBounds.X;
                        inventory.ClampedItem.location.Y = equipment.HelmBounds.Y;
                        Item tempitem = equipment.Helmet;
                        equipment.Helmet = inventory.ClampedItem;
                        GlobalVariables.HelmetEquipped(inventory.ClampedItem);
                        if (tempitem != null)
                        {
                            inventory.ClampedItem = tempitem;
                        }
                        else
                        {
                            inventory.ClampedItem = null;
                            blnClamp = false;
                        }
                    }
                }
                else
                {
                    if (equipment.Helmet != null)
                    {
                        inventory.ClampedItem = equipment.Helmet;
                        equipment.Hero.txtHero = Content.Load<Texture2D>("HeroSS2H");
                        equipment.Helmet = null;
                        blnClamp = true;
                        animatedSprite.CharHelm = null;
                        animatedSprite.Texture = Content.Load<Texture2D>("HeroSS2H");
                        GlobalVariables.UpdateStats();
                    }
                }
            }
            else if (rect.Intersects(equipment.ChestBounds) && mouseState.LeftButton == ButtonState.Pressed && MoldState.LeftButton == ButtonState.Released && blnEquip)
            {
                if (blnClamp)
                {
                    if (inventory.ClampedItem.ItemSlot == Item.itemSlot.Chest)
                    {
                        inventory.ClampedItem.location.X = equipment.ChestBounds.X;
                        inventory.ClampedItem.location.Y = equipment.ChestBounds.Y;
                        Item tempitem = equipment.Chest;
                        equipment.Chest = inventory.ClampedItem;
                        GlobalVariables.ChestEquipped(inventory.ClampedItem);
                        if (tempitem != null)
                        {
                            inventory.ClampedItem = tempitem;
                        }
                        else
                        {
                            inventory.ClampedItem = null;
                            blnClamp = false;
                        }
                    }
                }
                else
                {
                    if (equipment.Chest != null)
                    {
                        inventory.ClampedItem = equipment.Chest;
                        equipment.Chest = null;
                        blnClamp = true;
                        animatedSprite.CharChest = null;
                        GlobalVariables.UpdateStats();
                    }
                }
            }
            else if (rect.Intersects(equipment.RWeapbounds) && mouseState.LeftButton == ButtonState.Pressed && MoldState.LeftButton == ButtonState.Released && blnEquip)
            {
                if (blnClamp)
                {
                    if (inventory.ClampedItem.ItemSlot == Item.itemSlot.OneHanded || inventory.ClampedItem.ItemSlot == Item.itemSlot.TwoHanded)
                    {
                        inventory.ClampedItem.location.X = equipment.RWeapbounds.X;
                        inventory.ClampedItem.location.Y = equipment.RWeapbounds.Y;
                        Item tempitem = equipment.RightWeapon;
                        equipment.RightWeapon = inventory.ClampedItem;
                        GlobalVariables.WeaponEquiped(inventory.ClampedItem, 2);
                        if (tempitem != null)
                        {
                            inventory.ClampedItem = tempitem;
                        }
                        else
                        {
                            inventory.ClampedItem = null;
                            blnClamp = false;
                        }
                    }
                }
                else
                {
                    if (equipment.RightWeapon != null)
                    {
                        inventory.ClampedItem = equipment.RightWeapon;
                        GlobalVariables.WeaponEquiped(inventory.ClampedItem, 2);
                        equipment.RightWeapon = null;
                        blnClamp = true;
                        animatedSprite.CharWeapon = null;
                        GlobalVariables.UpdateStats();
                    }
                }
            }
            else if (rect.Intersects(equipment.LWeapBounds) && mouseState.LeftButton == ButtonState.Pressed && MoldState.LeftButton == ButtonState.Released && blnEquip)
            {
                if (blnClamp)
                {
                    if (inventory.ClampedItem.ItemSlot == Item.itemSlot.OneHanded || inventory.ClampedItem.ItemSlot == Item.itemSlot.TwoHanded)
                    {
                        inventory.ClampedItem.location.X = equipment.LWeapBounds.X;
                        inventory.ClampedItem.location.Y = equipment.LWeapBounds.Y;
                        Item tempitem = equipment.LeftWeapon;
                        equipment.LeftWeapon = inventory.ClampedItem;
                        GlobalVariables.WeaponEquiped(inventory.ClampedItem, 1);
                        if (tempitem != null)
                        {
                            inventory.ClampedItem = tempitem;
                        }
                        else
                        {
                            inventory.ClampedItem = null;
                            blnClamp = false;
                        }
                    }
                }
                else
                {
                    if (equipment.LeftWeapon != null)
                    {
                        inventory.ClampedItem = equipment.LeftWeapon;
                        GlobalVariables.WeaponEquiped(inventory.ClampedItem, 1);
                        equipment.LeftWeapon = null;
                        blnClamp = true;
                        animatedSprite.CharWeapon = null;
                        GlobalVariables.UpdateStats();
                    }
                }
            }
            else if (rect.Intersects(equipment.GlovesBounds) && mouseState.LeftButton == ButtonState.Pressed && MoldState.LeftButton == ButtonState.Released && blnEquip)
            {
                if (blnClamp)
                {
                    if (inventory.ClampedItem.ItemSlot == Item.itemSlot.Gloves)
                    {
                        inventory.ClampedItem.location.X = equipment.GlovesBounds.X;
                        inventory.ClampedItem.location.Y = equipment.GlovesBounds.Y;
                        Item tempitem = equipment.Gloves;
                        equipment.Gloves = inventory.ClampedItem;
                        GlobalVariables.GlovesEquipped(inventory.ClampedItem);
                        if (tempitem != null)
                        {
                            inventory.ClampedItem = tempitem;
                        }
                        else
                        {
                            inventory.ClampedItem = null;
                            blnClamp = false;
                        }
                    }
                }
                else
                {
                    if (equipment.Gloves != null)
                    {
                        inventory.ClampedItem = equipment.Gloves;
                        equipment.Gloves = null;
                        blnClamp = true;
                        animatedSprite.CharGloves = null;
                        GlobalVariables.UpdateStats();
                    }
                }
            }
            else if (rect.Intersects(equipment.ShouldersBounds) && mouseState.LeftButton == ButtonState.Pressed && MoldState.LeftButton == ButtonState.Released && blnEquip)
            {
                if (blnClamp)
                {
                    if (inventory.ClampedItem.ItemSlot == Item.itemSlot.Shoulders)
                    {
                        inventory.ClampedItem.location.X = equipment.ShouldersBounds.X;
                        inventory.ClampedItem.location.Y = equipment.ShouldersBounds.Y;
                        Item tempitem = equipment.Shoulders;
                        equipment.Shoulders = inventory.ClampedItem;
                        GlobalVariables.ShouldersEquipped(inventory.ClampedItem);
                        if (tempitem != null)
                        {
                            inventory.ClampedItem = tempitem;
                        }
                        else
                        {
                            inventory.ClampedItem = null;
                            blnClamp = false;
                        }
                    }
                }
                else
                {
                    if (equipment.Shoulders != null)
                    {
                        inventory.ClampedItem = equipment.Shoulders;
                        equipment.Shoulders = null;
                        blnClamp = true;
                        animatedSprite.CharShoulders = null;
                        GlobalVariables.UpdateStats();
                    }
                }
            }
            else if (rect.Intersects(equipment.PantsBounds) && mouseState.LeftButton == ButtonState.Pressed && MoldState.LeftButton == ButtonState.Released && blnEquip)
            {
                if (blnClamp)
                {
                    if (inventory.ClampedItem.ItemSlot == Item.itemSlot.Pants)
                    {
                        inventory.ClampedItem.location.X = equipment.PantsBounds.X;
                        inventory.ClampedItem.location.Y = equipment.PantsBounds.Y;
                        Item tempitem = equipment.Pants;
                        equipment.Pants = inventory.ClampedItem;
                        GlobalVariables.PantsEquipped(inventory.ClampedItem);
                        if (tempitem != null)
                        {
                            inventory.ClampedItem = tempitem;
                        }
                        else
                        {
                            inventory.ClampedItem = null;
                            blnClamp = false;
                        }
                    }
                }
                else
                {
                    if (equipment.Pants != null)
                    {
                        inventory.ClampedItem = equipment.Pants;
                        equipment.Pants = null;
                        blnClamp = true;
                        animatedSprite.CharPants = null;
                        GlobalVariables.UpdateStats();
                    }
                }
            }
            else if (rect.Intersects(equipment.BootsBounds) && mouseState.LeftButton == ButtonState.Pressed && MoldState.LeftButton == ButtonState.Released && blnEquip)
            {
                if (blnClamp)
                {
                    if (inventory.ClampedItem.ItemSlot == Item.itemSlot.Boots)
                    {
                        inventory.ClampedItem.location.X = equipment.BootsBounds.X;
                        inventory.ClampedItem.location.Y = equipment.BootsBounds.Y;
                        Item tempitem = equipment.Boots;
                        equipment.Boots = inventory.ClampedItem;
                        GlobalVariables.BootsEquipped(inventory.ClampedItem);
                        if (tempitem != null)
                        {
                            inventory.ClampedItem = tempitem;
                        }
                        else
                        {
                            inventory.ClampedItem = null;
                            blnClamp = false;
                        }
                    }
                }
                else
                {
                    if (equipment.Boots != null)
                    {
                        inventory.ClampedItem = equipment.Boots;
                        GlobalVariables.BootsEquipped(inventory.ClampedItem);
                        equipment.Boots = null;
                        blnClamp = true;
                        animatedSprite.CharBoots = null;
                        GlobalVariables.UpdateStats();
                    }
                }
            }
            else if (rect.Intersects(equipment.RightRingBounds) && mouseState.LeftButton == ButtonState.Pressed && MoldState.LeftButton == ButtonState.Released && blnEquip)
            {
                if (blnClamp)
                {
                    if (inventory.ClampedItem.ItemSlot == Item.itemSlot.Ring)
                    {
                        inventory.ClampedItem.location.X = equipment.RightRingBounds.X;
                        inventory.ClampedItem.location.Y = equipment.RightRingBounds.Y;
                        Item tempitem = equipment.RightRing;
                        equipment.RightRing = inventory.ClampedItem;
                        GlobalVariables.RingEquipped(inventory.ClampedItem);
                        if (tempitem != null)
                        {
                            inventory.ClampedItem = tempitem;
                        }
                        else
                        {
                            inventory.ClampedItem = null;
                            blnClamp = false;
                        }
                    }
                }
                else
                {
                    if (equipment.RightRing != null)
                    {
                        inventory.ClampedItem = equipment.RightRing;
                        equipment.RightRing = null;
                        blnClamp = true;
                        GlobalVariables.RingEquipped(inventory.ClampedItem);
                    }
                }
            }
            else if (rect.Intersects(equipment.LeftRingBounds) && mouseState.LeftButton == ButtonState.Pressed && MoldState.LeftButton == ButtonState.Released && blnEquip)
            {
                if (blnClamp)
                {
                    if (inventory.ClampedItem.ItemSlot == Item.itemSlot.Ring)
                    {
                        inventory.ClampedItem.location.X = equipment.LeftRingBounds.X;
                        inventory.ClampedItem.location.Y = equipment.LeftRingBounds.Y;
                        Item tempitem = equipment.LeftRing;
                        equipment.LeftRing = inventory.ClampedItem;
                        GlobalVariables.RingEquipped(inventory.ClampedItem);
                        if (tempitem != null)
                        {
                            inventory.ClampedItem = tempitem;
                        }
                        else
                        {
                            inventory.ClampedItem = null;
                            blnClamp = false;
                        }
                    }
                }
                else
                {
                    if (equipment.LeftRing != null)
                    {
                        inventory.ClampedItem = equipment.LeftRing;
                        equipment.LeftRing = null;
                        blnClamp = true;
                        GlobalVariables.RingEquipped(inventory.ClampedItem);
                    }
                }
            }
            else if (rect.Intersects(equipment.BeltBounds) && mouseState.LeftButton == ButtonState.Pressed && MoldState.LeftButton == ButtonState.Released && blnEquip)
            {
                if (blnClamp)
                {
                    if (inventory.ClampedItem.ItemSlot == Item.itemSlot.Belt)
                    {
                        inventory.ClampedItem.location.X = equipment.BeltBounds.X;
                        inventory.ClampedItem.location.Y = equipment.BeltBounds.Y;
                        Item tempitem = equipment.Belt;
                        equipment.Belt = inventory.ClampedItem;
                        GlobalVariables.BeltEquipped(inventory.ClampedItem);
                        if (tempitem != null)
                        {
                            inventory.ClampedItem = tempitem;
                        }
                        else
                        {
                            inventory.ClampedItem = null;
                            blnClamp = false;
                        }
                    }
                }
                else
                {
                    if (equipment.Belt != null)
                    {
                        inventory.ClampedItem = equipment.Belt;
                        equipment.Belt = null;
                        blnClamp = true;
                        GlobalVariables.BeltEquipped(inventory.ClampedItem);
                    }
                }
            }
            else if (blnClamp && mouseState.LeftButton == ButtonState.Pressed && MoldState.LeftButton == ButtonState.Released)
            {
                Item tempItem = inventory.ClampedItem;
                tempItem.location = position;
                tempItem.Bounds = new Rectangle((int)tempItem.location.X, (int)tempItem.location.Y, tempItem.ItemTexture.Width, tempItem.ItemTexture.Height);
                DroppedItems.Add(tempItem);
                blnClamp = false;
                inventory.ClampedItem = null;
            }

            inventory.Update(InvPos);
            equipment.Update();
            GlobalVariables.Equipment = equipment;

            if (!isDrag && mouseState.LeftButton == ButtonState.Pressed && MoldState.LeftButton == ButtonState.Released && PathFinding)
            {
                foreach (Enemy en in Enemies)
                {
                    en.CharacterAttacked = false;
                }
                PathFinding = false;
            }

            if (blnIgnore)
            {

                GlobalVariables.MoveToLoc = new Vector2(0, 0);
                GlobalVariables.CurrentDir = GlobalVariables.Dir.Nothing;
                PathFinding = true;

            }

            else if (!isDrag && mouseState.LeftButton == ButtonState.Pressed && !PathFinding)
            {
                GlobalVariables.MoveToLoc = new Vector2(0, 0);
                PathFinding = GlobalVariables.MoveCharacterToPosition(animatedSprite, this, new Vector2(mouseState.X, mouseState.Y), velocity, velocityup, Enemies);

            }

            if (!PathFinding && !isDrag)
            {
                PathFinding = GlobalVariables.MoveCharacterToPosition(animatedSprite, this, new Vector2(mouseState.X, mouseState.Y), velocity, velocityup, Enemies);
            }

            #endregion

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

            spriteBatch.Begin();

            if (blnLogTime && DrawTimes.Count < DebugCycles)
            {
                DebugTimer = new Stopwatch();
                DebugTimer.Start();
            }

            GraphicsDevice.Clear(Color.CornflowerBlue);

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



            //Inventory Selector

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

            if (blnOpen)
            {
                if (blnLogTime && DrawTimes_Inventory.Count < DebugCycles)
                {
                    DebugTimer1 = new Stopwatch();
                    DebugTimer1.Start();
                }

                spriteBatch.Draw(InvText, new Rectangle((int)InvPos.X, (int)InvPos.Y, 250, 300), Color.White);

                if (inventory.Items.Count > 0)
                {
                    inventory.Draw(spriteBatch);
                }

                if (blnLogTime && DrawTimes_Inventory.Count < DebugCycles && DebugTimer1 != null)
                {
                    DebugTimer1.Stop();
                    DrawTimes_Inventory.Add(DebugTimer1.ElapsedTicks);
                    DebugTimer1.Reset();
                }

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
            spriteBatch.DrawString(GlobalVariables.Font10, "Inventory", new Vector2(10, -2), Color.Beige);
            spriteBatch.DrawString(GlobalVariables.Font10, "Equipment", new Vector2(80, -2), Color.Beige);

            if (blnLogTime && DrawTimes_Other.Count < DebugCycles && DebugTimer1 != null)
            {
                DebugTimer1.Stop();
                DrawTimes_Other.Add(DebugTimer1.ElapsedTicks);
                DebugTimer1.Reset();
            }

            //Wait To Draw

            foreach (DrawItem draw in GlobalVariables.ItemsToBeDrawn)
            {
                switch (draw.CaseOfDraw)
                {
                    case 0:
                        //Texture
                        spriteBatch.Draw(draw.theTexture, draw.theLocation, draw.theRectangle, draw.theColor);
                        break;
                    case 1:
                        //String
                        spriteBatch.DrawString(draw.theFont, draw.theText, draw.theLocation, draw.theColor);
                        break;
                }
            }
            GlobalVariables.ItemsToBeDrawn = new List<DrawItem>();
            if (blnLogTime && DrawTimes_PauseMenu.Count < DebugCycles)
            {
                DebugTimer1 = new Stopwatch();
                DebugTimer1.Start();
            }

            //Draw ClampedItem
            if (blnClamp)
            {
                inventory.ClampedItem.Draw(spriteBatch, new Vector2(mouseState.X - (inventory.ClampedItem.ItemTexture.Width / 2), mouseState.Y - (inventory.ClampedItem.ItemTexture.Height / 2)), true);
            }

            //Pause Menu

            if (blnPaused)
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

            spriteBatch.End();

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
            blnPaused = false;
        }

        public void ExitGame(object sender, EventArgs eventArgs)
        {
            GlobalVariables.SaveGameData();
            GlobalVariables.SaveUserSettings();
            this.Exit();
        }

        public void DrawLogClicked(object sender, EventArgs eventArgs)
        {
            blnDrawDebuggerClicked = true;
            blnLogTime = false;
        }
    }
}
