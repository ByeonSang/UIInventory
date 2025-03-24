using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


// �׳� �ؽ�Ʈ ã�� �뵵
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
        Player player = GameManager.Instance.PlayerController;
        player.StatusUpdate = SetStatusText;
        player.StatusUpdate?.Invoke(player.DefaultStatus, player.PlayerEquipState.GetEquipmentStatus());
    }

    private void SetStatusText(Status _default, Status _plus)
    {
        if(_plus != null)
        {
            // �÷��̾ �����ؼ� �⺻ ��ġ�� �÷��� ��ġ�� ����
            meshTexts[(int)ABILITY.ATTACK].text = $"Attack : {_default.Attack} (+{_plus.Attack})";
            meshTexts[(int)ABILITY.DEFENCE].text = $"Defence : {_default.Defence} (+{_plus.Defence})";
            meshTexts[(int)ABILITY.HEART].text = $"Heart : {_default.Heart} (+{_plus.Heart})";
            meshTexts[(int)ABILITY.CRITICAL].text = $"Critical : {_default.Critical} (+{_plus.Critical})";
        }
        else
        {
            // �÷��̾ �����ؼ� �⺻ ��ġ�� �÷��� ��ġ�� ����
            meshTexts[(int)ABILITY.ATTACK].text = $"Attack : {_default.Attack} (+0)";
            meshTexts[(int)ABILITY.DEFENCE].text = $"Defence : {_default.Defence} (+0)";
            meshTexts[(int)ABILITY.HEART].text = $"Heart : {_default.Heart} (+0)";
            meshTexts[(int)ABILITY.CRITICAL].text = $"Critical : {_default.Critical} (+0)";
        }    
    }

}
