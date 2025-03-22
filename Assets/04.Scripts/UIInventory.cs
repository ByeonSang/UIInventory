using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public class UIInventory : UIPopupBase
{
    [SerializeField] private int defaultSlotCount;
    
    private UISlot[] slots;
    
    public int MaxSlot { get; set; }

    private void Awake()
    {
        slots = GetComponentsInChildren<UISlot>();
        MaxSlot = defaultSlotCount;
        InitinalSlots();
    }

    private void InitinalSlots()
    {
        int count = 0;
        foreach (var slot in slots)
        {
            if (count < MaxSlot)
            {
                slot.ActiveSlot(true);
                count++;
            }
            else
            {
                slot.ActiveSlot(false);
            }
        }
    }

    public bool AddItem(ItemData data)
    {
        int slotCount = 0;

        foreach(var slot in slots)
        {
            if(slotCount >= MaxSlot)
            {
                Debug.Log("full Slot!!!");
                return false;
            }

            if(slot.itemData == null)
            {
                slot.itemData = data;
                slot.IconDraw();
                return true;
            }

            slotCount++;
        }

        return false;
    }
}
