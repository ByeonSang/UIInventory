using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
public class Item : MonoBehaviour
{
    public ItemData itemData;

    private SpriteRenderer spr;
    private BoxCollider2D boxCol;


    private void Awake()
    {
        spr = GetComponent<SpriteRenderer>();
        boxCol = GetComponent<BoxCollider2D>();
        gameObject.SetActive(false);
    }

    // 오브젝트 풀 사용하는 오브젝은 시작을 OnEnable로 설정
    private void OnEnable()
    {
        transform.name = $"Item_{itemData.name}";
        spr.sprite = itemData.Image;
        boxCol.isTrigger = true;

        transform.localScale = Vector2.one * 0.5f;
        boxCol.size = Vector2.one;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if(UIManager.Instance.GetPopup<UIInventory>().AddItem(itemData))
            {
                GameManager.Instance.PlayerController.Inventory.Add(itemData);
                ObjectPoolManager.Instance.ReturnObject<ItemFactory>(this.gameObject);
            }
        }
    }
}
