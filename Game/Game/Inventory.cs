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
    class Inventory
    {
        #region Variables
        //Load Items
        public List<Vector2> InvBoxes = new List<Vector2>();
        public List<Item> Items = new List<Item>();
        public Color color = Color.White;
        public int ScalarText = 0;
        public Vector2 Box1Start = new Vector2(27,57);
        public Vector2 BoxRight = new Vector2(52, 0);
        public Vector2 BoxDown = new Vector2(0, 44);
        #endregion
        public Inventory(List<Item> ListItems, Vector2 InvPos)
        {
            Items = ListItems;

            #region SetBoxes
            InvBoxes.Add((Box1Start) + new Vector2(InvPos.X,InvPos.Y));
            InvBoxes.Add((Box1Start) + new Vector2(InvPos.X, InvPos.Y) + BoxRight);
            InvBoxes.Add((Box1Start) + new Vector2(InvPos.X, InvPos.Y) + (BoxRight * 2));
            InvBoxes.Add((Box1Start) + new Vector2(InvPos.X, InvPos.Y) + (BoxRight * 3));
            InvBoxes.Add((Box1Start) + BoxDown +  new Vector2(InvPos.X, InvPos.Y));
            InvBoxes.Add((Box1Start) + BoxDown + new Vector2(InvPos.X, InvPos.Y) + BoxRight);
            InvBoxes.Add((Box1Start) + BoxDown + new Vector2(InvPos.X, InvPos.Y) + (BoxRight * 2));
            InvBoxes.Add((Box1Start) + BoxDown + new Vector2(InvPos.X, InvPos.Y) + (BoxRight * 3));
            InvBoxes.Add((Box1Start) + (BoxDown * 2) + new Vector2(InvPos.X, InvPos.Y));
            InvBoxes.Add((Box1Start) + (BoxDown * 2) + new Vector2(InvPos.X, InvPos.Y) + BoxRight);
            InvBoxes.Add((Box1Start) + (BoxDown * 2) + new Vector2(InvPos.X, InvPos.Y) + (BoxRight * 2));
            InvBoxes.Add((Box1Start) + (BoxDown * 2) + new Vector2(InvPos.X, InvPos.Y) + (BoxRight * 3));
            InvBoxes.Add((Box1Start) + (BoxDown * 3) + new Vector2(InvPos.X, InvPos.Y));
            InvBoxes.Add((Box1Start) + (BoxDown * 3) + new Vector2(InvPos.X, InvPos.Y) + BoxRight);
            InvBoxes.Add((Box1Start) + (BoxDown * 3) + new Vector2(InvPos.X, InvPos.Y) + (BoxRight * 2));
            InvBoxes.Add((Box1Start) + (BoxDown * 3) + new Vector2(InvPos.X, InvPos.Y) + (BoxRight * 3));
            InvBoxes.Add((Box1Start) + (BoxDown * 4) + new Vector2(InvPos.X, InvPos.Y));
            InvBoxes.Add((Box1Start) + (BoxDown * 4) + new Vector2(InvPos.X, InvPos.Y) + BoxRight);
            InvBoxes.Add((Box1Start) + (BoxDown * 4) + new Vector2(InvPos.X, InvPos.Y) + (BoxRight * 2));
            InvBoxes.Add((Box1Start) + (BoxDown * 4) + new Vector2(InvPos.X, InvPos.Y) + (BoxRight * 3));
            #endregion
        }

        public void Update(Vector2 InvPos)
        {
            #region SetBoxes
            InvBoxes[0] = (Box1Start) + new Vector2(InvPos.X, InvPos.Y);
            InvBoxes[1] =(Box1Start) + new Vector2(InvPos.X, InvPos.Y) + BoxRight;
            InvBoxes[2] =(Box1Start) + new Vector2(InvPos.X, InvPos.Y) + (BoxRight * 2);
            InvBoxes[3] =(Box1Start) + new Vector2(InvPos.X, InvPos.Y) + (BoxRight * 3);
            InvBoxes[4] =(Box1Start) + BoxDown + new Vector2(InvPos.X, InvPos.Y);
            InvBoxes[5] =(Box1Start) + BoxDown + new Vector2(InvPos.X, InvPos.Y) + BoxRight;
            InvBoxes[6] =(Box1Start) + BoxDown + new Vector2(InvPos.X, InvPos.Y) + (BoxRight * 2);
            InvBoxes[7] =(Box1Start) + BoxDown + new Vector2(InvPos.X, InvPos.Y) + (BoxRight * 3);
            InvBoxes[8] =(Box1Start) + (BoxDown * 2) + new Vector2(InvPos.X, InvPos.Y);
            InvBoxes[9] =(Box1Start) + (BoxDown * 2) + new Vector2(InvPos.X, InvPos.Y) + BoxRight;
            InvBoxes[10] =(Box1Start) + (BoxDown * 2) + new Vector2(InvPos.X, InvPos.Y) + (BoxRight * 2);
            InvBoxes[11] =(Box1Start) + (BoxDown * 2) + new Vector2(InvPos.X, InvPos.Y) + (BoxRight * 3);
            InvBoxes[12] =(Box1Start) + (BoxDown * 3) + new Vector2(InvPos.X, InvPos.Y);
            InvBoxes[13] =(Box1Start) + (BoxDown * 3) + new Vector2(InvPos.X, InvPos.Y) + BoxRight;
            InvBoxes[14] =(Box1Start) + (BoxDown * 3) + new Vector2(InvPos.X, InvPos.Y) + (BoxRight * 2);
            InvBoxes[15] =(Box1Start) + (BoxDown * 3) + new Vector2(InvPos.X, InvPos.Y) + (BoxRight * 3);
            InvBoxes[16] =(Box1Start) + (BoxDown * 4) + new Vector2(InvPos.X, InvPos.Y);
            InvBoxes[17] =(Box1Start) + (BoxDown * 4) + new Vector2(InvPos.X, InvPos.Y) + BoxRight;
            InvBoxes[18] =(Box1Start) + (BoxDown * 4) + new Vector2(InvPos.X, InvPos.Y) + (BoxRight * 2);
            InvBoxes[19] =(Box1Start) + (BoxDown * 4) + new Vector2(InvPos.X, InvPos.Y) + (BoxRight * 3);
            #endregion
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (Items.Count > 0)
            {
                for (int i = 0; i < Items.Count; i++)
                {
                    if (Items[i].Rarity == 1)
                    {
                        color = Color.White;
                    }
                    if (Items[i].Rarity == 2)
                    {
                        color = Color.AliceBlue;
                    }
                    if (Items[i].Rarity == 3)
                    {
                        color = Color.DeepSkyBlue;
                    }
                    if (Items[i].Rarity == 4)
                    {
                        color = Color.Orange;
                    }
                    if (Items[i].Rarity == 5)
                    {
                        color = Color.Purple;
                    }
                    if (Items[i].Rarity == 6)
                    {
                        color = Color.Brown;
                    }

#region Inventory
                    //Row 1
                    if (i == 0)
                    {
                        if (Items[i].Rarity > 4)
                        {
                            spriteBatch.Draw(Items[i].LegendaryBg, new Rectangle((int)InvBoxes[0].X,(int)InvBoxes[0].Y,45,30), new Rectangle(0, 0, Items[i].LegendaryBg.Width, Items[i].LegendaryBg.Height), Color.White);
                        }
                        spriteBatch.Draw(Items[i].ItemTexture, new Rectangle((int)InvBoxes[0].X, (int)InvBoxes[0].Y, 45, 30), new Rectangle(0, 0, Items[i].ItemTexture.Width, Items[i].ItemTexture.Height), color);
                        Items[i].location = new Vector2(InvBoxes[0].X,InvBoxes[0].Y);
                    }
                    if (i == 1)
                    {
                        if (Items[i].Rarity > 4)
                        {
                            spriteBatch.Draw(Items[i].LegendaryBg, new Rectangle((int)InvBoxes[1].X, (int)InvBoxes[1].Y, 45, 30), new Rectangle(0, 0, Items[i].LegendaryBg.Width, Items[i].LegendaryBg.Height), Color.White);
                        }
                        spriteBatch.Draw(Items[i].ItemTexture, new Rectangle((int)InvBoxes[1].X, (int)InvBoxes[1].Y, 45, 30), new Rectangle(0, 0, Items[i].ItemTexture.Width, Items[i].ItemTexture.Height), color);
                        Items[i].location = new Vector2(InvBoxes[1].X, InvBoxes[1].Y);
                    }
                    if (i == 2)
                    {
                        if (Items[i].Rarity > 4)
                        {
                            spriteBatch.Draw(Items[i].LegendaryBg, new Rectangle((int)InvBoxes[2].X, (int)InvBoxes[2].Y, 45, 30), new Rectangle(0, 0, Items[i].LegendaryBg.Width, Items[i].LegendaryBg.Height), Color.White);
                        }
                        spriteBatch.Draw(Items[i].ItemTexture, new Rectangle((int)InvBoxes[2].X, (int)InvBoxes[2].Y, 45, 30), new Rectangle(0, 0, Items[i].ItemTexture.Width, Items[i].ItemTexture.Height), color);
                        Items[i].location = new Vector2(InvBoxes[2].X, InvBoxes[2].Y);
                    }
                    if (i == 3)
                    {
                        if (Items[i].Rarity > 4)
                        {
                            spriteBatch.Draw(Items[i].LegendaryBg, new Rectangle((int)InvBoxes[3].X, (int)InvBoxes[3].Y, 45, 30), new Rectangle(0, 0, Items[i].LegendaryBg.Width, Items[i].LegendaryBg.Height), Color.White);
                        }
                        spriteBatch.Draw(Items[i].ItemTexture, new Rectangle((int)InvBoxes[3].X, (int)InvBoxes[3].Y, 45, 30), new Rectangle(0, 0, Items[i].ItemTexture.Width, Items[i].ItemTexture.Height), color);
                        Items[i].location = new Vector2(InvBoxes[3].X, InvBoxes[3].Y);
                    }

                    //Row 2
                    if (i == 4)
                    {
                        if (Items[i].Rarity > 4)
                        {
                            spriteBatch.Draw(Items[i].LegendaryBg, new Rectangle((int)InvBoxes[4].X, (int)InvBoxes[4].Y, 45, 30), new Rectangle(0, 0, Items[i].LegendaryBg.Width, Items[i].LegendaryBg.Height), Color.White);
                        }
                        spriteBatch.Draw(Items[i].ItemTexture, new Rectangle((int)InvBoxes[4].X, (int)InvBoxes[4].Y, 45, 30), new Rectangle(0, 0, Items[i].ItemTexture.Width, Items[i].ItemTexture.Height), color);
                        Items[i].location = new Vector2(InvBoxes[4].X, InvBoxes[4].Y);
                    }
                    if (i == 5)
                    {
                        if (Items[i].Rarity > 4)
                        {
                            spriteBatch.Draw(Items[i].LegendaryBg, new Rectangle((int)InvBoxes[5].X, (int)InvBoxes[5].Y, 45, 30), new Rectangle(0, 0, Items[i].LegendaryBg.Width, Items[i].LegendaryBg.Height), Color.White);
                        }
                        spriteBatch.Draw(Items[i].ItemTexture, new Rectangle((int)InvBoxes[5].X, (int)InvBoxes[5].Y, 45, 30), new Rectangle(0, 0, Items[i].ItemTexture.Width, Items[i].ItemTexture.Height), color);
                        Items[i].location = new Vector2(InvBoxes[5].X, InvBoxes[5].Y);
                    }
                    if (i == 6)
                    {
                        if (Items[i].Rarity > 4)
                        {
                            spriteBatch.Draw(Items[i].LegendaryBg, new Rectangle((int)InvBoxes[6].X, (int)InvBoxes[6].Y, 45, 30), new Rectangle(0, 0, Items[i].LegendaryBg.Width, Items[i].LegendaryBg.Height), Color.White);
                        }
                        spriteBatch.Draw(Items[i].ItemTexture, new Rectangle((int)InvBoxes[6].X, (int)InvBoxes[6].Y, 45, 30), new Rectangle(0, 0, Items[i].ItemTexture.Width, Items[i].ItemTexture.Height), color);
                        Items[i].location = new Vector2(InvBoxes[6].X, InvBoxes[6].Y);
                    }
                    if (i == 7)
                    {
                        if (Items[i].Rarity > 4)
                        {
                            spriteBatch.Draw(Items[i].LegendaryBg, new Rectangle((int)InvBoxes[7].X, (int)InvBoxes[7].Y, 45, 30), new Rectangle(0, 0, Items[i].LegendaryBg.Width, Items[i].LegendaryBg.Height), Color.White);
                        }
                        spriteBatch.Draw(Items[i].ItemTexture, new Rectangle((int)InvBoxes[7].X, (int)InvBoxes[7].Y, 45, 30), new Rectangle(0, 0, Items[i].ItemTexture.Width, Items[i].ItemTexture.Height), color);
                        Items[i].location = new Vector2(InvBoxes[7].X, InvBoxes[7].Y);
                    }

                    //Row 3
                    if (i == 8)
                    {
                        if (Items[i].Rarity > 4)
                        {
                            spriteBatch.Draw(Items[i].LegendaryBg, new Rectangle((int)InvBoxes[8].X, (int)InvBoxes[8].Y, 45, 30), new Rectangle(0, 0, Items[i].LegendaryBg.Width, Items[i].LegendaryBg.Height), Color.White);
                        }
                        spriteBatch.Draw(Items[i].ItemTexture, new Rectangle((int)InvBoxes[8].X, (int)InvBoxes[8].Y, 45, 30), new Rectangle(0, 0, Items[i].ItemTexture.Width, Items[i].ItemTexture.Height), color);
                        Items[i].location = new Vector2(InvBoxes[8].X, InvBoxes[8].Y);
                    }
                    if (i == 9)
                    {
                        if (Items[i].Rarity > 4)
                        {
                            spriteBatch.Draw(Items[i].LegendaryBg, new Rectangle((int)InvBoxes[9].X, (int)InvBoxes[9].Y, 45, 30), new Rectangle(0, 0, Items[i].LegendaryBg.Width, Items[i].LegendaryBg.Height), Color.White);
                        }
                        spriteBatch.Draw(Items[i].ItemTexture, new Rectangle((int)InvBoxes[9].X, (int)InvBoxes[9].Y, 45, 30), new Rectangle(0, 0, Items[i].ItemTexture.Width, Items[i].ItemTexture.Height), color);
                        Items[i].location = new Vector2(InvBoxes[9].X, InvBoxes[9].Y);
                    }
                    if (i == 10)
                    {
                        if (Items[i].Rarity > 4)
                        {
                            spriteBatch.Draw(Items[i].LegendaryBg, new Rectangle((int)InvBoxes[10].X, (int)InvBoxes[10].Y, 45, 30), new Rectangle(0, 0, Items[i].LegendaryBg.Width, Items[i].LegendaryBg.Height), Color.White);
                        }
                        spriteBatch.Draw(Items[i].ItemTexture, new Rectangle((int)InvBoxes[10].X, (int)InvBoxes[10].Y, 45, 30), new Rectangle(0, 0, Items[i].ItemTexture.Width, Items[i].ItemTexture.Height), color);
                        Items[i].location = new Vector2(InvBoxes[10].X, InvBoxes[10].Y);
                    }
                    if (i == 11)
                    {
                        if (Items[i].Rarity > 4)
                        {
                            spriteBatch.Draw(Items[i].LegendaryBg, new Rectangle((int)InvBoxes[11].X, (int)InvBoxes[11].Y, 45, 30), new Rectangle(0, 0, Items[i].LegendaryBg.Width, Items[i].LegendaryBg.Height), Color.White);
                        }
                        spriteBatch.Draw(Items[i].ItemTexture, new Rectangle((int)InvBoxes[11].X, (int)InvBoxes[11].Y, 45, 30), new Rectangle(0, 0, Items[i].ItemTexture.Width, Items[i].ItemTexture.Height), color);
                        Items[i].location = new Vector2(InvBoxes[11].X, InvBoxes[11].Y);
                    }

                    //Row 4
                    if (i == 12)
                    {
                        if (Items[i].Rarity > 4)
                        {
                            spriteBatch.Draw(Items[i].LegendaryBg, new Rectangle((int)InvBoxes[12].X, (int)InvBoxes[12].Y, 45, 30), new Rectangle(0, 0, Items[i].LegendaryBg.Width, Items[i].LegendaryBg.Height), Color.White);
                        }
                        spriteBatch.Draw(Items[i].ItemTexture, new Rectangle((int)InvBoxes[12].X, (int)InvBoxes[12].Y, 45, 30), new Rectangle(0, 0, Items[i].ItemTexture.Width, Items[i].ItemTexture.Height), color);
                        Items[i].location = new Vector2(InvBoxes[12].X, InvBoxes[12].Y);
                    }
                    if (i == 13)
                    {
                        if (Items[i].Rarity > 4)
                        {
                            spriteBatch.Draw(Items[i].LegendaryBg, new Rectangle((int)InvBoxes[13].X, (int)InvBoxes[13].Y, 45, 30), new Rectangle(0, 0, Items[i].LegendaryBg.Width, Items[i].LegendaryBg.Height), Color.White);
                        }
                        spriteBatch.Draw(Items[i].ItemTexture, new Rectangle((int)InvBoxes[13].X, (int)InvBoxes[13].Y, 45, 30), new Rectangle(0, 0, Items[i].ItemTexture.Width, Items[i].ItemTexture.Height), color);
                        Items[i].location = new Vector2(InvBoxes[13].X, InvBoxes[13].Y);
                    }
                    if (i == 14)
                    {
                        if (Items[i].Rarity > 4)
                        {
                            spriteBatch.Draw(Items[i].LegendaryBg, new Rectangle((int)InvBoxes[14].X, (int)InvBoxes[14].Y, 45, 30), new Rectangle(0, 0, Items[i].LegendaryBg.Width, Items[i].LegendaryBg.Height), Color.White);
                        }
                        spriteBatch.Draw(Items[i].ItemTexture, new Rectangle((int)InvBoxes[14].X, (int)InvBoxes[14].Y, 45, 30), new Rectangle(0, 0, Items[i].ItemTexture.Width, Items[i].ItemTexture.Height), color);
                        Items[i].location = new Vector2(InvBoxes[14].X, InvBoxes[14].Y);
                    }
                    if (i == 15)
                    {
                        if (Items[i].Rarity > 4)
                        {
                            spriteBatch.Draw(Items[i].LegendaryBg, new Rectangle((int)InvBoxes[15].X, (int)InvBoxes[15].Y, 45, 30), new Rectangle(0, 0, Items[i].LegendaryBg.Width, Items[i].LegendaryBg.Height), Color.White);
                        }
                        spriteBatch.Draw(Items[i].ItemTexture, new Rectangle((int)InvBoxes[15].X, (int)InvBoxes[15].Y, 45, 30), new Rectangle(0, 0, Items[i].ItemTexture.Width, Items[i].ItemTexture.Height), color);
                        Items[i].location = new Vector2(InvBoxes[15].X, InvBoxes[15].Y);
                    }

                    //Row 5
                    if (i == 16)
                    {
                        if (Items[i].Rarity > 4)
                        {
                            spriteBatch.Draw(Items[i].LegendaryBg, new Rectangle((int)InvBoxes[16].X, (int)InvBoxes[16].Y, 45, 30), new Rectangle(0, 0, Items[i].LegendaryBg.Width, Items[i].LegendaryBg.Height), Color.White);
                        }
                        spriteBatch.Draw(Items[i].ItemTexture, new Rectangle((int)InvBoxes[16].X, (int)InvBoxes[16].Y, 45, 30), new Rectangle(0, 0, Items[i].ItemTexture.Width, Items[i].ItemTexture.Height), color);
                        Items[i].location = new Vector2(InvBoxes[16].X, InvBoxes[16].Y);
                    }
                    if (i == 17)
                    {
                        if (Items[i].Rarity > 4)
                        {
                            spriteBatch.Draw(Items[i].LegendaryBg, new Rectangle((int)InvBoxes[17].X, (int)InvBoxes[17].Y, 45, 30), new Rectangle(0, 0, Items[i].LegendaryBg.Width, Items[i].LegendaryBg.Height), Color.White);
                        }
                        spriteBatch.Draw(Items[i].ItemTexture, new Rectangle((int)InvBoxes[17].X, (int)InvBoxes[17].Y, 45, 30), new Rectangle(0, 0, Items[i].ItemTexture.Width, Items[i].ItemTexture.Height), color);
                        Items[i].location = new Vector2(InvBoxes[17].X, InvBoxes[17].Y);
                    }
                    if (i == 18)
                    {
                        if (Items[i].Rarity > 4)
                        {
                            spriteBatch.Draw(Items[i].LegendaryBg, new Rectangle((int)InvBoxes[18].X, (int)InvBoxes[18].Y, 45, 30), new Rectangle(0, 0, Items[i].LegendaryBg.Width, Items[i].LegendaryBg.Height), Color.White);
                        }
                        spriteBatch.Draw(Items[i].ItemTexture, new Rectangle((int)InvBoxes[18].X, (int)InvBoxes[18].Y, 45, 30), new Rectangle(0, 0, Items[i].ItemTexture.Width, Items[i].ItemTexture.Height), color);
                        Items[i].location = new Vector2(InvBoxes[18].X, InvBoxes[18].Y);
                    }
                    if (i == 19)
                    {
                        if (Items[i].Rarity > 4)
                        {
                            spriteBatch.Draw(Items[i].LegendaryBg, new Rectangle((int)InvBoxes[19].X, (int)InvBoxes[19].Y, 45, 30), new Rectangle(0, 0, Items[i].LegendaryBg.Width, Items[i].LegendaryBg.Height), Color.White);
                        }
                        spriteBatch.Draw(Items[i].ItemTexture, new Rectangle((int)InvBoxes[19].X, (int)InvBoxes[19].Y, 45, 30), new Rectangle(0, 0, Items[i].ItemTexture.Width, Items[i].ItemTexture.Height), color);
                        Items[i].location = new Vector2(InvBoxes[19].X, InvBoxes[19].Y);
                    }

                    if (Items[i].invhover)
                    {
                        //Draw Shaded backgorund
                        spriteBatch.Draw(Items[i].TextBackground, Items[i].location, Color.White);

                        //Draw Item Description
                        if (Items[i].Rarity == 6)
                        {
                            spriteBatch.DrawString(Items[i].Font1, Items[i].ItemName + " " + Items[i].ItemRarity, new Vector2((Items[i].location.X + 100), (Items[i].location.Y + 30)), Items[i].RarityColor);
                        }
                        else
                        {
                            spriteBatch.DrawString(Items[i].Font1, Items[i].ItemRarity + " " + Items[i].ItemName, new Vector2((Items[i].location.X + 100), (Items[i].location.Y + 30)), Items[i].RarityColor);
                        }

                        //Initialize Scalar Value
                        ScalarText = 20;

                        //Draw Item aFfixes
                        for (int intlc = 0; intlc < Items[i].affixdesclist.Count; intlc++)
                        {
                            if (intlc < 4)
                            {
                                spriteBatch.DrawString(Items[i].Font1, Items[i].affixdesclist[intlc], new Vector2((Items[i].location.X + 100), (Items[i].location.Y + ScalarText + 50)), Color.White);
                                ScalarText += 20;
                            }
                            if (intlc == 4)
                            {
                                spriteBatch.DrawString(Items[i].Font1, Items[i].affixdesclist[intlc], new Vector2((Items[i].location.X + 100), (Items[i].location.Y + ScalarText + 50)), Color.Orange);
                                ScalarText += 20;
                            }
                            if (intlc == 5)
                            {
                                spriteBatch.DrawString(Items[i].Font1, Items[i].affixdesclist[intlc], new Vector2((Items[i].location.X + 100), (Items[i].location.Y + ScalarText + 50)), Color.Purple);
                                ScalarText += 20;
                            }
                            if (intlc > 5)
                            {
                                spriteBatch.DrawString(Items[i].Font1, Items[i].affixdesclist[intlc], new Vector2((Items[i].location.X + 100), (Items[i].location.Y + ScalarText + 50)), Color.Brown);
                                ScalarText += 20;
                            }
                        }
                    }
#endregion

                }
            }
        }

    }
}
