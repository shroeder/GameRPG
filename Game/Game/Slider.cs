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
    class Slider
    {

        private int SelectedIndex = 0;

        public int Index
        {
            get
            {
                return SelectedIndex;
            }

            set
            {
                SelectedIndex = value;
            }
        }

        bool blnDragging = false;
        List<string> ReturnArray = new List<string>();

        public void Draw(SpriteBatch spriteBatch, SpriteFont font, GraphicsDeviceManager gfx, Texture2D txt, List<string> Array, int width, int height, int posX, int posY, Color color)
        {

            spriteBatch.Begin();

            ReturnArray = Array;

            MouseState ms = Mouse.GetState();

            posY += (int)(gfx.PreferredBackBufferHeight * .04);

            int interval = (width / Array.Count());
            int splitValue = 0;

            List<Vector2> SliderLocations = new List<Vector2>();

            //IsOdd
            if (Array.Count % 2 != 0)
            {
                splitValue = (interval / (Array.Count - 1));
                interval += splitValue;
            }

            int i = 0;

            spriteBatch.Draw(txt, new Rectangle(posX, posY, width, height), color);

            foreach (string item in Array)
            {
                int TextWidth = Convert.ToInt32(font.MeasureString(item).X);
                int TextHeight = Convert.ToInt32(font.MeasureString(item).Y);

                SliderLocations.Add(new Vector2((posX + (i * interval) - (int)(gfx.PreferredBackBufferWidth * .0125)), posY + (int)(gfx.PreferredBackBufferHeight * .015) - (int)(TextHeight * .01)));

                spriteBatch.DrawString(font, item, new Vector2((posX + (i * interval) - (int)(gfx.PreferredBackBufferWidth * .0125)), posY + (int)(gfx.PreferredBackBufferHeight * .015) - (int)(TextHeight * .01)), Color.WhiteSmoke);
                spriteBatch.Draw(txt, new Rectangle(posX + (i * interval), posY - (int)(gfx.PreferredBackBufferHeight * .01), (int)(gfx.PreferredBackBufferWidth * .0025), (int)(gfx.PreferredBackBufferHeight * .02)), color);

                i += 1;
            }

            Rectangle TheSlider = new Rectangle((int)SliderLocations[SelectedIndex].X + (int)(gfx.PreferredBackBufferWidth * .0125), (int)SliderLocations[SelectedIndex].Y - (int)(gfx.PreferredBackBufferHeight * .0325), 1 * (int)(gfx.PreferredBackBufferWidth * .0075), 1 * (int)(gfx.PreferredBackBufferHeight * .03));

            if (!blnDragging)
            {
                if (Game1.In(ms, TheSlider))
                {
                    spriteBatch.Draw(txt, TheSlider, Color.SlateGray);
                    if (ms.LeftButton == ButtonState.Pressed)
                    {
                        blnDragging = true;
                    }
                }
                else
                {
                    spriteBatch.Draw(txt, TheSlider, Color.White);
                }
            }
            else
            {
                if (ms.X < posX)
                {
                    TheSlider.X = posX;
                }
                else if (ms.X > (posX + width))
                {
                    TheSlider.X = (posX + width) - (int)(gfx.PreferredBackBufferWidth * .0025);
                }
                else
                {
                    TheSlider.X = ms.X;
                }
                if (ms.LeftButton == ButtonState.Released)
                {
                    blnDragging = false;
                    int x = 0;
                    foreach (Vector2 SliderLocation in SliderLocations)
                    {
                        if (Math.Abs(ms.X - SliderLocation.X) < (interval / 2))
                        {
                            SelectedIndex = x;
                        }
                        else
                        {
                            x += 1;
                        }
                    }
                }
                else
                {
                    spriteBatch.Draw(txt, TheSlider, Color.SlateGray);
                }
            }

            spriteBatch.End();

        }

        public string ReturnValue()
        {
            return ReturnArray[SelectedIndex];
        }

    }
}
