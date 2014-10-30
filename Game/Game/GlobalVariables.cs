﻿using System;
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
using Microsoft.Xna.Framework.Storage;
using System.IO;
using System.Xml.Serialization;

namespace TextureAtlas
{
    public static class GlobalVariables
    {
        public enum CollisionDirection
        {
            UpRight,
            UpLeft,
            DownRight,
            DownLeft
        }

        public static CollisionDirection CollideDirection { get; set; }

        public enum Pos
        {
            IsRight,
            IsLeft,
            IsUp,
            IsDown,
            IsUpright,
            IsUpLeft,
            IsDownRight,
            IsDownLeft,
            IsHere,
            Nothing
        }

        public static Pos CharPos = Pos.Nothing;

        //Enum set if and when I were to hit an enemy by moving in a direction
        public enum EnemyPos
        {
            IsRight,
            IsLeft,
            IsUp,
            IsDown
        }

        public enum Dir
        {
            Up,
            Left,
            Down,
            Right,
            UpRight,
            DownRight,
            UpLeft,
            DownLeft,
            Nothing
        }

        public static Dir CurrentDir { get; set; }

        public static bool PathFindingFail { get; set; }

        public static Vector2 MoveToLoc { get; set; }

        public static Rectangle CharacterBounds { get; set; }

        public static List<int> AffixRange = new List<int>();

        public static List<DrawItem> ItemsToBeDrawn = new List<DrawItem>();

        public static HeroDisplay TheHero { get; set; }
        public static Equipment Equipment { get; set; }
        public static Inventory Inventory { get; set; }

        public static Texture2D RotateClock { get; set; }
        public static Texture2D RotateCounter { get; set; }
        public static Texture2D TestSquare { get; set; }
        public static Texture2D CheckMark { get; set; }
        public static Texture2D LegendaryBG { get; set; }
        public static Texture2D LegendaryBeam { get; set; }
        public static Texture2D TextureBack { get; set; }

        public static SpriteFont Font10 { get; set; }
        public static SpriteFont Font12 { get; set; }
        public static SpriteFont font14 { get; set; }
        public static SpriteFont Font16 { get; set; }
        public static SpriteFont Font20 { get; set; }
        public static SpriteFont Font24 { get; set; }
        public static SpriteFont Font28 { get; set; }
        public static SpriteFont Font32 { get; set; }

        public static int CharacterMinDmg { get; set; }
        public static int CharacterMaxDmg { get; set; }
        public static int Rows { get; set; }
        public static int Columns { get; set; }
        public static int TileHeight { get; set; }
        public static int TileWidth { get; set; }

        public static float OverRoll { get; set; }

        //Savable Values
        public static int CharacterMeleeRange { get; set; }
        public static int CharacterWeaponType { get; set; }
        public static string CharacterWeaponName { get; set; }
        public static int GameDifficulty { get; set; }
        public static int CharacterLevel { get; set; }
        public static int CharacterStrength { get; set; }
        public static int CharacterDex { get; set; }
        public static int CharacterInt { get; set; }
        public static int CharacterWisdom { get; set; }
        public static int CharacterAccuracy { get; set; }
        public static int CharacterLuck { get; set; }
        public static int CharacterAllElementalReduction { get; set; }
        public static int CharacterFireResist { get; set; }
        public static int CharacterColdResist { get; set; }
        public static int CharacterLightningResist { get; set; }
        public static int CharacterEarthResist { get; set; }
        public static int CharacterArmour { get; set; }
        public static int CharacterMovementSpeed { get; set; }
        public static int CharacterAttackSpeed { get; set; }
        public static int CharacterHealth { get; set; }
        public static int CharacterMana { get; set; }
        public static int CharacterExperience { get; set; }
        public static int CharacterStatPoints { get; set; }
        public static int CharacterSkillPoints { get; set; }
        public static int CharacterIncreaseFlatPhysical { get; set; }
        public static int CharacterIncreaseFlatRange { get; set; }
        public static int CharacterIncreaseFlatMelee { get; set; }
        public static float CharacterIncreasePhysDmg { get; set; }
        public static float CharacterIncreasePhysRangeDmg { get; set; }
        public static float CharacterIncreasePhyMeleeDmg { get; set; }
        public static float CharacterIncreaseExpPct { get; set; }
        public static float CharacterMagicFindRarity { get; set; }
        public static float CharacterMagicFindQuantity { get; set; }
        public static float CharacterCritChance { get; set; }
        public static float CharacterCritDamageModifier { get; set; }
        public static float CharacterCoolDownReduction { get; set; }
        public static float CharacterPhysDamageReduction { get; set; }
        public static float CharacterVsBeastDamage { get; set; }
        public static float CharacterVsHumanDamage { get; set; }
        public static float CharacterVsUndeadDamage { get; set; }
        public static float CharacterVsBossDamage { get; set; }
        public static float CharacterVsEliteDamage { get; set; }
        public static float CharacterMagicPenetration { get; set; }
        public static float CharacterArmourPenetration { get; set; }
        public static float CharacterPhysicalReflect { get; set; }
        public static float CharacterMagicReflect { get; set; }
        public static float CharacterSpellDamage { get; set; }
        public static float CharacterHealthRegen { get; set; }
        public static float CharacterManaRegen { get; set; }
        public static float CharacterDamageReduction { get; set; }
        public static bool CharacterHasSkillFireBall { get; set; }
        public static int UserSetWidth { get; set; }
        public static int UserSetHeight { get; set; }
        public static int ScreenWidth { get; set; }
        public static int ScreenHeight { get; set; }
        public static int OldWidth { get; set; }
        public static int OldHeight { get; set; }
        public static int NewWidth { get; set; }
        public static int NewHeight { get; set; }
        public static bool OldFullScreen { get; set; }
        public static bool FullScreen { get; set; }
        public static bool UserSetFullScreen { get; set; }
        public static bool ShowEnemyNames { get; set; }
        public static bool ShowEnemyBars { get; set; }
        public static bool ShowItemNames { get; set; }
        public static bool ShowEnemyDamage { get; set; }

        public static Game1 TheGame { get; set; }
        public static GraphicsDeviceManager gfx { get; set; }

        public static SpriteFont LargeFont
        {
            get
            {
                return AutoFont(gfx, 1);
            }
        }

        public static SpriteFont MediumFont
        {
            get
            {
                return AutoFont(gfx, 2);
            }
        }

        public static SpriteFont SmallFont
        {
            get
            {
                return AutoFont(gfx, 3);
            }
        }
        [Serializable]
        public struct SaveSettings
        {
            public int ResolutionWidth;
            public int ResolutionHeight;
            public bool IsFullScreen;
            public bool blnShowEnemyNames;
            public bool blnShowEnemyBars;
            public bool blnShowItemNames;
            public bool blnShowEnemyDamage;
        }
        [Serializable]
        public struct GameData
        {
            public Inventory theInventory { get; set; }
            public Equipment theEquipment { get; set; }
            public string WeaponName { get; set; }
            public int MeleeRange { get; set; }
            public int WeaponType { get; set; }
            public int Difficulty { get; set; }
            public int Level { get; set; }
            public int Strength { get; set; }
            public int Dex { get; set; }
            public int Int { get; set; }
            public int Wisdom { get; set; }
            public int Accuracy { get; set; }
            public int Luck { get; set; }
            public int AllElementalReduction { get; set; }
            public int FireResist { get; set; }
            public int ColdResist { get; set; }
            public int LightningResist { get; set; }
            public int EarthResist { get; set; }
            public int Armour { get; set; }
            public int MovementSpeed { get; set; }
            public int AttackSpeed { get; set; }
            public int Health { get; set; }
            public int Mana { get; set; }
            public int Experience { get; set; }
            public int StatPoints { get; set; }
            public int SkillPoints { get; set; }
            public int IncreaseFlatPhys { get; set; }
            public int IncreaseFlatRanged { get; set; }
            public int IncreaseFlatMelee { get; set; }
            public float IncreasePhysMelee { get; set; }
            public float IncreasePhyRange { get; set; }
            public float IncreasePhys { get; set; }
            public float IncreaseExpPct { get; set; }
            public float MagicFindRarity { get; set; }
            public float MagicFindQuantity { get; set; }
            public float CritChance { get; set; }
            public float CritDamageModifier { get; set; }
            public float CoolDownReduction { get; set; }
            public float PhysDamageReduction { get; set; }
            public float VsBeastDamage { get; set; }
            public float VsHumanDamage { get; set; }
            public float VsUndeadDamage { get; set; }
            public float VsBossDamage { get; set; }
            public float VsEliteDamage { get; set; }
            public float MagicPenetration { get; set; }
            public float ArmourPenetration { get; set; }
            public float PhysicalReflect { get; set; }
            public float MagicReflect { get; set; }
            public float SpellDamage { get; set; }
            public float HealthRegen { get; set; }
            public float ManaRegen { get; set; }
            public float DamageReduction { get; set; }
            public bool HasSkillFireBall { get; set; }
        }

        public static void SaveGameData()
        {
            GameData data = new GameData();
            data.theInventory = Inventory;
            data.theEquipment = Equipment;
            data.MeleeRange = CharacterMeleeRange;
            data.WeaponType = CharacterWeaponType;
            data.Difficulty = GameDifficulty;
            data.Level = CharacterLevel;
            data.Strength = CharacterStrength;
            data.Dex = CharacterDex;
            data.Int = CharacterInt;
            data.Wisdom = CharacterWisdom;
            data.Accuracy = CharacterAccuracy;
            data.Luck = CharacterLuck;
            data.AllElementalReduction = CharacterAllElementalReduction;
            data.FireResist = CharacterFireResist;
            data.ColdResist = CharacterColdResist;
            data.LightningResist = CharacterLightningResist;
            data.EarthResist = CharacterEarthResist;
            data.Armour = CharacterArmour;
            data.MovementSpeed = CharacterMovementSpeed;
            data.AttackSpeed = CharacterAttackSpeed;
            data.Health = CharacterHealth;
            data.Mana = CharacterMana;
            data.Experience = CharacterExperience;
            data.StatPoints = CharacterStatPoints;
            data.SkillPoints = CharacterSkillPoints;
            data.IncreaseExpPct = CharacterIncreaseExpPct;
            data.MagicFindRarity = CharacterMagicFindRarity;
            data.MagicFindQuantity = CharacterMagicFindQuantity;
            data.CritChance = CharacterCritChance;
            data.IncreasePhyRange = CharacterIncreasePhysRangeDmg;
            data.IncreasePhysMelee = CharacterIncreasePhysDmg;
            data.IncreasePhys = CharacterIncreasePhysDmg;
            data.IncreaseFlatPhys = CharacterIncreaseFlatPhysical;
            data.IncreaseFlatMelee = CharacterIncreaseFlatMelee;
            data.IncreaseFlatRanged = CharacterIncreaseFlatRange;
            data.CritDamageModifier = CharacterCritDamageModifier;
            data.CoolDownReduction = CharacterCoolDownReduction;
            data.PhysDamageReduction = CharacterPhysDamageReduction;
            data.VsBeastDamage = CharacterVsBeastDamage;
            data.VsHumanDamage = CharacterVsHumanDamage;
            data.VsUndeadDamage = CharacterVsUndeadDamage;
            data.VsBossDamage = CharacterVsBossDamage;
            data.VsEliteDamage = CharacterVsEliteDamage;
            data.MagicPenetration = CharacterMagicPenetration;
            data.ArmourPenetration = CharacterArmourPenetration;
            data.PhysicalReflect = CharacterPhysicalReflect;
            data.MagicReflect = CharacterMagicReflect;
            data.SpellDamage = CharacterSpellDamage;
            data.HealthRegen = CharacterHealthRegen;
            data.DamageReduction = CharacterDamageReduction;
            data.HasSkillFireBall = CharacterHasSkillFireBall;

            IAsyncResult result1 = StorageDevice.BeginShowSelector(PlayerIndex.One, null, null);
            StorageDevice device = StorageDevice.EndShowSelector(result1);

            IAsyncResult result = device.BeginOpenContainer("GameData", null, null);
            result.AsyncWaitHandle.WaitOne();
            StorageContainer container = device.EndOpenContainer(result);
            result.AsyncWaitHandle.Close();
            string filename = "game.sav";

            container.DeleteFile(filename);

            Stream stream = container.CreateFile(filename);
            XmlSerializer serializer = new XmlSerializer(typeof(GameData));
            serializer.Serialize(stream, data);
            stream.Close();
            container.Dispose();
        }

        public static void LoadGameData()
        {
            ShowEnemyBars = true;
            ShowEnemyDamage = true;
            ShowEnemyNames = true;
            ShowItemNames = true;

            IAsyncResult result1 = StorageDevice.BeginShowSelector(PlayerIndex.One, null, null);
            StorageDevice device = StorageDevice.EndShowSelector(result1);

            IAsyncResult result = device.BeginOpenContainer("GameData", null, null);
            result.AsyncWaitHandle.WaitOne();
            StorageContainer container = device.EndOpenContainer(result);
            result.AsyncWaitHandle.Close();
            string filename = "game.sav";
            if (!container.FileExists(filename))
            {
                container.Dispose();
                return;
            }
            Stream stream = container.OpenFile(filename, FileMode.Open);
            XmlSerializer serializer = new XmlSerializer(typeof(GameData));

            try
            {

                GameData data = (GameData)serializer.Deserialize(stream);

                stream.Close();
                container.Dispose();
                Equipment = data.theEquipment;
                Inventory = data.theInventory;
                CharacterWeaponName = data.WeaponName;
                CharacterMeleeRange = data.MeleeRange;
                CharacterWeaponType = data.WeaponType;
                GameDifficulty = data.Difficulty;
                CharacterLevel = data.Level;
                CharacterStrength = data.Strength;
                CharacterDex = data.Dex;
                CharacterInt = data.Int;
                CharacterWisdom = data.Wisdom;
                CharacterAccuracy = data.Accuracy;
                CharacterLuck = data.Luck;
                CharacterAllElementalReduction = data.AllElementalReduction;
                CharacterColdResist = data.ColdResist;
                CharacterLightningResist = data.LightningResist;
                CharacterEarthResist = data.EarthResist;
                CharacterArmour = data.Armour;
                CharacterMovementSpeed = data.MovementSpeed;
                CharacterAttackSpeed = data.AttackSpeed;
                CharacterHealth = data.Health;
                CharacterMana = data.Mana;
                CharacterExperience = data.Experience;
                CharacterIncreasePhysDmg = data.IncreasePhysMelee;
                CharacterIncreasePhysRangeDmg = data.IncreasePhyRange;
                CharacterIncreasePhysDmg = data.IncreasePhys;
                CharacterIncreaseFlatMelee = data.IncreaseFlatMelee;
                CharacterIncreaseFlatPhysical = data.IncreaseFlatPhys;
                CharacterIncreaseFlatRange = data.IncreaseFlatRanged;
                CharacterStatPoints = data.StatPoints;
                CharacterSkillPoints = data.SkillPoints;
                CharacterIncreaseExpPct = data.IncreaseExpPct;
                CharacterMagicFindRarity = data.MagicFindRarity;
                CharacterMagicFindQuantity = data.MagicFindQuantity;
                CharacterCritChance = data.CritChance;
                CharacterCritDamageModifier = data.CritDamageModifier;
                CharacterCoolDownReduction = data.CoolDownReduction;
                CharacterPhysDamageReduction = data.PhysDamageReduction;
                CharacterVsBeastDamage = data.VsBeastDamage;
                CharacterVsHumanDamage = data.VsHumanDamage;
                CharacterVsUndeadDamage = data.VsUndeadDamage;
                CharacterVsBossDamage = data.VsBossDamage;
                CharacterVsEliteDamage = data.VsEliteDamage;
                CharacterMagicPenetration = data.MagicPenetration;
                CharacterArmourPenetration = data.ArmourPenetration;
                CharacterPhysicalReflect = data.PhysicalReflect;
                CharacterMagicReflect = data.MagicReflect;
                CharacterSpellDamage = data.SpellDamage;
                CharacterHealthRegen = data.HealthRegen;
                CharacterDamageReduction = data.DamageReduction;
                CharacterHasSkillFireBall = data.HasSkillFireBall;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void SaveUserSettings()
        {
            SaveSettings data = new SaveSettings();

            data.ResolutionWidth = GlobalVariables.UserSetWidth;
            data.ResolutionHeight = GlobalVariables.UserSetHeight;
            data.IsFullScreen = gfx.IsFullScreen;
            data.blnShowEnemyBars = ShowEnemyBars;
            data.blnShowEnemyNames = ShowEnemyNames;
            data.blnShowItemNames = ShowItemNames;
            data.blnShowEnemyDamage = ShowEnemyDamage;

            IAsyncResult result1 = StorageDevice.BeginShowSelector(PlayerIndex.One, null, null);
            StorageDevice device = StorageDevice.EndShowSelector(result1);

            IAsyncResult result = device.BeginOpenContainer("UserSettings", null, null);
            result.AsyncWaitHandle.WaitOne();
            StorageContainer container = device.EndOpenContainer(result);
            result.AsyncWaitHandle.Close();
            string filename = "settings.sav";

            if (container.FileExists(filename))
            {
                container.DeleteFile(filename);
            }

            Stream stream = container.CreateFile(filename);
            XmlSerializer serializer = new XmlSerializer(typeof(SaveSettings));
            serializer.Serialize(stream, data);
            stream.Close();
            container.Dispose();

        }

        public static void LoadUserSettings()
        {
            IAsyncResult result1 = StorageDevice.BeginShowSelector(PlayerIndex.One, null, null);
            StorageDevice device = StorageDevice.EndShowSelector(result1);

            IAsyncResult result = device.BeginOpenContainer("UserSettings", null, null);
            result.AsyncWaitHandle.WaitOne();
            StorageContainer container = device.EndOpenContainer(result);
            result.AsyncWaitHandle.Close();
            string filename = "settings.sav";
            if (!container.FileExists(filename))
            {
                container.Dispose();
                return;
            }
            Stream stream = container.OpenFile(filename, FileMode.Open);
            XmlSerializer serializer = new XmlSerializer(typeof(SaveSettings));
            SaveSettings data = (SaveSettings)serializer.Deserialize(stream);
            stream.Close();
            container.Dispose();

            UserSetWidth = data.ResolutionWidth;
            UserSetHeight = data.ResolutionHeight;
            UserSetFullScreen = data.IsFullScreen;
            ShowEnemyBars = data.blnShowEnemyBars;
            ShowEnemyNames = data.blnShowEnemyNames;
            ShowItemNames = data.blnShowItemNames;
            ShowEnemyDamage = data.blnShowEnemyDamage;

        }

        public static SpriteFont AutoFont(GraphicsDeviceManager gfx, int scale)
        {

            switch (scale)
            {

                case 1:

                    if (gfx.PreferredBackBufferWidth > 1600)
                    {
                        return Font32;
                    }
                    else if (gfx.PreferredBackBufferWidth > 1200 && gfx.PreferredBackBufferWidth < 1601)
                    {
                        return Font28;
                    }
                    else if (gfx.PreferredBackBufferWidth > 1000 && gfx.PreferredBackBufferWidth < 1201)
                    {
                        return Font24;
                    }
                    else if (gfx.PreferredBackBufferWidth > 800 && gfx.PreferredBackBufferWidth < 1001)
                    {
                        return Font20;
                    }
                    else if (gfx.PreferredBackBufferWidth > 600 && gfx.PreferredBackBufferWidth < 801)
                    {
                        return Font16;
                    }
                    else if (gfx.PreferredBackBufferWidth > 0 && gfx.PreferredBackBufferWidth < 601)
                    {
                        return Font10;
                    }
                    else
                    {
                        return Font10;
                    }

                case 2:

                    if (gfx.PreferredBackBufferWidth > 1600)
                    {
                        return Font24;
                    }
                    else if (gfx.PreferredBackBufferWidth > 1200 && gfx.PreferredBackBufferWidth < 1601)
                    {
                        return Font20;
                    }
                    else if (gfx.PreferredBackBufferWidth > 1000 && gfx.PreferredBackBufferWidth < 1201)
                    {
                        return Font16;
                    }
                    else
                    {
                        return Font10;
                    }

                case 3:

                    if (gfx.PreferredBackBufferWidth > 1600)
                    {
                        return Font20;
                    }
                    else if (gfx.PreferredBackBufferWidth > 1200 && gfx.PreferredBackBufferWidth < 1601)
                    {
                        return Font16;
                    }
                    else
                    {
                        return Font10;
                    }


                default:

                    return Font10;
            }
        }

        public static Vector2 GetInterSect(Rectangle me, Rectangle them, string XY = "")
        {

            bool MeRight;
            bool MeAbove;

            Vector2 MeTopLeft = new Vector2(me.X, me.Y);
            Vector2 MeTopRight = new Vector2((me.X + me.Width), me.Y);
            Vector2 MeBottomLeft = new Vector2(me.X, (me.Y + me.Height));
            Vector2 MeBottomRight = new Vector2((me.X + me.Width), (me.Y + me.Height));

            Vector2 ThemTopLeft = new Vector2(them.X, them.Y);
            Vector2 ThemTopRight = new Vector2((them.X + them.Width), them.Y);
            Vector2 ThemBottomLeft = new Vector2(them.X, (them.Y + them.Height));
            Vector2 ThemBottomRight = new Vector2((them.X + them.Width), (them.Y + them.Height));

            Vector2 ApplyValue = new Vector2(0, 0);

            if (me.Y > them.Y)
            {
                MeAbove = false;
            }
            else
            {
                MeAbove = true;
            }
            if (me.X > them.X)
            {
                MeRight = true;
            }
            else
            {
                MeRight = false;
            }

            if (MeRight && MeAbove)
            {
                ApplyValue = ThemTopRight - MeBottomLeft;
            }
            else if (MeRight && !MeAbove)
            {
                ApplyValue = ThemBottomRight - MeTopLeft;
            }
            else if (!MeRight && MeAbove)
            {
                ApplyValue = ThemTopLeft - MeBottomRight;
            }
            else if (!MeRight && !MeAbove)
            {
                ApplyValue = ThemBottomLeft - MeTopRight;
            }

            if (XY == "x")
            {
                ApplyValue.Y = 0;
            }
            else if (XY == "y")
            {
                ApplyValue.X = 0;
            }

            return ApplyValue;
        }

        public static int RollVsRarity()
        {
            int returnValue = 0;
            Random rng = new Random();
            int applyMF = rng.Next(1 + (int)(CharacterMagicFindQuantity / 5), 20 + (int)(CharacterMagicFindQuantity / 2));
            if (applyMF > 10)
            {
                int rarity = rng.Next(1 + (int)(applyMF - 10) + (int)(CharacterMagicFindQuantity / 5) + (int)(CharacterMagicFindRarity / 5), 100);
                if (rarity >= 30 && rarity < 60)
                {
                    returnValue = 2;
                }
                else if (rarity >= 60 && rarity < 80)
                {
                    returnValue = 3;
                }
                else if (rarity >= 80 && rarity < 90)
                {
                    returnValue = 4;
                }
                else if (rarity >= 90)
                {
                    int isUnique = rng.Next(0 + CharacterLuck + (int)CharacterMagicFindRarity + (int)CharacterMagicFindQuantity, 1000);
                    if (isUnique > 600)
                    {
                        returnValue = 6;
                    }
                    else
                    {
                        returnValue = 5;
                    }
                }
                else
                {
                    returnValue = 1;
                }
            }
            else
            {
                returnValue = 1;
            }

            return returnValue;

        }

        public static bool RollVsHit(int EnemyLevel, int EnemyEvasion)
        {
            Random rng = new Random();
            float DifficultyPrimitive = (CharacterLevel / 10);
            float requiredRollToHit = (float)((((EnemyEvasion * (GameDifficulty * 1.3)) + (EnemyLevel * GameDifficulty)) * DifficultyPrimitive) * (GameDifficulty * 1.3));
            float HitMark = (float)(((CharacterDex * .4) * (CharacterAccuracy / (CharacterLevel * .15))) / (DifficultyPrimitive * .3));
            int RandomNumber = rng.Next(5 - Convert.ToInt32(CharacterAccuracy * .07), 15 - Convert.ToInt32(CharacterDex * .12));
            if ((HitMark / RandomNumber) > requiredRollToHit)
            {
                OverRoll = (HitMark / RandomNumber) - requiredRollToHit;
                return true;
            }
            else
            {
                return false;
            }

        }

        public static bool RollVsCrit()
        {
            Random rng = new Random();
            float myRoll = (float)(OverRoll * (CharacterLuck * 2.5) / (rng.Next((50 - (int)(CharacterCritChance * .5)), 100)));
            if (myRoll > 100)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static Vector2 newLocation(Vector2 loc, int width, int height)
        {
            Vector2 location = loc;
            int tempx = 0;
            int tempy = 0;
            int difx = 0;
            int dify = 0;
            if (loc.X + width + 150 > gfx.PreferredBackBufferWidth)
            {
                difx = ((int)loc.X + width) - gfx.PreferredBackBufferWidth;
                //arg
                difx += 150;
                tempx = (int)loc.X - difx;
                location.X = tempx;
            }
            if (loc.Y + height > gfx.PreferredBackBufferHeight)
            {
                dify = ((int)loc.Y + height) - gfx.PreferredBackBufferHeight;
                tempy = (int)loc.Y - dify;
                location.Y = tempy;
            }
            return location;
        }

        public static string GetUniqueByTypes(int ItemType, int SubType)
        {
            string ItemName = "";

            switch (ItemType)
            {
                case 1:

                    switch (SubType)
                    {
                        case 1:
                            ItemName = "Paladin's GreatSword";
                            break;
                    }

                    break;

                case 2:

                    switch (SubType)
                    {
                        case 1:
                            ItemName = "Stink Steppers";
                            break;
                    }

                    break;

                case 3:

                    switch (SubType)
                    {
                        case 1:
                            ItemName = "Jagged Joggers";
                            break;
                    }

                    break;

                case 4:

                    switch (SubType)
                    {
                        case 1:
                            ItemName = "Rustic Regulators";
                            break;
                    }

                    break;

                case 5:

                    switch (SubType)
                    {
                        case 1:
                            ItemName = "Pain and Suffering";
                            break;
                    }

                    break;

                case 6:

                    switch (SubType)
                    {
                        case 1:
                            ItemName = "Sage's Gold Ring";
                            break;
                    }

                    break;

                case 7:

                    switch (SubType)
                    {
                        case 1:
                            ItemName = "The Slimming Belt";
                            break;
                    }

                    break;

                case 8:

                    switch (SubType)
                    {
                        case 1:
                            ItemName = "Peripheral Vision";
                            break;
                    }

                    break;

            }

            return ItemName;

        }

        public static void WaitToDraw(int scenario, Vector2 location, Rectangle locRect, Color color, SpriteFont font = null, Texture2D txt = null, string text = "")
        {
            DrawItem ItemToDraw = new DrawItem();

            switch (scenario)
            {
                case 0:
                    //Draw Item BG
                    ItemToDraw.theLocation = location;
                    ItemToDraw.theRectangle = locRect;
                    ItemToDraw.theColor = color;
                    ItemToDraw.theTexture = txt;
                    ItemToDraw.CaseOfDraw = 0;
                    ItemsToBeDrawn.Add(ItemToDraw);
                    break;
                case 1:
                    //Draw Item Affixes
                    ItemToDraw.theLocation = location;
                    ItemToDraw.theText = text;
                    ItemToDraw.theColor = color;
                    ItemToDraw.CaseOfDraw = 1;
                    ItemToDraw.theFont = font;
                    ItemsToBeDrawn.Add(ItemToDraw);
                    break;
            }
        }

        public static string GetItemByType(int ItemType, int SubType)
        {
            string ItemName = "";

            switch (ItemType)
            {
                case 1:

                    switch (SubType)
                    {
                        case 1:
                            ItemName = "Two-Handed Sword";
                            break;
                    }

                    break;

                case 2:

                    switch (SubType)
                    {
                        case 1:
                            ItemName = "Leather Boots";
                            break;
                    }

                    break;

                case 3:

                    switch (SubType)
                    {
                        case 1:
                            ItemName = "Leather Pants";
                            break;
                    }

                    break;
                case 4:

                    switch (SubType)
                    {
                        case 1:
                            ItemName = "Leather Chest";
                            break;
                    }

                    break;
                case 5:

                    switch (SubType)
                    {
                        case 1:
                            ItemName = "Leather Gloves";
                            break;
                    }

                    break;
                case 6:

                    switch (SubType)
                    {
                        case 1:
                            ItemName = "Gold Ring";
                            break;
                    }

                    break;
                case 7:

                    switch (SubType)
                    {
                        case 1:
                            ItemName = "Leather Belt";
                            break;
                    }

                    break;
                case 8:

                    switch (SubType)
                    {
                        case 1:
                            ItemName = "Leather Helmet";
                            break;
                    }

                    break;
            }

            return ItemName;

        }

        public static List<Affix> RollVsAffix(int Affixes, int ItemType, int ItmLvl, int SubType)
        {

            List<Affix> returnAffixs = new List<Affix>();

            for (int intlc = 0; intlc < Affixes; intlc++)
            {
                if (AffixRange.Count == 0)
                {

                    switch (ItemType)
                    {
                        //Two-Handed Sword
                        case 1:

                            switch (SubType)
                            {
                                //Starter Two-Hander
                                case 1:

                                    AffixRange = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 36, 37, 38, 39 };
                                    break;
                            }

                            break;
                        //Boots
                        case 2:

                            switch (SubType)
                            {
                                //Leather Boots
                                case 1:

                                    AffixRange = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 40, 41 };
                                    break;
                            }

                            break;
                        //Pants
                        case 3:

                            switch (SubType)
                            {
                                //Leather PAnts
                                case 1:

                                    AffixRange = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 36, 40, 41 };
                                    break;
                            }

                            break;
                        //Chest
                        case 4:

                            switch (SubType)
                            {
                                //Leather Chest
                                case 1:

                                    AffixRange = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 36, 40, 41 };
                                    break;
                            }

                            break;
                        //Gloves
                        case 5:

                            switch (SubType)
                            {
                                //Leather Gloves
                                case 1:

                                    AffixRange = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 36, 40, 41 };
                                    break;
                            }

                            break;
                        //Ring
                        case 6:

                            switch (SubType)
                            {
                                //Gold Ring
                                case 1:

                                    AffixRange = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 36, 40, 41 };
                                    break;
                            }
                            break;
                        //Belt
                        case 7:

                            switch (SubType)
                            {
                                //Leather Belt
                                case 1:

                                    AffixRange = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 36, 40, 41 };
                                    break;
                            }
                            break;
                        //Helmet
                        case 8:

                            switch (SubType)
                            {
                                //Leather Helmet
                                case 1:

                                    AffixRange = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 36, 40, 41 };
                                    break;
                            }
                            break;
                    }
                }

                Affix afx = RollAffix(AffixRange, ItmLvl);
                returnAffixs.Add(afx);
            }

            AffixRange = new List<int>();

            return returnAffixs;

        }

        public static int GetBaseStat(int type, int subtype)
        {
            int returnValue = 0;
            switch (type)
            {
                //Weapon
                case 1:
                    switch (subtype)
                    {
                        case 1:
                            //Two-Handed Sword
                            returnValue = 40;
                            break;
                    }
                    break;
                case 2:
                    //Boots
                    switch (subtype)
                    {
                        case 1:
                            //leather boots
                            returnValue = 20;
                            break;
                    }
                    break;
                case 3:
                    //pants
                    switch (subtype)
                    {
                        case 1:
                            //leather pants
                            returnValue = 40;
                            break;
                    }
                    break;
                case 4:
                    //chest
                    switch (subtype)
                    {
                        case 1:
                            //leather chest
                            returnValue = 60;
                            break;
                    }
                    break;
                case 5:
                    //gloves
                    switch (subtype)
                    {
                        case 1:
                            //leather gloves
                            returnValue = 35;
                            break;
                    }
                    break;
            }
            return returnValue;
        }

        public static Affix RollAffix(List<int> AffixRange, int ItemLvl)
        {
            Random rng = new Random();
            Affix returnAffix = new Affix();
            returnAffix.Stat = "";
            returnAffix.Desc = "";
            returnAffix.Value = 0;
            int index = rng.Next(1, AffixRange.Count);
            //Random Stats
            switch (AffixRange[index])
            {
                case 1:
                    returnAffix.Value = rng.Next(10, 20);
                    returnAffix.Desc = "Strength Increased By : " + returnAffix.Value.ToString();
                    returnAffix.Stat = "STR";
                    break;
                case 2:
                    returnAffix.Value = rng.Next(10, 20);
                    returnAffix.Stat = "AGI";
                    returnAffix.Desc = "Agility Increased By : " + returnAffix.Value.ToString();
                    break;
                case 3:
                    returnAffix.Value = rng.Next(10, 20);
                    returnAffix.Stat = "WIS";
                    returnAffix.Desc = "Wisdom Increased By : " + returnAffix.Value.ToString();
                    break;
                case 4:
                    returnAffix.Value = rng.Next(10, 20);
                    returnAffix.Stat = "DEX";
                    returnAffix.Desc = "Dexterity Increased By : " + returnAffix.Value.ToString();
                    break;
                case 5:
                    returnAffix.Value = rng.Next(10, 20);
                    returnAffix.Stat = "LUC";
                    returnAffix.Desc = "Luck Increased By : " + returnAffix.Value.ToString();
                    break;
                case 6:
                    returnAffix.Value = rng.Next(10, 20);
                    returnAffix.Stat = "INT";
                    returnAffix.Desc = "Intelligence Increased By : " + returnAffix.Value.ToString();
                    break;
                case 7:
                    returnAffix.Value = rng.Next(10, 20);
                    returnAffix.Stat = "VIT";
                    returnAffix.Desc = "Vitality Increased By : " + returnAffix.Value.ToString();
                    break;
                case 8:
                    returnAffix.Value = rng.Next(10, 20);
                    returnAffix.Stat = "CON";
                    returnAffix.Desc = "Constitution Increased By : " + returnAffix.Value.ToString();
                    break;
                case 9:
                    returnAffix.Value = rng.Next(50, 250);
                    returnAffix.Stat = "ACC";
                    returnAffix.Desc = "Accuracy Increased By : " + returnAffix.Value.ToString();
                    break;
                case 10:
                    returnAffix.Value = rng.Next(50, 250);
                    returnAffix.Stat = "MACC";
                    returnAffix.Desc = "Melee Accuracy Increased By : " + returnAffix.Value.ToString();
                    break;
                case 11:
                    returnAffix.Value = rng.Next(50, 250);
                    returnAffix.Stat = "RACC";
                    returnAffix.Desc = "Ranged Accuracy Increased By : " + returnAffix.Value.ToString();
                    break;
                case 12:
                    returnAffix.Value = rng.Next(10, 15);
                    returnAffix.Stat = "CC";
                    returnAffix.Desc = "Critical Chance Increased By : " + returnAffix.Value.ToString() + "%";
                    break;
                case 13:
                    returnAffix.Value = rng.Next(10, 15);
                    returnAffix.Stat = "CD";
                    returnAffix.Desc = "Critical Damage Increased By : " + returnAffix.Value.ToString() + "%";
                    break;
                case 14:
                    returnAffix.Value = rng.Next(10, 15);
                    returnAffix.Stat = "ATKSPD";
                    returnAffix.Desc = "Attack Speed Increased By : " + returnAffix.Value.ToString() + "%";
                    break;
                case 15:
                    returnAffix.Value = rng.Next(10, 15);
                    returnAffix.Stat = "CDR";
                    returnAffix.Desc = "CoolDowns Reduced By : " + returnAffix.Value.ToString() + "%";
                    break;
                case 16:
                    returnAffix.Value = rng.Next(10, 25);
                    returnAffix.Stat = "HPFLAT";
                    returnAffix.Desc = "Health Increased By : " + returnAffix.Value.ToString();
                    break;
                case 17:
                    returnAffix.Value = rng.Next(10, 25);
                    returnAffix.Stat = "MPFLAT";
                    returnAffix.Desc = "Mana Increased By : " + returnAffix.Value.ToString();
                    break;
                case 18:
                    returnAffix.Value = rng.Next(5, 20);
                    returnAffix.Stat = "HP";
                    returnAffix.Desc = "Health Increased By : " + returnAffix.Value.ToString() + "%";
                    break;
                case 19:
                    returnAffix.Value = rng.Next(5, 20);
                    returnAffix.Stat = "MP";
                    returnAffix.Desc = "Mana Increased By : " + returnAffix.Value.ToString() + "%";
                    break;
                case 20:
                    returnAffix.Value = rng.Next(10, 50);
                    returnAffix.Stat = "ARM";
                    returnAffix.Desc = "Armour Increased By : " + returnAffix.Value.ToString();
                    break;
                case 21:
                    returnAffix.Value = rng.Next(20, 40);
                    returnAffix.Value += rng.Next((int)(ItemLvl * .5), (int)(ItemLvl * 2));
                    returnAffix.Stat = "EVA";
                    returnAffix.Desc = "Evasion Increased By : " + returnAffix.Value.ToString();
                    break;
                case 22:
                    returnAffix.Value = rng.Next(10, 35);
                    returnAffix.Stat = "MF";
                    returnAffix.Desc = "Magic Find Increased By : " + returnAffix.Value.ToString() + "%";
                    break;
                case 23:
                    returnAffix.Value = rng.Next(10, 35);
                    returnAffix.Stat = "MQ";
                    returnAffix.Desc = "Magic Quantity Increased By : " + returnAffix.Value.ToString() + "%";
                    break;
                case 24:
                    returnAffix.Value = rng.Next(3, 15);
                    returnAffix.Stat = "MR";
                    returnAffix.Desc = "Melee Range Increased By : " + returnAffix.Value.ToString() + "%";
                    break;
                case 25:
                    returnAffix.Value = rng.Next(10, 25);
                    returnAffix.Stat = "EXP";
                    returnAffix.Desc = "Experience Increased By : " + returnAffix.Value.ToString() + "%";
                    break;
                case 26:
                    returnAffix.Value = rng.Next(10, 50);
                    returnAffix.Stat = "FPDMG";
                    returnAffix.Desc = "Physical Damage Increased By : " + returnAffix.Value.ToString();
                    break;
                case 27:
                    returnAffix.Value = rng.Next(3, 10);
                    returnAffix.Stat = "PDMG";
                    returnAffix.Desc = "Physical Damage Increased By : " + returnAffix.Value.ToString() + "%";
                    break;
                case 28:
                    returnAffix.Value = rng.Next(10, 50);
                    returnAffix.Stat = "FMPDMG";
                    returnAffix.Desc = "Melee Physical Damage Increased By : " + returnAffix.Value.ToString();
                    break;
                case 29:
                    returnAffix.Value = rng.Next(3, 10);
                    returnAffix.Stat = "MPDMG";
                    returnAffix.Desc = "Physical Damage Increased By : " + returnAffix.Value.ToString() + "%";
                    break;
                case 30:
                    returnAffix.Value = rng.Next(10, 50);
                    returnAffix.Stat = "FRPDMG";
                    returnAffix.Desc = "Ranged Physical Damage Increased By : " + returnAffix.Value.ToString();
                    break;
                case 31:
                    returnAffix.Value = rng.Next(10, 15);
                    returnAffix.Stat = "RPDMG";
                    returnAffix.Desc = "Ranged Physical Damage Increased By : " + returnAffix.Value.ToString() + "%";
                    break;
                case 32:
                    returnAffix.Value = rng.Next(10, 15);
                    returnAffix.Stat = "RATKSPD";
                    returnAffix.Desc = "Ranged Attack Speed Increased By : " + returnAffix.Value.ToString() + "%";
                    break;
                case 33:
                    returnAffix.Value = rng.Next(10, 15);
                    returnAffix.Stat = "MATKSPD";
                    returnAffix.Desc = "Melee Attack Speed Increased By : " + returnAffix.Value.ToString() + "%";
                    break;
                case 34:
                    returnAffix.Value = rng.Next(10, 15);
                    returnAffix.Stat = "CSPD";
                    returnAffix.Desc = "Cast Speed Increased By : " + returnAffix.Value.ToString() + "%";
                    break;
                case 35:
                    returnAffix.Value = rng.Next(10, 30);
                    returnAffix.Stat = "MVSPD";
                    returnAffix.Desc = "Movement Speed Increased By : " + returnAffix.Value.ToString() + "%";
                    break;
                case 36:
                    returnAffix.Value = rng.Next(10, 15);
                    returnAffix.Stat = "ARPEN";
                    returnAffix.Desc = "Armour Penetration Increased By : " + returnAffix.Value.ToString() + "%";
                    break;
                //WeaponSpecific
                case 37:
                    returnAffix.Value = rng.Next(30, 250);
                    returnAffix.Stat = "WPDMG";
                    returnAffix.Desc = "Weapon Damage Increased By : " + returnAffix.Value.ToString() + "%";
                    break;
                case 38:
                    returnAffix.Value = rng.Next(10, 50);
                    returnAffix.Stat = "FWPDMG";
                    returnAffix.Desc = "Weapon Damage Increased By : " + returnAffix.Value.ToString();
                    break;
                case 39:
                    returnAffix.Value = rng.Next(10, 30);
                    returnAffix.Stat = "WATKSPD";
                    returnAffix.Desc = "Weapon Attack Speed Increased By : " + returnAffix.Value.ToString() + "%";
                    break;
                //Armor Specific -- EVA
                case 40:
                    returnAffix.Value = rng.Next(10, 50);
                    returnAffix.Stat = "FAREVA";
                    returnAffix.Desc = "Item Evasion Increased By : " + returnAffix.Value.ToString();
                    break;
                case 41:
                    returnAffix.Value = rng.Next(30, 250);
                    returnAffix.Stat = "AREVA";
                    returnAffix.Desc = "Item Evasion Increased By : " + returnAffix.Value.ToString() + "%";
                    break;
            }

            AffixRange.Remove(AffixRange[index]);
            return returnAffix;
        }

        public static float CalculateMeleePhysStat(List<Affix> affixes, int BasePhys)
        {
            float returnValue = BasePhys;
            foreach (Affix afx in affixes)
            {
                if (afx.Stat == "FWPDMG")
                {
                    returnValue += afx.Value;
                    break;
                }
            }

            foreach (Affix afx in affixes)
            {
                if (afx.Stat == "WPDMG")
                {
                    returnValue = returnValue * (1 + (float)(afx.Value * .01));
                }
            }

            return returnValue;
        }

        public static float CalculateMeleeAttackSpeed(List<Affix> affixes, float BaseAtkSpd)
        {
            float returnValue = BaseAtkSpd;
            foreach (Affix afx in affixes)
            {
                if (afx.Stat == "WATKSPD")
                {
                    returnValue = returnValue * (1 + (float)(afx.Value * .01));
                    if (returnValue.ToString().Length > 3)
                    {
                        returnValue = (float)(Math.Round(returnValue, 2, MidpointRounding.AwayFromZero));
                    }
                }
            }
            return returnValue;
        }

        public static int CalculateEvasion(List<Affix> affixes, int baseEV)
        {
            int returnValue = baseEV;
            foreach (Affix afx in affixes)
            {
                if (afx.Stat == "FAREVA")
                {
                    returnValue += afx.Value;
                    break;
                }
            }

            foreach (Affix afx in affixes)
            {
                if (afx.Stat == "AREVA")
                {
                    returnValue = (int)(returnValue * (1 + (float)(afx.Value * .01)));
                }
            }
            return returnValue;
        }

        public static float RollMeleeHitDamage(bool isCrit, int enemyArmour)
        {
            enemyArmour -= (int)(enemyArmour * (CharacterArmourPenetration / 100));
            Random rng = new Random();
            int basedmg = rng.Next(GlobalVariables.CharacterMinDmg, GlobalVariables.CharacterMaxDmg);
            float damage = (basedmg + CharacterIncreaseFlatMelee + CharacterIncreaseFlatPhysical);
            damage += damage * (CharacterIncreasePhysDmg + CharacterIncreasePhyMeleeDmg);
            damage -= (damage * (int)(enemyArmour / 100));
            return damage;
        }

        public static int RollVsItemType()
        {
            Random rng = new Random();
            int type = rng.Next(1, 9);
            return type;
        }

        public static int RollVsWeaponType()
        {
            Random rng = new Random();
            int type = rng.Next(1, 1);
            return type;
        }

        public static int RollVsPantType()
        {
            //1 is leather pants
            Random rng = new Random();
            int type = rng.Next(1, 1);
            return type;
        }

        public static int RollVsChestType()
        {
            //1 is leather chest    
            Random rng = new Random();
            int type = rng.Next(1, 1);
            return type;
        }

        public static int RollVsGlovesType()
        {
            //1 is leather chest    
            Random rng = new Random();
            int type = rng.Next(1, 1);
            return type;
        }

        public static int RollVsRingType()
        {
            //1 is leather chest    
            Random rng = new Random();
            int type = rng.Next(1, 1);
            return type;
        }

        public static int RollVsBeltType()
        {
            //7 Belt   
            Random rng = new Random();
            int type = rng.Next(1, 1);
            return type;
        }

        public static int RollVsHelmType()
        {
            //8 Helm
            Random rng = new Random();
            int type = rng.Next(1, 1);
            return type;
        }

        public static int RollVsBootType()
        {
            //1 is leather boots
            Random rng = new Random();
            int type = rng.Next(1, 1);
            return type;
        }

        public static int RollVsTwoHanderType()
        {
            Random rng = new Random();
            int type = rng.Next(1, 1);
            return type;
        }

        public static int ItemRarity()
        {
            //Roll Logic Here, For Testing we will Return 5
            return 5;
        }

        public static void WeaponEquiped(Item item, int leftright)
        {
            //1 == left, 2 == right
            //Set the texture
            switch (leftright)
            {
                case 1:
                    GlobalVariables.TheGame.CharWeapon = TheGame.Content.Load<Texture2D>(item.ItemTextureName);
                    TheGame.equipment.Hero.txtLeftWeapon = TheGame.CharWeapon;
                    break;
                case 2:
                    GlobalVariables.TheGame.CharWeapon = TheGame.Content.Load<Texture2D>(item.ItemTextureName);
                    TheGame.equipment.Hero.txtRightWeapon = TheGame.CharWeapon;
                    break;
            }

        }

        public static void BootsEquipped(Item item)
        {
            GlobalVariables.TheGame.CharBoots = TheGame.Content.Load<Texture2D>(item.ItemTextureName);
            TheGame.equipment.Hero.txtBoots = TheGame.CharBoots;
        }

        public static void PantsEquipped(Item item)
        {
            GlobalVariables.TheGame.CharPants = TheGame.Content.Load<Texture2D>(item.ItemTextureName);
            TheGame.equipment.Hero.txtPants = TheGame.CharPants;
        }

        //public static void BeltEquipped(Item item)
        //{
        //    GlobalVariables.TheGame.CharBelt = TheGame.Content.Load<Texture2D>(item.ItemTextureName);
        //    TheGame.equipment.Hero.txtBelt = TheGame.CharBelt;
        //}

        public static void HelmetEquipped(Item item)
        {
            GlobalVariables.TheGame.CharHelmet = TheGame.Content.Load<Texture2D>(item.ItemTextureName);
            TheGame.equipment.Hero.txtHelm = TheGame.CharHelmet;
            TheGame.equipment.Hero.txtHero = TheGame.Content.Load<Texture2D>("SpriteSheetHelm");
        }

        public static void GlovesEquipped(Item item)
        {
            GlobalVariables.TheGame.CharGloves = TheGame.Content.Load<Texture2D>(item.ItemTextureName);
            TheGame.equipment.Hero.txtGloves = TheGame.CharGloves;
        }

        public static void ChestEquipped(Item item)
        {
            GlobalVariables.TheGame.CharChest = TheGame.Content.Load<Texture2D>(item.ItemTextureName);
            TheGame.equipment.Hero.txtChest = TheGame.CharChest;
        }

        public static string GetItemDescription(int type, int subType, bool isUnique)
        {
            string itemDesc = "";
            switch (type)
            {
                //Two Handed Sword
                case 1:
                    switch (subType)
                    {
                        //Starter Sword
                        case 1:

                            if (isUnique)
                            {
                                itemDesc = "A sword thought to have been lost in The Second Great Battle.  It belonged to a heroic Knight of great valor.";
                            }
                            else
                            {
                                itemDesc = "This sword looks pretty rough, the previous owner definately got their money's worth out of it.";
                            }
                            break;
                    }
                    break;
                //Boots
                case 2:
                    switch (subType)
                    {
                        //Leather Boots
                        case 1:
                            if (isUnique)
                            {
                                itemDesc = "Coined after the notoriously foul footed Trudy TrompStomper, these boots have only been referred to by legend.";
                            }
                            else
                            {
                                itemDesc = "Average leather boots, they look a bit ragged.";
                            }
                            break;
                    }
                    break;
                //Pants
                case 3:
                    switch (subType)
                    {
                        //Leather Pants
                        case 1:
                            if (isUnique)
                            {
                                itemDesc = "Used by the ancient monks of Tal'Knesh and known for their cutting edge design, these pants are made to tear and rend flesh.";
                            }
                            else
                            {
                                itemDesc = "Leather Pants, Offering low protection.";
                            }
                            break;
                    }
                    break;
                //Chest
                case 4:
                    switch (subType)
                    {
                        //Leather Chest
                        case 1:
                            if (isUnique)
                            {
                                itemDesc = "This tunic has been lost in history for many years.  The fact that it has now been found is foreboding at best.";
                            }
                            else
                            {
                                itemDesc = "A ordinary leather chest piece.";
                            }
                            break;
                    }
                    break;
                //Gloves
                case 5:
                    switch (subType)
                    {
                        //Leather Gloves
                        case 1:
                            if (isUnique)
                            {
                                itemDesc = "Mythic Gloves imbued with Titanic Power.  There is little documentation left of their origin.";
                            }
                            else
                            {
                                itemDesc = "Simple leather gloves.";
                            }
                            break;
                    }
                    break;
                //Ring
                case 6:
                    switch (subType)
                    {
                        //Gold Ring
                        case 1:
                            if (isUnique)
                            {
                                itemDesc = "Sages Ring.  Found in the bottom of an old Hermits Dwelling.  Unknown Power Source.";
                            }
                            else
                            {
                                itemDesc = "An old gold ring.";
                            }
                            break;
                    }
                    break;
                //Belt
                case 7:
                    switch (subType)
                    {
                        //Leather Belt
                        case 1:
                            if (isUnique)
                            {
                                itemDesc = "Very few of these belts exist, after their use was banished when user's noticed it tightens over time, causing asphyxiation.";
                            }
                            else
                            {
                                itemDesc = "Typical Leather Belt.  Offers little protection.";
                            }
                            break;
                    }
                    break;
                //Helmet
                case 8:
                    switch (subType)
                    {
                        //Leather Helmet
                        case 1:
                            if (isUnique)
                            {
                                itemDesc = "A cap that has retained the eye of Rah whom it is said was the most powerful of Mages in the 5th circle of the Blue Magi.";
                            }
                            else
                            {
                                itemDesc = "A worn leather cap.  Truly ordinary.";
                            }
                            break;
                    }
                    break;
            }
            return itemDesc;
        }

        public static List<string> GenerateDescList(string desc, double width, SpriteFont font)
        {
            List<string> DescList = new List<string>();

            double totWidth = font.MeasureString(desc).X;

            if (totWidth > width)
            {
                string newString = "";
                bool blnNewIndex = false;
                int newIndex = 0;
                for (int intlc = 0; intlc < desc.Length - 1; intlc++)
                {
                    newString += desc[intlc];
                    if (blnNewIndex)
                    {
                        newIndex += 1;
                        if (intlc == desc.Length - 2)
                        {
                            newString += ".";
                            DescList.Add(newString);
                        }
                        if (desc[intlc].ToString() == " ")
                        {
                            if (Font10.MeasureString(newString).X > width)
                            {
                                string lastWord = "";
                                int wordLength = 0;
                                newString = newString.Remove(newIndex - 1, 1);
                                intlc -= 1;
                                for (int intlc1 = newIndex; intlc1 != 0; intlc1--)
                                {
                                    lastWord += newString[intlc1 - 2];
                                    wordLength += 1;
                                    if (lastWord[wordLength - 1].ToString() == " ")
                                    {
                                        lastWord = "";
                                        break;
                                    }
                                }
                                newString = newString.Remove(newString.Length - wordLength, wordLength);
                                intlc -= wordLength;
                                DescList.Add(newString);
                                newString = "";
                                wordLength = 0;
                                newIndex = 0;
                                blnNewIndex = true;
                                lastWord = "";
                            }
                        }
                    }
                    else
                    {
                        if (intlc == desc.Length - 2)
                        {
                            newString += ".";
                            DescList.Add(newString);
                        }
                        if (newString[intlc].ToString() == " ")
                        {
                            if (Font10.MeasureString(newString).X > width)
                            {
                                string lastWord = "";
                                int wordLength = 0;
                                newString.Remove(intlc, 1);
                                intlc -= 1;
                                for (int intlc1 = intlc; intlc1 != 0; intlc1--)
                                {
                                    lastWord += newString[intlc1];
                                    wordLength += 1;
                                    if (lastWord[wordLength - 1].ToString() == " ")
                                    {
                                        wordLength -= 1;
                                        lastWord = "";
                                        break;
                                    }
                                }
                                newString = newString.Remove(intlc - (wordLength), wordLength + 2);
                                intlc -= wordLength + 1;
                                DescList.Add(newString);
                                newString = "";
                                wordLength = 0;
                                newIndex = 0;
                                blnNewIndex = true;
                                lastWord = "";
                            }
                        }
                    }
                }
            }
            else
            {
                DescList.Add(desc);
            }

            return DescList;
        }

        public static string GetTexture(int type, int subType, bool isUnique)
        {
            string itemName = "";
            switch (type)
            {
                //Two Handed Sword
                case 1:
                    switch (subType)
                    {
                        //Starter Sword
                        case 1:

                            if (isUnique)
                            {
                                itemName = "HeroSS2H1U";
                            }
                            else
                            {
                                itemName = "HeroSS2H1";
                            }
                            break;
                    }
                    break;
                //Boots
                case 2:
                    switch (subType)
                    {
                        //Leather Boots
                        case 1:
                            if (isUnique)
                            {
                                itemName = "HeroSS2HBootsU";
                            }
                            else
                            {
                                itemName = "HeroSS2HBoots";
                            }
                            break;
                    }
                    break;
                //Pants
                case 3:
                    switch (subType)
                    {
                        //Leather Pants
                        case 1:
                            if (isUnique)
                            {
                                itemName = "HeroSS2HPantsU";
                            }
                            else
                            {
                                itemName = "HeroSS2HPants";
                            }
                            break;
                    }
                    break;
                //Chest
                case 4:
                    switch (subType)
                    {
                        //Leather Chest
                        case 1:
                            if (isUnique)
                            {
                                itemName = "HeroSS2HChest1U";
                            }
                            else
                            {
                                itemName = "HeroSS2HChest1";
                            }
                            break;
                    }
                    break;
                //Gloves
                case 5:
                    switch (subType)
                    {
                        //Leather Gloves
                        case 1:
                            if (isUnique)
                            {
                                itemName = "HeroSS2HGloves1";
                            }
                            else
                            {
                                itemName = "HeroSS2HGloves1";
                            }
                            break;
                    }
                    break;

                    //no case 6 or 7 for ring and belt since we don't draw them on the character

                //Helmet
                case 8:
                    switch (subType)
                    {
                        //Leather Gloves
                        case 1:
                            if (isUnique)
                            {
                                itemName = "HeroSS2HLeatherHelmetU";
                            }
                            else
                            {
                                itemName = "HeroSS2HLeatherHelmet";
                            }
                            break;
                    }
                    break;
            }
            return itemName;
        }

        public static string GetItemName(int type, int subType, bool isUnique)
        {
            string itemName = "";
            switch (type)
            {
                //Two Handed Sword
                case 1:
                    switch (subType)
                    {
                        //Starter Sword
                        case 1:

                            if (isUnique)
                            {
                                itemName = "Two-HandedSwordU";
                            }
                            else
                            {
                                itemName = "Two-HandedSword";
                            }
                            break;
                    }
                    break;
                //Boots
                case 2:
                    switch (subType)
                    {
                        //Leather Boots
                        case 1:
                            if (isUnique)
                            {
                                itemName = "LeatherbootsU";
                            }
                            else
                            {
                                itemName = "Leatherboots";
                            }
                            break;
                    }
                    break;
                //Pants
                case 3:
                    switch (subType)
                    {
                        //Leather Pants
                        case 1:
                            if (isUnique)
                            {
                                itemName = "LeatherPantsU";
                            }
                            else
                            {
                                itemName = "LeatherPants";
                            }
                            break;
                    }
                    break;
                //Chest
                case 4:
                    switch (subType)
                    {
                        //Leather Pants
                        case 1:
                            if (isUnique)
                            {
                                itemName = "LeatherChestU";
                            }
                            else
                            {
                                itemName = "LeatherChest";
                            }
                            break;
                    }
                    break;
                //Gloves
                case 5:
                    switch (subType)
                    {
                        //Leather Pants
                        case 1:
                            if (isUnique)
                            {
                                itemName = "Leathergloves";
                            }
                            else
                            {
                                itemName = "Leathergloves";
                            }
                            break;
                    }
                    break;
                //Ring
                case 6:
                    switch (subType)
                    {
                        //Goldring
                        case 1:
                            if (isUnique)
                            {
                                itemName = "Ring1U";
                            }
                            else
                            {
                                itemName = "Ring1";
                            }
                            break;
                    }
                    break;
                //Belt
                case 7:
                    switch (subType)
                    {
                        //Leather Belt
                        case 1:
                            if (isUnique)
                            {
                                itemName = "Leatherbelt";
                            }
                            else
                            {
                                itemName = "Leatherbelt";
                            }
                            break;
                    }
                    break;
                //helmet
                case 8:
                    switch (subType)
                    {
                        //Leather Helmet
                        case 1:
                            if (isUnique)
                            {
                                itemName = "LeatherHelmetU";
                            }
                            else
                            {
                                itemName = "LeatherHelmet";
                            }
                            break;
                    }
                    break;
            }
            return itemName;
        }

        public static void UpdateChar(Pos pos, AnimatedSprite Character, Game1 Game, Vector2 Velocity, Vector2 VelocityUp, Dir dir = Dir.Nothing)
        {

            if (dir != Dir.Nothing)
            {
                switch (dir)
                {
                    case Dir.Up:

                        pos = Pos.IsDown;
                        break;

                    case Dir.Down:

                        pos = Pos.IsUp;
                        break;

                    case Dir.Left:

                        pos = Pos.IsRight;
                        break;

                    case Dir.Right:

                        pos = Pos.IsLeft;
                        break;

                }
            }

            switch (pos)
            {
                case Pos.IsDown:

                    CurrentDir = Dir.Up;
                    break;

                case Pos.IsUp:

                    CurrentDir = Dir.Down;
                    break;

                case Pos.IsRight:

                    CurrentDir = Dir.Left;
                    break;

                case Pos.IsLeft:

                    CurrentDir = Dir.Right;
                    break;

                case Pos.IsDownLeft:

                    CurrentDir = Dir.UpRight;
                    break;

                case Pos.IsDownRight:

                    CurrentDir = Dir.UpLeft;
                    break;

                case Pos.IsUpright:

                    CurrentDir = Dir.DownLeft;
                    break;

                case Pos.IsUpLeft:

                    CurrentDir = Dir.DownRight;
                    break;

            }
        }

        public static bool MoveCharacterToPosition(AnimatedSprite Character, Game1 Game, Vector2 MoveTo, Vector2 Velocity, Vector2 VelocityUp, List<Enemy> enemies)
        {
            Rectangle HeroMeleeRange = new Rectangle((Character.Bounds.X - CharacterMeleeRange), (Character.Bounds.Y - CharacterMeleeRange), (Character.Bounds.Width + (CharacterMeleeRange * 2)), (Character.Bounds.Height + (CharacterMeleeRange * 2)));

            foreach (Enemy en in enemies)
            {
                if (en.CharacterAttacked)
                {
                    if (HeroMeleeRange.Intersects(en.Bounds))
                    {
                        CurrentDir = Dir.Nothing;
                        MoveToLoc = new Vector2(0, 0);
                        return true;
                    }
                }
            }

            if (MoveToLoc.X == 0 && MoveToLoc.Y == 0)
            {
                if (MoveTo.X < ((Character.Height + 10) / 2))
                {
                    MoveTo.X = (Character.Height + 10) / 2;
                }
                if (MoveTo.Y < ((Character.Width + 10) / 2))
                {
                    MoveTo.Y = ((Character.Width + 10) / 2);
                }
                if (Game.animatedSprite.WorldPos.X + (Game.graphics.PreferredBackBufferWidth / 2) >= (Game.Columns * Game.TileWidth) - 10)
                {
                    if (MoveTo.X > (gfx.PreferredBackBufferWidth) - (Character.Width))
                    {
                        MoveTo.X = Convert.ToInt32(MoveTo.X - (Character.Width));
                    }
                }
                if (Game.animatedSprite.WorldPos.Y + (Game.graphics.PreferredBackBufferHeight / 2) >= (Game.Rows * Game.TileHeight) - 10)
                {
                    if (MoveTo.Y > (gfx.PreferredBackBufferHeight) - (Character.Height))
                    {
                        MoveTo.Y = Convert.ToInt32(MoveTo.Y - (Character.Height));
                    }
                }
                MoveToLoc = MoveTo;
            }

            Velocity = (Velocity * (float)Game.theGameTime.ElapsedGameTime.TotalSeconds);
            VelocityUp = (VelocityUp * (float)Game.theGameTime.ElapsedGameTime.TotalSeconds);

            List<EnemyPos> EnmyPos = new List<EnemyPos>();
            Vector2 Difference = (new Vector2((Character.position.X + (Character.Width / 2)), Character.position.Y + (Character.Height / 2))) - MoveToLoc;

            if (Difference.X <= 10 && Difference.X >= -10 && Difference.Y <= 10 && Difference.Y >= -10)
            {
                CurrentDir = Dir.Nothing;
                MoveToLoc = new Vector2(0, 0);
                return true;
            }

            foreach (Enemy enemy in enemies)
            {
                Vector2 Dif = Character.position - enemy.Location;

                if (Dif.X < 0)
                {
                    //Im to the left
                    Rectangle newRect = new Rectangle(Character.Bounds.X, Character.Bounds.Y, Character.Bounds.Width + Convert.ToInt32(Velocity.X), Character.Bounds.Height);
                    if (newRect.Intersects(enemy.Bounds))
                    {
                        EnmyPos.Add(EnemyPos.IsRight);
                    }
                }
                if (Dif.X > 0)
                {
                    //Im to the right

                    Rectangle newRect = new Rectangle(Character.Bounds.X - Convert.ToInt32(Velocity.X), Character.Bounds.Y, Character.Bounds.Width, Character.Bounds.Height);
                    if (newRect.Intersects(enemy.Bounds))
                    {
                        EnmyPos.Add(EnemyPos.IsLeft);
                    }
                }
                if (Dif.Y < 0)
                {
                    //Im Above

                    Rectangle newRect = new Rectangle(Character.Bounds.X, Character.Bounds.Y, Character.Bounds.Width, Character.Bounds.Height + Convert.ToInt32(VelocityUp.Y));
                    if (newRect.Intersects(enemy.Bounds))
                    {
                        EnmyPos.Add(EnemyPos.IsDown);
                    }
                }
                if (Dif.Y > 0)
                {
                    //Im Below

                    Rectangle newRect = new Rectangle(Character.Bounds.X, Character.Bounds.Y - Convert.ToInt32(VelocityUp.Y), Character.Bounds.Width, Character.Bounds.Height);
                    if (newRect.Intersects(enemy.Bounds))
                    {
                        EnmyPos.Add(EnemyPos.IsUp);
                    }
                }
            }

            if (Difference.X > 0 && Math.Abs(Difference.Y) < 10)
            {
                CharPos = Pos.IsRight;
            }
            else if (Difference.X < 0 && Math.Abs(Difference.Y) < 10)
            {
                CharPos = Pos.IsLeft;
            }
            else if (Math.Abs(Difference.X) < 10 && Difference.Y > 0)
            {
                CharPos = Pos.IsDown;
            }
            else if (Math.Abs(Difference.X) < 10 && Difference.Y < 0)
            {
                CharPos = Pos.IsUp;
            }
            else if (Difference.X < 0 && Difference.Y < 0)
            {
                CharPos = Pos.IsUpLeft;
            }
            else if (Difference.X > 0 && Difference.Y < 0)
            {
                CharPos = Pos.IsUpright;
            }
            else if (Difference.X < 0 && Difference.Y > 0)
            {
                CharPos = Pos.IsDownLeft;
            }
            else if (Difference.X > 0 && Difference.Y > 0)
            {
                CharPos = Pos.IsDownRight;
            }
            else
            {
                CharPos = Pos.IsHere;
                MoveToLoc = new Vector2(0, 0);
                return true;
            }

            if (Difference.X < Velocity.X && Difference.X > 0)
            {
                Velocity.X = Difference.X;
            }
            if (Difference.X > (Velocity.X - (Velocity.X * 2)) && Difference.X < 0)
            {
                Velocity.X = Difference.X;
            }
            if (Difference.Y < VelocityUp.Y && Difference.Y > 0)
            {
                VelocityUp.Y = Difference.Y;
            }
            if (Difference.Y > (VelocityUp.Y - (VelocityUp.Y * 2)) && Difference.Y < 0)
            {
                VelocityUp.Y = Difference.Y;
            }

            if (EnmyPos.Count == 0)
            {
                UpdateChar(CharPos, Character, Game, Velocity, VelocityUp);
                return false;
            }
            else
            {
                PathFind(CharPos, EnmyPos);
                UpdateChar(CharPos, Character, Game, Velocity, VelocityUp, CurrentDir);
                return false;
            }

        }

        public static bool TryRight(List<EnemyPos> EnmyPos)
        {
            //Check Right
            bool valid = true;
            foreach (EnemyPos enPos in EnmyPos)
            {
                if (enPos == EnemyPos.IsRight)
                {
                    valid = false;
                    break;
                }
                else
                {
                    CurrentDir = Dir.Right;
                    valid = true;
                }
            }

            return valid;
        }

        public static bool TryLeft(List<EnemyPos> EnmyPos)
        {
            //Check Left
            bool valid = true;
            foreach (EnemyPos enPos in EnmyPos)
            {
                if (enPos == EnemyPos.IsLeft)
                {
                    valid = false;
                    break;
                }
                else
                {
                    CurrentDir = Dir.Left;
                    valid = true;
                }
            }
            return valid;
        }

        public static bool TryUp(List<EnemyPos> EnmyPos)
        {
            //Check Up
            bool valid = true;
            foreach (EnemyPos enPos in EnmyPos)
            {
                if (enPos == EnemyPos.IsUp)
                {
                    valid = false;
                    break;
                }
                else
                {
                    CurrentDir = Dir.Up;
                    valid = true;
                }
            }
            return valid;
        }

        public static bool TryDown(List<EnemyPos> EnmyPos)
        {
            //Check Down
            bool valid = true;
            foreach (EnemyPos enPos in EnmyPos)
            {
                if (enPos == EnemyPos.IsDown)
                {
                    valid = false;
                    break;
                }
                else
                {
                    CurrentDir = Dir.Down;
                    valid = true;
                }
            }
            return valid;
        }

        public static void PathFind(Pos CharPos, List<EnemyPos> EnmyPos)
        {
            PathFindingFail = true;
            bool result = false;

            switch (CharPos)
            {
                case Pos.IsDown:

                    result = TryUp(EnmyPos);
                    if (!result)
                    {
                        TryRight(EnmyPos);
                    }

                    break;

                case Pos.IsDownLeft:

                    result = TryRight(EnmyPos);
                    if (!result)
                    {
                        TryDown(EnmyPos);
                    }

                    break;

                case Pos.IsDownRight:

                    result = TryUp(EnmyPos);
                    if (!result)
                    {
                        TryRight(EnmyPos);
                    }

                    break;

                case Pos.IsUp:

                    result = TryLeft(EnmyPos);
                    if (!result)
                    {
                        TryDown(EnmyPos);
                    }

                    break;

                case Pos.IsUpLeft:

                    result = TryDown(EnmyPos);
                    if (!result)
                    {
                        TryLeft(EnmyPos);
                    }

                    break;

                case Pos.IsUpright:

                    result = TryLeft(EnmyPos);
                    if (!result)
                    {
                        TryUp(EnmyPos);
                    }

                    break;

                case Pos.IsLeft:

                    result = TryDown(EnmyPos);
                    if (!result)
                    {
                        TryLeft(EnmyPos);
                    }

                    break;

                case Pos.IsRight:

                    result = TryUp(EnmyPos);
                    if (!result)
                    {
                        TryRight(EnmyPos);
                    }

                    break;

            }
        }
    }
}

