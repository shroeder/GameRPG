﻿using System;
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
using Microsoft.Xna.Framework.Storage;
using System.IO;
using System.Xml.Serialization;

namespace TextureAtlas
{
    public static class GlobalVariables
    {
        public static Texture2D CheckMark { get; set; }
        public static SpriteFont Font10 { get; set; }
        public static SpriteFont Font16 { get; set; }
        public static SpriteFont Font20 { get; set; }
        public static SpriteFont Font24 { get; set; }
        public static SpriteFont Font28 { get; set; }
        public static SpriteFont Font32 { get; set; }

        public static int UserSetWidth { get; set; }
        public static int UserSetHeight { get; set; }
        public static int ScreenWidth { get; set; }
        public static int ScreenHeight { get; set; }
        public static int OldWidth { get; set; }
        public static int OldHeight { get; set; }
        public static int NewWidth { get; set; }
        public static int NewHeight { get; set; }
        public static bool OldFullScreen { get; set; }
        public static bool FullScreen { get; set; }
        public static bool UserSetFullScreen { get; set; }
        public static bool ShowEnemyNames { get; set; }
        public static bool ShowEnemyBars { get; set; }
        public static bool ShowItemNames { get; set; }
        public static bool ShowEnemyDamage { get; set; }

        public static Game1 TheGame { get; set; }
        public static GraphicsDeviceManager gfx { get; set; }

        public static SpriteFont LargeFont
        {
            get
            {
                return AutoFont(gfx, 1);
            }
        }

        public static SpriteFont MediumFont
        {
            get
            {
                return AutoFont(gfx, 2);
            }
        }

        public static SpriteFont SmallFont
        {
            get
            {
                return AutoFont(gfx, 3);
            }
        }
        [Serializable]
        public struct SaveSettings
        {
            public int ResolutionWidth;
            public int ResolutionHeight;
            public bool IsFullScreen;
            public bool blnShowEnemyNames;
            public bool blnShowEnemyBars;
            public bool blnShowItemNames;
            public bool blnShowEnemyDamage;
        }

        public struct SaveGameData
        {

        }

        public static void SaveUserSettings()
        {
            SaveSettings data = new SaveSettings();

            data.ResolutionWidth = GlobalVariables.UserSetWidth;
            data.ResolutionHeight = GlobalVariables.UserSetHeight;
            data.IsFullScreen = gfx.IsFullScreen;
            data.blnShowEnemyBars = ShowEnemyBars;
            data.blnShowEnemyNames = ShowEnemyNames;
            data.blnShowItemNames = ShowItemNames;
            data.blnShowEnemyDamage = ShowEnemyDamage;

            IAsyncResult result1 = StorageDevice.BeginShowSelector(PlayerIndex.One, null, null);
            StorageDevice device = StorageDevice.EndShowSelector(result1);

            IAsyncResult result = device.BeginOpenContainer("UserSettings", null, null);
            result.AsyncWaitHandle.WaitOne();
            StorageContainer container = device.EndOpenContainer(result);
            result.AsyncWaitHandle.Close();
            string filename = "settings.sav";

            if (container.FileExists(filename))
            {
                container.DeleteFile(filename);
            }

            Stream stream = container.CreateFile(filename);
            XmlSerializer serializer = new XmlSerializer(typeof(SaveSettings));
            serializer.Serialize(stream, data);
            stream.Close();
            container.Dispose();

        }

        public static void LoadUserSettings()
        {
            IAsyncResult result1 = StorageDevice.BeginShowSelector(PlayerIndex.One, null, null);
            StorageDevice device = StorageDevice.EndShowSelector(result1);

            IAsyncResult result = device.BeginOpenContainer("UserSettings", null, null);
            result.AsyncWaitHandle.WaitOne();
            StorageContainer container = device.EndOpenContainer(result);
            result.AsyncWaitHandle.Close();
            string filename = "settings.sav";
            if (!container.FileExists(filename))
            {
                container.Dispose();
                return;
            }
            Stream stream = container.OpenFile(filename, FileMode.Open);
            XmlSerializer serializer = new XmlSerializer(typeof(SaveSettings));
            SaveSettings data = (SaveSettings)serializer.Deserialize(stream);
            stream.Close();
            container.Dispose();

            UserSetWidth = data.ResolutionWidth;
            UserSetHeight = data.ResolutionHeight;
            UserSetFullScreen = data.IsFullScreen;
            ShowEnemyBars = data.blnShowEnemyBars;
            ShowEnemyNames = data.blnShowEnemyNames;
            ShowItemNames = data.blnShowItemNames;
            ShowEnemyDamage = data.blnShowEnemyDamage;

        }

        public static SpriteFont AutoFont(GraphicsDeviceManager gfx, int scale)
        {

            switch (scale)
            {

                case 1:

                    if (gfx.PreferredBackBufferWidth > 1600)
                    {
                        return Font32;
                    }
                    else if (gfx.PreferredBackBufferWidth > 1200 && gfx.PreferredBackBufferWidth < 1601)
                    {
                        return Font28;
                    }
                    else if (gfx.PreferredBackBufferWidth > 1000 && gfx.PreferredBackBufferWidth < 1201)
                    {
                        return Font24;
                    }
                    else if (gfx.PreferredBackBufferWidth > 800 && gfx.PreferredBackBufferWidth < 1001)
                    {
                        return Font20;
                    }
                    else if (gfx.PreferredBackBufferWidth > 600 && gfx.PreferredBackBufferWidth < 801)
                    {
                        return Font16;
                    }
                    else if (gfx.PreferredBackBufferWidth > 0 && gfx.PreferredBackBufferWidth < 601)
                    {
                        return Font10;
                    }
                    else
                    {
                        return Font10;
                    }

                case 2:

                    if (gfx.PreferredBackBufferWidth > 1600)
                    {
                        return Font24;
                    }
                    else if (gfx.PreferredBackBufferWidth > 1200 && gfx.PreferredBackBufferWidth < 1601)
                    {
                        return Font20;
                    }
                    else if (gfx.PreferredBackBufferWidth > 1000 && gfx.PreferredBackBufferWidth < 1201)
                    {
                        return Font16;
                    }
                    else
                    {
                        return Font10;
                    }

                case 3:

                    if (gfx.PreferredBackBufferWidth > 1600)
                    {
                        return Font20;
                    }
                    else if (gfx.PreferredBackBufferWidth > 1200 && gfx.PreferredBackBufferWidth < 1601)
                    {
                        return Font16;
                    }
                    else
                    {
                        return Font10;
                    }


                default:

                    return Font10;
            }
        }

        public static Vector2 GetInterSect(Rectangle me, Rectangle them, string XY = "")
        {

            bool MeRight;
            bool MeAbove;

            Vector2 MeTopLeft = new Vector2(me.X,me.Y);
            Vector2 MeTopRight = new Vector2((me.X + me.Width),me.Y);
            Vector2 MeBottomLeft = new Vector2(me.X,(me.Y + me.Height));
            Vector2 MeBottomRight = new Vector2((me.X + me.Width ),(me.Y + me.Height));
            
            Vector2 ThemTopLeft = new Vector2(them.X,them.Y);
            Vector2 ThemTopRight = new Vector2((them.X + them.Width),them.Y);
            Vector2 ThemBottomLeft = new Vector2(them.X,(them.Y + them.Height));
            Vector2 ThemBottomRight = new Vector2((them.X + them.Width), (them.Y + them.Height));

            Vector2 ApplyValue = new Vector2(0,0);

            if (me.Y > them.Y)
            {
                MeAbove = false;
            }
            else
            {
                MeAbove = true;
            }
            if (me.X > them.X)
            {
                MeRight = true;
            }
            else
            {
                MeRight = false; 
            }

            if (MeRight && MeAbove)
            {
                ApplyValue = ThemTopRight - MeBottomLeft;
            }
            else if (MeRight && !MeAbove)
            {
                ApplyValue = ThemBottomRight - MeTopLeft;
            }
            else if (!MeRight && MeAbove)
            {
                ApplyValue = ThemTopLeft - MeBottomRight;
            }
            else if (!MeRight && !MeAbove)
            {
                ApplyValue = ThemBottomLeft - MeTopRight;
            }

            if (XY == "x")
            {
                ApplyValue.Y = 0;
            }
            else if (XY == "y")
            {
                ApplyValue.X = 0;
            }

            return ApplyValue;
        }
    }
}
