using UnityEngine;
using UnityEngine.UIElements;

public class HUDscript : MonoBehaviour
{
	private UIDocument uiDocument;

	private VisualElement settings;

	private Button SettingsCloseButton;
	private Button ExitButton;
	
	private Slider masterSlider;
	private Slider ambientSlider;
	private Slider musicSlider;
	private Slider soundSlider;

	private bool settingsOpen = false;


	void Start()
    {
		uiDocument = GetComponent<UIDocument>();

		settings = uiDocument.rootVisualElement.Q<VisualElement>("Settings");

		SettingsCloseButton = uiDocument.rootVisualElement.Q<Button>("btnsettingsclose");
		ExitButton = uiDocument.rootVisualElement.Q<Button>("btnexit");

		masterSlider = uiDocument.rootVisualElement.Q<Slider>("sldMaster");
		ambientSlider = uiDocument.rootVisualElement.Q<Slider>("sldAmbient");
		musicSlider = uiDocument.rootVisualElement.Q<Slider>("sldMusic");
		soundSlider = uiDocument.rootVisualElement.Q<Slider>("sldSoundEffects");

		SettingsCloseButton.clicked += () => Settings();
		ExitButton.clicked += () => GoToLevel(0);

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
		GameManager.Instance.GoToLevel(index);
	}

	public void Settings()
	{
		if (settingsOpen)
		{
			GameManager.Instance.Unpause();
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
