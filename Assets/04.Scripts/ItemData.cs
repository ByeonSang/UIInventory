using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CreateItem", fileName = "ItemData")]
public class ItemData : ScriptableObject
{
    public string ItemName;
    public Sprite Image;

    public Enums.EquipType type;
    public float Attack;
    public float Defence;
    public float Heart;
    public float Critical;
}
