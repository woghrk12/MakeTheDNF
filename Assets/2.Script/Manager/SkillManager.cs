using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : SingletonMonobehaviour<SkillManager>
{
    #region Variables
    
    private static Dictionary<PlayerKey, Skill> skills = new Dictionary<PlayerKey, Skill>();

    #endregion Variables

    #region Properties

    public static Dictionary<PlayerKey, Skill> Skills => skills;

    #endregion Properties

    #region Methods

    public static void RegisterSkill(PlayerKey p_key, Skill p_newSkill)
    {
        if (skills.ContainsKey(p_key)) skills.Remove(p_key);
        skills.Add(p_key, p_newSkill);
    }

    public static void RemoveSkill(PlayerKey p_key)
    {
        if (skills.ContainsKey(p_key)) skills.Remove(p_key);
    }

    #endregion Methods
}
