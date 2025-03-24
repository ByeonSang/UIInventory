using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFactory : FactoryBase
{
    // ������ ������
    private List<EquipData> dataList = new();

    private const string path = "Scriptable\\Item";
    // ������ ������ �ʿ��Ѱ�
    // ������ ������, ������ ������ ( ���� ��ü )

    // ���ҽ� �Ŵ������� �������� ��������
    // �� ������ �ȿ� �ִ� Item ��ũ��Ʈ�� ����
    // ���� ��, ItemData ��ü

    // ������ �����Ͱ��� �͵��� �����ũ���� ������ �ε�
    private void Awake()
    {
        dataList.Add(ResourceManager.Instance.LoadResource<WeaponData>("Hammer", $"{path}\\Hammer"));
        dataList.Add(ResourceManager.Instance.LoadResource<WeaponData>("WoodSword", $"{path}\\WoodSword"));
        dataList.Add(ResourceManager.Instance.LoadResource<ArmorData>("RockArmor", $"{path}\\RockArmor"));
        dataList.Add(ResourceManager.Instance.LoadResource<ArmorData>("WoodArmor", $"{path}\\WoodArmor"));
    }

    private void Start()
    {
        FactoryManager.Instance.path[typeof(ItemFactory).Name] = this;
    }

    // �Ű������� �޴� ������Ʈ�� ������ �װ� �����
    public override GameObject CreateObject(GameObject obj = null)
    {
        EquipData newData;
        Type type;
        if ((newData = GetRandomItemData(out type)) == null)
            return null;


        if(obj == null)
            obj = Instantiate(Prefab, this.transform);

        obj.GetComponent<Item>().itemData = newData;

        return obj;
    }

    private EquipData GetRandomItemData(out Type type)
    {
        if (dataList.Count > 0)
        {
            int rand = UnityEngine.Random.Range(0, dataList.Count);

            type = dataList[rand].GetType();
            return dataList[rand];
        }

        type = null;
        return null;
    }
}
