using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFactory : FactoryBase
{
    // 데이터 프리펩
    private List<ItemData> dataList = new();

    private const string path = "Scriptable\\Item";
    // 아이템 생성에 필요한것
    // 아이템 데이터, 아이템 프리펩 ( 원본 객체 )

    // 리소스 매니저에서 프리펩을 가져오고
    // 그 프리펩 안에 있는 Item 스크립트에 접근
    // 접근 후, ItemData 교체

    // 아이템 데이터관한 것들은 어웨이크에서 데이터 로드
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

    // 매개변수로 받는 오브젝트가 있으면 그걸 씌우기
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
