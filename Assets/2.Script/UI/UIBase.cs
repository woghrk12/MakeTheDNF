using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public static class MouseData
{
    public static UIBase interfaceMouseIsOver;
    public static GameObject slotMouseIsOver;
    public static RectTransform tempItemBeingDragged;
}

[RequireComponent(typeof(EventTrigger))]
public abstract class UIBase : MonoBehaviour
{
    #region Variables

    public GameObject[] slots = new GameObject[0];
    protected Dictionary<GameObject, UISlot> slotList = new Dictionary<GameObject, UISlot>();

    #endregion Variables

    #region Unity Event

    protected virtual void Awake()
    {
        AddEvent(gameObject, EventTriggerType.PointerEnter, delegate { OnEnterInterface(gameObject); });
        AddEvent(gameObject, EventTriggerType.PointerExit, delegate { OnExitInterface(gameObject); });
    }

    #endregion Unity Event

    #region Methods

    protected abstract void CreateSlotUIs();

    protected void AddEvent(GameObject p_obj, EventTriggerType p_type, UnityAction<BaseEventData> p_action)
    {
        if (!p_obj.TryGetComponent(out EventTrigger t_trigger))
        {
            Debug.LogWarning("No EventTrigger component found!");
            return;
        }

        EventTrigger.Entry t_entry = new EventTrigger.Entry { eventID = p_type };
        t_entry.callback.AddListener(p_action);
        t_trigger.triggers.Add(t_entry);
    }

    #endregion Methods

    #region Helper Methods

    private GameObject CreateDragImage(GameObject p_obj)
    {
        if (slotList[p_obj].id < 0) return null;
        
        GameObject t_dragImageObj = new GameObject();
        t_dragImageObj.name = "Drag Image";

        RectTransform t_rectTransform = t_dragImageObj.AddComponent<RectTransform>();
        t_rectTransform.sizeDelta = new Vector2(50, 50);
        t_dragImageObj.transform.SetParent(transform.parent);

        Image t_image = t_dragImageObj.AddComponent<Image>();
        t_image.sprite = slotList[p_obj].iconImage.sprite;
        t_image.raycastTarget = false;

        return t_dragImageObj;
    }

    #endregion Helper Methods

    #region Events

    protected void OnPostUpdate(UISlot p_slot)
    { 
        
    }

    public void OnEnterInterface(GameObject p_obj) { MouseData.interfaceMouseIsOver = p_obj.GetComponent<UIBase>(); }
    public void OnExitInterface(GameObject p_obj) { MouseData.interfaceMouseIsOver = null; }

    public void OnEnterSlot(GameObject p_obj) { MouseData.slotMouseIsOver = p_obj; }
    public void OnExitSlot(GameObject p_obj) { MouseData.slotMouseIsOver = null; }

    public void OnStartDrag(GameObject p_obj) { MouseData.tempItemBeingDragged = CreateDragImage(p_obj).GetComponent<RectTransform>(); }

    public void OnDrag(GameObject p_obj)
    {
        if (MouseData.tempItemBeingDragged == null) return;
        MouseData.tempItemBeingDragged.position = Input.mousePosition;
    }

    public virtual void OnEndDrag(GameObject p_obj) { Destroy(MouseData.tempItemBeingDragged.gameObject); }

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
