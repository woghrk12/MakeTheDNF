using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Skill Tree", menuName = "Skill System/Skill Tree")]
public class SkillTree : ScriptableObject
{
    #region Variables

    public List<Skill> skills = new List<Skill>();

    public Skill skillBaseAttack = null;
    private List<Skill> skillLevel0 = new List<Skill>();
    private List<Skill> skillLevel5 = new List<Skill>();
    private List<Skill> skillLevel10 = new List<Skill>();
    private List<Skill> skillLevel15 = new List<Skill>();
    private List<Skill> skillLevel20 = new List<Skill>();
    private List<Skill> skillLevel25 = new List<Skill>();
    private List<Skill> skillLevel30 = new List<Skill>();

    #endregion Variables

    #region Unity Event
    
    private void OnValidate()
    {
        skillBaseAttack = null;
        skillLevel0.Clear();
        skillLevel5.Clear();
        skillLevel10.Clear();
        skillLevel15.Clear();
        skillLevel20.Clear();
        skillLevel25.Clear();
        skillLevel30.Clear();

        foreach (Skill t_skill in skills)
        {
            switch (t_skill.skillStat.acquireLevel)
            {
                case SkillStat.EAcquireLevel.BASEATTACK:
                    skillBaseAttack = t_skill;
                    break;
                case SkillStat.EAcquireLevel.ZERO:
                    if(!skillLevel0.Contains(t_skill)) skillLevel0.Add(t_skill);
                    break;
                case SkillStat.EAcquireLevel.FIVE:
                    if (!skillLevel5.Contains(t_skill)) skillLevel5.Add(t_skill);
                    break;
                case SkillStat.EAcquireLevel.TEN:
                    if (!skillLevel10.Contains(t_skill)) skillLevel10.Add(t_skill);
                    break;
                case SkillStat.EAcquireLevel.FIFTEEN:
                    if (!skillLevel15.Contains(t_skill)) skillLevel15.Add(t_skill);
                    break;
                case SkillStat.EAcquireLevel.TWENTY:
                    if (!skillLevel20.Contains(t_skill)) skillLevel20.Add(t_skill);
                    break;
                case SkillStat.EAcquireLevel.TWENTYFIVE:
                    if (!skillLevel25.Contains(t_skill)) skillLevel25.Add(t_skill);
                    break;
                case SkillStat.EAcquireLevel.THIRTY:
                    if (!skillLevel30.Contains(t_skill)) skillLevel30.Add(t_skill);
                    break;
            }
        }
    }

    #endregion Unity Event

    #region Methods

    public bool CheckCanLearnSkill(Skill p_skill)
    {
        foreach (Skill t_needSkill in p_skill.skillStat.preLearnedList)
        {
            switch (t_needSkill.skillStat.acquireLevel)
            {
                case SkillStat.EAcquireLevel.ZERO:
                    //if (skillStatsLevel0.Find(x => x.skillID == t_needSkill.skillStat.skillID).skillLevel <= 0) return false;
                    break;
                case SkillStat.EAcquireLevel.FIVE:
                    //if (skillStatsLevel5.Find(x => x.skillID == t_needSkill.skillStat.skillID).skillLevel <= 0) return false;
                    break;
                case SkillStat.EAcquireLevel.TEN:
                    //if (skillStatsLevel10.Find(x => x.skillID == t_needSkill.skillStat.skillID).skillLevel <= 0) return false;
                    break;
                case SkillStat.EAcquireLevel.FIFTEEN:
                    //if (skillStatsLevel15.Find(x => x.skillID == t_needSkill.skillStat.skillID).skillLevel <= 0) return false;
                    break;
                case SkillStat.EAcquireLevel.TWENTY:
                    //if (skillStatsLevel20.Find(x => x.skillID == t_needSkill.skillStat.skillID).skillLevel <= 0) return false;
                    break;
                case SkillStat.EAcquireLevel.TWENTYFIVE:
                    //if (skillStatsLevel25.Find(x => x.skillID == t_needSkill.skillStat.skillID).skillLevel <= 0) return false;
                    break;
                case SkillStat.EAcquireLevel.THIRTY:
                    //if (skillStatsLevel30.Find(x => x.skillID == t_needSkill.skillStat.skillID).skillLevel <= 0) return false;
                    break;
            }
        }

        return true;
    }

    #endregion Methods
}
