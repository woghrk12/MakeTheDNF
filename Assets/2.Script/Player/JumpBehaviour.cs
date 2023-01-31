using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBehaviour : MonoBehaviour
{
    #region Variables

    private Animator anim = null;
    private DNFTransform charTransform = null;

    private float gravity = 12f;
    public float gravityFactor = 1f;
    private float jumpPower = 8f;
    public float jumpFactor = 1f;

    private float jumpTime = 0f;
    private float criteria = 0f;
    private float originY = 0f;

    private bool canJump = true;

    private int hashIsJump = 0;
    private int hashJumpStay = 0;
    private int hashJumpDown = 0;

    #endregion Variables

    #region Properties

    private float Height { get => jumpTime * jumpTime * (-(gravity * gravityFactor)) * 0.5f + jumpTime * jumpPower * jumpFactor; }

    private float MaxHeight { get => 0.5f * (jumpPower * jumpFactor) * (jumpPower * jumpFactor) * (1 / (gravity * gravityFactor)); }

    #endregion Properties

    #region Unity Events

    private void Awake()
    {
        anim = GetComponent<Animator>();
        charTransform = GetComponent<DNFTransform>();

        hashIsJump = Animator.StringToHash(AnimatorKey.IsJump);
        hashJumpStay = Animator.StringToHash(AnimatorKey.JumpStay);
        hashJumpDown = Animator.StringToHash(AnimatorKey.JumpDown);
    }

    private void Update()
    {
        if (canJump && InputManager.JumpButton.ButtonState == PlayerKey.EButtonState.DOWN)
            StartCoroutine(Jump());
    }

    #endregion Unity Events

    #region Methods

    private IEnumerator Jump()
    {
        canJump = false;
        jumpTime = 0f;
        criteria = 0.8f * MaxHeight + originY;

        anim.SetBool(hashIsJump, true);
        yield return JumpUp(charTransform, criteria);
        yield return JumpStay(charTransform, criteria);
        yield return JumpDown(charTransform);
        anim.SetBool(hashIsJump, false);

        jumpTime = 0f;
        charTransform.Y = originY;
        canJump = true;
    }

    private IEnumerator JumpUp(DNFTransform p_transform, float p_criteria)
    {
        while (p_transform.Y <= p_criteria)
        {
            p_transform.Y = originY + Height;
            jumpTime += Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator JumpStay(DNFTransform p_transform, float p_criteria)
    {
        anim.SetTrigger(hashJumpStay);

        while (p_transform.Y >= p_criteria)
        {
            p_transform.Y = originY + Height;
            jumpTime += Time.deltaTime;
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
