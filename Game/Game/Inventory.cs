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
    public class Inventory
    {
        #region Variables
        //Load Items
        public List<Rectangle> InvBoxes = new List<Rectangle>();
        public List<Item> Items = new List<Item>(new Item[] { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null });
        public Color color = Color.White;
        public int ScalarText = 0;
        public Vector2 Box1Start = new Vector2(27, 57);
        public Vector2 BoxRight = new Vector2(52, 0);
        public Vector2 BoxDown = new Vector2(0, 44);
        public MouseState ms;
        public Rectangle Bounds;
        public MouseState oms;
        public Item ClampedItem;
        #endregion

        public Inventory() : this(new List<Item>(),new Vector2(0,0)) { }

        public Inventory(List<Item> ListItems, Vector2 InvPos)
        {

            if (GlobalVariables.TheGame == null)
            {
                Items = null;
                return;
            }

            Bounds = new Rectangle((int)InvPos.X, (int)InvPos.Y, GlobalVariables.TheGame.InvText.Width - 155, GlobalVariables.TheGame.InvText.Height - 240);

            for (int intlc = 0; intlc < ListItems.Count; intlc++)
            {
                for (int intlc1 = 0; intlc1 < Items.Count; intlc1++)
                {
                    if (Items[intlc1] == null)
                    {
                        Items[intlc1] = ListItems[intlc];
                        break;
                    }
                }
            }

            #region SetBoxes
            //Row 1
            InvBoxes.Add(new Rectangle((int)(Box1Start.X + InvPos.X), (int)(Box1Start.Y + InvPos.Y), 45, 30));
            InvBoxes.Add(new Rectangle((int)(Box1Start.X + InvPos.X + BoxRight.X), (int)(Box1Start.Y + InvPos.Y), 45, 30));
            InvBoxes.Add(new Rectangle((int)(Box1Start.X + InvPos.X + (BoxRight.X * 2)), (int)(Box1Start.Y + InvPos.Y), 45, 30));
            InvBoxes.Add(new Rectangle((int)(Box1Start.X + InvPos.X + (BoxRight.X * 3)), (int)(Box1Start.Y + InvPos.Y), 45, 30));

            //Row 2
            InvBoxes.Add(new Rectangle((int)(Box1Start.X + InvPos.X), (int)(Box1Start.Y + InvPos.Y + BoxDown.Y), 45, 30));
            InvBoxes.Add(new Rectangle((int)(Box1Start.X + InvPos.X + BoxRight.X), (int)(Box1Start.Y + InvPos.Y + BoxDown.Y), 45, 30));
            InvBoxes.Add(new Rectangle((int)(Box1Start.X + InvPos.X + (BoxRight.X * 2)), (int)(Box1Start.Y + InvPos.Y + BoxDown.Y), 45, 30));
            InvBoxes.Add(new Rectangle((int)(Box1Start.X + InvPos.X + (BoxRight.X * 3)), (int)(Box1Start.Y + InvPos.Y + BoxDown.Y), 45, 30));

            //Row 3
            InvBoxes.Add(new Rectangle((int)(Box1Start.X + InvPos.X), (int)(Box1Start.Y + InvPos.Y + (BoxDown.Y * 2)), 45, 30));
            InvBoxes.Add(new Rectangle((int)(Box1Start.X + InvPos.X + BoxRight.X), (int)(Box1Start.Y + InvPos.Y + (BoxDown.Y * 2)), 45, 30));
            InvBoxes.Add(new Rectangle((int)(Box1Start.X + InvPos.X + (BoxRight.X * 2)), (int)(Box1Start.Y + InvPos.Y + (BoxDown.Y * 2)), 45, 30));
            InvBoxes.Add(new Rectangle((int)(Box1Start.X + InvPos.X + (BoxRight.X * 3)), (int)(Box1Start.Y + InvPos.Y + (BoxDown.Y * 2)), 45, 30));

            //Row 4
            InvBoxes.Add(new Rectangle((int)(Box1Start.X + InvPos.X), (int)(Box1Start.Y + InvPos.Y + (BoxDown.Y * 3)), 45, 30));
            InvBoxes.Add(new Rectangle((int)(Box1Start.X + InvPos.X + BoxRight.X), (int)(Box1Start.Y + InvPos.Y + (BoxDown.Y * 3)), 45, 30));
            InvBoxes.Add(new Rectangle((int)(Box1Start.X + InvPos.X + (BoxRight.X * 2)), (int)(Box1Start.Y + InvPos.Y + (BoxDown.Y * 3)), 45, 30));
            InvBoxes.Add(new Rectangle((int)(Box1Start.X + InvPos.X + (BoxRight.X * 3)), (int)(Box1Start.Y + InvPos.Y + (BoxDown.Y * 3)), 45, 30));

            //Row 5
            InvBoxes.Add(new Rectangle((int)(Box1Start.X + InvPos.X), (int)(Box1Start.Y + InvPos.Y + (BoxDown.Y * 4)), 45, 30));
            InvBoxes.Add(new Rectangle((int)(Box1Start.X + InvPos.X + BoxRight.X), (int)(Box1Start.Y + InvPos.Y + (BoxDown.Y * 4)), 45, 30));
            InvBoxes.Add(new Rectangle((int)(Box1Start.X + InvPos.X + (BoxRight.X * 2)), (int)(Box1Start.Y + InvPos.Y + (BoxDown.Y * 4)), 45, 30));
            InvBoxes.Add(new Rectangle((int)(Box1Start.X + InvPos.X + (BoxRight.X * 3)), (int)(Box1Start.Y + InvPos.Y + (BoxDown.Y * 4)), 45, 30));

            #endregion
        }

        public int Count()
        {
            int returnValue = 0;
            foreach (Item item in Items)
            {
                if (item != null)
                {
                    returnValue += 1;
                }
            }
            return returnValue;
        }

        public void GrabItem()
        {
            for (int intlc = 0; intlc < Items.Count; intlc++)
            {
                if (Items[intlc] != null)
                {
                    if (Items[intlc].invhover)
                    {
                            ClampedItem = Items[intlc];
                            Items[intlc] = null;
                            GlobalVariables.TheGame.blnClamp = true;
                    }
                }
            }
        }

        public void Update(Vector2 InvPos)
        {
            oms = ms;
            ms = Mouse.GetState();

            //set hover
            for (int intlc = 0; intlc < Items.Count; intlc++)
            {
                if (Items[intlc] != null)
                {
                    if ((ms.X - Items[intlc].location.X) < 50 && (ms.X - Items[intlc].location.X) > 0 && (ms.Y - Items[intlc].location.Y) < 30 && (ms.Y - Items[intlc].location.Y) > 0)
                    {
                        Items[intlc].invhover = true;
                    }
                    else
                    {
                        Items[intlc].invhover = false;
                    }
                }
            }

            Bounds = new Rectangle((int)InvPos.X, (int)InvPos.Y, GlobalVariables.TheGame.InvText.Width - 155, GlobalVariables.TheGame.InvText.Height - 240);

            #region SetBoxes
            //Row 1
            InvBoxes[0] = new Rectangle((int)(Box1Start.X + InvPos.X), (int)(Box1Start.Y + InvPos.Y), 45, 30);
            InvBoxes[1] = new Rectangle((int)(Box1Start.X + InvPos.X + BoxRight.X), (int)(Box1Start.Y + InvPos.Y), 45, 30);
            InvBoxes[2] = new Rectangle((int)(Box1Start.X + InvPos.X + (BoxRight.X * 2)), (int)(Box1Start.Y + InvPos.Y), 45, 30);
            InvBoxes[3] = new Rectangle((int)(Box1Start.X + InvPos.X + (BoxRight.X * 3)), (int)(Box1Start.Y + InvPos.Y), 45, 30);

            //Row 2
            InvBoxes[4] = new Rectangle((int)(Box1Start.X + InvPos.X), (int)(Box1Start.Y + InvPos.Y + BoxDown.Y), 45, 30);
            InvBoxes[5] = new Rectangle((int)(Box1Start.X + InvPos.X + BoxRight.X), (int)(Box1Start.Y + InvPos.Y + BoxDown.Y), 45, 30);
            InvBoxes[6] = new Rectangle((int)(Box1Start.X + InvPos.X + (BoxRight.X * 2)), (int)(Box1Start.Y + InvPos.Y + BoxDown.Y), 45, 30);
            InvBoxes[7] = new Rectangle((int)(Box1Start.X + InvPos.X + (BoxRight.X * 3)), (int)(Box1Start.Y + InvPos.Y + BoxDown.Y), 45, 30);

            //Row 3
            InvBoxes[8] = new Rectangle((int)(Box1Start.X + InvPos.X), (int)(Box1Start.Y + InvPos.Y + (BoxDown.Y * 2)), 45, 30);
            InvBoxes[9] = new Rectangle((int)(Box1Start.X + InvPos.X + BoxRight.X), (int)(Box1Start.Y + InvPos.Y + (BoxDown.Y * 2)), 45, 30);
            InvBoxes[10] = new Rectangle((int)(Box1Start.X + InvPos.X + (BoxRight.X * 2)), (int)(Box1Start.Y + InvPos.Y + (BoxDown.Y * 2)), 45, 30);
            InvBoxes[11] = new Rectangle((int)(Box1Start.X + InvPos.X + (BoxRight.X * 3)), (int)(Box1Start.Y + InvPos.Y + (BoxDown.Y * 2)), 45, 30);

            //Row 4
            InvBoxes[12] = new Rectangle((int)(Box1Start.X + InvPos.X), (int)(Box1Start.Y + InvPos.Y + (BoxDown.Y * 3)), 45, 30);
            InvBoxes[13] = new Rectangle((int)(Box1Start.X + InvPos.X + BoxRight.X), (int)(Box1Start.Y + InvPos.Y + (BoxDown.Y * 3)), 45, 30);
            InvBoxes[14] = new Rectangle((int)(Box1Start.X + InvPos.X + (BoxRight.X * 2)), (int)(Box1Start.Y + InvPos.Y + (BoxDown.Y * 3)), 45, 30);
            InvBoxes[15] = new Rectangle((int)(Box1Start.X + InvPos.X + (BoxRight.X * 3)), (int)(Box1Start.Y + InvPos.Y + (BoxDown.Y * 3)), 45, 30);

            //Row 5
            InvBoxes[16] = new Rectangle((int)(Box1Start.X + InvPos.X), (int)(Box1Start.Y + InvPos.Y + (BoxDown.Y * 4)), 45, 30);
            InvBoxes[17] = new Rectangle((int)(Box1Start.X + InvPos.X + BoxRight.X), (int)(Box1Start.Y + InvPos.Y + (BoxDown.Y * 4)), 45, 30);
            InvBoxes[18] = new Rectangle((int)(Box1Start.X + InvPos.X + (BoxRight.X * 2)), (int)(Box1Start.Y + InvPos.Y + (BoxDown.Y * 4)), 45, 30);
            InvBoxes[19] = new Rectangle((int)(Box1Start.X + InvPos.X + (BoxRight.X * 3)), (int)(Box1Start.Y + InvPos.Y + (BoxDown.Y * 4)), 45, 30);
            #endregion

            GlobalVariables.Inventory = this;
        }

        public void Add(Item item)
        {
            for (int intlc = 0; intlc < Items.Count; intlc++)
            {
                if (Items[intlc] == null)
                {
                    Items[intlc] = item;
                    break;
                }
            }
        }

        public void ItemDropped(Vector2 pos, Item item)
        {
            for (int intlc = 0; intlc < InvBoxes.Count; intlc++)
            {
                if (InvBoxes[intlc].Intersects(new Rectangle((int)pos.X, (int)pos.Y, 1, 1)))
                {
                    if (Items[intlc] != null)
                    {
                        ClampedItem = Items[intlc];
                    }
                    else
                    {
                        //Set bln in Game to stop drawing item at cursor
                        ClampedItem = null;
                        GlobalVariables.TheGame.blnClamp = false;
                    }
                    Items[intlc] = item;
                    break;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (Items.Count > 0)
            {
                for (int i = 0; i < Items.Count; i++)
                {
                    if (Items[i] != null)
                    {
                        if (Items[i].quality == 1)
                        {
                            color = Color.White;
                        }
                        if (Items[i].quality == 2)
                        {
                            color = Color.AliceBlue;
                        }
                        if (Items[i].quality == 3)
                        {
                            color = Color.DeepSkyBlue;
                        }
                        if (Items[i].quality == 4)
                        {
                            color = Color.Orange;
                        }
                        if (Items[i].quality == 5)
                        {
                            color = Color.NavajoWhite;
                        }
                        if (Items[i].quality == 6)
                        {
                            color = Color.White;
                        }

                        if (i == 0)
                        {
                            if (Items[i].quality > 4)
                            {
                                spriteBatch.Draw(Items[i].LegendaryBg, new Rectangle(InvBoxes[0].X, InvBoxes[0].Y, InvBoxes[0].Width, InvBoxes[0].Height - (InvBoxes[0].Height / 2)), new Rectangle(0, 0, Items[i].LegendaryBg.Width, Items[i].LegendaryBg.Height), Color.White);
                            }
                            //Boots
                            if (Items[i].ItemType == 2)
                            {
                                spriteBatch.Draw(Items[i].ItemTexture, new Rectangle(InvBoxes[0].X + (InvBoxes[0].Width / 4), InvBoxes[0].Y, InvBoxes[0].Width / 2, InvBoxes[0].Height - (InvBoxes[0].Height / 2)), new Rectangle(0, 0, Items[i].ItemTexture.Width, Items[i].ItemTexture.Height), color);
                            }
                            //Everything else
                            else
                            {
                                spriteBatch.Draw(Items[i].ItemTexture, new Rectangle(InvBoxes[0].X, InvBoxes[0].Y, InvBoxes[0].Width, InvBoxes[0].Height - (InvBoxes[0].Height / 2)), new Rectangle(0, 0, Items[i].ItemTexture.Width, Items[i].ItemTexture.Height), color);
                            }
                            Items[i].location = new Vector2(InvBoxes[0].X, InvBoxes[0].Y);
                        }
                        if (i == 1)
                        {
                            if (Items[i].quality > 4)
                            {
                                spriteBatch.Draw(Items[i].LegendaryBg, new Rectangle(InvBoxes[1].X, InvBoxes[1].Y, InvBoxes[1].Width, InvBoxes[1].Height - (InvBoxes[1].Height / 2)), new Rectangle(0, 0, Items[i].LegendaryBg.Width, Items[i].LegendaryBg.Height), Color.White);
                            }
                            //Boots
                            if (Items[i].ItemType == 2)
                            {
                                spriteBatch.Draw(Items[i].ItemTexture, new Rectangle(InvBoxes[1].X + (InvBoxes[1].Width / 4), InvBoxes[1].Y, InvBoxes[1].Width / 2, InvBoxes[1].Height - (InvBoxes[1].Height / 2)), new Rectangle(0, 0, Items[i].ItemTexture.Width, Items[i].ItemTexture.Height), color);

                            }
                            //Everything else
                            else
                            {
                                spriteBatch.Draw(Items[i].ItemTexture, new Rectangle(InvBoxes[1].X, InvBoxes[1].Y, InvBoxes[1].Width, InvBoxes[1].Height - (InvBoxes[1].Height / 2)), new Rectangle(0, 0, Items[i].ItemTexture.Width, Items[i].ItemTexture.Height), color);
                            }
                            Items[i].location = new Vector2(InvBoxes[1].X, InvBoxes[1].Y);
                        }
                        if (i == 2)
                        {
                            if (Items[i].quality > 4)
                            {
                                spriteBatch.Draw(Items[i].LegendaryBg, new Rectangle(InvBoxes[2].X, InvBoxes[2].Y, InvBoxes[2].Width, InvBoxes[2].Height - (InvBoxes[2].Height / 2)), new Rectangle(0, 0, Items[i].LegendaryBg.Width, Items[i].LegendaryBg.Height), Color.White);
                            }
                            if (Items[i].ItemType == 2)
                            {
                                spriteBatch.Draw(Items[i].ItemTexture, new Rectangle(InvBoxes[2].X + (InvBoxes[1].Width / 4), InvBoxes[2].Y, InvBoxes[2].Width / 2, InvBoxes[2].Height - (InvBoxes[2].Height / 2)), new Rectangle(0, 0, Items[i].ItemTexture.Width, Items[i].ItemTexture.Height), color);
                            }
                            else
                            {
                                spriteBatch.Draw(Items[i].ItemTexture, new Rectangle(InvBoxes[2].X, InvBoxes[2].Y, InvBoxes[2].Width, InvBoxes[2].Height - (InvBoxes[2].Height / 2)), new Rectangle(0, 0, Items[i].ItemTexture.Width, Items[i].ItemTexture.Height), color);
                            }
                            Items[i].location = new Vector2(InvBoxes[2].X, InvBoxes[2].Y);
                        }
                        if (i == 3)
                        {
                            if (Items[i].quality > 4)
                            {
                                spriteBatch.Draw(Items[i].LegendaryBg, new Rectangle(InvBoxes[3].X, InvBoxes[3].Y, InvBoxes[3].Width, InvBoxes[3].Height - (InvBoxes[3].Height / 2)), new Rectangle(0, 0, Items[i].LegendaryBg.Width, Items[i].LegendaryBg.Height), Color.White);
                            }
                            if (Items[i].ItemType == 2)
                            {
                                spriteBatch.Draw(Items[i].ItemTexture, new Rectangle(InvBoxes[3].X + (InvBoxes[3].Width / 4), InvBoxes[3].Y, InvBoxes[3].Width / 2, InvBoxes[3].Height - (InvBoxes[3].Height / 2)), new Rectangle(0, 0, Items[i].ItemTexture.Width, Items[i].ItemTexture.Height), color);
                            }
                            else
                            {
                                spriteBatch.Draw(Items[i].ItemTexture, new Rectangle(InvBoxes[3].X, InvBoxes[3].Y, InvBoxes[3].Width, InvBoxes[3].Height - (InvBoxes[3].Height / 2)), new Rectangle(0, 0, Items[i].ItemTexture.Width, Items[i].ItemTexture.Height), color);
                            }
                            Items[i].location = new Vector2(InvBoxes[3].X, InvBoxes[3].Y);
                        }

                        //Row 2
                        if (i == 4)
                        {
                            if (Items[i].quality > 4)
                            {
                                spriteBatch.Draw(Items[i].LegendaryBg, new Rectangle(InvBoxes[4].X, InvBoxes[4].Y, InvBoxes[4].Width, InvBoxes[4].Height - (InvBoxes[4].Height / 2)), new Rectangle(0, 0, Items[i].LegendaryBg.Width, Items[i].LegendaryBg.Height), Color.White);
                            }
                            if (Items[i].ItemType == 2)
                            {
                                spriteBatch.Draw(Items[i].ItemTexture, new Rectangle(InvBoxes[4].X + (InvBoxes[4].Width / 4), InvBoxes[4].Y, InvBoxes[4].Width / 2, InvBoxes[4].Height - (InvBoxes[4].Height / 2)), new Rectangle(0, 0, Items[i].ItemTexture.Width, Items[i].ItemTexture.Height), color);
                            }
                            else
                            {
                                spriteBatch.Draw(Items[i].ItemTexture, new Rectangle(InvBoxes[4].X, InvBoxes[4].Y, InvBoxes[4].Width, InvBoxes[4].Height - (InvBoxes[4].Height / 2)), new Rectangle(0, 0, Items[i].ItemTexture.Width, Items[i].ItemTexture.Height), color);
                            }
                            Items[i].location = new Vector2(InvBoxes[4].X, InvBoxes[4].Y);
                        }
                        if (i == 5)
                        {
                            if (Items[i].quality > 4)
                            {
                                spriteBatch.Draw(Items[i].LegendaryBg, new Rectangle(InvBoxes[5].X, InvBoxes[5].Y, InvBoxes[5].Width, InvBoxes[5].Height - (InvBoxes[5].Height / 2)), new Rectangle(0, 0, Items[i].LegendaryBg.Width, Items[i].LegendaryBg.Height), Color.White);
                            }
                            if (Items[i].ItemType == 2)
                            {
                                spriteBatch.Draw(Items[i].ItemTexture, new Rectangle(InvBoxes[5].X + (InvBoxes[5].Width / 4), InvBoxes[5].Y, InvBoxes[5].Width / 2, InvBoxes[5].Height - (InvBoxes[5].Height / 2)), new Rectangle(0, 0, Items[i].ItemTexture.Width, Items[i].ItemTexture.Height), color);
                            }
                            else
                            {
                                spriteBatch.Draw(Items[i].ItemTexture, new Rectangle(InvBoxes[5].X, InvBoxes[5].Y, InvBoxes[5].Width, InvBoxes[5].Height - (InvBoxes[5].Height / 2)), new Rectangle(0, 0, Items[i].ItemTexture.Width, Items[i].ItemTexture.Height), color);
                            }
                            Items[i].location = new Vector2(InvBoxes[5].X, InvBoxes[5].Y);
                        }
                        if (i == 6)
                        {
                            if (Items[i].quality > 4)
                            {
                                spriteBatch.Draw(Items[i].LegendaryBg, new Rectangle(InvBoxes[6].X, InvBoxes[6].Y, InvBoxes[6].Width, InvBoxes[6].Height - (InvBoxes[6].Height / 2)), new Rectangle(0, 0, Items[i].LegendaryBg.Width, Items[i].LegendaryBg.Height), Color.White);
                            }
                            if (Items[i].ItemType == 2)
                            {
                                spriteBatch.Draw(Items[i].ItemTexture, new Rectangle(InvBoxes[6].X + (InvBoxes[6].Width / 4), InvBoxes[6].Y, InvBoxes[6].Width / 2, InvBoxes[6].Height - (InvBoxes[6].Height / 2)), new Rectangle(0, 0, Items[i].ItemTexture.Width, Items[i].ItemTexture.Height), color);
                            }
                            else
                            {
                                spriteBatch.Draw(Items[i].ItemTexture, new Rectangle(InvBoxes[6].X, InvBoxes[6].Y, InvBoxes[6].Width, InvBoxes[6].Height - (InvBoxes[6].Height / 2)), new Rectangle(0, 0, Items[i].ItemTexture.Width, Items[i].ItemTexture.Height), color);
                            }
                            Items[i].location = new Vector2(InvBoxes[6].X, InvBoxes[6].Y);
                        }
                        if (i == 7)
                        {
                            if (Items[i].quality > 4)
                            {
                                spriteBatch.Draw(Items[i].LegendaryBg, new Rectangle(InvBoxes[7].X, InvBoxes[7].Y, InvBoxes[7].Width, InvBoxes[7].Height - (InvBoxes[7].Height / 2)), new Rectangle(0, 0, Items[i].LegendaryBg.Width, Items[i].LegendaryBg.Height), Color.White);
                            }
                            if (Items[i].ItemType == 2)
                            {
                                spriteBatch.Draw(Items[i].ItemTexture, new Rectangle(InvBoxes[7].X + (InvBoxes[7].Width / 4), InvBoxes[7].Y, InvBoxes[7].Width / 2, InvBoxes[7].Height - (InvBoxes[7].Height / 2)), new Rectangle(0, 0, Items[i].ItemTexture.Width, Items[i].ItemTexture.Height), color);
                            }
                            else
                            {
                                spriteBatch.Draw(Items[i].ItemTexture, new Rectangle(InvBoxes[7].X, InvBoxes[7].Y, InvBoxes[7].Width, InvBoxes[7].Height - (InvBoxes[7].Height / 2)), new Rectangle(0, 0, Items[i].ItemTexture.Width, Items[i].ItemTexture.Height), color);
                            }
                            Items[i].location = new Vector2(InvBoxes[7].X, InvBoxes[7].Y);
                        }

                        //Row 3
                        if (i == 8)
                        {
                            if (Items[i].quality > 4)
                            {
                                spriteBatch.Draw(Items[i].LegendaryBg, new Rectangle(InvBoxes[8].X, InvBoxes[8].Y, InvBoxes[8].Width, InvBoxes[8].Height - (InvBoxes[8].Height / 2)), new Rectangle(0, 0, Items[i].LegendaryBg.Width, Items[i].LegendaryBg.Height), Color.White);
                            }
                            if (Items[i].ItemType == 2)
                            {
                                spriteBatch.Draw(Items[i].ItemTexture, new Rectangle(InvBoxes[8].X + (InvBoxes[8].Width / 4), InvBoxes[8].Y, InvBoxes[8].Width / 2, InvBoxes[8].Height - (InvBoxes[8].Height / 2)), new Rectangle(0, 0, Items[i].ItemTexture.Width, Items[i].ItemTexture.Height), color);
                            }
                            else
                            {
                                spriteBatch.Draw(Items[i].ItemTexture, new Rectangle(InvBoxes[8].X, InvBoxes[8].Y, InvBoxes[8].Width, InvBoxes[8].Height - (InvBoxes[8].Height / 2)), new Rectangle(0, 0, Items[i].ItemTexture.Width, Items[i].ItemTexture.Height), color);
                            }
                            Items[i].location = new Vector2(InvBoxes[8].X, InvBoxes[8].Y);
                        }
                        if (i == 9)
                        {
                            if (Items[i].quality > 4)
                            {
                                spriteBatch.Draw(Items[i].LegendaryBg, new Rectangle(InvBoxes[9].X, InvBoxes[9].Y, InvBoxes[9].Width, InvBoxes[9].Height - (InvBoxes[9].Height / 2)), new Rectangle(0, 0, Items[i].LegendaryBg.Width, Items[i].LegendaryBg.Height), Color.White);
                            }
                            if (Items[i].ItemType == 2)
                            {
                                spriteBatch.Draw(Items[i].ItemTexture, new Rectangle(InvBoxes[9].X + (InvBoxes[9].Width / 4), InvBoxes[9].Y, InvBoxes[9].Width / 2, InvBoxes[9].Height - (InvBoxes[9].Height / 2)), new Rectangle(0, 0, Items[i].ItemTexture.Width, Items[i].ItemTexture.Height), color);
                            }
                            else
                            {
                                spriteBatch.Draw(Items[i].ItemTexture, new Rectangle(InvBoxes[9].X, InvBoxes[9].Y, InvBoxes[9].Width, InvBoxes[9].Height - (InvBoxes[9].Height / 2)), new Rectangle(0, 0, Items[i].ItemTexture.Width, Items[i].ItemTexture.Height), color);
                            }
                            Items[i].location = new Vector2(InvBoxes[9].X, InvBoxes[9].Y);
                        }
                        if (i == 10)
                        {
                            if (Items[i].quality > 4)
                            {
                                spriteBatch.Draw(Items[i].LegendaryBg, new Rectangle(InvBoxes[10].X, InvBoxes[10].Y, InvBoxes[10].Width, InvBoxes[10].Height - (InvBoxes[10].Height / 2)), new Rectangle(0, 0, Items[i].LegendaryBg.Width, Items[i].LegendaryBg.Height), Color.White);
                            }
                            if (Items[i].ItemType == 2)
                            {
                                spriteBatch.Draw(Items[i].ItemTexture, new Rectangle(InvBoxes[10].X + (InvBoxes[10].Width / 4), InvBoxes[10].Y, InvBoxes[10].Width / 2, InvBoxes[10].Height - (InvBoxes[10].Height / 2)), new Rectangle(0, 0, Items[i].ItemTexture.Width, Items[i].ItemTexture.Height), color);
                            }
                            else
                            {
                                spriteBatch.Draw(Items[i].ItemTexture, new Rectangle(InvBoxes[10].X, InvBoxes[10].Y, InvBoxes[10].Width, InvBoxes[10].Height - (InvBoxes[10].Height / 2)), new Rectangle(0, 0, Items[i].ItemTexture.Width, Items[i].ItemTexture.Height), color);
                            }
                            Items[i].location = new Vector2(InvBoxes[10].X, InvBoxes[10].Y);
                        }
                        if (i == 11)
                        {
                            if (Items[i].quality > 4)
                            {
                                spriteBatch.Draw(Items[i].LegendaryBg, new Rectangle(InvBoxes[11].X, InvBoxes[11].Y, InvBoxes[11].Width, InvBoxes[11].Height - (InvBoxes[11].Height / 2)), new Rectangle(0, 0, Items[i].LegendaryBg.Width, Items[i].LegendaryBg.Height), Color.White);
                            }
                            if (Items[i].ItemType == 2)
                            {
                                spriteBatch.Draw(Items[i].ItemTexture, new Rectangle(InvBoxes[11].X + (InvBoxes[11].Width / 4), InvBoxes[11].Y, InvBoxes[11].Width / 2, InvBoxes[11].Height - (InvBoxes[11].Height / 2)), new Rectangle(0, 0, Items[i].ItemTexture.Width, Items[i].ItemTexture.Height), color);
                            }
                            else
                            {
                                spriteBatch.Draw(Items[i].ItemTexture, new Rectangle(InvBoxes[11].X, InvBoxes[11].Y, InvBoxes[11].Width, InvBoxes[11].Height - (InvBoxes[11].Height / 2)), new Rectangle(0, 0, Items[i].ItemTexture.Width, Items[i].ItemTexture.Height), color);
                            }
                            Items[i].location = new Vector2(InvBoxes[11].X, InvBoxes[11].Y);
                        }

                        //Row 4
                        if (i == 12)
                        {
                            if (Items[i].quality > 4)
                            {
                                spriteBatch.Draw(Items[i].LegendaryBg, new Rectangle(InvBoxes[12].X, InvBoxes[12].Y, InvBoxes[12].Width, InvBoxes[12].Height - (InvBoxes[12].Height / 2)), new Rectangle(0, 0, Items[i].LegendaryBg.Width, Items[i].LegendaryBg.Height), Color.White);
                            }
                            if (Items[i].ItemType == 2)
                            {
                                spriteBatch.Draw(Items[i].ItemTexture, new Rectangle(InvBoxes[12].X + (InvBoxes[12].Width / 4), InvBoxes[12].Y, InvBoxes[12].Width / 2, InvBoxes[12].Height - (InvBoxes[12].Height / 2)), new Rectangle(0, 0, Items[i].ItemTexture.Width, Items[i].ItemTexture.Height), color);
                            }
                            else
                            {
                                spriteBatch.Draw(Items[i].ItemTexture, new Rectangle(InvBoxes[12].X, InvBoxes[12].Y, InvBoxes[12].Width, InvBoxes[12].Height - (InvBoxes[12].Height / 2)), new Rectangle(0, 0, Items[i].ItemTexture.Width, Items[i].ItemTexture.Height), color);
                            }
                            Items[i].location = new Vector2(InvBoxes[12].X, InvBoxes[12].Y);
                        }
                        if (i == 13)
                        {
                            if (Items[i].quality > 4)
                            {
                                spriteBatch.Draw(Items[i].LegendaryBg, new Rectangle(InvBoxes[13].X, InvBoxes[13].Y, InvBoxes[13].Width, InvBoxes[13].Height - (InvBoxes[13].Height / 2)), new Rectangle(0, 0, Items[i].LegendaryBg.Width, Items[i].LegendaryBg.Height), Color.White);
                            }
                            if (Items[i].ItemType == 2)
                            {
                                spriteBatch.Draw(Items[i].ItemTexture, new Rectangle(InvBoxes[13].X + (InvBoxes[13].Width / 4), InvBoxes[13].Y, InvBoxes[13].Width / 2, InvBoxes[13].Height - (InvBoxes[13].Height / 2)), new Rectangle(0, 0, Items[i].ItemTexture.Width, Items[i].ItemTexture.Height), color);
                            }
                            else
                            {
                                spriteBatch.Draw(Items[i].ItemTexture, new Rectangle(InvBoxes[13].X, InvBoxes[13].Y, InvBoxes[13].Width, InvBoxes[13].Height - (InvBoxes[13].Height / 2)), new Rectangle(0, 0, Items[i].ItemTexture.Width, Items[i].ItemTexture.Height), color);
                            }
                            Items[i].location = new Vector2(InvBoxes[13].X, InvBoxes[13].Y);
                        }
                        if (i == 14)
                        {
                            if (Items[i].quality > 4)
                            {
                                spriteBatch.Draw(Items[i].LegendaryBg, new Rectangle(InvBoxes[14].X, InvBoxes[14].Y, InvBoxes[14].Width, InvBoxes[14].Height - (InvBoxes[14].Height / 2)), new Rectangle(0, 0, Items[i].LegendaryBg.Width, Items[i].LegendaryBg.Height), Color.White);
                            }
                            if (Items[i].ItemType == 2)
                            {
                                spriteBatch.Draw(Items[i].ItemTexture, new Rectangle(InvBoxes[14].X + (InvBoxes[14].Width / 4), InvBoxes[14].Y, InvBoxes[14].Width / 2, InvBoxes[14].Height - (InvBoxes[14].Height / 2)), new Rectangle(0, 0, Items[i].ItemTexture.Width, Items[i].ItemTexture.Height), color);
                            }
                            else
                            {
                                spriteBatch.Draw(Items[i].ItemTexture, new Rectangle(InvBoxes[14].X, InvBoxes[14].Y, InvBoxes[14].Width, InvBoxes[14].Height - (InvBoxes[14].Height / 2)), new Rectangle(0, 0, Items[i].ItemTexture.Width, Items[i].ItemTexture.Height), color);
                            }
                            Items[i].location = new Vector2(InvBoxes[14].X, InvBoxes[14].Y);
                        }
                        if (i == 15)
                        {
                            if (Items[i].quality > 4)
                            {
                                spriteBatch.Draw(Items[i].LegendaryBg, new Rectangle(InvBoxes[15].X, InvBoxes[15].Y, InvBoxes[15].Width, InvBoxes[15].Height - (InvBoxes[15].Height / 2)), new Rectangle(0, 0, Items[i].LegendaryBg.Width, Items[i].LegendaryBg.Height), Color.White);
                            }
                            if (Items[i].ItemType == 2)
                            {
                                spriteBatch.Draw(Items[i].ItemTexture, new Rectangle(InvBoxes[15].X + (InvBoxes[15].Width / 4), InvBoxes[15].Y, InvBoxes[15].Width / 2, InvBoxes[15].Height - (InvBoxes[15].Height / 2)), new Rectangle(0, 0, Items[i].ItemTexture.Width, Items[i].ItemTexture.Height), color);
                            }
                            else
                            {
                                spriteBatch.Draw(Items[i].ItemTexture, new Rectangle(InvBoxes[15].X, InvBoxes[15].Y, InvBoxes[15].Width, InvBoxes[15].Height - (InvBoxes[15].Height / 2)), new Rectangle(0, 0, Items[i].ItemTexture.Width, Items[i].ItemTexture.Height), color);
                            }
                            Items[i].location = new Vector2(InvBoxes[15].X, InvBoxes[15].Y);
                        }

                        //Row 5
                        if (i == 16)
                        {
                            if (Items[i].quality > 4)
                            {
                                spriteBatch.Draw(Items[i].LegendaryBg, new Rectangle(InvBoxes[16].X, InvBoxes[16].Y, InvBoxes[16].Width, InvBoxes[16].Height - (InvBoxes[16].Height / 2)), new Rectangle(0, 0, Items[i].LegendaryBg.Width, Items[i].LegendaryBg.Height), Color.White);
                            }
                            if (Items[i].ItemType == 2)
                            {
                                spriteBatch.Draw(Items[i].ItemTexture, new Rectangle(InvBoxes[16].X + (InvBoxes[16].Width / 4), InvBoxes[16].Y, InvBoxes[16].Width / 2, InvBoxes[16].Height - (InvBoxes[16].Height / 2)), new Rectangle(0, 0, Items[i].ItemTexture.Width, Items[i].ItemTexture.Height), color);
                            }
                            else
                            {
                                spriteBatch.Draw(Items[i].ItemTexture, new Rectangle(InvBoxes[16].X, InvBoxes[16].Y, InvBoxes[16].Width, InvBoxes[16].Height - (InvBoxes[16].Height / 2)), new Rectangle(0, 0, Items[i].ItemTexture.Width, Items[i].ItemTexture.Height), color);
                            }
                            Items[i].location = new Vector2(InvBoxes[16].X, InvBoxes[16].Y);
                        }
                        if (i == 17)
                        {
                            if (Items[i].quality > 4)
                            {
                                spriteBatch.Draw(Items[i].LegendaryBg, new Rectangle(InvBoxes[17].X, InvBoxes[17].Y, InvBoxes[17].Width, InvBoxes[17].Height - (InvBoxes[17].Height / 2)), new Rectangle(0, 0, Items[i].LegendaryBg.Width, Items[i].LegendaryBg.Height), Color.White);
                            }
                            if (Items[i].ItemType == 2)
                            {
                                spriteBatch.Draw(Items[i].ItemTexture, new Rectangle(InvBoxes[17].X + (InvBoxes[17].Width / 4), InvBoxes[17].Y, InvBoxes[17].Width / 2, InvBoxes[17].Height - (InvBoxes[17].Height / 2)), new Rectangle(0, 0, Items[i].ItemTexture.Width, Items[i].ItemTexture.Height), color);
                            }
                            else
                            {
                                spriteBatch.Draw(Items[i].ItemTexture, new Rectangle(InvBoxes[17].X, InvBoxes[17].Y, InvBoxes[17].Width, InvBoxes[17].Height - (InvBoxes[17].Height / 2)), new Rectangle(0, 0, Items[i].ItemTexture.Width, Items[i].ItemTexture.Height), color);
                            }
                            Items[i].location = new Vector2(InvBoxes[17].X, InvBoxes[17].Y);
                        }
                        if (i == 18)
                        {
                            if (Items[i].quality > 4)
                            {
                                spriteBatch.Draw(Items[i].LegendaryBg, new Rectangle(InvBoxes[18].X, InvBoxes[18].Y, InvBoxes[18].Width, InvBoxes[18].Height - (InvBoxes[18].Height / 2)), new Rectangle(0, 0, Items[i].LegendaryBg.Width, Items[i].LegendaryBg.Height), Color.White);
                            }
                            if (Items[i].ItemType == 2)
                            {
                                spriteBatch.Draw(Items[i].ItemTexture, new Rectangle(InvBoxes[18].X + (InvBoxes[18].Width / 4), InvBoxes[18].Y, InvBoxes[18].Width / 2, InvBoxes[18].Height - (InvBoxes[18].Height / 2)), new Rectangle(0, 0, Items[i].ItemTexture.Width, Items[i].ItemTexture.Height), color);
                            }
                            else
                            {
                                spriteBatch.Draw(Items[i].ItemTexture, new Rectangle(InvBoxes[18].X, InvBoxes[18].Y, InvBoxes[18].Width, InvBoxes[18].Height - (InvBoxes[18].Height / 2)), new Rectangle(0, 0, Items[i].ItemTexture.Width, Items[i].ItemTexture.Height), color);
                            }
                            Items[i].location = new Vector2(InvBoxes[18].X, InvBoxes[18].Y);
                        }
                        if (i == 19)
                        {
                            if (Items[i].quality > 4)
                            {
                                spriteBatch.Draw(Items[i].LegendaryBg, new Rectangle(InvBoxes[19].X, InvBoxes[19].Y, InvBoxes[19].Width, InvBoxes[19].Height - (InvBoxes[19].Height / 2)), new Rectangle(0, 0, Items[i].LegendaryBg.Width, Items[i].LegendaryBg.Height), Color.White);
                            }
                            if (Items[i].ItemType == 2)
                            {
                                spriteBatch.Draw(Items[i].ItemTexture, new Rectangle(InvBoxes[19].X + (InvBoxes[19].Width / 4), InvBoxes[19].Y, InvBoxes[19].Width / 2, InvBoxes[19].Height - (InvBoxes[19].Height / 2)), new Rectangle(0, 0, Items[i].ItemTexture.Width, Items[i].ItemTexture.Height), color);
                            }
                            else
                            {
                                spriteBatch.Draw(Items[i].ItemTexture, new Rectangle(InvBoxes[19].X, InvBoxes[19].Y, InvBoxes[19].Width, InvBoxes[19].Height - (InvBoxes[19].Height / 2)), new Rectangle(0, 0, Items[i].ItemTexture.Width, Items[i].ItemTexture.Height), color);
                            }
                            Items[i].location = new Vector2(InvBoxes[19].X, InvBoxes[19].Y);
                        }

                        if (Items[i].invhover)
                        {

                            //Initialize Scalar Value
                            ScalarText = 20;

                            int widestString = 0;
                            foreach (Affix afx in Items[i].AffixList)
                            {
                                double textWidth1 = Items[i].Font1.MeasureString(afx.Desc).X;
                                if (textWidth1 > widestString)
                                {
                                    widestString = (int)textWidth1;
                                }
                            }

                            //Store location
                            Vector2 loc = Items[i].location;

                            List<string> ItemDesc = new List<string>();

                            if (Items[i].ItemType == 1)
                            {
                                ItemDesc = GlobalVariables.GenerateDescList(Items[i].ItemDescription, widestString + (Items[i].ItemTexture.Width * .9), Items[i].Font1);
                            }
                            else if (Items[i].ItemType == 2)
                            {
                                ItemDesc = GlobalVariables.GenerateDescList(Items[i].ItemDescription, widestString + (Items[i].ItemTexture.Width * .4), Items[i].Font1);
                            }

                            //check to render on screen
                            Items[i].location = GlobalVariables.newLocation(Items[i].location, widestString + 40, 70 + (int)(22 * Items[i].affixes) + (int)(22 * ItemDesc.Count));

                            //Draw Shaded backgorund
                            GlobalVariables.WaitToDraw(0, new Vector2((Items[i].location.X + 80), (Items[i].location.Y)), new Rectangle(0, 0, widestString + 40 + (int)(Items[i].ItemTexture.Width * .75), 70 + (int)(22 * Items[i].affixes) + (int)(22 * ItemDesc.Count)), Color.Black, null, GlobalVariables.TestSquare);

                            GlobalVariables.WaitToDraw(1, new Vector2((Items[i].location.X + 100), (Items[i].location.Y + 30)), new Rectangle(0, 0, 0, 0), Items[i].RarityColor, Items[i].Font1, null, Items[i].ItemName);

                            //Draw small item in top right
                            if (Items[i].ItemType == 1)
                            {
                                GlobalVariables.WaitToDraw(0, new Vector2((Items[i].location.X + 175 + (widestString - Items[i].ItemTexture.Width)), Items[i].location.Y + 25), new Rectangle(0, 0, Items[i].ItemTexture.Width, Items[i].ItemTexture.Height), Items[i].ItemColor, null, Items[i].ItemTexture);
                            }
                            else if (Items[i].ItemType == 2)
                            {
                                GlobalVariables.WaitToDraw(0, new Vector2((Items[i].location.X + 115 + (widestString - Items[i].ItemTexture.Width)), Items[i].location.Y + 25), new Rectangle(0, 0, Items[i].ItemTexture.Width, Items[i].ItemTexture.Height), Items[i].ItemColor, null, Items[i].ItemTexture);
                            }

                            //Draw Item aFfixes
                            for (int intlc = 0; intlc < Items[i].AffixList.Count; intlc++)
                            {
                                if (intlc < 4)
                                {
                                    GlobalVariables.WaitToDraw(1, new Vector2((Items[i].location.X + 100), (Items[i].location.Y + ScalarText + 50)), new Rectangle(0, 0, 0, 0), Color.White, Items[i].Font1, null, Items[i].AffixList[intlc].Desc);
                                    ScalarText += 20;
                                }
                                if (intlc == 4)
                                {
                                    GlobalVariables.WaitToDraw(1, new Vector2((Items[i].location.X + 100), (Items[i].location.Y + ScalarText + 50)), new Rectangle(0, 0, 0, 0), Color.Orange, Items[i].Font1, null, Items[i].AffixList[intlc].Desc);
                                    ScalarText += 20;
                                }
                                if (intlc == 5)
                                {
                                    GlobalVariables.WaitToDraw(1, new Vector2((Items[i].location.X + 100), (Items[i].location.Y + ScalarText + 50)), new Rectangle(0, 0, 0, 0), Color.Purple, Items[i].Font1, null, Items[i].AffixList[intlc].Desc);
                                    ScalarText += 20;
                                }
                                if (intlc > 5)
                                {
                                    GlobalVariables.WaitToDraw(1, new Vector2((Items[i].location.X + 100), (Items[i].location.Y + ScalarText + 50)), new Rectangle(0, 0, 0, 0), Color.Brown, Items[i].Font1, null, Items[i].AffixList[intlc].Desc);
                                    ScalarText += 20;
                                }
                            }
                            foreach (string str in ItemDesc)
                            {
                                GlobalVariables.WaitToDraw(1, new Vector2((Items[i].location.X + 100), (Items[i].location.Y + ScalarText + 50)), new Rectangle(0, 0, 0, 0), Color.Gray, Items[i].Font1, null, str);

                                ScalarText += 20;
                            }

                            Items[i].location = loc;
                        }
                    }
                }
            }
        }

    }
}
