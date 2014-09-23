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
        public Vector2 oldWorldPos;

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
        public bool blnDisplaydamage = false;

        public float i;
        public float l = 2.5f;

        public AnimatedSprite(Texture2D texture, int dir, int rows, int columns, Vector2 Location)
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

        public void UpdateDownRight(){}

        public void UpdateDownLeft(){}

        public void UpdateUpRight(){}

        public void UpdateUpLeft(){}

        public void AnimateAttack(Vector2 loc)
        {
            attack = true;
            Vector2 Dif = position - loc;
            if (Dif.X >= 0)
            {
                if (Dif.Y >= 0)
                {
                    if (Dif.X >= Dif.Y)
                    {
                        Direction = DirToAttack.Up;
                    }
                    else
                    {
                        Direction = DirToAttack.Left;
                    }
                }
                else if (Dif.Y < 0)
                {
                    if (Math.Abs(Dif.Y) <= Dif.X)
                    {
                        Direction = DirToAttack.Down;
                    }
                    else
                    {
                        Direction = DirToAttack.Left;
                    }
                }
                else
                {
                    Direction = DirToAttack.Left;
                }
            }
            else
            {
                if (Dif.Y >= 0)
                {
                    if (Math.Abs(Dif.X) >= Dif.Y)
                    {
                        Direction = DirToAttack.Up;
                    }
                    else
                    {
                        Direction = DirToAttack.Right;
                    }
                }
                else if (Dif.Y < 0)
                {
                    if (Math.Abs(Dif.Y) <= Math.Abs(Dif.X))
                    {
                        Direction = DirToAttack.Down;
                    }
                    else
                    {
                        Direction = DirToAttack.Right;
                    }
                }
                else
                {
                    Direction = DirToAttack.Right;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            if (attack)
            {
                if (Direction == DirToAttack.Down)
                {
                    if (!IsAttacking){
                        currentFrame = 4;
                        IsAttacking = true;
                        attack = true;
                    }
                    else
                    {
                        currentFrame = 5;
                        IsAttacking = false;
                        attack = false;
                    }
                }
                else if (Direction == DirToAttack.Up)
                {
                    if (!IsAttacking)
                    {
                        currentFrame = 10;
                        IsAttacking = true;
                        attack = true;
                    }
                    else
                    {
                        currentFrame = 11;
                        IsAttacking = true;
                        attack = false;
                    }
                }
                else if (Direction == DirToAttack.Left)
                {
                    if (!IsAttacking)
                    {
                        currentFrame = 16;
                        IsAttacking = true;
                        attack = true;
                    }
                    else
                    {
                        currentFrame = 17;
                        IsAttacking = false;
                        attack = false;
                    }
                }
                else if (Direction == DirToAttack.Right)
                {
                    if (!IsAttacking)
                    {
                        currentFrame = 22;
                        IsAttacking = true;
                        attack = true;
                    }
                    else
                    {
                        currentFrame = 23;
                        IsAttacking = false;
                        attack = false;
                    }
                }
            }

            int width = Texture.Width / Columns;
            int height = Texture.Height / Rows;
            int row = (int)((float)currentFrame / (float)Columns);
            int column = currentFrame % Columns;
            Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
            Rectangle destinationRectangle = new Rectangle((int)location.X, (int)location.Y, width, height);
            Bounds = new Rectangle(destinationRectangle.X + 15, destinationRectangle.Y + 25, destinationRectangle.Width - 30, destinationRectangle.Height - 50);

            spriteBatch.Begin();
            spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White);
            spriteBatch.End();
        }

    }
}

   