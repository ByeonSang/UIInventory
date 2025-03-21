using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

public class UIManager : Singleton<UIManager>
{
    private const string Inventory = "UIInventory";

    // 새로 만들어진 객체를 저장할 수 있는 딕셔너리
    Dictionary<Enums.POPUPTYPE, UIPopupBase> popupDict = new();

    public T CreatePopup<T>(Enums.POPUPTYPE type) where T : UIPopupBase
    {
        if(popupDict.TryGetValue(type, out var value))
        {
            return value as T;
        }

        // 원본 프리펩을 가져와야한다.
        // 새로 생성하고, 딕셔너리에 저장

        GameObject prefab = ResourceManager.Instance.GetResource<GameObject>(Enum.GetName(typeof(Enums.POPUPTYPE), type));
        GameObject go = Instantiate(prefab);

        if(go.TryGetComponent(out T popupScript))
        {
            popupDict.Add(type, popupScript);

            return popupScript;
        }
        else
        {
            Debug.Log($"didn't make {Enum.GetName(typeof(Enums.POPUPTYPE), type)} object");
            return null;
        }
    }

    public T GetPopupUI<T>(Enums.POPUPTYPE type) where T : UIPopupBase
    {
        if(popupDict.TryGetValue(type, out var popupScript))
        {
            if (popupScript is T)
                return popupScript as T;
        }

        // 없으면 새로 만들어준다
        return CreatePopup<T>(type);
    }
}
