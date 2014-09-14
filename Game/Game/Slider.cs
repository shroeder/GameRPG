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

        public void Draw(SpriteBatch spriteBatch,SpriteFont font ,GraphicsDeviceManager gfx, Texture2D txt, List<string> res43, List<string> res169, List<string> res1610, int width, int height, int posX, int posY, Color color)
        {

            int interval = (width / res169.Count());
            int splitValue = 0;

            //IsOdd
            if (res169.Count % 2 != 0)
            {
                splitValue = (interval / (res169.Count - 1));
                interval += splitValue;
            }

            int i = 0;

            spriteBatch.Draw(txt,new Rectangle(posX,posY,width,height),color);

            foreach (string item in res169)
            {
                int TextWidth = Convert.ToInt32(font.MeasureString(item).X);
                int TextHeight = Convert.ToInt32(font.MeasureString(item).Y);

                spriteBatch.DrawString(font, item, new Vector2((posX + (i * interval) - (int)(gfx.PreferredBackBufferWidth * .0125)), posY + (int)(gfx.PreferredBackBufferHeight * .015) - (int)(TextHeight * .01)), Color.WhiteSmoke);
                spriteBatch.Draw(txt, new Rectangle(posX + (i * interval), posY - (int)(gfx.PreferredBackBufferHeight * .01), (int)(gfx.PreferredBackBufferWidth * .0025), (int)(gfx.PreferredBackBufferHeight * .02)), Color.WhiteSmoke);

                i += 1;
            }

        }

    }
}
