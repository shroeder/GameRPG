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
    class Equipment
    {   
        //Variable
        public SpriteFont font1;
        public List<Item> Items = new List<Item>();
        public Texture2D CharBG;
        public Equipment(List<Item> ListItems)
        {
            Items = ListItems;
        }

        public void draw(SpriteBatch spriteBatach, Texture2D EquipSheet, SpriteFont font)
        {
            font1 = font;
            CharBG = EquipSheet;

            spriteBatach.Draw(CharBG, new Rectangle((int)(GlobalVariables.gfx.PreferredBackBufferWidth * .3), (int)(GlobalVariables.gfx.PreferredBackBufferHeight * .1), (int)(GlobalVariables.gfx.PreferredBackBufferWidth * .6), (int)(GlobalVariables.gfx.PreferredBackBufferHeight * .6)), Color.White);
        }
    }
}
