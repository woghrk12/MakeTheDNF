using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : SingletonMonobehaviour<InputManager>
{
    #region Variables

    private Vector3 inputDir = Vector3.zero;

    private static Dictionary<KeyCode, PlayerKey> buttons = new Dictionary<KeyCode, PlayerKey>();

    private KeyCode posHorizontal = KeyName.PosHorizontal;
    private KeyCode negHorizontal = KeyName.NegHorizontal;
    private KeyCode posVertical = KeyName.PosVertical;
    private KeyCode negVertical = KeyName.NegVertical;
    private KeyCode xButton = KeyName.Attack;
    private KeyCode jButton = KeyName.Jump;
    private KeyCode skillSlot1 = KeyName.SkillSlot1;
    private KeyCode skillSlot2 = KeyName.SkillSlot2;
    private KeyCode skillSlot3 = KeyName.SkillSlot3;
    private KeyCode skillSlot4 = KeyName.SkillSlot4;
    private KeyCode skillSlot5 = KeyName.SkillSlot5;
    private KeyCode skillSlot6 = KeyName.SkillSlot6;

    #endregion Variables

    #region Properties

    public Vector3 Direction => inputDir;

    public static Dictionary<KeyCode, PlayerKey> Buttons { get => buttons; }

    #endregion Properties

    #region Unity Event

    protected override void Awake()
    {
        base.Awake();

        buttons.Add(posHorizontal, new PlayerKey());
        buttons.Add(negHorizontal, new PlayerKey());
        buttons.Add(posVertical, new PlayerKey());
        buttons.Add(negVertical, new PlayerKey());
        buttons.Add(xButton, new PlayerKey());
        buttons.Add(jButton, new PlayerKey());
        buttons.Add(skillSlot1, new PlayerKey());
        buttons.Add(skillSlot2, new PlayerKey());
        buttons.Add(skillSlot3, new PlayerKey());
        buttons.Add(skillSlot4, new PlayerKey());
        buttons.Add(skillSlot5, new PlayerKey());
        buttons.Add(skillSlot6, new PlayerKey());
    }

    private void Update()
    {
        // Set horizontal direction
        if (buttons[posHorizontal].ButtonState == PlayerKey.EButtonState.IDLE && buttons[negHorizontal].ButtonState == PlayerKey.EButtonState.IDLE)
            inputDir.x = 0f;
        else if (buttons[posHorizontal].ButtonState == PlayerKey.EButtonState.PRESSED)
        {
            if (buttons[negHorizontal].ButtonState == PlayerKey.EButtonState.DOWN) inputDir.x = -1f;
            else if (buttons[negHorizontal].ButtonState != PlayerKey.EButtonState.PRESSED) inputDir.x = 1f;
        }
        else if (buttons[negHorizontal].ButtonState == PlayerKey.EButtonState.PRESSED)
        {
            if (buttons[posHorizontal].ButtonState == PlayerKey.EButtonState.DOWN) inputDir.x = 1f;
            else if (buttons[posHorizontal].ButtonState != PlayerKey.EButtonState.PRESSED) inputDir.x = -1f;
        }

        // Set vertical direction
        if (buttons[posVertical].ButtonState == PlayerKey.EButtonState.IDLE && buttons[negVertical].ButtonState == PlayerKey.EButtonState.IDLE)
            inputDir.z = 0;
        else if (buttons[posVertical].ButtonState == PlayerKey.EButtonState.PRESSED)
        {
            if (buttons[negVertical].ButtonState == PlayerKey.EButtonState.DOWN) inputDir.z = -1f;
            else if (buttons[negVertical].ButtonState != PlayerKey.EButtonState.PRESSED) inputDir.z = 1f;
        }
        else if (buttons[negVertical].ButtonState == PlayerKey.EButtonState.PRESSED)
        {
            if (buttons[posVertical].ButtonState == PlayerKey.EButtonState.DOWN) inputDir.z = 1f;
            else if (buttons[posVertical].ButtonState != PlayerKey.EButtonState.PRESSED) inputDir.z = -1f;
        }

        // Check pressed key
        foreach (var t_button in buttons)
            t_button.Value.SetButtonState(t_button.Key);
    }

    #endregion Unity Event

    #region Methods

    public void ChangeKeyCode(KeyCode p_oldKey, KeyCode p_newKey)
    {
        PlayerKey t_playerKey = buttons[p_oldKey];
        buttons.Remove(p_oldKey);
        buttons.Add(p_newKey, t_playerKey);
    }

    #endregion Methods
}
