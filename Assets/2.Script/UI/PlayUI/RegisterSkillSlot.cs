using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RegisterSkillSlot : UISlot
{
    #region Variables

    public Skill skill = null;
    private SkillStat skillStat = null;
    [SerializeField] private KeyCode key = KeyCode.None;

    #endregion Variables

    #region Methods

    public void SetSlot(Skill p_skill)
    {
        skill = p_skill;

        skillStat = skill.skillStat;
        iconImage.sprite = skillStat.skillIcon;
        id = skillStat.skillID;
    }

    #endregion Methods
}
