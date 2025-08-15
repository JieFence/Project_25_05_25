using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

public class QuestLogButton : MonoBehaviour, ISelectHandler
{
    public Button button { get; private set; }
    private TextMeshProUGUI buttonText;
    private UnityAction onSelectAction;

    public void Initialize(string displayName, UnityAction selectAction)
    {
        this.button = this.GetComponent<Button>();
        this.buttonText = this.GetComponentInChildren<TextMeshProUGUI>();
        this.buttonText.text = displayName;
        this.onSelectAction = selectAction;
    }

    public void OnSelect(BaseEventData eventData)
    {
        onSelectAction();
    }

    public void SetState(QuestState questState)
    {
        switch (questState)
        {
            case QuestState.REQUIREMENTS_NOT_MET:
                buttonText.color = Color.red;
                break;
            case QuestState.CAN_START:
                buttonText.color = HexToColor("#d9ff00ff");
                break;
            case QuestState.IN_PROGRESS:
                buttonText.color = HexToColor("#ff00ffff");
                break;
            case QuestState.CAN_FINISH:
                buttonText.color = HexToColor("#00ccffff");
                break;
            case QuestState.FINISHED:
                buttonText.color = Color.green;
                break;
            default:
                Debug.Log("Quest State not recognized by switch statement:" + questState);
                break;
        }
    }

    // 将十六进制颜色 如#FF5733 转换为 Color
    private Color HexToColor(string hex)
    {
        Color color;
        if (ColorUtility.TryParseHtmlString(hex, out color))
        {
            return color;
        }
        return Color.white; // 默认返回白色
    }

}
