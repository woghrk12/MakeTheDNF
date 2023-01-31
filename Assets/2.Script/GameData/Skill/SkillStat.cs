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
        [HideInInspector] public int numSkillEffect = 0;
        public EffectList[] skillEffects = new EffectList[0];
        public Vector3[] effectOffsets = new Vector3[0];
        public Vector3 skillRange = Vector3.zero;
        public Vector3 rangeOffset = Vector3.zero;
        public float preDelay = 0f;
        public float duration = 0f;
        public float postDelay = 0f;
    }

    #region Variables

    [Header("Identity")]
    public int skillID = 0;
    public Sprite skillIcon;

    [Header("Skill Info")]
    public EClassType classType = EClassType.NONE;
    public ESkillType skillType = ESkillType.NONE;
    public float coolTime = 0f;
    public int needMana = 0;
    public bool isNoMotion = false;
    [HideInInspector] public int numCombo = 0;
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
    }
}
