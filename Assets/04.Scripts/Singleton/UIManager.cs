using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

public class UIManager : Singleton<UIManager>
{
    // ���� ������� ��ü�� ������ �� �ִ� ��ųʸ�
    Dictionary<string, UIPopupBase> popupDict = new();

    public T ShowPopup<T>() where T : UIPopupBase
    {
        if(popupDict.TryGetValue(typeof(T).Name, out var value))
        {
            return value as T;
        }

        // ���� �������� �����;��Ѵ�.
        // ���� �����ϰ�, ��ųʸ��� ����

        string name = typeof(T).Name;
        string path = $"Popup\\{name}";

        GameObject prefab = ResourceManager.Instance.LoadResource<GameObject>(name, path);
        GameObject go = Instantiate(prefab);

        if(go.TryGetComponent(out T popupScript))
        {
            popupDict[typeof(T).Name] =  popupScript;

            return popupScript;
        }
        else
        {
            Debug.Log($"didn't make {typeof(T).Name} object");
            return null;
        }
    }
}
