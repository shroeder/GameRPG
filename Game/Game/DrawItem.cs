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
    public class DrawItem
    {
        public string theText { get; set; }
        public int CaseOfDraw { get; set; }
        public Texture2D theTexture { get; set; }
        public SpriteFont theFont { get; set; }
        public Vector2 theLocation { get; set; }
        public Rectangle theRectangle { get; set; }
        public Color theColor { get; set; }

    }
}
