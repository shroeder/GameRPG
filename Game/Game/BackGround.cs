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
    class BackGround
    {

        public void Draw(SpriteBatch spriteBatch, GraphicsDeviceManager gfx, Texture2D BackgroundImage, int Rows, int Columns, int TileHeight, int TileWidth, Vector2 OffSet)
        {

            for (int i = 0; i < Rows; i++){
                for (int x = 0; x < Columns; x++){
                    spriteBatch.Draw(BackgroundImage, new Rectangle((i * TileWidth) + Convert.ToInt32(OffSet.X), (x * TileHeight) + Convert.ToInt32(OffSet.Y), TileWidth, TileHeight), Color.White);
                }
            }
        }

    }
}
