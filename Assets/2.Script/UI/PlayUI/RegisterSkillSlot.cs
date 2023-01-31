using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RegisterSkillSlot : UISlot
{
    #region Variables

    public Skill skill = null;
    private SkillStat skillStat = null;

    private Color registerColor = new Color(1f, 1f, 1f, 1f);
    private Color noAlphaColor = new Color(1f, 1f, 1f, 0f);

    #endregion Variables

    #region Methods

    public void SetSlot(Skill p_skill)
    {
        skill = p_skill;

        skillStat = skill.skillStat;
        iconImage.sprite = skillStat.skillIcon;
        iconImage.color = registerColor;
        id = skillStat.skillID;
    }

    #endregion Methods
}
