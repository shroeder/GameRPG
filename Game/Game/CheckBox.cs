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
    class CheckBox
    {

        public event EventHandler ClickedChecked;

        public enum CheckedState
        {
            Checked,
            UnChecked
        }

        public CheckedState CheckState = CheckedState.UnChecked;

        Rectangle Rect;
        MouseState ms;
        MouseState oms;

        public void draw(SpriteBatch spriteBatch, Texture2D img, int posX, int posY, int width, int height, string text, GraphicsDeviceManager gfx, SpriteFont setFont = null)
        {

            oms = ms;
            ms = Mouse.GetState();
            SpriteFont font = GlobalVariables.MediumFont;

            if (setFont != null)
            {
                font = setFont;
            }

            int textWidth = Convert.ToInt32(font.MeasureString(text).X);
            int textHeight = Convert.ToInt32(font.MeasureString(text).Y);

            Rect = new Rectangle(posX,posY,width,height);

            spriteBatch.DrawString(font, text, new Vector2(Rect.X - (textWidth + (int)(gfx.PreferredBackBufferWidth*.005)), Rect.Y - (int)(textHeight * .35)), Color.WhiteSmoke);

            spriteBatch.Draw(img, Rect, Color.White);

            if (Game1.In(ms, Rect))
            {
                spriteBatch.Draw(img, new Rectangle((int)(Rect.X) + (int)(gfx.PreferredBackBufferWidth * .002), (int)(Rect.Y + (gfx.PreferredBackBufferHeight * .003)), (int)(Rect.Width * .7), (int)(Rect.Height * .7)), Color.SlateGray);
                if (ms.LeftButton == ButtonState.Pressed && oms.LeftButton == ButtonState.Released)
                {
                    if (CheckState == CheckedState.Checked)
                    {
                        CheckState = CheckedState.UnChecked;
                    }
                    else
                    {
                        CheckState = CheckedState.Checked;

                        if (ClickedChecked != null)
                        {
                            ClickedChecked(this, EventArgs.Empty);
                        }

                    }
                }
            }
            else
            {
                if (CheckState == CheckedState.Checked)
                {
                    spriteBatch.Draw(img, new Rectangle((int)(Rect.X) + (int)(gfx.PreferredBackBufferWidth * .002), (int)(Rect.Y + (gfx.PreferredBackBufferHeight * .003)), (int)(Rect.Width * .7), (int)(Rect.Height * .7)), Color.Black);
                }
                else
                {
                    spriteBatch.Draw(img, new Rectangle((int)(Rect.X) + (int)(gfx.PreferredBackBufferWidth * .002), (int)(Rect.Y + (gfx.PreferredBackBufferHeight * .003)), (int)(Rect.Width * .7), (int)(Rect.Height * .7)), Color.WhiteSmoke);
                }
            }

            if (CheckState == CheckedState.Checked)
            {
                spriteBatch.Draw(GlobalVariables.CheckMark, Rect, Color.White);
            }
        }

        public void Uncheck()
        {
            CheckState = CheckedState.UnChecked;
        }

        public void Check()
        {
            CheckState = CheckedState.Checked;
        }

        public bool IsChecked()
        {
            if (CheckState == CheckedState.Checked)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
