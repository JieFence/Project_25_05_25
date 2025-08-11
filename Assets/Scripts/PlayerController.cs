using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;

    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (DialogueManager.GetInstance().isDialoguePlaying)
        {
            return;
        }

        rb.AddForce(InputManager.instance.moveVector2 * moveSpeed);
    }

}
