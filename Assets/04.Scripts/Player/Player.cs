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


public class Player : MonoBehaviour
{
    // UI ������Ʈ �׼�
    public Action<Status, Status> StatusUpdate;

    [SerializeField] private float moveSpeed;
    private Vector2 direction;

    // ������ ���� -------------------------------
    public Status DefaultStatus { get; set; }


    // Player Inventory
    public List<EquipData> Inventory { get; set; }

    // --------------------------------------------

    // Player Equipmanet ----------------

    public EquipState equipState = new();

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

    private IEnumerator Start()
    {
        yield return (new WaitUntil(() => StatusUpdate != null));

        // �ʱ�ȭ
        StatusUpdate?.Invoke(DefaultStatus, equipState.GetEquipmentStatus());
    }

    private void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        direction = new Vector2(h, v);
        direction.Normalize();

        Vector2 moveToward = (Vector2)transform.position + direction * moveSpeed;

        transform.position = Vector3.Lerp(transform.position, moveToward, Time.deltaTime);


        if(Input.GetKeyDown(KeyCode.F))
        {
            GameObject item = ObjectPoolManager.Instance.GetObject<ItemFactory>(typeof(ItemFactory).Name);
            item.transform.position = new Vector3(2f, 0f);
        }
    }

    public void SetStatus()
    {
        Status equipStatus = equipState.GetEquipmentStatus();

        // UI ������Ʈ
        StatusUpdate?.Invoke(DefaultStatus, equipStatus);
    }
}
