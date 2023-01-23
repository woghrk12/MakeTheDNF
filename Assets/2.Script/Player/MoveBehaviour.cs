using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBehaviour : MonoBehaviour
{
    #region Variables

    private Animator anim = null;
    private int hashIsMove = 0;

    private float xMoveSpeed = 4f;
    private float zMoveSpeed = 4f;
    private Vector3 moveDir = Vector3.zero;
    private Vector3 position = Vector3.zero;

    private float minX, maxX, minZ, maxZ;

    private bool isLeft = false;
    private bool canMove = true;

    #endregion Variables

    #region Properties

    public bool IsLeft
    {
        set
        {
            isLeft = value;
            transform.localScale = new Vector3(isLeft ? -1f : 1f, 1f, 1f);
        }
        get => isLeft;
    }

    public bool CanMove
    {
        set
        {
            canMove = value;
            if (!canMove)
            {
                moveDir = Vector3.zero;
                anim.SetBool(AnimatorKey.IsMove, false);
            }
        }
        get => canMove;
    }

    #endregion Properties

    #region Methods

    public void InitBehaviour(Animator p_anim, float p_minX, float p_maxX, float p_minZ, float p_maxZ, bool p_canMove = true)
    {
        anim = p_anim;
        hashIsMove = Animator.StringToHash(AnimatorKey.IsMove);
        minX = p_minX;
        maxX = p_maxX;
        minZ = p_minZ;
        maxZ = p_maxZ;
        CanMove = p_canMove;
    }

    public void Move(DNFTransform p_transform, Vector3 p_moveDir, float p_xFactor = 1f, float p_zFactor = 1f)
    {
        // Handle Input
        moveDir = p_moveDir;
        moveDir.x *= xMoveSpeed * p_xFactor;
        moveDir.z *= zMoveSpeed * p_zFactor;

        // Limit Area
        position = p_transform.Position + moveDir * Time.fixedDeltaTime;
        if (position.x > maxX) position.x = maxX;
        if (position.x < minX) position.x = minX;
        if (position.z > maxZ) position.z = maxZ;
        if (position.z < minZ) position.z = minZ;   
        p_transform.Position = position;
        
        if (moveDir.x != 0) IsLeft = moveDir.x < 0;
        if (anim != null) anim.SetBool(hashIsMove, moveDir != Vector3.zero);
    }

    #endregion Methods
}
