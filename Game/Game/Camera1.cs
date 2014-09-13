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

namespace TextureAtlas
{
    class Camera1
    {
        public Matrix transform;
        Viewport view;
        Vector2 centre;

        public Camera1(Viewport newView)
        {
            view = newView;
        }

        public void Update(GameTime gameTime, Game1 ship)
        {
            centre = new Vector2(ship.position.X + (35), ship.position.Y + (35));
            transform = Matrix.CreateScale(new Vector3(1, 1, 0)) * Matrix.CreateTranslation(new Vector3(-centre.X, -centre.Y, 0));
        }
    }
}
