using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;



public class PrevEquipSlot
{
    private Dictionary<string, UISlot> prevEquipSlot = new();
    
    public PrevEquipSlot()
    {
        prevEquipSlot.Add(typeof(WeaponData).Name, null);
        prevEquipSlot.Add(typeof(ArmorData).Name, null);
    }

    public UISlot GetPrevSlot<T>() where T : EquipData
    {
        if(prevEquipSlot.TryGetValue(typeof(T).Name, out var result))
        {
            return result;
        }

        // 착용중인게 없으면
        return null;
    }

    public void SetPrevSlot<T>(UISlot slot) where T : EquipData
    {
        prevEquipSlot[typeof(T).Name] = slot;
    }
}

public class UIInventory : UIPopupBase
{

    [SerializeField] private int defaultSlotCount;
    
    private UISlot[] slots;

    private PrevEquipSlot prevSelectedSlot = new();
    
    public int MaxSlot { get; set; }

    private void Awake()
    {
        slots = GetComponentsInChildren<UISlot>();
        MaxSlot = defaultSlotCount;
        InitinalSlots();
    }

    private void InitinalSlots()
    {
        int countSlot = 0;
        foreach (var slot in slots)
        {
            if (countSlot < MaxSlot)
            {
                slot.ActiveSlot(true);
                slot.SlotIndex = countSlot++;
            }
            else
            {
                slot.ActiveSlot(false);
            }
        }
    }

    public void AddSlot()
    {
        MaxSlot++;
        MaxSlot = Mathf.Min(MaxSlot, MaxSlot, slots.Length);

        if (!slots[MaxSlot - 1].IsActive)
            slots[MaxSlot - 1].ActiveSlot(true);
    }


    public bool AddItem(EquipData data)
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

    public void Equip<T>(UISlot currentSlot) where T : EquipData
    {
        Player player = GameManager.Instance.PlayerController;
        EquipState curEquip = player.PlayerEquipState;



        // 이전 슬롯과 내가 현재 선택한 슬롯이 같으면 장비 해제
        // 꼼수지만 아이템 위치 변경을 하지 않으니깐 그냥 이대로...
        // 스크립터블 한개만 로드하는 상태라서 모든 데이터들이 똑같은 취급을 하므로
        // 해결 방법으로는 스크립터블을 각각 생성하는것밖에 생각이 나지 않는다.

        // 다른 방법으로
        // 장착슬롯 클래스를 하나 더파서 그 슬롯으로 장비를 장착했는지 여부를 파악했습니다.

        // 현재 착용중인 아이템과 현재 아이템 비교
        if(prevSelectedSlot.GetPrevSlot<T>()  == currentSlot)
        {
            curEquip.Equip<T>(null);
            prevSelectedSlot.GetPrevSlot<T>().EquipIcon.SetActive(false);
            prevSelectedSlot.SetPrevSlot<T>(null);
        }
        else
        {
            if(prevSelectedSlot.GetPrevSlot<T>() != null)
                prevSelectedSlot.GetPrevSlot<T>().EquipIcon.SetActive(false);
            
            curEquip.Equip<T>(currentSlot.itemData as T);
            currentSlot.EquipIcon.SetActive(true);
            prevSelectedSlot.SetPrevSlot<T>(currentSlot);
        }
        player.SetStatus();
    }
}
