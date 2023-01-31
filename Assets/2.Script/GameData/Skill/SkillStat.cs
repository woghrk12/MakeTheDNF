using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillInfo
{
    public string skillMotion = string.Empty;
    public int numSkillEffect = 0;
    public string[] skillEffectPaths = new string[0];
    public Vector3[] effectOffsets = new Vector3[0];
    public Vector3 skillRange = Vector3.zero;
    public Vector3 rangeOffset = Vector3.zero;
    public float preDelay = 0f;
    public float duration = 0f;
    public float postDelay = 0f;
}

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
    public int skillID = 0;
    public string clipPath = string.Empty;
    public string clipName = string.Empty;

    // Skill Info
    public string skillIconPath = string.Empty;
    public EClassType classType = EClassType.NONE;
    public ESkillType skillType = ESkillType.NONE;
    public float coolTime = 0f;
    public int needMana = 0;
    public bool isNoMotion = false;
    public int numCombo = 0;
    public SkillInfo[] skillInfo = null;

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

    /*
    private void OnValidate()
    {
        if (numCombo != skillInfo.Length) numCombo = skillInfo.Length;

        foreach (SkillInfo t_info in skillInfo)
        {
            if (t_info.numSkillEffect != t_info.skillEffects.Length) t_info.numSkillEffect = t_info.skillEffects.Length;

            while (t_info.numSkillEffect != t_info.effectOffsets.Length)
            {
                if (t_info.numSkillEffect < t_info.effectOffsets.Length) 
                    t_info.effectOffsets = ArrayHelper.Remove(t_info.effectOffsets.Length - 1, t_info.effectOffsets);
                else t_info.effectOffsets = ArrayHelper.Add(new Vector3(), t_info.effectOffsets);
            }
        }
    }*/
}
