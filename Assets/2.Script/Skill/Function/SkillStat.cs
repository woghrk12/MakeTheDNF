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
        public Vector3[] effectOffsets = new Vector3[0];
        public Vector3[] skillRanges = new Vector3[0];
        public Vector3[] rangeOffsets = new Vector3[0];
        public float preDelay = 0f;
        public float duration = 0f;
        public float postDelay = 0f;
    }

    #region Variables

    [Header("Identity")]
    public int skillID = 0;
    public Sprite skillIcon;
    public ESkillType skillType = ESkillType.NONE;
    public float coolTime = 0f;
    public int needMana = 0;

    [Header("Combo")]
    public int numCombo = 0;
    public SkillInfo[] skillInfo = null;    

    [Header("Acquire Skill")]
    // minimum level value for acquiring the skill
    public EAcquireLevel acquireLevel = 0;
    // minimum and maximum level value
    public int minLevel = 0, maxLevel = 0;
    // interval level value for raising skill level
    public int stepLevel = 0;
    // need point value for raising skill level
    public int needPoint = 0;

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
