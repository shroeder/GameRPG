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

        public bool blnOptionsOpen = false;
        public bool DefaultPauseOpen = true;
        public bool blnVideoOpen = false;
        public bool blnGameplayOpen = false;
        public bool blnAudioOpen = false;
        private bool blnChkYesBound = false;
        private bool chkNoBound = false;
        private bool mnuBtnAudioBound = false;
        private bool mnuBtnBackBound = false;
        private bool mnuBtnConfirmBound = false;
        private bool mnuBtnCancelBound = false;
        private bool MnuBtnExitBound = false;
        private bool mnuBtnGamePlayBound = false;
        private bool MnuBtnResumeBound = false;
        private bool mnuBtnRevertBound = false;
        private bool mnuBtnVideoBound = false;
        private bool mnuBtnOptionsBound = false;
        private bool mnuBtnAcceptChangesBound = false;

        public MouseState ms;
        public MouseState oms;
        public event EventHandler Resume;
        public event EventHandler Exit;
        public event EventHandler ResolutionChange;
        public event EventHandler ConfirmChange;
        public event EventHandler RevertChange;

        public List<string> Ratios = new List<string>(new string[] { "4:3", "16:9", "16:10" });

        public List<string> Resolution_169 = new List<string>(new string[] { "640 X 360", "960 X 540", "1280 X 720", "1600 X 900", "1920 X 1080" });
        public List<string> Resolution_1610 = new List<string>(new string[] { "640 X 400", "1280 X 800", "1440 X 900", "1680 X 1050", "1920 X 1200" });
        public List<string> Resolution_43 = new List<string>(new string[] { "640 X 480", "800 X 600", "1024 X 768", "1280 X 960", "1600 X 1200" });
        public List<string> DesiredArray = new List<string>();

        MenuButton mnuBtnResume;
        MenuButton mnuBtnExit;
        MenuButton mnuBtnOptions;
        MenuButton mnuBtnVideo;
        MenuButton mnuBtnAudio;
        MenuButton mnuBtnGamePlay;
        MenuButton mnuBtnBack;
        MenuButton mnuBtnAcceptChanges;
        MenuButton mnuBtnCancel;
        MenuButton mnuBtnConfirm;
        MenuButton mnuBtnRevert;

        CheckBox chkYes;
        CheckBox chkNo;
        
        Slider slider = new Slider();
        Slider slider1 = new Slider();

        public void Draw(SpriteBatch spriteBatch, Texture2D img, Texture2D MenuBtn, SpriteFont font, GraphicsDeviceManager gfx, SpriteFont smallfont, int MenuType)
        {
            mnuBtnAcceptChanges = null;
            mnuBtnCancel = null;
            mnuBtnConfirm = null;
            mnuBtnExit = null;
            mnuBtnOptions = null;
            mnuBtnResume = null;
            mnuBtnRevert = null;
            mnuBtnVideo = null;
            mnuBtnAudio = null;
            mnuBtnGamePlay = null;
            mnuBtnBack = null;

            switch (MenuType)
            {

                //Draw PauseMenu
                case 1:

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

                    if (DefaultPauseOpen)
                    {
                        if (mnuBtnResume == null)
                        {
                            mnuBtnResume = new MenuButton();
                            MnuBtnResumeBound = false;
                        }
                        if (mnuBtnExit == null)
                        {
                            mnuBtnExit = new MenuButton();
                            MnuBtnExitBound = false;
                        }
                        if (mnuBtnOptions == null)
                        {
                            mnuBtnOptions = new MenuButton();
                            mnuBtnOptionsBound = false;
                        }
                        if (!MnuBtnResumeBound)
                        {
                            mnuBtnResume.ButtonClicked += mnuBtnResume_Clicked;
                            MnuBtnResumeBound = true;
                        }
                        if (!MnuBtnExitBound)
                        {
                            mnuBtnExit.ButtonClicked += mnuBtnExit_Clicked;
                            MnuBtnExitBound = true;
                        }
                        if (!mnuBtnOptionsBound)
                        {
                            mnuBtnOptions.ButtonClicked += mnuBtnOptions_Clicked;
                            mnuBtnOptionsBound = false;
                        }

                        mnuBtnResume.Draw(spriteBatch, MenuBtn, gfx, new Rectangle(startposx, startposy, width, height), "Resume", font);
                        mnuBtnOptions.Draw(spriteBatch, MenuBtn, gfx, new Rectangle(startposx, startposy + Convert.ToInt32(gfx.PreferredBackBufferHeight * .15), width, height), "Options", font);
                        mnuBtnExit.Draw(spriteBatch, MenuBtn, gfx, new Rectangle(startposx, startposy + Convert.ToInt32(gfx.PreferredBackBufferHeight * .30), width, height), "Exit", font);

                    }
                    else
                    {
                        if (blnVideoOpen)
                        {

                            if (mnuBtnCancel == null)
                            {
                                mnuBtnCancel = new MenuButton();
                                mnuBtnCancelBound = false;
                            }
                            if (mnuBtnAcceptChanges == null)
                            {
                                mnuBtnAcceptChanges = new MenuButton();
                                mnuBtnAcceptChangesBound = false;
                            }
                            if(chkYes == null)
                            {
                                chkYes = new CheckBox();
                                blnChkYesBound = false;
                            }
                            if (chkNo == null)
                            {
                                chkNo = new CheckBox();
                                chkNoBound = false;
                            }

                            double TextWidth = font.MeasureString("Resolution").X;
                            double TextHeight = font.MeasureString("Resolution").Y;
                            int SliderWidth = (int)(gfx.PreferredBackBufferWidth * .3);
                            int SliderHeight = (int)(gfx.PreferredBackBufferHeight * .005);

                            spriteBatch.Begin();

                            spriteBatch.DrawString(font, "Aspect Ratio", new Vector2(startposx + ((float)(gfx.PreferredBackBufferWidth * .1) - (float)(TextWidth * .25)), startposy - (int)(gfx.PreferredBackBufferHeight * .05)), Color.Gray);

                            spriteBatch.End();

                            slider1.Draw(spriteBatch, smallfont, gfx, MenuBtn, Ratios, SliderWidth, SliderHeight, startposx - (int)(gfx.PreferredBackBufferWidth * .025), startposy - (int)(gfx.PreferredBackBufferHeight * .01), Color.Black);

                            string AspectRatio = slider1.ReturnValue();

                            if (AspectRatio.Contains("9"))
                            {
                                DesiredArray = Resolution_169;
                            }
                            else if (AspectRatio.Contains("10"))
                            {
                                DesiredArray = Resolution_1610;
                            }
                            else
                            {
                                DesiredArray = Resolution_43;
                            }

                            spriteBatch.Begin();

                            spriteBatch.DrawString(font, "Resolution", new Vector2(startposx + ((float)(gfx.PreferredBackBufferWidth * .1) - (float)(TextWidth * .25)), startposy + (int)(gfx.PreferredBackBufferHeight * .08)), Color.Gray);

                            spriteBatch.End();

                            slider.Draw(spriteBatch, smallfont, gfx, MenuBtn, DesiredArray, SliderWidth, SliderHeight, startposx - (int)(gfx.PreferredBackBufferWidth * .025), startposy + (int)(gfx.PreferredBackBufferHeight * .12), Color.Black);

                            string Resolution = slider.ReturnValue();
                            string[] Res = Resolution.Split('X');

                            GlobalVariables.NewWidth = Convert.ToInt32(Res[0].Trim());
                            GlobalVariables.NewHeight = Convert.ToInt32(Res[1].Trim());

                            if (oms.LeftButton == ButtonState.Released)
                            {
                                if (!mnuBtnCancelBound)
                                {
                                    mnuBtnCancel.ButtonClicked += mnuBtnCancel_Clicked;
                                    mnuBtnCancelBound = true;
                                }
                                if (!mnuBtnAcceptChangesBound)
                                {
                                    mnuBtnAcceptChanges.ButtonClicked += mnuBtnAcceptChanges_Clicked;
                                    mnuBtnAcceptChangesBound = true;
                                }
                                if (!chkNoBound)
                                {
                                    chkNo.ClickedChecked += ChkNo_Checked;
                                    chkNoBound = true;
                                }
                                if (!blnChkYesBound)
                                {
                                    chkYes.ClickedChecked += ChkYes_Checked;
                                    blnChkYesBound = true;
                                }
                            }

                            spriteBatch.Begin();

                            spriteBatch.DrawString(font, "FullScreen", new Vector2(startposx + ((float)(gfx.PreferredBackBufferWidth * .1) - (float)(TextWidth * .25)), startposy + (int)(gfx.PreferredBackBufferHeight * .22)), Color.Gray);

                            spriteBatch.End();

                            chkYes.draw(spriteBatch, MenuBtn, (int)((gfx.PreferredBackBufferWidth / 2) - gfx.PreferredBackBufferWidth * .10), (int)(gfx.PreferredBackBufferHeight * .45), (int)(gfx.PreferredBackBufferWidth * .01), (int)(gfx.PreferredBackBufferWidth * .01), "Yes", gfx);
                            chkNo.draw(spriteBatch, MenuBtn, (int)((gfx.PreferredBackBufferWidth / 2) + gfx.PreferredBackBufferWidth * .05), (int)(gfx.PreferredBackBufferHeight * .45), (int)(gfx.PreferredBackBufferWidth * .01), (int)(gfx.PreferredBackBufferWidth * .01), "No", gfx);

                            mnuBtnAcceptChanges.Draw(spriteBatch, MenuBtn, gfx, new Rectangle(startposx, startposy + Convert.ToInt32(gfx.PreferredBackBufferHeight * .45), width, height), "Accept", font);
                            mnuBtnCancel.Draw(spriteBatch, MenuBtn, gfx, new Rectangle(startposx, startposy + Convert.ToInt32(gfx.PreferredBackBufferHeight * .60), width, height), "Cancel", font);

                        }
                        else if (blnAudioOpen) { }
                        else if (blnGameplayOpen) { }
                        else if (blnOptionsOpen)
                        {
                            
                            if (mnuBtnVideo == null)
                            {
                                mnuBtnVideo = new MenuButton();
                                mnuBtnVideoBound = false;
                            }
                            if (mnuBtnAudio == null)
                            {
                                mnuBtnAudio = new MenuButton();
                                mnuBtnAudioBound = false;
                            }
                            if (mnuBtnGamePlay == null)
                            {
                                mnuBtnGamePlay = new MenuButton();
                                mnuBtnGamePlayBound = false;
                            }
                            if (mnuBtnBack == null)
                            {
                                mnuBtnBack = new MenuButton();
                                mnuBtnBackBound = false;
                            }
                            //keeps event from firing from previous menu, slowclick
                            if (oms.LeftButton == ButtonState.Released)
                            {
                                if (!mnuBtnVideoBound)
                                {
                                    mnuBtnVideo.ButtonClicked += mnuBtnVideo_Clicked;
                                    mnuBtnVideoBound = true;
                                }
                                if (!mnuBtnAudioBound)
                                {
                                    mnuBtnAudio.ButtonClicked += mnuBtnAudio_Clicked;
                                    mnuBtnAudioBound = true;
                                }
                                if (!mnuBtnGamePlayBound)
                                {
                                    mnuBtnGamePlay.ButtonClicked += mnuBtnGamePlay_Clicked;
                                    mnuBtnGamePlayBound = true;
                                }
                                if (!mnuBtnBackBound)
                                {
                                    mnuBtnBack.ButtonClicked += mnuBtnBack_Clicked;
                                    mnuBtnBackBound = true;
                                }
                            }

                            mnuBtnVideo.Draw(spriteBatch, MenuBtn, gfx, new Rectangle(startposx, startposy, width, height), "Video", font);
                            mnuBtnAudio.Draw(spriteBatch, MenuBtn, gfx, new Rectangle(startposx, startposy + Convert.ToInt32(gfx.PreferredBackBufferHeight * .15), width, height), "Audio", font);
                            mnuBtnGamePlay.Draw(spriteBatch, MenuBtn, gfx, new Rectangle(startposx, startposy + Convert.ToInt32(gfx.PreferredBackBufferHeight * .30), width, height), "Gameplay", font);
                            mnuBtnBack.Draw(spriteBatch, MenuBtn, gfx, new Rectangle(startposx, startposy + Convert.ToInt32(gfx.PreferredBackBufferHeight * .45), width, height), "Back", font);
                        }
                    }

                    break;

                //Draw Confirmation of Resolution Menu
                case 2:

                    oms = ms;
                    ms = Mouse.GetState();

                    Rect = new Rectangle(((int)(gfx.PreferredBackBufferWidth/2) - (int)(gfx.PreferredBackBufferWidth * .3)), ((int)(gfx.PreferredBackBufferHeight/2) - (int)(gfx.PreferredBackBufferHeight * .25)), (int)(gfx.PreferredBackBufferWidth * .55), (int)(gfx.PreferredBackBufferHeight * .30));

                    spriteBatch.Begin();
                    spriteBatch.Draw(MenuBtn, Rect, Color.SlateGray);
                    spriteBatch.End();

                    if (mnuBtnConfirm == null)
                    {
                        mnuBtnConfirm = new MenuButton();
                        mnuBtnConfirmBound = false;
                    }
                    if (mnuBtnRevert == null)
                    {
                        mnuBtnRevert = new MenuButton();
                        mnuBtnRevertBound = false;
                    }

                    if (!mnuBtnConfirmBound)
                    {
                        mnuBtnConfirm.ButtonClicked += mnuBtnConfirm_Clicked;
                        mnuBtnConfirmBound = true;
                    }
                    if (!mnuBtnRevertBound)
                    {
                        mnuBtnRevert.ButtonClicked += mnuBtnRevert_Clicked;
                        mnuBtnRevertBound = true;
                    }

                    spriteBatch.Begin();

                    spriteBatch.DrawString(font, "Keep Changes?", new Vector2((Rect.X + (int)(gfx.PreferredBackBufferWidth * .20)), ((Rect.Y) + (int)(gfx.PreferredBackBufferHeight * .01))), Color.WhiteSmoke);

                    spriteBatch.End();

                    mnuBtnConfirm.Draw(spriteBatch, MenuBtn, gfx, new Rectangle(Rect.X + (int)(gfx.PreferredBackBufferWidth * .1), Rect.Y + (int)(gfx.PreferredBackBufferHeight * .12), (int)(Rect.Width * .3), (int)(Rect.Height * .2)), "Confirm", font);
                    mnuBtnRevert.Draw(spriteBatch, MenuBtn, gfx, new Rectangle(Rect.X + (int)(gfx.PreferredBackBufferWidth * .3), Rect.Y + (int)(gfx.PreferredBackBufferHeight * .12), (int)(Rect.Width * .3), (int)(Rect.Height * .2)), "Revert", font);

                    break;

            }
        }

        private void ChkYes_Checked(object sender, EventArgs e)
        {
            if (chkNo.IsChecked())
            {
                chkNo.Uncheck();
            }
            GlobalVariables.FullScreen = true;
        }

        private void ChkNo_Checked(object sender, EventArgs e)
        {
            if (chkYes.IsChecked())
            {
                chkYes.Uncheck();
            }
            GlobalVariables.FullScreen = false;
        }

        private void mnuBtnRevert_Clicked(object sender, EventArgs e)
        {
            if (RevertChange != null)
            {
                RevertChange(this, EventArgs.Empty);
            }
        }

        private void mnuBtnConfirm_Clicked(object sender, EventArgs e)
        {
            if (ConfirmChange != null)
            {
                ConfirmChange(this, EventArgs.Empty);
            }
        }

        private void mnuBtnAcceptChanges_Clicked(object sender, EventArgs e)
        {
            if (ResolutionChange != null)
            {
                ResolutionChange(this, EventArgs.Empty);
            }
        }

        private void mnuBtnCancel_Clicked(object sender, EventArgs e)
        {
            blnVideoOpen = false;
            blnOptionsOpen = true;
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
            DefaultPauseOpen = false;
            blnOptionsOpen = true;
        }

        public void mnuBtnVideo_Clicked(object sender, EventArgs eventArgs)
        {
            blnVideoOpen = true;
        }

        public void mnuBtnAudio_Clicked(object sneder, EventArgs eventArgs)
        {
            blnAudioOpen = true;
        }

        public void mnuBtnGamePlay_Clicked(object sender, EventArgs evenArgs)
        {
            blnGameplayOpen = true;
        }

        public void mnuBtnBack_Clicked(object sender, EventArgs eventArgs)
        {
            DefaultPauseOpen = true;
        }

    }
}
