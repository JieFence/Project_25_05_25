using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using Unity.VisualScripting;
public class DialogueVariables
{
    public Dictionary<string, Ink.Runtime.Object> variables { get; private set; }

    private Story globalVariablesStory;
    private const string saveVariableKey = "INK_VARIABLES";
    public DialogueVariables(TextAsset loadGlobalsJSON)
    {
        globalVariablesStory = new Story(loadGlobalsJSON.text);

        if (PlayerPrefs.HasKey(saveVariableKey))
        {
            string jsonState = PlayerPrefs.GetString(saveVariableKey);
            globalVariablesStory.state.LoadJson(jsonState);
        }

        variables = new Dictionary<string, Ink.Runtime.Object>();
        foreach (string name in globalVariablesStory.variablesState)
        {
            Ink.Runtime.Object value = globalVariablesStory.variablesState.GetVariableWithName(name);
            variables.Add(name, value);
            Debug.Log("Initialized global dialogue variables : " + name + "=" + value);
        }
    }

    public void SaveVariables()
    {
        if (globalVariablesStory != null)
        {
            VariableToStory(globalVariablesStory);
            PlayerPrefs.SetString(saveVariableKey, globalVariablesStory.state.ToJson());
        }
    }

    public void StartListening(Story story)
    {
        VariableToStory(story);//将C#的字典覆写到ink中，然后监听ink中全局变量的改变。
        story.variablesState.variableChangedEvent += VariableChanged;
    }
    public void StopListening(Story story)
    {
        story.variablesState.variableChangedEvent -= VariableChanged;
    }

    private void VariableChanged(string name, Ink.Runtime.Object value)
    {
        if (variables.ContainsKey(name))
        {
            variables.Remove(name);
            variables.Add(name, value);

        }
    }

    private void VariableToStory(Story story)
    {
        foreach (KeyValuePair<string, Ink.Runtime.Object> valuePair in variables)
        {
            story.variablesState.SetGlobal(valuePair.Key, valuePair.Value);
        }
    }
}
