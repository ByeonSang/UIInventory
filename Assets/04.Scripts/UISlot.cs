using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class UISlot : MonoBehaviour
{
    private UIInventory inventory;

    public int SlotIndex;


    // Color Set
    private Color dontAcitveColor = Color.gray;
    private Color AcitveColor = Color.white;

    public EquipData itemData;
    public bool IsActive;

    private Image img;

    // Children Image data
    private Image itemIcon;
    public GameObject EquipIcon;

    private Color alphaZero = Color.white * 0f;
    private Color alphaOrigin = Color.white;


    private void Awake()
    {
        inventory = GetComponentInParent<UIInventory>();

        // slot
        img = GetComponent<Image>();

        // itemIcon
        itemIcon = transform.GetChild(0).GetComponent<Image>();

        ActiveSlot(IsActive);
        itemIcon.color = alphaZero;

        // 만약 아이템이 소비형 아이템이면
        // ItemData에 Type을 설정해줘서
        // 어떤 타입인지 따라서 체크해주고 그 상황에 맞는 메서드 호출
        GetComponent<Button>().onClick.AddListener(OnSlotClick);
    }

    public void ActiveSlot(bool active)
    {
        IsActive = active;

        if(img)
        {
            if (active)
                img.color = AcitveColor;
            else
                img.color = dontAcitveColor;
        }
        
    }

    public void IconDraw()
    {
        itemIcon.color = alphaOrigin;
        itemIcon.sprite = itemData.Image;
    }

    public void OnSlotClick()
    {
        if(itemData != null)
        {
            switch (itemData.type)
            {
                case Utils.EquipType.Weapon:
                    inventory.Equip<WeaponData>(this);
                    break;
                case Utils.EquipType.Armor:
                    inventory.Equip<ArmorData>(this);
                    break;
            }
        }
    }
}
