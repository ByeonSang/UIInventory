using System;
using TMPro;
using UnityEngine;

public class UICoin : MonoBehaviour
{
    public TextMeshProUGUI CoinText;

    private void Start()
    {
        Player player = GameManager.Instance.PlayerController;
        player.CoinUpdate = SetCoinText;
        player.CoinUpdate(player.Coin);
    }

    public void SetCoinText(int coin)
    {
        if (coin != 0)
            CoinText.text = string.Format("{0:#,###}", coin);
        else
            CoinText.text = $"0";
    }
}
