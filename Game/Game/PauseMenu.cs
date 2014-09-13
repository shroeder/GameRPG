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
    class PauseMenu
    {

        public event EventHandler Resume;

        public void Draw(SpriteBatch spriteBatch, Texture2D img, Texture2D MenuBtn, SpriteFont font ,GraphicsDeviceManager gfx)
        {

            Rectangle Rect = new Rectangle(0, 0, gfx.PreferredBackBufferWidth, gfx.PreferredBackBufferHeight);

            int startposx = Convert.ToInt32(gfx.PreferredBackBufferWidth * .35);
            int width = Convert.ToInt32(gfx.PreferredBackBufferWidth * .25);
            int startposy = Convert.ToInt32(gfx.PreferredBackBufferHeight * .15);
            int height = Convert.ToInt32(gfx.PreferredBackBufferHeight * .10);

            spriteBatch.Begin();

            //Draw Menu Buttons
            
            MenuButton mnuBtnResume = new MenuButton();

            spriteBatch.Draw(img, Rect, Color.Black);

            spriteBatch.End();

            mnuBtnResume.ButtonClicked += mnuBtnResume_Clicked;

            mnuBtnResume.Draw(spriteBatch, MenuBtn, gfx, new Rectangle(startposx, startposy, width, height), "Resume", font);
                        
        }

        public void mnuBtnResume_Clicked(object sender, EventArgs eventArgs)
        {
            if (Resume != null)
            {
                Resume(this, EventArgs.Empty);
            }
        }

    }
}
