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
    class Item
    {
        #region Variables

        public int type;
        public int Rarity;
        public int quality;
        public int affixes;
        public int affixroll;
        public int affixnumericroll;

        public Boolean hover = false;
        public Boolean invhover = false;

        public Vector2 worldloc;
        public Vector2 location;

        public string status;
        public string affixdesc;
        public string ItemName;
        public string ItemRarity;

        public List<int> affixvaluelist = new List<int>();
        public List<string> affixstatlist = new List<string>();
        public List<string> affixdesclist = new List<string>();

        public SpriteFont Font1;

        public Random RNG = new Random();

        public Texture2D LegBeam;
        public Texture2D ItemTexture;
        public Texture2D LegendaryBg;
        public Texture2D TextBackground;

        public Color RarityColor;

        #endregion

        public Item(Vector2 Location, Texture2D tex, string Status, SpriteFont Font, Texture2D LegendaryBG, Texture2D Legbeam, Texture2D Textbg)
        {
            //Selection of Item Type
            //type = RNG.Next(1-6);

            //set location
            TextBackground = Textbg;
            LegBeam = Legbeam;
            LegendaryBg = LegendaryBG;
            worldloc = Location;
            location = Location;
            status = Status;
            ItemTexture = tex;
            Font1 = Font;

            //Temporary Selection of sword for debug
            type = 1;
            
            //Selection of Item Quality
            quality = RNG.Next(1,101);
            //Designation of Item Types
            if (quality <= 60)
            {
                affixes = 2;
                Rarity = 1;
            }
            if (quality > 60 && quality <= 80)
            {
                affixes = 3;
                Rarity = 2;
            }
            if (quality > 80 && quality <= 95)
            {
                affixes = 4;
                Rarity = 3;
            }
            if (quality > 95 && quality <= 98)
            {
                affixes = 5;
                Rarity = 4;
            }
            if (quality > 98 && quality <= 99)
            {
                affixes = 6;
                Rarity = 5;
            }
            if (quality > 99 )
            {
                affixes = 10;
                Rarity = 6;
            }
            for (int intlc = 0; intlc <= affixes; intlc++)
            {
                //If type is weapon then include case 1 which will be base weapon damage
                if (type == 1)
                {
                ItemName = "Two-Handed Sword";
                affixroll = RNG.Next(1,10);

                switch (affixroll)
                {
                    case 1:
                        affixnumericroll = RNG.Next(1, 4);
                        affixdesc = "Physical Damage Increased by : " + affixnumericroll.ToString();
                        affixstatlist.Add("PHYSDMG");
                        affixvaluelist.Add(affixnumericroll);
                        affixdesclist.Add(affixdesc);
                        break;
                    case 2:
                        affixnumericroll = RNG.Next(1, 4);
                        affixdesc = "Dex Increased by : " + affixnumericroll.ToString();
                        affixstatlist.Add("DEX");
                        affixvaluelist.Add(affixnumericroll);
                        affixdesclist.Add(affixdesc);
                        break;
                    case 3:
                        affixnumericroll = RNG.Next(1, 4);
                        affixdesc = "Str Increased by : " + affixnumericroll.ToString();
                        affixstatlist.Add("STR");
                        affixvaluelist.Add(affixnumericroll);
                        affixdesclist.Add(affixdesc);
                        break;
                    case 4:
                        affixnumericroll = RNG.Next(10, 25);
                        affixdesc = "Dodge Increased by : " + affixnumericroll.ToString();
                        affixstatlist.Add("DODGE");
                        affixvaluelist.Add(affixnumericroll);
                        affixdesclist.Add(affixdesc);
                        break;
                    case 5:
                        affixnumericroll = RNG.Next(25, 250);
                        affixdesc = "Mana Increased by : " + affixnumericroll.ToString();
                        affixstatlist.Add("MANA");
                        affixvaluelist.Add(affixnumericroll);
                        affixdesclist.Add(affixdesc);
                        break;
                    case 6:
                        affixnumericroll = RNG.Next(25, 125);
                        affixdesc = "Health Increased by : " + affixnumericroll.ToString();
                        affixstatlist.Add("HP");
                        affixvaluelist.Add(affixnumericroll);
                        affixdesclist.Add(affixdesc);
                        break;
                    case 7:
                        affixnumericroll = RNG.Next(1, 5);
                        affixdesc = "Armor Increased by : " + affixnumericroll.ToString();
                        affixstatlist.Add("AR");
                        affixvaluelist.Add(affixnumericroll);
                        affixdesclist.Add(affixdesc);
                        break;
                    case 8:
                        affixnumericroll = RNG.Next(1, 10);
                        affixdesc = "Spell Power Increased by : " + affixnumericroll.ToString();
                        affixstatlist.Add("SPLPWR");
                        affixvaluelist.Add(affixnumericroll);
                        affixdesclist.Add(affixdesc);
                        break;
                    case 9:
                        affixnumericroll = RNG.Next(1, 10);
                        affixdesc = "Int Increased by : " + affixnumericroll.ToString();
                        affixstatlist.Add("INT");
                        affixvaluelist.Add(affixnumericroll);
                        affixdesclist.Add(affixdesc);
                        break;
                    case 10:
                        affixnumericroll = RNG.Next(10, 30);
                        affixdesc = "Movement Speed Increased by : " + affixnumericroll.ToString();
                        affixstatlist.Add("MOVE");
                        affixvaluelist.Add(affixnumericroll);
                        affixdesclist.Add(affixdesc);
                        break;
                }
                }
            }
        }

        public void CharMovedRight(GameTime gameTime, Vector2 velocity)
        {
            location -= (velocity * (float)gameTime.ElapsedGameTime.TotalSeconds);
        }

        public void CharMovedLeft(GameTime gameTime, Vector2 velocity)
        {
            location += (velocity * (float)gameTime.ElapsedGameTime.TotalSeconds);
        }

        public void CharMovedUp(GameTime gameTime, Vector2 velocityup)
        {
            location += (velocityup * (float)gameTime.ElapsedGameTime.TotalSeconds);
        }

        public void CharMovedDown(GameTime gameTime, Vector2 velocityup)
        {
            location -= (velocityup * (float)gameTime.ElapsedGameTime.TotalSeconds);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {

            spriteBatch.Begin();

            if (Rarity == 1)
            {
                spriteBatch.Draw(ItemTexture, new Rectangle(Convert.ToInt32(location.X), Convert.ToInt32(location.Y), ItemTexture.Width, ItemTexture.Height), Color.White);
                ItemRarity = "";
                RarityColor = Color.White;
            }
            if (Rarity == 2)
            {
                spriteBatch.Draw(ItemTexture, new Rectangle(Convert.ToInt32(location.X), Convert.ToInt32(location.Y), ItemTexture.Width, ItemTexture.Height), Color.AliceBlue);
                ItemRarity = "Reinforced";
                RarityColor = Color.AliceBlue;
            }
            if (Rarity == 3)
            {
                spriteBatch.Draw(ItemTexture, new Rectangle(Convert.ToInt32(location.X), Convert.ToInt32(location.Y), ItemTexture.Width, ItemTexture.Height), Color.DeepSkyBlue);
                ItemRarity = "Magic";
                RarityColor = Color.DeepSkyBlue;
            }
            if (Rarity == 4)
            {
                spriteBatch.Draw(ItemTexture, new Rectangle(Convert.ToInt32(location.X), Convert.ToInt32(location.Y), ItemTexture.Width, ItemTexture.Height), Color.Orange);
                ItemRarity = "Rare";
                RarityColor = Color.Orange;
            }
            if (Rarity == 5)
            {
                spriteBatch.Draw(LegendaryBg, new Rectangle(Convert.ToInt32(location.X), Convert.ToInt32(location.Y), ItemTexture.Width, ItemTexture.Height), Color.White);
                spriteBatch.Draw(ItemTexture, new Rectangle(Convert.ToInt32(location.X), Convert.ToInt32(location.Y), ItemTexture.Width, ItemTexture.Height), Color.Purple);
                ItemRarity = "Mythic";
                RarityColor = Color.Purple;
            }
            if (Rarity == 6)
            {
                spriteBatch.Draw(LegendaryBg, new Rectangle(Convert.ToInt32(location.X), Convert.ToInt32(location.Y), ItemTexture.Width, ItemTexture.Height), Color.White);
                spriteBatch.Draw(ItemTexture, new Rectangle(Convert.ToInt32(location.X), Convert.ToInt32(location.Y), ItemTexture.Width, ItemTexture.Height), Color.Brown);
                ItemRarity = "Of Legend";
                RarityColor = Color.Brown;
            }
            

            if (GlobalVariables.ShowItemNames)
            {
                if (type == 1)
                {
                    if (Rarity == 1)
                    {
                        if (hover)
                        {
                            spriteBatch.DrawString(Font1, "Sword", new Vector2(location.X, (location.Y - 20)), Color.Black);
                        }
                        else 
                        {
                            spriteBatch.DrawString(Font1, "Sword", new Vector2(location.X, (location.Y - 20)), Color.White);
                        } 
                    }
                    if (Rarity == 2)
                    {
                        if (hover)
                        {
                            spriteBatch.DrawString(Font1, "Reinforced Sword", new Vector2(location.X, (location.Y - 20)), Color.Black);
                        }
                        else
                        {
                            spriteBatch.DrawString(Font1, "Reinforced Sword", new Vector2(location.X, (location.Y - 20)), Color.AliceBlue);
                        } 
                    }
                    if (Rarity == 3)
                    {
                        if (hover)
                        {
                            spriteBatch.DrawString(Font1, "Magic Sword", new Vector2(location.X, (location.Y - 20)), Color.Black);
                        }
                        else
                        {
                            spriteBatch.DrawString(Font1, "Magic Sword", new Vector2(location.X, (location.Y - 20)), Color.DeepSkyBlue);
                        } 
                    }
                    if (Rarity == 4)
                    {
                        if (hover)
                        {
                            spriteBatch.DrawString(Font1, "Rare Sword", new Vector2(location.X, (location.Y - 20)), Color.Black);
                        }
                        else
                        {
                            spriteBatch.DrawString(Font1, "Rare Sword", new Vector2(location.X, (location.Y - 20)), Color.Orange);
                        } 
                    }
                    if (Rarity == 5)
                    {
                        if (hover)
                        {
                            spriteBatch.DrawString(Font1, "Mythic Sword", new Vector2(location.X, (location.Y - 20)), Color.Black);
                        }
                        else
                        {
                            spriteBatch.DrawString(Font1, "Mythic Sword", new Vector2(location.X, (location.Y - 20)), Color.Purple);
                        } 
                    }
                    if (Rarity == 6)
                    {
                        spriteBatch.Draw(LegBeam, new Rectangle(Convert.ToInt32(location.X), Convert.ToInt32(location.Y), Convert.ToInt32((location.X + 100)), Convert.ToInt32((location.Y + 10))), Color.Brown);
                        if (hover)
                        {
                            spriteBatch.DrawString(Font1, "Sword of Legend", new Vector2(location.X, (location.Y - 20)), Color.Black);
                        }
                        else
                        {
                            spriteBatch.DrawString(Font1, "Sword of Legend", new Vector2(location.X, (location.Y - 20)), Color.Brown);
                        } 
                    }
                }
            }
            spriteBatch.End();
        }
    }
}
