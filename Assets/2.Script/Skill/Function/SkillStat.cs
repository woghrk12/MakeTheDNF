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

    #region Variables

    [Header("Skill Info")]
    public ESkillType skillType = ESkillType.NONE;
    public int numCombo = 0;
    public string[] skillMotions = null;
    public string[] skillEffects = null;
    public Vector3[] skillRanges = null;
    public float[] preDelays = null;
    public float[] durations = null;
    public float[] postDelays = null;
    public float coolTime = 0f;
    [HideInInspector] public float remainCoolTime = 0f;

    [Header("Acquire Skill")]
    // minimum level value for acquiring the skill
    public int acquireLevel = 0;
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

    public List<Skill> canCancelList = new List<Skill>();

    #endregion Variables
}
