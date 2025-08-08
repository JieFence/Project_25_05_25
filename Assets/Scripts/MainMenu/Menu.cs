using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Menu : MonoBehaviour
{
    [Header("First Selected Menu")]
    [SerializeField] private GameObject firstSelectedMenu;

    protected virtual void OnEnable()
    {
        StartCoroutine(SelectFirstSelected(firstSelectedMenu));
    }

    public IEnumerator SelectFirstSelected(GameObject firstSelectedObject)
    {
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(firstSelectedObject);
    }
}
