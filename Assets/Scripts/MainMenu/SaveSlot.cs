using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SaveSlot : MonoBehaviour
{
    [Header("Profile")]
    [SerializeField] private string profileId = "";

    [Header("Content")]
    [SerializeField] private GameObject noDataContent;
    [SerializeField] private GameObject hasDataContent;
    [SerializeField] private TextMeshProUGUI deathCountText;

    public void SetData(GameData data)
    {
        // if there is no data, show the no data content
        if (data == null)
        {
            noDataContent.SetActive(true);
            hasDataContent.SetActive(false);
        }
        else
        {
            // if there is data, show the has data content
            noDataContent.SetActive(false);
            hasDataContent.SetActive(true);
            deathCountText.text = "DEATH COUNT: " + data.deathCount.ToString();
        }
    }
    public string GetProfileId()
    {
        return profileId;
    }
}
