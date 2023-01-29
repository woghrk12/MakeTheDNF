using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SkillSlot : UISlot
{
    #region Variables
    
    [SerializeField] private Skill skill = null;
    private SkillStat skillStat = null;

    #endregion Variables

    #region Constructor

    public SkillSlot() { }
    public SkillSlot(Skill p_skill) 
    {
        skill = p_skill;

        skillStat = skill.skillStat;
        iconImage.sprite = skillStat.skillIcon;
        id = skillStat.skillID;
        value = skill.level;
    }

    #endregion Constructor

    #region Methods

    public bool IncreaseLevel(ref int p_skillPoint, bool p_isAll = false)
    {
        if (skill == null) return false;
        if (skill.level >= skillStat.maxLevel) return false;
        if (skillStat.needPoint > p_skillPoint) return false;

        if (skillStat.needPoint == 0)
        {
            skill.level = p_isAll ? skillStat.maxLevel : skill.level + 1;
        }
        else if (p_isAll)
        {
            int t_upLevel = Mathf.Max(p_skillPoint / skillStat.needPoint, skillStat.maxLevel - skill.level);
            p_skillPoint -= t_upLevel * skillStat.needPoint;
            skill.level += t_upLevel;
        }
        else
        {
            p_skillPoint -= skillStat.needPoint;
            skill.level++;
        }

        return true;
    }

    public bool DecreaseLevel(ref int p_skillPoint, bool p_isAll = false)
    {
        if (skill == null) return false;
        if (skill.level <= 0) return false;

        int t_downLevel = p_isAll ? skill.level : 1;
        p_skillPoint += t_downLevel * skillStat.needPoint;
        skill.level -= t_downLevel;

        return true;
    }

    #endregion Methods

    #region Override Methods

    public override void OnEndDrag(PointerEventData p_data)
    {
        if (MouseData.slotMouseIsOver.TryGetComponent(out RegisterSkillSlot t_slot))
            t_slot.SetSlot(skill);

        base.OnEndDrag(p_data);
    }

    #endregion Override Methods
}
