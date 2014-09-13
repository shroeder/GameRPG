using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
namespace TextureAtlas
{
    public class AnimatedSprite
    {
        #region Variables
        //Variables
        public Texture2D Texture { get; set; }
        //Rows and Columns in Graphic Atlas for Character Image
        public int Rows { get; set; }
        public int Columns { get; set; }
        //Frame of Movement within the Character Atlas
        private int currentFrame;
        private int totalFrames;
        //Variables Controlling the Speed at which I iterate through Character Atlas(walking)
        private int currentUpdate;
        private int updatesPerFrame = 5;
        //Variable for Character's position on Screen
        public Vector2 position;
        //Variable for Drawing of Character
        public Rectangle spriteRectangle;
        //Variable for Drawing Weapon
        public Texture2D Sword;
        public Texture2D Sword2;
        //Boolean Controlling Whether Char can attack or not
        public bool attack = false;
        //Rotation of Sword(Swinging)
        public float i;
        //Variable to determine character's direction faces(passed in via game1)
        public int direction;
        //Variable for Position of Sword Tip
        public Vector2 SwordTip;
        //Variable for Sword Tip Collision Decrement/Increment
        public float l = 2.5f;
        //Boolean to Display damage after full weapon swing
        public bool blnDisplaydamage = false;
        //Variable for World Position of Character
        public Vector2 WorldPos;

        //Generic Variable for Screen Size(Used to find Center of Screen)
        public int screenWidth
        {
            get { return GraphicsDeviceManager.DefaultBackBufferWidth; }
        }

        public int screenHeight
        {
            get { return GraphicsDeviceManager.DefaultBackBufferHeight; }
        }
        //Character Properties
        public AnimatedSprite(Texture2D texture, Texture2D sword, Texture2D sword2,bool Attack, int dir, int rows, int columns, Vector2 Location)
        {
            //Set Character Properties to those Passed in from the Game Class
            WorldPos = Location;
            Sword = sword;
            attack = Attack;
            direction = dir;
            Texture = texture;
            Rows = rows;
            Columns = columns;
            currentFrame = 0;
            totalFrames = Rows * Columns;
            Sword2 = sword2;
            SwordTip = position;
        }
        #endregion

        #region Updates

        //Character Moves Left

        public void UpdateLeft()
        {
            //Iteration Through the Graphic Tiles of Character Walking
            currentUpdate++;
            //Check to See if number of Update Cycles has Been Reached(Controls Speed at which Character appears to be walking)
            if (currentUpdate == updatesPerFrame)
            {
                //Set First Stage of Walking
                currentUpdate = 0;
                //Check to See if Stage is out of Bounds for this Direction(Left)
                if (currentFrame < 4 || currentFrame > 8)
                    //Set Stage to beginning of walk cycle
                    currentFrame = 4;
                //Update Character Walk Cycle Stage
                currentFrame++;
                //Check to See if Character Reached Last Stage of Walk Cycle
                if (currentFrame == 8)
                    //Reset to Beginning of Walk Cycle
                    currentFrame = 4;
            }
        }

        public void UpdateRight()
        {
            currentUpdate++;
            if (currentUpdate == updatesPerFrame)
            {
                currentUpdate = 0;
                if (currentFrame < 8 || currentFrame > 12)
                    currentFrame = 8;
                currentFrame++;
                if (currentFrame == 12)
                    currentFrame = 8;
            }
        }

        public void UpdateDown()
        {
            currentUpdate++;
            if (currentUpdate == updatesPerFrame)
            {
                currentUpdate = 0;
                if (currentFrame > 4)
                    currentFrame = 0;
                currentFrame++;
                if (currentFrame == 4)
                    currentFrame = 0;
            }
        }

        public void UpdateUp()
        {
            currentUpdate++;
            if (currentUpdate == updatesPerFrame)
            {
                currentUpdate = 0;
                if (currentFrame < 13)
                    currentFrame = 13;
                currentFrame++;
                if (currentFrame == 16)
                    currentFrame = 13;
            }
        }

#endregion

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            blnDisplaydamage = true;
            //Width of Texture Atlas
            int width = Texture.Width / Columns;
            //Height of Texture Atlas
            int height = Texture.Height / Rows;
            //Width of Individual Tile in Texture Atlas
            int row = (int)((float)currentFrame / (float)Columns);
            //Height of Individual Tile in Texture Atlas
            int column = currentFrame % Columns;
            //Width for Sword Texture
            int width1 = Sword.Width;
            //height for Sword Texture
            int height1 = Sword.Height;
            //Size of Entire Texture Atlas
            Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
            //Size of the Individual Graphic Tile Within Texture Atlas
            Rectangle destinationRectangle = new Rectangle((int)location.X, (int)location.Y, width, height);
            //Size of Recation for Sword Graphic
            Rectangle DRect = new Rectangle((int)location.X, (int)location.Y, width1, height1);
            //Begin Draw
            spriteBatch.Begin();
            //Draw Character Graphic to Screen
            spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White);
            #region DrawAttack
            //Check to see if Attack is allowed
            if (attack)
            {
                //Direction Check for which way character is facing
                //Down
                if (direction == 1)
                {
                    //Check Iteration in Rotation of Swing
                    if (i >= 0 && i <= 2)
                    {
                        //Draw Sword at the rotation of i
                        spriteBatch.Draw(Sword, new Rectangle((int)location.X + 40, (int)location.Y + 80, width1, height1), null, Color.Red, i, Vector2.Zero, SpriteEffects.None, 0);
                        //Increment the rotation of the sword
                        i += .2f;
                        SwordTip.X = ((location.X + 80) - l);
                        SwordTip.Y = (location.Y + 135);
                        l += (l / 2);
                    }
                    //Check to See if the Animation is finished(Reached end of allowed Roation)
                    if (i > 2)
                    {
                        //Set Attack to False
                        attack = false;
                        //Return Rotation to Beginning of Range
                        i = 0f;
                        l = 2.5f;
                        blnDisplaydamage = true;
                    }
                }
                //Left
                if (direction == 2)
                {
                    if (i <= 2 && i >= 0)
                    {
                        spriteBatch.Draw(Sword2, new Rectangle((int)location.X + 40, (int)location.Y + 80, width1, height1), null, Color.Red, i, new Vector2((Vector2.Zero.X + Sword2.Width),Vector2.Zero.Y), SpriteEffects.None, 0);
                        i -= .2f;
                        SwordTip.X = (location.X - 25);
                        SwordTip.Y = (location.Y + l);
                        l += (l / 2);
                    }
                    if (i < 0)
                    {
                        attack = false;
                        i = 2f;
                        l = 2.5f;
                        blnDisplaydamage = true;
                    }
                }
                //Right
                if (direction == 3)
                {
                    if (i >= -2 && i <= 0)
                    {
                        spriteBatch.Draw(Sword, new Rectangle((int)location.X + 40, (int)location.Y + 80, width1, height1), null, Color.Red, i, Vector2.Zero, SpriteEffects.None, 0);
                        i += .2f;
                        SwordTip.X = (location.X + 90);
                        SwordTip.Y = (location.Y + l);
                        l += (l / 2);
                    }
                    if (i > 0)
                    {
                        attack = false;
                        i = -2f;
                        l = 2.5f;
                        blnDisplaydamage = true;
                    }
                }
                //Up
                if (direction == 4)
                {
                    if (i <= 4 && i >= 2)
                    {
                        spriteBatch.Draw(Sword2, new Rectangle((int)location.X + 40, (int)location.Y + 80, width1, height1), null, Color.Red, i, new Vector2((Vector2.Zero.X + Sword2.Width), Vector2.Zero.Y), SpriteEffects.None, 0);
                        i -= .2f;
                        SwordTip.X = ((location.X + 80) - l);
                        SwordTip.Y = (location.Y);
                        l += (l / 2);
                    }
                    if (i < 2)
                    {
                        attack = false;
                        i = 4f;
                        l = 2.5f;
                        blnDisplaydamage = true;
                    }
                }
            }
            #endregion
            spriteBatch.End();
        }

    }
}

   