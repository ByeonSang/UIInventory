using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFactory : FactoryBase
{
    // ������ ������
    private List<ItemData> dataList = new();

    private const string path = "Scriptable\\Item";
    // ������ ������ �ʿ��Ѱ�
    // ������ ������, ������ ������ ( ���� ��ü )

    // ���ҽ� �Ŵ������� �������� ��������
    // �� ������ �ȿ� �ִ� Item ��ũ��Ʈ�� ����
    // ���� ��, ItemData ��ü

    // ������ �����Ͱ��� �͵��� �����ũ���� ������ �ε�
    private void Awake()
    {
        dataList.Add(ResourceManager.Instance.LoadResource<ItemData>("Hammer", $"{path}\\Hammer"));
        dataList.Add(ResourceManager.Instance.LoadResource<ItemData>("RockArmor", $"{path}\\RockArmor"));
        dataList.Add(ResourceManager.Instance.LoadResource<ItemData>("WoodArmor", $"{path}\\WoodArmor"));
        dataList.Add(ResourceManager.Instance.LoadResource<ItemData>("WoodSword", $"{path}\\WoodSword"));
    }

    private void Start()
    {
        FactoryManager.Instance.path[typeof(ItemFactory).Name] = this;
    }

    // �Ű������� �޴� ������Ʈ�� ������ �װ� �����
    public override GameObject CreateObject(GameObject obj = null)
    {
        ItemData newData;
        if ((newData = GetRandomItemData()) == null)
            return null;


        if(obj == null)
            obj = Instantiate(Prefab, this.transform);

        obj.GetComponent<Item>().itemData = newData;

        return obj;
    }

    private ItemData GetRandomItemData()
    {
        if (dataList.Count > 0)
        {
            int rand = Random.Range(0, dataList.Count);

            return dataList[rand];
        }

        return null;
    }
}
