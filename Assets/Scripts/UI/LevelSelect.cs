using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class LevelSelect : MonoBehaviour
{

    private UIDocument uiDocument;
    private Button level1Button;
    private Button CloseButton;
    private Button SettingsButton;
    private Button SettingsCloseButton;
    private VisualElement settings;

    private bool settingsOpen = false;

    void Start()
    {
        uiDocument = GetComponent<UIDocument>();
        level1Button = uiDocument.rootVisualElement.Q<Button>("btnlvl1");
        CloseButton = uiDocument.rootVisualElement.Q<Button>("btnclose");
        settings = uiDocument.rootVisualElement.Q<VisualElement>("Settings");
        SettingsButton = uiDocument.rootVisualElement.Q<Button>("btnsettings");
        SettingsCloseButton = uiDocument.rootVisualElement.Q<Button>("btnsettingsclose");

        level1Button.clicked += () => GoToLevel(3);
        CloseButton.clicked += () => Close();
        SettingsButton.clicked += () => Settings();
        SettingsCloseButton.clicked += () => Settings();


        settings.style.visibility = Visibility.Hidden;
    }
    public void GoToLevel(int index)
    {
        GameManager.Instance.selectedLevel = index;
        //Debug.Log("Loading level: " + name);
        GameManager.Instance.GoToLevel(1);
    }

    public void Close()
    {
        Application.Quit();
    }

    public void Settings()
    {
        if (settingsOpen)
        {
            settings.style.visibility = Visibility.Hidden;
            settingsOpen = false;
        }
        else
        {
            settings.style.visibility = Visibility.Visible;
            settingsOpen = true;
        }
    }
}
