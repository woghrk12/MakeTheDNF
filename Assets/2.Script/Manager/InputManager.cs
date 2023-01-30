using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : SingletonMonobehaviour<InputManager>
{
    #region Variables

    private Vector3 inputDir = Vector3.zero;

    //private static Dictionary<int, PlayerKey> buttons = new Dictionary<int, PlayerKey>();
    private static PlayerKey[] buttons = new PlayerKey[KeyID.End];
    private static Dictionary<KeyCode, int> registerKeys = new Dictionary<KeyCode, int>();

    #region Default Key
    private KeyCode defaultPosHorizontal = KeyCode.RightArrow;
    private KeyCode defaultNegHorizontal = KeyCode.LeftArrow;
    private KeyCode defaultPosVertical = KeyCode.UpArrow;
    private KeyCode defaultNegVertical = KeyCode.DownArrow;
    private KeyCode defaultAttack = KeyCode.X;
    private KeyCode defaultJump = KeyCode.J;
    private KeyCode defaultSkillSlot1 = KeyCode.A;
    private KeyCode defaultSkillSlot2 = KeyCode.S;
    private KeyCode defaultSkillSlot3 = KeyCode.D;
    private KeyCode defaultSkillSlot4 = KeyCode.F;
    private KeyCode defaultSkillSlot5 = KeyCode.G;
    private KeyCode defaultSkillSlot6 = KeyCode.H;
    private KeyCode defaultInventory = KeyCode.I;
    private KeyCode defaultSkillTree = KeyCode.K;
    private KeyCode defaultCharInfo = KeyCode.M;
    #endregion Default Key
    
    #endregion Variables

    #region Properties

    public Vector3 Direction => inputDir;

    public static PlayerKey[] Buttons { get => buttons; }

    #endregion Properties

    #region Unity Event

    protected override void Awake()
    {
        base.Awake();

        // Move
        buttons[KeyID.PosHorizontal].key = PlayerPrefs.HasKey(KeyID.PosHorizontal.ToString())
            ? (KeyCode)PlayerPrefs.GetInt(KeyID.PosHorizontal.ToString())
            : defaultPosHorizontal;
        buttons[KeyID.NegHorizontal].key = PlayerPrefs.HasKey(KeyID.NegHorizontal.ToString())
            ? (KeyCode)PlayerPrefs.GetInt(KeyID.NegHorizontal.ToString())
            : defaultNegHorizontal;
        buttons[KeyID.PosVertical].key = PlayerPrefs.HasKey(KeyID.PosVertical.ToString())
            ? (KeyCode)PlayerPrefs.GetInt(KeyID.PosVertical.ToString())
            : defaultPosVertical;
        buttons[KeyID.NegVertical].key = PlayerPrefs.HasKey(KeyID.NegVertical.ToString())
            ? (KeyCode)PlayerPrefs.GetInt(KeyID.NegVertical.ToString())
            : defaultNegVertical;
        buttons[KeyID.Jump].key = PlayerPrefs.HasKey(KeyID.Jump.ToString())
           ? (KeyCode)PlayerPrefs.GetInt(KeyID.Jump.ToString())
           : defaultJump;

        // Attack
        buttons[KeyID.Attack].key = PlayerPrefs.HasKey(KeyID.Attack.ToString())
            ? (KeyCode)PlayerPrefs.GetInt(KeyID.Attack.ToString())
            : defaultAttack;
        buttons[KeyID.SkillSlot1].key = PlayerPrefs.HasKey(KeyID.SkillSlot1.ToString())
            ? (KeyCode)PlayerPrefs.GetInt(KeyID.SkillSlot1.ToString())
            : defaultSkillSlot1;
        buttons[KeyID.SkillSlot2].key = PlayerPrefs.HasKey(KeyID.SkillSlot2.ToString())
             ? (KeyCode)PlayerPrefs.GetInt(KeyID.SkillSlot2.ToString())
             : defaultSkillSlot2;
        buttons[KeyID.SkillSlot3].key = PlayerPrefs.HasKey(KeyID.SkillSlot3.ToString())
            ? (KeyCode)PlayerPrefs.GetInt(KeyID.SkillSlot3.ToString())
            : defaultSkillSlot3;
        buttons[KeyID.SkillSlot4].key = PlayerPrefs.HasKey(KeyID.SkillSlot4.ToString())
            ? (KeyCode)PlayerPrefs.GetInt(KeyID.SkillSlot4.ToString())
            : defaultSkillSlot4;
        buttons[KeyID.SkillSlot5].key = PlayerPrefs.HasKey(KeyID.SkillSlot5.ToString())
            ? (KeyCode)PlayerPrefs.GetInt(KeyID.SkillSlot5.ToString())
            : defaultSkillSlot5;
        buttons[KeyID.SkillSlot6].key = PlayerPrefs.HasKey(KeyID.SkillSlot6.ToString())
            ? (KeyCode)PlayerPrefs.GetInt(KeyID.SkillSlot6.ToString())
            : defaultSkillSlot6;

        // UI
        buttons[KeyID.Inventory].key = PlayerPrefs.HasKey(KeyID.Inventory.ToString())
            ? (KeyCode)PlayerPrefs.GetInt(KeyID.Inventory.ToString())
            : defaultInventory;
        buttons[KeyID.SkillTree].key = PlayerPrefs.HasKey(KeyID.SkillTree.ToString())
            ? (KeyCode)PlayerPrefs.GetInt(KeyID.SkillTree.ToString())
            : defaultSkillTree;
        buttons[KeyID.CharInfo].key = PlayerPrefs.HasKey(KeyID.CharInfo.ToString())
            ? (KeyCode)PlayerPrefs.GetInt(KeyID.CharInfo.ToString())
            : defaultCharInfo;
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
