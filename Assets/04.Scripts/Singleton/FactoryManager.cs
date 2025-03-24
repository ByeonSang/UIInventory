using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryManager : Singleton<FactoryManager>
{
    public Dictionary<string, FactoryBase> path = new();


    // µÒº≈≥ ∏Æ √ ±‚»≠
    public void ClearPath()
    {
        path.Clear();
    }
}
