using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : Singleton<ResourceManager>
{
    // ���� �������� ������ �ִ� ��ųʸ�
    private Dictionary<string, UnityEngine.Object> original = new();

    public T LoadResource<T>(string key, string path) where T : UnityEngine.Object
    {
        if(original.TryGetValue(key, out var value))
        {
            return value as T;
        }

        // ���� �ҷ��;ߵ˴ϴ�.
        var obj = Resources.Load(path);

        if(obj is T)
        {
            T result = obj as T;
            original.Add(key, result);
            return result;
        }

        Debug.LogWarning($"not found {path}");
        return null;
    }

    public T GetResource<T>(string key) where T : UnityEngine.Object
    {
        if(original.TryGetValue(key, out var value))
        {
            if (value is T)
                return value as T;
        }

        Debug.LogError($"not found Resource {key}");
        return null;
    }

    private void Awake()
    {
        LoadResource<GameObject>(Enum.GetName(typeof(Enums.POPUPTYPE), Enums.POPUPTYPE.INVENTORY), "Popup\\UIInventory");
    }
}
