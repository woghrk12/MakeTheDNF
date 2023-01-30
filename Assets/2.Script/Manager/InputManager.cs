using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : SingletonMonobehaviour<InputManager>
{
    #region Variables

    private Vector3 inputDir = Vector3.zero;

    private static PlayerKey[] buttons = new PlayerKey[KeyID.End];
    private static Dictionary<KeyCode, int> registerKeys = new Dictionary<KeyCode, int>();
    [SerializeField] private KeyCode[] defaultKey = new KeyCode[KeyID.End];
    
    #endregion Variables

    #region Properties

    public Vector3 Direction => inputDir;

    public static PlayerKey[] Buttons { get => buttons; }

    #endregion Properties

    #region Unity Event

    protected override void Awake()
    {
        base.Awake();

        for (int i = 0; i < KeyID.End; i++)
            buttons[i] = new PlayerKey(PlayerPrefs.HasKey(i.ToString()) ? (KeyCode)PlayerPrefs.GetInt(i.ToString()) : defaultKey[i]);
    }

    private void Update()
    {
        // Check button state
        foreach (var t_button in buttons)
            t_button.SetButtonState();

        // Set horizontal direction
        if (buttons[KeyID.PosHorizontal].ButtonState == PlayerKey.EButtonState.IDLE 
            && buttons[KeyID.NegHorizontal].ButtonState == PlayerKey.EButtonState.IDLE)
            inputDir.x = 0f;
        else if (buttons[KeyID.PosHorizontal].ButtonState == PlayerKey.EButtonState.PRESSED)
        {
            if (buttons[KeyID.NegHorizontal].ButtonState == PlayerKey.EButtonState.DOWN) inputDir.x = -1f;
            else if (buttons[KeyID.NegHorizontal].ButtonState != PlayerKey.EButtonState.PRESSED) inputDir.x = 1f;
        }
        else if (buttons[KeyID.NegHorizontal].ButtonState == PlayerKey.EButtonState.PRESSED)
        {
            if (buttons[KeyID.PosHorizontal].ButtonState == PlayerKey.EButtonState.DOWN) inputDir.x = 1f;
            else if (buttons[KeyID.PosHorizontal].ButtonState != PlayerKey.EButtonState.PRESSED) inputDir.x = -1f;
        }

        // Set vertical direction
        if (buttons[KeyID.PosVertical].ButtonState == PlayerKey.EButtonState.IDLE 
            && buttons[KeyID.NegVertical].ButtonState == PlayerKey.EButtonState.IDLE)
            inputDir.z = 0;
        else if (buttons[KeyID.PosVertical].ButtonState == PlayerKey.EButtonState.PRESSED)
        {
            if (buttons[KeyID.NegVertical].ButtonState == PlayerKey.EButtonState.DOWN) inputDir.z = -1f;
            else if (buttons[KeyID.NegVertical].ButtonState != PlayerKey.EButtonState.PRESSED) inputDir.z = 1f;
        }
        else if (buttons[KeyID.NegVertical].ButtonState == PlayerKey.EButtonState.PRESSED)
        {
            if (buttons[KeyID.PosVertical].ButtonState == PlayerKey.EButtonState.DOWN) inputDir.z = 1f;
            else if (buttons[KeyID.PosVertical].ButtonState != PlayerKey.EButtonState.PRESSED) inputDir.z = -1f;
        }
    }

    #endregion Unity Event

    #region Methods

    public void UpdateKeyCode(int p_button, KeyCode p_key)
    {
        if (registerKeys.TryGetValue(p_key, out int t_button))
        {
            registerKeys.Remove(p_key);
            buttons[t_button].key = KeyCode.None;
        }

        buttons[p_button].key = p_key;
        registerKeys.Add(p_key, p_button);
        PlayerPrefs.SetInt(p_button.ToString(), (int)p_key);
    }

    public void RemoveKeyCode(int p_button)
    {
        if (!registerKeys.ContainsKey(buttons[p_button].key)) return;

        registerKeys.Remove(buttons[p_button].key);
        buttons[p_button].key = KeyCode.None;
    }

    #endregion Methods
}
