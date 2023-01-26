using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    #region Variables

    public SkillStat skillStat;
    private int[] hashSkillMotion;

    private float remainCoolTime = 0f;

    #endregion Variables 

    #region Unity Event

    private void OnValidate()
    {
        if (skillStat == null) return;

        hashSkillMotion = new int[0];

        for (int i = 0; i < skillStat.numCombo; i++)
        {
            int t_hash = Animator.StringToHash(skillStat.skillInfo[i].skillMotion);
            hashSkillMotion = ArrayHelper.Add(t_hash, hashSkillMotion);
        }
    }

    #endregion Unity Event

    #region Methods

    public IEnumerator UseSkill(Animator p_anim, bool p_isLeft, PlayerKey p_key)
    {
        for (int i = 0; i < skillStat.numCombo; i++)
        {
            var t_info = skillStat.skillInfo[i];

            p_anim.SetTrigger(hashSkillMotion[i]);

            foreach (EffectList t_effect in t_info.skillEffects)
                EffectManager.Instance.EffectOneShot((int)t_effect);

            yield return Utilities.WaitForSeconds(t_info.preDelay);
            yield return Utilities.WaitForSeconds(t_info.duration);
            yield return Utilities.WaitForSeconds(t_info.postDelay);
        }
    }

    private IEnumerator ApplyCoolTime()
    {
        remainCoolTime = skillStat.coolTime;

        while (remainCoolTime > 0f)
        {
            yield return Utilities.WaitForSeconds(0.1f);
            remainCoolTime -= 0.1f;
        }
    }

    #endregion Methods
}
