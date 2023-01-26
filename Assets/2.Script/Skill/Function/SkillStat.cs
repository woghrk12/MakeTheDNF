using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Skill", menuName = "Skill System/Skill")]
public class SkillStat : ScriptableObject
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

    [Serializable]
    public class SkillInfo
    {
        public string skillMotion = string.Empty;
        public EffectList[] skillEffects = new EffectList[0];
        public float preDelay = 0f;
        public float duration = 0f;
        public float postDelay = 0f;
    }

    #region Variables

    [Header("Skill Info")]
    public int skillID = 0;
    public ESkillType skillType = ESkillType.NONE;
    public int numCombo = 0;
    public SkillInfo[] skillInfo = null;    
    public float coolTime = 0f;

    [Header("Acquire Skill")]
    // minimum level value for acquiring the skill
    public EAcquireLevel acquireLevel = 0;
    // interval level value for raising skill level
    public int stepLevel = 0;
    // need point value for raising skill level
    public int needPoint = 0;
    // current level value of the skill
    public int skillLevel = 0;

    [Header("Need Mana")]
    // base mana value for use skill, increasing in proportion to skill level
    public int needBaseMana = 0;
    public int needMana = 0;

    [Header("Skill List Info")]
    public List<Skill> canCancelList = new List<Skill>();
    public Skill[] preLearnedList = new Skill[0];

    #endregion Variables

    private void OnValidate()
    {
        if (numCombo == skillInfo.Length) return;
        else 
        {
            numCombo = skillInfo.Length;
        }
        
    }
}
