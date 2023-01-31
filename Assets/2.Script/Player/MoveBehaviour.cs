using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Move = KeyID.Move;

public class MoveBehaviour : MonoBehaviour
{
    #region Variables

    private Animator anim = null;
    private DNFTransform charTransform = null;

    private int hashIsMove = 0;

    private float xMoveSpeed = 4f;
    private float zMoveSpeed = 4f;
    public float xFactor = 1f;
    public float zFactor = 1f;
    private Vector3 inputDir = Vector3.zero;
    private Vector3 moveDir = Vector3.zero;
    private Vector3 position = Vector3.zero;

    private float minX = -50f, maxX = 50f, minZ = -10f, maxZ = 10f;

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
                anim?.SetBool(AnimatorKey.IsMove, false);
            }
        }
    }

    #endregion Properties

    #region Unity Event

    private void Awake()
    {
        anim = GetComponent<Animator>();
        charTransform = GetComponent<DNFTransform>();

        hashIsMove = Animator.StringToHash(AnimatorKey.IsMove);
    }

    private void Update()
    {
        PlayerKey[] t_moveButtons = InputManager.MoveButtons;

        // Set horizontal direction
        if (t_moveButtons[Move.PosHorizontal].ButtonState == PlayerKey.EButtonState.IDLE
            && t_moveButtons[Move.NegHorizontal].ButtonState == PlayerKey.EButtonState.IDLE)
            inputDir.x = 0f;
        else if (t_moveButtons[Move.PosHorizontal].ButtonState == PlayerKey.EButtonState.PRESSED)
        {
            if (t_moveButtons[Move.NegHorizontal].ButtonState == PlayerKey.EButtonState.DOWN) inputDir.x = -1f;
            else if (t_moveButtons[Move.NegHorizontal].ButtonState != PlayerKey.EButtonState.PRESSED) inputDir.x = 1f;
        }
        else if (t_moveButtons[Move.NegHorizontal].ButtonState == PlayerKey.EButtonState.PRESSED)
        {
            if (t_moveButtons[Move.PosHorizontal].ButtonState == PlayerKey.EButtonState.DOWN) inputDir.x = 1f;
            else if (t_moveButtons[Move.PosHorizontal].ButtonState != PlayerKey.EButtonState.PRESSED) inputDir.x = -1f;
        }

        // Set vertical direction
        if (t_moveButtons[Move.PosVertical].ButtonState == PlayerKey.EButtonState.IDLE
            && t_moveButtons[Move.NegVertical].ButtonState == PlayerKey.EButtonState.IDLE)
            inputDir.z = 0;
        else if (t_moveButtons[Move.PosVertical].ButtonState == PlayerKey.EButtonState.PRESSED)
        {
            if (t_moveButtons[Move.NegVertical].ButtonState == PlayerKey.EButtonState.DOWN) inputDir.z = -1f;
            else if (t_moveButtons[Move.NegVertical].ButtonState != PlayerKey.EButtonState.PRESSED) inputDir.z = 1f;
        }
        else if (t_moveButtons[Move.NegVertical].ButtonState == PlayerKey.EButtonState.PRESSED)
        {
            if (t_moveButtons[Move.PosVertical].ButtonState == PlayerKey.EButtonState.DOWN) inputDir.z = 1f;
            else if (t_moveButtons[Move.PosVertical].ButtonState != PlayerKey.EButtonState.PRESSED) inputDir.z = -1f;
        }
    }

    private void FixedUpdate()
    {
        if (!canMove) return;

        // Handle Input
        moveDir = inputDir;
        moveDir.x *= xMoveSpeed * xFactor;
        moveDir.z *= zMoveSpeed * zFactor;

        // Limit Area
        position = charTransform.Position + moveDir * Time.deltaTime;
        if (position.x > maxX) position.x = maxX;
        if (position.x < minX) position.x = minX;
        if (position.z > maxZ) position.z = maxZ;
        if (position.z < minZ) position.z = minZ;
        charTransform.Position = position;

        if (moveDir.x != 0) IsLeft = moveDir.x < 0;
        anim?.SetBool(hashIsMove, moveDir != Vector3.zero);
    }

    #endregion Unity Event

    #region Methods

    public void SetMovableArea(float p_minX, float p_maxX, float p_minZ, float p_maxZ)
    {
        minX = p_minX;
        maxX = p_maxX;
        minZ = p_minZ;
        maxZ = p_maxZ;
    }

    #endregion Methods
}
