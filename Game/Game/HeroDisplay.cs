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
using System.Timers;
using System.IO;
using System.Xml.Serialization;
using Microsoft.Xna.Framework.Storage;

namespace TextureAtlas
{
    public class HeroDisplay
    {

        public Texture2D txtHero;
        public Texture2D txtLeftWeapon;
        public Texture2D txtRightWeapon;
        public Texture2D txtBoots;
        public Texture2D txtShoulders;
        public Texture2D txtGloves;
        public Texture2D txtChest;
        public Texture2D txtPants;
        public Texture2D txtLeftRing;
        public Texture2D txtRightRing;
        public Texture2D txtBelt;
        public Texture2D txtHelm;
        public Rectangle DrawLocation;
        public Rectangle RotateClockBounds;
        public Rectangle RotateCounterBounds;
        public int Direction;
        private int currentFrame;
        public int Width;
        public int Height;
        public int Rows = 8;
        public int Columns = 6;

        public Item Helmet;
        public Item Shoulders;
        public Item Chest;
        public Item Pants;
        public Item RightWeapon;
        public Item LeftWeapon;
        public Item Gloves;
        public Item Boots;
        public Item Belt;
        public Item LeftRing;
        public Item RightRing;

        public event EventHandler RotateCounter;
        public event EventHandler RotateClock;

        public HeroDisplay() : this(true) { }

        public HeroDisplay(bool isNull = false)
        {

        }

        public void Update()
        {
            Rectangle RectMouse = new Rectangle(GlobalVariables.TheGame.mouseState.X, GlobalVariables.TheGame.mouseState.Y, 1, 1);
            if (RectMouse.Intersects(RotateCounterBounds) && GlobalVariables.TheGame.mouseState.LeftButton == ButtonState.Pressed && GlobalVariables.TheGame.MoldState.LeftButton == ButtonState.Released)
            {
                if (RotateCounter != null)
                {
                    RotateCounter(this, EventArgs.Empty);
                }
            }
            else if (RectMouse.Intersects(RotateClockBounds) && GlobalVariables.TheGame.mouseState.LeftButton == ButtonState.Pressed && GlobalVariables.TheGame.MoldState.LeftButton == ButtonState.Released)
            {
                if (RotateClock != null)
                {
                    RotateClock(this, EventArgs.Empty);
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //down
            if (Direction == 0)
            {
                currentFrame = 4;
            }
            //Right
            if (Direction == 1)
            {
                currentFrame = 10;
            }
            //up
            if (Direction == 2)
            {
                currentFrame = 22;
            }
            //left
            if (Direction == 3)
            {
                currentFrame = 16;
            }

            int width = txtHero.Width / Columns;
            int height = txtHero.Height / Rows;
            int row = (int)((float)currentFrame / (float)Columns);
            int column = currentFrame % Columns;
            Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
            spriteBatch.Draw(txtHero, DrawLocation, sourceRectangle, Color.White);

            spriteBatch.Draw(GlobalVariables.RotateCounter, RotateCounterBounds, Color.White);
            spriteBatch.Draw(GlobalVariables.RotateClock, RotateClockBounds, Color.White);

            if (GlobalVariables.TheGame.equipment.Boots != null)
            {
                spriteBatch.Draw(txtBoots, DrawLocation, sourceRectangle, GlobalVariables.TheGame.equipment.Boots.ItemColor);
            }
            if (GlobalVariables.TheGame.equipment.Pants != null)
            {
                spriteBatch.Draw(txtPants, DrawLocation, sourceRectangle, GlobalVariables.TheGame.equipment.Pants.ItemColor);
            }
            if (GlobalVariables.TheGame.equipment.Helmet != null)
            {
                spriteBatch.Draw(txtHelm, DrawLocation, sourceRectangle, GlobalVariables.TheGame.equipment.Helmet.ItemColor);
            }
            if (GlobalVariables.TheGame.equipment.Chest != null)
            {
                spriteBatch.Draw(txtChest, DrawLocation, sourceRectangle, GlobalVariables.TheGame.equipment.Chest.ItemColor);
            }
            if (GlobalVariables.TheGame.equipment.Shoulders != null)
            {
                spriteBatch.Draw(txtShoulders, DrawLocation, sourceRectangle, GlobalVariables.TheGame.equipment.Shoulders.ItemColor);
            }
            //if (GlobalVariables.TheGame.equipment.LeftRing != null)
            //{
            //    spriteBatch.Draw(txtLeftRing, DrawLocation, sourceRectangle, GlobalVariables.TheGame.equipment.LeftRing.ItemColor);
            //}
            //if (GlobalVariables.TheGame.equipment.RightRing != null)
            //{
            //    spriteBatch.Draw(txtRightRing, DrawLocation, sourceRectangle, GlobalVariables.TheGame.equipment.RightRing.ItemColor);
            //}
            if (GlobalVariables.TheGame.equipment.Gloves != null)
            {
                spriteBatch.Draw(txtGloves, DrawLocation, sourceRectangle, GlobalVariables.TheGame.equipment.Gloves.ItemColor);
            }
            //if (GlobalVariables.TheGame.equipment.Belt != null)
            //{
            //    spriteBatch.Draw(txtBelt, DrawLocation, sourceRectangle, GlobalVariables.TheGame.equipment.Belt.ItemColor);
            //}
            if (GlobalVariables.TheGame.equipment.RightWeapon != null)
            {
                spriteBatch.Draw(txtRightWeapon, DrawLocation, sourceRectangle, GlobalVariables.TheGame.equipment.RightWeapon.ItemColor);
            }
            if (GlobalVariables.TheGame.equipment.LeftWeapon != null)
            {
                spriteBatch.Draw(txtLeftWeapon, DrawLocation, sourceRectangle, GlobalVariables.TheGame.equipment.LeftWeapon.ItemColor);
            }
        }
    }
}
