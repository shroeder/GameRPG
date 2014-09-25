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
        public bool FinishAttack = false;
        public bool blnDisplaydamage = false;

        public float i;
        public float l = 2.5f;

        public AnimatedSprite(Texture2D texture, int dir, int rows, int columns, Vector2 Location)
        {
            if (GlobalVariables.CharacterLevel <= 0)
            {
                GlobalVariables.CharacterLevel = 1;
            }

            if (GlobalVariables.CharacterMeleeRange == 0)
            {
                GlobalVariables.CharacterMeleeRange = 20;
            }

            Width = Convert.ToInt32(texture.Width / columns);
            Height = Convert.ToInt32(texture.Height / rows);
            position = Location;
            WorldPos = Location;
            direction = dir;
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
            if (currentUpdate == updatesPerFrame)
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

        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            if (attack)
            {
                if (currentFrame > -1 && currentFrame < 4)
                {
                    //Down
                    if (attack)
                    {
                        currentFrame = 4;
                        attack = false;
                        IsAttacking = true;
                    }
                    else if(IsAttacking)
                    {
                        currentFrame = 5;
                        IsAttacking = false;
                        FinishAttack = true;
                    }
                    else if (FinishAttack)
                    {
                        currentFrame = 0;
                        FinishAttack = false;
                    }
                }
                else if (currentFrame > 5 && currentFrame < 10)
                {
                    //Right
                    if (attack)
                    {
                        currentFrame = 10;
                        attack = false;
                        IsAttacking = true;
                    }
                    else if (IsAttacking)
                    {
                        currentFrame = 11;
                        IsAttacking = false;
                        FinishAttack = true;
                    }
                    else if (FinishAttack)
                    {
                        currentFrame = 6;
                        FinishAttack = false;
                    }
                }
                else if (currentFrame > 12 && currentFrame < 16)
                {
                    //Left
                    if (attack)
                    {
                        currentFrame = 16;
                        attack = false;
                        IsAttacking = true;
                    }
                    else if (IsAttacking)
                    {
                        currentFrame = 17;
                        IsAttacking = false;
                        FinishAttack = true;
                    }
                    else if (FinishAttack)
                    {
                        currentFrame = 12;
                        FinishAttack = false;
                    }
                }
                else if (currentFrame > 18 && currentFrame < 22)
                {
                    //Up
                    if (attack)
                    {
                        currentFrame = 22;
                        attack = false;
                        IsAttacking = true;
                    }
                    else if (IsAttacking)
                    {
                        currentFrame = 23;
                        IsAttacking = false;
                        FinishAttack = true;
                    }
                    else if (FinishAttack)
                    {
                        currentFrame = 18;
                        FinishAttack = false;
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

            new Rectangle(destinationRectangle.X + 15, destinationRectangle.Y + 25, destinationRectangle.Width - 30, destinationRectangle.Height - 50);

            GlobalVariables.CharacterBounds = Bounds;

            spriteBatch.Begin();
            spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White);
            spriteBatch.End();
        }

    }
}

