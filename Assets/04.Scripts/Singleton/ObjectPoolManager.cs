using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : Singleton<ObjectPoolManager>
{
    // ���ӿ�����Ʈ�� ������ �� �ְ� ������ �� �ִ� ���� �ʿ�
    // Ű�� ��ȯ���� ���ӿ�����Ʈ Ÿ���� ����, �������� ����Ʈ�� �Ͽ��� ť�� ����� �� �ִ� ���ӿ�����Ʈ�� ������ ��ȯ
    // ������ ���� ������ ť�� �־��ص�, ��ȯ

    // ���� �Ѿ�� ��� �ʱ�ȭ << ���߿� ����� ���߰ڴ�

    private Dictionary<string, Queue<GameObject>> objectPool = new();

    private FactoryManager factory;

    protected override void Awake()
    {
        base.Awake();
        factory = FactoryManager.Instance;
    }

    /// <summary>
    /// T : FactoryType, key : FactoryTypeName
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key"></param>
    /// <returns></returns>
    public GameObject GetObject<T>(string key) where T : FactoryBase
    {
        GameObject poolGo = null;
        if(objectPool.TryGetValue(key, out var queue))
        {
            // ���ӿ�����Ʈ�� ��� ������
            if(queue.Count > 0)
            {
                GameObject dequeueGo = queue.Dequeue();
                poolGo = dequeueGo;
            }
        }

        // ���� �� �߰�
        GameObject obj = factory.path[typeof(T).Name].CreateObject(poolGo);

        // �ش�Ǵ� Ű�� ������ ���� ����
        if (!objectPool.ContainsKey(typeof(T).Name))
            objectPool.Add(typeof(T).Name, new Queue<GameObject>());

        obj.SetActive(true);
        return obj;
    }

    /// <summary>
    /// T : FactoryType, GameObject : FactoryType���� ������� obj
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key"></param>
    /// <returns></returns>
    public void ReturnObject<T>(GameObject obj)
    {
        if (obj == null)
            return;

        obj.SetActive(false);
        objectPool[typeof(T).Name].Enqueue(obj);
    }

    public void ClearObjectPool()
    {
        objectPool.Clear();
    }

}
