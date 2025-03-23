using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;


public class EquipmentSlot
{
    // 장비슬롯 멤버변수 저장할 공간
    // 현재는 무기밖에 없어서 간단하게 지정
    public UISlot WeaponSlot;
}

public class UIInventory : UIPopupBase
{
    private EquipmentSlot equipmentSlot;

    [SerializeField] private int defaultSlotCount;
    
    private UISlot[] slots;
    
    public int MaxSlot { get; set; }

    private void Awake()
    {
        equipmentSlot = new EquipmentSlot();
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
            }
            else
            {
                slot.ActiveSlot(false);
            }
            slot.SlotIndex = count++;
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

    public void Equip(UISlot slot)
    {
        Player player = GameManager.Instance.PlayerController;
        ItemData prevItem = player.CurrentWeapon;

        if (prevItem == slot.itemData)
        {
            equipmentSlot.WeaponSlot = null;
            slot.EquipIcon.SetActive(false);
            player.SetStatus(prevItem, true);
            player.CurrentWeapon = null;
        }
        else
        {
            if (equipmentSlot.WeaponSlot != null)
            {
                player.SetStatus(prevItem, true);
                equipmentSlot.WeaponSlot.EquipIcon.SetActive(false);
            }

            slot.EquipIcon.SetActive(true);

            // 현재 슬롯으로 바꿔치기
            equipmentSlot.WeaponSlot = slot;
            player.SetStatus(slot.itemData, false);
            player.CurrentWeapon = slot.itemData;
        }
    }

    /*public void UnEquip(ItemData data)
    {
        // 아이템 type이 생겼을 경우 구현
    }*/


}
