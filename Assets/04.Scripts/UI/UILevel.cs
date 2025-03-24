using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UILevel : MonoBehaviour
{
    public TextMeshProUGUI LevelText;
    public TextMeshProUGUI ExpText;
    public Image ExpGage;

    private void Start()
    {
        Player player = GameManager.Instance.PlayerController;
        player.LevelUpdate = SetLevelUI;
        player.LevelUpdate(player.PlayerLevel);
    }

    public void SetLevelUI(Level level)
    {
        LevelText.text = $"Lv.{level.Lv}";
        ExpGage.fillAmount = level.CurrentExp / 100f;
        ExpText.text = $"{level.CurrentExp % 100f} / 100";
    }
}
