using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

public class UIManager : Singleton<UIManager>
{
    private const string Inventory = "UIInventory";

    // ���� ������� ��ü�� ������ �� �ִ� ��ųʸ�
    Dictionary<Enums.POPUPTYPE, UIPopupBase> popupDict = new();

    public T CreatePopup<T>(Enums.POPUPTYPE type) where T : UIPopupBase
    {
        if(popupDict.TryGetValue(type, out var value))
        {
            return value as T;
        }

        // ���� �������� �����;��Ѵ�.
        // ���� �����ϰ�, ��ųʸ��� ����

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

        // ������ ���� ������ش�
        return CreatePopup<T>(type);
    }
}
