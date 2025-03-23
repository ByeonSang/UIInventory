using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


// 그냥 텍스트 찾기 용도
public enum ABILITY
{
    ATTACK,
    DEFENCE,
    HEART,
    CRITICAL,

    COUNT,
}

public class UIStatus : UIPopupBase
{
    public GameObject Content;

    private TextMeshProUGUI[] meshTexts;

    private void Awake()
    {
        meshTexts = Content.GetComponentsInChildren<TextMeshProUGUI>();
    }

    private void Start()
    {
        GameManager.Instance.PlayerController.StatusUpdate = SetStatusText;
    }

    private void SetStatusText(Status _default, Status _plus)
    {
        // 플레이어를 참조해서 기본 수치와 플러스 수치를 적용
        meshTexts[(int)ABILITY.ATTACK].text = $"Attack : {_default.Attack} (+{_plus.Attack})";
        meshTexts[(int)ABILITY.DEFENCE].text = $"Defence : {_default.Defence} (+{_plus.Defence})";
        meshTexts[(int)ABILITY.HEART].text = $"Heart : {_default.Heart} (+{_plus.Heart})";
        meshTexts[(int)ABILITY.CRITICAL].text = $"Critical : {_default.Critical} (+{_plus.Critical})";
    }

}
