using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryManager : Singleton<FactoryManager>
{
    public Dictionary<string, FactoryBase> path = new();


    // ��ųʸ� �ʱ�ȭ
    public void ClearPath()
    {
        path.Clear();
    }
}
