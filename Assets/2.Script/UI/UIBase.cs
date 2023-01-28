using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public static class MouseData
{
    public static UIBase interfaceMouseIsOver;
    public static GameObject slotMouseIsOver;
    public static RectTransform tempItemBeingDragged;
}

public abstract class UIBase : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    #region Variables

    public GameObject[] slots = new GameObject[0];
    protected Dictionary<GameObject, UISlot> slotList = new Dictionary<GameObject, UISlot>();
    
    public GameObject draggedImageObj;
    
    #endregion Variables

    #region Unity Event

    protected virtual void Awake()
    {
        CreateSlotUIs();
    }

    #endregion Unity Event

    #region Methods

    protected abstract void CreateSlotUIs();

    #endregion Methods

    #region Interface Methods

    public void OnPointerEnter(PointerEventData eventData) { MouseData.interfaceMouseIsOver = this; }
    public void OnPointerExit(PointerEventData eventData) { MouseData.interfaceMouseIsOver = null; }

    #endregion Interface Methods

    #region Events

    protected void OnPostUpdate(UISlot p_slot)
    { 
        
    }

    public void OnClick(GameObject p_obj, PointerEventData p_data)
    { 
        
    }

    protected virtual void OnRightClick(GameObject p_obj)
    { 
    
    }

    protected virtual void OnLeftClick(GameObject p_obj)
    { 
    
    }

    #endregion Events
}
