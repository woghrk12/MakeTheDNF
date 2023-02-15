using System.Collections.Generic;
using UnityEngine;

public class SkillStat
{
    public enum ESkillType
    {
        NONE = -1,
        ACTIVE,
        PASSIVE,
        BUFF,
    }

    public enum EAcquireLevel
    {
        NONE = -1,
        BASEATTACK,
        ZERO,
        FIVE,
        TEN,
        FIFTEEN,
        TWENTY,
        TWENTYFIVE,
        THIRTY,
    }

    #region Variables

    // Identity
    public int skillID = -1;

    // Skill Icon
    public string skillIconPath = string.Empty;
    public string skillIconName = string.Empty;
    public Sprite skillIcon = null;

    // Skill Stat
    public EClassType classType = EClassType.NONE;
    public ESkillType skillType = ESkillType.NONE;
    public float coolTime = 0f;
    public int needMana = 0;
    public float preDelay = 0f;
    public float duration = 0f;
    public float postDelay = 0f;

    // Skill Motion
    public bool isNoMotion = false;
    public string skillMotion = string.Empty;

    // Skill Effect
    public EEffectList skillEffect = EEffectList.NONE;

    // Acquire Level
    // minimum level value for acquiring the skill
    public EAcquireLevel acquireLevel = 0;
    // minimum and maximum level value
    public int minLevel = 0, maxLevel = 0;
    // interval level value for raising skill level
    public int stepLevel = 0;
    // need point value for raising skill level
    public int needPoint = 0;

    // Skill List Info
    public List<int> canCancelList = new List<int>();
    public List<int> preLearnedList = new List<int>();

    #endregion Variables

    #region Methods

    public void PreLoadIcon()
    {
        if (skillIcon != null) return;
        skillIcon = Resources.Load(skillIconPath + skillIconName, typeof(Sprite)) as Sprite;
    }

    public void ReleaseIcon()
    {
        if (skillIcon == null) return;
        skillIcon = null;
    }

    #endregion Methods
}
