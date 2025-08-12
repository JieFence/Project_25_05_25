using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : Menu
{
    [Header("Menu Navigation")]
    [SerializeField] private SaveSlotsMenu saveSlotsMenu;
    [Header("Menu Buttons")]
    [SerializeField] private Button newGameButton;
    [SerializeField] private Button continueGameButton;
    [SerializeField] private Button loadGameButton;

    private void Start()
    {
        DisableButtonsDependingOnData();
    }
    private void DisableButtonsDependingOnData()
    {
        // if there is no game data, disable the continue button
        if (!DataPersistenceManager.instance.HasGameData())
        {
            continueGameButton.interactable = false;
            loadGameButton.interactable = false;
        }
    }

    public void OnNewGameClicked()
    {
        DeactivateMenu();
        saveSlotsMenu.ActivateMenu(false);
    }

    public void OnLoadGameClicked()
    {
        DeactivateMenu();
        saveSlotsMenu.ActivateMenu(true);
    }

    public void OnContinueGameClicked()
    {
        DisableMenuButtons();
        //save the game anytime before loading the scene
        DataPersistenceManager.instance.SaveGame();

        // load the gameplay scene - which will in turn load the game data because of 
        // OnSceneLoaded() in DataPersistenceManager
        SceneManager.LoadSceneAsync("Scene01");
    }

    private void DisableMenuButtons()
    {
        newGameButton.interactable = false;
        continueGameButton.interactable = false;
    }

    public void ActivateMenu()
    {
        gameObject.SetActive(true);
        DisableButtonsDependingOnData();
    }   
    public void DeactivateMenu()
    {
        gameObject.SetActive(false);
    }
}
