using FMOD.Studio;
using FMODUnity;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class LevelSelect : MonoBehaviour
{
    private EventInstance playsfx;
    private EventInstance buttonsfx;

    private UIDocument uiDocument;
    private Button level1Button;
    private Button CloseButton;
    private Button SettingsButton;
    private Button SettingsCloseButton;
    private VisualElement settings;

    private Slider masterSlider;
    private Slider ambientSlider;
    private Slider musicSlider;
    private Slider soundSlider;


    private bool settingsOpen = false;

	void Start()
    {
        uiDocument = GetComponent<UIDocument>();
        level1Button = uiDocument.rootVisualElement.Q<Button>("btnlvl1");
        CloseButton = uiDocument.rootVisualElement.Q<Button>("btnclose");
        settings = uiDocument.rootVisualElement.Q<VisualElement>("Settings");
        SettingsButton = uiDocument.rootVisualElement.Q<Button>("btnsettings");
        SettingsCloseButton = uiDocument.rootVisualElement.Q<Button>("btnsettingsclose");

        masterSlider = uiDocument.rootVisualElement.Q<Slider>("sldMaster");
        ambientSlider = uiDocument.rootVisualElement.Q<Slider>("sldAmbient");
        musicSlider = uiDocument.rootVisualElement.Q<Slider>("sldMusic");
        soundSlider = uiDocument.rootVisualElement.Q<Slider>("sldSoundEffects");


        playsfx = AudioManager.instance.CreateInstance(FMODEvents.instance.UIPlayButton);
        buttonsfx = AudioManager.instance.CreateInstance(FMODEvents.instance.UIClick);


        level1Button.clicked += () => GoToLevel(2);
        CloseButton.clicked += () => Close();
        SettingsButton.clicked += () => Settings();
        SettingsCloseButton.clicked += () => Settings();


        settings.style.visibility = Visibility.Hidden;
    }

	private void Update()
	{

		AudioManager.instance.masterVolume = masterSlider.value;
		AudioManager.instance.ambienceVolume = ambientSlider.value;
		AudioManager.instance.musicVolume = musicSlider.value;
		AudioManager.instance.SFXVolume = soundSlider.value;
	}

	public void GoToLevel(int index)
    {
        playsfx.start();
        GameManager.Instance.selectedLevel = index;
        //Debug.Log("Loading level: " + name);
        GameManager.Instance.GoToLevel(1);
    }

    public void Close()
    {
        buttonsfx.start();
        Application.Quit();
    }

    public void Settings()
    {
        buttonsfx.start();
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
