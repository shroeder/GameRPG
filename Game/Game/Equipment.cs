using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Starbound.RealmFactoryCore;
using System.Timers;

namespace TextureAtlas
{
    public class Equipment
    {   
        //Variable
        public SpriteFont font1;
        public List<Item> Items = new List<Item>();
        public Texture2D CharBG;

        public Rectangle Bounds = new Rectangle((int)(GlobalVariables.gfx.PreferredBackBufferWidth * .3), (int)(GlobalVariables.gfx.PreferredBackBufferHeight * .1), (int)(GlobalVariables.gfx.PreferredBackBufferWidth * .6), (int)(GlobalVariables.gfx.PreferredBackBufferHeight * .6));

        public Rectangle HelmBounds;
        public Rectangle BootsBounds;
        public Rectangle BeltBounds;
        public Rectangle ChestBounds;
        public Rectangle BackBounds;
        public Rectangle GlovesBounds;
        public Rectangle RWeapbounds;
        public Rectangle LWeapBounds;
        public Rectangle ShouldersBounds;
        public Rectangle LeftRingBounds;
        public Rectangle RightRingBounds;
        public Rectangle StatsBounds;
        public Rectangle SkillsBounds;
        public Rectangle PassivesBounds;
        public Rectangle UnlocksBounds;

        public Item Helmet;
        public Item Shoulders;
        public Item Chest;
        public Item Back;
        public Item RightWeapon;
        public Item LeftWeapon;
        public Item Gloves;
        public Item Boots;
        public Item Belt;
        public Item LeftRing;
        public Item RightRing;

        public MouseState ms;

        public HeroDisplay Hero;

        public Equipment(Equipment equip)
        {
            if (equip != null)
            {
                Helmet = equip.Helmet;
                Shoulders = equip.Shoulders;
                Chest = equip.Chest;
                Back = equip.Chest;
                RightWeapon = equip.RightWeapon;
                LeftWeapon = equip.LeftWeapon;
                Gloves = equip.Gloves;
                Boots = equip.Boots;
                Belt = equip.Belt;
                LeftRing = equip.LeftRing;
                RightRing = equip.RightRing;
            }
            Hero = new HeroDisplay(this);
            Hero.Direction = 0;
            Hero.DrawLocation = new Rectangle((int)(GlobalVariables.gfx.PreferredBackBufferWidth * .395), (int)(GlobalVariables.gfx.PreferredBackBufferHeight * .25), (int)(GlobalVariables.gfx.PreferredBackBufferWidth * .14), (int)(GlobalVariables.gfx.PreferredBackBufferHeight * .3675));
            Hero.RotateClock += RotateClock;
            Hero.RotateCounter += RotateCounter;
        }

        public void Update()
        {
            ms = Mouse.GetState();

            Hero.Update();

            int width = (int)(GlobalVariables.gfx.PreferredBackBufferWidth * .0628);
            int height = (int)(GlobalVariables.gfx.PreferredBackBufferHeight * .0971);

            HelmBounds = new Rectangle((int)(GlobalVariables.gfx.PreferredBackBufferWidth * .323425), (int)(GlobalVariables.gfx.PreferredBackBufferHeight * .25), width,height);
            ChestBounds = new Rectangle((int)(GlobalVariables.gfx.PreferredBackBufferWidth * .323425), (int)(GlobalVariables.gfx.PreferredBackBufferHeight * .3525), width, height);
            RWeapbounds = new Rectangle((int)(GlobalVariables.gfx.PreferredBackBufferWidth * .323425), (int)(GlobalVariables.gfx.PreferredBackBufferHeight * .455), width, height);
            GlovesBounds = new Rectangle((int)(GlobalVariables.gfx.PreferredBackBufferWidth * .323425), (int)(GlobalVariables.gfx.PreferredBackBufferHeight * .5565), width, height);

            ShouldersBounds = new Rectangle((int)(GlobalVariables.gfx.PreferredBackBufferWidth * .544), (int)(GlobalVariables.gfx.PreferredBackBufferHeight * .25), width, height);
            BackBounds = new Rectangle((int)(GlobalVariables.gfx.PreferredBackBufferWidth * .544), (int)(GlobalVariables.gfx.PreferredBackBufferHeight * .3525), width, height);
            LWeapBounds = new Rectangle((int)(GlobalVariables.gfx.PreferredBackBufferWidth * .544), (int)(GlobalVariables.gfx.PreferredBackBufferHeight * .455), width, height);
            BootsBounds = new Rectangle((int)(GlobalVariables.gfx.PreferredBackBufferWidth * .544), (int)(GlobalVariables.gfx.PreferredBackBufferHeight * .5565), width, height);

            int beltwidth = (int)(GlobalVariables.gfx.PreferredBackBufferWidth * .0954);
            int beltheight = (int)(GlobalVariables.gfx.PreferredBackBufferHeight * .032);

            BeltBounds = new Rectangle((int)(GlobalVariables.gfx.PreferredBackBufferWidth * .4175), (int)(GlobalVariables.gfx.PreferredBackBufferHeight * .6215), beltwidth, beltheight);

            int ringwidth = (int)(GlobalVariables.gfx.PreferredBackBufferWidth * .02);
            int ringheight = (int)(GlobalVariables.gfx.PreferredBackBufferHeight * .032);

            LeftRingBounds = new Rectangle((int)(GlobalVariables.gfx.PreferredBackBufferWidth * .3945), (int)(GlobalVariables.gfx.PreferredBackBufferHeight * .6215), ringwidth, ringheight);
            RightRingBounds = new Rectangle((int)(GlobalVariables.gfx.PreferredBackBufferWidth * .515), (int)(GlobalVariables.gfx.PreferredBackBufferHeight * .6215), ringwidth, ringheight);

            Rectangle rect = new Rectangle(ms.X,ms.Y,1,1);

            //Set Hover

            if (Helmet != null)
            {
                if (rect.Intersects(HelmBounds))
                {
                    Helmet.hover = true;
                }
                else
                {
                    Helmet.hover = false;
                }
            }
            if (Chest != null)
            {
                if (rect.Intersects(ChestBounds))
                {
                    Chest.hover = true;
                }
                else
                {
                    Chest.hover = false;
                }
            }
            if (RightWeapon != null)
            {
                if (rect.Intersects(RWeapbounds))
                {
                    RightWeapon.hover = true;
                }
                else
                {
                    RightWeapon.hover = false;
                }
            }
            if (Gloves != null)
            {
                if (rect.Intersects(GlovesBounds))
                {
                    Gloves.hover = true;
                }
                else
                {
                    Gloves.hover = false;
                }
            }
            if (Shoulders != null)
            {
                if (rect.Intersects(ShouldersBounds))
                {
                    Shoulders.hover = true;
                }
                else
                {
                    Shoulders.hover = false;
                }
            }
            if (Back != null)
            {
                if (rect.Intersects(BackBounds))
                {
                    Back.hover = true;
                }
                else
                {
                    Back.hover = false;
                }
            }
            if (LeftWeapon != null)
            {
                if (rect.Intersects(LWeapBounds))
                {
                    LeftWeapon.hover = true;
                }
                else
                {
                    LeftWeapon.hover = false;
                }
            }
            if (Boots != null)
            {
                if (rect.Intersects(BootsBounds))
                {
                    Boots.hover = true;
                }
                else
                {
                    Boots.hover = false;
                }
            }
            if (LeftRing != null)
            {
                if (rect.Intersects(LeftRingBounds))
                {
                    LeftRing.hover = true;
                }
                else
                {
                    LeftRing.hover = false;
                }
            }
            if (Belt != null)
            {
                if (rect.Intersects(BeltBounds))
                {
                    Belt.hover = true;
                }
                else
                {
                    Belt.hover = false;
                }
            }
            if (RightRing != null)
            {
                if (rect.Intersects(RightRingBounds))
                {
                    RightRing.hover = true;
                }
                else
                {
                    RightRing.hover = false;
                }
            }
        }

        public void DrawHover(SpriteBatch spriteBatch, Item item)
        {
            //Draw Shaded backgorund
            GlobalVariables.WaitToDraw(0, item.location, new Rectangle(0, 0, item.TextureBack.Width, 70 + (int)(22 * item.affixes)), Color.Black, null, item.TextureBack);

            GlobalVariables.WaitToDraw(1, new Vector2((item.location.X + 100), (item.location.Y + 30)), new Rectangle(0, 0, 0, 0), item.RarityColor, item.Font1, null, item.ItemName);

            //Initialize Scalar Value
            int ScalarText = 20;


            //Draw Item aFfixes
            for (int intlc = 0; intlc < item.AffixList.Count; intlc++)
            {
                if (intlc < 4)
                {
                    GlobalVariables.WaitToDraw(1, new Vector2((item.location.X + 100), (item.location.Y + ScalarText + 50)), new Rectangle(0, 0, 0, 0), Color.White, item.Font1, null, item.AffixList[intlc].Desc);
                    ScalarText += 20;
                }
                if (intlc == 4)
                {
                    GlobalVariables.WaitToDraw(1, new Vector2((item.location.X + 100), (item.location.Y + ScalarText + 50)), new Rectangle(0, 0, 0, 0), Color.Orange, item.Font1, null, item.AffixList[intlc].Desc);
                    ScalarText += 20;
                }
                if (intlc == 5)
                {
                    GlobalVariables.WaitToDraw(1, new Vector2((item.location.X + 100), (item.location.Y + ScalarText + 50)), new Rectangle(0, 0, 0, 0), Color.Purple, item.Font1, null, item.AffixList[intlc].Desc);
                    ScalarText += 20;
                }
                if (intlc > 5)
                {
                    GlobalVariables.WaitToDraw(1, new Vector2((item.location.X + 100), (item.location.Y + ScalarText + 50)), new Rectangle(0, 0, 0, 0), Color.Brown, item.Font1, null, item.AffixList[intlc].Desc);
                    ScalarText += 20;
                }
            }
        }

        public void draw(SpriteBatch spriteBatach, Texture2D EquipSheet, SpriteFont font)
        {

            font1 = font;
            CharBG = EquipSheet;

            spriteBatach.Draw(CharBG, Bounds, Color.White);

            if (Helmet != null)
            {
                spriteBatach.Draw(Helmet.ItemTexture, HelmBounds, Helmet.RarityColor);
                if (Helmet.hover)
                {
                    DrawHover(spriteBatach, Helmet);
                }
            }

            if (Chest != null)
            {
                spriteBatach.Draw(Chest.ItemTexture, ChestBounds, Chest.RarityColor);
                if (Chest.hover)
                {
                    DrawHover(spriteBatach, Chest);
                }
            }

            if (RightWeapon != null)
            {
                spriteBatach.Draw(RightWeapon.ItemTexture, RWeapbounds,RightWeapon.RarityColor);
                if (RightWeapon.hover)
                {
                    DrawHover(spriteBatach, RightWeapon);
                }
            }

            if (Gloves != null)
            {
                spriteBatach.Draw(Gloves.ItemTexture, GlovesBounds, Gloves.RarityColor);
                if (Gloves.hover)
                {
                    DrawHover(spriteBatach, Gloves);
                }
            }

            if (Shoulders != null)
            {
                spriteBatach.Draw(Shoulders.ItemTexture, ShouldersBounds, Shoulders.RarityColor);
                if (Shoulders.hover)
                {
                    DrawHover(spriteBatach, Shoulders);
                }
            }

            if (Back != null)
            {
                spriteBatach.Draw(Back.ItemTexture, BackBounds, Back.RarityColor);
                if (Back.hover)
                {
                    DrawHover(spriteBatach, Back);
                }
            }

            if (LeftWeapon != null)
            {
                spriteBatach.Draw(LeftWeapon.ItemTexture, LWeapBounds, LeftWeapon.RarityColor);
                if (LeftWeapon.hover)
                {
                    DrawHover(spriteBatach, LeftWeapon);
                }
            }

            if (Boots != null)
            {
                spriteBatach.Draw(Boots.ItemTexture, BootsBounds, Boots.RarityColor);
                if (Boots.hover)
                {
                    DrawHover(spriteBatach, Boots);
                }
            }

            if (Belt != null)
            {
                spriteBatach.Draw(Belt.ItemTexture, BeltBounds, Belt.RarityColor);
                if (Belt.hover)
                {
                    DrawHover(spriteBatach, Belt);
                }
            }

            if (LeftRing != null)
            {
                spriteBatach.Draw(LeftRing.ItemTexture, LeftRingBounds, LeftRing.RarityColor);
                if (LeftRing.hover)
                {
                    DrawHover(spriteBatach, LeftRing);
                }
            }

            if (RightRing != null)
            {
                spriteBatach.Draw(RightRing.ItemTexture, RightRingBounds, RightRing.RarityColor);
                if (RightRing.hover)
                {
                    DrawHover(spriteBatach, RightRing);
                }
            }

            Hero.Draw(spriteBatach);

        }

        public void RotateClock(object sender, EventArgs eventArgs)
        {
            if (Hero.Direction == 3)
            {
                Hero.Direction = 0;
            }
            else
            {
                Hero.Direction += 1;
            }
        }

        public void RotateCounter(object sender, EventArgs eventArgs)
        {
            if (Hero.Direction == 0)
            {
                Hero.Direction = 3;
            }
            else
            {
                Hero.Direction -= 1;
            }
        }

    }
}
