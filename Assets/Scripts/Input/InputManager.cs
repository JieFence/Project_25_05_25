using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{

    private bool isInteractPressed;
    private bool isSubmitPressed;

    public Vector2 moveVector2 { get; private set; }


    public JieFenceInputSystem jieFenceInputSystem;

    public static InputManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        jieFenceInputSystem = new JieFenceInputSystem();


    }
    void OnEnable()
    {
        // test
        jieFenceInputSystem.Player.Enable();
        jieFenceInputSystem.UI.Enable();

        jieFenceInputSystem.Player.Jump.performed += Jump;
        jieFenceInputSystem.Player.Interact.performed += IsInteractPressed;
        jieFenceInputSystem.Player.Interact.canceled += IsInteractPressed;

        jieFenceInputSystem.UI.Submit.performed += IsSubmitPressed;
        jieFenceInputSystem.UI.Submit.canceled += IsSubmitPressed;
    }
    private void FixedUpdate()
    {
        moveVector2 = jieFenceInputSystem.Player.Move.ReadValue<Vector2>();
    }


    public void Jump(InputAction.CallbackContext context)
    {
        Debug.Log("Jumping" + "\n" + context.phase + " " + context.time);
    }

    private void IsInteractPressed(InputAction.CallbackContext context)
    {
        Debug.Log("Interacting"  + context.phase + " " + context.time);

        if (context.performed)
        {
            isInteractPressed = true;
        }
        else if (context.canceled)
        {
            isInteractPressed = false;
        }
    }

    public void IsSubmitPressed(InputAction.CallbackContext context)
    {
        Debug.Log("Submitting"  + context.phase + " " + context.time);
        if (context.performed)
        {
            isSubmitPressed = true;
        }
        else if (context.canceled)
        {
            isSubmitPressed = false;
        }
    }

    public bool GetInteractPressed()
    {
        bool result = isInteractPressed;
        isInteractPressed = false; // Reset after reading
        return result;
    }
    public bool GetSubmitPressed()
    {
        bool result = isSubmitPressed;
        isSubmitPressed = false; // Reset after reading
        return result;
    }



}
