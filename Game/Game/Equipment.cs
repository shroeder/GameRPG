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

        [XmlIgnore]
        public MenuButton btnOffense;
        [XmlIgnore]
        public MenuButton btnDefense;
        [XmlIgnore]
        public MenuButton btnUtility;
        [XmlIgnore]
        public Boolean btnOffenseBound = false;
        [XmlIgnore]
        public Boolean btnUtilityBound = false;
        [XmlIgnore]
        public Boolean btnDefenseBound = false;
        [XmlIgnore]
        public Boolean Offense = true;
        [XmlIgnore]
        public Boolean Defense = false;
        [XmlIgnore]
        public Boolean Utility = false;

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

                if (btnOffense == null)
                {
                    btnOffense = new MenuButton();
                    btnOffenseBound = false;
                }
                if (btnDefense == null)
                {
                    btnDefense = new MenuButton();
                    btnDefenseBound = false;
                }
                if (btnUtility == null)
                {
                    btnUtility = new MenuButton();
                    btnUtilityBound = false;
                }
                if (!btnOffenseBound)
                {
                    btnOffense.ButtonClicked += btnOffense_Clicked;
                    btnOffenseBound = true;
                }
                if (!btnDefenseBound)
                {
                    btnDefense.ButtonClicked += btnDefense_Clicked;
                    btnDefenseBound = true;
                }
                if (!btnUtilityBound)
                {
                    btnUtility.ButtonClicked += btnUtility_Clicked;
                    btnUtilityBound = true;
                }
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

        private void btnUtility_Clicked(object sender, EventArgs e)
        {
            Offense = false;
            Defense = false;
            Utility = true;
        }

        private void btnDefense_Clicked(object sender, EventArgs e)
        {
            Offense = false;
            Defense = true;
            Utility = false;
        }

        private void btnOffense_Clicked(object sender, EventArgs e)
        {
            Offense = true;
            Defense = false;
            Utility = false;
        }

        public void Update()
        {

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

                Hero.RotateCounterBounds = new Rectangle((int)(GlobalVariables.gfx.PreferredBackBufferWidth * .5), (int)(GlobalVariables.gfx.PreferredBackBufferHeight * .57), (int)(GlobalVariables.gfx.PreferredBackBufferWidth * .03), (int)(GlobalVariables.gfx.PreferredBackBufferHeight * .03));
                Hero.RotateClockBounds = new Rectangle((int)(GlobalVariables.gfx.PreferredBackBufferWidth * .4), (int)(GlobalVariables.gfx.PreferredBackBufferHeight * .57), (int)(GlobalVariables.gfx.PreferredBackBufferWidth * .03), (int)(GlobalVariables.gfx.PreferredBackBufferHeight * .03));


            }

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

            float textWidth2;

            float WeaponBaseDisplay = 0;

            if (item.ItemSlot == Item.itemSlot.OneHanded || item.ItemSlot == Item.itemSlot.TwoHanded)
            {
                WeaponBaseDisplay = GlobalVariables.CalculateMeleePhysStat(item.AffixList, item.BaseStat);
            }

            if (item.BaseStat != WeaponBaseDisplay)
            {
                textWidth2 = GlobalVariables.Font16.MeasureString(((int)(WeaponBaseDisplay) - 20).ToString() + " - " + ((int)(WeaponBaseDisplay) + 20).ToString()).X;
            }
            else
            {
                textWidth2 = GlobalVariables.Font16.MeasureString((item.BaseStat - 20).ToString() + " - " + (item.BaseStat + 20).ToString()).X;
            }

            if (widestString < textWidth2)
            {
                widestString = (int)textWidth2;
            }

            float textWidth3 = (float)(0.0);

            if (item.BaseAttackSpeed > 0)
            {
                textWidth3 = GlobalVariables.font14.MeasureString("Attack Speed : " + item.BaseAttackSpeed.ToString()).X;
            }

            if (widestString < textWidth3)
            {
                widestString = (int)textWidth3;
            }

            //Store location
            Vector2 loc = item.location;

            List<string> ItemDesc = new List<string>();

            if (item.ItemType == 1)
            {
                ItemDesc = GlobalVariables.GenerateDescList(item.ItemDescription, widestString + (item.ItemTexture.Width * .75), item.Font1);
            }
            else if (item.ItemType == 2 || item.ItemType == 5 || item.ItemType == 6 || item.ItemType == 7 || item.ItemType == 8 || item.ItemType == 9)
            {
                ItemDesc = GlobalVariables.GenerateDescList(item.ItemDescription, widestString + (item.ItemTexture.Width * .35), item.Font1);
            }
            else if (item.ItemType == 3)
            {
                ItemDesc = GlobalVariables.GenerateDescList(item.ItemDescription, widestString + (item.ItemTexture.Width * .2), item.Font1);
            }
            else if (item.ItemType == 4)
            {
                ItemDesc = GlobalVariables.GenerateDescList(item.ItemDescription, widestString + (item.ItemTexture.Width * .3), item.Font1);
            }

            //check to render on screen
            item.location = GlobalVariables.newLocation(item.location, widestString + 40, 70 + (int)(22 * item.affixes) + (int)(22 * ItemDesc.Count));

            //Draw Shaded backgorund
            GlobalVariables.WaitToDraw(0, new Vector2((item.location.X + 80), (item.location.Y)), new Rectangle(0, 0, widestString + 40 + (int)(item.ItemTexture.Width * .75), 70 + (int)(22 * item.affixes) + (int)(22 * ItemDesc.Count) + 60), Color.Black, null, GlobalVariables.TestSquare);

            GlobalVariables.WaitToDraw(1, new Vector2((item.location.X + 100), (item.location.Y + 30)), new Rectangle(0, 0, 0, 0), item.RarityColor, item.Font1, null, item.ItemName);

            //Draw small item in top right
            if (item.ItemType == 1)
            {
                GlobalVariables.WaitToDraw(0, new Vector2((item.location.X + 175 + (widestString - item.ItemTexture.Width)), item.location.Y + 25), new Rectangle(0, 0, item.ItemTexture.Width, item.ItemTexture.Height), item.ItemColor, null, item.ItemTexture);
            }
            else if (item.ItemType == 2 || item.ItemType == 5 || item.ItemType == 6 || item.ItemType == 7 || item.ItemType == 8 || item.ItemType == 9)
            {
                GlobalVariables.WaitToDraw(0, new Vector2((item.location.X + 115 + (widestString - item.ItemTexture.Width)), item.location.Y + 25), new Rectangle(0, 0, item.ItemTexture.Width, item.ItemTexture.Height), item.ItemColor, null, item.ItemTexture);
            }
            else if (item.ItemType == 3 || item.ItemType == 4)
            {
                GlobalVariables.WaitToDraw(0, new Vector2((item.location.X + 140 + (widestString - item.ItemTexture.Width)), item.location.Y + 5), new Rectangle(0, 0, item.ItemTexture.Width, item.ItemTexture.Height), item.ItemColor, null, item.ItemTexture);
            }

            //Draw Base Stat
            if (item.ItemSlot == Item.itemSlot.OneHanded || item.ItemSlot == Item.itemSlot.TwoHanded)
            {
                if (item.BaseStat != (int)(WeaponBaseDisplay))
                {
                    GlobalVariables.WaitToDraw(1, new Vector2((item.location.X + 100), (item.location.Y + ScalarText + 50)), new Rectangle(0, 0, 0, 0), Color.DarkSlateBlue, GlobalVariables.Font16, null, item.BaseStatName + " : " + ((int)(WeaponBaseDisplay) - 20).ToString() + " - " + ((int)(WeaponBaseDisplay) + 20).ToString());
                }
                else
                {
                    GlobalVariables.WaitToDraw(1, new Vector2((item.location.X + 100), (item.location.Y + ScalarText + 50)), new Rectangle(0, 0, 0, 0), Color.Gray, GlobalVariables.Font16, null, item.BaseStatName + " : " + (item.BaseStat - 20).ToString() + " - " + (item.BaseStat + 20).ToString());
                }
                ScalarText += 30;
            }
            else if (item.ItemSlot == Item.itemSlot.Boots || item.ItemSlot == Item.itemSlot.Pants || item.ItemSlot == Item.itemSlot.Gloves || item.ItemSlot == Item.itemSlot.Chest || item.ItemSlot == Item.itemSlot.Helmet || item.ItemSlot == Item.itemSlot.Shoulders)
            {
                int newValue = GlobalVariables.CalculateEvasion(item.AffixList, item.BaseStat);
                if (newValue != item.BaseStat)
                {
                    GlobalVariables.WaitToDraw(1, new Vector2((item.location.X + 100), (item.location.Y + ScalarText + 50)), new Rectangle(0, 0, 0, 0), Color.DarkSlateBlue, GlobalVariables.Font16, null, item.BaseStatName + " : " + newValue.ToString());
                }
                else
                {
                    GlobalVariables.WaitToDraw(1, new Vector2((item.location.X + 100), (item.location.Y + ScalarText + 50)), new Rectangle(0, 0, 0, 0), Color.Gray, GlobalVariables.Font16, null, item.BaseStatName + " : " + item.BaseStat.ToString());
                }
                ScalarText += 30;
            }

            //Draw Attack Speed if applicable
            if (item.ItemSlot == Item.itemSlot.OneHanded || item.ItemSlot == Item.itemSlot.TwoHanded)
            {
                float newAtkSpd = GlobalVariables.CalculateMeleeAttackSpeed(item.AffixList, (float)item.BaseAttackSpeed);
                if ((float)newAtkSpd != (float)item.BaseAttackSpeed)
                {
                    GlobalVariables.WaitToDraw(1, new Vector2((item.location.X + 100), (item.location.Y + ScalarText + 50)), new Rectangle(0, 0, 0, 0), Color.DarkSlateBlue, GlobalVariables.font14, null, "Attack Speed : " + newAtkSpd.ToString());
                }
                else
                {
                    GlobalVariables.WaitToDraw(1, new Vector2((item.location.X + 100), (item.location.Y + ScalarText + 50)), new Rectangle(0, 0, 0, 0), Color.Gray, GlobalVariables.font14, null, "Attack Speed : " + item.BaseAttackSpeed.ToString());
                }
                ScalarText += 30;
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

            List<string> Values = new List<string>();
            double theValue = 0;

            Vector2 StartPosition = new Vector2((float)(GlobalVariables.gfx.PreferredBackBufferWidth * .64), (float)(GlobalVariables.gfx.PreferredBackBufferHeight * .275));

            //Base Damage

            if (RightWeapon != null)
            {
                if (RightWeapon.RangedMelee == 0)
                {
                    theValue = GlobalVariables.GetCharacterPhysicalDamage(0);
                }
                else if (RightWeapon.RangedMelee == 1)
                {
                    theValue = GlobalVariables.GetCharacterPhysicalDamage(1);
                }
            }
            else if (LeftWeapon != null)
            {
                if (RightWeapon.RangedMelee == 0)
                {
                    theValue = GlobalVariables.GetCharacterPhysicalDamage(0);
                }
                else if (RightWeapon.RangedMelee == 1)
                {
                    theValue = GlobalVariables.GetCharacterPhysicalDamage(1);
                }
            }
            else
            {
                theValue = GlobalVariables.GetCharacterPhysicalDamage(0);
            }

            theValue = Math.Round(theValue,2);

            if (Offense)
            {
                Values.Add("Physical Damage : " + theValue.ToString());
                Values.Add("Attack Speed : " + GlobalVariables._CharacterAttackSpeed.ToString() + "%");
                Values.Add("Chance To Hit : " + GlobalVariables._CharacterChanceToHit.ToString() + "%");
                Values.Add("Melee Range : " + GlobalVariables._CharacterMeleeRange.ToString() + "%");
                Values.Add("Critical Chance : " + GlobalVariables._CharacterCritChance.ToString() + "%");
                Values.Add("Critical Damage Modifier : " + GlobalVariables._CharacterCritDamageModifier.ToString() + "%");
                Values.Add("Armour Penetration : " + GlobalVariables._CharacterArmourPenetration.ToString() + "%");
                Values.Add("Magic Penetration : " + GlobalVariables._CharacterMagicPenetration.ToString() + "%");
                Values.Add("Increase Damage Vs Boss : " + GlobalVariables._CharacterVsBossDamage.ToString() + "%");
                Values.Add("Increase Damage Vs Elite : " + GlobalVariables._CharacterVsEliteDamage.ToString() + "%");
                Values.Add("Increase Damage Vs Beast : " + GlobalVariables._CharacterVsBeastDamage.ToString() + "%");
                Values.Add("Increase Damage Vs Human : " + GlobalVariables._CharacterVsHumanDamage.ToString() + "%");
                Values.Add("Increase Damage Vs Undead : " + GlobalVariables._CharacterVsUndeadDamage.ToString() + "%");
            }
            else if (Defense)
            {
                Values.Add("Health : " + GlobalVariables._CharacterTotalHealth.ToString());
                Values.Add("Health Regen : " + GlobalVariables._CharacterTotalHealthRegen.ToString());
                Values.Add("Life Steal : " + GlobalVariables._CharacterHealthSteal.ToString() + "%");
                Values.Add("Armour : " + GlobalVariables._CharacterTotalArmour.ToString());
                Values.Add("Evasion : " + GlobalVariables._CharacterTotalEvasion.ToString());
                Values.Add("Chance to Dodge : " + GlobalVariables._CharacterChanceToDodge.ToString() + "%");
                Values.Add("Physical Damage Reduction : " + GlobalVariables._CharacterPhysDamageReduction.ToString() + "%");
            }
            else if (Utility)
            {
                Values.Add("Mana : " + GlobalVariables._CharacterTotalMana.ToString());
                Values.Add("Mana Regen : " + GlobalVariables._CharacterTotalManaRegen.ToString());
                Values.Add("Mana Steal : " + GlobalVariables._CharacterManaSteal.ToString() + "%");
                Values.Add("Movement Speed : " + GlobalVariables._CharacterMovementSpeed.ToString() + "%");
                Values.Add("Increased Experience : " + GlobalVariables._CharacterIncreaseExpPct.ToString() + "%");
                Values.Add("Increased Rarity : " + GlobalVariables._CharacterMagicFindRarity.ToString() + "%");
                Values.Add("Increased Quantity : " + GlobalVariables._CharacterMagicFindQuantity.ToString() + "%");
            }

            //Loop through and display values

            foreach (string str in Values)
            {
                GlobalVariables.WaitToDraw(1, StartPosition, new Rectangle(0, 0, 0, 0), Color.Black, GlobalVariables.AutoFont(GlobalVariables.gfx, 3), null, str);
                StartPosition.Y += GlobalVariables.AutoFont(GlobalVariables.gfx, 3).MeasureString("T").Y;
            }

            font1 = font;
            CharBG = EquipSheet;

            spriteBatach.Draw(CharBG, Bounds, Color.White);

            if (Helmet != null)
            {
                Rectangle newRect = new Rectangle(HelmBounds.X + (HelmBounds.Width / 4), HelmBounds.Y + (HelmBounds.Height / 4), HelmBounds.Width / 2, HelmBounds.Height / 2);
                spriteBatach.Draw(Helmet.ItemTexture, newRect, Helmet.ItemColor);
                if (Helmet.hover)
                {
                    DrawHover(spriteBatach, Helmet);
                }
            }

            if (Chest != null)
            {
                Rectangle newRect = new Rectangle(ChestBounds.X, ChestBounds.Y, ChestBounds.Width, ChestBounds.Height);
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
                Rectangle newRect = new Rectangle(GlovesBounds.X + (GlovesBounds.Width / 4), GlovesBounds.Y + (GlovesBounds.Height / 4), GlovesBounds.Width / 2, GlovesBounds.Height / 2);
                spriteBatach.Draw(Gloves.ItemTexture, newRect, Gloves.ItemColor);
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
                Rectangle newRect = new Rectangle(BeltBounds.X + (BeltBounds.Width / 4), BeltBounds.Y, BeltBounds.Width / 2, BeltBounds.Height );
                spriteBatach.Draw(Belt.ItemTexture, newRect, Belt.ItemColor);
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

                btnOffense.Draw(spriteBatach, GlobalVariables.txtButton, GlobalVariables.gfx, new Rectangle((int)(GlobalVariables.gfx.PreferredBackBufferWidth * .64), (int)(GlobalVariables.gfx.PreferredBackBufferHeight * .22), (int)(GlobalVariables.gfx.PreferredBackBufferWidth * .05), (int)(GlobalVariables.gfx.PreferredBackBufferHeight * .05)), "Offense", GlobalVariables.AutoFont(GlobalVariables.gfx, 2));
                btnDefense.Draw(spriteBatach, GlobalVariables.txtButton, GlobalVariables.gfx, new Rectangle((int)(GlobalVariables.gfx.PreferredBackBufferWidth * .70), (int)(GlobalVariables.gfx.PreferredBackBufferHeight * .22), (int)(GlobalVariables.gfx.PreferredBackBufferWidth * .05), (int)(GlobalVariables.gfx.PreferredBackBufferHeight * .05)), "Defense", GlobalVariables.AutoFont(GlobalVariables.gfx, 2));
                btnUtility.Draw(spriteBatach, GlobalVariables.txtButton, GlobalVariables.gfx, new Rectangle((int)(GlobalVariables.gfx.PreferredBackBufferWidth * .76), (int)(GlobalVariables.gfx.PreferredBackBufferHeight * .22), (int)(GlobalVariables.gfx.PreferredBackBufferWidth * .05), (int)(GlobalVariables.gfx.PreferredBackBufferHeight * .05)), "Utility", GlobalVariables.AutoFont(GlobalVariables.gfx, 2));

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
