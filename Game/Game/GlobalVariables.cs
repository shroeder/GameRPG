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
    public static class GlobalVariables
    {
        public static Texture2D CheckMark { get; set; }
        public static SpriteFont Font10 { get; set; }
        public static SpriteFont Font16 { get; set; }
        public static SpriteFont Font20 { get; set; }
        public static SpriteFont Font24 { get; set; }
        public static SpriteFont Font28 { get; set; }
        public static SpriteFont Font32 { get; set; }
        public static Game1 TheGame { get; set; }
        public static int NewHeight { get; set; }
        public static int NewWidth { get; set; }
        public static int NewestWidth { get; set; }
        public static int NewestHeight { get; set; }
        public static bool NewestFullScreen { get; set; }
        public static bool FullScreen { get; set; }
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
    }
}
