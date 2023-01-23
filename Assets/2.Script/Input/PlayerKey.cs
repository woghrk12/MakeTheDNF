using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKey
{
    public enum EButtonState { IDLE, DOWN, PRESSED, UP }

    #region Variables

    private EButtonState buttonState = EButtonState.IDLE;

    private bool isPressed = false;
    private bool onPressed = false;

    #endregion Variables

    #region Properties

    public EButtonState ButtonState => buttonState;

    #endregion Properties

    #region Methods

    public void SetButtonState(KeyCode p_key)
    {
        if (Input.GetKeyDown(p_key)) isPressed = true;
        if (Input.GetKeyUp(p_key)) isPressed = false;

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

    #endregion Methods
}
