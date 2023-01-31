using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XmlElementName
{
    public const string SOUND = "Sound";
    public const string EFFECT = "Effect";
    public const string INPUT = "Input";
    // Data Attribute
    public const string LENGTH = "Length";

    // Identity Element
    public const string IDENTITY = "Identity";
    // Common Identity Attribute
    public const string ID = "ID";
    public const string NAME = "Name";
    public const string CLIPPATH = "ClipPath";
    public const string CLIPNAME = "ClipName";

    public class SoundData
    {
        // Options Element
        public const string OPTIONS = "Options";
        // Options Attribute
        public const string PLAYTYPE = "PlayType";
        public const string MAXVOLUME = "MaxVolume";
        public const string PITCH = "Pitch";
        public const string SPATIALBLEND = "SpatialBlend";

        // Loop Element
        public const string LOOPOPTIONS = "LoopOptions";
        // Loop Attribute
        public const string ISLOOP = "Loop";
        public const string CNTLOOP = "CntLoop";
        public const string STARTLOOP = "StartLoop";
        public const string CHECKTIME = "CheckTime";
        public const string SETTIME = "SetTime";
    }

    public class EffectData
    { 
    
    }

    public class InputData
    {
        // Control movement behaviour
        public const string MOVE = "Move";
        public const string POSHORIZONTAL = "PosHorizontal";
        public const string NEGHORIZONTAL = "NegHorizontal";
        public const string POSVERTICAL = "PosVertical";
        public const string NEGVERTICAL = "NegVertical";

        // Control basic behaviour
        public const string Attack = "Attack";
        public const string Jump = "Jump";

        // Control skill slots
        public const string SKILLSLOTS = "SkillSlots";
        public const string SKILLSLOT1 = "SkillSlot1";
        public const string SKILLSLOT2 = "SkillSlot2";
        public const string SKILLSLOT3 = "SkillSlot3";
        public const string SKILLSLOT4 = "SkillSlot4";
        public const string SKILLSLOT5 = "SkillSlot5";
        public const string SKILLSLOT6 = "SkillSlot6";
        public const string SKILLSLOT7 = "SkillSlot7";
        public const string SKILLSLOT8 = "SkillSlot8";
        public const string SKILLSLOT9 = "SkillSlot9";
        public const string SKILLSLOT10 = "SkillSlot10";
        public const string SKILLSLOT11 = "SkillSlot11";
        public const string SKILLSLOT12 = "SkillSlot12";

        // Control UI
        public const string UI = "UI";
        public const string INVENTORY = "Inventory";
        public const string SKILL = "Skill";
        public const string CHARINFO = "CharInfo";
    }
}
