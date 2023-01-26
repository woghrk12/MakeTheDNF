using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayer : MonoBehaviour
{
    #region Variables
    
    private Animator anim = null;

    // Hitbox Info
    private DNFTransform charTransform = null;
    private Hitbox hitbox = null;

    // Behaviours
    private MoveBehaviour moveController = null;
    private JumpBehaviour jumpController = null;
    private AttackBehaviour attackController = null;

    #endregion Variables

    #region Unity Event

    private void Awake()
    {
        // Init Component
        anim = GetComponent<Animator>();
        moveController = GetComponent<MoveBehaviour>();
        jumpController = GetComponent<JumpBehaviour>();
        attackController = GetComponent<AttackBehaviour>();
        charTransform = GetComponent<DNFTransform>();
        hitbox = GetComponent<Hitbox>();

        // Init Hitbox
        hitbox.charTransform = charTransform;

        // Init Behaviours 
        moveController.InitBehaviour(anim, -50f, 50f, -10f, 10f);
        jumpController.InitBehaviour(anim);
        attackController.InitBehaviour(anim);
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
