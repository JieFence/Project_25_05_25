using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [Header("First Selected Menu")]
    [SerializeField] private Button firstSelected;

    protected virtual void OnEnable()
    {
        SelectFirstSelected(firstSelected);
    }

    public void SelectFirstSelected(Button firstSelectedButton)
    {
        firstSelectedButton.Select();
    }
}
