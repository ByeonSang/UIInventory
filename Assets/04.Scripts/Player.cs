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

    // new 새로 할당하는 자체가 싫어서 그냥 Set으로 만들었음
    public void Set(float attack, float defence, float heart, float critical)
    {
        Attack = attack;
        Defence = defence;
        Heart = heart;
        Critical = critical;
    }
}


public class Player : MonoBehaviour
{
    // UI 업데이트 액션
    public Action<Status, Status> StatusUpdate;

    [SerializeField] private float moveSpeed;
    private Rigidbody2D rigid;
    private Vector2 direction;

    // 간단한 구조 -------------------------------
    public Status DefaultStatus { get; set; }
    public Status PlusStatus { get; set; }


    // Player Inventory
    public List<ItemData> Inventory { get; set; }

    // --------------------------------------------

    // Player Equipmanet ----------------

    public ItemData CurrentWeapon { get; set; }

    // ----------------------------------

    private void Awake()
    {
        Inventory = new();
        DefaultStatus = new(2f, 2f, 2f, 2f);
        PlusStatus = new(0f, 0f, 0f, 0f);

        GameManager.Instance.PlayerController = this;
        
        // 플레이어가 필요한 팝업창 셋팅
        UIManager.Instance.ShowPopup<UIStatus>();
        UIManager.Instance.ShowPopup<UIInventory>();
    }

    private IEnumerator Start()
    {
        rigid = GetComponent<Rigidbody2D>();

        yield return (new WaitUntil(() => StatusUpdate != null));

        // 초기화
        StatusUpdate?.Invoke(DefaultStatus, PlusStatus);
    }

    private void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        direction = new Vector2(h, v);
        direction.Normalize();

        Vector2 moveToward = (Vector2)transform.position + direction * moveSpeed;

        transform.position = Vector3.Lerp(transform.position, moveToward, Time.deltaTime);
    }

    public void SetStatus(ItemData data, bool unEquip)
    {
        if(unEquip)
            PlusStatus.Set(PlusStatus.Attack - data.Attack, PlusStatus.Defence - data.Defence, PlusStatus.Heart - data.Heart, PlusStatus.Critical - data.Critical);        
        else        
            PlusStatus.Set(PlusStatus.Attack + data.Attack, PlusStatus.Defence + data.Defence, PlusStatus.Heart + data.Heart, PlusStatus.Critical + data.Critical);

        // UI 업데이트
        StatusUpdate?.Invoke(DefaultStatus, PlusStatus);
    }
}
