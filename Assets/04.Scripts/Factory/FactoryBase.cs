using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FactoryBase : MonoBehaviour
{
    public GameObject Prefab;
    protected ResourceManager resourceManager;

    public abstract GameObject CreateObject(GameObject obj = null);
}
