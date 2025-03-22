using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] private float moveSpeed;
    private Rigidbody2D rigid;
    private Vector2 direction;

    // 간단한 구조 -------------------------------

    [Header("PlayerStatus")]
    public float DefaultAttack = 2f;
    public float DefaultDefence = 2f;
    public float DefaultHeart = 2f;
    public float DefaultCritical = 2f;

    public float PlusAttack { get; set; }
    public float PlusDefence  { get; set; }
    public float PlusHeart  { get; set; }
    public float PlusCritical  { get; set; }


    // Player Inventory
    public List<ItemData> Inventory { get; set; }

    // --------------------------------------------

    private void Awake()
    {
        Inventory = new();
    }

    private void Start()
    {
        GameManager.Instance.PlayerController = this;
        rigid = GetComponent<Rigidbody2D>();
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
}
