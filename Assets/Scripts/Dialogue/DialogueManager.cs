using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;
using System;
using UnityEngine.EventSystems;

public class DialogueManager : MonoBehaviour
{


    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;

    [Header("Choices UI")]
    [SerializeField] private GameObject[] choices;
    private TextMeshProUGUI[] choiceTexts;

    private Story currentStory;
    public bool isDialoguePlaying { get; private set; }


    private static DialogueManager instance;


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
    }

    private void Start()
    {
        isDialoguePlaying = false;
        dialoguePanel.SetActive(false);

        choiceTexts = new TextMeshProUGUI[choices.Length];
        int index = 0;
        foreach (GameObject choice in choices)
        {
            choiceTexts[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++;
        }
    }

    private void Update()
    {
        if (!isDialoguePlaying)
        {
            return;
        }

        if (InputManager.instance.GetSubmitPressed())
        {
            Debug.Log("Submit Pressed");
            ContinueStory();
        }
    }

    public static DialogueManager GetInstance()
    {
        return instance;
    }

    public void EnterDialogueMode(TextAsset inkJson)
    {
        //进入UI的对话模式，关闭Playr Map
        InputManager.instance.jieFenceInputSystem.Player.Disable();

        //
        currentStory = new Story(inkJson.text);
        isDialoguePlaying = true;
        dialoguePanel.SetActive(true);

        ContinueStory();
    }

    private void ContinueStory()
    {
        //如果故事还没讲完，就进入下一个叙述内容。（一行文字，或是）
        if (currentStory.canContinue)
        {
            Debug.Log("currentStory.canContinue:" + currentStory.canContinue.ToString());

            dialogueText.text = currentStory.Continue();
            DisplayChoices();

            Debug.Log("currentStory.canContinue:" + currentStory.canContinue.ToString());

        }
        else
        {
            Debug.Log("currentStory.canContinue:" + currentStory.canContinue.ToString());
            ExitDialogueMode();
        }
    }
    
    private void ExitDialogueMode()
    {
        InputManager.instance.jieFenceInputSystem.Player.Enable();

        isDialoguePlaying = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = string.Empty;
    }

    private void DisplayChoices()
    {
        List<Choice> currentChoices = currentStory.currentChoices;
        if (currentChoices.Count > choices.Length)
        {
            Debug.LogWarning("Not enough choice UI elements to display all choices.");
            return;
        }

        int index = 0;
        foreach (Choice choice in currentChoices)
        {
            choices[index].gameObject.SetActive(true);
            choiceTexts[index].text = choice.text;
            index++;
        }

        for (int i = index; i < choices.Length; i++)
        {
            choices[i].gameObject.SetActive(false);
        }

        StartCoroutine(SelectFirstChoice());
    }


    private IEnumerator SelectFirstChoice()
    {
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();

        EventSystem.current.SetSelectedGameObject(choices[0].gameObject);
    }

    public void MakeChoice(int choiceIndex)
    {
        currentStory.ChooseChoiceIndex(choiceIndex);
    } 
}
