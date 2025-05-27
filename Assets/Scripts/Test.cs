using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test : MonoBehaviour
{
    void Update()
    {
        // if (Keyboard.current.aKey.wasPressedThisFrame)
        // {
        //     Debug.Log("A key was pressed");
        // }
    }
    
    
    public void Jump(InputAction.CallbackContext context)
    {
        print(context);
    }  
}
