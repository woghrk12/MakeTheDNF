using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : SingletonMonobehaviour<InputManager>
{
    #region Variables

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
