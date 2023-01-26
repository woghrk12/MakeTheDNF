using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Skill Tree", menuName = "Skill System/Skill Tree")]
public class SkillTree : ScriptableObject
{
    #region Variables

    public List<SkillStat> skillStats = new List<SkillStat>();

    private SkillStat skillStatBaseAttack = null;
    private List<SkillStat> skillStatsLevel0 = new List<SkillStat>();
    private List<SkillStat> skillStatsLevel5 = new List<SkillStat>();
    private List<SkillStat> skillStatsLevel10 = new List<SkillStat>();
    private List<SkillStat> skillStatsLevel15 = new List<SkillStat>();
    private List<SkillStat> skillStatsLevel20 = new List<SkillStat>();
    private List<SkillStat> skillStatsLevel25 = new List<SkillStat>();
    private List<SkillStat> skillStatsLevel30 = new List<SkillStat>();

    #endregion Variables

    #region Unity Event
    
    private void OnValidate()
    {
        skillStatBaseAttack = null;
        skillStatsLevel0.Clear();
        skillStatsLevel5.Clear();
        skillStatsLevel10.Clear();
        skillStatsLevel15.Clear();
        skillStatsLevel20.Clear();
        skillStatsLevel25.Clear();
        skillStatsLevel30.Clear();

        foreach (SkillStat t_skill in skillStats)
        {
            switch (t_skill.acquireLevel)
            {
                case SkillStat.EAcquireLevel.BASEATTACK:
                    skillStatBaseAttack = t_skill;
                    break;
                case SkillStat.EAcquireLevel.ZERO:
                    if(!skillStatsLevel0.Contains(t_skill)) skillStatsLevel0.Add(t_skill);
                    break;
                case SkillStat.EAcquireLevel.FIVE:
                    if (!skillStatsLevel5.Contains(t_skill)) skillStatsLevel5.Add(t_skill);
                    break;
                case SkillStat.EAcquireLevel.TEN:
                    if (!skillStatsLevel10.Contains(t_skill)) skillStatsLevel10.Add(t_skill);
                    break;
                case SkillStat.EAcquireLevel.FIFTEEN:
                    if (!skillStatsLevel15.Contains(t_skill)) skillStatsLevel15.Add(t_skill);
                    break;
                case SkillStat.EAcquireLevel.TWENTY:
                    if (!skillStatsLevel20.Contains(t_skill)) skillStatsLevel20.Add(t_skill);
                    break;
                case SkillStat.EAcquireLevel.TWENTYFIVE:
                    if (!skillStatsLevel25.Contains(t_skill)) skillStatsLevel25.Add(t_skill);
                    break;
                case SkillStat.EAcquireLevel.THIRTY:
                    if (!skillStatsLevel30.Contains(t_skill)) skillStatsLevel30.Add(t_skill);
                    break;
            }
        }
    }

    #endregion Unity Event

    #region Methods

    public bool CheckCanLearnSkill(SkillStat p_skillStat)
    {
        foreach (Skill t_needSkill in p_skillStat.preLearnedList)
        {
            switch (t_needSkill.skillStat.acquireLevel)
            {
                case SkillStat.EAcquireLevel.ZERO:
                    if (skillStatsLevel0.Find(x => x.skillID == t_needSkill.skillStat.skillID).skillLevel <= 0) return false;
                    break;
                case SkillStat.EAcquireLevel.FIVE:
                    if (skillStatsLevel5.Find(x => x.skillID == t_needSkill.skillStat.skillID).skillLevel <= 0) return false;
                    break;
                case SkillStat.EAcquireLevel.TEN:
                    if (skillStatsLevel10.Find(x => x.skillID == t_needSkill.skillStat.skillID).skillLevel <= 0) return false;
                    break;
                case SkillStat.EAcquireLevel.FIFTEEN:
                    if (skillStatsLevel15.Find(x => x.skillID == t_needSkill.skillStat.skillID).skillLevel <= 0) return false;
                    break;
                case SkillStat.EAcquireLevel.TWENTY:
                    if (skillStatsLevel20.Find(x => x.skillID == t_needSkill.skillStat.skillID).skillLevel <= 0) return false;
                    break;
                case SkillStat.EAcquireLevel.TWENTYFIVE:
                    if (skillStatsLevel25.Find(x => x.skillID == t_needSkill.skillStat.skillID).skillLevel <= 0) return false;
                    break;
                case SkillStat.EAcquireLevel.THIRTY:
                    if (skillStatsLevel30.Find(x => x.skillID == t_needSkill.skillStat.skillID).skillLevel <= 0) return false;
                    break;
            }
        }

        return true;
    }

    #endregion Methods
}
