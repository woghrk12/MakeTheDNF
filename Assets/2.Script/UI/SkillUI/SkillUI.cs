using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SkillUI : UIBase
{ 
    protected override void CreateSlotUIs()
    {
        foreach (GameObject t_obj in slots)
        {
            SkillSlot t_slot = t_obj.GetComponent<SkillSlot>();
            t_slot.uiBase = this;
            slotList.Add(t_obj, t_slot);
        }
    }
}
