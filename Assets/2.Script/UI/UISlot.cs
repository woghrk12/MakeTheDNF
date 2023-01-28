using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISlot : MonoBehaviour
{
    #region Variables

    public Image iconImage;
    public Text valueText;
    [HideInInspector] public int id = -1;
    [HideInInspector] public int value = 0;

    public Action<UISlot> OnPreUpdate;
    public Action<UISlot> OnPostUpdate;
    
    #endregion Variables
}
