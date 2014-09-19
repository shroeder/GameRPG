using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
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
using Microsoft.Xna.Framework.Storage;

namespace TextureAtlas
{
    class PauseMenu
    {
        public bool blnCanBind = false;
        public bool blnIndexValues = false;
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
        private bool mnuGamePlayConfirmBound = false;
        private bool mnuGamePlayCancelBound = false;
        private bool StartTimer = false;

        //Have to use a timer for when clicked the options button to prevent the audio button from clicking immediately
        //optimize later for sure, for now its 250 ms which ensures a few updates take place before allowing the 
        //audio event to have a handler
        public Stopwatch tmr = new Stopwatch();

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
        MenuButton mnuGamePlayConfirm;
        MenuButton mnuGamePlayCancel;

        CheckBox chkYes;
        CheckBox chkNo;
        CheckBox chkShowItemNames;
        CheckBox chkShowEnemyNames;
        CheckBox chkShowEnemyBars;
        CheckBox chkShowEnemyDamage;

        Slider slider = new Slider();
        Slider slider1 = new Slider();

        public void Update()
        {
            oms = ms;
            ms = Mouse.GetState();
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D img, Texture2D MenuBtn, SpriteFont font, GraphicsDeviceManager gfx, SpriteFont smallfont, int MenuType)
        {
            if (StartTimer)
            {
                StartTimer = false;
                tmr.Start();
            }
            if (tmr.ElapsedMilliseconds > 250)
            {
                tmr.Stop();
                tmr.Reset();
                blnCanBind = true;
            }
            switch (MenuType)
            {

                //Draw PauseMenu
                case 1:

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
                            mnuBtnOptionsBound = true;
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
                            if (chkYes == null)
                            {
                                chkYes = new CheckBox();
                                blnChkYesBound = false;
                            }
                            if (chkNo == null)
                            {
                                chkNo = new CheckBox();
                                chkNoBound = false;
                            }

                            if (blnIndexValues)
                            {
                                if (GlobalVariables.UserSetFullScreen)
                                {
                                    chkYes.Check();
                                    chkNo.Uncheck();
                                }
                                else
                                {
                                    chkNo.Check();
                                    chkYes.Uncheck();
                                }
                                double ht;
                                double wdt;
                                if (GlobalVariables.UserSetHeight > 0)
                                {
                                    ht = Convert.ToDouble(GlobalVariables.UserSetHeight);
                                }
                                else
                                {
                                    ht = Convert.ToDouble(GlobalVariables.ScreenHeight);
                                }
                                if (GlobalVariables.UserSetWidth > 0)
                                {
                                    wdt = Convert.ToDouble(GlobalVariables.UserSetWidth);
                                }
                                else
                                {
                                    wdt = Convert.ToDouble(GlobalVariables.ScreenWidth);
                                }
                                double dbl = (wdt / ht);
                                string ratio = Convert.ToString(dbl);
                                if (ratio.Contains("1.33333"))
                                {
                                    slider1.Index = 0;
                                }
                                else if (ratio.Contains("1.77777"))
                                {
                                    slider1.Index = 1;
                                }
                                else if (ratio.Contains("1.6"))
                                {
                                    slider1.Index = 2;
                                }
                                string sliderRes = Convert.ToString(GlobalVariables.UserSetWidth) + " X " + Convert.ToString(GlobalVariables.UserSetHeight);
                                int i = 0;
                                foreach (string str in Resolution_43)
                                {
                                    if (str == sliderRes)
                                    {
                                        slider.Index = i;
                                    }
                                    i += 1;
                                }
                                i = 0;
                                foreach (string str in Resolution_169)
                                {
                                    if (str == sliderRes)
                                    {
                                        slider.Index = i;
                                    }
                                    i += 1;
                                }
                                i = 0;
                                foreach (string str in Resolution_1610)
                                {
                                    if (str == sliderRes)
                                    {
                                        slider.Index = i;
                                    }
                                    i += 1;
                                }
                                blnIndexValues = false;
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

                            spriteBatch.Begin();

                            spriteBatch.DrawString(font, "FullScreen", new Vector2(startposx + ((float)(gfx.PreferredBackBufferWidth * .1) - (float)(TextWidth * .25)), startposy + (int)(gfx.PreferredBackBufferHeight * .22)), Color.Gray);

                            spriteBatch.End();

                            chkYes.draw(spriteBatch, MenuBtn, (int)((gfx.PreferredBackBufferWidth / 2) - gfx.PreferredBackBufferWidth * .10), (int)(gfx.PreferredBackBufferHeight * .45), (int)(gfx.PreferredBackBufferWidth * .01), (int)(gfx.PreferredBackBufferWidth * .01), "Yes", gfx);
                            chkNo.draw(spriteBatch, MenuBtn, (int)((gfx.PreferredBackBufferWidth / 2) + gfx.PreferredBackBufferWidth * .05), (int)(gfx.PreferredBackBufferHeight * .45), (int)(gfx.PreferredBackBufferWidth * .01), (int)(gfx.PreferredBackBufferWidth * .01), "No", gfx);

                            mnuBtnAcceptChanges.Draw(spriteBatch, MenuBtn, gfx, new Rectangle(startposx, startposy + Convert.ToInt32(gfx.PreferredBackBufferHeight * .45), width, height), "Accept", font);
                            mnuBtnCancel.Draw(spriteBatch, MenuBtn, gfx, new Rectangle(startposx, startposy + Convert.ToInt32(gfx.PreferredBackBufferHeight * .60), width, height), "Back", font);

                        }
                        else if (blnAudioOpen) { }
                        else if (blnGameplayOpen)
                        {
                            if (mnuGamePlayConfirm == null)
                            {
                                mnuGamePlayConfirm = new MenuButton();
                                mnuGamePlayConfirmBound = false;
                            }
                            if (mnuGamePlayCancel == null)
                            {
                                mnuGamePlayCancel = new MenuButton();
                                mnuGamePlayCancelBound = false;
                            }
                            if (!mnuGamePlayCancelBound)
                            {
                                mnuGamePlayCancel.ButtonClicked += mnuGamePlayCancel_Clicked;
                                mnuGamePlayCancelBound = true;
                            }
                            if (!mnuGamePlayConfirmBound)
                            {
                                mnuGamePlayConfirm.ButtonClicked += mnuGamePlayConfirm_Clicked;
                                mnuGamePlayConfirmBound = true;
                            }
                            if (chkShowItemNames == null)
                            {
                                chkShowItemNames = new CheckBox();
                            }
                            if (chkShowEnemyBars == null)
                            {
                                chkShowEnemyBars = new CheckBox();
                            }
                            if (chkShowEnemyNames == null)
                            {
                                chkShowEnemyNames = new CheckBox();
                            }
                            if (chkShowEnemyDamage == null)
                            {
                                chkShowEnemyDamage = new CheckBox();
                            }
                            SpriteFont setFont = GlobalVariables.LargeFont;
                            chkShowItemNames.draw(spriteBatch, MenuBtn, (int)(gfx.PreferredBackBufferWidth / 2) + (int)(gfx.PreferredBackBufferWidth * .065), (int)(gfx.PreferredBackBufferHeight * .15), (int)(gfx.PreferredBackBufferWidth * .01), (int)(gfx.PreferredBackBufferWidth * .01), "Show Item Names", gfx,setFont);
                            chkShowEnemyNames.draw(spriteBatch, MenuBtn, (int)(gfx.PreferredBackBufferWidth / 2) + (int)(gfx.PreferredBackBufferWidth * .065), (int)(gfx.PreferredBackBufferHeight * .25), (int)(gfx.PreferredBackBufferWidth * .01), (int)(gfx.PreferredBackBufferWidth * .01), "Show Enemy Names", gfx, setFont);
                            chkShowEnemyBars.draw(spriteBatch, MenuBtn, (int)(gfx.PreferredBackBufferWidth / 2) + (int)(gfx.PreferredBackBufferWidth * .065), (int)(gfx.PreferredBackBufferHeight * .35), (int)(gfx.PreferredBackBufferWidth * .01), (int)(gfx.PreferredBackBufferWidth * .01), "Show Enemy HP Bars", gfx, setFont);
                            chkShowEnemyDamage.draw(spriteBatch, MenuBtn, (int)(gfx.PreferredBackBufferWidth / 2) + (int)(gfx.PreferredBackBufferWidth * .065), (int)(gfx.PreferredBackBufferHeight * .45), (int)(gfx.PreferredBackBufferWidth * .01), (int)(gfx.PreferredBackBufferWidth * .01), "Show Enemy Damage", gfx, setFont);
                            mnuGamePlayConfirm.Draw(spriteBatch, MenuBtn, gfx, new Rectangle(startposx, startposy + Convert.ToInt32(gfx.PreferredBackBufferHeight * .45), width, height), "Confirm", font);
                            mnuGamePlayCancel.Draw(spriteBatch, MenuBtn, gfx, new Rectangle(startposx, startposy + Convert.ToInt32(gfx.PreferredBackBufferHeight * .60), width, height), "Back", font);
                            if (blnIndexValues)
                            {
                                if (GlobalVariables.ShowItemNames)
                                {
                                    chkShowItemNames.Check();
                                }
                                else
                                {
                                    chkShowItemNames.Uncheck();
                                }
                                if (GlobalVariables.ShowEnemyBars)
                                {
                                    chkShowEnemyBars.Check();
                                }
                                else
                                {
                                    chkShowEnemyBars.Uncheck();
                                }
                                if (GlobalVariables.ShowEnemyNames)
                                {
                                    chkShowEnemyNames.Check();
                                }
                                else
                                {
                                    chkShowEnemyNames.Uncheck();
                                }
                                if (GlobalVariables.ShowEnemyDamage)
                                {
                                    chkShowEnemyDamage.Check();
                                }
                                else
                                {
                                    chkShowEnemyDamage.Uncheck();
                                }
                            }
                            blnIndexValues = false;
                        }
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

                            if (blnCanBind)
                            {
                                blnCanBind = false;
                                if (!mnuBtnAudioBound)
                                {
                                    mnuBtnAudio.ButtonClicked += mnuBtnAudio_Clicked;
                                    mnuBtnAudioBound = true;
                                }
                            }

                            if (!mnuBtnVideoBound)
                            {
                                mnuBtnVideo.ButtonClicked += mnuBtnVideo_Clicked;
                                mnuBtnVideoBound = true;
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

                    Rect = new Rectangle(((int)(gfx.PreferredBackBufferWidth / 2) - (int)(gfx.PreferredBackBufferWidth * .3)), ((int)(gfx.PreferredBackBufferHeight / 2) - (int)(gfx.PreferredBackBufferHeight * .25)), (int)(gfx.PreferredBackBufferWidth * .55), (int)(gfx.PreferredBackBufferHeight * .30));

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

        private void mnuGamePlayConfirm_Clicked(object sender, EventArgs e)
        {
            if (chkShowEnemyBars.IsChecked())
            {
                GlobalVariables.ShowEnemyBars = true;
            }
            else
            {
                GlobalVariables.ShowEnemyBars = false;
            }
            if (chkShowEnemyNames.IsChecked())
            {
                GlobalVariables.ShowEnemyNames = true;
            }
            else
            {
                GlobalVariables.ShowEnemyNames = false;
            }
            if (chkShowItemNames.IsChecked())
            {
                GlobalVariables.ShowItemNames = true;
            }
            else
            {
                GlobalVariables.ShowItemNames = false;
            }
            if (chkShowEnemyDamage.IsChecked())
            {
                GlobalVariables.ShowEnemyDamage = true;
            }
            else
            {
                GlobalVariables.ShowEnemyDamage = false;
            }
            GlobalVariables.SaveUserSettings();
            blnGameplayOpen = false;
            blnOptionsOpen = true;
        }

        private void mnuGamePlayCancel_Clicked(object sender, EventArgs e)
        {
            blnGameplayOpen = false;
            blnOptionsOpen = true;
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
            StartTimer = true;
        }

        public void mnuBtnVideo_Clicked(object sender, EventArgs eventArgs)
        {
            blnIndexValues = true;
            blnVideoOpen = true;
        }

        public void mnuBtnAudio_Clicked(object sneder, EventArgs eventArgs)
        {
            blnAudioOpen = true;
        }

        public void mnuBtnGamePlay_Clicked(object sender, EventArgs evenArgs)
        {
            blnIndexValues = true;
            blnGameplayOpen = true;
        }

        public void mnuBtnBack_Clicked(object sender, EventArgs eventArgs)
        {
            mnuBtnOptions.ButtonClicked -= mnuBtnVideo_Clicked;
            DefaultPauseOpen = true;
        }
    }
}
