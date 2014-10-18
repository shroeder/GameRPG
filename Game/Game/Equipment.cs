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
using System.IO;
using System.Xml.Serialization;

namespace TextureAtlas
{
    public class Equipment
    {   
        //Variable
        [XmlIgnore]
        public SpriteFont font1;
        //public List<Item> Items = new List<Item>();
        public Texture2D CharBG;

        public Rectangle Bounds;

        public Rectangle HelmBounds;
        public Rectangle BootsBounds;
        public Rectangle BeltBounds;
        public Rectangle ChestBounds;
        public Rectangle PantsBounds;
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
        public Item Pants;
        public Item RightWeapon;
        public Item LeftWeapon;
        public Item Gloves;
        public Item Boots;
        public Item Belt;
        public Item LeftRing;
        public Item RightRing;
        public HeroDisplay Hero;
        public MouseState ms;


        public Equipment() : this(null,null,null,null,null,null,null,null,null,null,null,null,true) { }

        public Equipment(Item helmet, Item shoulders, Item chest, Item pants, Item rightweapon, Item leftweapon, Item gloves, Item boots, Item belt, Item leftring, Item rightring, HeroDisplay theHero, bool isNull = false)
        {
            if (isNull)
            {

            }
            else
            {
                Hero = theHero;
                Hero.txtRightWeapon = GlobalVariables.TheGame.CharWeapon;
                Hero.txtHero = GlobalVariables.TheGame.HeroTxt;
                Hero.Width = Convert.ToInt32(Hero.txtHero.Width / Hero.Columns);
                Hero.Height = Convert.ToInt32(Hero.txtHero.Height / Hero.Rows);

                Hero.RotateCounterBounds = new Rectangle((int)(GlobalVariables.gfx.PreferredBackBufferWidth * .5), (int)(GlobalVariables.gfx.PreferredBackBufferHeight * .57), (int)(GlobalVariables.gfx.PreferredBackBufferWidth * .03), (int)(GlobalVariables.gfx.PreferredBackBufferHeight * .03));
                Hero.RotateClockBounds = new Rectangle((int)(GlobalVariables.gfx.PreferredBackBufferWidth * .4), (int)(GlobalVariables.gfx.PreferredBackBufferHeight * .57), (int)(GlobalVariables.gfx.PreferredBackBufferWidth * .03), (int)(GlobalVariables.gfx.PreferredBackBufferHeight * .03));

                Hero.Direction = 0;
                Hero.RotateClock += RotateClock;
                Hero.RotateCounter += RotateCounter;
            }
            

            if (GlobalVariables.TheGame == null)
            {
                return;
            }

            if (helmet != null)
            {
                Helmet = helmet;
            }
            if (shoulders != null)
            {
                Shoulders = shoulders;
            }
            if (chest != null){
                Chest = chest;
            }
            if (pants != null){
                Pants = pants;
            }
            if (rightweapon != null){
                RightWeapon = rightweapon;
            }
            if (leftweapon != null){
                LeftWeapon = leftweapon;
            }
            if (gloves != null){
                Gloves = gloves;
            }
            if (boots != null){
                Boots = boots;
            }
            if (belt != null){
                Belt = belt;
            }
            if (leftring != null){
                LeftRing = leftring;
            }
            if (rightring != null){
                RightRing = rightring;
            }

            if (GlobalVariables.gfx != null)
            {
                if (Hero != null)
                {
                    Hero.DrawLocation = new Rectangle((int)(GlobalVariables.gfx.PreferredBackBufferWidth * .395), (int)(GlobalVariables.gfx.PreferredBackBufferHeight * .25), (int)(GlobalVariables.gfx.PreferredBackBufferWidth * .14), (int)(GlobalVariables.gfx.PreferredBackBufferHeight * .3675));
                }

                Bounds = new Rectangle((int)(GlobalVariables.gfx.PreferredBackBufferWidth * .3), (int)(GlobalVariables.gfx.PreferredBackBufferHeight * .1), (int)(GlobalVariables.gfx.PreferredBackBufferWidth * .6), (int)(GlobalVariables.gfx.PreferredBackBufferHeight * .6));

                int width = (int)(GlobalVariables.gfx.PreferredBackBufferWidth * .0628);
                int height = (int)(GlobalVariables.gfx.PreferredBackBufferHeight * .0971);

                HelmBounds = new Rectangle((int)(GlobalVariables.gfx.PreferredBackBufferWidth * .323425), (int)(GlobalVariables.gfx.PreferredBackBufferHeight * .25), width, height);
                ChestBounds = new Rectangle((int)(GlobalVariables.gfx.PreferredBackBufferWidth * .323425), (int)(GlobalVariables.gfx.PreferredBackBufferHeight * .3525), width, height);
                RWeapbounds = new Rectangle((int)(GlobalVariables.gfx.PreferredBackBufferWidth * .323425), (int)(GlobalVariables.gfx.PreferredBackBufferHeight * .455), width, height);
                GlovesBounds = new Rectangle((int)(GlobalVariables.gfx.PreferredBackBufferWidth * .323425), (int)(GlobalVariables.gfx.PreferredBackBufferHeight * .5565), width, height);

                ShouldersBounds = new Rectangle((int)(GlobalVariables.gfx.PreferredBackBufferWidth * .544), (int)(GlobalVariables.gfx.PreferredBackBufferHeight * .25), width, height);
                PantsBounds = new Rectangle((int)(GlobalVariables.gfx.PreferredBackBufferWidth * .544), (int)(GlobalVariables.gfx.PreferredBackBufferHeight * .3525), width, height);
                LWeapBounds = new Rectangle((int)(GlobalVariables.gfx.PreferredBackBufferWidth * .544), (int)(GlobalVariables.gfx.PreferredBackBufferHeight * .455), width, height);
                BootsBounds = new Rectangle((int)(GlobalVariables.gfx.PreferredBackBufferWidth * .544), (int)(GlobalVariables.gfx.PreferredBackBufferHeight * .5565), width, height);

                int beltwidth = (int)(GlobalVariables.gfx.PreferredBackBufferWidth * .0954);
                int beltheight = (int)(GlobalVariables.gfx.PreferredBackBufferHeight * .032);

                BeltBounds = new Rectangle((int)(GlobalVariables.gfx.PreferredBackBufferWidth * .4175), (int)(GlobalVariables.gfx.PreferredBackBufferHeight * .6215), beltwidth, beltheight);

                int ringwidth = (int)(GlobalVariables.gfx.PreferredBackBufferWidth * .02);
                int ringheight = (int)(GlobalVariables.gfx.PreferredBackBufferHeight * .032);

                LeftRingBounds = new Rectangle((int)(GlobalVariables.gfx.PreferredBackBufferWidth * .3945), (int)(GlobalVariables.gfx.PreferredBackBufferHeight * .6215), ringwidth, ringheight);
                RightRingBounds = new Rectangle((int)(GlobalVariables.gfx.PreferredBackBufferWidth * .515), (int)(GlobalVariables.gfx.PreferredBackBufferHeight * .6215), ringwidth, ringheight);

            }

        }

        public void Update()
        {

            ms = Mouse.GetState();

            if (Hero != null)
            {
                Hero.Update();
            }

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
            if (Pants != null)
            {
                if (rect.Intersects(PantsBounds))
                {
                    Pants.hover = true;
                }
                else
                {
                    Pants.hover = false;
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

            //Initialize Scalar Value
            int ScalarText = 20;

            int widestString = 0;
            foreach (Affix afx in item.AffixList)
            {
                double textWidth1 = item.Font1.MeasureString(afx.Desc).X;
                if (textWidth1 > widestString)
                {
                    widestString = (int)textWidth1;
                }
            }

            //Store location
            Vector2 loc = item.location;

            List<string> ItemDesc = new List<string>();

            if (item.ItemType == 1)
            {
                ItemDesc = GlobalVariables.GenerateDescList(item.ItemDescription, widestString + (item.ItemTexture.Width * .8), item.Font1);
            }
            else if (item.ItemType == 2)
            {
                ItemDesc = GlobalVariables.GenerateDescList(item.ItemDescription, widestString + (item.ItemTexture.Width * .4), item.Font1);
            }
            else if (item.ItemType == 3)
            {
                ItemDesc = GlobalVariables.GenerateDescList(item.ItemDescription, widestString + (item.ItemTexture.Width * .4), item.Font1);
            }

            //check to render on screen
            item.location = GlobalVariables.newLocation(item.location, widestString + 40, 70 + (int)(22 * item.affixes) + (int)(22 * ItemDesc.Count));

            //Draw Shaded backgorund
            GlobalVariables.WaitToDraw(0, new Vector2((item.location.X + 80), (item.location.Y)), new Rectangle(0, 0, widestString + 40 + (int)(item.ItemTexture.Width * .75), 70 + (int)(22 * item.affixes) + (int)(22 * ItemDesc.Count)), Color.Black, null, GlobalVariables.TestSquare);

            GlobalVariables.WaitToDraw(1, new Vector2((item.location.X + 100), (item.location.Y + 30)), new Rectangle(0, 0, 0, 0), item.RarityColor, item.Font1, null, item.ItemName);

            //Draw small item in top right
            if (item.ItemType == 1)
            {
                GlobalVariables.WaitToDraw(0, new Vector2((item.location.X + 175 + (widestString - item.ItemTexture.Width)), item.location.Y + 25), new Rectangle(0, 0, item.ItemTexture.Width, item.ItemTexture.Height), item.ItemColor, null, item.ItemTexture);
            }
            else if (item.ItemType == 2)
            {
                GlobalVariables.WaitToDraw(0, new Vector2((item.location.X + 115 + (widestString - item.ItemTexture.Width)), item.location.Y + 25), new Rectangle(0, 0, item.ItemTexture.Width, item.ItemTexture.Height), item.ItemColor, null, item.ItemTexture);
            }
            else if (item.ItemType == 3)
            {
                GlobalVariables.WaitToDraw(0, new Vector2((item.location.X + 140 + (widestString - item.ItemTexture.Width)), item.location.Y + 5), new Rectangle(0, 0, item.ItemTexture.Width, item.ItemTexture.Height), item.ItemColor, null, item.ItemTexture);
            }


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
            foreach (string str in ItemDesc)
            {
                GlobalVariables.WaitToDraw(1, new Vector2((item.location.X + 100), (item.location.Y + ScalarText + 50)), new Rectangle(0, 0, 0, 0), Color.Gray, item.Font1, null, str);

                ScalarText += 20;
            }

            item.location = loc;
        }

        public void draw(SpriteBatch spriteBatach, Texture2D EquipSheet, SpriteFont font)
        {

            font1 = font;
            CharBG = EquipSheet;

            spriteBatach.Draw(CharBG, Bounds, Color.White);

            if (Helmet != null)
            {
                Rectangle newRect = new Rectangle(HelmBounds.X + (HelmBounds.X / 4), HelmBounds.Y, HelmBounds.Width / 2, HelmBounds.Height);
                spriteBatach.Draw(Helmet.ItemTexture, newRect, Helmet.ItemColor);
                if (Helmet.hover)
                {
                    DrawHover(spriteBatach, Helmet);
                }
            }

            if (Chest != null)
            {
                Rectangle newRect = new Rectangle(ChestBounds.X + (ChestBounds.X / 4), ChestBounds.Y, ChestBounds.Width / 2, ChestBounds.Height);
                spriteBatach.Draw(Chest.ItemTexture, newRect, Chest.ItemColor);
                if (Chest.hover)
                {
                    DrawHover(spriteBatach, Chest);
                }
            }

            if (RightWeapon != null)
            {
                Rectangle newRect = new Rectangle(RWeapbounds.X, RWeapbounds.Y + (RWeapbounds.Height / 4), RWeapbounds.Width, RWeapbounds.Height / 2);
                spriteBatach.Draw(RightWeapon.ItemTexture, newRect, RightWeapon.ItemColor);
                if (RightWeapon.hover)
                {
                    DrawHover(spriteBatach, RightWeapon);
                }
            }

            if (Gloves != null)
            {
                spriteBatach.Draw(Gloves.ItemTexture, GlovesBounds, Gloves.ItemColor);
                if (Gloves.hover)
                {
                    DrawHover(spriteBatach, Gloves);
                }
            }

            if (Shoulders != null)
            {
                spriteBatach.Draw(Shoulders.ItemTexture, ShouldersBounds, Shoulders.ItemColor);
                if (Shoulders.hover)
                {
                    DrawHover(spriteBatach, Shoulders);
                }
            }

            if (Pants != null)
            {
                spriteBatach.Draw(Pants.ItemTexture, PantsBounds, Pants.ItemColor);
                if (Pants.hover)
                {
                    DrawHover(spriteBatach, Pants);
                }
            }

            if (LeftWeapon != null)
            {
                Rectangle newRect = new Rectangle(LWeapBounds.X, LWeapBounds.Y + (LWeapBounds.Height / 4), LWeapBounds.Width, LWeapBounds.Height / 2);
                spriteBatach.Draw(LeftWeapon.ItemTexture, newRect, LeftWeapon.ItemColor);
                if (LeftWeapon.hover)
                {
                    DrawHover(spriteBatach, LeftWeapon);
                }
            }

            if (Boots != null)
            {
                Rectangle newRect = new Rectangle(BootsBounds.X + (BootsBounds.Width / 4), BootsBounds.Y + (BootsBounds.Height / 4), BootsBounds.Width / 2, BootsBounds.Height / 2);
                spriteBatach.Draw(Boots.ItemTexture, newRect, Boots.ItemColor);
                if (Boots.hover)
                {
                    DrawHover(spriteBatach, Boots);
                }
            }

            if (Belt != null)
            {
                spriteBatach.Draw(Belt.ItemTexture, BeltBounds, Belt.ItemColor);
                if (Belt.hover)
                {
                    DrawHover(spriteBatach, Belt);
                }
            }

            if (LeftRing != null)
            {
                spriteBatach.Draw(LeftRing.ItemTexture, LeftRingBounds, LeftRing.ItemColor);
                if (LeftRing.hover)
                {
                    DrawHover(spriteBatach, LeftRing);
                }
            }

            if (RightRing != null)
            {
                spriteBatach.Draw(RightRing.ItemTexture, RightRingBounds, RightRing.ItemColor);
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
