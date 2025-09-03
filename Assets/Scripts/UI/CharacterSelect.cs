using FMOD.Studio;
using UnityEngine;
using UnityEngine.UIElements;

public class CharacterSelect : MonoBehaviour
{
	private EventInstance buttonsfx;


	private UIDocument uiDocument;
    private Button submitButton;

	private RadioButtonGroup rbgplayer1;
	private RadioButtonGroup rbgplayer2;

	void Start()
    {
		uiDocument = GetComponent<UIDocument>();
		submitButton = uiDocument.rootVisualElement.Q<Button>("btnsubmit");

		rbgplayer1 = uiDocument.rootVisualElement.Q<RadioButtonGroup>("rbgplayer1");
		rbgplayer2 = uiDocument.rootVisualElement.Q<RadioButtonGroup>("rbgplayer2");

		buttonsfx = AudioManager.instance.CreateInstance(FMODEvents.instance.UIClick);

		submitButton.clicked += () => GoToLevel();
	}

	public void GoToLevel()
	{
		Debug.Log(rbgplayer1.value);


		buttonsfx.start();


		switch (rbgplayer1.value)
		{
			case 0:
				GameManager.Instance.selectedCharacter1 = "KnightPrefab";
				break;
			case 1:
				GameManager.Instance.selectedCharacter1 = "MagePrefab";
				break;
			default:
				return;
		}

		switch (rbgplayer2.value)
		{
			case 0:
				GameManager.Instance.selectedCharacter2 = "KnightPrefab";
				break;
			case 1:
				GameManager.Instance.selectedCharacter2 = "MagePrefab";
				break;
			default:
				break;
		}

		GameManager.Instance.GoToLevel(GameManager.Instance.selectedLevel);
	}
}
