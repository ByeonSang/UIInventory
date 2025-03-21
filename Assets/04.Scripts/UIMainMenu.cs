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

        StatusBtn.onClick.AddListener(() => uiManager.ShowPopup<UIStatus>().SwitchOn());
        InventoryBtn.onClick.AddListener(() => uiManager.ShowPopup<UIInventory>().SwitchOn());
        uiManager.ShowPopup<UIInventory>().SetActive(false);
        uiManager.ShowPopup<UIStatus>().SetActive(false);
    }
}
