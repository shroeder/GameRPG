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

        public MouseState ms;
        public MouseState oms;
        public event EventHandler Resume;
        public event EventHandler Exit;
        public bool blnOptionsOpen = false;
        public bool blnVideoOpen = false;
        public bool blnGameplayOpen = false;
        public bool blnAudioOpen = false;

        public List<string> Ratios = new List<string>(new string[] { "4:3", "16:9", "16:10" });

        public List<string> Resolution_169 = new List<string>(new string[] { "640 X 360", "960 X 540", "1280 X 720", "1600 X 900", "1920 X 1080" });
        public List<string> Resolution_1610 = new List<string>(new string[] { "640 X 400", "1280 X 800", "1440 X 900", "1680 X 1050", "1920 X 1200" });
        public List<string> Resolution_43 = new List<string>(new string[] { "640 X 480", "800 X 600", "1024 X 768", "1280 X 960", "1600 X 1200" });

        MenuButton mnuBtnResume;
        MenuButton mnuBtnExit;
        MenuButton mnuBtnOptions;

        MenuButton mnuBtnVideo;
        MenuButton mnuBtnAudio;
        MenuButton mnuBtnGamePlay;
        MenuButton mnuBtnBack;

        Slider slider;

        public void Draw(SpriteBatch spriteBatch, Texture2D img, Texture2D MenuBtn, SpriteFont font, GraphicsDeviceManager gfx, SpriteFont smallfont)
        {
            oms = ms;
            ms = Mouse.GetState();

            Rectangle Rect = new Rectangle(0, 0, gfx.PreferredBackBufferWidth, gfx.PreferredBackBufferHeight);

            spriteBatch.Begin();
            spriteBatch.Draw(img, Rect, Color.Black);
            spriteBatch.End();

            int startposx = Convert.ToInt32(gfx.PreferredBackBufferWidth * .35);
            int width = Convert.ToInt32(gfx.PreferredBackBufferWidth * .25);
            int startposy = Convert.ToInt32(gfx.PreferredBackBufferHeight * .15);
            int height = Convert.ToInt32(gfx.PreferredBackBufferHeight * .10);

            if (!blnOptionsOpen)
            {

                mnuBtnResume = new MenuButton();
                mnuBtnExit = new MenuButton();
                mnuBtnOptions = new MenuButton();

                mnuBtnResume.ButtonClicked += mnuBtnResume_Clicked;
                mnuBtnExit.ButtonClicked += mnuBtnExit_Clicked;
                mnuBtnOptions.ButtonClicked += mnuBtnOptions_Clicked;

                mnuBtnResume.Draw(spriteBatch, MenuBtn, gfx, new Rectangle(startposx, startposy, width, height), "Resume", font);
                mnuBtnOptions.Draw(spriteBatch, MenuBtn, gfx, new Rectangle(startposx, startposy + Convert.ToInt32(gfx.PreferredBackBufferHeight * .15), width, height), "Options", font);
                mnuBtnExit.Draw(spriteBatch, MenuBtn, gfx, new Rectangle(startposx, startposy + Convert.ToInt32(gfx.PreferredBackBufferHeight * .30), width, height), "Exit", font);

            }
            else
            {
                if (blnVideoOpen) {

                    slider = new Slider();

                    double TextWidth = font.MeasureString("Resolution").X;
                    double TextHeight = font.MeasureString("Resolution").Y;
                    int SliderWidth = (int)(gfx.PreferredBackBufferWidth * .3);
                    int SliderHeight = (int)(gfx.PreferredBackBufferHeight * .005);

                    spriteBatch.Begin();
                    spriteBatch.DrawString(font, "Resolution", new Vector2(startposx + ((float)(gfx.PreferredBackBufferWidth * .1) - (float)(TextWidth * .25)),startposy), Color.Gray);

                    slider.Draw(spriteBatch, smallfont, gfx, MenuBtn, Resolution_43, Resolution_169, Resolution_1610, SliderWidth, SliderHeight, startposx - (int)(gfx.PreferredBackBufferWidth * .025), startposy + (int)(gfx.PreferredBackBufferHeight * .07), Color.Black);
                    spriteBatch.End();

                }
                else if (blnAudioOpen) { }
                else if (blnGameplayOpen) { }
                else
                {
                        mnuBtnVideo = new MenuButton();
                        mnuBtnAudio = new MenuButton();
                        mnuBtnGamePlay = new MenuButton();
                        mnuBtnBack = new MenuButton();

                        if (oms.LeftButton == ButtonState.Released)
                        {
                            mnuBtnVideo.ButtonClicked += mnuBtnVideo_Clicked;
                            mnuBtnAudio.ButtonClicked += mnuBtnAudio_Clicked;
                            mnuBtnGamePlay.ButtonClicked += mnuBtnGamePlay_Clicked;
                            mnuBtnBack.ButtonClicked += mnuBtnBack_Clicked;
                        }

                        mnuBtnVideo.Draw(spriteBatch, MenuBtn, gfx, new Rectangle(startposx, startposy, width, height), "Video", font);
                        mnuBtnAudio.Draw(spriteBatch, MenuBtn, gfx, new Rectangle(startposx, startposy + Convert.ToInt32(gfx.PreferredBackBufferHeight * .15), width, height), "Audio", font);
                        mnuBtnGamePlay.Draw(spriteBatch, MenuBtn, gfx, new Rectangle(startposx, startposy + Convert.ToInt32(gfx.PreferredBackBufferHeight * .30), width, height), "Gameplay", font);
                        mnuBtnBack.Draw(spriteBatch, MenuBtn, gfx, new Rectangle(startposx, startposy + Convert.ToInt32(gfx.PreferredBackBufferHeight * .45), width, height), "Back", font);
                }
            }
        }

        public void mnuBtnResume_Clicked(object sender, EventArgs eventArgs)
        {
            if (Resume != null)
            {
                Resume(this, EventArgs.Empty);
            }
        }

        public void mnuBtnExit_Clicked(object sender, EventArgs eventArgs)
        {
            if (Exit != null)
            {
                Exit(this, EventArgs.Empty);
            }
        }

        public void mnuBtnOptions_Clicked(object sender, EventArgs eventArgs)
        {
            blnOptionsOpen = true;
        }

        public void mnuBtnVideo_Clicked(object sender, EventArgs eventArgs)
        {
            blnVideoOpen = true;
        }

        public void mnuBtnAudio_Clicked(object sneder, EventArgs eventArgs){
            blnAudioOpen = true;
        }

        public void mnuBtnGamePlay_Clicked(object sender, EventArgs evenArgs){
            blnGameplayOpen = true;
        }

        public void mnuBtnBack_Clicked(object sender, EventArgs eventArgs)
        {
            blnOptionsOpen = false;
        }

    }
}
