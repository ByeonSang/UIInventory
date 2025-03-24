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


    // ������ ���� ����ϱ� �׳� Plus�� �� ���⸦ ������ �ϵ���
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

    // ��� ���� ��������
    public Status GetEquipmentStatus()
    {
        // ����
        float heart = 0f;
        float critical = 0f;

        // ����
        WeaponData weapon;
        float attack = 0f;
        if(weapon = GetEquip<WeaponData>())
        {
            // weapon���� ������
            attack = weapon.Attack;
            heart += weapon.Heart;
            critical += weapon.Critical;
        }

        // ��
        ArmorData armor;
        float defence = 0f;
        if (armor = GetEquip<ArmorData>())
        {
            // weapon���� ������
            defence = armor.Defence;
            heart += armor.Heart;
            critical += armor.Critical;
        }

        return new Status(attack, defence, heart, critical);
    }

    // ���� �����ߴ� ������ ��ȯ�ؾߵǴµ� �����ϴ�
    // UIInventory���� ���� ������ ���� ���� �̰Ŵ�
}
