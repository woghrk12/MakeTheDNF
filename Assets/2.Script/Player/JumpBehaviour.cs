using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBehaviour : MonoBehaviour
{
    #region Variables

    private Animator anim = null;

    private float gravity = 9.8f;
    private float invGravity = 1 / 9.8f;
    private float jumpPower = 8f;

    private float jumpTime = 0f;
    private float criteria = 0f;
    private float originY = 0f;

    private bool canJump = true;

    private int hashIsJump = 0;
    private int hashJumpStay = 0;
    private int hashJumpDown = 0;

    #endregion Variables

    #region Properties

    private float Height { get => jumpTime * jumpTime * (-gravity) * 0.5f + jumpTime * jumpPower; }

    private float MaxHeight { get => 0.5f * jumpTime * jumpTime * invGravity; }

    public bool CanJump { get => canJump; }

    #endregion Properties

    #region Methods

    public void InitBehaviour(Animator p_anim)
    {
        anim = p_anim;

        hashIsJump = Animator.StringToHash(AnimatorKey.IsJump);
        hashJumpStay = Animator.StringToHash(AnimatorKey.JumpStay);
        hashJumpDown = Animator.StringToHash(AnimatorKey.JumpDown);
    }

    public IEnumerator Jump(DNFTransform p_transform)
    {
        canJump = false;
        jumpTime = 0f;
        criteria = 0.8f * MaxHeight + originY;

        anim.SetBool(hashIsJump, true);
        yield return JumpUp(p_transform, criteria);
        yield return JumpStay(p_transform, criteria);
        yield return JumpDown(p_transform);
        anim.SetBool(hashIsJump, false);

        jumpTime = 0f;
        p_transform.Y = originY;
        canJump = true;
    }

    private IEnumerator JumpUp(DNFTransform p_transform, float p_criteria)
    {
        while (p_transform.Y <= p_criteria)
        {
            p_transform.Y = originY + Height;
            jumpTime += Time.fixedDeltaTime;
            yield return null;
        }
    }

    private IEnumerator JumpStay(DNFTransform p_transform, float p_criteria)
    {
        anim.SetTrigger(hashJumpStay);

        while (p_transform.Y >= p_criteria)
        {
            p_transform.Y = originY + Height;
            jumpTime += Time.fixedDeltaTime;
            yield return null;
        }
    }

    private IEnumerator JumpDown(DNFTransform p_transform)
    {
        anim.SetTrigger(hashJumpDown);

        while (p_transform.Y >= originY)
        {
            p_transform.Y = originY + Height;
            jumpTime += Time.deltaTime;
            yield return null;
        }
    }

    #endregion Methods
}
