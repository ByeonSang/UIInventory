using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMainMenu : MonoBehaviour
{
    UIManager uiManager;

    private void Awake()
    {
        uiManager = UIManager.Instance;

        uiManager.CreatePopup<UIInventory>(Enums.POPUPTYPE.INVENTORY);
    }
}
