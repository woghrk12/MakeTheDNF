using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    #region Variables

    public SkillStat skillStat;
    public Hitbox[] hitboxes;
    public int[] hashSkillMotion;

    private float remainCoolTime = 0f;

    #endregion Variables 

    #region Unity Event

    private void Awake()
    {
        for (int i = 0; i < skillStat.numCombo; i++)
        {
            Hitbox t_hitbox = new GameObject(typeof(Hitbox).Name, typeof(Hitbox)).GetComponent<Hitbox>();
            t_hitbox.sizeX = skillStat.skillRanges[i].x;
            t_hitbox.sizeY = skillStat.skillRanges[i].y;
            t_hitbox.sizeZ = skillStat.skillRanges[i].z;
            hitboxes = ArrayHelper.Add(t_hitbox, hitboxes);

            int t_hash = Animator.StringToHash(skillStat.skillMotions[i]);
            hashSkillMotion = ArrayHelper.Add(t_hash, hashSkillMotion);
        }
    }

    #endregion Unity Event

    #region Methods

    public IEnumerator UseSkill(Animator p_anim, bool p_isLeft, PlayerKey p_key)
    {
        for (int i = 0; i < skillStat.numCombo; i++)
        {
            p_anim.SetTrigger(hashSkillMotion[i]);
            EffectManager.Instance.EffectOneShot((int)skillStat.skillEffects[i]);
            yield return Utilities.WaitForSeconds(skillStat.preDelays[i]);
            yield return Utilities.WaitForSeconds(skillStat.durations[i]);
            yield return Utilities.WaitForSeconds(skillStat.postDelays[i]);
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
