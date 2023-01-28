using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SkillUI : UIBase
{
    protected override void CreateSlotUIs()
    {
        foreach (GameObject t_slot in slots)
        {
            AddEvent(t_slot, EventTriggerType.PointerEnter, delegate { OnEnterSlot(t_slot); });
            AddEvent(t_slot, EventTriggerType.PointerExit, delegate { OnExitSlot(t_slot); });
            AddEvent(t_slot, EventTriggerType.BeginDrag, delegate { OnStartDrag(t_slot); });
            AddEvent(t_slot, EventTriggerType.Drag, delegate { OnDrag(t_slot); });
            AddEvent(t_slot, EventTriggerType.EndDrag, delegate { OnEndDrag(t_slot); });
        }
    }
}
