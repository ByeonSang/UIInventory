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

        // �������ΰ� ������
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



        // ���� ���԰� ���� ���� ������ ������ ������ ��� ����
        // �ļ����� ������ ��ġ ������ ���� �����ϱ� �׳� �̴��...
        // ��ũ���ͺ� �Ѱ��� �ε��ϴ� ���¶� ��� �����͵��� �Ȱ��� ����� �ϹǷ�
        // �ذ� ������δ� ��ũ���ͺ��� ���� �����ϴ°͹ۿ� ������ ���� �ʴ´�.

        // �ٸ� �������
        // �������� Ŭ������ �ϳ� ���ļ� �� �������� ��� �����ߴ��� ���θ� �ľ��߽��ϴ�.

        // ���� �������� �����۰� ���� ������ ��
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
