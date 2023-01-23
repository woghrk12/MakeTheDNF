using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayer : MonoBehaviour
{
    #region Variables

    private Animator anim = null;

    // DNF Transform
    private DNFTransform charTransform = null;
    private Transform posObj = null;
    private Transform yPosObj = null;
    private Transform scaleObj = null;

    // Behaviours
    private MoveBehaviour moveController = null;
    private JumpBehaviour jumpController = null;

    #endregion Variables

    #region Unity Event

    private void Awake()
    {
        // Init Component
        anim = GetComponent<Animator>();
        moveController = GetComponent<MoveBehaviour>();
        jumpController = GetComponent<JumpBehaviour>();

        // Init DNF Transform
        if (posObj == null) posObj = transform;
        if (yPosObj == null) yPosObj = posObj.GetChild(0).transform;
        if (scaleObj == null) scaleObj = yPosObj.GetChild(0).transform;
        charTransform = new DNFTransform(posObj, yPosObj, scaleObj);

        // Init Behaviours 
        moveController.InitBehaviour(anim, -50f, 50f, -10f, 10f);
        jumpController.InitBehaviour(anim);
    }

    private void Update()
    {
        if (jumpController.CanJump && InputManager.Buttons[KeyName.Jump].ButtonState == PlayerKey.EButtonState.DOWN)
            StartCoroutine(jumpController.Jump(charTransform));
    }

    private void FixedUpdate()
    {
        moveController.Move(charTransform, InputManager.Instance.Direction);
    }

    #endregion Unity Event
}
