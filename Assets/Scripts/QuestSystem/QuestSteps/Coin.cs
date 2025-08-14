using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private int goldGained = 1;
    private CircleCollider2D circleCollider2D;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        circleCollider2D = GetComponent<CircleCollider2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }
    private void CollectCoin()
    {
        Debug.Log("The egg has been collected");
        GameEventsManager.instance.goldEvents.GoldGained(goldGained);
        GameEventsManager.instance.miscEvents.coinsCollected();

        circleCollider2D.gameObject.SetActive(false);

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            CollectCoin();
        }
    }
}
