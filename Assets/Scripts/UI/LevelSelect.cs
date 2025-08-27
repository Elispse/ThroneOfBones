using UnityEngine;
using UnityEngine.UIElements;

public class LevelSelect : MonoBehaviour
{
    private UIDocument uiDocument;
    private Button leveltestButton;
    private Button level1Button;
    private Button fivehundredwolvesButton;
    void Start()
    {
        uiDocument = GetComponent<UIDocument>();
        leveltestButton = uiDocument.rootVisualElement.Q<Button>("btnlvltest");
        level1Button = uiDocument.rootVisualElement.Q<Button>("btnlvl1");
        fivehundredwolvesButton = uiDocument.rootVisualElement.Q<Button>("btnlvlwolf");

        leveltestButton.clicked += () => GoToLevel("SampleScene");
        level1Button.clicked += () => GoToLevel("Level1");
        fivehundredwolvesButton.clicked += () => GoToLevel("500Wolves");


    }
    public void GoToLevel(string name)
    {
        Debug.Log("Loading level: " + name);
        UnityEngine.SceneManagement.SceneManager.LoadScene(name);
    }
}
