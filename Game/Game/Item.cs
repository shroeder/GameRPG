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
    public class Item
    {
        #region Variables

        public int quality;
        public int affixes;

        public Boolean hover = false;
        public Boolean invhover = false;

        public Vector2 worldloc;
        public Vector2 location;

        public Rectangle Bounds;

        public string ItemName;

        public List<Affix> AffixList = new List<Affix>();

        public SpriteFont Font1;

        public Random RNG = new Random();

        public Texture2D LegBeam;
        public Texture2D TextureBack;
        public Texture2D ItemTexture;
        public Texture2D LegendaryBg;

        public Color RarityColor;

        #endregion

        public Item(Vector2 Location, Texture2D tex, int ItemType, int ItemLevel, int SubType = 0)
        {
            LegBeam = GlobalVariables.LegendaryBeam;
            LegendaryBg = GlobalVariables.LegendaryBG;
            TextureBack = GlobalVariables.TextureBack;
            worldloc = Location;
            location = Location;
            ItemTexture = tex;
            Bounds = new Rectangle((int)location.X, (int)location.Y, tex.Width, tex.Height);
            Font1 = GlobalVariables.Font10;
            quality = GlobalVariables.RollVsRarity();
            ItemName = "";

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
                    ItemName = GlobalVariables.GetUniqueByTypes(ItemType,SubType);
                    affixes = 10;
                    break;
            }

            AffixList = GlobalVariables.RollVsAffix(affixes, ItemType, ItemLevel, SubType);
            
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

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {

            spriteBatch.Begin();

            if (quality == 1)
            {
                spriteBatch.Draw(ItemTexture, new Rectangle(Convert.ToInt32(location.X), Convert.ToInt32(location.Y), ItemTexture.Width, ItemTexture.Height), Color.White);
                RarityColor = Color.White;
            }
            if (quality == 2)
            {
                spriteBatch.Draw(ItemTexture, new Rectangle(Convert.ToInt32(location.X), Convert.ToInt32(location.Y), ItemTexture.Width, ItemTexture.Height), Color.AliceBlue);
                RarityColor = Color.AliceBlue;
            }
            if (quality == 3)
            {
                spriteBatch.Draw(ItemTexture, new Rectangle(Convert.ToInt32(location.X), Convert.ToInt32(location.Y), ItemTexture.Width, ItemTexture.Height), Color.DeepSkyBlue);
                RarityColor = Color.DeepSkyBlue;
            }
            if (quality == 4)
            {
                spriteBatch.Draw(ItemTexture, new Rectangle(Convert.ToInt32(location.X), Convert.ToInt32(location.Y), ItemTexture.Width, ItemTexture.Height), Color.Orange);
                RarityColor = Color.Orange;
            }
            if (quality == 5)
            {
                spriteBatch.Draw(LegendaryBg, new Rectangle(Convert.ToInt32(location.X), Convert.ToInt32(location.Y), ItemTexture.Width, ItemTexture.Height), Color.White);
                spriteBatch.Draw(ItemTexture, new Rectangle(Convert.ToInt32(location.X), Convert.ToInt32(location.Y), ItemTexture.Width, ItemTexture.Height), Color.Purple);
                RarityColor = Color.Purple;
            }
            if (quality == 6)
            {
                spriteBatch.Draw(LegendaryBg, new Rectangle(Convert.ToInt32(location.X), Convert.ToInt32(location.Y), ItemTexture.Width, ItemTexture.Height), Color.White);
                spriteBatch.Draw(ItemTexture, new Rectangle(Convert.ToInt32(location.X), Convert.ToInt32(location.Y), ItemTexture.Width, ItemTexture.Height), Color.Brown);
                RarityColor = Color.Brown;
            }
            

            if (GlobalVariables.ShowItemNames)
            {
                        if (hover)
                        {
                            spriteBatch.DrawString(Font1, ItemName, new Vector2(location.X, (location.Y - 20)), Color.Black);

                            //Draw Shaded backgorund
                            GlobalVariables.WaitToDraw(0, location, new Rectangle(0, 0, TextureBack.Width, 70 + (int)(22 * affixes)), Color.Black, null, TextureBack);

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

                        }
                        else 
                        {
                            spriteBatch.DrawString(Font1, ItemName, new Vector2(location.X, (location.Y - 20)), RarityColor);
                        } 
            }
            spriteBatch.End();
        }
    }
}
