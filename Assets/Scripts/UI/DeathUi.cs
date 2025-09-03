using FMOD.Studio;
using UnityEngine;
using UnityEngine.UIElements;

public class DeathUi : MonoBehaviour
{
	private EventInstance buttonsfx;


	private UIDocument uiDocument;

	private Button respawnButton;

    void Start()
    {
		uiDocument = GetComponent<UIDocument>();
		respawnButton = uiDocument.rootVisualElement.Q<Button>("btnrespawn");

		respawnButton.clicked += () => Respawn();
	}


	public void Respawn()
	{
		buttonsfx.start();
		GameManager.Instance.Respawn();
	}
	
}
