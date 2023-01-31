using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    #region Variables

    public SkillStat skillStat;
    private int[] hashSkillMotion;

    public int level = 0;
    private float remainCoolTime = 0f;

    #endregion Variables 

    #region Properties

    public float RemainCoolTime => remainCoolTime;

    #endregion Properties

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

    public IEnumerator UseSkill(DNFTransform p_transform, Animator p_anim, bool p_isLeft, int p_keyID)
    {
        for (int t_comboIdx = 0; t_comboIdx < skillStat.numCombo; t_comboIdx++)
        {
            var t_info = skillStat.skillInfo[t_comboIdx];
            var t_hitbox = ObjectPoolingManager.Instantiate(PoolingObjectName.Hitbox).GetComponent<Hitbox>();
            
            p_anim.SetTrigger(hashSkillMotion[t_comboIdx]);

            yield return Utilities.WaitForSeconds(t_info.preDelay);

            for (int t_effectIdx = 0; t_effectIdx < t_info.numSkillEffect; t_effectIdx++)
            {
                PlayEffect(t_info.skillEffects[t_effectIdx], p_isLeft, p_transform.Position, t_info.effectOffsets[t_effectIdx]);
            }
            
            yield return Utilities.WaitForSeconds(t_info.duration);
            yield return Utilities.WaitForSeconds(t_info.postDelay);
        }
    }

    private GameObject PlayEffect(EffectList p_effect, bool p_isLeft, Vector3 p_position, Vector3 p_offset)
    {
        Vector3 t_effectPos = new Vector3(p_position.x, p_position.y + p_position.z * DNFTransform.convRate, 0f);
        Vector3 t_offset = new Vector3(p_offset.x, p_offset.y + p_offset.z * DNFTransform.convRate, 0f);
        Vector3 t_localScale = new Vector3(1f, 1f, 1f);

        if (p_isLeft)
        {
            t_offset.x *= -1f;
            t_localScale.x *= -1f;
        }

        GameObject t_effect = DataManager.EffectData.GetClip((int)p_effect).Instantiate(t_effectPos + t_offset);
        t_effect.transform.localScale = t_localScale;

        return t_effect;
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
