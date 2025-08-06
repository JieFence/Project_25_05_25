using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]

public class Player : MonoBehaviour, IDataPersistence
{
    [SerializeField]
    private int deathCount;


    private BoxCollider2D boxCollider;
    private Rigidbody2D rb;

    void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
    }


    public void LoadGame(GameData gameData)
    {
        deathCount = gameData.deathCount;
        transform.position = gameData.playerPosition;
    }

    public void SaveGame(ref GameData gameData)
    {
        gameData.deathCount = deathCount;
        gameData.playerPosition = transform.position;
    }
}
