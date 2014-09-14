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
 public class Enemy

#region Variables
    {

        public Vector2 velocity = new Vector2(150, 0);
        public Vector2 velocityup = new Vector2(0, 150);

        public Texture2D Texture2 { get; set; }
        public Texture2D HpBar { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }
        public Vector2 Location { get; set; }
        private int currentFrame;
        private int totalFrames;
        private int currentUpdate;
        private int updatesPerFrame = 5;
        public Rectangle spriteRectangle;
        public Random RNG = new Random();
        public int move;
        public float maxhp;
        public float graphicwidth;
        public float graphicheight;
        int counter = 1;
        int limit = 125;
        float counterDuration = 3f;
        float currentTime = 0f;
        public bool Valid = false;
        public Vector2 charpos;
        public Vector2 worldloc;
        public string name;
        public float hp;
        public float tothp;
        SpriteFont font;
        SpriteFont font2;
        public bool blnDie= false;
        public bool blnDead = false;
        public float i;
        public int dir;
        public Texture2D HPBAR75;
        public Texture2D HPBARHALF;
        public Texture2D HPBARQUARTER;
        public Texture2D HPBAR40;
        public float DamageCounter;
        public bool blnShowDamage;
        public int m = 2;

#endregion

        public Enemy(Texture2D texture,Texture2D hpbar, Texture2D hpbar75, Texture2D hpbarHalf, Texture2D hpbarquarter,Texture2D hpbar40,SpriteFont Font,SpriteFont Font2, int rows, int columns,float MAXHP, Vector2 location,string Name, int HP)
        {
            Texture2 = texture;
            HpBar = hpbar;
            Rows = rows;
            Columns = columns;
            currentFrame = 0;
            totalFrames = Rows * Columns;
            Location = location;
            worldloc = location;
            name = Name;
            tothp = HP;
            hp = HP;
            font = Font;
            maxhp = MAXHP;
            font2 = Font2;
            HPBAR75 = hpbar75;
            HPBARHALF = hpbarHalf;
            HPBARQUARTER = hpbarquarter;
            HPBAR40 = hpbar40;
        }
        
        public void Update(GameTime gameTime, Vector2 HeroPosition)
        {
            if (blnDie == false)
            {
                charpos = HeroPosition;
                currentTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (currentTime >= counterDuration)
                {
                    counter++;
                    if (move == 1)
                    {
                        i = 0;
                        dir = 1;
                        UpdateLeft(gameTime);
                    }
                    if (move == 2)
                    {
                        i = 0;
                        dir = 2;
                        UpdateRight(gameTime);
                    }
                    if (move == 3)
                    {
                        i = 0;
                        dir = 3;
                        UpdateUp(gameTime);
                    }
                    if (move == 4)
                    {
                        i = 0;
                        dir = 4;
                        UpdateDown(gameTime);
                    }
                    else
                    {
                        dir = 1;
                    }
                }
                if (counter >= limit)
                {
                    counter = 0;
                    currentTime = 0;
                    move = RNG.Next(1, 5);
                }


            }
       }

        public void CharMovedRight(GameTime gameTime, Vector2 velocity)
        {
            Location -= (velocity * (float)gameTime.ElapsedGameTime.TotalSeconds);
            worldloc -= (velocity * (float)gameTime.ElapsedGameTime.TotalSeconds);
        }

        public void CharMovedLeft(GameTime gameTime, Vector2 velocity)
        {
            Location += (velocity * (float)gameTime.ElapsedGameTime.TotalSeconds);
            worldloc += (velocity * (float)gameTime.ElapsedGameTime.TotalSeconds);
        }

        public void CharMovedUp(GameTime gameTime, Vector2 velocityup)
        {
            Location += (velocityup * (float)gameTime.ElapsedGameTime.TotalSeconds);
            worldloc += (velocityup * (float)gameTime.ElapsedGameTime.TotalSeconds);
        }

        public void CharMovedDown(GameTime gameTime, Vector2 velocityup)
        {
            Location -= (velocityup * (float)gameTime.ElapsedGameTime.TotalSeconds);
            worldloc -= (velocityup * (float)gameTime.ElapsedGameTime.TotalSeconds);
        }

        public void UpdateLeft(GameTime gameTime)
        {
            Valid = true;
            if (worldloc.X > 10)
            {
                if (Math.Abs(((Location.X - 10) - charpos.X)) < 35 && (Math.Abs(Location.Y - charpos.Y) < 75))
                {
                    Valid = false;
                }
                if (Valid)
                {
                    currentUpdate++;
                    if (currentUpdate == updatesPerFrame)
                    {
                        currentFrame++;
                        currentUpdate = 0;
                        if (currentFrame < 4 || currentFrame > 8)
                            currentFrame = 4;
                        Location -= (velocity * (float)gameTime.ElapsedGameTime.TotalSeconds);
                        worldloc -= (velocity * (float)gameTime.ElapsedGameTime.TotalSeconds);
                        if (currentFrame == 8)
                            currentFrame = 4;
                        Location -= (velocity * (float)gameTime.ElapsedGameTime.TotalSeconds);
                        worldloc -= (velocity * (float)gameTime.ElapsedGameTime.TotalSeconds);
                    }
                }
            }
        }

        public void UpdateRight(GameTime gameTime)
        {
            Valid = true;
            if (worldloc.X < 1250)
                    {
                        if (Math.Abs(((Location.X + 10) - charpos.X)) < 35 && (Math.Abs(Location.Y - charpos.Y) < 75))
                                {
                                    Valid = false;
                                }
                        if (Valid)
                        {
                            currentUpdate++;
                            if (currentUpdate == updatesPerFrame)
                            {
                                currentFrame++;
                                currentUpdate = 0;
                                if (currentFrame < 9 || currentFrame > 12)
                                    currentFrame = 9;
                                Location += (velocity * (float)gameTime.ElapsedGameTime.TotalSeconds);
                                worldloc += (velocity * (float)gameTime.ElapsedGameTime.TotalSeconds);
                                if (currentFrame == 12)
                                    currentFrame = 9;
                                Location += (velocity * (float)gameTime.ElapsedGameTime.TotalSeconds);
                                worldloc += (velocity * (float)gameTime.ElapsedGameTime.TotalSeconds);
                            }
                        }
                    }
        }

        public void UpdateDown(GameTime gameTime)
        {
            Valid = true;
            if (worldloc.Y < 550)
            {
                if (Math.Abs(((Location.X) - charpos.X)) < 35 && (Math.Abs((Location.Y + 10) - charpos.Y) < 75))
                {
                    Valid = false;
                }
                if (Valid)
                {
                    currentUpdate++;
                    if (currentUpdate == updatesPerFrame)
                    {
                        currentFrame++;
                        currentUpdate = 0;
                        if (currentFrame > 4)
                            currentFrame = 0;
                        Location += (velocityup * (float)gameTime.ElapsedGameTime.TotalSeconds);
                        worldloc += (velocityup * (float)gameTime.ElapsedGameTime.TotalSeconds);
                        if (currentFrame == 4)
                            currentFrame = 0;
                        Location += (velocityup * (float)gameTime.ElapsedGameTime.TotalSeconds);
                        worldloc += (velocityup * (float)gameTime.ElapsedGameTime.TotalSeconds);
                    }
                }
            }
        }

        public void UpdateUp(GameTime gameTime)
        {
            Valid = true;
            if (worldloc.Y > -10)
            {
                if (Math.Abs(((Location.X) - charpos.X)) < 35 && (Math.Abs((Location.Y - 10) - charpos.Y) < 75))
                {
                    Valid = false;
                }
                if (Valid)
                {
                    currentUpdate++;
                    if (currentUpdate == updatesPerFrame)
                    {
                        currentFrame++;
                        currentUpdate = 0;
                        if (currentFrame < 13)
                            currentFrame = 13;
                        Location -= (velocityup * (float)gameTime.ElapsedGameTime.TotalSeconds);
                        worldloc -= (velocityup * (float)gameTime.ElapsedGameTime.TotalSeconds);
                        if (currentFrame == 16)
                            currentFrame = 13;
                        Location -= (velocityup * (float)gameTime.ElapsedGameTime.TotalSeconds);
                        worldloc -= (velocityup * (float)gameTime.ElapsedGameTime.TotalSeconds);
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {

            int width = Texture2.Width / Columns;
            int height = Texture2.Height / Rows;
            int row = (int)((float)currentFrame / (float)Columns);
            int column = currentFrame % Columns;
            int width1 = HpBar.Width;
            int height1 = HpBar.Height;
            graphicheight = height;
            graphicwidth = width;
            Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
            Rectangle destinationRectangle = new Rectangle((int)location.X, (int)location.Y, width, height);
            Rectangle Rec1 = new Rectangle((int)location.X, (int)location.Y, width1, height1);
            
            spriteBatch.Begin();
            
            location.Y -= 1;
            if (blnDie == false)
            {
                if ((hp/tothp) == 1)
                    spriteBatch.Draw(HpBar, Rec1, Color.White);
                if ((hp / tothp) >= .75 && (hp/tothp) < 1)
                    spriteBatch.Draw(HPBAR75, new Rectangle((int)location.X, (int)location.Y, HPBAR75.Width, HPBAR75.Height), Color.White);
                if ((hp / tothp) >= .50 && (hp / tothp) < .75)
                    spriteBatch.Draw(HPBARHALF, new Rectangle((int)location.X, (int)location.Y, HPBARHALF.Width, HPBARHALF.Height), Color.White);
                if ((hp / tothp) >= .25 && (hp / tothp) < .50)
                    spriteBatch.Draw(HPBAR40, new Rectangle((int)location.X, (int)location.Y, HPBAR40.Width, HPBAR40.Height), Color.White);
                if ((hp / tothp) >= 0 && (hp / tothp) < .25)
                    spriteBatch.Draw(HPBARQUARTER, new Rectangle((int)location.X, (int)location.Y, HPBARQUARTER.Width, HPBARQUARTER.Height), Color.White);
                spriteBatch.DrawString(font, hp + "/" + tothp, location, Color.Black);
                if (blnShowDamage)
                {
                    if (m >= 2 && m <= 100)
                    {
                                        spriteBatch.DrawString(font2, DamageCounter.ToString(), new Vector2(location.X, ((location.Y - 40) - m)), Color.Red);
                    m += (m / 2);
                    }
                    else
                    {
                        m = 2;
                        blnShowDamage = false;
                        DamageCounter = 0;
                    }
                }
            }
            
            if ((tothp / maxhp) >= 0 && (tothp / maxhp) < .75)
                spriteBatch.DrawString(font2, name, new Vector2(location.X,(location.Y - 20)), Color.Black);
            if ((tothp / maxhp) >= .75 && (tothp / maxhp) < .9)
                spriteBatch.DrawString(font2, "Hard " + name, new Vector2(location.X - 10, (location.Y - 20)), Color.Green);
            if ((tothp / maxhp) >= .9 && (tothp / maxhp) < .95)
                spriteBatch.DrawString(font2, "Elite " + name, new Vector2(location.X - 10, (location.Y - 20)), Color.Yellow);
            if ((tothp / maxhp) >= .95 && (tothp / maxhp) < 1)
                spriteBatch.DrawString(font2, "Epic " + name, new Vector2(location.X - 10, (location.Y - 20)), Color.Orange);
            if ((tothp / maxhp) == 1)
                spriteBatch.DrawString(font2, "Godly " + name, new Vector2(location.X - 10, (location.Y - 20)), Color.White);

            if (blnDie)
            {
                    if (i >= 0 && i <= 2)
                    {
                        spriteBatch.Draw(Texture2, new Rectangle(((int)location.X + 50), ((int)location.Y + 100), width, height), sourceRectangle, Color.White, i, new Vector2((Vector2.Zero.X + width), (Vector2.Zero.Y + height)), SpriteEffects.None, 0);
                        i += .05f;
                    }
                    if (i > 2)
                    {
                        blnDead = true;
                        blnDie = false;
                    }
                }
            else
            {
                spriteBatch.Draw(Texture2, destinationRectangle, sourceRectangle, Color.White);            
            }
            spriteBatch.End();
        }
    }

}

