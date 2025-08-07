using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Header("Menu Buttons")]
    [SerializeField] private Button newGameButton;
    [SerializeField] private Button continueGameButton;

    private void Start()
    {
        // if there is no game data, disable the continue button
        if (!DataPersistenceManager.instance.HasGameData())
        {
            continueGameButton.interactable = false;
        }
    }
    public void OnNewGameClicked()
    {
        DisableMenuButtons();
        // create a new game - which will reset all game data
        DataPersistenceManager.instance.NewGame();
        // load the gameplay scene - which will in turn save the game data because of 
        // OnSceneUnloaded() in DataPersistenceManager
        SceneManager.LoadSceneAsync("Scene01");
    }
    public void OnContinueGameClicked()
    {
        DisableMenuButtons();
        // load the gameplay scene - which will in turn load the game data because of 
        // OnSceneLoaded() in DataPersistenceManager
        SceneManager.LoadSceneAsync("Scene01");
    }


    private void DisableMenuButtons()
    {
        newGameButton.interactable = false;
        continueGameButton.interactable = false;
    }
}
