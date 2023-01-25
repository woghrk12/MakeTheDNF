using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : SingletonMonobehaviour<SkillManager>
{
    #region Variables
    
    public Skill baseAttack;
    private static Dictionary<KeyCode, Skill> skills = new Dictionary<KeyCode, Skill>();

    #endregion Variables

    #region Properties

    public static Dictionary<KeyCode, Skill> Skills => skills;

    #endregion Properties

    #region Methods

    public static void RegisterSkill(KeyCode p_key, Skill p_newSkill)
    {
        if (skills.ContainsKey(p_key)) skills.Remove(p_key);

        skills.Add(p_key, p_newSkill);
    }

    public static void RemoveSkill(KeyCode p_key)
    {
        if (skills.ContainsKey(p_key)) skills.Remove(p_key);
    }

    #endregion Methods
}
