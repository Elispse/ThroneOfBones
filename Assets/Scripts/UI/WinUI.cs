using FMOD.Studio;
using UnityEngine;
using UnityEngine.UIElements;

public class WinUI : MonoBehaviour
{
	private EventInstance buttonsfx;


	private UIDocument uiDocument;

	private Button restartButton;

    void Start()
    {
		uiDocument = GetComponent<UIDocument>();
		restartButton = uiDocument.rootVisualElement.Q<Button>("btnExit");

		restartButton.clicked += () => Restart();
	}


	public void Restart()
	{
		buttonsfx.start();
		GameManager.Instance.GoToLevel(0);
	}
	
}
