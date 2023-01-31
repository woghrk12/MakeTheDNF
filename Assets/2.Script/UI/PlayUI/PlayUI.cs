using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayUI : UIBase
{
    [SerializeField] private GameObject[] registerSkillSlots = null;
    
    protected override void CreateSlotUIs()
    {
        foreach (GameObject t_obj in registerSkillSlots)
        {
            RegisterSkillSlot t_slot = t_obj.GetComponent<RegisterSkillSlot>();
            t_slot.uiBase = this;
            slotList.Add(t_obj, t_slot);
        }
    }
}
