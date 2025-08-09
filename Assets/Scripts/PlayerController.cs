using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    
    private Rigidbody2D rb;
    private JieFenceInputSystem jieFenceInputSystem;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        jieFenceInputSystem = new JieFenceInputSystem();

        jieFenceInputSystem.Player.Enable();
        jieFenceInputSystem.Player.Jump.performed += Jump;
        jieFenceInputSystem.Player.Move.performed += Move;

    }
    void FixedUpdate()
    {
        Vector2 inputVector = jieFenceInputSystem.Player.Move.ReadValue<Vector2>().normalized;
        rb.AddForce(inputVector * moveSpeed, ForceMode2D.Force);
    }

    public void Jump(InputAction.CallbackContext context)
    {
        Debug.Log("Jumping"+ "\n" + context.phase);
    }
    public void Move(InputAction.CallbackContext context)
    {
        Debug.Log("Moving" + "\n" + context.phase);
    }
}
