using UnityEngine;
using UnityEngine.UIElements;

public class CharacterSelect : MonoBehaviour
{
	private UIDocument uiDocument;
    private Button fighterButton;
    private Button wizardButton;
	void Start()
    {
		uiDocument = GetComponent<UIDocument>();
		fighterButton = uiDocument.rootVisualElement.Q<Button>("btnfighter");
		wizardButton = uiDocument.rootVisualElement.Q<Button>("btnwizard");

		fighterButton.clicked += () => GoToLevel("KnightPrefab");
		wizardButton.clicked += () => GoToLevel("MagePrefab");
	}

	public void GoToLevel(string name)
	{
		GameManager.Instance.selectedCharacter1 = name;
		Debug.Log("Loading Character: " + name);
		GameManager.Instance.GoToLevel(GameManager.Instance.selectedLevel);
	}
}
