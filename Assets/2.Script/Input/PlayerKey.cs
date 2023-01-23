using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKey : MonoBehaviour
{
    public enum EButtonState { IDLE, DOWN, PRESSED, UP }

    #region Variables

    [HideInInspector] public string buttonName = string.Empty;
    
    private EButtonState buttonState = EButtonState.IDLE;

    private bool isPressed = false;
    private bool onPressed = false;

    #endregion Variables

    #region Properties

    public EButtonState ButtonState => buttonState;

    #endregion Properties

    #region Unity Event

    private void Update()
    {
        if (Input.GetButtonDown(buttonName)) isPressed = true;
        if (Input.GetButtonUp(buttonName)) isPressed = false;

        if (isPressed)
        {
            buttonState = onPressed ? EButtonState.PRESSED : EButtonState.DOWN;
            onPressed = true;
        }
        else
        {
            buttonState = onPressed ? EButtonState.UP : EButtonState.IDLE;
            onPressed = false;
        }
    }

    #endregion Unity Event
}
