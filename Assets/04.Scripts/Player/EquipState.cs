using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EquipState
{
    private Dictionary<string, EquipData> currentEquipState = new();

    public EquipState(WeaponData weapon = null, ArmorData armor = null)
    {
        currentEquipState.Add(typeof(WeaponData).Name, weapon);
        currentEquipState.Add(typeof(ArmorData).Name, armor);
    }


    // 어차피 덮어 씌우니깐 그냥 Plus는 그 무기를 참조만 하도록
    public void Equip<T>(EquipData data) where T : EquipData
    {
        T equipment = null;
        if (data != null)
            equipment = data as T;

        currentEquipState[typeof(T).Name] = equipment;
    }

    public T GetEquip<T>() where T : EquipData
    {
        if(currentEquipState.TryGetValue(typeof(T).Name, out var result))
        {
            return result as T;
        }

        Debug.Log($"Not found {typeof(T).Name}");
        return null;
    }

    // 장비 스탯 가져오기
    public Status GetEquipmentStatus()
    {
        // 공통
        float heart = 0f;
        float critical = 0f;

        // 무기
        WeaponData weapon;
        float attack = 0f;
        if(weapon = GetEquip<WeaponData>())
        {
            // weapon값이 있으면
            attack = weapon.Attack;
            heart += weapon.Heart;
            critical += weapon.Critical;
        }

        // 방어구
        ArmorData armor;
        float defence = 0f;
        if (armor = GetEquip<ArmorData>())
        {
            // weapon값이 있으면
            defence = armor.Defence;
            heart += armor.Heart;
            critical += armor.Critical;
        }

        return new Status(attack, defence, heart, critical);
    }

    // 방어구를 장착했던 슬롯을 반환해야되는데 어케하누
    // UIInventory에서 이전 슬롯을 저장 ㅇㅋ 이거다
}
