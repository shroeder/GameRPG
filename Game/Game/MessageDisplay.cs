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
    class MessageDisplay
    {

        public float tmrDSP = 0;
        public string Message;
        public SpriteFont font;

        public void draw(SpriteBatch spriteBatch, GameTime gameTime, string DisplayMessage, SpriteFont font1)
        {
            font = font1;
            Message = DisplayMessage;
            if (tmrDSP < 1500f)
            {
                spriteBatch.DrawString(font1, Message, new Vector2(250, 0), Color.White);
                tmrDSP += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            }

        }
    }
}
