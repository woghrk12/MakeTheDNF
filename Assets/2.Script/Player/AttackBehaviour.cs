using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBehaviour : MonoBehaviour
{
    #region Variables

    private Animator anim = null;
    private GamePlayer player = null;
    private DNFTransform charTransform = null;

    private int hashIsAttack = 0;

    private bool canAttack = true;

    [SerializeField] private SkillTree skillTree = null;
    private Skill baseAttack = null;
    private Skill[] skillRegistered = new Skill[6];

    private Skill curSkill = null;
    private Coroutine curRunningSkill = null;

    #endregion Variables

    #region Unity Events

    private void Awake()
    {
        anim = GetComponent<Animator>();
        player = GetComponent<GamePlayer>();
        charTransform = GetComponent<DNFTransform>();

        hashIsAttack = Animator.StringToHash(AnimatorKey.IsAttack);

        baseAttack = skillTree.skillBaseAttack;
    }

    private void Update()
    {
        if (!canAttack) return;

        if (InputManager.AttackButton.ButtonState == PlayerKey.EButtonState.DOWN)
            curRunningSkill = StartCoroutine(Attack(baseAttack, player.IsLeft, InputManager.AttackButton.key));
    }

    #endregion Unity Events

    #region Methods

    private IEnumerator Attack(Skill p_skill, bool p_isLeft, KeyCode p_key)
    {
        if (!CheckCanUse(p_skill)) yield break;

        anim?.SetBool(hashIsAttack, true);
        yield return p_skill.UseSkill(charTransform, anim, p_isLeft, p_key);
        anim?.SetBool(hashIsAttack, false);

        curSkill = null;
    }

    private bool CheckCanUse(Skill p_skill)
    {
        // Check Mana

        if (p_skill.skillStat.isNoMotion) return true;
        if (curSkill == null) return true;
        if (p_skill.skillStat.canCancelList.Contains(p_skill)) return true;

        return false;
    }

    public void StopAttack()
    {
        anim?.SetBool(hashIsAttack, false);

        StopCoroutine(curRunningSkill);
        curSkill = null;
    }

    #endregion Methods
}
