using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Move = KeyID.Move;
using SkillSlot = KeyID.SkillSlot;
using UI = KeyID.UI;

public class InputManager : SingletonMonobehaviour<InputManager>
{
    #region Variables

    private Vector3 inputDir = Vector3.zero;
    private PlayerKeySetting keySetting = null;

    private static PlayerKey[] moveButtons;
    private static PlayerKey attackButton;
    private static PlayerKey jumpButton;
    private static PlayerKey[] skillSlotButtons;
    private static PlayerKey[] uiButtons;

    [SerializeField] private KeyCode[] defaultMoveKey;
    [SerializeField] private KeyCode defaultAttackKey;
    [SerializeField] private KeyCode defaultJumpKey;
    [SerializeField] private KeyCode[] defaultSkillSlotKeys;
    [SerializeField] private KeyCode[] defaultUIKeys;

    #endregion Variables

    #region Properties

    public Vector3 Direction => inputDir;

    public static PlayerKey[] MoveButtons => moveButtons;
    public static PlayerKey AttackButton => attackButton;
    public static PlayerKey JumpButton => jumpButton;
    public static PlayerKey[] SkillSlotButtons => skillSlotButtons;
    public static PlayerKey[] UIButtons => uiButtons;

    #endregion Properties

    #region Unity Event

    protected override void Awake()
    {
        base.Awake();

        keySetting = DataManager.InputData.LoadData();

        if (keySetting == null)
        {
            keySetting = new PlayerKeySetting();
            InitButtons();
            DataManager.InputData.SaveData(keySetting);
        }

        moveButtons = new PlayerKey[4];
        for (int i = 0; i < moveButtons.Length; i++) moveButtons[i] = new PlayerKey(keySetting.moveButtons[i]);
        attackButton = new PlayerKey(keySetting.attackButton);
        jumpButton = new PlayerKey(keySetting.jumpButton);
        skillSlotButtons = new PlayerKey[12];
        for (int i = 0; i < skillSlotButtons.Length; i++) skillSlotButtons[i] = new PlayerKey(keySetting.skillSlotButtons[i]);
        uiButtons = new PlayerKey[3];
        for (int i = 0; i < uiButtons.Length; i++) uiButtons[i] = new PlayerKey(keySetting.uiButtons[i]);
    }

    private void Update()
    {
        // Check button state
        foreach (PlayerKey t_button in moveButtons) t_button.SetButtonState();
        attackButton.SetButtonState();
        jumpButton.SetButtonState();
        foreach (PlayerKey t_button in skillSlotButtons) t_button.SetButtonState();
        foreach (PlayerKey t_button in uiButtons) t_button.SetButtonState();

        // Set horizontal direction
        if (moveButtons[Move.PosHorizontal].ButtonState == PlayerKey.EButtonState.IDLE 
            && moveButtons[Move.NegHorizontal].ButtonState == PlayerKey.EButtonState.IDLE)
            inputDir.x = 0f;
        else if (moveButtons[Move.PosHorizontal].ButtonState == PlayerKey.EButtonState.PRESSED)
        {
            if (moveButtons[Move.NegHorizontal].ButtonState == PlayerKey.EButtonState.DOWN) inputDir.x = -1f;
            else if (moveButtons[Move.NegHorizontal].ButtonState != PlayerKey.EButtonState.PRESSED) inputDir.x = 1f;
        }
        else if (moveButtons[Move.NegHorizontal].ButtonState == PlayerKey.EButtonState.PRESSED)
        {
            if (moveButtons[Move.PosHorizontal].ButtonState == PlayerKey.EButtonState.DOWN) inputDir.x = 1f;
            else if (moveButtons[Move.PosHorizontal].ButtonState != PlayerKey.EButtonState.PRESSED) inputDir.x = -1f;
        }

        // Set vertical direction
        if (moveButtons[Move.PosVertical].ButtonState == PlayerKey.EButtonState.IDLE 
            && moveButtons[Move.NegVertical].ButtonState == PlayerKey.EButtonState.IDLE)
            inputDir.z = 0;
        else if (moveButtons[Move.PosVertical].ButtonState == PlayerKey.EButtonState.PRESSED)
        {
            if (moveButtons[Move.NegVertical].ButtonState == PlayerKey.EButtonState.DOWN) inputDir.z = -1f;
            else if (moveButtons[Move.NegVertical].ButtonState != PlayerKey.EButtonState.PRESSED) inputDir.z = 1f;
        }
        else if (moveButtons[Move.NegVertical].ButtonState == PlayerKey.EButtonState.PRESSED)
        {
            if (moveButtons[Move.PosVertical].ButtonState == PlayerKey.EButtonState.DOWN) inputDir.z = 1f;
            else if (moveButtons[Move.PosVertical].ButtonState != PlayerKey.EButtonState.PRESSED) inputDir.z = -1f;
        }
    }

    #endregion Unity Event

    #region Methods

    private void InitButtons()
    {
        keySetting.moveButtons = new KeyCode[4];
        for (int i = 0; i < defaultMoveKey.Length; i++) keySetting.moveButtons[i] = defaultMoveKey[i];
        keySetting.attackButton = defaultAttackKey;
        keySetting.jumpButton = defaultJumpKey;
        keySetting.skillSlotButtons = new KeyCode[12];
        for (int i = 0; i < defaultSkillSlotKeys.Length; i++) keySetting.skillSlotButtons[i] = defaultSkillSlotKeys[i];
        keySetting.uiButtons = new KeyCode[3];
        for (int i = 0; i < defaultUIKeys.Length; i++) keySetting.uiButtons[i] = defaultUIKeys[i];
    }

    #endregion Methods
}
