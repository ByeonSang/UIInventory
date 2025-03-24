using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class EquipData : ScriptableObject
{
    public Enums.EquipType type;
    public string ItemName;
    public Sprite Image;

    public float Heart;
    public float Critical;
}
