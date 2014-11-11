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
    public class MenuButton
    {

        public MouseState oms;
        public MouseState ms;

        public event EventHandler ButtonClicked;

        public void Draw(SpriteBatch spriteBatch, Texture2D img, GraphicsDeviceManager gfx, Rectangle Location, string mnuText, SpriteFont font)
        {

            float TextWidth = font.MeasureString(mnuText).X;
            float TextHeight = font.MeasureString(mnuText).Y;

            oms = ms;
            ms = Mouse.GetState();

            if (Game1.In(ms,Location))
            {
                if (ms.LeftButton == ButtonState.Pressed && oms.LeftButton == ButtonState.Released)
                {
                    spriteBatch.Draw(img, Location, Color.Black);
                    spriteBatch.DrawString(font, mnuText, new Vector2(((Location.X + Convert.ToInt32(Location.Width * .50) - (TextWidth / 2))), (Location.Y + Convert.ToInt32(Location.Height * .50) - (TextHeight / 2))), Color.Gray);

                    if (ButtonClicked != null)
                    {
                        ButtonClicked(this, EventArgs.Empty);
                    }

                }
                else
                {
                    spriteBatch.Draw(img, Location, Color.SteelBlue);
                    spriteBatch.DrawString(font, mnuText, new Vector2(((Location.X + Convert.ToInt32(Location.Width * .50) - (TextWidth / 2))), (Location.Y + Convert.ToInt32(Location.Height * .50) - (TextHeight / 2))), Color.Gray);
                }
            }
            else
            {
                spriteBatch.Draw(img, Location, Color.SlateGray);
                spriteBatch.DrawString(font, mnuText, new Vector2(((Location.X + Convert.ToInt32(Location.Width * .50) - (TextWidth / 2))), (Location.Y + Convert.ToInt32(Location.Height * .50) - (TextHeight / 2))), Color.Black);
            }

        }

    }
}
