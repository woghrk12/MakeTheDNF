using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UISlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    #region Variables

    public UIBase uiBase;

    public Image iconImage;
    public Text valueText;

    [HideInInspector] public int id = -1;
    [HideInInspector] public int value = 0;

    public Action<UISlot> OnPreUpdate;
    public Action<UISlot> OnPostUpdate;

    #endregion Variables

    #region Interface Methods

    public void OnPointerEnter(PointerEventData p_data) { MouseData.slotMouseIsOver = gameObject; }
    public void OnPointerExit(PointerEventData p_data) { MouseData.slotMouseIsOver = null; }

    public void OnBeginDrag(PointerEventData p_data) 
    {
        //if (id < 0) return;
        
        GameObject t_dragObj = uiBase.draggedImageObj;

        t_dragObj.SetActive(true);
        t_dragObj.GetComponent<Image>().sprite = iconImage.sprite;
        MouseData.tempItemBeingDragged = t_dragObj.GetComponent<RectTransform>();
    }
    public void OnDrag(PointerEventData p_data)
    {
        if (MouseData.tempItemBeingDragged == null) return;
        MouseData.tempItemBeingDragged.position = Input.mousePosition;
    }
    public virtual void OnEndDrag(PointerEventData p_data) 
    {
        uiBase.draggedImageObj.SetActive(false);
        MouseData.tempItemBeingDragged = null; 
    }

    public void OnPointerClick(PointerEventData eventData)
    {

    }
    #endregion Interface Methods

    #region Helper Methods

    #endregion Helper Methods
}
