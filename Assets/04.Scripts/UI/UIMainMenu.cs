using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMainMenu : MonoBehaviour
{
    private UIManager uiManager;

    public Button StatusBtn;
    public Button InventoryBtn;

    private void Awake()
    {
        uiManager = UIManager.Instance;

        StatusBtn.onClick.AddListener(() => uiManager.GetPopup<UIStatus>().SwitchOn());
        InventoryBtn.onClick.AddListener(() => uiManager.GetPopup<UIInventory>().SwitchOn());
        uiManager.GetPopup<UIInventory>().SetActive(false);
        uiManager.GetPopup<UIStatus>().SetActive(false);
    }
}
