using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XmlElementName
{
    public const string SOUND = "Sound";
    public const string EFFECT = "Effect";
    public const string SKILL = "Skill";
    public const string INPUT = "Input";
    // Data Attribute
    public const string LENGTH = "Length";

    // Common Identity Attribute
    public const string ID = "ID";
    public const string NAME = "Name";

    public class SoundData
    {
        public const string SOUNDSETTINGS = "SoundSettings";

        // Sound Clip
        public const string SOUNDCLIP = "SoundClip";
        public const string CLIPPATH = "ClipPath";
        public const string CLIPNAME = "ClipName";

        // Options
        public const string OPTIONS = "Options";
        public const string PLAYTYPE = "PlayType";
        public const string MAXVOLUME = "MaxVolume";
        public const string PITCH = "Pitch";
        public const string SPATIALBLEND = "SpatialBlend";

        // Loop Options
        public const string LOOPOPTIONS = "LoopOptions";
        public const string ISLOOP = "Loop";
        public const string CNTLOOP = "CntLoop";
        public const string STARTLOOP = "StartLoop";
        public const string CHECKTIME = "CheckTime";
        public const string SETTIME = "SetTime";
    }

    public class EffectData
    {
        public const string EFFECTSETTINGS = "EffectSettings";

        // Sound Clip
        public const string EFFECTCLIP = "EffectClip";
        public const string CLIPPATH = "ClipPath";
        public const string CLIPNAME = "ClipName";

        // Options
        public const string OPTIONS = "Options";
        public const string PLAYTYPE = "PlayType";
    }

    public class SkillData
    {
        public const string SKILLINFO = "SkillInfo";

        // Skill Icon
        public const string SKILLICON = "SkillIcon";
        public const string ICONFILEPATH = "IconFilePath";
        public const string ICONFILENAME = "IconFileName";

        // Skill Stat
        public const string SKILLSTAT = "SkillStat";
        public const string CLASSTYPE = "ClassType";
        public const string SKILLTYPE = "SkillType";
        public const string COOLTIME = "CoolTime";
        public const string NEEDMANA = "NeedMana";
        public const string PREDELAY = "PreDelay";
        public const string DURATION = "Duration";
        public const string POSTDELAY = "PostDelay";

        // Skill Motion
        public const string MOTION = "Motion";
        public const string ISNOMOTION = "IsNoMotion";
        public const string SKILLMOTION = "SkillMotion";

        // Skill Effect
        public const string SKILLEFFECT = "SkillEffect";
        public const string NUMSKILLEFFECT = "NumSkillEffect";
        public const string SKILLEFFECTPATH = "SkillEffectPath";
        public const string SKILLEFFECTNAME = "SkillEffectName";
        public const string EFFECTOFFSET = "EffectOffset";

        // Acquire Level
        public const string ACQUIRE = "Acquire";
        public const string ACQUIRELEVEL = "AcquireLevel";
        public const string MINLEVEL = "MinLevel";
        public const string MAXLEVEL = "MaxLevel";
        public const string STEPLEVEL = "StepLevel";
        public const string NEEDPOINT = "NeedPoint";

        // Skill List
        public const string LIST = "List";
        public const string CANCANCELLIST = "CanCancelList";
        public const string PRELEAREDLIST = "PreLearnedList";
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
