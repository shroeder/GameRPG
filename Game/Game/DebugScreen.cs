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
    class DebugScreen
    {

        public event EventHandler DrawBgClicked;

        private int average = 0;
        private int offsetY = 0;
        private int PositionX = 0;
        private string CyclesListed1;
        private string CyclesListed2;
        private string CyclesListed3;
        private string CyclesListed4;
        private MouseState ms;

        public void Draw(SpriteBatch spriteBatch, SpriteFont font, SpriteFont largefont, bool StillRunning, GraphicsDeviceManager gfx, List<long> ListOfTimes, string NameOfCycle, string baseClass, string NumberOfCycles, Texture2D BackGround, int CaseNumber)
        {

            ms = Mouse.GetState();

            Rectangle BgButton = new Rectangle(PositionX, 100, 225, 300);

            if (StillRunning)
            {
                spriteBatch.DrawString(largefont, "Running Custom Debugger...", new Vector2((float)(gfx.PreferredBackBufferWidth / 2.3), (float)100), Color.DarkRed);
            }
            else
            {

                switch (CaseNumber)
                {

                    case 1:

                        PositionX = 100;
                        offsetY = 0;
                        break;

                    case 2:

                        PositionX = 350;
                        offsetY = 0;
                        break;

                    case 3:

                        PositionX = 600;
                        offsetY = 0;
                        break;

                    case 4:

                        PositionX = 850;
                        offsetY = 0;
                        break;

                    case 5:

                        PositionX = 1100;
                        offsetY = 0;
                        break;

                    case 6:

                        PositionX = 100;
                        offsetY = 400;
                        BgButton = new Rectangle(PositionX, 500, 225, 300);
                        break;

                    case 7:

                        PositionX = 350;
                        offsetY = 400;
                        BgButton = new Rectangle(PositionX, 500, 225, 300);
                        break;

                    case 8:

                        PositionX = 600;
                        offsetY = 400;
                        BgButton = new Rectangle(PositionX, 500, 225, 300);
                        break;

                    case 9:

                        PositionX = 850;
                        offsetY = 400;
                        BgButton = new Rectangle(PositionX, 500, 225, 300);
                        break;

                    case 10:

                        PositionX = 1100;
                        offsetY = 400;
                        BgButton = new Rectangle(PositionX, 500, 225, 300);
                        break;

                }

                if (CyclesListed1 == null)
                {

                    int count = 0;
                    int total = 0;

                    foreach (long Time in ListOfTimes)
                    {

                        total += Convert.ToInt32(Time);

                        if (count < 10)
                        {
                            CyclesListed1 += Time.ToString() + "\n";
                        }
                        else if (count < 20)
                        {
                            CyclesListed2 += Time.ToString() + "\n";
                        }
                        else if (count < 30)
                        {
                            CyclesListed3 += Time.ToString() + "\n";
                        }
                        else
                        {
                            CyclesListed4 += Time.ToString() + "\n";
                        }

                        count += 1;
                    }

                    average = (total / Convert.ToInt32(NumberOfCycles));

                }

                Color colorCode;

                if (ms.X > BgButton.X && ms.X < BgButton.X + BgButton.Width && ms.Y > BgButton.Y && ms.Y < BgButton.Y + BgButton.Height)
                {
                    colorCode = Color.LightBlue;

                    if (ms.LeftButton == ButtonState.Pressed)
                    {
                        if (DrawBgClicked != null)
                        {
                            DrawBgClicked(this, EventArgs.Empty);
                        }
                    }

                }
                else
                {

                    if (average < 1000)
                    {
                        colorCode = Color.Wheat;
                    }
                    else if (average < 5000)
                    {
                        colorCode = Color.Brown;
                    }
                    else if (average < 10000)
                    {
                        colorCode = Color.Maroon;
                    }
                    else
                    {
                        colorCode = Color.Red;
                    }
                }

                spriteBatch.Draw(BackGround, BgButton, colorCode);

                spriteBatch.DrawString(font, baseClass + "." + NameOfCycle, new Vector2(PositionX + 20, offsetY + 120), Color.White);

                spriteBatch.DrawString(font, "Number Of Cycles : " + Convert.ToString(NumberOfCycles), new Vector2(PositionX + 20, offsetY + 140), Color.White);

                if (CyclesListed1 != null)
                {
                    spriteBatch.DrawString(font, CyclesListed1, new Vector2(PositionX + 20, 160 + offsetY), Color.Black);
                }

                if (CyclesListed2 != null)
                {
                    spriteBatch.DrawString(font, CyclesListed2, new Vector2(PositionX + 70, 160 + offsetY), Color.Black);
                }

                if (CyclesListed3 != null)
                {
                    spriteBatch.DrawString(font, CyclesListed3, new Vector2(PositionX + 120, 160 + offsetY), Color.Black);
                }

                if (CyclesListed4 != null)
                {
                    spriteBatch.DrawString(font, CyclesListed4, new Vector2(PositionX + 170, 160 + offsetY), Color.Black);
                }

                spriteBatch.DrawString(font, "Average : " + average.ToString(), new Vector2(PositionX + 20, 350 + offsetY), Color.Black);

            }
        }
    }
}
