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
    public class Item
    {
        #region Variables

        public int quality;
        public int affixes;
        public int ItemType;

        public Boolean hover = false;
        public Boolean invhover = false;

        public Vector2 worldloc;
        public Vector2 location;

        public string DroppedTextureName;

        public Rectangle Bounds;

        public string ItemName;

        public List<Affix> AffixList = new List<Affix>();

        [XmlIgnore]
        public SpriteFont Font1;

        public Random RNG = new Random();

        public string ItemDescription;

        public Texture2D LegBeam;
        public Texture2D TextureBack;
        public Texture2D ItemTexture;
        public Texture2D LegendaryBg;
        public int ItemLevel;
        public int SubType;

        public string ItemTextureName;

        public Color RarityColor;
        public Color ItemColor;

        #endregion

        public enum itemSlot 
        {
            Helmet,
            OneHanded,
            TwoHanded,
            Gloves,
            Shoulders,
            Boots,
            Chest,
            Back,
            Ring,
            Belt,
            Nothing
        }

        public itemSlot ItemSlot;

        public Item() : this(new Vector2(0, 0), null, 1, 1, itemSlot.Nothing, "", 1) { }

        public Item(Vector2 Location, Texture2D tex, int itemtype, int itmlvl, itemSlot itmslot, string drptxtname , int subType = 0, bool indexed = false)
        {
            ItemLevel = itmlvl;
            DroppedTextureName = drptxtname;
            ItemSlot = itmslot;
            LegBeam = GlobalVariables.LegendaryBeam;
            LegendaryBg = GlobalVariables.LegendaryBG;
            TextureBack = GlobalVariables.TextureBack;
            worldloc = Location;
            location = Location;
            ItemTexture = tex;
            ItemType = itemtype;
            SubType = subType;
            if (tex == null)
            {
                return;
            }
            Bounds = new Rectangle((int)location.X, (int)location.Y, tex.Width, tex.Height);
            Font1 = GlobalVariables.Font10;

            ItemName = "";

            if (!indexed)
            {
                quality = GlobalVariables.RollVsRarity();
                if (quality > 5)
                {
                    ItemTextureName = GlobalVariables.GetTexture(itemtype, SubType, true);
                    ItemDescription = GlobalVariables.GetItemDescription(itemtype, SubType, true);
                    DroppedTextureName = GlobalVariables.GetItemName(itemtype, SubType, true);
                    ItemTexture = GlobalVariables.TheGame.Content.Load<Texture2D>(DroppedTextureName);
                }
                else
                {
                    ItemTextureName = GlobalVariables.GetTexture(itemtype, SubType, false);
                    ItemDescription = GlobalVariables.GetItemDescription(itemtype, SubType, false);
                    DroppedTextureName = GlobalVariables.GetItemName(itemtype, SubType, false);
                }

                switch (quality)
                {
                    case 1:
                        ItemName = GlobalVariables.GetItemByType(ItemType, SubType);
                        affixes = 2;
                        break;
                    case 2:
                        ItemName = GlobalVariables.GetItemByType(ItemType, SubType);
                        affixes = 3;
                        break;
                    case 3:
                        ItemName = GlobalVariables.GetItemByType(ItemType, SubType);
                        affixes = 4;
                        break;
                    case 4:
                        ItemName = GlobalVariables.GetItemByType(ItemType, SubType);
                        affixes = 5;
                        break;
                    case 5:
                        ItemName = GlobalVariables.GetItemByType(ItemType, SubType);
                        affixes = 6;
                        break;
                    case 6:
                        ItemName = GlobalVariables.GetUniqueByTypes(ItemType, SubType);
                        affixes = 10;
                        break;
                }

                AffixList = GlobalVariables.RollVsAffix(affixes, ItemType, ItemLevel, SubType);
            }
        }

        public void CharMovedRight(GameTime gameTime, Vector2 velocity)
        {
            location -= (velocity * (float)gameTime.ElapsedGameTime.TotalSeconds);
            Bounds.X = (int)location.X;
        }

        public void CharMovedLeft(GameTime gameTime, Vector2 velocity)
        {
            location += (velocity * (float)gameTime.ElapsedGameTime.TotalSeconds);
            Bounds.X = (int)location.X;
        }

        public void CharMovedUp(GameTime gameTime, Vector2 velocityup)
        {
            location += (velocityup * (float)gameTime.ElapsedGameTime.TotalSeconds);
            Bounds.Y = (int)location.Y;
        }

        public void CharMovedDown(GameTime gameTime, Vector2 velocityup)
        {
            location -= (velocityup * (float)gameTime.ElapsedGameTime.TotalSeconds);
            Bounds.Y = (int)location.Y;
        } 

        public void Draw(SpriteBatch spriteBatch, Vector2 location, bool Clamp = false)
        {

            spriteBatch.Begin();

            if (Clamp)
            {
                spriteBatch.Draw(ItemTexture, new Rectangle((int)location.X,(int)location.Y,ItemTexture.Width,ItemTexture.Height), ItemColor);
                spriteBatch.End();
                return;
            }

            if (quality == 1)
            {
                spriteBatch.Draw(ItemTexture, new Rectangle(Convert.ToInt32(location.X), Convert.ToInt32(location.Y), ItemTexture.Width, ItemTexture.Height), Color.White);
                RarityColor = Color.White;
                ItemColor = Color.White;
            }
            if (quality == 2)
            {
                spriteBatch.Draw(ItemTexture, new Rectangle(Convert.ToInt32(location.X), Convert.ToInt32(location.Y), ItemTexture.Width, ItemTexture.Height), Color.AliceBlue);
                RarityColor = Color.AliceBlue;
                ItemColor = Color.AliceBlue;
            }
            if (quality == 3)
            {
                spriteBatch.Draw(ItemTexture, new Rectangle(Convert.ToInt32(location.X), Convert.ToInt32(location.Y), ItemTexture.Width, ItemTexture.Height), Color.DeepSkyBlue);
                RarityColor = Color.DeepSkyBlue;
                ItemColor = Color.DeepSkyBlue;
            }
            if (quality == 4)
            {
                spriteBatch.Draw(ItemTexture, new Rectangle(Convert.ToInt32(location.X), Convert.ToInt32(location.Y), ItemTexture.Width, ItemTexture.Height), Color.Orange);
                RarityColor = Color.Orange;
                ItemColor = Color.Orange;
            }
            if (quality == 5)
            {
                spriteBatch.Draw(LegendaryBg, new Rectangle(Convert.ToInt32(location.X), Convert.ToInt32(location.Y), ItemTexture.Width, ItemTexture.Height), Color.White);
                spriteBatch.Draw(ItemTexture, new Rectangle(Convert.ToInt32(location.X), Convert.ToInt32(location.Y), ItemTexture.Width, ItemTexture.Height), Color.NavajoWhite);
                RarityColor = Color.Purple;
                ItemColor = Color.NavajoWhite;
            }
            if (quality == 6)
            {
                spriteBatch.Draw(LegendaryBg, new Rectangle(Convert.ToInt32(location.X), Convert.ToInt32(location.Y), ItemTexture.Width, ItemTexture.Height), Color.White);
                spriteBatch.Draw(ItemTexture, new Rectangle(Convert.ToInt32(location.X), Convert.ToInt32(location.Y), ItemTexture.Width, ItemTexture.Height), Color.White);
                RarityColor = Color.Brown;
                ItemColor = Color.White;
            }
            

                        if (hover)
                        {
                            if (GlobalVariables.TheGame.blnEquip)
                            {
                                if (!GlobalVariables.TheGame.equipment.Bounds.Intersects(Bounds))
                                {
                                    spriteBatch.End();
                                    return;
                                }
                            }

                            if (GlobalVariables.ShowItemNames)
                            {
                                double textWidth = Font1.MeasureString(ItemName).X;
                                if (ItemType == 1)
                                {
                                    int offset = (int)(textWidth * .025);
                                    spriteBatch.DrawString(Font1, ItemName, new Vector2(location.X - offset - offset, (location.Y - 20)), RarityColor);
                                }
                                else if (ItemType == 2)
                                {
                                    int offset = (int)(textWidth * .275);
                                    spriteBatch.DrawString(Font1, ItemName, new Vector2(location.X - offset, (location.Y - 20)), RarityColor);
                                }
                            }

                            int widestString = 0;
                            foreach (Affix afx in AffixList)
                            {
                                double textWidth1 = Font1.MeasureString(afx.Desc).X;
                                if (textWidth1 > widestString)
                                {
                                    widestString = (int)textWidth1;
                                }
                            }

                            //Store location
                            Vector2 loc = location;

                            List<string> ItemDesc = new List<string>();

                            if (ItemType == 1)
                            {
                                ItemDesc = GlobalVariables.GenerateDescList(ItemDescription, widestString + (ItemTexture.Width * .9), Font1);
                            }
                            else if (ItemType == 2)
                            {
                                ItemDesc = GlobalVariables.GenerateDescList(ItemDescription, widestString + (ItemTexture.Width * .4), Font1);
                            }

                            //check to render on screen
                            location = GlobalVariables.newLocation(location, widestString + 40, 70 + (int)(22 * affixes) + (int)(22 * ItemDesc.Count));

                            //Draw Shaded backgorund
                            GlobalVariables.WaitToDraw(0, new Vector2((location.X + 80),(location.Y)), new Rectangle(0, 0, widestString + 40 + (int)(ItemTexture.Width * .75), 70 + (int)(22 * affixes) + (int)(22 * ItemDesc.Count)), Color.Black, null, GlobalVariables.TestSquare);

                            //Draw small item in top right
                            if (ItemType == 1)
                            {
                                GlobalVariables.WaitToDraw(0, new Vector2((location.X + 175 + (widestString - ItemTexture.Width)), location.Y + 25), new Rectangle(0, 0, ItemTexture.Width, ItemTexture.Height), ItemColor, null, ItemTexture);
                            }
                            else if (ItemType == 2)
                            {
                                GlobalVariables.WaitToDraw(0, new Vector2((location.X + 115 + (widestString - ItemTexture.Width)), location.Y + 25), new Rectangle(0, 0, ItemTexture.Width, ItemTexture.Height), ItemColor, null, ItemTexture);
                            }

                            GlobalVariables.WaitToDraw(1,  new Vector2((location.X + 100), (location.Y + 30)), new Rectangle(0, 0, 0, 0),RarityColor, Font1, null,ItemName);

                            //Initialize Scalar Value
                            int ScalarText = 20;

                            //Draw Item aFfixes
                            for (int intlc = 0; intlc < AffixList.Count; intlc++)
                            {
                                if (intlc < 4)
                                {
                                    GlobalVariables.WaitToDraw(1, new Vector2((location.X + 100), (location.Y + ScalarText + 50)), new Rectangle(0, 0, 0, 0), Color.White, Font1, null, AffixList[intlc].Desc);
                                    ScalarText += 20;
                                }
                                if (intlc == 4)
                                {
                                    GlobalVariables.WaitToDraw(1, new Vector2((location.X + 100), (location.Y + ScalarText + 50)), new Rectangle(0, 0, 0, 0), Color.Orange, Font1, null, AffixList[intlc].Desc);
                                    ScalarText += 20;
                                }
                                if (intlc == 5)
                                {
                                    GlobalVariables.WaitToDraw(1, new Vector2((location.X + 100), (location.Y + ScalarText + 50)), new Rectangle(0, 0, 0, 0), Color.Purple, Font1, null, AffixList[intlc].Desc);
                                    ScalarText += 20;
                                }
                                if (intlc > 5)
                                {
                                    GlobalVariables.WaitToDraw(1, new Vector2((location.X + 100), (location.Y + ScalarText + 50)), new Rectangle(0, 0, 0, 0), Color.Brown, Font1, null, AffixList[intlc].Desc);
                                    ScalarText += 20;
                                }
                            }

                            foreach (string str in ItemDesc)
                            {
                                GlobalVariables.WaitToDraw(1, new Vector2((location.X + 100), (location.Y + ScalarText + 50)), new Rectangle(0, 0, 0, 0), Color.Gray, Font1, null, str);

                                ScalarText += 20;
                            }

                            location = loc;

                        }

                        else 
                        {
                            if (GlobalVariables.ShowItemNames)
                            {
                                double textWidth = Font1.MeasureString(ItemName).X;
                                if (ItemType == 1)
                                {
                                    int offset = (int)(textWidth * .025);
                                    spriteBatch.DrawString(Font1, ItemName, new Vector2(location.X - offset - offset, (location.Y - 20)), RarityColor);
                                }
                                else if (ItemType == 2)
                                {
                                    int offset = (int)(textWidth * .275);
                                    spriteBatch.DrawString(Font1, ItemName, new Vector2(location.X - offset, (location.Y - 20)), RarityColor);
                                }
                            }
                        } 
            spriteBatch.End();
        }
    }
}
