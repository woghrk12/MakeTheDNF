using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerKey
{
    public enum EButtonState { IDLE, DOWN, PRESSED, UP }

    #region Variables

    public KeyCode key = KeyCode.None;
    private EButtonState buttonState = EButtonState.IDLE;

    private bool isPressed = false;
    private bool onPressed = false;

    #endregion Variables

    #region Properties

    public EButtonState ButtonState => buttonState;

    #endregion Properties

    #region Constructor

    public PlayerKey(KeyCode p_key) { key = p_key; }

    #endregion Constructor

    #region Methods

    public void SetButtonState()
    {
        if (key == KeyCode.None)
        {
            buttonState = EButtonState.IDLE;
            isPressed = false;
            onPressed = false;
            return;
        }

        if (Input.GetKeyDown(key)) isPressed = true;
        if (Input.GetKeyUp(key)) isPressed = false;

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
