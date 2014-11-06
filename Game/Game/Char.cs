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
        public enum DirToAttack
        {
            Up,
            Down,
            Left,
            Right,
            Nothing
        }

        public DirToAttack Direction = DirToAttack.Nothing;

        public Rectangle Bounds;
        public Rectangle SwordBounds;
        public Rectangle spriteRectangle;

        public Texture2D Texture { get; set; }
        public Texture2D CharWeapon { get; set; }

        public Texture2D CharBoots { get; set; }
        public Texture2D CharGloves { get; set; }
        public Texture2D CharHelm { get; set; }
        public Texture2D CharShoulders { get; set; }
        public Texture2D CharChest { get; set; }
        public Texture2D CharPants { get; set; }

        public Vector2 position;
        public Vector2 SwordTip;
        public Vector2 WorldPos;

        public int Width;
        public int Height;
        public int direction;
        public int Rows { get; set; }
        public int Columns { get; set; }
        private int currentFrame;
        private int totalFrames;
        private int currentUpdate;
        private int updatesPerFrame = 10;

        public bool IsAttacking = false;
        public bool attack = false;
        public bool attack1 = false;
        public bool FinishAttack = false;
        public bool blnDisplaydamage = false;

        public float i;
        public float l = 2.5f;

        public AnimatedSprite(Texture2D texture, int dir, int rows, int columns, Vector2 Location, Texture2D charweapon = null)
        {
            if (GlobalVariables.CharacterLevel <= 0)
            {
                GlobalVariables.CharacterLevel = 1;
            }
            
            Width = Convert.ToInt32(texture.Width / columns);
            Height = Convert.ToInt32(texture.Height / rows);
            position = Location;
            WorldPos = Location;
            direction = dir;
            if (charweapon != null)
            {
                CharWeapon = charweapon;
            }
            Texture = texture;
            Rows = rows;
            Columns = columns;
            currentFrame = 0;
            totalFrames = Rows * Columns;
            SwordTip = position;
        }

        public void UpdateLeft()
        {
            currentUpdate++;
            if (currentUpdate >= updatesPerFrame)
            {
                currentUpdate = 0;
                if (currentFrame < 12 || currentFrame > 16)
                    currentFrame = 12;
                currentFrame++;
                if (currentFrame == 16)
                    currentFrame = 12;
            }
        }

        public void UpdateRight()
        {
            currentUpdate++;
            if (currentUpdate == updatesPerFrame)
            {
                currentUpdate = 0;
                if (currentFrame < 6 || currentFrame > 10)
                    currentFrame = 6;
                currentFrame++;
                if (currentFrame == 10)
                    currentFrame = 6;
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
                if (currentFrame < 18)
                    currentFrame = 18;
                currentFrame++;
                if (currentFrame == 22)
                    currentFrame = 18;
            }
        }

        public void UpdateDownRight() { }

        public void UpdateDownLeft() { }

        public void UpdateUpRight() { }

        public void UpdateUpLeft() { }

        public void AnimateAttack(Rectangle EnemyBounds)
        {
            attack = true;
            attack1 = true;

        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            if (attack)
            {
                currentUpdate++;
                if (currentUpdate >= 6)
                {
                    currentUpdate = 0;
                    if (currentFrame > -1 && currentFrame < 6)
                    {
                        //Down
                        if (attack1)
                        {
                            currentFrame = 4;
                            attack1 = false;
                            IsAttacking = true;
                        }
                        else if (IsAttacking)
                        {
                            FinishAttack = true;
                            IsAttacking = false;
                            currentFrame = 5;
                        }
                        else if (FinishAttack)
                        {
                            currentFrame = 0;
                            attack = false;
                            FinishAttack = false;
                        }
                    }
                    else if (currentFrame > 5 && currentFrame < 12)
                    {
                        //Right
                        if (attack1)
                        {
                            currentFrame = 10;
                            attack1 = false;
                            IsAttacking = true;
                        }
                        else if (IsAttacking)
                        {
                            FinishAttack = true;
                            IsAttacking = false;
                            currentFrame = 11;
                        }
                        else if (FinishAttack)
                        {
                            currentFrame = 6;
                            attack = false;
                            FinishAttack = false;
                        }
                    }
                    else if (currentFrame > 11 && currentFrame < 18)
                    {
                        //Left
                        if (attack1)
                        {
                            currentFrame = 16;
                            attack1 = false;
                            IsAttacking = true;
                        }
                        else if (IsAttacking)
                        {
                            FinishAttack = true;
                            IsAttacking = false;
                            currentFrame = 17;
                        }
                        else if (FinishAttack)
                        {
                            currentFrame = 12;
                            attack = false;
                            FinishAttack = false;
                        }
                    }
                    else if (currentFrame > 17 && currentFrame < 24)
                    {
                        //Up
                        if (attack1)
                        {
                            currentFrame = 22;
                            attack1 = false;
                            IsAttacking = true;
                        }
                        else if (IsAttacking)
                        {
                            FinishAttack = true;
                            IsAttacking = false;
                            currentFrame = 23;
                        }
                        else if (FinishAttack)
                        {
                            currentFrame = 18;
                            attack = false;
                            FinishAttack = false;
                        }
                    }
                }
            }

            int width = Texture.Width / Columns;
            int height = Texture.Height / Rows;
            int row = (int)((float)currentFrame / (float)Columns);
            int column = currentFrame % Columns;
            Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
            Rectangle destinationRectangle = new Rectangle((int)location.X, (int)location.Y, width, height);

            Bounds = destinationRectangle;
            Bounds.X += Convert.ToInt32(width * .25);
            Bounds.Y += Convert.ToInt32(height * .1);
            Bounds.Width = Convert.ToInt32(width * .60);
            Bounds.Height = Convert.ToInt32(height * .8);

            //new Rectangle(destinationRectangle.X + 15, destinationRectangle.Y + 25, destinationRectangle.Width - 30, destinationRectangle.Height - 50);

            GlobalVariables.CharacterBounds = Bounds;

            spriteBatch.Begin();
            spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White);
            
            if (CharGloves != null)
            {
                spriteBatch.Draw(CharGloves, destinationRectangle, sourceRectangle, GlobalVariables.TheGame.equipment.Gloves.ItemColor);
            }

            if (CharBoots != null)
            {
                spriteBatch.Draw(CharBoots, destinationRectangle, sourceRectangle, GlobalVariables.TheGame.equipment.Boots.ItemColor);
            }

            if (CharShoulders != null)
            {
                spriteBatch.Draw(CharShoulders, destinationRectangle, sourceRectangle, GlobalVariables.TheGame.equipment.Shoulders.ItemColor);
            }

            if (CharHelm != null)
            {
                spriteBatch.Draw(CharHelm, destinationRectangle, sourceRectangle, GlobalVariables.TheGame.equipment.Helmet.ItemColor);
            }

            if (CharChest != null)
            {
                spriteBatch.Draw(CharChest, destinationRectangle, sourceRectangle, GlobalVariables.TheGame.equipment.Chest.ItemColor);
            }

            if (CharPants != null)
            {
                spriteBatch.Draw(CharPants, destinationRectangle, sourceRectangle, GlobalVariables.TheGame.equipment.Pants.ItemColor);
            }

            if (CharWeapon != null)
            {
                if (attack)
                {
                    spriteBatch.Draw(CharWeapon, destinationRectangle, sourceRectangle, Color.White);
                }
            }

            spriteBatch.End();
        }

    }
}

