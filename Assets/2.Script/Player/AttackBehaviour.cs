using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBehaviour : MonoBehaviour
{
    #region Variables

    private Animator anim = null;
    private int hashIsAttack = 0;

    #endregion Variables

    #region Methods

    public void InitBehaviour(Animator p_anim)
    {
        anim = p_anim;
        hashIsAttack = Animator.StringToHash(AnimatorKey.IsAttack);
    }

    public IEnumerator Attack(Skill p_skill, bool p_isLeft, PlayerKey p_key)
    {
        anim.SetBool(hashIsAttack, true);
        yield return p_skill.UseSkill(anim, p_isLeft, p_key);
        anim.SetBool(hashIsAttack, false);
    }

    public void StopAttack()
    {
        anim.SetBool(hashIsAttack, false);
    }

    #endregion Methods
}
