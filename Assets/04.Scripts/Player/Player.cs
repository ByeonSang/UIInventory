using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Status
{
    public float Attack;
    public float Defence;
    public float Heart;
    public float Critical;

    public Status(float attack, float defence, float heart, float critical)
    {
        Attack = attack;
        Defence = defence;
        Heart = heart;
        Critical = critical;
    }
}

public class Level
{
    public int Lv;
    public float CurrentExp;

    public Level(int lv, float currentExt)
    {
        Lv = lv;
        CurrentExp = currentExt;
    }

    public bool AddExp(float plusExp, Status defaultStatus)
    {
        bool result = false;

        if (plusExp < 0f)
            return result;

        int levelUpCount = (int)((CurrentExp + plusExp) / 100f);
        float Calculate = (CurrentExp + plusExp) % 100f;

        // �������ϸ� ����Ʈ ��������
        if(levelUpCount > 0)
        {
            defaultStatus.Attack += levelUpCount;
            defaultStatus.Defence += levelUpCount;
            defaultStatus.Heart += levelUpCount;
            defaultStatus.Critical += levelUpCount;
            result = true;
        }

        Lv += levelUpCount;
        CurrentExp = Calculate;
        return result;
    } 
}


public class Player : MonoBehaviour
{
    // UI ������Ʈ �׼�
    public Action<Status, Status> StatusUpdate;
    public Action<Level> LevelUpdate;
    public Action<int> CoinUpdate;

    [SerializeField] private float moveSpeed;
    private Vector2 direction;

    // ������ ���� -------------------------------
    public Status DefaultStatus { get; set; }


    // Player Inventory
    public List<EquipData> Inventory { get; set; }

    // --------------------------------------------

    // Player Equipmanet ----------------

    public EquipState PlayerEquipState { get; set; } = new();

    // ----------------------------------
    public Level PlayerLevel { get; set; } = new(1,0f);
    private int coin = 0;
    public int Coin
    {
        get { return coin; }
        private set
        {
            coin = value;
            if (coin >= maxCoin)
            {
                coin = maxCoin;
                Debug.Log("Max Coin!!!");
            }
            else if(coin < 0)
            {
                coin = 0;
            }
        }
    }
    private int maxCoin = 3000;
    // ----------------------------------


    private void Awake()
    {
        Inventory = new();
        DefaultStatus = new(2f, 2f, 2f, 2f);

        GameManager.Instance.PlayerController = this;
        
        // �÷��̾ �ʿ��� �˾�â ����
        UIManager.Instance.GetPopup<UIStatus>();
        UIManager.Instance.GetPopup<UIInventory>();
    }

    private void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        direction = new Vector2(h, v);
        direction.Normalize();

        Vector2 moveToward = (Vector2)transform.position + direction * moveSpeed;

        transform.position = Vector3.Lerp(transform.position, moveToward, Time.deltaTime);


        // Test Item��ȯ
        if(Input.GetKeyDown(KeyCode.F))
        {
            GameObject item = ObjectPoolManager.Instance.GetObject<ItemFactory>(typeof(ItemFactory).Name);
            item.transform.position = new Vector3(2f, 0f);
        }

        // Test ����ġ �޾ƸԱ�
        if(Input.GetKeyDown(KeyCode.G))
        {
            // �������� ���� �ٽ� ��ο�
            if (PlayerLevel.AddExp(20f, DefaultStatus))
            {
                Coin += 1000;
                CoinUpdate?.Invoke(Coin);
                SetStatus();
            }

            LevelUpdate?.Invoke(PlayerLevel);
        }
    }

    public void SetStatus()
    {
        Status equipStatus = PlayerEquipState.GetEquipmentStatus();

        // UI ������Ʈ
        StatusUpdate?.Invoke(DefaultStatus, equipStatus);
    }
}
